using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof { 
	public struct DeviceInfo
	{
		public int displayWidthPixels;
		public int displayHeightPixels;
		public float displayRefreshRateHz;
		public int targetEyeWidthPixels;
		public int targetEyeHeightPixels;
		public float targetFovXRad;
		public float targetFovYRad;
		public ViewFrustum targetFrustumLeft;
		public ViewFrustum targetFrustumRight;
		public float targetFrustumConvergence;
		public float targetFrustumPitch;
	}

	public struct ViewFrustum
	{
		public float left;           //!< Left Plane of Frustum
		public float right;          //!< Right Plane of Frustum
		public float top;            //!< Top Plane of Frustum
		public float bottom;         //!< Bottom Plane of Frustum

		public float near;           //!< Near Plane of Frustum
		public float far;            //!< Far Plane of Frustum (Arbitrary)
	}
}
