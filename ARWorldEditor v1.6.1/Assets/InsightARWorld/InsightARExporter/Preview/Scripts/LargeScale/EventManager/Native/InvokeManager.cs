using Duktape;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTime = UnityEngine.Time;

namespace Insight
{
    public class InvokeManager
    {
        private const string TAG = "InvokeManager";
        /// <summary>
        /// 缓存延迟调用的事件
        /// </summary>
        private static Dictionary<int, InvokeData> InvokeEventDic = new Dictionary<int, InvokeData>();
        /// <summary>
        /// 暂存待删除的事件
        /// </summary>
        private static List<int> deleteList = new List<int>();
        public class InvokeData
        {
            public float repeatTime;
            public bool isRepeat;
            public Action method;
            public float time;
        }

        public static long Invoke(DuktapeObject moduleObject, DuktapeObject funcObjcet, float _time)
        {
            Action temp = () => {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr);
            };

            int hashcode = temp.GetHashCode();
            InvokeManager.Invoke(hashcode, new InvokeManager.InvokeData() { time = _time, method = temp, isRepeat = false, repeatTime = 0f });
            return hashcode;
        }
        public static long InvokeRepeating(DuktapeObject moduleObject, DuktapeObject funcObjcet, float _time, float _repeatTime)
        {
            Action temp = () => {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr);
            };

            int hashcode = temp.GetHashCode();
            InvokeManager.Invoke(hashcode, new InvokeManager.InvokeData() { time = _time, method = temp, isRepeat = true, repeatTime = _repeatTime });
            return hashcode;
        }

        /// <summary>
        /// 取消事件
        /// </summary>
        /// <param name="code"></param>
        public static void CancelInvoke(int code)
        {
            foreach (var item in InvokeEventDic)
            {
                Debug.LogError(item.Key + " " + code + " bool:" + (code == item.Key));
            }

            if (!InvokeEventDic.ContainsKey(code))
            {
                InsightDebug.Log(TAG, "未注册该方法");
                return;
            }

            deleteList.Add(code);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="hashcode"></param>
        /// <param name="data"></param>
        private static void Invoke(int hashcode, InvokeData data)
        {
            data.time += UnityTime.unscaledTime;
            InvokeEventDic.Add(hashcode, data);
            DukTapeVMManager.Instance.StartCoroutine(Invoke());

        }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <returns></returns>
        static IEnumerator Invoke()
        {
            if (InvokeEventDic.Count > 1)
            {
                yield break;
            }

            while (InvokeEventDic.Count > 0)
            {
                if (!DukTapeVMManager.Instance.IsLoaded)
                {
                    InvokeEventDic.Clear();
                    deleteList.Clear();
                    yield break;
                }


                foreach (var item in InvokeEventDic)
                {
                    if (item.Value.time <= UnityTime.unscaledTime)
                    {
                        item.Value.method?.Invoke();
                        if (item.Value.isRepeat)
                        {
                            item.Value.time += item.Value.repeatTime;
                        }
                        else
                        {
                            deleteList.Add(item.Key);
                        }
                    }
                }


                if (deleteList.Count > 0)
                {
                    foreach (var item in deleteList)
                    {
                        InvokeEventDic.Remove(item);
                    }
                    deleteList.Clear();
                }

                yield return null;
            }
        }
    }
}