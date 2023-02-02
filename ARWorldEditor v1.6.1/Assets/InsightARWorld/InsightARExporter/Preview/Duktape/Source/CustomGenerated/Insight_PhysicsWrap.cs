//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Physics
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS
{
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Physics : DuktapeBinding
    {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Physics();
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
        public static int BindStatic_Raycast(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 6)
                    {
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        Insight.Vector3 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        UnityEngine.RaycastHit arg2;
                        Insight.RaycastHit iarg2;
                        duk_get_classvalue(ctx, 2, out iarg2);
                        float arg3;
                        arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                        int arg4;
                        arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                        UnityEngine.QueryTriggerInteraction arg5;
                        arg5 = (UnityEngine.QueryTriggerInteraction)DuktapeDLL.duk_get_int(ctx, 5);
                        var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), out arg2, arg3, arg4, arg5);
                        if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 2))
                        {
                            //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg2));
                            //DuktapeDLL.duk_unity_put_target_i(ctx, 2);
                            iarg2.Copy(arg2);
                        }
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                    if (argc == 5)
                    {
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3), null, typeof(float), typeof(int)))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            UnityEngine.RaycastHit arg2;
                            Insight.RaycastHit iarg2;
                            duk_get_classvalue(ctx, 2, out iarg2);
                            float arg3;
                            arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), out arg2, arg3, arg4);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 2))
                            {
                                iarg2.Copy(arg2);
                               // Insight.RaycastHit raycastHit = Insight.RaycastHit.FromRaycast(arg2);
                               // duk_push_classvalue(ctx, raycastHit);
                               // UnityEngine.Debug.Log("point " + raycastHit.point.x);
                               // DuktapeDLL.duk_unity_put_target_i(ctx, 2);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 4)
                    {
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3), typeof(float), typeof(int)))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), arg2, arg3);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3), null, typeof(float)))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            UnityEngine.RaycastHit arg2;
                            Insight.RaycastHit iarg2;
                            duk_get_classvalue(ctx, 2, out iarg2);
                            float arg3;
                            arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), out arg2, arg3);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 2))
                            {
                                //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg2));
                                //DuktapeDLL.duk_unity_put_target_i(ctx, 2);
                                iarg2.Copy(arg2);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Ray), null, typeof(float), typeof(int)))
                        {
                            UnityEngine.Ray arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            UnityEngine.RaycastHit arg1;
                            Insight.RaycastHit iarg1;
                            duk_get_classvalue(ctx, 1, out iarg1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            int arg3;
                            arg3 = DuktapeDLL.duk_get_int(ctx, 3);
                            var ret = UnityEngine.Physics.Raycast(arg0, out arg1, arg2, arg3);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 1))
                            {
                                //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg1));
                                //DuktapeDLL.duk_unity_put_target_i(ctx, 1);
                                iarg1.Copy(arg1);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3), typeof(float)))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3( arg1), arg2);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3), null))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            UnityEngine.RaycastHit arg2;
                            Insight.RaycastHit iarg2;
                            duk_get_classvalue(ctx, 2, out iarg2);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), out arg2);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 2))
                            {
                                //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg2));
                                //DuktapeDLL.duk_unity_put_target_i(ctx, 2);
                                iarg2.Copy(arg2);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Ray), typeof(float), typeof(int)))
                        {
                            UnityEngine.Ray arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            int arg2;
                            arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                            var ret = UnityEngine.Physics.Raycast(arg0, arg1, arg2);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Ray), null, typeof(float)))
                        {
                            UnityEngine.Ray arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            UnityEngine.RaycastHit arg1;
                            Insight.RaycastHit iarg1;
                            duk_get_classvalue(ctx, 1, out iarg1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            var ret = UnityEngine.Physics.Raycast(arg0, out arg1, arg2);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 1))
                            {
                                //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg1));
                                //DuktapeDLL.duk_unity_put_target_i(ctx, 1);
                                iarg1.Copy(arg1);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3)))
                        {
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var ret = UnityEngine.Physics.Raycast(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1));
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Ray), typeof(float)))
                        {
                            UnityEngine.Ray arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            var ret = UnityEngine.Physics.Raycast(arg0, arg1);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Ray), null))
                        {
                            UnityEngine.Ray arg0;
                            duk_get_structvalue(ctx, 0, out arg0);
                            UnityEngine.RaycastHit arg1;
                            Insight.RaycastHit iarg1;
                            duk_get_classvalue(ctx, 1, out iarg1);
                            var ret = UnityEngine.Physics.Raycast(arg0, out arg1);
                            if (!DuktapeDLL.duk_is_null_or_undefined(ctx, 1))
                            {
                                //duk_push_classvalue(ctx, Insight.RaycastHit.FromRaycast(arg1));
                                //DuktapeDLL.duk_unity_put_target_i(ctx, 1);
                                iarg1.Copy(arg1);
                            }
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Ray arg0;
                        duk_get_structvalue(ctx, 0, out arg0);
                        var ret = UnityEngine.Physics.Raycast(arg0);
                        DuktapeDLL.duk_push_boolean(ctx, ret);
                        return 1;
                    }
                } while (false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticRead_gravity(IntPtr ctx)
        {
            try
            {
                var ret = UnityEngine.Physics.gravity;
                duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindStaticWrite_gravity(IntPtr ctx)
        {
            try
            {
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                UnityEngine.Physics.gravity = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        public static int reg(IntPtr ctx)
        {
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "Physics", typeof(UnityEngine.Physics), BindConstructor);
            duk_add_method(ctx, "Raycast", BindStatic_Raycast, -2);
            duk_add_property(ctx, "gravity", BindStaticRead_gravity, BindStaticWrite_gravity, -2);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
