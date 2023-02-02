using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insight
{

	public static class ImageExtension
	{

		public static string toString(this Image image)
		{
			return image.ToString();
		}

		public static bool getEnabled(this Image image)
		{
            return image.enabled;
		}
		public static void setEnabled(this Image image, bool enabled)
		{
            if (image != null) image.enabled = enabled;
        }

		public static GameObject gameObject(this Image image)
		{
			return image.gameObject;
		}

		public static bool isActiveAndEnabled(this Image image)
		{
			return image.gameObject.activeSelf;
		}

		public static string name(this Image image)
		{
			return image.name;
		}

		public static string tag(this Image image)
		{
			return image.tag;
		}

		public static Transform transform(this Image image)
		{
			return image.transform;
		}



        public static Vector4 getColor(this Image image) {

            Color _color = image.color;
            return new Vector4(_color.r, _color.g, _color.b, _color.a);
        }

        public static void setColor(this Image image, Insight.Vector4 color)
        {
            image.color = new Color(color.x,color.y,color.z,color.w);
        }
    }


}

