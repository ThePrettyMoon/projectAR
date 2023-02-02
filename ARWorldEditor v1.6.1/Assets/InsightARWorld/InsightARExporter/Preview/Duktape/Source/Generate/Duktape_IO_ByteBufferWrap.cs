//#if UNITY_STANDALONE_WIN
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// Type: Duktape.IO.ByteBuffer
// Unity: 2018.4.8f1
using System;
using System.Collections.Generic;

namespace DuktapeJS {
    using Duktape;
    [JSBindingAttribute(65537)]
    [UnityEngine.Scripting.Preserve]
    public class DuktapeJS_DuktapeJS_ByteBuffer : DuktapeBinding {
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindConstructor(IntPtr ctx)
        {
            try
            {
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                int arg1;
                arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                Duktape.IO.ByteBufferAllocator arg2;
                duk_get_classvalue(ctx, 2, out arg2);
                var o = new Duktape.IO.ByteBuffer(arg0, arg1, arg2);
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
        public static int Bind_ToString(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
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
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_Release(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Release();
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
        public static int Bind_Retain(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.Retain();
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
        public static int Bind_CheckReadalbe(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.CheckReadalbe(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ReadBytes(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        Duktape.IO.ByteBuffer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        byte[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        var ret = self.ReadBytes(arg0, arg1, arg2);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 2)
                    {
                        Duktape.IO.ByteBuffer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.IntPtr arg0;
                        duk_get_primitive(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        var ret = self.ReadBytes(arg0, arg1);
                        DuktapeDLL.duk_push_int(ctx, ret);
                        return 1;
                    }
                    if (argc == 1)
                    {
                        Duktape.IO.ByteBuffer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        int arg0;
                        arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                        self.ReadBytes(arg0);
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
        public static int Bind_ReadUByte(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadUByte();
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
        public static int Bind_ReadSByte(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadSByte();
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
        public static int Bind_ReadBoolean(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadBoolean();
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
        public static int Bind_ReadAllBytes(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadAllBytes();
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
        public static int Bind_ReadSingle(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadSingle();
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ReadDouble(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadDouble();
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ReadInt16(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadInt16();
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
        public static int Bind_ReadUInt16(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadUInt16();
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
        public static int Bind_ReadInt32(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadInt32();
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
        public static int Bind_ReadUInt32(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadUInt32();
                DuktapeDLL.duk_push_uint(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ReadInt64(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadInt64();
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_ReadUInt64(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.ReadUInt64();
                DuktapeDLL.duk_push_number(ctx, ret);
                return 1;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_EnsureCapacity(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.EnsureCapacity(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteByte(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                byte arg0;
                arg0 = (byte)DuktapeDLL.duk_get_int(ctx, 0);
                self.WriteByte(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteSByte(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                sbyte arg0;
                arg0 = (sbyte)DuktapeDLL.duk_get_int(ctx, 0);
                self.WriteSByte(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteBytes(IntPtr ctx)
        {
            try
            {
                var argc = DuktapeDLL.duk_get_top(ctx);
                do
                {
                    if (argc == 3)
                    {
                        Duktape.IO.ByteBuffer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        byte[] arg0;
                        duk_get_primitive_array(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        int arg2;
                        arg2 = DuktapeDLL.duk_get_int(ctx, 2);
                        self.WriteBytes(arg0, arg1, arg2);
                        return 0;
                    }
                    if (argc == 2)
                    {
                        Duktape.IO.ByteBuffer self;
                        DuktapeDLL.duk_push_this(ctx);
                        duk_get_classvalue(ctx, -1, out self);
                        DuktapeDLL.duk_pop(ctx);
                        System.IO.MemoryStream arg0;
                        duk_get_classvalue(ctx, 0, out arg0);
                        int arg1;
                        arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                        self.WriteBytes(arg0, arg1);
                        return 0;
                    }
                    if (argc == 1)
                    {
                        if (duk_match_types(ctx, argc, typeof(Duktape.IO.ByteBuffer)))
                        {
                            Duktape.IO.ByteBuffer self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            Duktape.IO.ByteBuffer arg0;
                            duk_get_classvalue(ctx, 0, out arg0);
                            self.WriteBytes(arg0);
                            return 0;
                        }
                        if (duk_match_types(ctx, argc, typeof(byte[])))
                        {
                            Duktape.IO.ByteBuffer self;
                            DuktapeDLL.duk_push_this(ctx);
                            duk_get_classvalue(ctx, -1, out self);
                            DuktapeDLL.duk_pop(ctx);
                            byte[] arg0;
                            duk_get_primitive_array(ctx, 0, out arg0);
                            self.WriteBytes(arg0);
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
        public static int Bind_WriteBoolean(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                bool arg0;
                arg0 = DuktapeDLL.duk_get_boolean(ctx, 0);
                self.WriteBoolean(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteInt16(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                short arg0;
                arg0 = (short)DuktapeDLL.duk_get_int(ctx, 0);
                self.WriteInt16(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteUInt16(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                ushort arg0;
                arg0 = (ushort)DuktapeDLL.duk_get_int(ctx, 0);
                self.WriteUInt16(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteInt32(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int arg0;
                arg0 = DuktapeDLL.duk_get_int(ctx, 0);
                self.WriteInt32(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteUInt32(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                uint arg0;
                arg0 = DuktapeDLL.duk_get_uint(ctx, 0);
                self.WriteUInt32(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteInt64(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                long arg0;
                arg0 = (long)DuktapeDLL.duk_get_number(ctx, 0);
                self.WriteInt64(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteUInt64(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                ulong arg0;
                arg0 = (ulong)DuktapeDLL.duk_get_number(ctx, 0);
                self.WriteUInt64(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteSingle(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                float arg0;
                arg0 = (float)DuktapeDLL.duk_get_number(ctx, 0);
                self.WriteSingle(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int Bind_WriteDouble(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                double arg0;
                arg0 = DuktapeDLL.duk_get_number(ctx, 0);
                self.WriteDouble(arg0);
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_data(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.data;
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
        public static int BindRead_capacity(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.capacity;
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
        public static int BindRead_writerIndex(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.writerIndex;
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
        public static int BindWrite_writerIndex(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.writerIndex = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_readableBytes(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.readableBytes;
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
        public static int BindRead_readerIndex(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.readerIndex;
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
        public static int BindWrite_readerIndex(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                int value;
                value = DuktapeDLL.duk_get_int(ctx, 0);
                self.readerIndex = value;
                return 0;
            }
            catch (Exception exception)
            {
                return DuktapeDLL.duk_exception(ctx, exception);
            }
        }
        [UnityEngine.Scripting.Preserve]
        [AOT.MonoPInvokeCallbackAttribute(typeof(DuktapeDLL.duk_c_function))]
        public static int BindRead_maxCapacity(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.maxCapacity;
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
        public static int BindRead_isWritable(IntPtr ctx)
        {
            try
            {
                Duktape.IO.ByteBuffer self;
                DuktapeDLL.duk_push_this(ctx);
                duk_get_classvalue(ctx, -1, out self);
                DuktapeDLL.duk_pop(ctx);
                var ret = self.isWritable;
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
            duk_begin_namespace(ctx, "DuktapeJS");
            duk_begin_class(ctx, "ByteBuffer", typeof(Duktape.IO.ByteBuffer), BindConstructor);
            duk_add_method(ctx, "toString", Bind_ToString, -1);
            duk_add_method(ctx, "release", Bind_Release, -1);
            duk_add_method(ctx, "retain", Bind_Retain, -1);
            duk_add_method(ctx, "checkReadalbe", Bind_CheckReadalbe, -1);
            duk_add_method(ctx, "readBytes", Bind_ReadBytes, -1);
            duk_add_method(ctx, "readUByte", Bind_ReadUByte, -1);
            duk_add_method(ctx, "readSByte", Bind_ReadSByte, -1);
            duk_add_method(ctx, "readBoolean", Bind_ReadBoolean, -1);
            duk_add_method(ctx, "readAllBytes", Bind_ReadAllBytes, -1);
            duk_add_method(ctx, "readSingle", Bind_ReadSingle, -1);
            duk_add_method(ctx, "readDouble", Bind_ReadDouble, -1);
            duk_add_method(ctx, "readInt16", Bind_ReadInt16, -1);
            duk_add_method(ctx, "readUInt16", Bind_ReadUInt16, -1);
            duk_add_method(ctx, "readInt32", Bind_ReadInt32, -1);
            duk_add_method(ctx, "readUInt32", Bind_ReadUInt32, -1);
            duk_add_method(ctx, "readInt64", Bind_ReadInt64, -1);
            duk_add_method(ctx, "readUInt64", Bind_ReadUInt64, -1);
            duk_add_method(ctx, "ensureCapacity", Bind_EnsureCapacity, -1);
            duk_add_method(ctx, "writeByte", Bind_WriteByte, -1);
            duk_add_method(ctx, "writeSByte", Bind_WriteSByte, -1);
            duk_add_method(ctx, "writeBytes", Bind_WriteBytes, -1);
            duk_add_method(ctx, "writeBoolean", Bind_WriteBoolean, -1);
            duk_add_method(ctx, "writeInt16", Bind_WriteInt16, -1);
            duk_add_method(ctx, "writeUInt16", Bind_WriteUInt16, -1);
            duk_add_method(ctx, "writeInt32", Bind_WriteInt32, -1);
            duk_add_method(ctx, "writeUInt32", Bind_WriteUInt32, -1);
            duk_add_method(ctx, "writeInt64", Bind_WriteInt64, -1);
            duk_add_method(ctx, "writeUInt64", Bind_WriteUInt64, -1);
            duk_add_method(ctx, "writeSingle", Bind_WriteSingle, -1);
            duk_add_method(ctx, "writeDouble", Bind_WriteDouble, -1);
            duk_add_property(ctx, "data", BindRead_data, null, -1);
            duk_add_property(ctx, "capacity", BindRead_capacity, null, -1);
            duk_add_property(ctx, "writerIndex", BindRead_writerIndex, BindWrite_writerIndex, -1);
            duk_add_property(ctx, "readableBytes", BindRead_readableBytes, null, -1);
            duk_add_property(ctx, "readerIndex", BindRead_readerIndex, BindWrite_readerIndex, -1);
            duk_add_property(ctx, "maxCapacity", BindRead_maxCapacity, null, -1);
            duk_add_property(ctx, "isWritable", BindRead_isWritable, null, -1);
            duk_end_class(ctx);
            duk_end_namespace(ctx);
            return 0;
        }
    }
}
//#endif
