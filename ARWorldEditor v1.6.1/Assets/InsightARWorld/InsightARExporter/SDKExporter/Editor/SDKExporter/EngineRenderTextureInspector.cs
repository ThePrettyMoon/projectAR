using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEngine;
using UnityEditor;

namespace RenderEngine
{
	[CustomEditor( typeof( EngineRenderTexture ) ) ]
	public class EngineRenderTextureInspector : Editor
	{
		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EngineRenderTexture texture = (EngineRenderTexture)target;
			//DrawDefaultInspector();

			EditorGUILayout.PropertyField( serializedObject.FindProperty( "size_type" ) , new GUIContent( "Size Type" ) );
			switch( texture.size_type )
			{
			case RENDER_TEXTURE_SIZE_TYPE.AbsoluteSizeInPixel:
				EditorGUILayout.PropertyField( serializedObject.FindProperty( "abs_width" ) , new GUIContent( "Absolute Width" ) );
				EditorGUILayout.PropertyField( serializedObject.FindProperty( "abs_height" ) , new GUIContent( "Absolute Height" ) );
				break;
			case RENDER_TEXTURE_SIZE_TYPE.RelativeToScreen:
				EditorGUILayout.PropertyField( serializedObject.FindProperty( "rel_width" ) , new GUIContent( "Relative Width" ) );
				EditorGUILayout.PropertyField( serializedObject.FindProperty( "rel_height" ) , new GUIContent( "Relative Height" ) );
				break;

			default:
				break;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}