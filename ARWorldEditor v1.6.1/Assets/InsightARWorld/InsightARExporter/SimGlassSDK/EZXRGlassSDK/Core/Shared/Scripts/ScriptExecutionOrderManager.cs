using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace EZXR.Glass.Common
{
    public class ScriptExecutionOrder : Attribute
    {
        public int order;
        public ScriptExecutionOrder(int order) { this.order = order; }
    }

#if UNITY_EDITOR
    [InitializeOnLoad]
    public class ScriptExecutionOrderManager
    {
        static ScriptExecutionOrderManager()
        {
            foreach (MonoScript monoScript in MonoImporter.GetAllRuntimeMonoScripts())
            {
                if (monoScript.GetClass() != null)
                {
                    foreach (var attr in Attribute.GetCustomAttributes(monoScript.GetClass(), typeof(ScriptExecutionOrder)))
                    {
                        var newOrder = ((ScriptExecutionOrder)attr).order;
                        if (MonoImporter.GetExecutionOrder(monoScript) != newOrder)
                            MonoImporter.SetExecutionOrder(monoScript, newOrder);
                    }
                }
            }
        }
    }
#endif

    /*
    // USAGE EXAMPLE
    [ScriptExecutionOrder(100)]
    public class MyScript : MonoBehaviour
    {
            // ...
    }
    */
}