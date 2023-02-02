//#if UNITY_STANDALONE_WIN
// Assembly: UnityEngine.PhysicsModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: UnityEngine.Collision
// Unity: 2019.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_UnityEngine_Collision : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                var o = new UnityEngine.Collision();
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
        public static int Bind_GetContact(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                var ret = self.GetContact(arg0);
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
        public static int Bind_GetContacts(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(UnityEngine.ContactPoint[])))
                        {
                            UnityEngine.Collision self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            UnityEngine.ContactPoint[] arg0;
                            duk_get_structvalue_array(ctx, 0, out arg0);
                            var ret = self.GetContacts(arg0);
                            DuktapeDLL.duk_push_int(ctx, ret);
                            return 1;
                        }
                        if (duk_match_types(ctx, argc, typeof(System.Collections.Generic.List<UnityEngine.ContactPoint>)))
                        {
                            UnityEngine.Collision self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            System.Collections.Generic.List<UnityEngine.ContactPoint> arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            var ret = self.GetContacts(arg0);
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
        public static int BindRead_relativeVelocity(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = MathConverter.FromVector3( self.relativeVelocity);
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
        public static int BindRead_rigidbody(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.rigidbody;
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
        public static int BindRead_collider(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.collider;
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
        public static int BindRead_transform(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.transform;
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
        public static int BindRead_gameObject(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
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
        public static int BindRead_contactCount(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.contactCount;
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
        public static int BindRead_impulse(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = MathConverter.FromVector3(self.impulse);
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
        public static int Bind_name(IntPtr ctx)
        {
            try
            {
                UnityEngine.Collision self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.transform.name;
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
            duk_begin_class(ctx, "Collision", typeof(UnityEngine.Collision), BindConstructor);
            duk_add_method(ctx, "getContact", Bind_GetContact, -1);
            duk_add_method(ctx, "getContacts", Bind_GetContacts, -1);
            duk_add_property(ctx, "rigidbody", BindRead_rigidbody, null, -1);
            duk_add_property(ctx, "relativeVelocity", BindRead_relativeVelocity, null, -1);
            duk_add_property(ctx, "contactCount", BindRead_contactCount, null, -1);
            duk_add_property(ctx, "collider", BindRead_collider, null, -1);
            duk_add_property(ctx, "transform", BindRead_transform, null, -1);
            duk_add_property(ctx, "gameObject", BindRead_gameObject, null, -1);
            duk_add_property(ctx, "impulse", BindRead_impulse, null, -1);
            duk_add_property(ctx, "name", Bind_name, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
