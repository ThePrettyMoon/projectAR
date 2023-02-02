using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Duktape;

public class DuktapeUtility 
{
    public enum DaketapeRunState { 
        uninited = 0,
        initing = 1,
        running = 2,
        unloading = 4
    }

    private static DaketapeRunState runState = DaketapeRunState.initing;

    /// <summary>
    /// 设置Duktape解析器状态
    /// </summary>
    /// <param name="state"></param>
    public static void SetDaktapeRunState(DaketapeRunState state) {
        runState = state;
    }
    /// <summary>
    /// 获取Duktape解析器状态
    /// </summary>
    /// <returns></returns>
    public static DaketapeRunState GetDaktapeRunState()
    {
        return runState;
    }


    /// <summary>
    /// 调用js方法
    /// </summary>
    /// <param name="ctx"></param>
    /// <param name="modulePtr"></param>
    /// <param name="funcPtr"></param>
    /// <param name="args"></param>
    public static void CallMethod(IntPtr ctx,IntPtr modulePtr,IntPtr funcPtr,params object[] args)
    {

        if (runState != DaketapeRunState.running) {
            InsightDebug.Log("Duktape", "CallMethod when not in running state");
            return;
        }

        DuktapeDLL.duk_push_heapptr(ctx, funcPtr);
        DuktapeDLL.duk_push_heapptr(ctx, modulePtr);

        var nargs = args.Length;
        for (var i = 0; i < nargs; i++)
        {
            DuktapeBinding.duk_push_var(ctx, args[i]);
        }

        var ret = DuktapeDLL.duk_pcall_method(ctx, nargs);
        if (ret != DuktapeDLL.DUK_EXEC_SUCCESS)
        {
            DuktapeAux.PrintError(ctx, -1, "CallMethod");
            DuktapeDLL.duk_pop(ctx);
            throw new Exception("DuktapeFunction error catch and rethrow");
        }
        DuktapeDLL.duk_pop(ctx);
    }
}
