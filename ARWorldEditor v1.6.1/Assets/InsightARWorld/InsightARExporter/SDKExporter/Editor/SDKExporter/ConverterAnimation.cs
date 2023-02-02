/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

namespace RenderEngine
{
	static class ConverterAnimation
	{
		public static ProtoWorld.Animation ConvertAnimation
			( string name , AnimationClip clip , string dest_file )
		{
			ProtoWorld.Animation ret = new ProtoWorld.Animation
			{
				Name = clip.name ,
				File = dest_file ,
				Length = clip.length ,
				FramePerSecond = clip.frameRate ,
			};

			ProtoWorld.Clip protoClip = new ProtoWorld.Clip
			{
				Name = clip.name ,
				Length = clip.length ,
				Loop = clip.isLooping ,
				FramePerSecond = clip.frameRate ,
				Start = 0
			};
			ret.Clips.Add(protoClip);

			// export curves
			EditorCurveBinding[] bindings = AnimationUtility.GetCurveBindings( clip );
			foreach( EditorCurveBinding binding in bindings )
			{
				AnimationCurve curve = UnityEditor.AnimationUtility.GetEditorCurve( clip , binding );

				string post_fix = null;
				string property = ExtractPropertyName(binding.propertyName, out post_fix);
				ProtoWorld.Curve pcurve = FindOrCreateCurve( ret , binding.path, binding.type.Name, 0 , property ); // index 0: only the first component can be animated in UnityEditor.
				MergeCurve( pcurve , curve , post_fix );
			}


			return ret;
		}

		private static void MergeCurve
			( ProtoWorld.Curve dest
			, AnimationCurve src
			, string post_fix )
		{
			// get merging dimension
			int index = 0;
			if( null != post_fix )
			{
				if( post_fix.Equals( "x" ) || post_fix.Equals( "r" )) index = 0;
				else if( post_fix.Equals( "y" ) || post_fix.Equals( "g" )) index = 1;
				else if( post_fix.Equals( "z" ) || post_fix.Equals( "b" )) index = 2;
				else if( post_fix.Equals( "w" ) || post_fix.Equals( "a" )) index = 3;
			}

			// update number of dimensions
			int old_dim = dest.Dim;
			if( dest.Dim <= index ) dest.Dim = index+1;
			int new_dim = dest.Dim;

			// prepare data
			List<float> ttime = new List<float>();
			List<float> tdata = new List<float>();

			// copy old data
			ttime.AddRange( dest.Time );
			Resize( tdata , dest.Time.Count * new_dim , float.NaN ); // use NaN to mark invalid data
			for( int ki = 0 ; ki < dest.Time.Count ; ++ki )
			{
				for( int di = 0 ; di < old_dim ; ++di )
				{
					int idx = ki * new_dim + di;
					tdata[ idx ] = dest.Data[ ki * old_dim + di ];
				}
			}

			// insert new data
			MergeKeyframes( ttime , tdata , src , index , old_dim , new_dim );

			// copy back to dest
			dest.Time.Clear();
			dest.Data.Clear();
			dest.Time.AddRange( ttime );
			dest.Data.AddRange( tdata );
		}

		private static void MergeKeyframes
			( List<float> dtime , List<float> ddata
			, AnimationCurve src
			, int index , int old_dim , int new_dim )
		{
			for( int ki = 0 ; ki < src.length ; ++ki )
			{
				float new_time = src.keys[ki].time;
				float new_value = src.keys[ki].value;

				int ti = MatchTime( dtime , new_time );
				if( ti < dtime.Count && dtime[ti] == new_time ) // replace old data
				{
					ddata[ti * new_dim + index] = new_value;
				}
				else // insert new data
				{
					// NOTICE: insert data before insert time,
					// because dtime is used when sampling
					float[] data = new float[new_dim];
					for( int di = 0 ; di < new_dim ; ++di )
						data[di] = ( index == di ? new_value : Sample( dtime , ddata , ti , new_time , di , new_dim ) );

					ddata.InsertRange( ti * new_dim , data );

					dtime.Insert( ti , new_time );
				}
			}

			// compensate missed times
			for( int ki = 0 ; ki < dtime.Count ; ++ki )
			{
				int i = ki * new_dim + index;
				if( float.IsNaN( ddata[i] ) )
					ddata[i] = Compensate( dtime , ddata , ki , new_dim , index );
			}
		}

