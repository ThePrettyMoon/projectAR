using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duktape;

/// <summary>
/// 四元数扩展方法
/// </summary>

public static class QuaternionExtension
{
    /*  public static Quaternion set(this Quaternion quat, float x, float y, float z, float w)
      {
          quat.x = x;
          quat.y = y;
          quat.z = z;
          quat.w = w; 
          return quat;
      }*/

    public static Quaternion New()
    {
        return new Quaternion();
    }

    public static Quaternion setX(this Quaternion quat,float x)
    {
        quat.x = x;
        return quat;
    }

    public static Quaternion setY(this Quaternion quat, float y)
    {
        quat.y = y;
        return quat;
    }

    public static Quaternion setZ(this Quaternion quat, float z)
    {
        quat.z = z;
        return quat;
    }

    public static Quaternion setW(this Quaternion quat, float w)
    {
        quat.w = w;
        return quat;
    }

    public static float getComponent(this Quaternion quat,int index)
    {
        switch (index){
            case 0:
                return quat.x;
            case 1:
                return quat.y;
            case 2:
                return quat.z;
            case 3:
                return quat.w;
            default:
                Debug.LogFormat("index %d is out of [0,3]", index);
                return 0.0f;
        }
    }

    public static Quaternion setComponent(this Quaternion quat, int index,float num)
    {
        switch (index)
        {
            case 0:
                quat.x = num;
                break;
            case 1:
                quat.y = num;
                break;
            case 2:
                quat.z = num;
                break;
            case 3:
                quat.w = num;
                break;
            default:
                Debug.LogFormat("index %d is out of [0,3]", index);
                break;
        }
        return quat;
    }


    public static Quaternion identity(this Quaternion quat)
    {
        return new Quaternion(0, 0, 0, 1);
    }

    public static Quaternion clone(this Quaternion quat)
    {
        return new Quaternion(quat.x, quat.y, quat.z, quat.w);
    }

    public static Quaternion pow(this Quaternion quat,float n)
    {
        //ln
        float x = quat.x;
        float y = quat.y;
        float z = quat.z;
        float w = quat.w;
        float r0 = (float)Mathf.Sqrt(x * x + y * y + z * z);
        float t0 = r0 > 0.00001f ? (float)Mathf.Atan2(r0, w) / r0 : 0.0f;
        quat.w = 0.5f * (float)Mathf.Log(w * w + x * x + y * y + z * z);
        quat.x *= t0;
        quat.y *= t0;
        quat.z *= t0;
        //scale
        quat.w *= n;
        quat.x *= n;
        quat.y *= n;
        quat.z *= n;
        //exp
        float r1 = (float)Mathf.Sqrt(x * x + y * y + z * z);
        float et = (float)Mathf.Exp(w);
        float s = r1 >= 0.00001f ? et * (float)Mathf.Sin(r1) / r1 : 0f;

        quat.w = et * (float)Mathf.Cos(r1);
        quat.x *= s;
        quat.y *= s;
        quat.z *= s;
        return quat;
    }


    public static Quaternion copy(this Quaternion quat, Quaternion quaternion)
    {
        quat.x = quaternion.x;
        quat.y = quaternion.y;
        quat.z = quaternion.z;
        quat.w = quaternion.w;
        return quat;
    }

    public static Quaternion setFromAxisAngle(this Quaternion quat, Vector3 axis, float angle)
    {
        float halfAngle = angle / 2.0f;
        float s = Mathf.Sin(halfAngle);

        quat.x = axis.x * s;
        quat.y = axis.y * s;
        quat.z = axis.z * s;
        quat.w = Mathf.Cos(halfAngle);
        return quat;
    }

