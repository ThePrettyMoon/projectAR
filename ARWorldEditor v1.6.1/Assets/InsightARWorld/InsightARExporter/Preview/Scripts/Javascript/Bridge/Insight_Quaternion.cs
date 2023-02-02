using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Insight;

namespace Insight
{
    public class Quaternion
    {
        public float _x;
        public float _y;
        public float _z;
        public float _w;

        public float x
        {
            get
            {
                return this._x;
            }
            set
            {
                this._x = value;
            }
        }

        public float y
        {
            get
            {
                return this._y;
            }
            set
            {
                this._y = value;
            }
        }

        public float z
        {
            get
            {
                return this._z;
            }
            set
            {
                this._z = value;
            }
        }

        public float w
        {
            get
            {
                return this._w;
            }
            set
            {
                this._w = value;
            }
        }

        public Quaternion()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.w = 1;
        }

        public static Quaternion New()
        {
            return new Quaternion();
        }

        public Quaternion identity()
        {
            return new Quaternion(0, 0, 0, 1);
        }

        public Quaternion(float x, float y, float z, float w)
        {
            this._x = x;
            this._y = y;
            this._z = z;
            this._w = w;
        }

        public Quaternion set(float x, float y, float z, float w)
        {
            this._x = x;
            this._y = y;
            this._z = z;
            this._w = w;
            return this;
        }

        public Quaternion setX(float x)
        {
            this._x = x;
            return this;
        }

        public Quaternion setY(float y)
        {
            this._y = y;
            return this;
        }

        public Quaternion setZ(float z)
        {
            this._z = z;
            return this;
        }

        public Quaternion setW(float w)
        {
            this._w = w;
            return this;
        }

        public Quaternion setComponent(int index, float value)
        {
            switch (index)
            {
                case 0: this.x = value; break;
                case 1: this.y = value; break;
                case 2: this.z = value; break;
                case 3: this.w = value; break;
                default: throw new Exception("index is out of range :" + index);
            }
            return this;
        }

        public float getComponent(int index)
        {
            switch (index)
            {
                case 0: return this.x;
                case 1: return this.y;
                case 2: return this.z;
                case 3: return this.w;
                default: throw new Exception("index is out of range :" + index);
            }
        }

        public Quaternion clone()
        {
            return new Quaternion(this._x, this._y, this.z, this._w);
        }


        public Quaternion copy(Quaternion quaternion)
        {
            this._x = quaternion.x;
            this._y = quaternion.y;
            this._z = quaternion.z;
            this._w = quaternion.w;
            return this;
        }

        public Quaternion setFromAxisAngle(Vector3 axis, float angle)
        {
            float halfAngle = angle / 2.0f;
            float s = Mathf.Sin(halfAngle);

            this._x = axis.x * s;
            this._y = axis.y * s;
            this._z = axis.z * s;
            this._w = Mathf.Cos(halfAngle);
            return this;
        }

        public Quaternion setFromRotationMatrix(Insight.Matrix4x4 m)
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

