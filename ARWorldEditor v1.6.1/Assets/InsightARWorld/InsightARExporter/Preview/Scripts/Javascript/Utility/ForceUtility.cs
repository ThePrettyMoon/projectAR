using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUtility 
{
    public static UnityEngine.ForceMode ToMode(Insight.ForceMode mode)
    {
        if (mode == Insight.ForceMode.Acceleration) return UnityEngine.ForceMode.Acceleration;
        else if (mode == Insight.ForceMode.Force) return UnityEngine.ForceMode.Force;
        else if (mode == Insight.ForceMode.Impulse) return UnityEngine.ForceMode.Impulse;
        else return ForceMode.VelocityChange;
    }
}