    public static Quaternion setFromRotationMatrix(this Quaternion quat, Insight.Matrix4x4 m)
    {
        var te = m.elements;

        float m11 = te[0]; float m12 = te[4]; float m13 = te[8];
        float m21 = te[1]; float m22 = te[5]; float m23 = te[9];
        float m31 = te[2]; float m32 = te[6]; float m33 = te[10];

        float trace = m11 + m22 + m33;

        float s;

        if (trace > 0)
        {
            s = 0.5f / Mathf.Sqrt(trace + 1.0f);

            quat.w = 0.25f / s;
            quat.x = (m32 - m23) * s;
            quat.y = (m13 - m31) * s;
            quat.z = (m21 - m12) * s;
        }
        else if (m11 > m22 && m11 > m33)
        {

            s = 2.0f * Mathf.Sqrt(1.0f + m11 - m22 - m33);

            quat.w = (m32 - m23) / s;
            quat.x = 0.25f * s;
            quat.y = (m12 + m21) / s;
            quat.z = (m13 + m31) / s;

        }
        else if (m22 > m33)
        {

            s = 2.0f * Mathf.Sqrt(1.0f + m22 - m11 - m33);

            quat.w = (m13 - m31) / s;
            quat.x = (m12 + m21) / s;
            quat.y = 0.25f * s;
            quat.z = (m23 + m32) / s;

        }
        else
        {

            s = 2.0f * Mathf.Sqrt(1.0f + m33 - m11 - m22);

            quat.w = (m21 - m12) / s;
            quat.x = (m13 + m31) / s;
            quat.y = (m23 + m32) / s;
            quat.z = 0.25f * s;
        }
        return quat;
    }

    public static Quaternion setFromUnitVectors(this Quaternion quat, Insight.Vector4 vFrom, Insight.Vector4 vTo)
    {

        var r = vFrom.dot(vTo) + 1;

        if (r < MathConverter.EPS)
        {

            r = 0;

            if (Mathf.Abs(vFrom.x) > Mathf.Abs(vFrom.z))
            {

                quat.x = -vFrom.y;
                quat.y = vFrom.x;
                quat.z = 0;
                quat.w = r;

            }
            else
            {

                quat.x = 0;
                quat.y = -vFrom.z;
                quat.z = vFrom.y;
                quat.w = r;

            }

        }
        else
        {

            // crossVectors( vFrom, vTo ); // inlined to avoid cyclic dependency on Vector3

            quat.x = vFrom.y * vTo.z - vFrom.z * vTo.y;
            quat.y = vFrom.z * vTo.x - vFrom.x * vTo.z;
            quat.z = vFrom.x * vTo.y - vFrom.y * vTo.x;
            quat.w = r;

        }

        return quat.normalize();
    }

    public static float angleTo(this Quaternion quat, Quaternion q)
    {
        return 2.0f * Mathf.Acos(Mathf.Abs(Mathf.Clamp(quat.dot(q), -1, 1)));
    }

    public static Quaternion rotateTowards(this Quaternion quat, Quaternion q, float step)
    {
        var angle = quat.angleTo(q);
        if (angle == 0) return quat;

        var t = Mathf.Min(1, step / angle);
        return quat.slerp(q, t);
    }

    public static Quaternion inverse(this Quaternion quat)
    {
        return quat.conjugate();
    }

    public static Quaternion conjugate(this Quaternion quat)
    {
        quat.x *= -1;
        quat.y *= -1;
        quat.z *= -1;
        quat.w *= -1;
        return quat;
    }

    public static float dot(this Quaternion quat, Quaternion q)
    {
        return quat.x * q.x + quat.y * q.y + quat.z * q.z + quat.w * q.w;
    }

    public static float lengthSq(this Quaternion quat)
    {
        return quat.x * quat.x + quat.y * quat.y + quat.z * quat.z + quat.w * quat.w;
    }

    public static float length(this Quaternion quat)
    {
        return Mathf.Sqrt(quat.x * quat.x + quat.y * quat.y + quat.z * quat.z + quat.w * quat.w);
    }

