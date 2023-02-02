using UnityEngine;

namespace ARWorldEditor
{
	[RequireComponent (typeof(Camera))]
	public class EnginePostEffect : MonoBehaviour
	{
		//private Camera _camera = null;
		//private int _camera_param = -1;

		public Material _material = null;
		public bool [] _using_depth = null; // use the depth texture, instead of the color texture as a property.
		public RenderTexture [] _targets = null;

		protected void Awake()
		{
			//_camera = GetComponent< Camera >();
			//_camera_param = Shader.PropertyToID( "_CameraNearFarSpan" );
		}

        protected void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
/*			if( null != material )
			{
				material.SetVector( _camera_param
					, new Vector4( _camera.nearClipPlane , _camera.farClipPlane , _camera.farClipPlane - _camera.nearClipPlane , 0 ) );

				for( int pi = 0 ; pi < material.passCount ; ++pi )
				{
					RenderTexture d = dest;
					if( targets.Length > pi )
						if( null != targets[pi] )
							d = targets[pi];
					Graphics.Blit( src , d , material , pi );
				}
			}
			else
			{*/
				Graphics.Blit(null, dest,_material);
			//}
		}
	}
}