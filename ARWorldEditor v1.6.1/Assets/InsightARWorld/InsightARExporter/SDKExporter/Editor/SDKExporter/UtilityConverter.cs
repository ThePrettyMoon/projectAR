/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.IO;
using System.IO.Compression;

namespace RenderEngine
{
	class UtilityConverter
	{
		public static ProtoMath.matrix ConvertMatrix( UnityEngine.Matrix4x4 src )
		{
			ProtoMath.matrix ret = new ProtoMath.matrix();
			ret.W = 4;
			ret.H = 4;
			for( int y = 0 ; y < 4 ; ++y )
			{
				UnityEngine.Vector4 row = src.GetRow(y);
				ret.M.Add( row.x );
				ret.M.Add( row.y );
				ret.M.Add( row.z );
				ret.M.Add( row.w );
			}
			return ret;
		}

		public static ProtoMath.float4 ConvertColor( UnityEngine.Color c )
		{
			ProtoMath.float4 ret = new ProtoMath.float4
			{
				X = c.r, Y = c.g, Z = c.b, W = c.a
			};
			return ret;
		}

		public static ProtoMath.int4 ConvertColor32( UnityEngine.Color32 c )
		{
			ProtoMath.int4 ret = new ProtoMath.int4
			{
				X = c.r, Y = c.g, Z = c.b, W = c.a
			};
			return ret;
		}
		public static ProtoMath.transform ConvertTransform( UnityEngine.Transform t )
		{
			// get transform
			UnityEngine.Vector3 pos = t.localPosition;
			UnityEngine.Quaternion rotation = t.localRotation;
			UnityEngine.Vector3 scale = t.localScale;

			return new ProtoMath.transform
			{
				Scale = ConvertVector3( scale ) ,
				RotType = ProtoMath.transform.Types.ROTATION.Quaternion ,
				Rotation = ConvertVector4( rotation ) ,
				Translate = ConvertVector3( pos ) ,
			};
		}

		public static ProtoMath.RectTransform ConvertRectTransform(UnityEngine.RectTransform rectTransform)
		{
			ProtoMath.RectTransform ret = new ProtoMath.RectTransform
			{
				OffsetMax = UtilityConverter.ConvertVector2(rectTransform.offsetMax),
				OffsetMin = UtilityConverter.ConvertVector2(rectTransform.offsetMin),
				Pivot = UtilityConverter.ConvertVector2(rectTransform.pivot),
				SizeDelta = UtilityConverter.ConvertVector2(rectTransform.sizeDelta),
				AnchoredPosition = UtilityConverter.ConvertVector3(rectTransform.anchoredPosition3D),
				AnchorMax = UtilityConverter.ConvertVector2(rectTransform.anchorMax),
				Rect = UtilityConverter.ConvertRect(rectTransform.rect),
				AnchorMin = UtilityConverter.ConvertVector2(rectTransform.anchorMin),
			};

			return ret;
		}

		public static ProtoMath.float2 ConvertVector2( UnityEngine.Vector2 v )
		{
			return new ProtoMath.float2{ X = v.x , Y = v.y };
		}

		public static ProtoMath.float3 ConvertVector3( UnityEngine.Vector3 v )
		{
			return new ProtoMath.float3{ X = v.x , Y = v.y , Z = v.z };
		}

		public static ProtoMath.float4 ConvertVector4( UnityEngine.Vector4 v )
		{
			return new ProtoMath.float4{ X = v.x , Y = v.y , Z = v.z , W = v.w };
		}

		public static ProtoMath.float4 ConvertVector4( UnityEngine.Quaternion q )
		{
			return new ProtoMath.float4{ X = q.x , Y = q.y , Z = q.z , W = q.w };
		}

		public static ProtoMath.quaternion ConvertQuaternion( UnityEngine.Quaternion q )
		{
			return new ProtoMath.quaternion{ I = q.x , J = q.y , K = q.z , S = q.w };
		}

		public static ProtoMath.float4 ConvertRect(UnityEngine.Rect rect)
		{
			return new ProtoMath.float4{ X = rect.x, Y = rect.y, Z = rect.width, W = rect.height };
		}

		public static ProtoWorld.TEXTURE_WRAP ConvertWrap( UnityEngine.TextureWrapMode mode )
		{
			switch( mode )
			{
			default:
			case UnityEngine.TextureWrapMode.Repeat:
				return ProtoWorld.TEXTURE_WRAP.WrapRepeat;
			case UnityEngine.TextureWrapMode.Clamp:
				return ProtoWorld.TEXTURE_WRAP.WrapClamp;
				#if false
				case UnityEngine.TextureWrapMode.Mirror:
				case UnityEngine.TextureWrapMode.MirrorOnce:
				return ProtoWorld.TEXTURE_WRAP.WrapMirror;
				#endif
			}
		}

