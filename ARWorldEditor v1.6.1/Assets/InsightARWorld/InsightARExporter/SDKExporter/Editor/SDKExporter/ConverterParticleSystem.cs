/* ------------------------------
 * Copyright (c) NetEase Insight
 * All rights reserved.
 * ------------------------------ */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace RenderEngine
{
	
	class CurveData
	{
		public List<float> kftime;
		public List<float> kfdata;
		public List<float> intan;
		public List<float> outan;

		public List<UnityEditor.AnimationUtility.TangentMode> leftMode;
		public List<UnityEditor.AnimationUtility.TangentMode> rightMode;

		public CurveData()
		{
			kftime = new List<float> ();
			kfdata = new List<float> ();
			intan = new List<float> ();
			outan = new List<float> ();

			leftMode = new List<UnityEditor.AnimationUtility.TangentMode> ();
			rightMode = new List<UnityEditor.AnimationUtility.TangentMode> ();
		}
	}
	class ConverterParticleSystem
	{
		public static double[] SplineInterpolation ( CurveData dcurve, float thistime)
		{
			double[] ret;

			int prepos = 0;
			int nextpos = 0;

			int index = 0;

			if (thistime >= dcurve.kftime [dcurve.kftime.Count - 1])
				index = -1;
			else 
			{
				for (int i = 0; i < dcurve.kftime.Count; ++i) 
				{
					if (dcurve.kftime [i] > thistime) 
					{
						index = i;
						break;
					}
				}
			}

			if (index == 0)
				ret = new double[]{0, 0, 0, dcurve.kfdata [0]};
			else if (index == -1)
				ret = new double[]{0, 0, 0, dcurve.kfdata [dcurve.kftime.Count - 1]};
			else 
			{
				nextpos = index;
				prepos = nextpos - 1;

				if (dcurve.rightMode [prepos] == UnityEditor.AnimationUtility.TangentMode.Constant
				    || dcurve.leftMode [nextpos] == UnityEditor.AnimationUtility.TangentMode.Constant)

					ret = new double[]{ 0, 0, 0, dcurve.kfdata [prepos] };
				
				else 
				{
					double[,] A;
					double t1 = dcurve.kftime[prepos];
					double t2 = dcurve.kftime[nextpos];
					double d1 = dcurve.kfdata[prepos];
					double d2 = dcurve.kfdata[nextpos];
					double preOT = dcurve.outan[prepos];
					double nextIT = dcurve.intan[nextpos];

					// t1 = 0.1; t2 = 0.2; d1 = 1; d2 = 1; preOT = 1; nextIT = 1;

					A = new double[4, 5]{ { Math.Pow(t1, 3),     Math.Pow(t1, 2),   t1,   1,   d1     },
						{ Math.Pow(t2, 3),     Math.Pow(t2, 2),   t2,   1,   d2     },
						{ 3 * Math.Pow(t1, 2),     2 * t1,         1,   0,   preOT  },
						{ 3 * Math.Pow(t2, 2),     2 * t2,         1,   0,   nextIT } };

					ret = GaussProgram.Gauss (4, A);
				}


			}

			return ret;

		}
		public static ProtoPs.ParticleSystemScalingMode ConvertScalingMode(UnityEngine.ParticleSystemScalingMode mode)
		{
			ProtoPs.ParticleSystemScalingMode Pmode = new ProtoPs.ParticleSystemScalingMode();
			switch(mode)
			{
				case UnityEngine.ParticleSystemScalingMode.Hierarchy:
				Pmode = ProtoPs.ParticleSystemScalingMode.Smhierarchy;
				break;
			case UnityEngine.ParticleSystemScalingMode.Local:
				Pmode = ProtoPs.ParticleSystemScalingMode.Smlocal;
				UnityEngine.Debug.LogWarning ("粒子main模块ScalingMode不支持Local!\n");
				break;
				case UnityEngine.ParticleSystemScalingMode.Shape:
				Pmode = ProtoPs.ParticleSystemScalingMode.Smshape;
				UnityEngine.Debug.LogWarning ("粒子main模块ScalingMode不支持Shape!\n");
				break;
			}
			return Pmode;
		}
		public static ProtoPs.ParticleSystemCurveMode ConvertParticleSystemCurveMode( UnityEngine.ParticleSystemCurveMode mode)
		{
			ProtoPs.ParticleSystemCurveMode protoMode = new ProtoPs.ParticleSystemCurveMode();
			switch(mode)
			{
				case UnityEngine.ParticleSystemCurveMode.Constant:
				protoMode = ProtoPs.ParticleSystemCurveMode.Cmconstant;
				break;
				case UnityEngine.ParticleSystemCurveMode.Curve:
				protoMode = ProtoPs.ParticleSystemCurveMode.Cmcurve;
				break;
				case UnityEngine.ParticleSystemCurveMode.TwoConstants:
				protoMode = ProtoPs.ParticleSystemCurveMode.CmtwoConstants;
				break;
				case UnityEngine.ParticleSystemCurveMode.TwoCurves:
				protoMode = ProtoPs.ParticleSystemCurveMode.CmtwoCurves;
				break;
			}
			return protoMode;
		}

		public static ProtoPs.ParticleSystemGradientMode ConvertGradientMode(UnityEngine.ParticleSystemGradientMode mode)
		{
			ProtoPs.ParticleSystemGradientMode protoMode = new ProtoPs.ParticleSystemGradientMode();
			switch(mode)
			{
				case UnityEngine.ParticleSystemGradientMode.Color:
				protoMode = ProtoPs.ParticleSystemGradientMode.Gmcolor;
				break;
				case UnityEngine.ParticleSystemGradientMode.Gradient:
				protoMode = ProtoPs.ParticleSystemGradientMode.Gmgradient;
				break;
				case UnityEngine.ParticleSystemGradientMode.RandomColor:
				protoMode = ProtoPs.ParticleSystemGradientMode.GmrandomColor;
				break;
				case UnityEngine.ParticleSystemGradientMode.TwoColors:
				protoMode = ProtoPs.ParticleSystemGradientMode.GmtwoColors;
				break;
				case UnityEngine.ParticleSystemGradientMode.TwoGradients:
				protoMode = ProtoPs.ParticleSystemGradientMode.GmtwoGradients;
				break;
			}
			return protoMode;
		}
		public static ProtoPs.ParticleSystemSimulationSpace ConvertSimulationSpace(UnityEngine.ParticleSystemSimulationSpace space)
		{
			ProtoPs.ParticleSystemSimulationSpace protoSpace = new ProtoPs.ParticleSystemSimulationSpace();
			switch(space)
			{
				case UnityEngine.ParticleSystemSimulationSpace.Custom:
				protoSpace = ProtoPs.ParticleSystemSimulationSpace.SsCustom;
				break;
				case UnityEngine.ParticleSystemSimulationSpace.Local:
				protoSpace = ProtoPs.ParticleSystemSimulationSpace.SsLocal;
				break;
				case UnityEngine.ParticleSystemSimulationSpace.World:
				protoSpace = ProtoPs.ParticleSystemSimulationSpace.SsWorld;
				break;
			}
			return protoSpace;
		}

#if !UNITY_5_6
		public static ProtoPs.ParticleSystemEmitterVelocityMode ConvertVelocityMode(UnityEngine.ParticleSystemEmitterVelocityMode mode)
		{
			ProtoPs.ParticleSystemEmitterVelocityMode pMode = new ProtoPs.ParticleSystemEmitterVelocityMode();
			switch(mode)
			{
				case UnityEngine.ParticleSystemEmitterVelocityMode.Rigidbody:
				pMode = ProtoPs.ParticleSystemEmitterVelocityMode.Evmrigidbody;
				break;
				case UnityEngine.ParticleSystemEmitterVelocityMode.Transform:
				pMode = ProtoPs.ParticleSystemEmitterVelocityMode.Evmtransform;
				break;
			}
			return pMode;
		}
#endif

		public static ProtoPs.GradientMode ConvertGradientMode(UnityEngine.GradientMode m)
		{
			ProtoPs.GradientMode pm = new ProtoPs.GradientMode();
			switch(m)
			{
				case UnityEngine.GradientMode.Blend:
				pm = ProtoPs.GradientMode.Gmblend;
				break;
				case UnityEngine.GradientMode.Fixed:
				pm = ProtoPs.GradientMode.Gmfixed;
				break;
			}
			return pm;
		}

		public static ProtoPs.GradientAlphaKey ConvertGradientAlphaKey(UnityEngine.GradientAlphaKey key)
		{
			ProtoPs.GradientAlphaKey k = new ProtoPs.GradientAlphaKey();
			k.Alpha = key.alpha;
			k.Time = key.time;
			return k;
		}
		public static ProtoPs.GradientColorKey ConvertGradientColorKey(UnityEngine.GradientColorKey key)
		{
			ProtoPs.GradientColorKey k = new ProtoPs.GradientColorKey();
			k.Color = UtilityConverter.ConvertColor(key.color);
			k.Time = key.time;
			return k;
		}

		public static ProtoPs.Gradient ConvertGradient(UnityEngine.Gradient g)
		{			
			ProtoPs.Gradient p = new ProtoPs.Gradient();
			if(g == null)
				return p;

			p.Mode = ConvertGradientMode(g.mode);
			UnityEngine.GradientAlphaKey[] alphaKeys = g.alphaKeys;
			for(int i=0; i< alphaKeys.Length; i++)
			{
				UnityEngine.GradientAlphaKey key = alphaKeys[i];
				p.AlphaKey.Add(ConvertGradientAlphaKey(key));
			}
			UnityEngine.GradientColorKey[] colorKeys = g.colorKeys;
			for(int i=0; i< colorKeys.Length; i++)
			{				
				p.ColorKeys.Add(ConvertGradientColorKey(colorKeys[i]));
			}	
			return p;
		}		

		public static ProtoPs.Keyframe ConvertKeyframe(UnityEngine.Keyframe f)
		{
			ProtoPs.Keyframe pf = new ProtoPs.Keyframe();
			pf.Time = f.time;
			pf.Value = f.value;
			pf.InTangent = f.inTangent;
			pf.OutTangent = f.outTangent;
			//pf.mode = f.tangentMode;
			return pf;
		}

		public static ProtoPs.WrapMode ConvertWrapMode(UnityEngine.WrapMode mode)
		{
			ProtoPs.WrapMode pm = new ProtoPs.WrapMode();
			switch(mode)
			{
				case UnityEngine.WrapMode.Default:
				pm = ProtoPs.WrapMode.Wmdefault;
				break;
				case UnityEngine.WrapMode.Once:
				//case UnityEngine.WrapMode.Clamp:				
				pm = ProtoPs.WrapMode.Wmonce;
				break;
				case UnityEngine.WrapMode.Loop:
				pm = ProtoPs.WrapMode.Wmloop;
				break;
				case UnityEngine.WrapMode.PingPong:
				pm = ProtoPs.WrapMode.WmpingPong;
				break;
				case UnityEngine.WrapMode.ClampForever:
				pm = ProtoPs.WrapMode.WmclampForever;
				break;			
			}
			return pm;
		}

		public static ProtoPs.AnimationCurve ConvertAnimationCurve(UnityEngine.AnimationCurve c)
		{
			ProtoPs.AnimationCurve pc = new ProtoPs.AnimationCurve();
			if(c == null)
				return pc;
			if(c.keys == null)
				return pc;

			
			for(int i=0; i< c.keys.Length; i++)
				pc.Keys.Add(ConvertKeyframe(c.keys[i]));

			pc.Length = c.length;
			pc.PostWrapMode = ConvertWrapMode(c.postWrapMode);
			pc.PreWrapMode = ConvertWrapMode(c.preWrapMode);
			return pc;
		}
		public static void ConvertCurve(UnityEngine.AnimationCurve curve, ref CurveData dcurve, float curve_start)
		{
			if(curve == null)
				return;
			if(curve.keys == null)
				return;
			for( int fi = 0 ; fi < curve.keys.Length ; ++fi )
			{
				UnityEngine.Keyframe kf = curve.keys[fi];

				if (dcurve.kftime.Count > 0 && ( kf.time + curve_start) <= dcurve.kftime [dcurve.kftime.Count - 1]) 
				{
					int keypos;

					if (hasKey (dcurve, kf.time + curve_start, out keypos))
						continue;
					else 
					{
						dcurve.kftime.Insert (keypos, kf.time + curve_start);
						dcurve.kfdata.Insert (keypos, kf.value);
						dcurve.intan.Insert (keypos, kf.inTangent);
						dcurve.outan.Insert (keypos, kf.outTangent);

						dcurve.leftMode.Insert (keypos, UnityEditor.AnimationUtility.GetKeyLeftTangentMode(curve, fi));
						dcurve.rightMode.Insert (keypos, UnityEditor.AnimationUtility.GetKeyRightTangentMode(curve, fi));
					}
				} 
				else 
				{
					dcurve.kftime.Add (kf.time + curve_start);
					dcurve.kfdata.Add (kf.value);
					dcurve.intan.Add (kf.inTangent);
					dcurve.outan.Add (kf.outTangent);

					dcurve.leftMode.Add (UnityEditor.AnimationUtility.GetKeyLeftTangentMode(curve, fi));
					dcurve.rightMode.Add (UnityEditor.AnimationUtility.GetKeyRightTangentMode(curve, fi));
				}
					
			}
				
		}

		public static bool hasKey(CurveData dcurve, float keytime, out int keypos)
		{
			keypos = 0;

			for (int i = 0; i < dcurve.kftime.Count; ++i) 
			{
				keypos = i;

				if (System.Math.Abs(dcurve.kftime [i] - keytime) < 0.00001f) 
					return true;

				if (dcurve.kftime [i] > keytime)
					return false;				
			}

			return false;
		}

		public static ProtoPs.CurveData ConvertCurveData( List<CurveData> dcurves )
		{
			ProtoPs.CurveData dest = new ProtoPs.CurveData();		

			bool hasdata;

			float thistime = 0.0f, pretime = 0.0f, dis = -1.0f;

			while (true) 
			{
				dest.Times.Add (thistime);

				for (int i = 0; i < dcurves.Count; ++i) 
				{
					double[] data = SplineInterpolation (dcurves [i], thistime);

					dest.Datas.Add ( (float) data[0] );
					dest.Datas.Add ( (float) data[1] );
					dest.Datas.Add ( (float) data[2] );
					dest.Datas.Add ( (float) data[3] );
				}


				hasdata = false;

				// compute next thistime
				dis = float.MaxValue;
				pretime = thistime; // pretime is the previous thistime
				for (int i = 0; i < dcurves.Count; ++i) 
				{					
					int index = 0;
					if (pretime < dcurves [i].kftime [dcurves [i].kftime.Count - 1]) 
					{
						hasdata = true;

						for (int j = 0; j < dcurves[i].kftime.Count; ++j) 
						{
							if (dcurves[i].kftime [j] > pretime) 
							{								
								index = j;
								break;
							}
						}

						if (dis > dcurves [i].kftime [index] - pretime)
						{
							dis = dcurves [i].kftime [index] - pretime;
							thistime = dcurves [i].kftime [index];
						}
					}
				}


				if (!hasdata)
					break;
			}				

			return dest;
		}

		public static ProtoPs.CurveData ConvertCurveData( CurveData dcurve )
		{
			ProtoPs.CurveData dest = new ProtoPs.CurveData();		

			bool hasdata;

			float thistime = 0.0f, pretime = 0.0f, dis = -1.0f;

			while (true) 
			{
				dest.Times.Add (thistime);

				//for (int i = 0; i < dcurves.Count; ++i) 
				{
					double[] data = SplineInterpolation (dcurve, thistime);

					dest.Datas.Add ( (float) data[0] );
					dest.Datas.Add ( (float) data[1] );
					dest.Datas.Add ( (float) data[2] );
					dest.Datas.Add ( (float) data[3] );
				}


				hasdata = false;

				// compute next thistime
				dis = float.MaxValue;
				pretime = thistime; // pretime is the previous thistime
				//for (int i = 0; i < dcurves.Count; ++i) 
				{					
					int index = 0;
					if (pretime < dcurve.kftime[dcurve.kftime.Count - 1]) 
					{
						hasdata = true;

						for (int j = 0; j < dcurve.kftime.Count; ++j) 
						{
							if (dcurve.kftime [j] > pretime) 
							{								
								index = j;
								break;
							}
						}

						if (dis > dcurve.kftime [index] - pretime)
						{
							dis = dcurve.kftime [index] - pretime;
							thistime = dcurve.kftime [index];
						}
					}
				}


				if (!hasdata)
					break;
			}				

			return dest;
		}

		public static ProtoPs.MinMaxCurve ConvertMinMaxCurve( UnityEngine.ParticleSystem.MinMaxCurve curve)
		{
			ProtoPs.MinMaxCurve protoCurve = new ProtoPs.MinMaxCurve();	

			protoCurve.Constant = curve.constant;
			protoCurve.ConstantMax = curve.constantMax;
			protoCurve.ConstantMin = curve.constantMin;			
			protoCurve.CurveMultiplier = curve.curveMultiplier;
			protoCurve.Mode = ConvertParticleSystemCurveMode(curve.mode);	
			//protoCurve.CurveMax = ConvertAnimationCurve(curve.curveMax);
			//protoCurve.CurveMin = ConvertAnimationCurve(curve.curveMin);
			//protoCurve.Curve = ConvertAnimationCurve(curve.curve);	

			if( curve.curve != null && curve.curve.keys != null)
			{
				CurveData data = new CurveData();
				ConvertCurve(curve.curve, ref data, 0.0f);
				protoCurve.Curve = ConvertCurveData(data);
			}
				
			if(curve.curveMin != null && curve.curveMin.keys != null)
			{
				CurveData dataMin = new CurveData();
				ConvertCurve(curve.curveMin, ref dataMin, 0.0f);
				protoCurve.CurveMin = ConvertCurveData(dataMin);
			}
				
			if(curve.curveMax != null && curve.curveMax.keys != null)
			{
				CurveData dataMax = new CurveData();
				ConvertCurve(curve.curveMax, ref dataMax, 0.0f);	
				protoCurve.CurveMax =  ConvertCurveData(dataMax);
			}	
			
			return protoCurve;
		}
		public static ProtoPs.MinMaxGradient ConvertMinMaxGradient(UnityEngine.ParticleSystem.MinMaxGradient gradient)
		{
			ProtoPs.MinMaxGradient protoG = new ProtoPs.MinMaxGradient();			

			protoG.Mode = ConvertGradientMode( gradient.mode);
			protoG.GradientMax = ConvertGradient(gradient.gradientMax);
			protoG.GradientMin = ConvertGradient(gradient.gradientMin);
			protoG.ColorMax = UtilityConverter.ConvertColor(gradient.colorMax);
			protoG.ColorMin = UtilityConverter.ConvertColor(gradient.colorMin);
			protoG.Color = UtilityConverter.ConvertColor(gradient.color);
			protoG.Gradient = ConvertGradient(gradient.gradient);
			return protoG;
		}
		public static ProtoPs.MainModule ConvertMain( ExporterConfig config , UnityEngine.ParticleSystem.MainModule main )
		{
			ProtoPs.MainModule protoMain = new ProtoPs.MainModule();
			protoMain.Loop = main.loop;
			protoMain.Duration = main.duration;
			
			protoMain.StartRotationX = ConvertMinMaxCurve(main.startRotationX);
			protoMain.StartRotationXMultiplier = main.startRotationXMultiplier;
			protoMain.StartRotationY = ConvertMinMaxCurve(main.startRotationY);
			protoMain.StartRotationYMultiplier = main.startRotationYMultiplier;
			protoMain.StartRotationZ = ConvertMinMaxCurve(main.startRotationZ);
			protoMain.StartRotationZMultiplier = main.startRotationZMultiplier;

			protoMain.RandomizeRotationDirection = main.flipRotation;
			protoMain.StartRotationMultiplier = main.startRotationMultiplier;
			protoMain.StartColor = ConvertMinMaxGradient( main.startColor);
			protoMain.GravityModifierMultiplier = main.gravityModifierMultiplier;
			protoMain.SimulationSpace = ConvertSimulationSpace(main.simulationSpace);

			if(main.customSimulationSpace)
				protoMain.CustomSimulationSpace = UtilityConverter.ConvertTransform(main.customSimulationSpace);

			protoMain.SimulationSpeed = main.simulationSpeed;
			#if !UNITY_5_6
			protoMain.UseUnscaledTime = main.useUnscaledTime;
			#endif
			protoMain.ScalingMode = ConvertScalingMode(main.scalingMode);
			protoMain.PlayOnAwake = main.playOnAwake;
			protoMain.GravityModifier = ConvertMinMaxCurve(main.gravityModifier);
			protoMain.MaxParticles = main.maxParticles;
			protoMain.StartRotation = ConvertMinMaxCurve(main.startRotation);
			
			protoMain.Prewarm = main.prewarm;
			if (main.prewarm)
				UnityEngine.Debug.LogWarning ("粒子Main不支持prewarm！");
			protoMain.StartDelay = ConvertMinMaxCurve(main.startDelay);
			protoMain.StartDelayMultiplier = main.startDelayMultiplier;
			protoMain.StartLifetime = ConvertMinMaxCurve(main.startLifetime);
			protoMain.StartLifetimeMultiplier = main.startLifetimeMultiplier;
			protoMain.StartSpeed = ConvertMinMaxCurve(main.startSpeed);
			protoMain.StartRotation3D = main.startRotation3D;
			protoMain.StartSpeedMultiplier = main.startSpeedMultiplier;
			protoMain.StartSize = ConvertMinMaxCurve(main.startSize);
			protoMain.StartSizeMultiplier = main.startSizeMultiplier;
			protoMain.StartSizeX = ConvertMinMaxCurve(main.startSizeX);
			protoMain.StartSizeXMultiplier = main.startSizeXMultiplier;
			protoMain.StartSizeY = ConvertMinMaxCurve(main.startSizeY);
			protoMain.StartSizeYMultiplier = main.startSizeYMultiplier;
			protoMain.StartSizeZ = ConvertMinMaxCurve(main.startSizeZ);
			protoMain.StartSizeZMultiplier = main.startSizeZMultiplier;
			protoMain.StartSize3D = main.startSize3D;
			#if !UNITY_5_6
			protoMain.EmitterVelocityMode = ConvertVelocityMode(main.emitterVelocityMode);
			#endif
			return protoMain;
		}

		public static ProtoPs.Burst ConvertBurst(UnityEngine.ParticleSystem.Burst burst)
		{
			ProtoPs.Burst pb = new ProtoPs.Burst();
			pb.Time = burst.time;
			pb.MinCount = burst.minCount;
			pb.MaxCount = burst.maxCount;
			#if UNITY_2017_2_OR_NEWER
			pb.Count = ConvertMinMaxCurve(burst.count); // Unity没有暴露burst.count对应的countMultiplier
			if(burst.count.mode == UnityEngine.ParticleSystemCurveMode.Constant || burst.count.mode == UnityEngine.ParticleSystemCurveMode.TwoConstants)
				pb.CountMultiplier = burst.count.constant;
			else
				pb.CountMultiplier = burst.count.curveMultiplier;
			#endif
			pb.CycleCount = burst.cycleCount;
			pb.RepeatInterval = burst.repeatInterval;
			return pb;
		}
		public static ProtoPs.EmissionModule ConvertEmission(UnityEngine.ParticleSystem.EmissionModule emission)
		{
			ProtoPs.EmissionModule protoEmission = new ProtoPs.EmissionModule();
			//if(emission.enabled)
			{
				protoEmission.Enabled = emission.enabled;
				protoEmission.BurstCount = emission.burstCount;
				UnityEngine.ParticleSystem.Burst[] bursts = new UnityEngine.ParticleSystem.Burst[emission.burstCount]; 
				emission.GetBursts(bursts);
				
				for(int i=0; i< emission.burstCount; i++)
				{
					UnityEngine.ParticleSystem.Burst burst = bursts[i];	
					protoEmission.Bursts.Add(ConvertBurst(burst));
				}
				UnityEngine.Debug.Log ("粒子Emission模块不支持RateOverDistance!\n");
				protoEmission.RateOverDistance = ConvertMinMaxCurve(emission.rateOverDistance);
				protoEmission.RateOverDistanceMultiplier = emission.rateOverDistanceMultiplier;
				protoEmission.RateOverTime = ConvertMinMaxCurve(emission.rateOverTime);
				protoEmission.RateOverTimeMultiplier = emission.rateOverTimeMultiplier;				
			}
			
			return protoEmission;
		}

		public static ProtoPs.ColorBySpeedModule ConvertColorBySpeed(UnityEngine.ParticleSystem.ColorBySpeedModule m)
		{
			ProtoPs.ColorBySpeedModule pm = new ProtoPs.ColorBySpeedModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Color = ConvertMinMaxGradient(m.color);
				pm.Range = UtilityConverter.ConvertVector2(m.range);
			}	
			return pm;
		}

		#if !UNITY_5_6
		//todo: UVChannelFlags 
		public static ProtoPs.ParticleSystemAnimationMode ConvertAnimationMode(UnityEngine.ParticleSystemAnimationMode mode)
		{
			ProtoPs.ParticleSystemAnimationMode pm = new ProtoPs.ParticleSystemAnimationMode();
			switch(mode)
			{
				case UnityEngine.ParticleSystemAnimationMode.Grid:
				pm = ProtoPs.ParticleSystemAnimationMode.Amgrid;
				break;
				case UnityEngine.ParticleSystemAnimationMode.Sprites:
				pm = ProtoPs.ParticleSystemAnimationMode.Amsprites;
				break;
			}
			return pm;
		}
		#endif
		public static ProtoPs.ParticleSystemAnimationType ConvertAnimationType(UnityEngine.ParticleSystemAnimationType type)
		{
			ProtoPs.ParticleSystemAnimationType pt = new ProtoPs.ParticleSystemAnimationType();
			switch(type)
			{
				case UnityEngine.ParticleSystemAnimationType.SingleRow:
				pt = ProtoPs.ParticleSystemAnimationType.AtsingleRow;
				break;
				case UnityEngine.ParticleSystemAnimationType.WholeSheet:
				pt = ProtoPs.ParticleSystemAnimationType.AtwholeSheet;
				break;
			}
			return pt;
		}
		public static ProtoPs.TextureSheetAnimationModule ConvertTextureSheetAnimation(UnityEngine.ParticleSystem.TextureSheetAnimationModule m)
		{
			ProtoPs.TextureSheetAnimationModule pm = new ProtoPs.TextureSheetAnimationModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.UseRandomRow = m.useRandomRow;
				pm.FlipU = m.flipU;
				//pm.UvChannelMask = 
				pm.RowIndex = m.rowIndex;
				pm.CycleCount = m.cycleCount;
				pm.StartFrameMultiplier = m.startFrameMultiplier;
				pm.StartFrame = ConvertMinMaxCurve(m.startFrame);
				pm.FrameOverTimeMultiplier = m.frameOverTimeMultiplier;
				pm.FrameOverTime = ConvertMinMaxCurve(m.frameOverTime);
				#if !UNITY_5_6
				pm.SpriteCount = m.spriteCount;	
				#endif
				pm.Animation = ConvertAnimationType(m.animation);
				#if !UNITY_5_6
				pm.Mode = ConvertAnimationMode(m.mode);
				#endif
				pm.NumTilesX = m.numTilesX;
				pm.NumTilesY = m.numTilesY;
				pm.FlipV = m.flipV;
			}
			return pm;
		}

		public static ProtoPs.ParticleSystemSubEmitterType ConvertSubEmitterType(UnityEngine.ParticleSystemSubEmitterType type)
		{
			ProtoPs.ParticleSystemSubEmitterType pt = new ProtoPs.ParticleSystemSubEmitterType();
			switch(type)
			{
				case UnityEngine.ParticleSystemSubEmitterType.Birth:
				pt = ProtoPs.ParticleSystemSubEmitterType.Setbirth;
				break;
				case UnityEngine.ParticleSystemSubEmitterType.Collision:
				pt = ProtoPs.ParticleSystemSubEmitterType.Setcollision;
				break;
				case UnityEngine.ParticleSystemSubEmitterType.Death:
				pt = ProtoPs.ParticleSystemSubEmitterType.Setdeath;
				break;				
			}
			return pt;
		}
		public static ProtoPs.ParticleSystemSubEmitterProperties ConvertSubEmitterProperty(UnityEngine.ParticleSystemSubEmitterProperties type)
		{
			ProtoPs.ParticleSystemSubEmitterProperties pt = new ProtoPs.ParticleSystemSubEmitterProperties();
			switch(type)
			{
				case UnityEngine.ParticleSystemSubEmitterProperties.InheritColor:
				pt = ProtoPs.ParticleSystemSubEmitterProperties.SepinheritColor;
				break;
				case UnityEngine.ParticleSystemSubEmitterProperties.InheritEverything:
				pt = ProtoPs.ParticleSystemSubEmitterProperties.SepinheritEverything;
				break;
				case UnityEngine.ParticleSystemSubEmitterProperties.InheritNothing:
				pt = ProtoPs.ParticleSystemSubEmitterProperties.SepinheritNothing;
				break;
				case UnityEngine.ParticleSystemSubEmitterProperties.InheritRotation:
				pt = ProtoPs.ParticleSystemSubEmitterProperties.SepinheritRotation;
				break;
				case UnityEngine.ParticleSystemSubEmitterProperties.InheritSize:
				pt = ProtoPs.ParticleSystemSubEmitterProperties.SepinheritSize;
				break;						
			}
			return pt;
		}
		

		public static ProtoPs.ExternalForcesModule ConvertExternalForces(UnityEngine.ParticleSystem.ExternalForcesModule m)
		{
			ProtoPs.ExternalForcesModule pm = new ProtoPs.ExternalForcesModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Multiplier = m.multiplier;
			}
			return pm;
		}

		public static ProtoPs.RotationBySpeedModule ConvertRotationBySpeed(UnityEngine.ParticleSystem.RotationBySpeedModule m)
		{
			ProtoPs.RotationBySpeedModule pm = new ProtoPs.RotationBySpeedModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.X =  ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.SeparateAxes = m.separateAxes;
				pm.Range = UtilityConverter.ConvertVector2(m.range);
			}
			return pm;
		}

		public static ProtoPs.RotationOverLifetimeModule ConvertRotationOverLifetime(UnityEngine.ParticleSystem.RotationOverLifetimeModule m)
		{
			ProtoPs.RotationOverLifetimeModule pm = new ProtoPs.RotationOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.X = ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.SeparateAxes = m.separateAxes;
			}
			return pm;
		}

		public static ProtoPs.SizeBySpeedModule ConvertSizeBySpeed(UnityEngine.ParticleSystem.SizeBySpeedModule m)
		{
			ProtoPs.SizeBySpeedModule pm = new ProtoPs.SizeBySpeedModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Size = ConvertMinMaxCurve(m.size);
				pm.SizeMultiplier = m.sizeMultiplier;
				pm.X = ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.SeparateAxes = m.separateAxes;
				pm.Range = UtilityConverter.ConvertVector2(m.range);
			}
			return pm;
		}

		public static ProtoPs.SizeOverLifetimeModule ConvertSizeOverLifetime(UnityEngine.ParticleSystem.SizeOverLifetimeModule m)
		{
			ProtoPs.SizeOverLifetimeModule pm = new ProtoPs.SizeOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Size = ConvertMinMaxCurve(m.size);
				pm.SizeMultiplier = m.sizeMultiplier;
				pm.X = ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.SeparateAxes = m.separateAxes;
			}
			return pm;
		}

		public static ProtoPs.ColorOverLifetimeModule ConvertSizeOverLifetime(UnityEngine.ParticleSystem.ColorOverLifetimeModule m)
		{
			ProtoPs.ColorOverLifetimeModule pm = new ProtoPs.ColorOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Color = ConvertMinMaxGradient(m.color);
			}
			return pm;
		}

		public static ProtoPs.ForceOverLifetimeModule ConvertForceOverLifetime( UnityEngine.ParticleSystem.ForceOverLifetimeModule m)
		{
			ProtoPs.ForceOverLifetimeModule pm = new ProtoPs.ForceOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;				
				pm.X = ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.Space = ConvertSimulationSpace(m.space);
				pm.Randomized = m.randomized;
			}
			return pm;
		}

		public static ProtoPs.ParticleSystemInheritVelocityMode ConvertInheritVelMode(UnityEngine.ParticleSystemInheritVelocityMode m)
		{
			ProtoPs.ParticleSystemInheritVelocityMode pm = new ProtoPs.ParticleSystemInheritVelocityMode ();
			switch (m) 
			{
			case UnityEngine.ParticleSystemInheritVelocityMode.Initial:
				pm = ProtoPs.ParticleSystemInheritVelocityMode.IvmInit;
				break;
			case UnityEngine.ParticleSystemInheritVelocityMode.Current:
				pm = ProtoPs.ParticleSystemInheritVelocityMode.IvmCurrent;
				break;
			}
			return pm;
		}

		public static ProtoPs.InheritVelocityModule ConvertInheritVelocity( UnityEngine.ParticleSystem.InheritVelocityModule m)
		{
			ProtoPs.InheritVelocityModule pm = new ProtoPs.InheritVelocityModule();
			//if(m.enabled)
			{
				pm.Mode = ConvertInheritVelMode( m.mode) ;
				pm.Enabled = m.enabled;				
				pm.Curve = ConvertMinMaxCurve(m.curve);
				pm.CurveMultiplier = m.curveMultiplier;
			}
			return pm;
		}
		
		public static ProtoPs.LimitVelocityOverLifetimeModule ConvertLimitVelocityOverLifetime( UnityEngine.ParticleSystem.LimitVelocityOverLifetimeModule m)
		{
			ProtoPs.LimitVelocityOverLifetimeModule pm = new ProtoPs.LimitVelocityOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;				
				pm.LimitX = ConvertMinMaxCurve(m.limitX);
				pm.LimitXMultiplier = m.limitXMultiplier;
				pm.LimitY = ConvertMinMaxCurve(m.limitY);
				pm.LimitYMultiplier = m.limitYMultiplier;
				pm.LimitZ = ConvertMinMaxCurve(m.limitZ);
				pm.LimitZMultiplier = m.limitZMultiplier;
				pm.Limit = ConvertMinMaxCurve(m.limit);
				pm.LimitMultiplier = m.limitMultiplier;
				pm.Dampen = m.dampen;
				pm.SeparateAxes = m.separateAxes;
				pm.Space = ConvertSimulationSpace(m.space);
			}
			return pm;
		}

		public static ProtoPs.ColorOverLifetimeModule ConvertColorOverLifetime(UnityEngine.ParticleSystem.ColorOverLifetimeModule m)
		{
			ProtoPs.ColorOverLifetimeModule pm = new ProtoPs.ColorOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				pm.Color = ConvertMinMaxGradient(m.color);
			}
			return pm;
		}


		public static ProtoPs.VelocityOverLifetimeModule ConvertVelocityOverLifetime( UnityEngine.ParticleSystem.VelocityOverLifetimeModule m)
		{
			ProtoPs.VelocityOverLifetimeModule pm = new ProtoPs.VelocityOverLifetimeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;				
				pm.X = ConvertMinMaxCurve(m.x);
				pm.XMultiplier = m.xMultiplier;
				pm.Y = ConvertMinMaxCurve(m.y);
				pm.YMultiplier = m.yMultiplier;
				pm.Z = ConvertMinMaxCurve(m.z);
				pm.ZMultiplier = m.zMultiplier;
				pm.Space = ConvertSimulationSpace(m.space);		

				pm.OrbitalX = ConvertMinMaxCurve(m.orbitalX);
				pm.OrbitalXMultiplier = m.orbitalXMultiplier;
				pm.OrbitalY = ConvertMinMaxCurve(m.orbitalY);
				pm.OrbitalYMultiplier = m.orbitalYMultiplier;
				pm.OrbitalZ = ConvertMinMaxCurve(m.orbitalZ);
				pm.OrbitalZMultiplier = m.orbitalZMultiplier;

				pm.OffsetX = ConvertMinMaxCurve(m.orbitalOffsetX);
				pm.OffsetXMultiplier = m.orbitalOffsetXMultiplier;
				pm.OffsetY = ConvertMinMaxCurve(m.orbitalOffsetY);
				pm.OffsetYMultiplier = m.orbitalOffsetYMultiplier;
				pm.OffsetZ = ConvertMinMaxCurve(m.orbitalOffsetZ);
				pm.OffsetZMultiplier = m.orbitalOffsetZMultiplier;

				pm.Radial = ConvertMinMaxCurve(m.radial);
				pm.RadialMultiplier = m.radialMultiplier;
				pm.Speed = ConvertMinMaxCurve(m.speedModifier);
				pm.SpeedMultiplier = m.speedModifierMultiplier;
			}
			return pm;
		}

		public static ProtoPs.ParticleSystemShapeType ConvertShapeType(UnityEngine.ParticleSystemShapeType type)
		{
			ProtoPs.ParticleSystemShapeType pt = new ProtoPs.ParticleSystemShapeType();
			switch(type)
			{
				case UnityEngine.ParticleSystemShapeType.Box:
				pt = ProtoPs.ParticleSystemShapeType.Stbox;
				break;
				case UnityEngine.ParticleSystemShapeType.BoxEdge:
				pt = ProtoPs.ParticleSystemShapeType.StboxEdge;
				break;
				case UnityEngine.ParticleSystemShapeType.BoxShell:
				pt = ProtoPs.ParticleSystemShapeType.StboxShell;
				UnityEngine.Debug.LogWarning ("粒子Shape模块不支持BoxShell!\n");
				break;
				case UnityEngine.ParticleSystemShapeType.Circle:
				pt = ProtoPs.ParticleSystemShapeType.Stcircle;
				break;
//				case UnityEngine.ParticleSystemShapeType.CircleEdge:
//				pt = ProtoPs.ParticleSystemShapeType.StcircleEdge;
//				break;
			case UnityEngine.ParticleSystemShapeType.Cone:
				pt = ProtoPs.ParticleSystemShapeType.Stcone;
				break;
//				case UnityEngine.ParticleSystemShapeType.ConeShell:
//				pt = ProtoPs.ParticleSystemShapeType.StconeShell;
//				break;
				case UnityEngine.ParticleSystemShapeType.ConeVolume:
				pt = ProtoPs.ParticleSystemShapeType.StconeVolume;
				UnityEngine.Debug.LogWarning ("粒子Shape模块不支持ConeVolume!\n");
				break;
//				case UnityEngine.ParticleSystemShapeType.ConeVolumeShell:
//				pt = ProtoPs.ParticleSystemShapeType.StconeVolumeShell;
//				break;
#if !UNITY_5_6
				case UnityEngine.ParticleSystemShapeType.Donut:
				pt = ProtoPs.ParticleSystemShapeType.Stdonut;
				UnityEngine.Debug.LogWarning ("粒子Shape模块不支持Donut!\n");
				break;
#endif
				case UnityEngine.ParticleSystemShapeType.Hemisphere:
				pt = ProtoPs.ParticleSystemShapeType.Sthemisphere;
				break;
//				case UnityEngine.ParticleSystemShapeType.HemisphereShell:
//				pt = ProtoPs.ParticleSystemShapeType.SthemisphereShell;
//				break;
				case UnityEngine.ParticleSystemShapeType.Mesh:
				pt = ProtoPs.ParticleSystemShapeType.Stmesh;
				break;
				case UnityEngine.ParticleSystemShapeType.MeshRenderer:
				pt = ProtoPs.ParticleSystemShapeType.StmeshRenderer;
				break;
				case UnityEngine.ParticleSystemShapeType.SingleSidedEdge:
				pt = ProtoPs.ParticleSystemShapeType.StsingleSidedEdge;
				UnityEngine.Debug.LogWarning ("粒子Shape模块不支持Edge!\n");
				break;
				case UnityEngine.ParticleSystemShapeType.SkinnedMeshRenderer:
				pt = ProtoPs.ParticleSystemShapeType.StskinnedMeshRenderer;
				break;
				case UnityEngine.ParticleSystemShapeType.Sphere:
				pt = ProtoPs.ParticleSystemShapeType.Stsphere;
				break;
//				case UnityEngine.ParticleSystemShapeType.SphereShell:
//				pt = ProtoPs.ParticleSystemShapeType.StsphereShell;
//				break;		
				
				default:
				break;
			}
			return pt;
		}

		public static ProtoPs.ParticleSystemRenderMode ConvertRenderMode(UnityEngine.ParticleSystemRenderMode mode)
		{
			ProtoPs.ParticleSystemRenderMode m = new ProtoPs.ParticleSystemRenderMode();
			switch(mode)
			{
			case UnityEngine.ParticleSystemRenderMode.Billboard:
				m = ProtoPs.ParticleSystemRenderMode.Rmbillboard;
				break;			
			case UnityEngine.ParticleSystemRenderMode.HorizontalBillboard:
				m = ProtoPs.ParticleSystemRenderMode.RmhorizontalBillboard;
				break;
			case UnityEngine.ParticleSystemRenderMode.VerticalBillboard:
				m = ProtoPs.ParticleSystemRenderMode.RmverticalBillboard;
				break;
			case UnityEngine.ParticleSystemRenderMode.Stretch:
				m = ProtoPs.ParticleSystemRenderMode.Rmstretch;
				break;
			case UnityEngine.ParticleSystemRenderMode.Mesh:
				m = ProtoPs.ParticleSystemRenderMode.Rmmesh;
				break;
			case UnityEngine.ParticleSystemRenderMode.None:
				m = ProtoPs.ParticleSystemRenderMode.Rmnone;
				break;			
			default:
				break;
			}
			return m;
		}
		public static ProtoPs.ParticleSystemRenderSpace CovertRenderSpace(UnityEngine.ParticleSystemRenderSpace space)
		{
			ProtoPs.ParticleSystemRenderSpace ps = new ProtoPs.ParticleSystemRenderSpace();
			switch(space)
			{
			case UnityEngine.ParticleSystemRenderSpace.View:
				ps = ProtoPs.ParticleSystemRenderSpace.Rsview;
				break;			
			case UnityEngine.ParticleSystemRenderSpace.Facing:
				ps = ProtoPs.ParticleSystemRenderSpace.Rsfacing;
				break;
			case UnityEngine.ParticleSystemRenderSpace.Local:
				ps = ProtoPs.ParticleSystemRenderSpace.Rslocal;
				break;
#if !UNITY_5_6
			case UnityEngine.ParticleSystemRenderSpace.Velocity:
				ps = ProtoPs.ParticleSystemRenderSpace.Rsvelocity;
				break;
#endif
			case UnityEngine.ParticleSystemRenderSpace.World:
				ps = ProtoPs.ParticleSystemRenderSpace.Rsworld;
				break;			
			default:
				break;
			}
			return ps;
		}

		public static ProtoPs.ParticleSystemRenderer ConvertRenderer(UnityEngine.ParticleSystemRenderer render, ExporterConfig config)
		{
			ProtoPs.ParticleSystemRenderer pRender = new ProtoPs.ParticleSystemRenderer();
			//pRender.Material = render.sharedMaterial.name;
			pRender.Enabled = render.enabled; 
			//pRender.TrailMaterial = render.trailMaterial;
			pRender.MeshCount = render.meshCount;	
			UnityEngine.Mesh[] meshes = new UnityEngine.Mesh[pRender.MeshCount];
			render.GetMeshes (meshes);
			for (int i = 0; i < pRender.MeshCount; i++)
			{
				string name = ExportMesh (meshes[i], config);
				pRender.MeshNames.Add (name);
			}
//			if(render.mesh != null)
//				pRender.MeshName =  ExportMesh(render.mesh, config);
			pRender.MaxParticleSize = render.maxParticleSize;
			pRender.MinParticleSize = render.minParticleSize;
			pRender.SortingFudge = render.sortingFudge;
			//pRender.SortMode = render.sortMode;
			pRender.Pivot = UtilityConverter.ConvertVector3(render.pivot);
			pRender.NormalDirection = render.normalDirection;
			pRender.CameraVelocityScale = render.cameraVelocityScale;
			pRender.VelocityScale = render.velocityScale;
			pRender.LengthScale = render.lengthScale;
			pRender.RenderMode = ConvertRenderMode(render.renderMode);
			pRender.Alignment = CovertRenderSpace (render.alignment);

			return pRender;
		}

		public static ProtoPs.ParticleSystemMeshShapeType ConvertMeshShapeType(UnityEngine.ParticleSystemMeshShapeType type)
		{
			ProtoPs.ParticleSystemMeshShapeType pst = new ProtoPs.ParticleSystemMeshShapeType ();
			switch(type)
			{
				case UnityEngine.ParticleSystemMeshShapeType.Vertex:
					pst = ProtoPs.ParticleSystemMeshShapeType.Stvertex;
				break;
				case UnityEngine.ParticleSystemMeshShapeType.Edge:
					pst = ProtoPs.ParticleSystemMeshShapeType.Stedge;
				break;
				case UnityEngine.ParticleSystemMeshShapeType.Triangle:
					pst = ProtoPs.ParticleSystemMeshShapeType.Sttriangle;
				break;
			}
			return pst;
		}
		//todo: lq

		public static string ExportMeshRenderer(UnityEngine.MeshRenderer meshRender, ExporterConfig config)
		{
			// export its mesh
			UnityEngine.MeshFilter meshfilter = meshRender.gameObject.GetComponent<UnityEngine.MeshFilter>();
			UnityEngine.Mesh mesh = null;
			string export_mesh_file = "";
			if( null != meshfilter )
			{
				mesh = meshfilter.sharedMesh;
				export_mesh_file = ExporterResource.ExportModel( config , mesh );
			}
			return export_mesh_file;
		}

		public static string ExportMesh(UnityEngine.Mesh mesh, ExporterConfig config)
		{
			// export its mesh		
			string export_mesh_file = ExporterResource.ExportModel( config , mesh );
			return export_mesh_file;
		}
			
		public static ProtoPs.ShapeModule ConvertShape(UnityEngine.ParticleSystem.ShapeModule m, ExporterConfig config)
		{
			ProtoPs.ShapeModule pm = new ProtoPs.ShapeModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				//pm.SkinnedMeshRenderer 
				pm.UseMeshMaterialIndex = m.useMeshMaterialIndex;
				pm.MeshMaterialIndex = m.meshMaterialIndex;
				pm.UseMeshColors = m.useMeshColors;
				pm.NormalOffset = m.normalOffset;
				//pm.MeshRenderer

				if (m.meshRenderer != null) 
				{
					pm.MeshRendererName = m.meshRenderer.gameObject.name;
					pm.MeshName = ExportMeshRenderer (m.meshRenderer, config);
				}
				if (m.mesh != null)
					pm.MeshName = ExportMesh (m.mesh, config);
				
				pm.Arc = m.arc;
				pm.ArcSpread = m.arcSpread;
				pm.ArcSpeed = ConvertMinMaxCurve(m.arcSpeed);
				pm.ArcSpeedMultiplier = m.arcSpeedMultiplier;
#if !UNITY_5_6
				pm.DonutRadius = m.donutRadius;
				pm.Position = UtilityConverter.ConvertVector3(m.position);
				//pm.Rotation = UtilityConverter.ConvertVector3(m.rotation);
				UnityEngine.Quaternion q = UnityEngine.Quaternion.Euler (m.rotation);
				pm.Rotation = UtilityConverter.ConvertVector4 (q);
				pm.Scale = UtilityConverter.ConvertVector3(m.scale);
				//pm.arcMode
				pm.BoxThickness = UtilityConverter.ConvertVector3(m.boxThickness);
#endif
				//pm.shapeType;
				pm.ShapeType = ConvertShapeType(m.shapeType);
				pm.RandomDirectionAmount = m.randomDirectionAmount;
				pm.SphericalDirectionAmount = m.sphericalDirectionAmount;
#if !UNITY_5_6
				pm.RandomPositionAmount = m.randomPositionAmount;
#endif
				pm.AlignToDirection = m.alignToDirection;
				pm.Radius = m.radius;
				pm.MeshShapeType = ConvertMeshShapeType(m.meshShapeType);
				//pm.RadiusMode;
				pm.RadiusSpeed = ConvertMinMaxCurve(m.radiusSpeed);
				pm.RadiusSpeedMultiplier = m.radiusSpeedMultiplier;
#if !UNITY_5_6
				pm.RadiusThickness = m.radiusThickness;
#endif
				pm.Angle = m.angle;
				pm.Length = m.length;
				pm.RadiusSpread = m.radiusSpread;
			}
			return pm;
		}

		public static ProtoPs.ParticleSystemTrailTextureMode ConvertTrailTextureMode(UnityEngine.ParticleSystemTrailTextureMode mode)
		{
			ProtoPs.ParticleSystemTrailTextureMode pm = new ProtoPs.ParticleSystemTrailTextureMode ();
			switch (mode) {
			case UnityEngine.ParticleSystemTrailTextureMode.Stretch:
				pm = ProtoPs.ParticleSystemTrailTextureMode.Ttmstretch;
				break;
			case UnityEngine.ParticleSystemTrailTextureMode.Tile:
				pm = ProtoPs.ParticleSystemTrailTextureMode.Ttmtile;
				break;
			case UnityEngine.ParticleSystemTrailTextureMode.DistributePerSegment:
				pm = ProtoPs.ParticleSystemTrailTextureMode.TtmdistributePerSegment;
				break;
			case UnityEngine.ParticleSystemTrailTextureMode.RepeatPerSegment:
				pm = ProtoPs.ParticleSystemTrailTextureMode.TtmrepeatPerSegment;
				break;
			}
			return pm;
		}

		public static ProtoPs.TrailModule ConvertTrails(UnityEngine.ParticleSystem.TrailModule t)
		{
			ProtoPs.TrailModule pt = new ProtoPs.TrailModule ();
//			if (pt.Enabled) 
			{
				pt.Enabled = t.enabled;
				pt.ColorOverLifetime=ConvertMinMaxGradient(t.colorOverLifetime);
				pt.ColorOverTrail = ConvertMinMaxGradient( t.colorOverTrail);
				pt.DieWithParticles = t.dieWithParticles;
#if !UNITY_5_6
				pt.GenerateLightingData = t.generateLightingData;
#endif
				pt.InheritParticleColor = t.inheritParticleColor;
				pt.Lifetime = ConvertMinMaxCurve(t.lifetime);
				pt.LifetimeMultiplier = t.lifetimeMultiplier;
				pt.MinVertexDistance = t.minVertexDistance;
				pt.Ratio = t.ratio;
				pt.SizeAffectsLifetime = t.sizeAffectsLifetime;
				pt.SizeAffectsWidth = t.sizeAffectsWidth;
				pt.TextureMode = ConvertTrailTextureMode(t.textureMode);
				pt.WidthOverTrail = ConvertMinMaxCurve(t.widthOverTrail);
				pt.WidthOverTrailMultiplier = t.widthOverTrailMultiplier;
				pt.WorldSpace = t.worldSpace;
			}
			return pt;
		}

		public static ProtoPs.ParticleSystemOverlapAction ConvertOverlapAction(UnityEngine.ParticleSystemOverlapAction act)
		{
			ProtoPs.ParticleSystemOverlapAction pact = new ProtoPs.ParticleSystemOverlapAction ();
			switch(act)
			{
			case UnityEngine.ParticleSystemOverlapAction.Callback:
				pact = ProtoPs.ParticleSystemOverlapAction.OverlapActionCallback;
				break;
			case UnityEngine.ParticleSystemOverlapAction.Ignore:
				pact = ProtoPs.ParticleSystemOverlapAction.OverlapActionIgnore;
				break;
			case UnityEngine.ParticleSystemOverlapAction.Kill:
				pact = ProtoPs.ParticleSystemOverlapAction.OverlapActionKill;
				break;
			}
			return pact;
		}
		public static ProtoPs.TriggerModule ConvertTriggers(ExporterConfig config, UnityEngine.ParticleSystem ps)
		{
			UnityEngine.ParticleSystem.TriggerModule t = ps.trigger;

			ProtoPs.TriggerModule pt = new ProtoPs.TriggerModule ();
			//			if (pt.Enabled) 
			{
				pt.Enabled = t.enabled;
				pt.RadiusScale = t.radiusScale;
				pt.MaxColliderCount = t.maxColliderCount;
				pt.Inside = ConvertOverlapAction (t.inside);
				pt.Outside = ConvertOverlapAction (t.outside);
				pt.Enter = ConvertOverlapAction (t.enter);
				pt.Exit = ConvertOverlapAction (t.exit);
				for(int i=0; i< t.maxColliderCount; i++)
				{
					var collider = t.GetCollider (i);
					if (collider != null) 
					{
						//string triggerName = ExporterUtility.FindRelativePath (collider.transform, ps.transform);
						string triggerName = collider.transform.name;
						pt.Colliders.Add( triggerName );
					}
				}
			}
			return pt;
		}
		public static ProtoPs.LayerMask ConvertLayerMask(UnityEngine.LayerMask mask)
		{
			ProtoPs.LayerMask ret = new ProtoPs.LayerMask ();
			ret.Value = mask.value;
			return ret;
		}
		public static ProtoPs.ParticleSystemCollisionQuality ConvertCollisionQuality(UnityEngine.ParticleSystemCollisionQuality q)
		{
			ProtoPs.ParticleSystemCollisionQuality ret = new ProtoPs.ParticleSystemCollisionQuality ();
			switch (q) {
			case UnityEngine.ParticleSystemCollisionQuality.High:
				ret = ProtoPs.ParticleSystemCollisionQuality.CqHight;
				break;
			case UnityEngine.ParticleSystemCollisionQuality.Low:
				ret = ProtoPs.ParticleSystemCollisionQuality.CqLow;
				break;
			case UnityEngine.ParticleSystemCollisionQuality.Medium:
				ret = ProtoPs.ParticleSystemCollisionQuality.CqMedium;
				break;
			}
			return ret;
		}
		public static ProtoPs.ParticleSystemCollisionMode ConvertCollisionMode(UnityEngine.ParticleSystemCollisionMode m)
		{
			ProtoPs.ParticleSystemCollisionMode ret = new ProtoPs.ParticleSystemCollisionMode ();
			switch (m) {
			case UnityEngine.ParticleSystemCollisionMode.Collision2D:
				ret = ProtoPs.ParticleSystemCollisionMode.CmCollision2D;
				break;
			case UnityEngine.ParticleSystemCollisionMode.Collision3D:
				ret = ProtoPs.ParticleSystemCollisionMode.CmCollision3D;
				break;
			}
			return ret;
		}
		public static ProtoPs.ParticleSystemCollisionType ConvertCollisionType(UnityEngine.ParticleSystemCollisionType type)
		{
			ProtoPs.ParticleSystemCollisionType ret = new ProtoPs.ParticleSystemCollisionType ();
			switch (type) {
			case UnityEngine.ParticleSystemCollisionType.Planes:
				ret = ProtoPs.ParticleSystemCollisionType.CtPlanes;
				break;
			case UnityEngine.ParticleSystemCollisionType.World:
				ret = ProtoPs.ParticleSystemCollisionType.CtWorld;
				break;
			}
			return ret;
		}
		public static ProtoPs.CollisionModule ConvertCollision(ExporterConfig config, UnityEngine.ParticleSystem ps,  UnityEngine.ParticleSystem.CollisionModule c)
		{
			ProtoPs.CollisionModule ret = new ProtoPs.CollisionModule ();
			ret.MaxKillSpeed = c.maxKillSpeed;
#if !UNITY_5_6
			ret.MultiplyColliderForceByParticleSpeed = c.multiplyColliderForceByParticleSpeed;
			ret.MultiplyColliderForceByCollisionAngle = c.multiplyColliderForceByCollisionAngle;
			ret.MultiplyColliderForceByParticleSize = c.multiplyColliderForceByParticleSize;
			ret.ColliderForce = c.colliderForce;
#endif
			ret.SendCollisionMessages = c.sendCollisionMessages;
			ret.RadiusScale = c.radiusScale;
			ret.VoxelSize = c.voxelSize;
			ret.Quality = ConvertCollisionQuality(c.quality);
			ret.MaxCollisionShapes = c.maxCollisionShapes;
			ret.EnableDynamicColliders = c.enableDynamicColliders;
			ret.CollidesWith = ConvertLayerMask (c.collidesWith);
			ret.MaxPlaneCount = c.maxPlaneCount;
			ret.MinKillSpeed = c.minKillSpeed;
			ret.LifetimeLossMultiplier = c.lifetimeLossMultiplier;
			ret.LifetimeLoss = ConvertMinMaxCurve (c.lifetimeLoss);
			ret.BounceMultiplier = c.bounceMultiplier;
			ret.Bounce = ConvertMinMaxCurve (c.bounce);
			ret.DampenMultiplier = c.dampenMultiplier;
			ret.Dampen = ConvertMinMaxCurve (c.dampen);
			ret.Mode = ConvertCollisionMode(c.mode);
			ret.Type = ConvertCollisionType(c.type);
			ret.Enabled = c.enabled;		
			for (int i = 0; i < ret.MaxPlaneCount; i++) 
			{
				UnityEngine.Transform plane = c.GetPlane (i);
				if (plane != null) {
					//string planeName = ExporterUtility.FindRelativePath (plane, ps.transform);
					string planeName = plane.name;
					ret.Planes.Add (planeName);
				}
			}
				
			return ret;
		}
		public static ProtoPs.NoiseModule ConvertNoise(UnityEngine.ParticleSystem.NoiseModule n)
		{
			ProtoPs.NoiseModule pn = new ProtoPs.NoiseModule();
			//if(n.enabled)
			{
				pn.Enabled = n.enabled;
#if !UNITY_5_6
				pn.PositionAmount = ConvertMinMaxCurve(n.positionAmount);
#endif
				//remap
				pn.RemapEnabled = n.remapEnabled;

				pn.RemapZMultiplier = n.remapZMultiplier;
				pn.RemapZ = ConvertMinMaxCurve(n.remapZ);
				pn.RemapYMultiplier = n.remapYMultiplier;
				pn.RemapY = ConvertMinMaxCurve(n.remapY);
				pn.RemapXMultiplier = n.remapXMultiplier;
				pn.RemapX = ConvertMinMaxCurve(n.remapX);
				pn.RemapMultiplier = n.remapMultiplier;
				pn.Remap = ConvertMinMaxCurve(n.remapMultiplier);

				//scroll the noise map
				pn.ScrollSpeed = ConvertMinMaxCurve(n.scrollSpeed);
				pn.ScrollSpeedMultiplier = n.scrollSpeedMultiplier;

				// 1d, 2d, 3d noise
				pn.Quality = (ProtoPs.ParticleSystemNoiseQuality)n.quality;
				
				//Octave
				//When combining each octave, zoom in by this amount.
				pn.OctaveScale = n.octaveScale;
				//When combining each octave, scale the intensity by this amount.
				pn.OctaveMultiplier = n.octaveMultiplier;
				//     Layers of noise that combine to produce final noise.
				pn.OctaveCount = n.octaveCount;

				//Higher frequency noise will reduce the strength by a proportional amount, if enabled.
				pn.Damping = n.damping;

				//Low values create soft, smooth noise, and high values create rapidly changing noise.
				pn.Frequency = n.frequency;

				//strength
				pn.StrengthZMultiplier = n.strengthZMultiplier;
				pn.StrengthZ = ConvertMinMaxCurve(n.strengthZ);
				pn.StrengthY = ConvertMinMaxCurve(n.strengthY);
				pn.StrengthYMultiplier = n.strengthYMultiplier;
				pn.StrengthXMultiplier = n.strengthXMultiplier;
				pn.StrengthX = ConvertMinMaxCurve(n.strengthX);
				pn.Strength = ConvertMinMaxCurve(n.strength);
				pn.StrengthMultiplier = n.strengthMultiplier;

				//
				pn.SeparateAxes = n.separateAxes;

				// How much the noise affects the particle rotation, in degrees per second.
#if !UNITY_5_6
				pn.RotationAmount = ConvertMinMaxCurve(n.rotationAmount);
				//How much the noise affects the particle sizes, applied as a multiplier on the size of each particle.
				pn.SizeAmount = ConvertMinMaxCurve(n.sizeAmount);
#endif
			}
			return pn;
		}
		
	}
}
