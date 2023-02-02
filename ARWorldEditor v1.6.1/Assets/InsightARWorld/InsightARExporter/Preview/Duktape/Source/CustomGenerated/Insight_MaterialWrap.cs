//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Material
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Material : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Shader)))
                        {
                            UnityEngine.Shader arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var o = new UnityEngine.Material(arg0);
                            duk_bind_native(ctx, o);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.Material)))
                        {
                            UnityEngine.Material arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var o = new UnityEngine.Material(arg0);
                            duk_bind_native(ctx, o);
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
        public static int Bind_HasProperty(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.HasProperty(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.HasProperty(arg0);
                            DuktapeDLL.duk_push_boolean(ctx, ret);
                            return 1;
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
        public static int Bind_EnableKeyword(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                self.EnableKeyword(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_DisableKeyword(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                self.DisableKeyword(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_IsKeywordEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.IsKeywordEnabled(arg0);
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetShaderPassEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                bool arg1;
                arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                self.SetShaderPassEnabled(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetShaderPassEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.GetShaderPassEnabled(arg0);
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetPassName(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetPassName(arg0);
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
        public static int Bind_FindPass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = self.FindPass(arg0);
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
        public static int Bind_SetOverrideTag(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                self.SetOverrideTag(arg0, arg1);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetTag(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        string arg2;
                        arg2 = DuktapeDLL.duk_get_string(ctx, 2);
                        var ret = self.GetTag(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_string(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        bool arg1;
                        arg1 = DuktapeDLL.duk_get_boolean(ctx, 1);
                        var ret = self.GetTag(arg0, arg1);
                        DuktapeDLL.duk_push_string(ctx, ret);
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
        public static int Bind_Lerp(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Material arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                UnityEngine.Material arg1;
                duk_get_classvalue(ctx, 1, out arg1);
                float arg2;
                arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                self.Lerp(arg0, arg1, arg2);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetPass(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.SetPass(arg0);
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_CopyPropertiesFromMaterial(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Material arg0;
                duk_get_classvalue(ctx, 0, out arg0);
                self.CopyPropertiesFromMaterial(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_GetTexturePropertyNames(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.Collections.Generic.List<string> arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.GetTexturePropertyNames(arg0);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.GetTexturePropertyNames();
                        duk_push_classvalue(ctx, ret);
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
        public static int Bind_GetTexturePropertyNameIDs(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.Collections.Generic.List<int> arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        self.GetTexturePropertyNameIDs(arg0);
                        return 0;
                    }
                    if (argc == 0)
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = self.GetTexturePropertyNameIDs();
                        duk_push_classvalue(ctx, ret);
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
        public static int Bind_SetFloat(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(float)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            self.SetFloat(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(float)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            float arg1;
                            arg1 = (float)DuktapeDLL.duk_get_number(ctx, 1);
                            self.SetFloat(arg0, arg1);
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
        public static int Bind_SetInt(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            self.SetInt(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            int arg1;
                            arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                            self.SetInt(arg0, arg1);
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
        public static int Bind_SetColor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);

                if (argc == 2)
                {
                    if (duk_match_types(ctx, argc, typeof(string), typeof(Insight.Vector4)))
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        string arg0;
                        arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var color = new UnityEngine.Color(arg1.x, arg1.y, arg1.z, arg1.w);
                        self.SetColor(arg0, color);
                        return 0;
                    }
                    else if (duk_match_types(ctx, argc, typeof(int), typeof(Insight.Vector4)))
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        var color = new UnityEngine.Color(arg1.x, arg1.y, arg1.z, arg1.w);
                        self.SetColor(arg0, color);
                        return 0;
                    }
                    else
                    {
                        UnityEngine.Material self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        Insight.Vector4 arg1;
                        duk_get_classvalue(ctx, 1, out arg1);
                        UnityEngine.Color ret = new UnityEngine.Color(arg1.x, arg1.y, arg1.z, arg1.w);
                        self.SetColor(arg0, ret);
                        return 0;
                    }
                }
                return DuktapeDLL.duk_generic_error(ctx, "no matched method variant");
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_SetVector(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(Insight.Vector4)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            Insight.Vector4 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var color = new UnityEngine.Color(arg1.x, arg1.y, arg1.z, arg1.w);
                            self.SetVector(arg0, color);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(Insight.Vector4)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            Insight.Vector4 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var color = new UnityEngine.Color(arg1.x, arg1.y, arg1.z, arg1.w);
                            self.SetVector(arg0, color);
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
        public static int Bind_SetMatrix(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(Insight.Matrix4x4)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            Insight.Matrix4x4 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var mat = MathConverter.ToMatrix4x4(arg1);
                            self.SetMatrix(arg0, mat);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(Insight.Matrix4x4)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            Insight.Matrix4x4 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            var mat = MathConverter.ToMatrix4x4(arg1);
                            self.SetMatrix(arg0, mat);
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
        public static int Bind_SetTexture(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(UnityEngine.Texture)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            UnityEngine.Texture arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTexture(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(UnityEngine.Texture)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            UnityEngine.Texture arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTexture(arg0, arg1);
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
        public static int Bind_SetBuffer(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(UnityEngine.ComputeBuffer)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            UnityEngine.ComputeBuffer arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetBuffer(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(UnityEngine.ComputeBuffer)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            UnityEngine.ComputeBuffer arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetBuffer(arg0, arg1);
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
        public static int Bind_SetFloatArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.Collections.Generic.List<float>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Collections.Generic.List<float> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetFloatArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(System.Collections.Generic.List<float>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            System.Collections.Generic.List<float> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetFloatArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(float[])))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            float[] arg1;
                            duk_get_primitive_array(ctx, 1, out arg1);
                            self.SetFloatArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(float[])))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            float[] arg1;
                            duk_get_primitive_array(ctx, 1, out arg1);
                            self.SetFloatArray(arg0, arg1);
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
        public static int Bind_SetColorArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.Collections.Generic.List<UnityEngine.Color>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Collections.Generic.List<UnityEngine.Color> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetColorArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(System.Collections.Generic.List<UnityEngine.Color>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            System.Collections.Generic.List<UnityEngine.Color> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetColorArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(string), typeof(UnityEngine.Color[])))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            UnityEngine.Color[] arg1;
                            duk_get_structvalue_array(ctx, 1, out arg1);
                            self.SetColorArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(UnityEngine.Color[])))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            UnityEngine.Color[] arg1;
                            duk_get_structvalue_array(ctx, 1, out arg1);
                            self.SetColorArray(arg0, arg1);
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
        public static int Bind_GetFloat(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetFloat(arg0);
                            DuktapeDLL.duk_push_number(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetFloat(arg0);
                            DuktapeDLL.duk_push_number(ctx, ret);
                            return 1;
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
        public static int Bind_GetInt(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetInt(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetInt(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
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
        public static int Bind_GetColor(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetColor(arg0);
                            var color = new Insight.Vector4(ret.r, ret.g, ret.b, ret.a);
                            duk_push_classvalue(ctx, color);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetColor(arg0);
                            var color = new Insight.Vector4(ret.r, ret.g, ret.b, ret.a);
                            duk_push_classvalue(ctx, color);
                            return 1;
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
        public static int Bind_GetVector(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetVector(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromVector4(ret));
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetVector(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromVector4(ret));
                            return 1;
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
        public static int Bind_GetMatrix(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetMatrix(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromMatrix4x4(ret));
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetMatrix(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromMatrix4x4(ret));
                            return 1;
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
        public static int Bind_GetTexture(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetTexture(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetTexture(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
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
        public static int Bind_GetFloatArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.Collections.Generic.List<float>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Collections.Generic.List<float> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.GetFloatArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(System.Collections.Generic.List<float>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            System.Collections.Generic.List<float> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.GetFloatArray(arg0, arg1);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetFloatArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetFloatArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
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
        public static int Bind_GetColorArray(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(System.Collections.Generic.List<UnityEngine.Color>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            System.Collections.Generic.List<UnityEngine.Color> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.GetColorArray(arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(System.Collections.Generic.List<UnityEngine.Color>)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            System.Collections.Generic.List<UnityEngine.Color> arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.GetColorArray(arg0, arg1);
                            return 0;
                        }
                        break;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetColorArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetColorArray(arg0);
                            duk_push_classvalue(ctx, ret);
                            return 1;
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
        public static int Bind_SetTextureOffset(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(Insight.Vector2)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            Insight.Vector2 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTextureOffset(arg0, MathConverter.ToVector2( arg1));
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(Insight.Vector2)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            Insight.Vector2 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTextureOffset(arg0, MathConverter.ToVector2(arg1));
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
        public static int Bind_SetTextureScale(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(Insight.Vector2)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            Insight.Vector2 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTextureScale(arg0, MathConverter.ToVector2(arg1));
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(Insight.Vector2)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            Insight.Vector2 arg1;
                            duk_get_classvalue(ctx, 1, out arg1);
                            self.SetTextureScale(arg0, MathConverter.ToVector2(arg1));
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
        public static int Bind_GetTextureOffset(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetTextureOffset(arg0);
                            duk_push_classvalue(ctx,MathConverter.FromVector2(ret));
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetTextureOffset(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                            return 1;
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
        public static int Bind_GetTextureScale(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = self.GetTextureScale(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = self.GetTextureScale(arg0);
                            duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                            return 1;
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
        public static int Bind_toString(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.MaterialExtension.toString(self);
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
        public static int Bind_getTextureName(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            var ret = Insight.MaterialExtension.getTextureName(self, arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(int)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            var ret = Insight.MaterialExtension.getTextureName(self, arg0);
                            DuktapeDLL.duk_push_string(ctx, ret);
                            return 1;
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
        public static int Bind_propertyToID(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var ret = Insight.MaterialExtension.propertyToID(self, arg0);
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
        public static int Bind_setText(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 16)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string), typeof(float), typeof(float), typeof(int), typeof(float), typeof(float), typeof(float), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            float arg3;
                            arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            float arg5;
                            arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                            float arg6;
                            arg6 = (float)DuktapeDLL.duk_get_number(ctx, 6);
                            float arg7;
                            arg7 = (float)DuktapeDLL.duk_get_number(ctx, 7);
                            string arg8;
                            arg8 = DuktapeDLL.duk_get_string(ctx, 8);
                            float arg9;
                            arg9 = (float)DuktapeDLL.duk_get_number(ctx, 9);
                            float arg10;
                            arg10 = (float)DuktapeDLL.duk_get_number(ctx, 10);
                            float arg11;
                            arg11 = (float)DuktapeDLL.duk_get_number(ctx, 11);
                            float arg12;
                            arg12 = (float)DuktapeDLL.duk_get_number(ctx, 12);
                            float arg13;
                            arg13 = (float)DuktapeDLL.duk_get_number(ctx, 13);
                            float arg14;
                            arg14 = (float)DuktapeDLL.duk_get_number(ctx, 14);
                            float arg15;
                            arg15 = (float)DuktapeDLL.duk_get_number(ctx, 15);
                            Insight.MaterialExtension.setText(self, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(string), typeof(float), typeof(float), typeof(int), typeof(float), typeof(float), typeof(float), typeof(string), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float), typeof(float)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            float arg2;
                            arg2 = (float)DuktapeDLL.duk_get_number(ctx, 2);
                            float arg3;
                            arg3 = (float)DuktapeDLL.duk_get_number(ctx, 3);
                            int arg4;
                            arg4 = DuktapeDLL.duk_get_int(ctx, 4);
                            float arg5;
                            arg5 = (float)DuktapeDLL.duk_get_number(ctx, 5);
                            float arg6;
                            arg6 = (float)DuktapeDLL.duk_get_number(ctx, 6);
                            float arg7;
                            arg7 = (float)DuktapeDLL.duk_get_number(ctx, 7);
                            string arg8;
                            arg8 = DuktapeDLL.duk_get_string(ctx, 8);
                            float arg9;
                            arg9 = (float)DuktapeDLL.duk_get_number(ctx, 9);
                            float arg10;
                            arg10 = (float)DuktapeDLL.duk_get_number(ctx, 10);
                            float arg11;
                            arg11 = (float)DuktapeDLL.duk_get_number(ctx, 11);
                            float arg12;
                            arg12 = (float)DuktapeDLL.duk_get_number(ctx, 12);
                            float arg13;
                            arg13 = (float)DuktapeDLL.duk_get_number(ctx, 13);
                            float arg14;
                            arg14 = (float)DuktapeDLL.duk_get_number(ctx, 14);
                            float arg15;
                            arg15 = (float)DuktapeDLL.duk_get_number(ctx, 15);
                            Insight.MaterialExtension.setText(self, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
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
        public static int Bind_setTextureByName(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 2)
                    {
                        if (duk_match_types(ctx, argc, typeof(string), typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            string arg0;
                            arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            Insight.MaterialExtension.setTextureByName(self, arg0, arg1);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(int), typeof(string)))
                        {
                            UnityEngine.Material self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            int arg0;
                            arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                            string arg1;
                            arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                            Insight.MaterialExtension.setTextureByName(self, arg0, arg1);
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
        public static int BindRead_shader(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.shader;
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
        public static int BindWrite_shader(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Shader value;
                duk_get_classvalue(ctx, 0, out value);
                self.shader = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_color(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.color;
                var color = new Insight.Vector4(ret.r, ret.b, ret.b, ret.a);
                duk_push_classvalue(ctx, color);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_color(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector4 value;
                duk_get_classvalue(ctx, 0, out value);
                self.color = new UnityEngine.Color(value.x,value.y,value.z,value.w);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_mainTexture(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.mainTexture;
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
        public static int BindWrite_mainTexture(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.Texture value;
                duk_get_classvalue(ctx, 0, out value);
                self.mainTexture = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_mainTextureOffset(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.mainTextureOffset;
                duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_mainTextureOffset(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 value;
                duk_get_classvalue(ctx, 0, out value);
                self.mainTextureOffset =MathConverter.ToVector2( value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_mainTextureScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.mainTextureScale;
                duk_push_classvalue(ctx, MathConverter.FromVector2(ret));
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_mainTextureScale(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                Insight.Vector2 value;
                duk_get_classvalue(ctx, 0, out value);
                self.mainTextureScale = MathConverter.ToVector2(value);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_renderQueue(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.renderQueue;
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
        public static int BindWrite_renderQueue(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.renderQueue = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_globalIlluminationFlags(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.globalIlluminationFlags;
                DuktapeDLL.duk_push_int(ctx, (int)ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_globalIlluminationFlags(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                UnityEngine.MaterialGlobalIlluminationFlags value;
                value = (UnityEngine.MaterialGlobalIlluminationFlags)DuktapeDLL.duk_get_int(ctx, 0);
                self.globalIlluminationFlags = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_doubleSidedGI(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.doubleSidedGI;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_doubleSidedGI(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.doubleSidedGI = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_enableInstancing(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.enableInstancing;
                DuktapeDLL.duk_push_boolean(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindWrite_enableInstancing(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool value;
                value = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.enableInstancing = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_passCount(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.passCount;
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
        public static int BindRead_shaderKeywords(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.shaderKeywords;
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
        public static int BindWrite_shaderKeywords(IntPtr ctx)
        {
            try
            {
                UnityEngine.Material self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string[] value;
                duk_get_primitive_array(ctx, 0, out value);
                self.shaderKeywords = value;
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
            duk_begin_class(ctx, "Material", typeof(UnityEngine.Material), BindConstructor);
            duk_add_property(ctx, "color", BindRead_color, BindWrite_color, -1);
            //sdk ÊÇstring£¬untiyÐèÒªtexture
            duk_add_property(ctx, "mainTexture", BindRead_mainTexture, BindWrite_mainTexture, -1);
            duk_add_property(ctx, "passCount", BindRead_passCount, null, -1);
            duk_add_property(ctx, "renderQueue", BindRead_renderQueue, BindWrite_renderQueue, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_method(ctx, "getColor", Bind_GetColor, -1);
            duk_add_method(ctx, "getFloat", Bind_GetFloat, -1);
            duk_add_method(ctx, "getInt", Bind_GetInt, -1);
            duk_add_method(ctx, "getMatrix", Bind_GetMatrix, -1);
            duk_add_method(ctx, "getTextureName", Bind_getTextureName, -1);
            duk_add_method(ctx, "getVector", Bind_GetVector, -1);
            duk_add_method(ctx, "hasProperty", Bind_HasProperty, -1);
            duk_add_method(ctx, "propertyToID", Bind_propertyToID, -1);
            duk_add_method(ctx, "setColor", Bind_SetColor, -1);
            duk_add_method(ctx, "setFloat", Bind_SetFloat, -1);
            duk_add_method(ctx, "setInt", Bind_SetInt, -1);
            duk_add_method(ctx, "setMatrix", Bind_SetMatrix, -1);
            duk_add_method(ctx, "setText", Bind_setText, -1);
            duk_add_method(ctx, "setTextureByName", Bind_setTextureByName, -1);
            duk_add_method(ctx, "setVector", Bind_SetVector, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
