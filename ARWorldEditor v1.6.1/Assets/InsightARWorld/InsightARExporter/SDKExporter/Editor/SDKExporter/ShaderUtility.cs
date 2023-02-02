/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;

namespace RenderEngine
{
	class ShaderUtility
	{
		public enum TYPE
		{
			Color ,
			Vector ,
			Float ,
			Range ,
			TexEnv ,
		}
			
		#if UNITY_EDITOR
		public static int GetPropertyCount( Shader shader )
		{
			return UnityEditor.ShaderUtil.GetPropertyCount( shader );
		}
		public static string GetPropertyName( Shader shader , int pi )
		{
			return UnityEditor.ShaderUtil.GetPropertyName( shader , pi );
		}
		public static TYPE GetPropertyType( Shader shader , int pi )
		{
			switch( UnityEditor.ShaderUtil.GetPropertyType( shader , pi ) )
			{
			case UnityEditor.ShaderUtil.ShaderPropertyType.Color:
				return TYPE.Color;
			case UnityEditor.ShaderUtil.ShaderPropertyType.Vector:
				return TYPE.Vector;
			case UnityEditor.ShaderUtil.ShaderPropertyType.Float:
				return TYPE.Float;
			case UnityEditor.ShaderUtil.ShaderPropertyType.Range:
				return TYPE.Range;
			case UnityEditor.ShaderUtil.ShaderPropertyType.TexEnv:
				return TYPE.TexEnv;
			}
			return TYPE.Vector;
		}
		public static float GetRangeLimits( Shader shader , int pi , int v )
		{
			return UnityEditor.ShaderUtil.GetRangeLimits( shader , pi , v );
		}
		#else
		private struct PROPERTY
		{
			public PROPERTY( string n , TYPE y )
			{
				name = n;
				type = y;
			}
			public string name;
			public TYPE type;
		}
		private static PROPERTY[] _properties =
		{
			new PROPERTY( "_Color" , TYPE.Color ) ,
			new PROPERTY( "_MainTex" , TYPE.TexEnv ) ,

			new PROPERTY( "_Cutoff" , TYPE.Range ) ,

			new PROPERTY( "_Glossiness" , TYPE.Range ) ,
			new PROPERTY( "_GlossMapScale" , TYPE.Range ) ,
			new PROPERTY( "_SmoothnessTextureChannel" , TYPE.Float ) ,

			new PROPERTY( "_Metallic" , TYPE.Range ) ,
			new PROPERTY( "_MetallicGlossMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_SpecularHighlights" , TYPE.Float ) ,
			new PROPERTY( "_GlossyReflections" , TYPE.Float ) ,

			new PROPERTY( "_BumpScale" , TYPE.Float ) ,
			new PROPERTY( "_BumpMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_Parallax" , TYPE.Range ) ,
			new PROPERTY( "_ParallaxMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_OcclusionStrength" , TYPE.Range ) ,
			new PROPERTY( "_OcclusionMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_EmissionColor" , TYPE.Color ) ,
			new PROPERTY( "_EmissionMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_DetailMask" , TYPE.TexEnv ) ,

			new PROPERTY( "_DetailAlbedoMap" , TYPE.TexEnv ) ,
			new PROPERTY( "_DetailNormalMapScale" , TYPE.Float ) ,
			new PROPERTY( "_DetailNormalMap" , TYPE.TexEnv ) ,

			new PROPERTY( "_UVSec" , TYPE.Float ) ,

			new PROPERTY( "_Mode" , TYPE.Float ) ,
			new PROPERTY( "_SrcBlend" , TYPE.Float ) ,
			new PROPERTY( "_DstBlend" , TYPE.Float ) ,
			new PROPERTY( "_ZWrite" , TYPE.Float ) ,
		};
		public static int GetPropertyCount( Shader shader )
		{
			return _properties.Length;
		}
		public static string GetPropertyName( Shader shader , int pi )
		{
			return _properties[pi].name;
		}
		public static TYPE GetPropertyType( Shader shader , int pi )
		{
			return _properties[pi].type;
		}
		public static float GetRangeLimits( Shader shader , int pi , int v )
		{
		return 0;
		}
		#endif
	}
}
