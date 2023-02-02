using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public class NativeConsts
    {
#if EZXRCS
    public const string NativeLibrary = "ezipc";
#else
        public const string NativeLibrary = "ez-glassar";
#endif
    }
}