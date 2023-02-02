namespace ARWorldEditor
{
	public enum RENDER_TEXTURE_SIZE_TYPE {
		AbsoluteSizeInPixel = 0,
		RelativeToScreen = 1,
		Count = 2,
	}

	[UnityEngine.CreateAssetMenu( menuName = "i3dRenderTexture" , fileName = "RenderTexture.asset" ) ]
	public class EngineRenderTexture : UnityEngine.ScriptableObject
	{
		public int abs_width = 256;
		public int abs_height = 256;
		public float rel_width = 1;
		public float rel_height = 1;
		public RENDER_TEXTURE_SIZE_TYPE size_type = RENDER_TEXTURE_SIZE_TYPE.AbsoluteSizeInPixel;
	}
}