		public static ProtoWorld.TEXTURE_FILTER ConvertFilter( UnityEngine.FilterMode mode )
		{
			switch( mode )
			{
			case UnityEngine.FilterMode.Point:
				return ProtoWorld.TEXTURE_FILTER.FilterPoint;
			default:
			case UnityEngine.FilterMode.Bilinear:
				return ProtoWorld.TEXTURE_FILTER.FilterBilinear;
			case UnityEngine.FilterMode.Trilinear:
				return ProtoWorld.TEXTURE_FILTER.FilterTrilinear;
			}
		}

		public static ProtoWorld.TEXTURE_FORMAT ConvertFormat( UnityEngine.TextureFormat format )
		{
			//TODO complete the format convert
			switch( format )
			{
			default:
			case UnityEngine.TextureFormat.BGRA32:
			case UnityEngine.TextureFormat.ARGB32:
				return ProtoWorld.TEXTURE_FORMAT.FormatRgba8;
			case UnityEngine.TextureFormat.Alpha8:
			case UnityEngine.TextureFormat.R8:
				return ProtoWorld.TEXTURE_FORMAT.FormatR8;
			}
		}

		public static ProtoWorld.TEXTURE_FORMAT ConvertFormat( UnityEngine.RenderTextureFormat format )
		{
			//TODO complete the format convert
			switch( format )
			{
			default:
			case UnityEngine.RenderTextureFormat.Default:
			case UnityEngine.RenderTextureFormat.ARGB32:
			case UnityEngine.RenderTextureFormat.BGRA32:
				return ProtoWorld.TEXTURE_FORMAT.FormatRgba8;
			case UnityEngine.RenderTextureFormat.R8:
				return ProtoWorld.TEXTURE_FORMAT.FormatR8;
			}
		}
	}

	// code from https://answers.unity.com/questions/7789/calculating-tangents-vector4.html
	class TangentSolver
	{
		public static UnityEngine.Vector4[] Solve(UnityEngine.Mesh mesh)
		{
			int triangleCount = mesh.triangles.Length;
			int vertexCount = mesh.vertices.Length;

			UnityEngine.Vector3[] tan1 = new UnityEngine.Vector3[vertexCount];
			UnityEngine.Vector3[] tan2 = new UnityEngine.Vector3[vertexCount];
			UnityEngine.Vector4[] tangents = new UnityEngine.Vector4[vertexCount];
			for(long a = 0; a < triangleCount; a+=3)
			{
				long i1 = mesh.triangles[a+0];
				long i2 = mesh.triangles[a+1];
				long i3 = mesh.triangles[a+2];
				UnityEngine.Vector3 v1 = mesh.vertices[i1];
				UnityEngine.Vector3 v2 = mesh.vertices[i2];
				UnityEngine.Vector3 v3 = mesh.vertices[i3];
				UnityEngine.Vector2 w1 = mesh.uv[i1];
				UnityEngine.Vector2 w2 = mesh.uv[i2];
				UnityEngine.Vector2 w3 = mesh.uv[i3];
				float x1 = v2.x - v1.x;
				float x2 = v3.x - v1.x;
				float y1 = v2.y - v1.y;
				float y2 = v3.y - v1.y;
				float z1 = v2.z - v1.z;
				float z2 = v3.z - v1.z;
				float s1 = w2.x - w1.x;
				float s2 = w3.x - w1.x;
				float t1 = w2.y - w1.y;
				float t2 = w3.y - w1.y;
				float r = 1.0f / (s1 * t2 - s2 * t1);
				UnityEngine.Vector3 sdir = new UnityEngine.Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
				UnityEngine.Vector3 tdir = new UnityEngine.Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);
				tan1[i1] += sdir;
				tan1[i2] += sdir;
				tan1[i3] += sdir;
				tan2[i1] += tdir;
				tan2[i2] += tdir;
				tan2[i3] += tdir;
			}
			for( long a = 0; a < vertexCount; ++a )
			{
				UnityEngine.Vector3 n = mesh.normals[a];
				UnityEngine.Vector3 t = tan1[a];
				UnityEngine.Vector3 tmp = (t - n * UnityEngine.Vector3.Dot(n, t)).normalized;
				tangents[a] = new UnityEngine.Vector4(tmp.x, tmp.y, tmp.z);
				tangents[a].w = (UnityEngine.Vector3.Dot(UnityEngine.Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
			}
			return tangents;
		}
	}
}