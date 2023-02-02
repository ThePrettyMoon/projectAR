using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Insight;

namespace Insight
{
    public class Vector3
    {
        public float x;
        public float y;
        public float z;

        public static Vector3 New(float x,float y,float z)
        {
            return new Vector3(x, y, z);
        }

        public Vector3()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            return this;
        }

        public Vector3 setScalar(float scalar)
        {
            this.x = scalar;
            this.y = scalar;
            this.z = scalar;
            return this;
        }

        public Vector3 setX(float x)
        {
            this.x = x;
            return this;
        }

        public Vector3 setY(float y)
        {
            this.y = y;
            return this;
        }

        public Vector3 setZ(float z)
        {
            this.z = z;
            return this;
        }

        public Vector3 setComponent(int index, float value)
        {
            switch (index)
            {
                case 0: this.x = value; break;
                case 1: this.y = value; break;
                case 2: this.z = value; break;
                default: throw new Exception("index is out range: " + index);
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
                default: throw new Exception("index is out of range " + index);
            }
        }

        public Vector3 clone()
        {
            return new Vector3(this.x, this.y, this.z);
        }

        public Vector3 copy(Vector3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
            return this;
        }

        public Vector3 add(Vector3 v)
        {
            this.x += v.x;
            this.y += v.y;
            this.z += v.z;
            return this;
        }

        public Vector3 addScalar(float s)
        {
            this.x += s;
            this.y += s;
            this.z += s;
            return this;
        }

        public Vector3 addVectors(Vector3 a, Vector3 b)
        {
            this.x = a.x + b.x;
            this.y = a.y + b.y;
            this.z = a.z + b.z;
            return this;
        }

        public Vector3 addScaledVector(Vector3 v, float s)
        {
            this.x += v.x * s;
            this.y += v.y * s;
            this.z += v.z * s;
            return this;
        }

        public Vector3 sub(Vector3 v)
        {
            this.x -= v.x;
            this.y -= v.y;
            this.z -= v.z;
            return this;
        }

        public Vector3 subScalar(float s)
        {
            this.x -= s;
            this.y -= s;
            this.z -= s;
            return this;
        }

        public Vector3 subVectors(Vector3 a, Vector3 b)
        {
            this.x = a.x - b.x;
            this.y = a.y - b.y;
            this.z = a.z - b.z;
            return this;
        }

        public Vector3 multiply(Vector3 v)
        {
            this.x *= v.x;
            this.y *= v.y;
            this.z *= v.z;
            return this;
        }

        public Vector3 multiplyScalar(float scalar)
        {
            this.x *= scalar;
            this.y *= scalar;
            this.z *= scalar;
            return this;
        }

        public Vector3 multiplyVectors(Vector3 a, Vector3 b)
        {
            this.x = a.x * b.x;
            this.y = a.y * b.y;
            this.z = a.z * b.z;
            return this;
        }

        public Vector3 applyAxisAngle(Vector3 axis, float angle)
        {
            Quaternion quaternion = new Quaternion();
            return this.applyQuaternion(quaternion.setFromAxisAngle(axis, angle));
        }

        public Vector3 applyMatrix4(Insight.Matrix4x4 m)
        {
            float x = this.x;
            float y = this.y;
            float z = this.z;

            var e = m.elements;

            var w = 1 / (e[3] * x + e[7] * y + e[11] * z + e[15]);

            this.x = (e[0] * x + e[4] * y + e[8] * z + e[12]) * w;
            this.y = (e[1] * x + e[5] * y + e[9] * z + e[13]) * w;
            this.z = (e[2] * x + e[6] * y + e[10] * z + e[14]) * w;

            return this;
        }



        public Vector3 applyQuaternion(Quaternion q)
        {
            float x = this.x;
            float y = this.y;
            float z = this.z;
            float qx = q.x;
            float qy = q.y;
            float qz = q.z;
            float qw = q.w;

            // calculate quat * vector
            float ix = qw * x + qy * z - qz * y;
            float iy = qw * y + qz * x - qx * z;
            float iz = qw * z + qx * y - qy * x;
            float iw = -qx * x - qy * y - qz * z;

            // calculate result * inverse quat
            this.x = ix * qw + iw * -qx + iy * -qz - iz * -qy;
            this.y = iy * qw + iw * -qy + iz * -qx - ix * -qz;
            this.z = iz * qw + iw * -qz + ix * -qy - iy * -qx;
            return this;
        }

