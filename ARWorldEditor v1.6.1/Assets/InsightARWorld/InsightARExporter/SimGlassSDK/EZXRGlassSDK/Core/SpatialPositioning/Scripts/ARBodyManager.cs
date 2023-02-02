using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    [ScriptExecutionOrder(-13)]
    public class ARBodyManager : MonoBehaviour
    {
        #region singleton
        private static ARBodyManager _instance;
        public static ARBodyManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        private void Awake()
        {
            _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}