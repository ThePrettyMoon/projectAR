using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.Hand;

namespace EZXR.Glass.UI
{
    public abstract class SpacialSelectable : SpacialUIBase
    {
        public abstract void OnRayCastHit(HandInfo handInfo);
    }
}