using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.UI
{
    public class KeyBoard : MonoBehaviour
    {
        /// <summary>
        /// 计算器用于显示文字的地方
        /// </summary>
        public TextMesh text;
        /// <summary>
        /// 键盘
        /// </summary>
        public GameObject keyboard;

        public void OnClick(int id)
        {
            text.text = id.ToString();
        }

        public void Open()
        {
            keyboard.SetActive(!keyboard.activeSelf);
        }

        public void Close()
        {
            StartCoroutine(CloseEnumarator());
        }

        IEnumerator CloseEnumarator()
        {
            yield return new WaitForSeconds(0.5f);
            keyboard.SetActive(false);
        }
    }
}
