namespace RenderEngine
{
	class UtilityRendering
	{
		public static void ConvertSHEMapConstants( UnityEngine.Vector4 [] dest , UnityEngine.Rendering.SphericalHarmonicsL2 src , float ambientIntensity )
		{
			UnityEngine.Vector4 [] coeff = new UnityEngine.Vector4[3];

			// Exposure = pow(x, gamma)
			float exposure = UnityEngine.Mathf.Pow (ambientIntensity, 2.2f);

			// SHA
			for( int ic = 0 ; ic < 3 ; ic++ )
			{
				coeff[ic].x = exposure * src[ic,3];
				coeff[ic].y = exposure * src[ic,1];
				coeff[ic].z = exposure * src[ic,2];
				coeff[ic].w = exposure * (src[ic,0] - src[ic,6]);
			}

			dest[0] = coeff[0]; // cAr
			dest[1] = coeff[1]; // cAg
			dest[2] = coeff[2]; // cAb

			// SHB
			for( int ic = 0; ic < 3 ; ic++ )
			{
				coeff[ic].x = exposure * src[ic,4];
				coeff[ic].y = exposure * src[ic,5];
				coeff[ic].z = exposure * 3.0f * src[ic,6];
				coeff[ic].w = exposure * src[ic,7];
			}

			dest[3] = coeff[0]; // cBr
			dest[4] = coeff[1]; // cBg
			dest[5] = coeff[2]; // cBb

			// SHC
			coeff[0].x = exposure * src[0,8];
			coeff[0].y = exposure * src[1,8];
			coeff[0].z = exposure * src[2,8];
			coeff[0].w = 1.0f;
			dest[6] = coeff[0]; // cC
		}

	}
}