using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    /// <summary>
    /// 需要根据实际结果进行对应
    /// </summary>
    public enum SessionState
    {
        UnInitialize = 0,
        Initing = 1,
        InitingError =2,
        Tracking =3,
        LostTracking =4
    }
}