    public static Quaternion normalize(this Quaternion quat)
    {
        var l = quat.length();

        if (l == 0)
        {
            quat.x = 0;
            quat.y = 0;
            quat.z = 0;
            quat.w = 0;
        }
        else
        {
            l = 1.0f / l;

            quat.x = quat.x * l;
            quat.y = quat.y * l;
            quat.z = quat.z * l;
            quat.w = quat.w * l;
        }
        return quat;
    }


    public static Quaternion multiply(this Quaternion quat, Quaternion q)
    {
        return quat.multiplyQuaternions(quat, q);
    }

    public static Quaternion premultiply(this Quaternion quat, Quaternion q)
    {
        return quat.multiplyQuaternions(q, quat);
    }

    public static Quaternion multiplyQuaternions(this Quaternion quat, Quaternion a, Quaternion b)
    {
        // from http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/code/index.htm

        var qax = a.x; var qay = a.y; var qaz = a.z; var qaw = a.w;
        var qbx = b.x; var qby = b.y; var qbz = b.z; var qbw = b.w;

        quat.x = qax * qbw + qaw * qbx + qay * qbz - qaz * qby;
        quat.y = qay * qbw + qaw * qby + qaz * qbx - qax * qbz;
        quat.z = qaz * qbw + qaw * qbz + qax * qby - qay * qbx;
        quat.w = qaw * qbw - qax * qbx - qay * qby - qaz * qbz;

        return quat;
    }

    public static Quaternion Slerp(Quaternion qa, Quaternion qb, Quaternion qm, float t)
    {
        return qm.copy(qa).slerp(qb, t);
    }

    public static Quaternion slerp(this Quaternion quat, Quaternion qb, float t)
    {
        if (t == 0) return quat;
        if (t == 1) return quat.copy(qb);

        var x = quat.x; var y = quat.y; var z = quat.z; var w = quat.w;

        // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/slerp/

        var cosHalfTheta = w * qb.w + x * qb.x + y * qb.y + z * qb.z;

        if (cosHalfTheta < 0)
        {

            quat.w = -qb.w;
            quat.x = -qb.x;
            quat.y = -qb.y;
            quat.z = -qb.z;

            cosHalfTheta = -cosHalfTheta;

        }
        else
        {

            quat.copy(qb);

        }

        if (cosHalfTheta >= 1.0f)
        {

            quat.w = w;
            quat.x = x;
            quat.y = y;
            quat.z = z;

            return quat;

        }

        var sqrSinHalfTheta = 1.0f - cosHalfTheta * cosHalfTheta;

        if (sqrSinHalfTheta <= Mathf.Epsilon)
        {

            var s = 1 - t;
            quat.w = s * w + t * quat.w;
            quat.x = s * x + t * quat.x;
            quat.y = s * y + t * quat.y;
            quat.z = s * z + t * quat.z;

            quat.normalize();

            return quat;

        }

        var sinHalfTheta = Mathf.Sqrt(sqrSinHalfTheta);
        var halfTheta = Mathf.Atan2(sinHalfTheta, cosHalfTheta);
        var ratioA = Mathf.Sin((1 - t) * halfTheta) / sinHalfTheta;
        var ratioB = Mathf.Sin(t * halfTheta) / sinHalfTheta;

        quat.w = (w * ratioA + quat.w * ratioB);
        quat.x = (x * ratioA + quat.x * ratioB);
        quat.y = (y * ratioA + quat.y * ratioB);
        quat.z = (z * ratioA + quat.z * ratioB);

        return quat;

    }

    /*public bool equals(Quaternion quaternion)
    {
        return (quat.x == quaternion.x) && (quat.y == quaternion.y) && (quat.z == quaternion.z) && (quat.w == quaternion.w);
    }*/

    public static Quaternion fromArray(this Quaternion quat, List<float> array)
    {
        quat.x = array[0];
        quat.y = array[1];
        quat.z = array[2];
        quat.w = array[3];
        return quat;
    }

