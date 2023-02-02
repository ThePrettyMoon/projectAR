using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Insight;

namespace Insight
{
    public class Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public float r
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public float g
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public float b
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }

        public float a
        {
            get
            {
                return this.w;
            }
            set
            {
                this.w = value;
            }
        }


        public float width
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }

        public float height
        {
            get
            {
                return this.w;
            }
            set
            {
                this.w = value;
            }
        }

        public Vector4()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.w = 0;
        }

        public static Vector4 New(float x,float y,float z,float w)
        {
            return new Vector4(x, y, z, w);
        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4 set(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
            return this;
        }

        public Vector4 setScalar(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
            this.z = scalar;
            this.w = scalar;
            return this;
        }

        public Vector4 setX(float x)
        {
            this.x = x;
            return this;
        }


        public Vector4 setY(float y)
        {
            this.y = y;
            return this;
        }

        public Vector4 setZ(float z)
        {
            this.z = z;
            return this;
        }

        public Vector4 setW(float w)
        {
            this.w = w;
            return this;
        }


        public Vector4 setComponent(int index, float value)
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

        public Vector4 clone()
        {
            return new Vector4(this.x, this.y, this.z, this.w);
        }


        public Vector4 copy(Vector4 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            this.w = v.w;
            return this;
        }


        public Vector4 add(Vector4 v)
        {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
            this.w += v.w;
            return this;
        }

        public Vector4 add(Vector4 v,Vector4 w)
        {
            return this.addVectors(v, w);
        }

        public Vector4 addScalar(float s)
        {
            this.x += s;
            this.y += s;
            this.z += s;
            this.w += s;
            return this;
        }

        public Vector4 addVectors(Vector4 a, Vector4 b)
        {
            this.x = a.x + b.x;
            this.y = a.y + b.y;
            this.z = a.z + b.z;
            this.w = a.w + b.w;
            return this;
        }

        public Vector4 addScaledVector(Vector4 v, float s)
        {
            this.x += v.x * s;
            this.y += v.y * s;
            this.z += v.z * s;
            this.w += v.w * s;
            return this;
        }


        public Vector4 sub(Vector4 v)
        {
            this.x -= v.x;
            this.y -= v.y;
            this.z -= v.z;
            this.w -= v.w;
            return this;
        }

        public Vector4 subScalar(float s)
        {
            this.x -= s;
            this.y -= s;
            this.z -= s;
            this.w -= s;
            return this;
        }

        public Vector4 subVectors(Vector4 a, Vector4 b)
        {
            this.x = a.x - b.x;
            this.y = a.y - b.y;
            this.z = a.z - b.z;
            this.w = a.w - b.w;
            return this;
        }

        public Vector4 multiply( Vector4 a)
        {
            this.x *= a.x;
            this.y *= a.y;
            this.z *= a.z;
            this.w *= a.w;
            return this;
        }

        public Vector4 divide(Vector4 a)
        {
            this.x /= a.x;
            this.y /= a.y;
            this.z /= a.z;
            this.w /= a.w;
            return this;
        }

        public Vector4 multiplyScalar(float scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
            this.z *= scalar;
            this.w *= scalar;
            return this;
        }

        public Vector4 multiplyVectors( Vector4 a, Vector4 b)
        {
            this.x = a.x * b.x;
            this.y = a.y * b.y;
            this.z = a.z * b.z;
            this.w = a.w * b.w;
            return this;
        }

        public Vector4 applyMatrix4(Insight.Matrix4x4 m)
        {
            float x = this.x;
            float y = this.y;
            float z = this.z;
            float[] e = m.elements; 

            this.x = e[0] * x + e[4] * y + e[8] * z + e[12] * w;
            this.y = e[1] * x + e[5] * y + e[9] * z + e[13] * w;
            this.z = e[2] * x + e[6] * y + e[10] * z + e[14] * w;
            this.w = e[3] * x + e[7] * y + e[11] * z + e[15] * w;
            return this;
        }

        public Vector4 divideScalar(float scalar)
        {
            return this.multiplyScalar(1.0f / scalar);
        }

        public Vector4 divideVectors( Vector4 a, Vector4 b)
        {
            this.x = a.x / b.x;
            this.y = a.y / b.y;
            this.z = a.z / b.z;
            this.w = a.w / b.w;
            return this;
        }


        public Vector4 setAxisAngleFromQuaternion(Quaternion q)
        {
            this.w = 2.0f * Mathf.Acos(q.w);
            float s = Mathf.Sqrt(1 - q.w * q.w);

            if (s < 0.0001f)
            {
                this.x = 1;
                this.y = 0;
                this.z = 0;
            }
            else
            {
                this.x = q.x / s;
                this.y = q.y / s;
                this.z = q.z / s;
            }
            return this;
        }

        public Vector4 min(Vector4 v)
        {
            this.x = Mathf.Min(this.x, v.x);
            this.y = Mathf.Min(this.y, v.y);
            this.z = Mathf.Min(this.z, v.z);
            this.w = Mathf.Min(this.w, v.w);
            return this;
        }

        public Vector4 max(Vector4 v)
        {
            this.x = Mathf.Max(this.x, v.x);
            this.y = Mathf.Max(this.y, v.y);
            this.z = Mathf.Max(this.z, v.z);
            this.w = Mathf.Max(this.w, v.w);
            return this;
        }

        public Vector4 clamp(Vector4 min, Vector4 max)
        {

            this.x = Mathf.Max(min.x, Mathf.Min(max.x, this.x));
            this.y = Mathf.Max(min.y, Mathf.Min(max.y, this.y));
            this.z = Mathf.Max(min.z, Mathf.Min(max.z, this.z));
            this.w = Mathf.Max(min.w, Mathf.Min(max.w, this.w));

            return this;
        }


        public Vector4 clampScalar(float minVal, float maxVal)
        {
            this.x = Mathf.Max(minVal, Mathf.Min(maxVal, this.x));
            this.y = Mathf.Max(minVal, Mathf.Min(maxVal, this.y));
            this.z = Mathf.Max(minVal, Mathf.Min(maxVal, this.z));
            this.w = Mathf.Max(minVal, Mathf.Min(maxVal, this.w));

            return this;
        }

        public Vector4 clampLength(float min, float max)
        {
            var length = this.length();

            return this.divideScalar(length < MathConverter.EPS ? 1.0f : length).multiplyScalar(Mathf.Max(min, Mathf.Min(max, length)));
        }

        public Vector4 floor()
        {
            this.x = Mathf.Floor(this.x);
            this.y = Mathf.Floor(this.y);
            this.z = Mathf.Floor(this.z);
            this.w = Mathf.Floor(this.w);
            return this;
        }

        public Vector4 ceil()
        {
            this.x = Mathf.Ceil(this.x);
            this.y = Mathf.Ceil(this.y);
            this.z = Mathf.Ceil(this.z);
            this.w = Mathf.Ceil(this.w);
            return this;
        }

        public Vector4 round()
        {
            this.x = Mathf.Round(this.x);
            this.y = Mathf.Round(this.y);
            this.z = Mathf.Round(this.z);
            this.w = Mathf.Round(this.w);
            return this;
        }


        public Vector4 roundToZero()
        {
            this.x = (this.x < 0) ? Mathf.Ceil(this.x) : Mathf.Floor(this.x);
            this.y = (this.y < 0) ? Mathf.Ceil(this.y) : Mathf.Floor(this.y);
            this.z = (this.z < 0) ? Mathf.Ceil(this.z) : Mathf.Floor(this.z);
            this.w = (this.w < 0) ? Mathf.Ceil(this.w) : Mathf.Floor(this.w);
            return this;
        }

        public Vector4 negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            this.w = -this.w;
            return this;
        }

        public float dot(Vector4 v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z + this.w * v.w;
        }

        public float lengthSq()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w;
        }

        public float length()
        {
            return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z + this.w * this.w);
        }

        public float manhattanLength()
        {
            return Mathf.Abs(this.x) + Mathf.Abs(this.y) + Mathf.Abs(this.z) + Mathf.Abs(this.w);
        }

        public float manhattanDistanceTo(Vector4 v)
        {
            return Mathf.Abs(this.x - v.x) + Mathf.Abs(this.y - v.y) + Mathf.Abs(this.z - v.z)+ Mathf.Abs(this.w -v.w);
        }

        public Vector4 normalize()
        {
            return this.divideScalar(this.length() < MathConverter.EPS ? 1.0f : this.length());
        }

        public Vector4 setLength(float length)
        {
            return this.normalize().multiplyScalar(length);
        }

        public Vector4 lerp(Vector4 v, float alpha)
        {
            this.x += (v.x - this.x) * alpha;
            this.y += (v.y - this.y) * alpha;
            this.z += (v.z - this.z) * alpha;
            this.w += (v.w - this.w) * alpha;
            return this;
        }

        public Vector4 lerpVectors(Vector4 v1, Vector4 v2, float alpha)
        {
            return this.subVectors(v2, v1).multiplyScalar(alpha).add(v1);
        }

        public bool equals(Vector4 v)
        {
            return ((v.x == this.x) && (v.y == this.y) && (v.z == this.z) && (v.w == this.w));
        }

        public Vector4 fromArray(float[] array)
        {
            this.x = array[0];
            this.y = array[1];
            this.z = array[2];
            this.w = array[3];
            return this;
        }

        public Vector4 fromArray(float[] array, int offset)
        {
            this.x = array[offset];
            this.y = array[offset + 1];
            this.z = array[offset + 2];
            this.w = array[offset + 3];
            return this;
        }

        public float[] toArray()
        {
            float[] array = new float[4];
            array[0] = this.x;
            array[1] = this.y;
            array[2] = this.z;
            array[3] = this.w;
            return array;
        }


        public float[] toArray(float[] array)
        {
            if (array == null) array = new float[4] ;
            array[0] = this.x;
            array[1] = this.y;
            array[2] = this.z;
            array[3] = this.w;
            return array;
        }

        public float[] toArray(float[] array, int offset)
        {
            if (array == null) array = new float[4] ;
            array[offset] = this.x;
            array[offset + 1] = this.y;
            array[offset + 2] = this.z;
            array[offset + 3] = this.w;
            return array;
        }

        public string toString()
        {
            return "Vector4( x == " + this.x + " y == " + this.y + " z == " + this.z + " w == " + this.w + ")";
        }

        public float magnitude
        {
            get
            {
                return this.length();
            }
        }

        public Vector4 normalized
        {
            get
            {
                return this.normalize();
            }
        }

        public float sqrMagnitude
        {
            get
            {
                return this.lengthSq();
            }
        }

        public static Vector4 black
        {
            get
            {
                return new Vector4(0, 0, 0, 1);
            }
        }

        public static Vector4 blue
        {
            get
            {
                return new Vector4(0, 0, 1, 1);
            }
        }

        public static Vector4 clear
        {
            get
            {
                return new Vector4(0, 1, 1, 1);
            }
        }

        public static Vector4 cyan
        {
            get
            {
                return new Vector4(0, 0, 0, 0);
            }
        }

        public static Vector4 gray
        {
            get
            {
                return new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }

        public static Vector4 grey
        {
            get
            {
                return new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
            }
        }

        public static Vector4 green
        {
            get
            {
                return new Vector4(0, 1, 0, 1);
            }
        }

        public static Vector4 magenta
        {
            get
            {
                return new Vector4(1, 0, 1, 1);
            }
        }

        public static Vector4 negativeInfinity
        {
            get
            {
                return new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            }
        }

        public static Vector4 one
        {
            get
            {
                return new Vector4(1, 1, 1, 1);
            }
        }

        public static Vector4 positiveInfinity
        {
            get
            {
                return new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            }
        }

        public static Vector4 red
        {
            get
            {
                return new Vector4(1, 0, 0, 1);
            }
        }

        public static Vector4 white
        {
            get
            {
                return new Vector4(1, 1, 1, 1);
            }
        }

        public static Vector4 yellow
        {
            get
            {
                return new Vector4(1, 1, 0, 1);
            }
        }

        public static Vector4 zero
        {
            get
            {
                return new Vector4(0, 0, 0, 0);
            }
        }

        public static float Distance(Vector4 a, Vector4 b)
        {
            return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)
                + (a.z - b.z) * (a.z - b.z) + (a.w - b.w) * (a.w - b.w));
        }

        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t
                , a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
        }

        public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
        {
            return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t
             , a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
        }

        public static Vector4 Max(Vector4 a, Vector4 b)
        {
            return new Vector4(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y)
                , Mathf.Max(a.z, b.z), Mathf.Max(a.w, b.w));
        }

        public static Vector4 Min(Vector4 a, Vector4 b)
        {
            return new Vector4(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y)
                , Mathf.Min(a.z, b.z), Mathf.Min(a.w, b.w));
        }

        public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
        {
            return MathConverter.FromVector4(UnityEngine.Vector4.MoveTowards(MathConverter.ToVector4(current), MathConverter.ToVector4(target), maxDistanceDelta));
        }

        public static Vector4 Project(Vector4 vector, Vector4 onNormal)
        {
            return MathConverter.FromVector4(UnityEngine.Vector4.Project(MathConverter.ToVector4(vector), MathConverter.ToVector4(onNormal)));
        }

        public static Vector4 Scale(Vector4 a, Vector4 b)
        {
            float x = a.x * b.x;
            float y = a.y * b.y;
            float z = a.z * b.z;
            float w = a.w * b.w;
            return new Vector4(x, y, z, w);
        }
    }
}
