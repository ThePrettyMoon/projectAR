using System.Collections;
using System.Collections.Generic;

namespace RenderEngine
{
	class ConverterPhysics
	{
		public static ProtoWorld.ColliderBox ConvertCollider( UnityEngine.BoxCollider box )
		{
			ProtoMath.float3 center = UtilityConverter.ConvertVector3( box.center );
			ProtoMath.float3 size = UtilityConverter.ConvertVector3( box.size );
			return new ProtoWorld.ColliderBox{ Enabled = box.enabled , HasEnabled = true, Center = center , Size = size , PhysicsMaterial = ConvertPhysicsMaterial( box.sharedMaterial ) , IsTrigger = box.isTrigger };
		}

		public static ProtoWorld.ColliderSphere ConvertCollider( UnityEngine.SphereCollider sphere )
		{
			ProtoMath.float3 center = UtilityConverter.ConvertVector3( sphere.center );
			float radius = sphere.radius;
			return new ProtoWorld.ColliderSphere{ Enabled = sphere.enabled , HasEnabled = true, Center = center , Radius = radius , PhysicsMaterial = ConvertPhysicsMaterial( sphere.sharedMaterial ) , IsTrigger = sphere.isTrigger };
		}

		private static ProtoWorld.PHYSICS_COMBINE Convert( UnityEngine.PhysicMaterialCombine s )
		{
			switch( s )
			{
			default:
			case UnityEngine.PhysicMaterialCombine.Average:
				return ProtoWorld.PHYSICS_COMBINE.Average;
			case UnityEngine.PhysicMaterialCombine.Minimum:
				return ProtoWorld.PHYSICS_COMBINE.Minimum;
			case UnityEngine.PhysicMaterialCombine.Maximum:
				return ProtoWorld.PHYSICS_COMBINE.Maximum;
			case UnityEngine.PhysicMaterialCombine.Multiply:
				return ProtoWorld.PHYSICS_COMBINE.Multiply;
			}
		}
		
		public static ProtoWorld.PhysicsMaterial ConvertPhysicsMaterial( UnityEngine.PhysicMaterial src )
		{
			ProtoWorld.PhysicsMaterial dest = new ProtoWorld.PhysicsMaterial();
			if( null != src )
			{
				dest.StaticFriction = src.staticFriction;
				dest.DynamicFriction = src.dynamicFriction;
				dest.Restitution = src.bounciness;
				dest.FrictionCombine = Convert( src.frictionCombine );
				dest.RestitutionCombine = Convert( src.bounceCombine );
			}
			else
			{
				dest.StaticFriction = 1;
				dest.DynamicFriction = 1;
				dest.Restitution = 0;
				dest.FrictionCombine = ProtoWorld.PHYSICS_COMBINE.Average;
				dest.RestitutionCombine = ProtoWorld.PHYSICS_COMBINE.Average;
			}
			return dest;
		}

		public static ProtoWorld.HingeJoint ConvertHingJoint( UnityEngine.Transform hinged_object , UnityEngine.HingeJoint src )
		{
			ProtoWorld.HingeJoint dest = new ProtoWorld.HingeJoint()
			{
				Enabled = true ,

				Anchor = UtilityConverter.ConvertVector3( src.anchor ) ,
				Axis = UtilityConverter.ConvertVector3( src.axis ) ,

				UseContinuousMotor = src.useMotor ,
				ContinuousMotorTargetVelocity = src.motor.targetVelocity ,
				ContinuousMotorMaxForce = src.motor.force ,

				UseLimits = src.useLimits ,
				LimitsMin = src.limits.min ,
				LimitsMax = src.limits.max ,
			};
			if( null != src.connectedBody )
			{
				bool found = false;
				string path = ExporterUtility.FindRelativePath( src.connectedBody.transform , hinged_object, out found );
				if (found)
					dest.ConnectedBody = path;
				else
					UnityEditor.EditorUtility.DisplayDialog (ExporterConfig.TITLE
						, "can not find relative path to " + src.connectedBody.name + ". the hinge joint will attach to the static scene."
						, "OK");
			}
			return dest;
		}

		public static ProtoWorld.Rigidbody ConvertRigidbody( UnityEngine.Rigidbody src )
		{
			ProtoWorld.Rigidbody dest = new ProtoWorld.Rigidbody();
			if( null != src )
			{
				dest.Enabled = true;
				dest.Mass = src.mass;
				dest.LinearDrag = src.drag;
				dest.AngularDrag = src.angularDrag;
				dest.UseGravity = src.useGravity;
				dest.IsStatic = false;
				dest.IsKinematic = src.isKinematic;
			}
			else
			{
				dest.Enabled = true;
				dest.Mass = 0;
				dest.LinearDrag = 0;
				dest.AngularDrag = 0;
				dest.UseGravity = false;
				dest.IsStatic = true;
				dest.IsKinematic = false;
			}
			return dest;
		}
	}
}