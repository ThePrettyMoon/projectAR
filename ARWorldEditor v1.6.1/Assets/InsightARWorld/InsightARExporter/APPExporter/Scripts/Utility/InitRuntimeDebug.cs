using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsightARWorldEditor.Debug
{
    public class InitRuntimeDebug : MonoBehaviour
    {
        private void Awake()
        {
            SRDebug.Init();
        }
    }
}
