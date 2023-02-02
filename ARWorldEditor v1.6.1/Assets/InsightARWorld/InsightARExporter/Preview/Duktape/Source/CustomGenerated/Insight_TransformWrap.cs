//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Transform
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Transform : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetParent(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        UnityEngine.Transform arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        self.SetParent(arg0, arg1);
                        return 0;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_rotate(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        bool arg3;
                        arg3 = DuktapeDLL.duk_get_boolean(ctx, 3);
                        self.Rotate(arg0, arg1, arg2, arg3?UnityEngine.Space.World:UnityEngine.Space.Self);
                        return 0;
                    }
                    if (argc == 3)
                    {
                        if (duk_match_types(ctx, argc, typeof(float), typeof(float), typeof(float)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            float arg0;
                            arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            self.Rotate(arg0, arg1, arg2);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(float), typeof(bool)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            bool arg2;
                            arg2 = DuktapeDLL.duk_get_boolean(ctx, 2);
                            self.Rotate(MathConverter.ToVector3(arg0), arg1, arg2?UnityEngine.Space.World:UnityEngine.Space.Self);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(bool)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            bool arg1;
                            arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                            self.Rotate( MathConverter.ToVector3(arg0), arg1?UnityEngine.Space.World:UnityEngine.Space.Self);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(float)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            self.Rotate(MathConverter.ToVector3(arg0), arg1);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.Rotate(MathConverter.ToVector3(arg0));
                        return 0;
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
        public static int Bind_RotateAround(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                Insight.Vector3 arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                self.RotateAround(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1), arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_LookAt(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Transform), typeof(Insight.Vector3)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            UnityEngine.Transform arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.LookAt(arg0, MathConverter.ToVector3(arg1));
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3), typeof(Insight.Vector3)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            Insight.Vector3 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.LookAt(MathConverter.ToVector3(arg0), MathConverter.ToVector3(arg1));
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Transform)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            UnityEngine.Transform arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            self.LookAt(arg0);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(Insight.Vector3)))
                        {
                            UnityEngine.Transform self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Insight.Vector3 arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            self.LookAt(MathConverter.ToVector3(arg0));
                            return 0;
                        }
                        break;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TransformDirection(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.TransformDirection(arg0, arg1, arg2);
                        duk_push_classvalue(ctx,MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.TransformDirection(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_InverseTransformDirection(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.InverseTransformDirection(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.InverseTransformDirection(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TransformVector(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.TransformVector(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.TransformVector(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_InverseTransformVector(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.InverseTransformVector(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.InverseTransformVector(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_TransformPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.TransformPoint(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.TransformPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_InverseTransformPoint(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        var ret = self.InverseTransformPoint(arg0, arg1, arg2);
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        var ret = self.InverseTransformPoint(MathConverter.ToVector3(arg0));
                        duk_push_classvalue(ctx, MathConverter.FromVector3(ret));
                        return 1;
                    }
                } while(false);
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Find(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.Find(arg0);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetChild(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetChild(arg0);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = MathConverter.FromVector3( self.position);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_position(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.position = MathConverter.ToVector3( value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_localPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret =MathConverter.FromVector3( self.localPosition);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_localPosition(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.localPosition = MathConverter.ToVector3( value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_right(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.right;
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
        public static int BindWrite_right(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.right = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_up(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.up;
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
        public static int BindWrite_up(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.up = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_forward(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.forward;
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
        public static int BindWrite_forward(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.forward = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_rotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.rotation; 
                duk_push_classvalue(ctx,MathConverter.FromQuaternion(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_rotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion value; 
                duk_get_classvalue(ctx, 0, out value);
                self.rotation =MathConverter.ToQuaternion(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_localRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.localRotation;
                duk_push_classvalue(ctx, MathConverter.FromQuaternion(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_localRotation(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Quaternion value;
                duk_get_classvalue(ctx, 0, out value);
                self.localRotation =MathConverter.ToQuaternion(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_localScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.localScale;
                duk_push_classvalue(ctx,MathConverter.FromVector3(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_localScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector3 value;
                duk_get_classvalue(ctx, 0, out value);
                self.localScale = MathConverter.ToVector3(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_parent(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.parent;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_worldToLocalMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var mat = self.localToWorldMatrix;
                var ret = MathConverter.FromMatrix4x4(mat);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_localToWorldMatrix(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var mat = self.localToWorldMatrix;
                var ret = MathConverter.FromMatrix4x4(mat);
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_childCount(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.childCount;
                DuktapeDLL.duk_push_int(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_lossyScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.lossyScale;
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
        public static int BindRead_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.name;
                DuktapeDLL.duk_push_string(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string value;
                value = DuktapeDLL.duk_get_string(ctx, 0);
                self.name = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_gameObject(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.gameObject;
                duk_push_classvalue(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }

        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_translate(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 4)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        bool arg3;
                        arg3 = DuktapeDLL.duk_get_boolean(ctx, 3);
                        self.Translate(arg0, arg1, arg2, arg3?UnityEngine.Space.World:UnityEngine.Space.Self);
                        return 0;
                    }
                    if (argc == 3)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        float arg0;
                        arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                        float arg1;
                        arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                        float arg2;
                        arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                        self.Translate(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        self.Translate(MathConverter.ToVector3(arg0), arg1?UnityEngine.Space.World:UnityEngine.Space.Self);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        UnityEngine.Transform self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        Insight.Vector3 arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.Translate(MathConverter.ToVector3(arg0));
                        return 0;
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Transform self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ToString();
                DuktapeDLL.duk_push_string(ctx, ret);
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
            duk_begin_namespace(ctx, "Insight");
            duk_begin_class(ctx, "Transform", typeof(UnityEngine.Transform), object_private_ctor);
            duk_add_property(ctx, "childCount", BindRead_childCount, null, -1);
            duk_add_property(ctx, "forward", BindRead_forward, BindWrite_forward, -1);
            duk_add_property(ctx, "right", BindRead_right, BindWrite_right, -1);
            duk_add_property(ctx, "up", BindRead_up, BindWrite_up, -1);
            duk_add_property(ctx, "localPosition", BindRead_localPosition, BindWrite_localPosition, -1);
            duk_add_property(ctx, "localRotation", BindRead_localRotation, BindWrite_localRotation, -1);
            duk_add_property(ctx, "localScale", BindRead_localScale, BindWrite_localScale, -1);
            duk_add_property(ctx, "localToWorldMatrix", BindRead_localToWorldMatrix, null, -1);
            duk_add_property(ctx, "worldToLocalMatrix", BindRead_worldToLocalMatrix, null, -1);     
            duk_add_property(ctx, "parent", BindRead_parent, null, -1);
            duk_add_property(ctx, "position", BindRead_position, BindWrite_position, -1);
            duk_add_property(ctx, "rotation", BindRead_rotation, BindWrite_rotation, -1);
            duk_add_property(ctx, "lossyScale", BindRead_lossyScale, null, -1);     
            duk_add_method(ctx, "find", Bind_Find, -1);
            duk_add_method(ctx, "getChild", Bind_GetChild, -1);
            duk_add_method(ctx, "inverseTransformDirection", Bind_InverseTransformDirection, -1);
            duk_add_method(ctx, "inverseTransformPoint", Bind_InverseTransformPoint, -1);
            duk_add_method(ctx, "inverseTransformVector", Bind_InverseTransformVector, -1);
            duk_add_method(ctx, "transformDirection", Bind_TransformDirection, -1);
            duk_add_method(ctx, "transformPoint", Bind_TransformPoint, -1);
            duk_add_method(ctx, "transformVector", Bind_TransformVector, -1);
            duk_add_method(ctx, "lookAt", Bind_LookAt, -1);
            duk_add_method(ctx, "rotate", Bind_rotate, -1);
            duk_add_method(ctx, "rotateAround", Bind_RotateAround, -1);
            duk_add_method(ctx, "setParent", Bind_SetParent, -1);
            duk_add_method(ctx, "translate", Bind_translate, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_property(ctx, "gameObject", BindRead_gameObject, null, -1);
            duk_add_property(ctx, "name", BindRead_name, BindWrite_name, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
