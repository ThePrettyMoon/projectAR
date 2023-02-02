/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.IO;
using System.IO.Compression;

namespace RenderEngine
{
	class UtilityDataCopy
	{
		public static Google.Protobuf.ByteString ColorsToByteStringR( UnityEngine.Color[] src )
		{
			const int STRIDE = 1;
			byte[] data = new byte[ src.Length * STRIDE ];
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Color c = src[i];
				data[i * STRIDE + 0] = (byte)(0xFF * c.r);
			}
			return Google.Protobuf.ByteString.CopyFrom( data );
		}

		public static Google.Protobuf.ByteString ColorsToByteStringRGB( UnityEngine.Color[] src )
		{
			const int STRIDE = 3;
			byte[] data = new byte[ src.Length * STRIDE ];
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Color c = src[i];
				data[i * STRIDE + 0] = (byte)(0xFF * c.r);
				data[i * STRIDE + 1] = (byte)(0xFF * c.g);
				data[i * STRIDE + 2] = (byte)(0xFF * c.b);
			}
			return Google.Protobuf.ByteString.CopyFrom( data );
		}

		public static Google.Protobuf.ByteString ColorsToByteStringRGBA( UnityEngine.Color[] src )
		{
			const int STRIDE = 4;
			byte[] data = new byte[ src.Length * STRIDE ];
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Color c = src[i];
				data[i * STRIDE + 0] = (byte)(0xFF * c.r);
				data[i * STRIDE + 1] = (byte)(0xFF * c.g);
				data[i * STRIDE + 2] = (byte)(0xFF * c.b);
				data[i * STRIDE + 3] = (byte)(0xFF * c.a);
			}
			return Google.Protobuf.ByteString.CopyFrom( data );
		}

		public static Google.Protobuf.ByteString ColorsToByteStringA( UnityEngine.Color[] src )
		{
			const int STRIDE = 1;
			byte[] data = new byte[ src.Length * STRIDE ];
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Color c = src[i];
				data[i * STRIDE + 0] = (byte)(0xFF * c.a);
			}
			return Google.Protobuf.ByteString.CopyFrom( data );
		}

		public static void CopyFloat2s
		( Google.Protobuf.Collections.RepeatedField< ProtoMath.float2 > dest
			, UnityEngine.Vector2[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Vector2 v = src[i];
				dest.Add( new ProtoMath.float2{ X = v.x , Y = v.y } );
			}
		}

		public static void CopyFloat3s
		( Google.Protobuf.Collections.RepeatedField< ProtoMath.float3 > dest
			, UnityEngine.Vector3[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Vector3 v = src[i];
				dest.Add( new ProtoMath.float3{ X = v.x , Y = v.y , Z = v.z } );
			}
		}

		public static void CopyFloat4s
		( Google.Protobuf.Collections.RepeatedField< ProtoMath.float4 > dest
			, UnityEngine.Vector4[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Vector4 v = src[i];
				dest.Add( new ProtoMath.float4{ X = v.x , Y = v.y , Z = v.z , W = v.w } );
			}
		}

		public static void CopyColors
		( Google.Protobuf.Collections.RepeatedField< ProtoMath.float4 > dest
			, UnityEngine.Color[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Color v = src[i];
				dest.Add( new ProtoMath.float4{ X = v.r , Y = v.g , Z = v.b , W = v.a } );
			}
		}

		public static void CopyInt4s
		( Google.Protobuf.Collections.RepeatedField< ProtoMath.int4 > dest
			, UnityEngine.Vector4[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				UnityEngine.Vector4 v = src[i];
				dest.Add( new ProtoMath.int4{ X = (int)v.x , Y = (int)v.y , Z = (int)v.z , W = (int)v.w } );
			}
		}

		public static void CopyString
		( Google.Protobuf.Collections.RepeatedField< string > dest, string[] src )
		{
			for( int i = 0 ; i < src.Length ; ++i )
			{
				dest.Add( src[i] );
			}
		}
	}
}