                this._w = 0.25f / s;
                this._x = (m32 - m23) * s;
                this._y = (m13 - m31) * s;
                this._z = (m21 - m12) * s;
            }
            else if (m11 > m22 && m11 > m33)
            {

                s = 2.0f * Mathf.Sqrt(1.0f + m11 - m22 - m33);

                this._w = (m32 - m23) / s;
                this._x = 0.25f * s;
                this._y = (m12 + m21) / s;
                this._z = (m13 + m31) / s;

            }
            else if (m22 > m33)
            {

                s = 2.0f * Mathf.Sqrt(1.0f + m22 - m11 - m33);

                this._w = (m13 - m31) / s;
                this._x = (m12 + m21) / s;
                this._y = 0.25f * s;
                this._z = (m23 + m32) / s;

            }
            else
            {

                s = 2.0f * Mathf.Sqrt(1.0f + m33 - m11 - m22);

                this._w = (m21 - m12) / s;
                this._x = (m13 + m31) / s;
                this._y = (m23 + m32) / s;
                this._z = 0.25f * s;
            }
            return this;
        }

        public Quaternion setFromToRotation(Vector3 from,Vector3 to)
        {
            UnityEngine.Quaternion quaternion = UnityEngine.Quaternion.identity;
            quaternion.SetFromToRotation(MathConverter.ToVector3(from), MathConverter.ToVector3(to));
            return MathConverter.FromQuaternion(quaternion);
        }

        public Quaternion setFromUnitVectors(Vector4 vFrom, Vector4 vTo)
        {

            var r = vFrom.dot(vTo) + 1;

            if (r < MathConverter.EPS)
            {

                r = 0;

                if (Mathf.Abs(vFrom.x) > Mathf.Abs(vFrom.z))
                {

                    this._x = -vFrom.y;
                    this._y = vFrom.x;
                    this._z = 0;
                    this._w = r;

                }
                else
                {

                    this._x = 0;
                    this._y = -vFrom.z;
                    this._z = vFrom.y;
                    this._w = r;

                }

            }
            else
            {

                // crossVectors( vFrom, vTo ); // inlined to avoid cyclic dependency on Vector3

                this._x = vFrom.y * vTo.z - vFrom.z * vTo.y;
                this._y = vFrom.z * vTo.x - vFrom.x * vTo.z;
                this._z = vFrom.x * vTo.y - vFrom.y * vTo.x;
                this._w = r;

            }

            return this.normalize();
        }

        public float angleTo(Quaternion q)
        {
            return 2.0f * Mathf.Acos(Mathf.Abs(Mathf.Clamp(this.dot(q), -1, 1)));
        }

        public Quaternion rotateTowards(Quaternion q, float step)
        {
            var angle = this.angleTo(q);
            if (angle == 0) return this;

            var t = Mathf.Min(1, step / angle);
            return this.slerp(q, t);
        }

        public Quaternion inverse()
        {
            return this.conjugate();
        }

        public Quaternion conjugate()
        {
            this._x *= -1;
            this._y *= -1;
            this._z *= -1;
            this._w *= -1;
            return this;
        }

        public float dot(Quaternion q)
        {
            return this._x * q._x + this._y * q._y + this._z * q._z + this._w * q._w;
        }

        public float lengthSq()
        {
            return this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w;
        }

        public float length()
        {
            return Mathf.Sqrt(this._x * this._x + this._y * this._y + this._z * this._z + this._w * this._w);
        }

        public Quaternion normalize()
        {
            var l = this.length();

            if (l == 0)
            {
                this._x = 0;
                this._y = 0;
                this._z = 0;
                this._w = 0;
            }
            else
            {
                l = 1.0f / l;

                this._x = this._x * l;
                this._y = this._y * l;
                this._z = this._z * l;
                this._w = this._w * l;
            }
            return this;
        }


        public Quaternion multiply(Quaternion q)
        {
            return this.multiplyQuaternions(this, q);
        }

        public Quaternion premultiply(Quaternion q)
        {
            return this.multiplyQuaternions(q, this);
        }

        public Quaternion multiplyQuaternions(Quaternion a, Quaternion b)
        {
            // from http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/code/index.htm

            var qax = a._x; var qay = a._y; var qaz = a._z; var qaw = a._w;
            var qbx = b._x; var qby = b._y; var qbz = b._z; var qbw = b._w;

            this._x = qax * qbw + qaw * qbx + qay * qbz - qaz * qby;
            this._y = qay * qbw + qaw * qby + qaz * qbx - qax * qbz;
            this._z = qaz * qbw + qaw * qbz + qax * qby - qay * qbx;
            this._w = qaw * qbw - qax * qbx - qay * qby - qaz * qbz;

            return this;
        }

        public Quaternion slerp(Quaternion qb, float t)
        {
            if (t == 0) return this;
            if (t == 1) return this.copy(qb);

            var x = this._x; var y = this._y; var z = this._z; var w = this._w;

            // http://www.euclideanspace.com/maths/algebra/realNormedAlgebra/quaternions/slerp/

            var cosHalfTheta = w * qb._w + x * qb._x + y * qb._y + z * qb._z;

            if (cosHalfTheta < 0)
            {

                this._w = -qb._w;
                this._x = -qb._x;
                this._y = -qb._y;
                this._z = -qb._z;

                cosHalfTheta = -cosHalfTheta;

            }
            else
            {

                this.copy(qb);

            }

            if (cosHalfTheta >= 1.0f)
            {

                this._w = w;
                this._x = x;
                this._y = y;
                this._z = z;

                return this;

            }

            var sqrSinHalfTheta = 1.0f - cosHalfTheta * cosHalfTheta;

            if (sqrSinHalfTheta <= Mathf.Epsilon)
            {

                var s = 1 - t;
                this._w = s * w + t * this._w;
                this._x = s * x + t * this._x;
                this._y = s * y + t * this._y;
                this._z = s * z + t * this._z;

                this.normalize();

                return this;

            }

            var sinHalfTheta = Mathf.Sqrt(sqrSinHalfTheta);
            var halfTheta = Mathf.Atan2(sinHalfTheta, cosHalfTheta);
            var ratioA = Mathf.Sin((1 - t) * halfTheta) / sinHalfTheta;
            var ratioB = Mathf.Sin(t * halfTheta) / sinHalfTheta;

            this._w = (w * ratioA + this._w * ratioB);
            this._x = (x * ratioA + this._x * ratioB);
            this._y = (y * ratioA + this._y * ratioB);
            this._z = (z * ratioA + this._z * ratioB);

            return this;

        }

        public bool equals(Quaternion quaternion)
        {
            return (this._x == quaternion._x) && (this._y == quaternion._y) && (this._z == quaternion._z) && (this._w == quaternion.w);
        }

        public Quaternion pow(float exponent)
        {
            UnityEngine.Quaternion q = MathConverter.ToQuaternion(this);
            return MathConverter.FromQuaternion(q.pow(exponent));
        }

        public Quaternion fromArray(float[] array)
        {
            this._x = array[0];
            this._y = array[1];
            this._z = array[2];
            this._w = array[3];
            return this;
        }

        public Quaternion fromArray(float[] array, int offset)
        {
            this._x = array[offset];
            this._y = array[offset + 1];
            this._z = array[offset + 2];
            this._w = array[offset + 3];
            return this;
        }

        public float[] toArray()
        {
            float[] array = new float[4];
            array[0] = this._x;
            array[1] = this._y;
            array[2] = this._z;
            array[3] = this._w;
            return array;
        }

        public float[] toArray(float[] array)
        {
            if (array == null) array = new float[4];
            array[0] = this._x;
            array[1] = this._y;
            array[2] = this._z;
            array[3] = this._w;
            return array;
        }

        public float[] toArray(float[] array, int offset)
        {
            if (array == null) array = new float[4];
            array[offset] = this._x;
            array[offset + 1] = this._y;
            array[offset + 2] = this._z;
            array[offset + 3] = this._w;
            return array;
        }


        public string toString()
        {
            return "Quaternion( x == " + this.x + " y == " + this.y + " z == " + this.z + " w == " + this.w + ")";
        }

        public static Quaternion Slerp(Quaternion qa, Quaternion qb, float t)
        {
            Quaternion quaternion = new Quaternion();
            return Slerp(qa, qb, quaternion, t);
        }

        public static Quaternion Slerp(Quaternion qa, Quaternion qb, Quaternion qm, float t)
        {
            return qm.copy(qa).slerp(qb, t);
        }

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

        public static Vector3 multiplyVector3(Quaternion q, Vector3 point)
        {
            var vec = new Vector3();
            var num = q.x * 2.0f;
            var num2 = q.y * 2.0f;

            var num3 = q.z * 2.0f;

            var num4 = q.x * num;

            var num5 = q.y * num2;

            var num6 = q.z * num3;

            var num7 = q.x * num2;

            var num8 = q.x * num3;

            var num9 = q.y * num3;

            var num10 = q.w * num;

            var num11 = q.w * num2;

            var num12 = q.w * num3;


            vec.x = (((1 - (num5 + num6)) * point.x) + ((num7 - num12) * point.y)) + ((num8 + num11) * point.z);

            vec.y = (((num7 + num12) * point.x) + ((1 - (num4 + num6)) * point.y)) + ((num9 - num10) * point.z);

            vec.z = (((num8 - num11) * point.x) + ((num9 + num10) * point.y)) + ((1 - (num4 + num5)) * point.z);

            return vec;
        }

        public Vector3 eulerAngles
        {
            get
            {
                return MathConverter.FromVector3((MathConverter.ToQuaternion(this).eulerAngles));
            }
        }

        public void setFromRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            MathConverter.ToQuaternion(this).SetFromToRotation(MathConverter.ToVector3(fromDirection), MathConverter.ToVector3(toDirection));
        }

        public Quaternion setLookRotation(Vector3 view)
        {
            return setLookRotation(view, new Vector3(0, 1, 0));
        }

        public Quaternion setLookRotation(Vector3 view, Vector3 up)
        {
            UnityEngine.Quaternion quat = MathConverter.ToQuaternion(this);
            quat.SetLookRotation(MathConverter.ToVector3(view), MathConverter.ToVector3(up));
            return MathConverter.FromQuaternion(quat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public void toAngleAxis(out float angle,out Insight.Vector3 axis)
        {
            UnityEngine.Vector3 uaxis;
            MathConverter.ToQuaternion(this).ToAngleAxis(out angle, out uaxis);
            axis = MathConverter.FromVector3(uaxis);
        }

        public static float Angle(Quaternion a, Quaternion b)
        {
            return a.angleTo(b);
        }

        public static Quaternion AngleAxis(float angle, float x,float y,float z)
        {
            Quaternion quaternion = new Quaternion();
            Vector3 axis = new Vector3(x, y, z);
            return quaternion.setFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="axis"></param>
        public static Quaternion AngleAxis(float angle, Vector3 axis)
        {
            Quaternion quaternion = new Quaternion();
            return quaternion.setFromAxisAngle(axis, angle);
        }

        public static float Dot(Quaternion a, Quaternion b)
        {
            return a.dot(b);
        }

        public static Quaternion Euler(float x,float y,float z)
        {
            return MathConverter.FromQuaternion(UnityEngine.Quaternion.Euler(new UnityEngine.Vector3(x,x,z)));
        }

        public static Quaternion Euler(Vector3 v)
        {
            return MathConverter.FromQuaternion(UnityEngine.Quaternion.Euler(MathConverter.ToVector3(v)));
        }

        public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
        {
            UnityEngine.Quaternion q = new UnityEngine.Quaternion();
            q.SetFromToRotation(MathConverter.ToVector3(fromDirection), MathConverter.ToVector3(toDirection));
            return MathConverter.FromQuaternion(q);
        }

        public static Quaternion Lerp(Quaternion a, Quaternion b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            Quaternion q = new Quaternion();
            if (Quaternion.Dot(a, b) < 0)
            {
                q.x = a.x + t * (-b.x - a.x);
                q.y = a.y + t * (-b.y - a.y);
                q.z = a.z + t * (-b.z - a.z);
                q.w = a.w + t * (-b.w - a.w);
            }
            else
            {
                q.x = a.x + (b.x - a.x) * t;
                q.y = a.y + (b.y - a.y) * t;
                q.z = a.z + (b.z - a.z) * t;
                q.w = a.w + (b.w - a.w) * t;
            }
            q.normalize();
            return q;
        }

        public static Quaternion LerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            Quaternion q = new Quaternion();
            if (Quaternion.Dot(a, b) < 0)
            {
                q.x = a.x + t * (-b.x - a.x);
                q.y = a.y + t * (-b.y - a.y);
                q.z = a.z + t * (-b.z - a.z);
                q.w = a.w + t * (-b.w - a.w);
            }
            else
            {
                q.x = a.x + (b.x - a.x) * t;
                q.y = a.y + (b.y - a.y) * t;
                q.z = a.z + (b.z - a.z) * t;
                q.w = a.w + (b.w - a.w) * t;
            }
            q.normalize();
            return q;
        }

        public static Quaternion SlerpUnclamped(Quaternion a, Quaternion b, float t)
        {
            var dot = a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
            if (dot < 0)
            {
                dot = -dot;
                b.x = -b.x;
                b.y = -b.y;
                b.z = -b.z;
                b.w = -b.w;
            }

            if (dot < 0.9999f)
            {
                var angle = Mathf.Acos(dot);
                var invSinAngle = 1.0f / Mathf.Sin(angle);
                var t1 = Mathf.Sin((1 - t) * angle) * invSinAngle;
                var t2 = Mathf.Sin(t * angle) * invSinAngle;
                a.x = a.x * t1 + b.x * t2;
                a.y = a.y * t1 + b.y * t2;
                a.z = a.z * t1 + b.z * t2;
                a.w = a.w * t1 + b.w * t2;
                return a;
            }
            else
            {
                a.x = a.x + t * (b.x - a.x);
                a.y = a.y + t * (b.y - a.y);
                a.z = a.z + t * (b.z - a.z);
                a.w = a.w + t * (b.w - a.w);
                a.normalize();
                return a;
            }
        }

        public static Quaternion LookRotation(Vector3 forward)
        {
            return LookRotation(forward, Vector3.up);
        }

        public static Quaternion LookRotation(Vector3 forward, Vector3 up)
        {
            UnityEngine.Quaternion q = new UnityEngine.Quaternion();
            q.SetLookRotation(MathConverter.ToVector3(forward),
                MathConverter.ToVector3(up));
            return MathConverter.FromQuaternion(q);
        }


        public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
        {
            return MathConverter.FromQuaternion(UnityEngine.Quaternion.RotateTowards(MathConverter.ToQuaternion(from),
                 MathConverter.ToQuaternion(to), maxDegreesDelta));
        }
    }
}
