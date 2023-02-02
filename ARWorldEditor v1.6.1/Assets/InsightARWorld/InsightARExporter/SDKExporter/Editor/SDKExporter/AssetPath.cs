/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;
using System.Collections;

namespace RenderEngine
{
	class AssetPath
	{
		#if UNITY_EDITOR
		public string GetPath( UnityEditor.SceneAsset scene )
		{
			return UnityEditor.AssetDatabase.GetAssetPath( scene );
		}
		#endif
			
		public string GetPath( Shader shader )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( shader );
			#else
			return shader.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Material material )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( material );
			#else
			return material.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Texture texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( UnityEngine.Video.VideoClip video )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( video );
			#else
			return video.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Texture2D texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Texture3D texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Cubemap texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Texture2DArray texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Mesh mesh )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( mesh );
			#else
			return mesh.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( AnimationClip animation_clip )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( animation_clip );
			#else
			return animation_clip.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( Animator animator )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( animator );
			#else
			return animator.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( RuntimeAnimatorController controller )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( controller );
			#else
			return controller.GetInstanceID().ToString();
			#endif
		}

		public string GetPath( AudioClip clip )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( clip );
			#else
			return clip.GetInstanceID().ToString();
			#endif
		}

		/*
		public string GetPath( ExporterTexture texture )
		{
			#if UNITY_EDITOR
			return UnityEditor.AssetDatabase.GetAssetPath( texture );
			#else
			return texture.GetInstanceID().ToString();
			#endif
		}*/

		public string GetPath( ParticleSystem ps )
		{			
			return "particles/" + ps.GetInstanceID().ToString();			
		}
	}
}
