using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace RenderEngine
{
	static class ExporterParticle 
	{
		public static string Export( ExporterConfig config , ParticleSystem ps, string parent = "")
		{			
			//render
			string matPath = "";
			string trailMatPath = "";		
			ParticleSystemRenderer render = ps.gameObject.GetComponent<ParticleSystemRenderer>();
			if(render != null)
			{	
				if (render.sharedMaterial != null) {
					matPath = ExporterResource.ExportMaterial (config, render.sharedMaterial, false, null, null);	
				} 
				else 
				{
					Material defaultMat = UnityEditor.AssetDatabase.LoadAssetAtPath<Material>("Assets/InsightARWorld/Samples/Materials/MaterialParticle.mat");
					matPath = ExporterResource.ExportMaterial (config, defaultMat, false, null, null);	
				}
				
				if(render.trailMaterial != null)
					trailMatPath = ExporterResource.ExportMaterial(config,render.trailMaterial,false,null,null);
			}	
			//
			string dst;			
			string srcPath = config.asset_path.GetPath(ps);
			if(ExporterUtility.PrepareExport(srcPath, config.root_path, ExporterConfig.PARTICLE_EXTENSION, out dst))
			{	
				ProtoPs.ParticleSystem pps = new ProtoPs.ParticleSystem();
				pps.ParentPS = parent;
						
				if(render != null)
				{
					pps.Renderer = ConverterParticleSystem.ConvertRenderer(render, config);
					pps.Renderer.Material = matPath;
					pps.Renderer.TrailMaterial = trailMatPath;
				}
				pps.Main = ConverterParticleSystem.ConvertMain(config, ps.main);
				pps.Emission = ConverterParticleSystem.ConvertEmission(ps.emission);
				pps.Shape = ConverterParticleSystem.ConvertShape(ps.shape, config);
				pps.VelocityOverLifetime = ConverterParticleSystem.ConvertVelocityOverLifetime(ps.velocityOverLifetime);
				pps.LimitVelocityOverLifetime = ConverterParticleSystem.ConvertLimitVelocityOverLifetime(ps.limitVelocityOverLifetime);
				pps.InheritVelocity = ConverterParticleSystem.ConvertInheritVelocity(ps.inheritVelocity);
				pps.ForceOverLifetime = ConverterParticleSystem.ConvertForceOverLifetime(ps.forceOverLifetime);
				
				pps.TextureSheetAnimation = ConverterParticleSystem.ConvertTextureSheetAnimation(ps.textureSheetAnimation);

				pps.ExternalForces = ConverterParticleSystem.ConvertExternalForces(ps.externalForces);	
				pps.RotationBySpeed = ConverterParticleSystem.ConvertRotationBySpeed(ps.rotationBySpeed);
				pps.RotationOverLifetime = ConverterParticleSystem.ConvertRotationOverLifetime(ps.rotationOverLifetime);
				pps.SizeBySpeed = ConverterParticleSystem.ConvertSizeBySpeed(ps.sizeBySpeed);
				pps.SizeOverLifetime = ConverterParticleSystem.ConvertSizeOverLifetime(ps.sizeOverLifetime);
				pps.ColorBySpeed = ConverterParticleSystem.ConvertColorBySpeed(ps.colorBySpeed);
				pps.ColorOverLifetime = ConverterParticleSystem.ConvertColorOverLifetime(ps.colorOverLifetime);	
							
				pps.Noise = ConverterParticleSystem.ConvertNoise(ps.noise);
				pps.Trails = ConverterParticleSystem.ConvertTrails (ps.trails);
				pps.Trigger = ConverterParticleSystem.ConvertTriggers (config, ps);

			
				pps.SubEmitters = ConvertSubEmitters(ps, config, dst);
				pps.Collision = ConverterParticleSystem.ConvertCollision (config, ps, ps.collision);
				//todo:lq
				//pps.CustomData  pps.Lights pps.collsion 
				
				ExporterUtility.WriteBinary( pps, config.root_path, dst);
				//ExporterUtility.WriteJson(pps, config.root_path, dst);
			}

			return dst;
			
		}

		public static ProtoPs.SubEmittersModule ConvertSubEmitters(UnityEngine.ParticleSystem ps, ExporterConfig config, string parent)
		{
			UnityEngine.ParticleSystem.SubEmittersModule m = ps.subEmitters;


			ProtoPs.SubEmittersModule pm = new ProtoPs.SubEmittersModule();
			//if(m.enabled)
			{
				pm.Enabled = m.enabled;
				//pm.SubEmittersCount = m.subEmittersCount;

				for(int i=0; i< m.subEmittersCount; i++)
				{
					if (m.GetSubEmitterSystem (i) != null) 
					{
						ProtoPs.SubEmitter sub = new ProtoPs.SubEmitter ();
						//sub.Ps = Export(config, m.GetSubEmitterSystem(i), parent);
						bool found = false;
						sub.Ps = ExporterUtility.FindRelativePath (m.GetSubEmitterSystem (i).transform, ps.transform, out found);
						sub.Type = ConverterParticleSystem.ConvertSubEmitterType (m.GetSubEmitterType (i));
						sub.Inherit = ConverterParticleSystem.ConvertSubEmitterProperty (m.GetSubEmitterProperties (i));
						pm.SubEmitters.Add (sub);
					}
				}

				pm.SubEmittersCount = pm.SubEmitters.Count;
				
			}
			return pm;
		}
	}

}