        public Vector3 project(Camera camera)
        {
            Insight.Matrix4x4 T_C_W = MathConverter.FromMatrix4x4(camera.worldToCameraMatrix);
            Insight.Matrix4x4 T_Ortho = MathConverter.FromMatrix4x4(camera.projectionMatrix);
            return this.applyMatrix4(T_C_W).applyMatrix4(T_Ortho);
        }

        public Vector3 unproject(Camera camera)
        {
            Insight.Matrix4x4 T_Ortho_Inverse = MathConverter.FromMatrix4x4(camera.projectionMatrix.inverse);
            Insight.Matrix4x4 T_W_C = MathConverter.FromMatrix4x4(camera.cameraToWorldMatrix);
            return this.applyMatrix4(T_Ortho_Inverse).applyMatrix4(T_W_C);
        }

        public Vector3 transformDirection(Insight.Matrix4x4 m)
        {
            float x = this.x;
            float y = this.y;
            float z = this.z;
            float[] e = m.elements;

            this.x = e[0] * x + e[4] * y + e[8] * z;
            this.y = e[1] * x + e[5] * y + e[9] * z;
            this.z = e[2] * x + e[6] * y + e[10] * z;

            return this.normalize();
        }

        public Vector3 divide(Vector3 v)
        {
            this.x /= v.x;
            this.y /= v.y;
            this.z /= v.z;
            return this;
        }

        public Vector3 divideScalar(float scalar)
        {
            return this.multiplyScalar(1.0f / scalar);
        }

        public Vector3 divideVectors( Vector3 a, Vector3 b)
        {
            this.x = a.x / b.x;
            this.y = a.y / b.y;
            this.z = a.z / b.z;
            return this;
        }

        public Vector3 min(Vector3 v)
        {
            this.x = Mathf.Min(this.x, v.x);
            this.y = Mathf.Min(this.y, v.y);
            this.z = Mathf.Min(this.z, v.z);
            return this;
        }

        public Vector3 max(Vector3 v)
        {
            this.x = Mathf.Max(this.x, v.x);
            this.y = Mathf.Max(this.y, v.y);
            this.z = Mathf.Max(this.z, v.z);
            return this;
        }

        public Vector3 clamp(Vector3 min, Vector3 max)
        {
            this.x = Mathf.Max(min.x, Mathf.Min(max.x, this.x));
            this.y = Mathf.Max(min.y, Mathf.Min(max.y, this.y));
            this.z = Mathf.Max(min.z, Mathf.Min(max.z, this.z));
            return this;
        }

        public Vector3 clampScalar(float minVal, float maxVal)
        {
            this.x = Mathf.Max(minVal, Mathf.Min(maxVal, this.x));
            this.y = Mathf.Max(minVal, Mathf.Min(maxVal, this.y));
            this.z = Mathf.Max(minVal, Mathf.Min(maxVal, this.z));
            return this;
        }

        public Vector3 clampLength(float min, float max)
        {
            var length = this.length();

            return this.divideScalar(length < MathConverter.EPS ? 1.0f : length).multiplyScalar(Mathf.Max(min, Mathf.Min(max, length)));
        }

        public Vector3 floor()
        {
            this.x = Mathf.Floor(this.x);
            this.y = Mathf.Floor(this.y);
            this.z = Mathf.Floor(this.z);
            return this;
        }

        public Vector3 ceil()
        {
            this.x = Mathf.Ceil(this.x);
            this.y = Mathf.Ceil(this.y);
            this.z = Mathf.Ceil(this.z);
            return this;
        }

        public Vector3 round()
        {
            this.x = Mathf.Round(this.x);
            this.y = Mathf.Round(this.y);
            this.z = Mathf.Round(this.z);
            return this;
        }

        public Vector3 roundToZero()
        {
            this.x = (this.x < 0) ? Mathf.Ceil(this.x) : Mathf.Floor(this.x);
            this.y = (this.y < 0) ? Mathf.Ceil(this.y) : Mathf.Floor(this.y);
            this.z = (this.z < 0) ? Mathf.Ceil(this.z) : Mathf.Floor(this.z);

            return this;
        }

