using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace Insight
{
    /// <summary>
    /// vector2 成员变量和方法驼峰命名法
    /// </summary>
    public class Vector2
    {
        public float x;
        public float y;

        public float width
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

        public float height
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

        public Vector2()
        {
            this.x = 0;
            this.y = 0;
        }

        public static Vector2 New(float x,float y)
        {
            Vector2 vect = new Vector2(x, y);
            return vect;
        }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 set(float x, float y)
        {
            this.x = x;
            this.y = y;
            return this;
        }

        public Vector2 setScalar(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
            return this;
        }

        public Vector2 setX(float x)
        {
            this.x = x;
            return this;
        }

        public Vector2 setY(float y)
        {
            this.y = y;
            return this;
        }

        public Vector2 setComponent(int index, float value)
        {
            switch (index)
            {
                case 0:
                    this.x = value; break;
                case 1:
                    this.y = value; break;
                default:
                    throw new Exception("index is out of range: " + index);
            }
            return this;
        }

        public float getComponent(int index)
        {
            switch (index)
            {
                case 0: return this.x;
                case 1: return this.y;
                default:
                    throw new Exception("index is out of range: " + index);
            }
        }

        public Vector2 clone()
        {
            return new Vector2(this.x, this.y);
        }

        public Vector2 copy(Vector2 v)
        {
            this.x = v.x;
            this.y = v.y;
            return this;
        }

        public Vector2 add(Vector2 v)
        {
            this.x += v.x;
            this.y += v.y;
            return this;
        }

        public Vector2 addScalar(float s)
        {
            this.x += s;
            this.y += s;
            return this;
        }

        public Vector2 addVectors(Vector2 a, Vector2 b)
        {
            this.x = a.x + b.x;
            this.y = a.y + b.y;
            return this;
        }

        public Vector2 addScaledVector(Vector2 v, float s)
        {
            this.x += v.x * s;
            this.y += v.y * s;
            return this;
        }

        public Vector2 sub(Vector2 v)
        {
            this.x -= v.x;
            this.y -= v.y;
            return this;
        }

        public Vector2 subScalar(float s)
        {
            this.x -= s;
            this.y -= s;
            return this;
        }

        public Vector2 subVectors(Vector2 a, Vector2 b)
        {
            this.x = a.x - b.x;
            this.y = a.y - b.y;
            return this;
        }

        public Vector2 multiply(Vector2 v)
        {
            this.x *= v.x;
            this.y *= v.y;
            return this;
        }

        public Vector2 multiplyScalar(float scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
            return this;
        }

        public Vector2 multiplyVectors( Vector2 a, Vector2 b)
        {
            this.x = a.x * b.x;
            this.y = a.y * b.y;
            return this;
        }

        public Vector2 divide(Vector2 v)
        {
            this.x /= v.x;
            this.y /= v.y;
            return this;
        }

        public Vector2 divideScalar(float scalar)
        {
            return this.multiplyScalar(1.0f / scalar);
        }

        public Vector2 divideVectors(Vector2 a, Vector2 b)
        {
            this.x = a.x / b.x;
            this.y = a.y / b.y;
            return this;
        }

        public Vector2 min(Vector2 v)
        {
            this.x = Mathf.Min(this.x, x);
            this.y = Mathf.Min(this.y, y);
            return this;
        }

        public Vector2 max(Vector2 v)
        {
            this.x = Mathf.Max(this.x, x);
            this.y = Mathf.Max(this.y, y);
            return this;
        }

        public Vector2 clamp(Vector2 min, Vector2 max)
        {
            this.x = Mathf.Max(min.x, Mathf.Min(max.x, this.x));
            this.y = Mathf.Max(min.y, Mathf.Min(max.y, this.y));
            return this;
        }

        public Vector2 clampScalar(float minVal, float maxVal)
        {
            this.x = Mathf.Max(minVal, Mathf.Min(maxVal, this.x));
            this.y = Mathf.Max(minVal, Mathf.Min(maxVal, this.y));
            return this;
        }

        public Vector2 clampLength(float min, float max)
        {
            float length = this.length();
            return this.divideScalar(length < MathConverter.EPS ? 1.0f : length).multiplyScalar(Mathf.Max(min, Mathf.Min(max, length)));
        }

        public Vector2 floor()
        {
            this.x = Mathf.Floor(this.x);
            this.y = Mathf.Floor(this.y);
            return this;
        }

        public Vector2 ceil()
        {
            this.x = Mathf.Ceil(this.x);
            this.y = Mathf.Ceil(this.y);
            return this;
        }

        public Vector2 round()
        {
            this.x = Mathf.Round(this.x);
            this.y = Mathf.Round(this.y);
            return this;
        }

        public Vector2 roundToZero()
        {
            this.x = (this.x < 0) ? Mathf.Ceil(this.x) : Mathf.Floor(this.x);
            this.y = (this.y < 0) ? Mathf.Ceil(this.y) : Mathf.Floor(this.y);

            return this;
        }

        public Vector2 negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            return this;
        }

        public float dot(Vector2 v)
        {
            return this.x * v.x + this.y * v.y;
        }

        public float cross(Vector2 v)
        {
            return this.x * v.y - this.y * v.x;
        }

        public float lengthSq()
        {
            return this.x * this.x + this.y * this.y;
        }

        public float length()
        {
            return Mathf.Sqrt(this.x * this.x + this.y * this.y);
        }

        public float manhattanLength()
        {
            return Mathf.Abs(this.x) + Mathf.Abs(this.y);
        }

        public Vector2 normalize()
        {
            return this.divideScalar(this.length() < MathConverter.EPS ? 1.0f : this.length());
        }

        public float angle()
        {
            float angle = Mathf.Atan2(this.y, this.x);
            if (angle < 0) angle += 2 * Mathf.PI;
            return angle;
        }

        public float distanceTo(Vector2 v)
        {
            return Mathf.Sqrt(this.distanceToSquared(v));
        }

        public float distanceToSquared(Vector2 v)
        {
            float dx = this.x - v.x;
            float dy = this.y - v.y;
            return dx * dx + dy * dy;
        }

        public float manhattanDistanceTo(Vector2 v)
        {
            return Mathf.Abs(this.x - v.x) + Mathf.Abs(this.y - v.y);
        }

        public Vector2 setLength(float length)
        {
            return this.normalize().multiplyScalar(length);
        }

        public Vector2 lerp(Vector2 v, float alpha)
        {
            this.x += (v.x - this.x) * alpha;
            this.y += (v.y - this.y) * alpha;
            return this;
        }

        public Vector2 lerpVectors(Vector2 v1, Vector2 v2, float alpha)
        {
            return this.subVectors(v2, v1).multiplyScalar(alpha).add(v1);
        }

        public bool equals(Vector2 v)
        {
            return ((v.x == this.x) && (v.y == this.y));
        }

        public Vector2 fromArray(float[] array)
        {
            this.x = array[0];
            this.y = array[1];
            return this;
        }

        public Vector2 fromArray(float[] array, int offset)
        {
            this.x = array[offset];
            this.y = array[offset + 1];
            return this;
        }

        public float[] toArray()
        {
            float[] array = new float[2];
            array[0] = this.x;
            array[1] = this.y;
            return array;
        }

        public float[] toArray(float[] array)
        {
            if (array == null) array = new float[] { };
            array[0] = this.x;
            array[1] = this.y;
            return array;
        }

        public float[] toArray(float[] array, int offset)
        {
            if (array == null) array = new float[] { };
            array[offset] = this.x;
            array[offset + 1] = this.y;
            return array;
        }

        public Vector2 rotateAround(Vector2 center, float angle)
        {
            float c = Mathf.Cos(angle);
            float s = Mathf.Sin(angle);

            float x = this.x - center.x;
            float y = this.y - center.y;

            this.x = x * c - y * s + center.x;
            this.y = x * s + y * c + center.y;

            return this;
        }

        /// <summary>
        /// 日志打印，成员函数
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            return "Vector2( x == " + this.x + " y == " + this.y + ")";
        }

        public float magnitude
        {
            get
            {
                return this.length();
            }
        }

        public Vector2 normalized
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

        public static Vector2 down
        {
            get
            {
                return new Vector2(0, -1);
            }
        }

        public static Vector2 up
        {
            get
            {
                return new Vector2(0, 1);
            }
        }

        public static Vector2 left
        {
            get
            {
                return new Vector2(-1, 0);
            }
        }

        public static Vector2 right
        {
            get
            {
                return new Vector2(1, 0);
            }
        }

        public static Vector2 negativeInfinity
        {
            get
            {
                return new Vector2(float.NegativeInfinity, float.NegativeInfinity);
            }
        }

        public static Vector2 positiveInfinity
        {
            get
            {
                return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
            }
        }

        public static Vector2 one
        {
            get
            {
                return new Vector2(1, 1);
            }
        }

        public static Vector2 zero
        {
            get
            {
                return new Vector2(0, 0);
            }
        }

        public static float angle(Vector2 a, Vector2 b)
        {
            float x1 = a.x;
            float y1 = a.y;
            float d = Mathf.Sqrt(x1 * x1 + y1 * y1);

            if (d > MathConverter.EPS)
            {
                x1 = x1 / d;
                y1 = y1 / d;
            }
            else
            {
                x1 = y1 = 0;
            }

            float x2 = b.x;
            float y2 = b.y;
            d = Mathf.Sqrt(x2 * x2 + y2 * y2);

            if (d > MathConverter.EPS)
            {
                x2 = x2 / d;
                y2 = y2 / d;
            }
            else
            {
                x2 = y2 = 0;
            }

            d = x1 * x2 + y1 * y2;

            if (d < -1)
            {
                d = -1;
            }
            else if (d > 1)
            {
                d = 1;
            }
            return Mathf.Acos(d) * Mathf.Rad2Deg;
        }

        public static Vector2 ClampMagnitude(Vector2 v, float maxLength)
        {
            float x = v.x;
            float y = v.y;
            float sqrMag = x * x + y * y;

            if (sqrMag > maxLength * maxLength)
            {
                float mag = maxLength / Mathf.Sqrt(sqrMag);
                x *= mag;
                y *= mag;
                return new Vector2(x, y);
            }
            return new Vector2(x, y);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            return Mathf.Sqrt(Mathf.Pow((a.x - b.x), 2) + Mathf.Pow((a.y - b.y), 2));
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y));
        }

        public static Vector2 Min(Vector2 a, Vector2 b)
        {
            return new Vector2(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y));
        }

        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float cx = current.x;
            float cy = current.y;
            float x = target.x - cx;
            float y = target.y - cy;
            float s = x * x + y * y;

            if (s > maxDistanceDelta * maxDistanceDelta && s != 0)
            {
                s = maxDistanceDelta / Mathf.Sqrt(s);
                return new Vector2(cx + x * s, cy + y * s);
            }

            return new Vector2(target.x, target.y);
        }

        public static Vector2 Perpendicular(Vector2 v)
        {
            return new Vector2(-v.y, v.x);
        }

        public static Vector2 Reflect(Vector2 dir, Vector2 normal)
        {
            float dx = dir.x;
            float dy = dir.y;
            float nx = normal.x;
            float ny = normal.y;
            float s = -2 * (dx * nx + dy * ny);

            return new Vector2(s * nx + dx, s * ny + dy);
        }

        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        public static float Angle(Vector2 from,Vector2 to)
        {
           return Vector2.angle(from, to);
        }

        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            float angle = Vector2.angle(from, to);
            int signed = (Insight.Vector3.Cross(new Insight.Vector3(from.x, from.y, 1),
                new Insight.Vector3(to.x, to.y, 1))).z < 0 ? -1 : 1;
            return angle * signed;
        }

        public static Vector2 SmoothDamp(Vector2 current, Vector2 target,
            Vector2 currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
        {
            //deltaTime = deltaTime or Time.deltaTime
            //maxSpeed = maxSpeed or math.huge
            smoothTime = Mathf.Max((float)MathConverter.EPS, smoothTime);

            float num = 2.0f / smoothTime;
            float num2 = num * deltaTime;
            num2 = 1.0f / (1 + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            float tx = target.x;
            float ty = target.y;
            float cx = current.x;
            float cy = current.y;
            float vecx = cx - tx;
            float vecy = cy - ty;
            float m = vecx * vecx + vecy * vecy;
            float n = maxSpeed * smoothTime;

            if (m > n * n)
            {
                m = n / Mathf.Sqrt(m);
                vecx = vecx * m;
                vecy = vecy * m;
            }

            m = currentVelocity.x;
            n = currentVelocity.y;

            float vec3x = (m + num * vecx) * deltaTime;
            float vec3y = (n + num * vecy) * deltaTime;
            currentVelocity.x = (m - num * vec3x) * num2;

            currentVelocity.y = (n - num * vec3y) * num2;
            m = cx - vecx + (vecx + vec3x) * num2;
            n = cy - vecy + (vecy + vec3y) * num2;

            if ((tx - cx) * (m - tx) + (ty - cy) * (n - ty) > 0)
            {
                m = tx;
                n = ty;
                currentVelocity.x = 0;
                currentVelocity.y = 0;
            }
            return new Vector2(m, n);
        }
    }
}