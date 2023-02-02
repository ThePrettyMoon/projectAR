//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityExtensions
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS__UnityExtensions : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityExtensions();
                duk_bind_native(ctx, o);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_Vector3Rot(IntPtr ctx)
        {
            try
            {
                UnityEngine.Vector3 arg0;
                duk_get_structvalue(ctx, 0, out arg0);
                UnityEngine.Quaternion arg1;
                duk_get_structvalue(ctx, 1, out arg1);
                var ret = UnityExtensions.Vector3Rot(arg0, arg1);
                duk_push_structvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStatic_RaycastMousePosition(IntPtr ctx)
        {
            try
            {
                Duktape.DuktapeObject arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                float arg1;
                arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                int arg2;
                arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                var ret = UnityExtensions.RaycastMousePosition(arg0, arg1, arg2);
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "");
            duk_begin_class(ctx, "UnityExtensions", typeof(UnityExtensions), BindConstructor);
            duk_add_method(ctx, "Vector3Rot", BindStatic_Vector3Rot, -2);
            duk_add_method(ctx, "RaycastMousePosition", BindStatic_RaycastMousePosition, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
