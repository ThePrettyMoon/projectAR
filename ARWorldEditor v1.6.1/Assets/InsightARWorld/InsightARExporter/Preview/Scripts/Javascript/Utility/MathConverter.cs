using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insight;

public static class MathConverter
{
    public const double EPS = 1e-5;

    public static Insight.Vector2 FromVector2(UnityEngine.Vector2 vect)
    {
        return new Insight.Vector2(vect.x, vect.y);
    }

    public static UnityEngine.Vector2 ToVector2(Insight.Vector2 vect)
    {
        return new UnityEngine.Vector2(vect.x, vect.y);
    }

    public static Insight.Vector3 FromVector3(UnityEngine.Vector3 vect)
    {
        return new Insight.Vector3(vect.x, vect.y, vect.z);
    }

    public static UnityEngine.Vector3 ToVector3(Insight.Vector3 vect)
    {
        return new UnityEngine.Vector3(vect.x, vect.y, vect.z);
    }

    public static Insight.Vector4 FromVector4(UnityEngine.Vector4 vect)
    {
        return new Insight.Vector4(vect.x, vect.y,vect.z,vect.w);
    }

    public static UnityEngine.Vector4 ToVector4(Insight.Vector4 vect)
    {
        return new UnityEngine.Vector4(vect.x, vect.y, vect.z,vect.w);
    }

    public static UnityEngine.Quaternion ToQuaternion(Insight.Quaternion vect)
    {
        return new UnityEngine.Quaternion(vect.x, vect.y, vect.z, vect.w);
    }

    public static Insight.Quaternion FromQuaternion(UnityEngine.Quaternion vect)
    {
        return new Insight.Quaternion(vect.x, vect.y, vect.z, vect.w);
    }

    public static Insight.Matrix4x4 FromMatrix4x4(UnityEngine.Matrix4x4 mat)
    {
        Insight.Matrix4x4 matrix4 = new Insight.Matrix4x4();
        matrix4.elements = new float[16];
        var te = matrix4.elements;

        te[0] = mat.m00; te[4] = mat.m01; te[8] = mat.m02; te[12] = mat.m03;
        te[1] = mat.m10; te[5] = mat.m11; te[9] = mat.m12; te[13] = mat.m13;
        te[2] = mat.m20; te[6] = mat.m21; te[10] = mat.m22; te[14] = mat.m23;
        te[3] = mat.m30; te[7] = mat.m31; te[11] = mat.m32; te[15] = mat.m33;
        return matrix4;
    }

    public static UnityEngine.Matrix4x4 ToMatrix4x4(Insight.Matrix4x4 mat)
    {
        UnityEngine.Matrix4x4 matrix4 = new UnityEngine.Matrix4x4();
        if (mat.elements != null)
        {
            var te = mat.elements;
            matrix4.m00 = te[0]; matrix4.m01 = te[4]; matrix4.m02 = te[8]; matrix4.m03 = te[12];
            matrix4.m10 = te[1]; matrix4.m11 = te[5]; matrix4.m12 = te[9]; matrix4.m13 = te[13];
            matrix4.m20 = te[2]; matrix4.m21 = te[6]; matrix4.m22 = te[10]; matrix4.m23 = te[14];
            matrix4.m30 = te[3]; matrix4.m31 = te[7]; matrix4.m32 = te[11]; matrix4.m33 = te[15];
        }
        return matrix4;
    }
}