        public Vector3 negate()
        {
            this.x = -this.x;
            this.y = -this.y;
            this.z = -this.z;
            return this;
        }

        public float dot(Vector3 v)
        {
            return this.x * v.x + this.y * v.y + this.z * v.z;
        }

        public float lengthSq()
        {
            return this.x * this.x + this.y * this.y + this.z * this.z;
        }

        public float length()
        {
            return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
        }

        public float manhattanLength()
        {
            return Mathf.Abs(this.x) + Mathf.Abs(this.y) + Mathf.Abs(this.z);
        }

        public Vector3 normalize()
        {
            return this.divideScalar(this.length() < MathConverter.EPS ? 1.0f : this.length());
        }

        public Vector3 setLength(float length)
        {
            return this.normalize().multiplyScalar(length);
        }

        public Vector3 lerp(Vector3 v, float alpha)
        {
            this.x += (v.x - this.x) * alpha;
            this.y += (v.y - this.y) * alpha;
            this.z += (v.z - this.z) * alpha;
            return this;
        }

        public Vector3 lerpVectors(Vector3 v1, Vector3 v2, float alpha)
        {
            return this.subVectors(v2, v1).multiplyScalar(alpha).add(v1);
        }

        public Vector3 cross(Vector3 v)
        {
            return this.crossVectors(this, v);
        }

        public Vector3 crossVectors(Vector3 a, Vector3 b)
        {
            var ax = a.x;
            var ay = a.y;
            var az = a.z;
            var bx = b.x;
            var by = b.y;
            var bz = b.z;

            this.x = ay * bz - az * by;
            this.y = az * bx - ax * bz;
            this.z = ax * by - ay * bx;

            return this;
        }

        public Vector3 projectOnVector(Vector3 v)
        {
            var scalar = v.dot(this) / v.lengthSq();
            return this.copy(v).multiplyScalar(scalar);
        }

        public Vector3 projectOnPlane(Vector3 planeNormal)
        {
            UnityEngine.Vector3 uVect = MathConverter.ToVector3(this);
            UnityEngine.Vector3 uNormal = MathConverter.ToVector3(planeNormal);
            return MathConverter.FromVector3(UnityEngine.Vector3.ProjectOnPlane(uVect, uNormal));
        }

        public Vector3 reflect(Vector3 normal)
        {
            Vector3 vect = new Vector3();
            return this.sub(vect.copy(normal).multiplyScalar(2 * this.dot(normal)));
        }

        public float angleTo(Vector3 v)
        {
            var denominator = Mathf.Sqrt(this.lengthSq() * v.lengthSq());
            if (denominator == 0) throw new Exception("Vector3:angleTo can't handle zero length vectors.");
            var theta = this.dot(v) / denominator;
            return Mathf.Acos(Mathf.Clamp(theta, -1, 1));
        }

        public float distanceTo(Vector3 v)
        {
            return Mathf.Sqrt(this.distanceToSquared(v));
        }

        public float distanceToSquared(Vector3 v)
        {
            float dx = this.x - v.x;
            float dy = this.y - v.y;
            float dz = this.z - v.z;
            return dx * dx + dy * dy + dz * dz;
        }

        public float manhattanDistanceTo(Vector3 v)
        {
            return Mathf.Abs(this.x - v.x) + Mathf.Abs(this.y - v.y) + Mathf.Abs(this.z - v.z);
        }

        public Vector3 setFromSphericalCoords(float radius, float phi, float theta)
        {
            float sinPhiRadius = Mathf.Sin(phi) * radius;

            this.x = sinPhiRadius * Mathf.Sin(theta);
            this.y = Mathf.Cos(phi) * radius;
            this.z = sinPhiRadius * Mathf.Cos(theta);
            return this;
        }

        public Vector3 setFromCylindricalCoords(float radius, float theta, float y)
        {
            this.x = radius * Mathf.Sin(theta);
            this.y = y;
            this.z = radius * Mathf.Cos(theta);
            return this;
        }