    public static Quaternion fromArray(this Quaternion quat, List<float> array, int offset)
    {
        quat.x = array[offset];
        quat.y = array[offset + 1];
        quat.z = array[offset + 2];
        quat.w = array[offset + 3];
        return quat;
    }

    public static Quaternion fromArray(this Quaternion quat, float[] array)
    {
        quat.x = array[0];
        quat.y = array[1];
        quat.z = array[2];
        quat.w = array[3];
        return quat;
    }

    public static Quaternion fromArray(this Quaternion quat, float[] array, int offset)
    {
        quat.x = array[offset];
        quat.y = array[offset + 1];
        quat.z = array[offset + 2];
        quat.w = array[offset + 3];
        return quat;
    }

    public static float[] toArray(this Quaternion quat, float[] array)
    {
        if (array == null) array = new float[] { };
        array[0] = quat.x;
        array[1] = quat.y;
        array[2] = quat.z;
        array[3] = quat.w;
        return array;
    }

    public static float[] toArray(this Quaternion quat, float[] array, int offset)
    {
        if (array == null) array = new float[] { };
        array[offset] = quat.x;
        array[offset + 1] = quat.y;
        array[offset + 2] = quat.z;
        array[offset + 3] = quat.w;
        return array;
    }

    public static List<float> toArray(this Quaternion quat, List<float> array)
    {
        if (array == null) array = new List<float>();
        array[0] = quat.x;
        array[1] = quat.y;
        array[2] = quat.z;
        array[3] = quat.w;
        return array;
    }

    public static List<float> toArray(this Quaternion quat, List<float> array, int offset)
    {
        if (array == null) array = new List<float>();
        array[offset] = quat.x;
        array[offset + 1] = quat.y;
        array[offset + 2] = quat.z;
        array[offset + 3] = quat.w;
        return array;
    }


    /*  public string toString()
      {
          return "Quaternion( x == " + quat.x + " y == " + quat.y + " z == " + quat.z + " w == " + quat.w + ")";
      }*/

    public static Quaternion SlerpFlat(float[] dst, int dstOffset, float[] src0, int srcOffset0, float[] src1, int srcOffset1, float t)
    {
        var x0 = src0[srcOffset0 + 0];
        var y0 = src0[srcOffset0 + 1];
        var z0 = src0[srcOffset0 + 2];
        var w0 = src0[srcOffset0 + 3];

        var x1 = src1[srcOffset1 + 0];
        var y1 = src1[srcOffset1 + 1];
        var z1 = src1[srcOffset1 + 2];
        var w1 = src1[srcOffset1 + 3];

        if (w0 != w1 || x0 != x1 || y0 != y1 || z0 != z1)
        {

            var s = 1 - t;

            var cos = x0 * x1 + y0 * y1 + z0 * z1 + w0 * w1;

            var dir = (cos >= 0 ? 1 : -1);
            var sqrSin = 1 - cos * cos;

            // Skip the Slerp for tiny steps to avoid numeric problems:
            if (sqrSin > Mathf.Epsilon)
            {

                var sin = Mathf.Sqrt(sqrSin);
                var len = Mathf.Atan2(sin, cos * dir);

                s = Mathf.Sin(s * len) / sin;
                t = Mathf.Sin(t * len) / sin;

            }

            var tDir = t * dir;

            x0 = x0 * s + x1 * tDir;
            y0 = y0 * s + y1 * tDir;
            z0 = z0 * s + z1 * tDir;
            w0 = w0 * s + w1 * tDir;

            // Normalize in case we just did a lerp:
            if (s == 1 - t)
            {

                var f = 1 / Mathf.Sqrt(x0 * x0 + y0 * y0 + z0 * z0 + w0 * w0);

                x0 *= f;
                y0 *= f;
                z0 *= f;
                w0 *= f;

            }

        }

        dst[dstOffset] = x0;
        dst[dstOffset + 1] = y0;
        dst[dstOffset + 2] = z0;
        dst[dstOffset + 3] = w0;
        return new Quaternion(x0, y0, z0, w0);
    }
}
