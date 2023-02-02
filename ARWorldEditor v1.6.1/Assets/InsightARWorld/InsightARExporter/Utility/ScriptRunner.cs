using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Duktape;
using System.IO;
using Object = UnityEngine.Object;

public class ScriptRunner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
#if UNITY_EDITOR
        static void CreateScriptAsset(string templatePath, string destName)
        {
#if UNITY_2019_1_OR_NEWER
            UnityEditor.ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, destName);
#else
            typeof(UnityEditor.ProjectWindowUtil)
                .GetMethod("CreateScriptAsset", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                .Invoke(null, new object[] { templatePath, destName });
#endif
        }

        [UnityEditor.MenuItem("Assets/InsightARWorld/Create/i3dJavaScript", false, 1)]
        static void Createi3dJavaScript()
        {
            CreateScriptAsset("Assets/InsightARWorld/InsightARExporter/TemplateScript/JSTemplatesScript.txt", "newJavaScript.js");
        }

        [UnityEditor.MenuItem("Assets/InsightARWorld/Create/i3dLua", false, 1)]
        static void Createi3dLua()
        {
            CreateScriptAsset("Assets/InsightARWorld/InsightARExporter/TemplateScript/LuaTemplatesScript.txt", "newLua.lua");
        }
#endif

#region params
        private string mRelativeFilePath = null;
        public UnityEngine.GameObject OverrideGameObject;

#if UNITY_EDITOR
        public Object ScriptSource;
#endif

        public string GetScriptPath()
        {
#if UNITY_EDITOR
            if (ScriptSource != null)
                return UnityEditor.AssetDatabase.GetAssetPath(ScriptSource);
            else
                return "";
#else
        return "";
#endif

        }

        public string RelativeFilePath
        {
            get
            {
                return mRelativeFilePath;
            }
            set
            {
                mRelativeFilePath = value;
            }
        }
#endregion

        private DuktapeObject m_ModuleObject;
        private DuktapeObject m_ModuleInstance;
        private static IntPtr s_VMContext;
        private Dictionary<string, IntPtr> funcPtrs;
        private static int count_instance = 0;
        public DuktapeObject GetModuleInstance()
        {
            return m_ModuleInstance;
        }

        private void Awake()
        {
        }

        private void Start()
        {
            Invoke("Start");
        }

        private void Update()
        {
            Invoke("Update");
        }

        private void FixedUpdate()
        {
            Invoke("FixedUpdate");
        }

        private void OnCollisionEnter(Collision collision)
        {
            Invoke("OnCollisionEnter", collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            Invoke("OnCollisionExit", collision);
        }

        private void OnTriggerEnter(Collider other)
        {
            Invoke("OnTriggerEnter", other);
        }

        private void OnTriggerStay(Collider other)
        {
            Invoke("OnTriggerStay", other);
        }

        private void OnTriggerExit(Collider other)
        {
            Invoke("OnTriggerExit", other);
        }



        private void OnEnable()
        {
            Invoke("OnEnable");
        }

        private void OnMouseDown()
        {
            Invoke("OnMouseDown");
        }

        private void OnMouseUp()
        {
            Invoke("OnMouseUp");
        }

        private void OnApplicationPause(bool pause)
        {
            Invoke("OnApplicationPause", pause);
        }

        private void OnApplicationFocus(bool focus)
        {
            Invoke("OnApplicationFocus", focus);
        }

        private void OnApplicationQuit()
        {
            Invoke("OnApplicationQuit");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Invoke("OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Invoke("OnPointerUp");
        }

        private void OnDisable()
        {
            Invoke("OnDisable");
        }

        private void OnDestroy()
        {
            Invoke("OnDestroy");
        }

        /// <summary>
        /// 创建实例对象
        /// </summary>
        /// <returns></returns>
        private DuktapeObject CreateDuktapeInstance()
        {
            DuktapeDLL.duk_push_heapptr(s_VMContext, m_ModuleObject.heapPtr);
            DuktapeBinding.duk_push_classvalue(s_VMContext, gameObject);
            DuktapeDLL.duk_new(s_VMContext, 1);
            count_instance++;
            DuktapeDLL.duk_put_global_string(s_VMContext, count_instance.ToString());
            DuktapeDLL.duk_get_global_string(s_VMContext, count_instance.ToString());
            IntPtr instance = DuktapeDLL.duk_get_heapptr(s_VMContext, -1);
            uint refid = DuktapeDLL.duk_unity_ref(s_VMContext);
            DuktapeObject o = new DuktapeObject(s_VMContext, refid, instance);
            return o;
        }

        private void Invoke(string funcName, params object[] args)
        {
            try
            {
#if UNITY_EDITOR
            if (DukTapeVMManager.InstanceNoCreate == null|| !DukTapeVMManager.Instance.IsLoaded)
#else
            if (!DukTapeVMManager.Instance.IsLoaded)
#endif
            {
                return;
            }


            if (s_VMContext == IntPtr.Zero || m_ModuleInstance == null || m_ModuleObject == null
                    || m_ModuleInstance.heapPtr == IntPtr.Zero) return;
                IntPtr funcPtr;
                if (funcPtrs.TryGetValue(funcName, out funcPtr))
                {
                    if (IntPtr.Zero != funcPtr)
                    {
                        DuktapeUtility.CallMethod(s_VMContext, m_ModuleInstance.heapPtr, funcPtr, args);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Invoke()
        {

        }


        private void Close()
        {
            int length = ConstMethodInfo.FUNCTION_NAMES.Length;
            if (funcPtrs == null) return;
            var enumetor = funcPtrs.GetEnumerator();
            List<IntPtr> list = new List<IntPtr>();
            while (enumetor.MoveNext())
            {
                var refPtr = enumetor.Current.Value;
                list.Add(refPtr);
            }

            for (int i = 0; i < list.Count; i++) list[i] = IntPtr.Zero;
            funcPtrs.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="relativeFilePath"></param>
        public void ParseScript(string directory, string relativeFilePath)
        {
            int length = ConstMethodInfo.FUNCTION_NAMES.Length;
            funcPtrs = new Dictionary<string, IntPtr>(length);
            string path = Path.Combine(directory, relativeFilePath);
            DukTapeVMManager dukTapeVM = DukTapeVMManager.Instance;
            if (!string.IsNullOrEmpty(path))
            {
                string scriptDirectory = Path.GetDirectoryName(path);
                dukTapeVM.AddSearchPath(scriptDirectory);
                string fileName = Path.GetFileName(path);
                s_VMContext = dukTapeVM.DuktapeVM.context.rawValue;
                m_ModuleObject = dukTapeVM.RunScript(fileName, ref funcPtrs);
                m_ModuleInstance = CreateDuktapeInstance();
            }
            this.mRelativeFilePath = relativeFilePath;
        }
}