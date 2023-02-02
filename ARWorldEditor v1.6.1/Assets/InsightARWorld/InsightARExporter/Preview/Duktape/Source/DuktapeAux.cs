using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Duktape
{
    public delegate string duk_source_position_cb(IntPtr ctx, string funcName, string fileName, int lineNumber);

    // 基础环境
    public static partial class DuktapeAux
    {
        // 用于提取 error.stack 中的 stacktrace 信息
        private static Regex _stRegex;
        public static bool printStacktrace = true;
        public static duk_source_position_cb duk_source_position = default_duk_source_position;

        public static Type GetType(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var type = System.Reflection.Assembly.GetExecutingAssembly().GetType(name);
            return type;
        }

        public static byte[] DumpBytecode(IntPtr ctx, string filename, byte[] source)
        {
            if (ctx == IntPtr.Zero)
            {
                throw new InvalidOperationException();
            }
            if (source == null || source.Length == 0)
            {
                return null;
            }
            if (source[0] == 0xbf)
            {
                return source;
            }
            if (filename == null)
            {
                filename = "eval";
            }
            DuktapeDLL.duk_unity_push_lstring(ctx, source, (uint)source.Length);
            DuktapeDLL.duk_push_string(ctx, filename);
            if (DuktapeDLL.duk_pcompile(ctx, 0) != 0)
            {
                DuktapeAux.PrintError(ctx, -1, filename);
                DuktapeDLL.duk_pop(ctx);
                return null;
            }
            else
            {
                DuktapeDLL.duk_dump_function(ctx);
                uint buffer_size;
                var buffer_ptr = DuktapeDLL.duk_unity_require_buffer_data(ctx, -1, out buffer_size);
                var bytes = new byte[buffer_size];
                Marshal.Copy(buffer_ptr, bytes, 0, (int)buffer_size);
                DuktapeDLL.duk_pop(ctx);
                return bytes;
            }
        }

        public static void PrintError(IntPtr ctx, int idx)
        {
            PrintError(ctx, idx, null);
        }

        //NOTE: 危险, 仍然可能间接调用 duk_throw. 需改进.
        public static void PrintError(IntPtr ctx, int idx, string filename)
        {
            string err;
            if (DuktapeDLL.duk_is_error(ctx, idx))
            {
                DuktapeDLL.duk_get_prop_string(ctx, idx, "stack");
                err = DuktapeDLL.duk_safe_to_string(ctx, -1);
                DuktapeDLL.duk_pop(ctx);
            }
            else
            {
                err = DuktapeDLL.duk_safe_to_string(ctx, idx);
            }

            if (duk_source_position != default_duk_source_position)
            {
                var errlines = err.Split('\n');
                err = "";
                if (_stRegex == null)
                {
                    _stRegex = new Regex(@"^\s+at\s(.+)\s\((.+\.js):(\d+)\)(.*)$", RegexOptions.Compiled);
                }
                for (var i = 0; i < errlines.Length; i++)
                {
                    var line = errlines[i];
                    var matches = _stRegex.Matches(line);
                    if (matches.Count == 1)
                    {
                        var match = matches[0];
                        if (match.Groups.Count >= 4)
                        {
                            var funcName = match.Groups[1].Value;
                            var fileName = match.Groups[2].Value;
                            var lineNumber = 0;
                            int.TryParse(match.Groups[3].Value, out lineNumber);
                            var extra = match.Groups.Count >= 5 ? match.Groups[4].Value : "";
                            var sroucePosition = duk_source_position(ctx, funcName, fileName, lineNumber);
                            err += $"    at {sroucePosition}{extra}\n";
                            continue;
                        }
                    }
                    err += line + "\n";
                }
            }
            if (filename != null)
            {
                Debug.LogError($"[JSError][{filename}] {err}");
            }
            else
            {
                Debug.LogError($"[JSError] {err}");
            }
        }

        public static void duk_open(IntPtr ctx)
        {
            DuktapeDLL.duk_push_global_object(ctx);
            DuktapeDLL.duk_push_c_function(ctx, duk_print_log, DuktapeDLL.DUK_VARARGS);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "print");

            DuktapeDLL.duk_push_c_function(ctx, duk_dofile, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "dofile");

            DuktapeDLL.duk_push_c_function(ctx, duk_dostring, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "dostring");

            DuktapeDLL.duk_push_c_function(ctx, duk_addSearchPath, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "addSearchPath");

            DuktapeDLL.duk_push_c_function(ctx, duk_enableStacktrace, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "enableStacktrace");

            //add by wy
            DuktapeDLL.duk_push_c_function(ctx, duk_tostring, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "tostring");

            DuktapeDLL.duk_push_c_function(ctx, duk_tonumber, 1);
            DuktapeDLL.duk_put_prop_string(ctx, -2, "tonumber");

             DuktapeDLL.duk_push_c_function(ctx, duk_stacktrace, 0);
             DuktapeDLL.duk_put_prop_string(ctx, -2, "stacktrace");

            DuktapeDLL.duk_pop(ctx);
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_addSearchPath(IntPtr ctx)
        {
            var path = DuktapeAux.duk_require_string(ctx, 0);
            DuktapeVM.GetVM(ctx).AddSearchPath(path);
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_print_log(IntPtr ctx)
        {
            //先判定是否是string如果是的话，直接打印
            var jstype = DuktapeDLL.duk_get_type(ctx, -1);
            if (jstype == duk_type_t.DUK_TYPE_STRING)
            {
                var str = DuktapeDLL.duk_get_string(ctx, -1);
                Debug.Log(str);
            }
            else
            {
                Debug.Log(duk_print_string(ctx, 0, printStacktrace));
            }
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_print_warn(IntPtr ctx)
        {
            Debug.LogWarning(duk_print_string(ctx, 0, printStacktrace));
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_print_err(IntPtr ctx)
        {
            Debug.LogError(duk_print_string(ctx, 0, printStacktrace));
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_assert(IntPtr ctx)
        {
            if (!DuktapeDLL.duk_get_boolean(ctx, 0))
            {
                var msg = string.Empty;
                if (DuktapeDLL.duk_get_top(ctx) > 1)
                {
                    msg = duk_print_string(ctx, 1, false);
                }
                return DuktapeDLL.duk_generic_error(ctx, "assertion failed: " + msg);
            }
            return 0;
        }

        private static string duk_print_string(IntPtr ctx, int startIndex, bool withStacktrace)
        {
            var narg = DuktapeDLL.duk_get_top(ctx);
            var str = string.Empty;
            for (int i = startIndex; i < narg; i++)
            {
                object o;
                // 先尝试转为 C# 对象, 以调用其 ToString(), 不可用时再使用 js 字符串转换
                if (DuktapeBinding.duk_get_object(ctx, i, out o))
                {
                    str += (o == null ? "(null)" : o.ToString()) + " ";
                }
                else
                {
                    str += DuktapeDLL.duk_safe_to_string(ctx, i) + " ";
                }
            }
            if (withStacktrace)
            {
                str += $"\n{duk_get_stacktrace(ctx)}";
            }
            return str;
        }

        private static string duk_get_stacktrace(IntPtr ctx)
        {
            var stacktrace = "\n";
            stacktrace += "stacktrace:\n";
            for (int i = -2; ; i--)
            {
                DuktapeDLL.duk_inspect_callstack_entry(ctx, i);
                if (!DuktapeDLL.duk_is_undefined(ctx, -1))
                {
                    DuktapeDLL.duk_get_prop_string(ctx, -1, "lineNumber");
                    var lineNumber = DuktapeDLL.duk_to_int(ctx, -1);
                    DuktapeDLL.duk_get_prop_string(ctx, -2, "function");
                    DuktapeDLL.duk_get_prop_string(ctx, -1, "name");
                    var funcName = DuktapeDLL.duk_safe_to_string(ctx, -1);
                    DuktapeDLL.duk_get_prop_string(ctx, -2, "fileName");
                    var fileName = DuktapeDLL.duk_safe_to_string(ctx, -1);
                    DuktapeDLL.duk_pop_n(ctx, 4);
                    stacktrace += (duk_source_position ?? default_duk_source_position)(ctx, funcName, fileName, lineNumber);
                    stacktrace += "\n";
                }
                else
                {
                    DuktapeDLL.duk_pop(ctx);
                    break;
                }
            }
            return stacktrace;
        }

        public static string default_duk_source_position(IntPtr ctx, string funcName, string fileName, int lineNumber)
        {
            if (lineNumber != 0)
            {
                if (string.IsNullOrEmpty(funcName))
                {
                    funcName = "[anonymous]";
                }
                return $"{funcName} ({fileName}:{lineNumber})";
            }
            return $"{funcName} (<native code>)";
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_dofile(IntPtr ctx)
        {
            var filename = DuktapeAux.duk_require_string(ctx, 0);
            DuktapeVM.GetVM(ctx).EvalFile(filename);
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_dostring(IntPtr ctx)
        {
            // var source = DuktapeDLL.duk_get_string(ctx, 0);
            uint len;
            var buffer_ptr = DuktapeDLL.duk_unity_get_lstring(ctx, 0, out len);
            if (len > 0)
            {
                var bytes = new byte[len];
                Marshal.Copy(buffer_ptr, bytes, 0, (int)len);
                var filename = DuktapeDLL.duk_get_string(ctx, 1);
                DuktapeVM.GetVM(ctx).EvalSource(filename, bytes);
            }
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_enableStacktrace(IntPtr ctx)
        {
            printStacktrace = DuktapeDLL.duk_require_boolean(ctx, 0);
            return 0;
        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_tostring(IntPtr ctx)
        {
            if (DuktapeDLL.duk_check_type(ctx, 0,duk_type_t.DUK_TYPE_NONE))
            {
                throw new Exception("no param to convert");
            }
            duk_to_string(ctx, 0);
            DuktapeDLL.duk_set_top(ctx, 1);
            return 1;

        }

        [AOT.MonoPInvokeCallback(typeof(DuktapeDLL.duk_c_function))]
        public static int duk_tonumber(IntPtr ctx)
        {
            var argc = DuktapeDLL.duk_get_top(ctx);
            if (argc < 1 || argc > 2)
                throw new Exception("arg numbers are not valid");

            if (argc == 1 || DuktapeDLL.duk_is_null(ctx, 1))
            {
                if (DuktapeDLL.duk_check_type(ctx, 0, duk_type_t.DUK_TYPE_NONE))
                {
                    throw new Exception("no param to convert");
                }
                if (DuktapeDLL.duk_is_number(ctx, 0))
                {
                    DuktapeDLL.duk_set_top(ctx, 1);
                    return 1;
                }
                else
                {
                    DuktapeDLL.duk_to_number(ctx, 0);
                    if (!DuktapeDLL.duk_is_nan(ctx, 0))
                    {
                        return 1;
                    }
                }
            }
            else {
                var arg0 = DuktapeDLL.duk_get_string(ctx, 0);
                var arg1 = DuktapeDLL.duk_get_int(ctx, 1);
                if (!(arg1 >= 2 && arg1 <= 36))
                {
                    throw new Exception("base out of range");
                }
                long value = Convert.ToInt64(arg0, arg1);
                DuktapeBinding.duk_push_primitive(ctx,value);
                return 1;
            }
            DuktapeDLL.duk_push_null(ctx);
            return 1;

        }

        public static int duk_stacktrace(IntPtr ctx)
        {
            DuktapeDLL.duk_inspect_callstack_entry(ctx, -2);
            DuktapeDLL.duk_get_prop_string(ctx, -1, "lineNumber");
            var lineNumber = DuktapeDLL.duk_to_int(ctx, -1);
            DuktapeDLL.duk_pop(ctx);
            DuktapeDLL.duk_get_prop_string(ctx, -1, "function");
            DuktapeDLL.duk_get_prop_string(ctx, -1, "fileName");
            var fileName = DuktapeDLL.duk_safe_to_string(ctx, -1);
            DuktapeDLL.duk_pop_2(ctx);
            DuktapeDLL.duk_push_string(ctx, $"{fileName}:{lineNumber}");
            return 1;
        }
    }
}
