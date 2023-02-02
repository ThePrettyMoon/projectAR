using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

namespace RenderEngine
{
	static class ConverterAnimationController
	{
		public static ProtoWorld.ANIMATOR_CONDITION_MODE ConvertConditionMode( AnimatorConditionMode src )
		{
			switch( src )
			{
			case AnimatorConditionMode.If:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.If;
			case AnimatorConditionMode.IfNot:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.IfNot;
			case AnimatorConditionMode.Greater:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.Greater;
			case AnimatorConditionMode.Less:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.Less;
			case AnimatorConditionMode.Equals:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.Equals;
			case AnimatorConditionMode.NotEqual:
				return ProtoWorld.ANIMATOR_CONDITION_MODE.NotEqual;
			default:
				break;
			}
			return ProtoWorld.ANIMATOR_CONDITION_MODE.Count;
		}

		private static int FindParam( ProtoWorld.AnimatorController controller, string name )
		{
			for( int pi = 0 ; pi < controller.Params.Count ; ++pi )
			{
				if( controller.Params[pi].Name == name )
					return pi;
			}
			UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, "can not find param " + name + " in controller " + controller.Name, "OK!");
			return 0;
		}

		private static AnimatorStateMachine FindStateMachineFromState( AnimatorStateMachine machine, AnimatorState state )
		{
			foreach( ChildAnimatorState s in machine.states )
			{
				if( s.state == state )
					return machine;
			}

			foreach( ChildAnimatorStateMachine sasm in machine.stateMachines )
			{
				AnimatorStateMachine r = FindStateMachineFromState( sasm.stateMachine, state );
				if( null != r )
					return r;
			}
			return null;
		}

		private static AnimatorStateMachine FindStateMachineFromState( AnimatorController controller, AnimatorState state )
		{
			foreach( AnimatorControllerLayer li in controller.layers )
			{
				AnimatorStateMachine asm = FindStateMachineFromState( li.stateMachine, state );
				if( null != asm )
					return asm;
			}
			return null;
		}

		public static ProtoWorld.AnimatorCondition ConvertCondition( ExporterConfig config, ProtoWorld.AnimatorController controller, AnimatorCondition src )
		{
			ProtoWorld.AnimatorCondition dest = new ProtoWorld.AnimatorCondition()
			{
				Mode = ConvertConditionMode( src.mode ),
				Param = FindParam( controller, src.parameter ),
				Threshold = src.threshold
			};
			return dest;
		}

		// 
		// |               *=========*===============*
		// ^               ^         ^               ^
		// abs_time  abs_time_pos  normalized_time_offset
		//                 |<-------time_length----->|
		// |<-------return---------->|
		//
		// abs_time: current time in absolute timespace
		// abs_time_pos: start time in absolute timespace
		// normalized_time_offset: normalized time offset in the entire animation clip
		// time_length: relative length of the animation clip
		//
		//public static float GetTimeDelay( float abs_time, float abs_time_pos, float normalized_time_offset, float time_length, float speed )
		//{
		//	return ( abs_time_pos - abs_time ) / speed + normalized_time_offset * time_length;
		//}

		public static ProtoWorld.AnimatorTransition ConvertStateTransition
			( ExporterConfig config
			, ProtoWorld.AnimatorController controller
			, AnimatorController src_controller
			, AnimatorState src_state
			, AnimatorStateTransition src )
		{
			ProtoWorld.AnimatorTransition dest = new ProtoWorld.AnimatorTransition(){ Name = src.name };

			AnimationClip src_clip = (AnimationClip)src_state.motion;

			float transition_time_offset = -src.exitTime * src_clip.length / src_state.speed;
			float transition_time_duration = src.duration * src_clip.length / src_state.speed;
			if( src.hasFixedDuration )
				transition_time_duration = src.duration;
			
			float dest_time_offset = 0;
			if( null != src.destinationStateMachine )
			{
				// transition to a state machine, the state is uncertain.
				dest.DestStateMachine = src.destinationStateMachine.name;
				dest.DestState = "";
				dest_time_offset = src.offset; // the time offset is normalized
			}
			if( null != src.destinationState )
			{
				// transition to a state
				dest.DestStateMachine = FindStateMachineFromState( src_controller, src.destinationState ).name;
				dest.DestState = src.destinationState.name;
				AnimationClip dest_clip = (AnimationClip)src.destinationState.motion;
				dest_time_offset = transition_time_offset * src.destinationState.speed + src.offset * dest_clip.length; // the time offset is in dest state timespace
			}
			dest.TransitionTimeOffset = transition_time_offset;
			dest.TransitionTimeDuration = transition_time_duration;
			dest.DestTimeOffset = dest_time_offset;

			foreach( AnimatorCondition condition in src.conditions )
			{
				ProtoWorld.AnimatorCondition dest_condition = ConvertCondition( config, controller, condition );
				dest.Conditions.Add( dest_condition );
			}
			return dest;
		}

		public static ProtoWorld.AnimatorTransition ConvertTransition
			( ExporterConfig config
			, ProtoWorld.AnimatorController controller
			, AnimatorController src_controller
			, AnimatorTransition src )
		{
			ProtoWorld.AnimatorTransition dest = new ProtoWorld.AnimatorTransition()
			{
				Name = src.name,
				DestTimeOffset = 0,
				TransitionTimeOffset = 0,
				TransitionTimeDuration = 0,
			};

			if( null != src.destinationStateMachine )
			{
				dest.DestStateMachine = src.destinationStateMachine.name;
				dest.DestState = null;
			}
			else if( null != src.destinationState )
			{
				dest.DestStateMachine = FindStateMachineFromState( src_controller, src.destinationState ).name;
				dest.DestState = src.destinationState.name;
			}

			foreach( AnimatorCondition condition in src.conditions )
			{
				ProtoWorld.AnimatorCondition dest_condition = ConvertCondition( config, controller, condition );
				dest.Conditions.Add( dest_condition );
			}
			return dest;
		}