        public Vector3 setFromMatrixPosition(Matrix4x4 m)
        {
            var e = m.elements;
            this.x = e[12];
            this.y = e[13];
            this.z = e[14];
            return this;
        }

        public Vector3 setFromMatrixScale(Matrix4x4 m)
        {
            var sx = this.setFromMatrixColumn(m, 0).length();
            var sy = this.setFromMatrixColumn(m, 1).length();
            var sz = this.setFromMatrixColumn(m, 2).length();
            this.x = sx;
            this.y = sy;
            this.z = sz;
            return this;
        }

        public Vector3 setFromMatrixColumn(Matrix4x4 m, int index)
        {
            return this.fromArray(m.elements, index * 4);
        }

        public float[] toArray()
        {
            float[] array = new float[3];
            array[0] = this.x;
            array[1] = this.y;
            array[2] = this.z;
            return array;
        }

        public float[] toArray(float[] array)
        {
            if (array == null) array = new float[3];
            array[0] = this.x;
            array[1] = this.y;
            array[2] = this.z;
            return array;
        }

        public float[] toArray(float[] array, int offset)
        {
            if (array == null) array = new float[3];
            array[offset] = this.x;
            array[offset + 1] = this.y;
            array[offset + 2] = this.z;
            return array;
        }


        public bool equals(Vector3 v)
        {
            return ((v.x == this.x) && (v.y == this.y) && (v.z == this.z));
        }

        public Vector3 fromArray(float[] array, int offset)
        {
            if (array == null) array = new float[3];
            this.x = array[offset];
            this.y = array[offset + 1];
            this.z = array[offset + 2];
            return this;
        }

        public  Vector3 fromArray(float[] array)
        {
            if (array == null) array = new float[3];
            this.x = array[0];
            this.y = array[1];
            this.z = array[2];
            return this;
        }

        public string toString()
        {
            return "Vector3( x == " + this.x + " y == " + this.y + " z == " + this.z + ")";
        }

        public float magnitude
        {
            get
            {
                return this.length();
            }
        }

        public Vector3 normalized
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

        public static Vector3 forward
        {
            get
            {
                return new Vector3(0, 0, 1);
            }
        }

        public static Vector3 back
        {
            get
            {
                return new Vector3(0, 0, -1);
            }
        }

        public static Vector3 up
        {
            get
            {
                return new Vector3(0, 1, 0);
            }
        }

        public static Vector3 down
        {
            get
            {
                return new Vector3(0, -1, 0);
            }
        }

        public static Vector3 right
        {
            get
            {
                return new Vector3(1, 0, 0);
            }
        }

        public static Vector3 left
        {
            get
            {
                return new Vector3(-1, 0, 0);
            }
        }

        public static Vector3 negativeInfinity
        {
            get
            {
                return new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
            }
        }

        public static Vector3 positiveInfinity
        {
            get
            {
                return new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
            }
        }

        public static Vector3 one
        {
            get
            {
                return new Vector3(1, 1, 1);
            }
        }

        public static Vector3 zero
        {
            get
            {
                return new Vector3(0, 0, 0);
            }
        }

        public static float Angle(Vector3 a, Vector3 b)
        {
            Vector3 x = a.normalize();
            Vector3 y = a.normalize();
            return Mathf.Acos(Mathf.Clamp(Vector3.Dot(a, b), -1, 1)) * Mathf.Rad2Deg;
        }

        public static Vector3 ClampMagnitude(Vector3 v, float maxLength)
        {
            Vector3 vect = v.clone();
            if (v.sqrMagnitude > maxLength * maxLength)
            {
                vect = v.normalize();
                vect.multiplyScalar(maxLength);
            }
            return vect;
        }

