using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public class ARGesture 
    {
        private static GestureType m_GestureType = GestureType.NO_HAND;
        private static bool m_IsRunning;

        public static GestureType GestureType
        {
            get
            {
                return m_GestureType;
            }
        }

        public static bool IsRunning
        {
            get
            {
                return m_IsRunning;
            }
        }

        public static void OnFixedUpdate()
        {
            m_IsRunning = NativeGesture.IsGestureRunning();
            if (!m_IsRunning) return;
            m_GestureType = NativeGesture.GetGestureResult();

            if (m_GestureType == GestureType.ENTER_GESTURE ||
                m_GestureType == GestureType.RETURN_GESTURE ||
                m_GestureType == GestureType.HOME_GESTURE)
            {
                Debug.Log("Unity Call Gesture Type " + m_GestureType);
            }
        }
    }
}