		public static ProtoWorld.AnimatorState ConvertState
			( ExporterConfig config, ProtoWorld.AnimatorController controller
			, AnimatorController src_controller, ChildAnimatorState src )
		{
			ProtoWorld.AnimatorState dest = new ProtoWorld.AnimatorState()
			{
				Name = src.state.name,
				Animation = src.state.motion.name, //HERE
				Speed = Mathf.Abs( src.state.speed ),
				Revert = src.state.speed < 0,
			};

			foreach( AnimatorStateTransition transition in src.state.transitions )
			{
				ProtoWorld.AnimatorTransition dest_transition = ConvertStateTransition( config, controller, src_controller, src.state, transition );
				dest.Transitions.Add( dest_transition );
			}
			return dest;
		}

		public static ProtoWorld.AnimatorStateMachine ConvertStateMachine
			( ExporterConfig config, ref ProtoWorld.AnimatorController controller
			, AnimatorController src_controller, AnimatorStateMachine src )
		{
			ProtoWorld.AnimatorStateMachine dest = new ProtoWorld.AnimatorStateMachine(){ Name = src.name };

			if( null != src.defaultState )
			{
				dest.DefaultState = src.defaultState.name;
			}

			foreach( AnimatorTransition entry in src.entryTransitions )
			{
				ProtoWorld.AnimatorTransition dest_transition = ConvertTransition( config, controller, src_controller, entry );
				dest.EntryTransitions.Add( dest_transition );
			}

			foreach( ChildAnimatorState state in src.states )
			{
				ProtoWorld.AnimatorState dest_state = ConvertState( config, controller, src_controller, state );
				dest.States.Add( dest_state );
			}

			foreach( ChildAnimatorStateMachine machine in src.stateMachines )
			{
				ProtoWorld.AnimatorStateMachine dest_machine = ConvertStateMachine( config, ref controller, src_controller, machine.stateMachine );
				controller.StateMachines.Add( dest_machine );
			}
			return dest;
		}

		public static ProtoWorld.AnimatorController ConvertAnimatorController
			( ExporterConfig config
			, Animator animator )
		{
			if( null == animator.runtimeAnimatorController )
				return null;
			
			AnimatorController src = UnityEditor.AssetDatabase.LoadAssetAtPath< AnimatorController >
				( config.asset_path.GetPath( animator.runtimeAnimatorController ) );
			
			if( src == null )
			{
				UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE, animator.name + "'s AnimatorController is null." , "OK!");
				return null;
			}

			ProtoWorld.AnimatorController dest_controller = new ProtoWorld.AnimatorController() { Name = src.name };

			foreach( AnimatorControllerParameter param in src.parameters )
			{
				switch( param.type )
				{
				case AnimatorControllerParameterType.Bool:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultBool ? 1 : 0 } );
					break;
				case AnimatorControllerParameterType.Float:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultFloat } );
					break;
				case AnimatorControllerParameterType.Int:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultInt } );
					break;
				case AnimatorControllerParameterType.Trigger:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = 0 } );
					break;
				}
			}

			foreach( AnimatorControllerLayer layer in src.layers )
			{
				ProtoWorld.AnimatorLayer dest_layer = new ProtoWorld.AnimatorLayer(){ Name = layer.name, StateMachine = layer.stateMachine.name } ;
				dest_controller.Layers.Add( dest_layer );

				ProtoWorld.AnimatorStateMachine dest_state_machine = ConvertStateMachine( config, ref dest_controller, src, layer.stateMachine );
				dest_controller.StateMachines.Add( dest_state_machine );
			}
			return dest_controller;
		}

		/*public static ProtoWorld.AnimatorController ConvertAnimationController( ExporterConfig config , AnimatorController src )
		{

			ProtoWorld.AnimatorController dest_controller = new ProtoWorld.AnimatorController() { Name = src.name };

			foreach( AnimatorControllerParameter param in src.parameters )
			{
				switch( param.type )
				{
				case AnimatorControllerParameterType.Bool:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultBool ? 1 : 0 } );
					break;
				case AnimatorControllerParameterType.Float:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultFloat } );
					break;
				case AnimatorControllerParameterType.Int:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = param.defaultInt } );
					break;
				case AnimatorControllerParameterType.Trigger:
					dest_controller.Params.Add( new ProtoWorld.AnimatorParam(){ Name = param.name, DefaultValue = 0 } );
					break;
				}
			}

			foreach( AnimatorControllerLayer layer in src.layers )
			{
				ProtoWorld.AnimatorLayer dest_layer = new ProtoWorld.AnimatorLayer(){ Name = layer.name, StateMachine = layer.stateMachine.name } ;
				dest_controller.Layers.Add( dest_layer );

				ProtoWorld.AnimatorStateMachine dest_state_machine = ConvertStateMachine( config, ref dest_controller, layer.stateMachine );
				dest_controller.StateMachines.Add( dest_state_machine );
			}
			return dest_controller;
		}

		[MenuItem("Temp/Dump Animator", false, 1)]
		public static void DumpAnimator()
		{
			if( Selection.activeObject != null && Selection.activeObject.GetType() == typeof( AnimatorController ) )
			{
				ConvertAnimationController( null, (AnimatorController)Selection.activeObject );
			}
		}*/
	}
}