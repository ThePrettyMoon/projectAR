using AOT;
using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace EZXR.Glass.Common
{
    public class GlassDevicePermissionHelper
    {
        public delegate void GlassDevicePermissionListener(int usbVender, int usbProdyctId, bool isGranted);

        private static GlassDevicePermissionListener callbackOut;
        public static void RequestGlassDevicePermission(GlassDevicePermissionListener callback)
        {
            callbackOut = callback;
            NativeAPI.RequestGlassPermission(OnGlassPermssionChange);
        }
        public static bool IsGlassDevicePermissionAllGranted()
        {
            if (Application.isEditor)
            {
                return true;
            }
            else
            {
                return NativeAPI.IsGlassPermissionAllGranted();
            }
        }

        [MonoPInvokeCallback(typeof(GlassDevicePermissionHelper.GlassDevicePermissionListener))]
        private static void OnGlassPermssionChange(int usbVenderId, int usbProductId, bool isGranted)
        {
            if (callbackOut != null)
                callbackOut(usbVenderId, usbProductId, isGranted);
        }
        private partial struct NativeAPI
        {
            [DllImport(NativeConsts.NativeLibrary)]
            public static extern bool IsGlassPermissionAllGranted();


            [DllImport(NativeConsts.NativeLibrary)]
            public static extern void RequestGlassPermission(GlassDevicePermissionListener callback);
        }
    }
}