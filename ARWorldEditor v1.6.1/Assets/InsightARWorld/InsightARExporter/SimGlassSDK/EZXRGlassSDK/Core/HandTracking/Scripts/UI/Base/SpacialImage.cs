using EZXR.Glass.Hand;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EZXR.Glass.UI
{
    public class SpacialImage : SpacialUIBase
    {
        public enum ImageType
        {
            Simple,
            Filled
        }
        public ImageType imageType;

        protected override void Awake()
        {
            base.Awake();
        }

        private void OnDestroy()
        {

        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();

        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

        }
    }
}