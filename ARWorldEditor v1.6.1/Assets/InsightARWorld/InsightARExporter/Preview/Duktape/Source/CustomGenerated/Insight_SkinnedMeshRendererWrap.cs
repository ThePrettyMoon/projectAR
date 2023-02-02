//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Insight.SkinnedMeshRendererExtension
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_Insight_SkinnedMeshRenderer : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_getBounds(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.getBounds(self);
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
        public static int Bind_getMaterial(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        UnityEngine.SkinnedMeshRenderer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        var ret = Insight.SkinnedMeshRendererExtension.getMaterial(self, arg0);
                        duk_push_classvalue(ctx, ret);
                        return 1;
                    }
                    if (argc == 0)
                    {
                        UnityEngine.SkinnedMeshRenderer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        var ret = Insight.SkinnedMeshRendererExtension.getMaterial(self);
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
        public static int Bind_getMaterialCount(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.getMaterialCount(self);
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
        public static int Bind_loadFromFiles(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                string arg0;
                arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                string arg1;
                arg1 = DuktapeDLL.duk_get_string(ctx, 1);
                Insight.SkinnedMeshRendererExtension.loadFromFiles(self, arg0, arg1);
                return 0;
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
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.toString(self);
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
        public static int Bind_getEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.getEnabled(self);
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
        public static int Bind_setEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool arg0;
                arg0 = DuktapeDLL.duk_get_boolean(ctx, 0);
                Insight.SkinnedMeshRendererExtension.setEnabled(self, arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_gameObject(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.gameObject(self);
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
        public static int Bind_isActiveAndEnabled(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.isActiveAndEnabled(self);
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
        public static int Bind_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.name(self);
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
        public static int Bind_tag(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.tag(self);
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
        public static int Bind_transform(IntPtr ctx)
        {
            try
            {
                UnityEngine.SkinnedMeshRenderer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = Insight.SkinnedMeshRendererExtension.transform(self);
                duk_push_classvalue(ctx, ret);
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
            duk_begin_class(ctx, "SkinnedMeshRenderer", typeof(UnityEngine.SkinnedMeshRenderer), object_private_ctor);
            duk_add_method(ctx, "loadFromFiles", Bind_loadFromFiles, -1);
            duk_add_method(ctx, "getMaterial", Bind_getMaterial, -1);
            duk_add_method(ctx, "toString", Bind_toString, -1);
            duk_add_property(ctx, "bounds", Bind_getBounds, null, -1);
            duk_add_property(ctx, "material", Bind_getMaterial, null, -1);
            duk_add_property(ctx, "materialCount", Bind_getMaterialCount, null, -1);
            duk_add_property(ctx, "enabled", Bind_getEnabled, Bind_setEnabled, - 1);
            duk_add_property(ctx, "gameObject", Bind_gameObject,null, -1);
            duk_add_property(ctx, "isActiveAndEnabled", Bind_isActiveAndEnabled,null, -1);
            duk_add_property(ctx, "name", Bind_name,null, -1);
            duk_add_property(ctx, "tag", Bind_tag,null, -1);
            duk_add_property(ctx, "transform", Bind_transform, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
