using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public enum LostTrackingReason
    {

        NONE = 0,

        INITIALIZING = 1,

        EXCESSIVE_MOTION = 2,

        INSUFFICIENT_FEATURES = 3,

        RELOCALIZING = 4,
    }
}

