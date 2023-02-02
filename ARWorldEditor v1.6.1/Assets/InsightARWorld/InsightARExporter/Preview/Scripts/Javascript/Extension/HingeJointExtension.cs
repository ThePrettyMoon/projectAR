using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight
{

	public static class HingeJointExtension
	{

		public static string toString(this HingeJoint hingeJoint)
		{
			return hingeJoint.ToString();
		}

		public static bool getEnabled(this HingeJoint hingeJoint)
		{
            //return hingeJoint.enabled;
            //todo
            return true;
		}
		public static void setEnabled(this HingeJoint hingeJoint, bool enabled)
		{
			//if (hingeJoint != null) hingeJoint.enabled = enabled;
            //todo
		}

		public static GameObject gameObject(this HingeJoint hingeJoint)
		{
			return hingeJoint.gameObject;
		}

		public static bool isActiveAndEnabled(this HingeJoint hingeJoint)
		{
			return hingeJoint.gameObject.activeSelf;
		}

		public static string name(this HingeJoint hingeJoint)
		{
			return hingeJoint.name;
		}

		public static string tag(this HingeJoint hingeJoint)
		{
			return hingeJoint.tag;
		}

		public static Transform transform(this HingeJoint hingeJoint)
		{
			return hingeJoint.transform;
		}


	}


}

