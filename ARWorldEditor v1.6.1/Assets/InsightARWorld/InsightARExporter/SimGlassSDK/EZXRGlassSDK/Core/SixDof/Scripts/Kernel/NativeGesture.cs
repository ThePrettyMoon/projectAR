using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public class NativeGesture
    {
        public static GestureType GetGestureResult()
        {
            //long sessionId = NativeTracking.SessionId;
            //if (sessionId == 0) return GestureType.NO_HAND;
            return (GestureType)NativeAPI.getGestureResult();
        }

        public static void PauseGesture()
        {
            //long sessionId = NativeTracking.SessionId;
            //if (sessionId != 0)
            //{
            NativeAPI.pauseGesture();
            //}
        }

        public static void ResumeGesture()
        {
            //long sessionId = NativeTracking.SessionId;
            //if (sessionId != 0)
            //{
            NativeAPI.resumeGesture();
            //}
        }

        public static bool IsGestureRunning()
        {
            //long sessionId = NativeTracking.SessionId;
            //if (sessionId == 0) return false;
            return NativeAPI.isGestureRunning();
        }
        private partial struct NativeAPI
        {
#if UNITY_ANDROID
        [DllImport(NativeConsts.NativeLibrary)]
        public static extern GestureType getGestureResult();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void pauseGesture();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern void resumeGesture();

        [DllImport(NativeConsts.NativeLibrary)]
        public static extern bool isGestureRunning();
#else 
            public static GestureType getGestureResult()
            {
                return GestureType.NO_HAND;
            }

            public static void pauseGesture() { }

            public static void resumeGesture() { }

            public static bool isGestureRunning() { return true; }
#endif
        }
    }
}
