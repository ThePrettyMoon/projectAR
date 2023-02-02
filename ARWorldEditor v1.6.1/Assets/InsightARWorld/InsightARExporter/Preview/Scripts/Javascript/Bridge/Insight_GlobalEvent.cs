using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;
using System;

namespace Insight
{
    public class GlobalEvent
    {
        public static void AddCaptureLister(DuktapeObject moduleObject, DuktapeObject funcObjcet)
        {
            UserEventManager.onUserCaptureEvent = null;
            UserEventManager.onUserCaptureEvent += (int eventType) =>
            {
                IntPtr context = DukTapeVMManager.Instance.DuktapeVM.context.rawValue;
                DuktapeUtility.CallMethod(context, moduleObject.heapPtr, funcObjcet.heapPtr, eventType);
            };
        }
    }
}
