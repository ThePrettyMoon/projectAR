using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraPathElement: TreeElement
{
	[SerializeField] public Vector3 position;
	[SerializeField] public Vector3 eulerAngle;
	[SerializeField] public bool isUnderControl;
	[SerializeField] public Vector3 controlPoint1;
	[SerializeField] public Vector3 controlPoint2;
	[SerializeField] public CameraPathControlPoint pathPoint;
	[SerializeField] public CameraPathOrientation rotatePoint;
	public bool enabled;
	public CameraPathElement(string name, int depth, int id) : base(name, depth, id)
	{
		position = Vector3.one;
		eulerAngle = Vector3.zero;
		isUnderControl = false;
		controlPoint1 = Vector3.zero;
		controlPoint2 = Vector3.zero;
		enabled = false;
	}
	public CameraPathElement(string name, int depth, int id,CameraPathControlPoint _pathPoint, CameraPathOrientation _rotatePoint) : base(name, depth, id)
    {
		position = _pathPoint.worldPosition;
		eulerAngle = _pathPoint.trackDirection;
		isUnderControl = false;
		controlPoint1 = Vector3.zero;
		controlPoint2 = Vector3.zero;
		enabled = false;
		pathPoint = _pathPoint;
		rotatePoint = _rotatePoint;
	}
}