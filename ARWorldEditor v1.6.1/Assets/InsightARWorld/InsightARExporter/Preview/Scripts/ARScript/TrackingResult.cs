using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct TrackingResult
{
    public  Vector3 point;
    public  bool tracked;

    public TrackingResult(Vector3 _point, bool _tracked)
    {
        point = _point;
        tracked = _tracked;
    }
}

