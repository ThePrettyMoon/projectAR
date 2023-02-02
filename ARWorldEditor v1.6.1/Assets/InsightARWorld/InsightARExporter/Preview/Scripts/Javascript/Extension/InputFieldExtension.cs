using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Insight {

    public static class InputFieldExtension
    {
        private static bool mInteractable = true;

        public static bool getInteractable(this UnityEngine.UI.InputField inputField)
        {
            return inputField.interactable;
        }
        public static void setInteractable(this UnityEngine.UI.InputField inputField, bool interactable)
        {
            inputField.interactable = interactable;
        }

        public static int getCount(this UnityEngine.UI.InputField inputField)
        {
            return inputField.text.Length;
        }

        public static string toString(this UnityEngine.UI.InputField inputField)
        {
            return inputField.ToString();
        }

		public static void setTextWithoutNotify(this UnityEngine.UI.InputField inputField, string text)
		{
			inputField.text = text;
		}

		public static bool getEnabled(this UnityEngine.UI.InputField inputField)
        {
            return inputField.enabled;
        }
        public static void setEnabled(this UnityEngine.UI.InputField inputField, bool enabled)
        {
            if (inputField != null) inputField.enabled = enabled;
        }

        public static GameObject gameObject(this UnityEngine.UI.InputField inputField)
        {
            return inputField.gameObject;
        }

        public static bool isActiveAndEnabled(this UnityEngine.UI.InputField inputField)
        {
            return inputField.gameObject.activeSelf;
        }

        public static string name(this UnityEngine.UI.InputField inputField)
        {
            return inputField.name;
        }

        public static string tag(this UnityEngine.UI.InputField inputField)
        {
            return inputField.tag;
        }

        public static Transform transform(this UnityEngine.UI.InputField inputField)
        {
            return inputField.transform;
        }


    }

}


