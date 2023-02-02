using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEngine;
using UnityEditor;

namespace RenderEngine
{
	[CustomEditor( typeof( EnginePostEffect ) ) ]
	public class EnginePostEffectInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			EnginePostEffect effect = (EnginePostEffect)target;
			serializedObject.Update();

			// material
			EditorGUILayout.PropertyField( serializedObject.FindProperty("_material") , new GUIContent( "Material" ) );
			EditorGUILayout.Space();

			// render targets
			if( null != effect._material )
			{
				// create Using Depth
				int num_property = ShaderUtility.GetPropertyCount( effect._material.shader );
				if( null == effect._using_depth )
				{
					effect._using_depth = new bool[ num_property ];
				}
				else if( effect._using_depth.Length != num_property )
				{
					bool [] teud = new bool[num_property];
					for( int pi = 0 ; pi < num_property ; ++pi )
						teud[pi] = pi < effect._using_depth.Length ? effect._using_depth[pi] : false;
					effect._using_depth = teud;
				}

				// show
				ShowUsingDepth( serializedObject.FindProperty("_using_depth") , effect._material.shader );

				// create Targets
				int num_pass = effect._material.passCount;
				if( null == effect._targets )
				{
					effect._targets = new RenderTexture[ num_pass ];
				}
				else if( effect._targets.Length != num_pass )
				{
					RenderTexture [] tert = new RenderTexture[ num_pass ];
					for( int pi = 0 ; pi < num_pass ; ++pi )
						tert[pi] = pi < effect._targets.Length ? effect._targets[pi] : null;
					effect._targets = tert;
				}
				// show
				ShowTargets( serializedObject.FindProperty("_targets") );
			}
			else
			{
				effect._targets = null;
			}

			//
			serializedObject.ApplyModifiedProperties();
		}

		static void ShowUsingDepth( SerializedProperty list , Shader shader )
		{
			EditorGUILayout.PropertyField( list , new GUIContent( "Using Depth" ) );
			if( list.isExpanded )
			{
				for( int i = 0 ; i < list.arraySize ; i++ )
				{
					if( ShaderUtility.TYPE.TexEnv == ShaderUtility.GetPropertyType( shader , i ) )
					{
						EditorGUILayout.PropertyField( list.GetArrayElementAtIndex( i )
							, new GUIContent( ShaderUtility.GetPropertyName( shader , i ) ) );
					}
				}
			}
		}

		static void ShowTargets( SerializedProperty list )
		{
			EditorGUILayout.PropertyField( list , new GUIContent( "Targets" ) );
			if( list.isExpanded )
			{
				for( int i = 0 ; i < list.arraySize ; i++ )
				{
					EditorGUILayout.PropertyField( list.GetArrayElementAtIndex( i ) , new GUIContent( "Pass " + i ) );
				}
			}
		}
	}
}