        public static float Distance(Vector3 a, Vector3 b)
        {
            return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y)
              + (a.z - b.z) * (a.z - b.z));
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3 Lerp(Vector3 a, Vector3 b, float t)
        {
            t = Mathf.Clamp(t, 0, 1);
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t
                , a.z + (b.z - a.z) * t);
        }

        public static Vector3 LerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return new Vector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t
                , a.z + (b.z - a.z) * t);
        }

        public static Vector3 Slerp(Vector3 a, Vector3 b, float t)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.Slerp(MathConverter.ToVector3(a), MathConverter.ToVector3(b), t));
        }

        public static Vector3 SlerpUnclamped(Vector3 a, Vector3 b, float t)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.SlerpUnclamped(MathConverter.ToVector3(a), MathConverter.ToVector3(b), t));
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3().crossVectors(a, b);
        }

        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y)
                , Mathf.Max(a.z, b.z));
        }

        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y)
                , Mathf.Min(a.z, b.z));
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.MoveTowards(MathConverter.ToVector3(current), MathConverter.ToVector3(target), maxDistanceDelta));
        }

        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.Project(MathConverter.ToVector3(vector), MathConverter.ToVector3(onNormal)));
        }

        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 onNormal)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.ProjectOnPlane(MathConverter.ToVector3(vector), MathConverter.ToVector3(onNormal)));
        }

        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.Reflect(MathConverter.ToVector3(inDirection), MathConverter.ToVector3(inNormal)));
        }

        public static void OrthoNormalize(Vector3 normal, Vector3 tangent, Vector3 binormal)
        {
            normal.normalize();
            Vector3 vb = tangent.sub(Vector3.Project(tangent, normal));
            vb = vb.normalize();

            Vector3 vc = binormal.sub(Vector3.Project(binormal, normal));
            vc.sub(Vector3.Project(binormal, tangent));
            vc.normalize();
        }

        public static Vector3 RotateTowards(Vector3 current, Vector3 target,
            float maxRadiansDelta, float maxMagnitudeDelta)
        {
            return MathConverter.FromVector3(UnityEngine.Vector3.RotateTowards(MathConverter.ToVector3(current), MathConverter.ToVector3(target)
                , maxRadiansDelta, maxMagnitudeDelta));
        }

        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            float x = a.x * b.x;
            float y = a.y * b.y;
            float z = a.z * b.z;
            return new Vector3(x, y, z);
        }

        public static float SignedAngle(Vector3 a, Vector3 b, Vector3 axis)
        {
            return UnityEngine.Vector3.SignedAngle(MathConverter.ToVector3(a), MathConverter.ToVector3(b)
                , MathConverter.ToVector3(axis));
        }

        public static Vector3 SmoothDamp(Vector3 current, Vector3 target, Vector3 currentVelocity
            , float smoothTime, float maxSpeed, float deltaTime)
        {
            //  deltaTime = deltaTime or Time.deltaTime
            // maxSpeed = maxSpeed or math.huge
            smoothTime = Mathf.Max(0.0001f, smoothTime);
            float num = 2.0f / smoothTime;
            float num2 = num * deltaTime;
            float num3 = 1.0f / (1 + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
            float tx = target.x;
            float ty = target.y;
            float tz = target.z;
            float cx = current.x;
            float cy = current.y;
            float cz = current.z;
            float vecx = cx - tx;
            float vecy = cy - ty;
            float vecz = cz - tz;
            float m = vecx * vecx + vecy * vecy + vecz * vecz;
            float n = maxSpeed * smoothTime;

            if (m > n * n)
            {
                m = n / Mathf.Sqrt(m);
                vecx = vecx * m;
                vecy = vecy * m;
                vecz = vecz * m;
            }

            m = currentVelocity.x;
            n = currentVelocity.y;
            float k = currentVelocity.z;

            float vec4x = (m + num * vecx) * deltaTime;
            float vec4y = (n + num * vecy) * deltaTime;
            float vec4z = (k + num * vecz) * deltaTime;

            currentVelocity.x = (m - num * vec4x) * num3;
            currentVelocity.y = (n - num * vec4y) * num3;

            currentVelocity.z = (k - num * vec4z) * num3;

            m = cx - vecx + (vecx + vec4x) * num3;

            n = cy - vecy + (vecy + vec4y) * num3;

            k = cz - vecz + (vecz + vec4z) * num3;

            if ((tx - cx) * (m - tx) + (ty - cy) * (n - ty) + (tz - cz) * (k - tz) > 0)
            {
                m = tx;
                n = ty;
                k = tz;
                currentVelocity.x = 0;
                currentVelocity.y = 0;
                currentVelocity.z = 0;
            }
            return new Vector3(m, n, k);
        }
    }
}