		private static ProtoWorld.Curve FindOrCreateCurve
		( ProtoWorld.Animation src
			, string target, string component, int index , string property )
		{
			foreach( ProtoWorld.Curve curve in src.Curves )
			{
				if( curve.Target.Equals( target ) && curve.Component.Equals( component ) && curve.Property.Equals( property ) )
					return curve;
			}

			ProtoWorld.Curve new_curve = new ProtoWorld.Curve
			{
				Target = target ,
				Component = component ,
				Index = index ,
				Property = property ,
				Dim = 0 ,
				KeyID = (target + property).GetHashCode() ,
			};
			src.Curves.Add( new_curve );
			return new_curve;
		}

		private static string ExtractPropertyName( string src , out string post_fix )
		{
			int len = src.Length;
			if( src[len-2] == '.' )
			{
				if( src[len-1] == 'x'
					|| src[len-1] == 'y'
					|| src[len-1] == 'z'
					|| src[len-1] == 'w' 
					|| src[len-1] == 'r' 
					|| src[len-1] == 'g' 
					|| src[len-1] == 'b' 
					|| src[len-1] == 'a')
				{
					post_fix = src[len-1].ToString();
					return src.Substring( 0 , len - 2 );
				}
			}
			post_fix = null;
			return src;
		}

		private static float Compensate
		( List<float> dtime , List<float> ddata
			, int ti , int dim , int index )
		{
			// find begining
			int begin = -1;
			for( int si = ti-1 ; si >= 0 ; --si )
			{
				if( false == float.IsNaN( ddata[si * dim + index] ) )
				{
					begin = si;
					break;
				}
			}

			// find ending
			int end = -1;
			for( int si = ti+1 ; si < dtime.Count ; ++si )
			{
				if( false == float.IsNaN( ddata[si * dim + index] ) )
				{
					end = si;
					break;
				}
			}

			// compensate
			if( -1 == begin )
				return ddata[ end * dim + index ];
			else if( -1 == end )
				return ddata[ begin * dim + index ];
			else
				return Lerp( dtime , ddata , begin , end , dtime[ti] , dim , index );
		}

		private static int MatchTime( List<float> dtime , float time )
		{
			for( int i = 0 ; i < dtime.Count ; ++i )
				if( dtime[i] >= time )
					return i;
			return dtime.Count;
		}

		private static float Sample
			( List<float> dtime , List<float> ddata
			, int ti
			, float time , int index , int dim )
		{
			if( ti >= dtime.Count )
			{
				if( 0 == dtime.Count )
				{
					// has no data
					return float.NaN;
				}
				else
				{
					// most right
					return ddata[ ddata.Count - dim + index ];
				}
			}
			else if( ti == 0 ) // most left
				return ddata[ index ];
			else // between
				return Lerp( dtime , ddata , ti-1 , ti , time , dim , index );
		}

		private static void Resize<T>(this List<T> list, int sz, T c)
		{
			int cur = list.Count;
			if(sz < cur)
				list.RemoveRange(sz, cur - sz);
			else if(sz > cur)
			{
				if(sz > list.Capacity)//this bit is purely an optimisation, to avoid multiple automatic capacity changes.
					list.Capacity = sz;
				list.AddRange(Enumerable.Repeat(c, sz - cur));
			}
		}

		private static float Lerp( List<float> dtime , List<float> ddata
			, int begin , int end , float time
			, int dim , int index )
		{
			float tbegin = dtime[begin];
			float tend = dtime[end];
			float ratio = (time - tbegin) / (tend - tbegin);

			float vbegin = ddata[ begin * dim + index ];
			float vend = ddata[ end * dim + index ];
			return vbegin + (vend - vbegin) * ratio;
		}
	}
}
