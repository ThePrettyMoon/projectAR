using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Insight
{
    public class Matrix4x4
    {
        public float[] elements;

        public Matrix4x4 set(float n11, float n12, float n13, float n14,
            float n21, float n22, float n23, float n24,
            float n31, float n32, float n33, float n34,
            float n41, float n42, float n43, float n44)
        {
           if(this.elements==null) this.elements = new float[16];
            var te = this.elements;

            te[0] = n11; te[4] = n12; te[8] = n13; te[12] = n14;
            te[1] = n21; te[5] = n22; te[9] = n23; te[13] = n24;
            te[2] = n31; te[6] = n32; te[10] = n33; te[14] = n34;
            te[3] = n41; te[7] = n42; te[11] = n43; te[15] = n44;
            return this;
        }

        public Matrix4x4()
        {
            this.elements = new float[16];
        }

        public static Matrix4x4 New()
        {
            Matrix4x4 m =  new Matrix4x4();
            m.elements = new float[16];
            return m;
        }

        public static Matrix4x4 zero
        {
            get
            {
                Matrix4x4 mat = new Matrix4x4();
                mat.set(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return mat;
            }
        }

        public static Matrix4x4 identity
        {
            get
            {
                Matrix4x4 mat = new Matrix4x4();
                mat.set(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                return mat;
            }
        }

        public float getComponent(int index)
        {
            if (index < this.elements.Length)
            {
                return this.elements[index];
            }
            else
            {
                Debug.LogFormat("index %d is out of [0,15] ", index);
                return 0.0f;
            }
        }

        public Matrix4x4 setComponent(int index,float num)
        {
            if (index < this.elements.Length)
            {
               this.elements[index] = num;
            }
            else
            {
                Debug.LogFormat("index %d is out of [0,15] ", index);
            }
            return this;
        }

        public Matrix4x4 clone()
        {
            return new Matrix4x4().fromArray(this.elements);
        }

        public Matrix4x4 copy(Matrix4x4 m)
        {
            if (this.elements == null) this.elements = new float[16];
            var te = this.elements;
            var me = m.elements;

            te[0] = me[0]; te[1] = me[1]; te[2] = me[2]; te[3] = me[3];
            te[4] = me[4]; te[5] = me[5]; te[6] = me[6]; te[7] = me[7];
            te[8] = me[8]; te[9] = me[9]; te[10] = me[10]; te[11] = me[11];
            te[12] = me[12]; te[13] = me[13]; te[14] = me[14]; te[15] = me[15];

            return this;
        }

        public static Matrix4x4 Copy(UnityEngine.Matrix4x4 m)
        {
            Matrix4x4 mat = new Matrix4x4();
            if (mat.elements == null) mat.elements = new float[16];
            var te = mat.elements;

            te[0] = m.m00; te[1] = m.m01; te[2] = m.m02; te[3] = m.m03;
            te[4] = m.m10; te[5] = m.m11; te[6] = m.m12; te[7] = m.m13;
            te[8] = m.m20; te[9] = m.m21; te[10] = m.m22; te[11] = m.m23;
            te[12] = m.m30; te[13] = m.m31; te[14] = m.m32; te[15] = m.m33;

            return mat;
        }

        public Matrix4x4 copyPosition(Matrix4x4 m)
        {
            if (this.elements == null) this.elements = new float[16];
            var te = this.elements;
            var me = m.elements;

            te[12] = me[12];
            te[13] = me[13];
            te[14] = me[14];

            return this;
        }

        public Matrix4x4 extractBasis(Vector3 xAxis, Vector3 yAxis,
            Vector3 zAxis)
        {
            xAxis.setFromMatrixColumn(this, 0);
            yAxis.setFromMatrixColumn(this, 1);
            zAxis.setFromMatrixColumn(this, 2);

            return this;
        }

        public Matrix4x4 makeBasis(Vector3 xAxis, Vector3 yAxis,
            Vector3 zAxis)
        {
            this.set(xAxis.x, yAxis.x, zAxis.x, 0,
                     xAxis.y, yAxis.y, zAxis.y, 0,
                     zAxis.z, zAxis.z, zAxis.z, 0,
                     0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 extractRotation(Matrix4x4 m)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var me = m.elements;

            Vector3 _v1 = new Vector3();
            var scaleX = 1 / _v1.setFromMatrixColumn(m, 0).length();
            var scaleY = 1 / _v1.setFromMatrixColumn(m, 1).length();
            var scaleZ = 1 / _v1.setFromMatrixColumn(m, 2).length();

            te[0] = me[0] * scaleX;
            te[1] = me[1] * scaleX;
            te[2] = me[2] * scaleX;
            te[3] = 0;

            te[4] = me[4] * scaleY;
            te[5] = me[5] * scaleY;
            te[6] = me[6] * scaleY;
            te[7] = 0;

            te[8] = me[8] * scaleZ;
            te[9] = me[9] * scaleZ;
            te[10] = me[10] * scaleZ;
            te[11] = 0;

            te[12] = 0;
            te[13] = 0;
            te[14] = 0;
            te[15] = 1;

            return this;
        }

        public Matrix4x4 makeRotationFromQuaternion(Quaternion q)
        {
            Vector3 _zero = new Vector3(0, 0, 0);
            Vector3 _one = new Vector3(1, 1, 1);
            return this.compose(_zero, q, _one);
        }

        public Matrix4x4 lookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            var _x = new Vector3();
            var _y = new Vector3();
            var _z = new Vector3();

            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            _z.subVectors(eye, target);

            if (_z.lengthSq() == 0)
            {

                // eye and target are in the same position

                _z.z = 1;

            }

            _z.normalize();
            _x.crossVectors(up, _z);

            if (_x.lengthSq() == 0)
            {

                // up and z are parallel

                if (Mathf.Abs(up.z) == 1)
                {

                    _z.x += 0.0001f;

                }
                else
                {

                    _z.z += 0.0001f;

                }

                _z.normalize();
                _x.crossVectors(up, _z);

            }

            _x.normalize();
            _y.crossVectors(_z, _x);

            te[0] = _x.x; te[4] = _y.x; te[8] = _z.x;
            te[1] = _x.y; te[5] = _y.y; te[9] = _z.y;
            te[2] = _x.z; te[6] = _y.z; te[10] = _z.z;

            return this;
        }

        public Matrix4x4 multiply(Matrix4x4 m)
        {
            return this.multiplyMatrices(this, m);
        }

        public Matrix4x4 premultiply(Matrix4x4 m)
        {
            return this.multiplyMatrices(m, this);
        }

        public Matrix4x4 multiplyMatrices(Matrix4x4 a, Matrix4x4 b)
        {
            if (this.elements == null) this.elements = new float[16];

            var ae = a.elements;
            var be = b.elements;
            var te = this.elements;

            var a11 = ae[0]; var a12 = ae[4]; var a13 = ae[8]; var a14 = ae[12];
            var a21 = ae[1]; var a22 = ae[5]; var a23 = ae[9]; var a24 = ae[13];
            var a31 = ae[2]; var a32 = ae[6]; var a33 = ae[10]; var a34 = ae[14];
            var a41 = ae[3]; var a42 = ae[7]; var a43 = ae[11]; var a44 = ae[15];

            var b11 = be[0]; var b12 = be[4]; var b13 = be[8]; var b14 = be[12];
            var b21 = be[1]; var b22 = be[5]; var b23 = be[9]; var b24 = be[13];
            var b31 = be[2]; var b32 = be[6]; var b33 = be[10]; var b34 = be[14];
            var b41 = be[3]; var b42 = be[7]; var b43 = be[11]; var b44 = be[15];

            te[0] = a11 * b11 + a12 * b21 + a13 * b31 + a14 * b41;
            te[4] = a11 * b12 + a12 * b22 + a13 * b32 + a14 * b42;
            te[8] = a11 * b13 + a12 * b23 + a13 * b33 + a14 * b43;
            te[12] = a11 * b14 + a12 * b24 + a13 * b34 + a14 * b44;

            te[1] = a21 * b11 + a22 * b21 + a23 * b31 + a24 * b41;
            te[5] = a21 * b12 + a22 * b22 + a23 * b32 + a24 * b42;
            te[9] = a21 * b13 + a22 * b23 + a23 * b33 + a24 * b43;
            te[13] = a21 * b14 + a22 * b24 + a23 * b34 + a24 * b44;

            te[2] = a31 * b11 + a32 * b21 + a33 * b31 + a34 * b41;
            te[6] = a31 * b12 + a32 * b22 + a33 * b32 + a34 * b42;
            te[10] = a31 * b13 + a32 * b23 + a33 * b33 + a34 * b43;
            te[14] = a31 * b14 + a32 * b24 + a33 * b34 + a34 * b44;

            te[3] = a41 * b11 + a42 * b21 + a43 * b31 + a44 * b41;
            te[7] = a41 * b12 + a42 * b22 + a43 * b32 + a44 * b42;
            te[11] = a41 * b13 + a42 * b23 + a43 * b33 + a44 * b43;
            te[15] = a41 * b14 + a42 * b24 + a43 * b34 + a44 * b44;

            return this;
        }

        public Matrix4x4 multiplyScalar(float s)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            te[0] *= s; te[4] *= s; te[8] *= s; te[12] *= s;
            te[1] *= s; te[5] *= s; te[9] *= s; te[13] *= s;
            te[2] *= s; te[6] *= s; te[10] *= s; te[14] *= s;
            te[3] *= s; te[7] *= s; te[11] *= s; te[15] *= s;

            return this;
        }

        public float determinant()
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            var n11 = te[0]; var n12 = te[4]; var n13 = te[8]; var n14 = te[12];
            var n21 = te[1]; var n22 = te[5]; var n23 = te[9]; var n24 = te[13];
            var n31 = te[2]; var n32 = te[6]; var n33 = te[10]; var n34 = te[14];
            var n41 = te[3]; var n42 = te[7]; var n43 = te[11]; var n44 = te[15];

            //TODO: make this more efficient
            //( based on http://www.euclideanspace.com/maths/algebra/matrix/functions/inverse/fourD/index.htm )

            return (
                n41 * (
                    +n14 * n23 * n32
                     - n13 * n24 * n32
                     - n14 * n22 * n33
                     + n12 * n24 * n33
                     + n13 * n22 * n34
                     - n12 * n23 * n34
                ) +
                n42 * (
                    +n11 * n23 * n34
                     - n11 * n24 * n33
                     + n14 * n21 * n33
                     - n13 * n21 * n34
                     + n13 * n24 * n31
                     - n14 * n23 * n31
                ) +
                n43 * (
                    +n11 * n24 * n32
                     - n11 * n22 * n34
                     - n14 * n21 * n32
                     + n12 * n21 * n34
                     + n14 * n22 * n31
                     - n12 * n24 * n31
                ) +
                n44 * (
                    -n13 * n22 * n31
                     - n11 * n23 * n32
                     + n11 * n22 * n33
                     + n13 * n21 * n32
                     - n12 * n21 * n33
                     + n12 * n23 * n31
                )

            );
        }

        public Matrix4x4 transpose()
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var tmp = 0.0f;

            tmp = te[1]; te[1] = te[4]; te[4] = tmp;
            tmp = te[2]; te[2] = te[8]; te[8] = tmp;
            tmp = te[6]; te[6] = te[9]; te[9] = tmp;

            tmp = te[3]; te[3] = te[12]; te[12] = tmp;
            tmp = te[7]; te[7] = te[13]; te[13] = tmp;
            tmp = te[11]; te[11] = te[14]; te[14] = tmp;

            return this;
        }

        public Matrix4x4 setPosition(Vector3 v)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            te[12] = v.x;
            te[13] = v.y;
            te[14] = v.z;
            return this;
        }

        public Matrix4x4 setPosition(float x, float y, float z)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            te[12] = x;
            te[13] = y;
            te[14] = z;
            return this;
        }


        public Matrix4x4 getInverse(Matrix4x4 m)
        {
            return this.getInverse(m, false);
        }

        public Matrix4x4 getInverse(Matrix4x4 m, bool throwOnDegenerate)
        {
            if (this.elements == null) this.elements = new float[16];

            // based on http://www.euclideanspace.com/maths/algebra/matrix/functions/inverse/fourD/index.htm
            var te = this.elements;
            var me = m.elements;

            var n11 = me[0]; var n21 = me[1]; var n31 = me[2]; var n41 = me[3];
            var n12 = me[4]; var n22 = me[5]; var n32 = me[6]; var n42 = me[7];
            var n13 = me[8]; var n23 = me[9]; var n33 = me[10]; var n43 = me[11];
            var n14 = me[12]; var n24 = me[13]; var n34 = me[14]; var n44 = me[15];

            var t11 = n23 * n34 * n42 - n24 * n33 * n42 + n24 * n32 * n43 - n22 * n34 * n43 - n23 * n32 * n44 + n22 * n33 * n44;
            var t12 = n14 * n33 * n42 - n13 * n34 * n42 - n14 * n32 * n43 + n12 * n34 * n43 + n13 * n32 * n44 - n12 * n33 * n44;
            var t13 = n13 * n24 * n42 - n14 * n23 * n42 + n14 * n22 * n43 - n12 * n24 * n43 - n13 * n22 * n44 + n12 * n23 * n44;
            var t14 = n14 * n23 * n32 - n13 * n24 * n32 - n14 * n22 * n33 + n12 * n24 * n33 + n13 * n22 * n34 - n12 * n23 * n34;

            var det = n11 * t11 + n21 * t12 + n31 * t13 + n41 * t14;

            if (det == 0)
            {
                string errorMessage = "Matrix4x4: .getInverse() can't invert matrix, determinant is 0";
                if (throwOnDegenerate)
                {
                    throw new Exception(errorMessage);
                }
                else
                {
                    Debug.LogWarning(errorMessage);
                }
                return Matrix4x4.identity;
            }

            var detInv = 1.0f / det;

            te[0] = t11 * detInv;
            te[1] = (n24 * n33 * n41 - n23 * n34 * n41 - n24 * n31 * n43 + n21 * n34 * n43 + n23 * n31 * n44 - n21 * n33 * n44) * detInv;
            te[2] = (n22 * n34 * n41 - n24 * n32 * n41 + n24 * n31 * n42 - n21 * n34 * n42 - n22 * n31 * n44 + n21 * n32 * n44) * detInv;
            te[3] = (n23 * n32 * n41 - n22 * n33 * n41 - n23 * n31 * n42 + n21 * n33 * n42 + n22 * n31 * n43 - n21 * n32 * n43) * detInv;

            te[4] = t12 * detInv;
            te[5] = (n13 * n34 * n41 - n14 * n33 * n41 + n14 * n31 * n43 - n11 * n34 * n43 - n13 * n31 * n44 + n11 * n33 * n44) * detInv;
            te[6] = (n14 * n32 * n41 - n12 * n34 * n41 - n14 * n31 * n42 + n11 * n34 * n42 + n12 * n31 * n44 - n11 * n32 * n44) * detInv;
            te[7] = (n12 * n33 * n41 - n13 * n32 * n41 + n13 * n31 * n42 - n11 * n33 * n42 - n12 * n31 * n43 + n11 * n32 * n43) * detInv;

            te[8] = t13 * detInv;
            te[9] = (n14 * n23 * n41 - n13 * n24 * n41 - n14 * n21 * n43 + n11 * n24 * n43 + n13 * n21 * n44 - n11 * n23 * n44) * detInv;
            te[10] = (n12 * n24 * n41 - n14 * n22 * n41 + n14 * n21 * n42 - n11 * n24 * n42 - n12 * n21 * n44 + n11 * n22 * n44) * detInv;
            te[11] = (n13 * n22 * n41 - n12 * n23 * n41 - n13 * n21 * n42 + n11 * n23 * n42 + n12 * n21 * n43 - n11 * n22 * n43) * detInv;

            te[12] = t14 * detInv;
            te[13] = (n13 * n24 * n31 - n14 * n23 * n31 + n14 * n21 * n33 - n11 * n24 * n33 - n13 * n21 * n34 + n11 * n23 * n34) * detInv;
            te[14] = (n14 * n22 * n31 - n12 * n24 * n31 - n14 * n21 * n32 + n11 * n24 * n32 + n12 * n21 * n34 - n11 * n22 * n34) * detInv;
            te[15] = (n12 * n23 * n31 - n13 * n22 * n31 + n13 * n21 * n32 - n11 * n23 * n32 - n12 * n21 * n33 + n11 * n22 * n33) * detInv;

            return this;
        }

        public Matrix4x4 scale(Vector3 v)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var x = v.x; var y = v.y; var z = v.z;

            te[0] *= x; te[4] *= y; te[8] *= z;
            te[1] *= x; te[5] *= y; te[9] *= z;
            te[2] *= x; te[6] *= y; te[10] *= z;
            te[3] *= x; te[7] *= y; te[11] *= z;

            return this;
        }

        public float getMaxScaleOnAxis()
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            var scaleXSq = te[0] * te[0] + te[1] * te[1] + te[2] * te[2];
            var scaleYSq = te[4] * te[4] + te[5] * te[5] + te[6] * te[6];
            var scaleZSq = te[8] * te[8] + te[9] * te[9] + te[10] * te[10];

            return Mathf.Sqrt(Mathf.Max(scaleXSq, scaleYSq, scaleZSq));
        }

        public Matrix4x4 makeTranslation(float x, float y, float z)
        {
            this.set(1, 0, 0, x, 0, 1, 0, y, 0, 0, 1, z, 0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 makeRotationX(float theta)
        {
            var c = Mathf.Cos(theta); var s = Mathf.Sin(theta);
            this.set(1, 0, 0, 0, 0, c, -s, 0, 0, s, c, 0, 0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 makeRotationY(float theta)
        {
            var c = Mathf.Cos(theta); var s = Mathf.Sin(theta);
            this.set(c, 0, s, 0, 0, 1, 0, 0, -s, 0, c, 0, 0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 makeRotationZ(float theta)
        {
            var c = Mathf.Cos(theta); var s = Mathf.Sin(theta);
            this.set(c, -s, 0, 0, s, c, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 makeRotationAxis(Vector3 axis, float angle)
        {
            // Based on http://www.gamedev.net/reference/articles/article1199.asp

            var c = Mathf.Cos(angle);
            var s = Mathf.Sin(angle);
            var t = 1 - c;
            var x = axis.x; var y = axis.y; var z = axis.z;
            var tx = t * x; var ty = t * y;
            this.set(
                tx * x + c, tx * y - s * z, tx * z + s * y, 0,
                tx * y + s * z, ty * y + c, ty * z - s * x, 0,
                tx * z - s * y, ty * z + s * x, t * z * z + c, 0,
                0, 0, 0, 1
            );
            return this;
        }

        public Matrix4x4 makeScale(float x, float y, float z)
        {
            this.set(
            x, 0, 0, 0,
            0, y, 0, 0,
            0, 0, z, 0,
            0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 makeShear(float x, float y, float z)
        {
            this.set(
            1, y, z, 0,
            x, 1, z, 0,
            x, y, 1, 0,
            0, 0, 0, 1);
            return this;
        }

        public Matrix4x4 compose(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            var x = quaternion.x; var y = quaternion.y; var z = quaternion.z; var w = quaternion.w;
            var x2 = x + x; var y2 = y + y; var z2 = z + z;
            var xx = x * x2; var xy = x * y2; var xz = x * z2;
            var yy = y * y2; var yz = y * z2; var zz = z * z2;
            var wx = w * x2; var wy = w * y2; var wz = w * z2;

            var sx = scale.x; var sy = scale.y; var sz = scale.z;

            te[0] = (1 - (yy + zz)) * sx;
            te[1] = (xy + wz) * sx;
            te[2] = (xz - wy) * sx;
            te[3] = 0;

            te[4] = (xy - wz) * sy;
            te[5] = (1 - (xx + zz)) * sy;
            te[6] = (yz + wx) * sy;
            te[7] = 0;

            te[8] = (xz + wy) * sz;
            te[9] = (yz - wx) * sz;
            te[10] = (1 - (xx + yy)) * sz;
            te[11] = 0;

            te[12] = position.x;
            te[13] = position.y;
            te[14] = position.z;
            te[15] = 1;

            return this;
        }

        public Matrix4x4 decompose(Vector3 position, Quaternion quaternion, Vector3 scale)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            Vector3 _v1 = new Vector3();
            Matrix4x4 _m1 = new Matrix4x4();

            var sx = _v1.set(te[0], te[1], te[2]).length();
            var sy = _v1.set(te[4], te[5], te[6]).length();
            var sz = _v1.set(te[8], te[9], te[10]).length();

            // if determine is negative, we need to invert one scale
            var det = this.determinant();
            if (det < 0) sx = -sx;

            position.x = te[12];
            position.y = te[13];
            position.z = te[14];

            // scale the rotation part
            _m1.copy(this);

            var invSX = 1 / sx;
            var invSY = 1 / sy;
            var invSZ = 1 / sz;

            _m1.elements[0] *= invSX;
            _m1.elements[1] *= invSX;
            _m1.elements[2] *= invSX;

            _m1.elements[4] *= invSY;
            _m1.elements[5] *= invSY;
            _m1.elements[6] *= invSY;

            _m1.elements[8] *= invSZ;
            _m1.elements[9] *= invSZ;
            _m1.elements[10] *= invSZ;

            quaternion.setFromRotationMatrix(_m1);

            scale.x = sx;
            scale.y = sy;
            scale.z = sz;

            return this;
        }

        public Matrix4x4 makePerspective(float fovy, float aspect, float near, float far)
        {
            var top = near * Mathf.Tan(fovy * Mathf.Deg2Rad * 0.5f);

            var bottom = -top;

            var right = top * aspect;

            var left = -right;
            Matrix4x4 mat = new Matrix4x4();
            mat.makePerspective(left, right, top, bottom, near, far);
            return mat;
        }

        public Matrix4x4 makePerspective(float left, float right, float top, float bottom,
            float near, float far)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var x = 2 * near / (right - left);
            var y = 2 * near / (top - bottom);

            var a = (right + left) / (right - left);
            var b = (top + bottom) / (top - bottom);
            var c = -(far + near) / (far - near);
            var d = -2 * far * near / (far - near);

            te[0] = x; te[4] = 0; te[8] = a; te[12] = 0;
            te[1] = 0; te[5] = y; te[9] = b; te[13] = 0;
            te[2] = 0; te[6] = 0; te[10] = c; te[14] = d;
            te[3] = 0; te[7] = 0; te[11] = -1; te[15] = 0;

            return this;
        }

        public Matrix4x4 makeOrthographic(float left, float right, float top, float bottom,
            float near, float far)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var w = 1.0f / (right - left);
            var h = 1.0f / (top - bottom);
            var p = 1.0f / (far - near);

            var x = (right + left) * w;
            var y = (top + bottom) * h;
            var z = (far + near) * p;

            te[0] = 2 * w; te[4] = 0; te[8] = 0; te[12] = -x;
            te[1] = 0; te[5] = 2 * h; te[9] = 0; te[13] = -y;
            te[2] = 0; te[6] = 0; te[10] = -2 * p; te[14] = -z;
            te[3] = 0; te[7] = 0; te[11] = 0; te[15] = 1;

            return this;
        }

        public bool equals(Matrix4x4 m)
        {
            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;
            var me = m.elements;

            for (var i = 0; i < 16; i++)
            {

                if (te[i] != me[i]) return false;

            }

            return true;
        }

        public Matrix4x4 fromArray(float[] array)
        {
            return this.fromArray(array, 0);
        }

        public Matrix4x4 fromArray(float[] array, int offset)
        {
            if (this.elements == null) this.elements = new float[16];

            for (var i = 0; i < 16; i++)
            {
                this.elements[i] = array[i + offset];
            }
            return this;
        }

        public float[] toArray()
        {
          float[]  array = new float[16];
            return this.toArray(array, 0);
        }

        public float[] toArray(float[] array)
        {
            return this.toArray(array, 0);
        }

        public float[] toArray(float[] array, int offset)
        {
            if (array == null) array = new float[16];

            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            array[offset] = te[0];
            array[offset + 1] = te[1];
            array[offset + 2] = te[2];
            array[offset + 3] = te[3];

            array[offset + 4] = te[4];
            array[offset + 5] = te[5];
            array[offset + 6] = te[6];
            array[offset + 7] = te[7];

            array[offset + 8] = te[8];
            array[offset + 9] = te[9];
            array[offset + 10] = te[10];
            array[offset + 11] = te[11];

            array[offset + 12] = te[12];
            array[offset + 13] = te[13];
            array[offset + 14] = te[14];
            array[offset + 15] = te[15];

            return array;
        }

        public List<float> toArray(List<float> array)
        {
            return this.toArray(array, 0);
        }

        public List<float> toArray(List<float> array, int offset)
        {
            if (array == null) array = new List<float> ();

            if (this.elements == null) this.elements = new float[16];

            var te = this.elements;

            array[offset] = te[0];
            array[offset + 1] = te[1];
            array[offset + 2] = te[2];
            array[offset + 3] = te[3];

            array[offset + 4] = te[4];
            array[offset + 5] = te[5];
            array[offset + 6] = te[6];
            array[offset + 7] = te[7];

            array[offset + 8] = te[8];
            array[offset + 9] = te[9];
            array[offset + 10] = te[10];
            array[offset + 11] = te[11];

            array[offset + 12] = te[12];
            array[offset + 13] = te[13];
            array[offset + 14] = te[14];
            array[offset + 15] = te[15];

            return array;
        }

        public string toString()
        {
            return "Matrix4x4(" + this.elements[0] + ", " + this.elements[4] + ", " + this.elements[8] + ", " + this.elements[12] + ", " +
                this.elements[1] + ", " + this.elements[5] + ", " + this.elements[9] + ", " + this.elements[13] + ", " +
                 this.elements[2] + ", " + this.elements[6] + ", " + this.elements[10] + ", " + this.elements[14] + ", " +
                 this.elements[3] + ", " + this.elements[7] + ", " + this.elements[11] + ", " + this.elements[15] + ")";
        }

        public Quaternion getRotation()
        {
            Quaternion q = new Quaternion(0,0,0,1);
            if (this.elements == null) this.elements = new float[16];

            float[] me = this.elements;

            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + me[0] + me[5] + me[10])) / 2;
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + me[0] - me[5] - me[10])) / 2;
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - me[0] + me[5] - me[10])) / 2;
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - me[0] - me[5] + me[10])) / 2;
            q.x = q.x * (Mathf.Sign(q.x * (me[6] - me[9])));
            q.y = q.y * (Mathf.Sign(q.y * (me[8] - me[2])));
            q.z = q.x * (Mathf.Sign(q.z * (me[1] - me[4])));
            q.normalize();
            return q;
        }


        public Vector3 getPosition()
        {
            var p = new Vector3(0, 0, 0);
            if (this.elements == null) this.elements = new float[16];

            float[] me = this.elements;
            p.x = me[12];
            p.y = me[13];
            p.z = me[14];
            return p;
        }

        public Matrix4x4 inverse
        {
            get
            {
                return this.getInverse(this);
            }
        }

        public bool isIdentity
        {
            get
            {
                return this.equals(Matrix4x4.identity);
            }
        }

        public Vector3 lossyScale
        {
            get
            {
                if (this.elements == null) this.elements = new float[16];

                float[] me = this.elements;
                var p = this.getPosition();
                var q = this.getRotation();
                var normQ = q.normalize();
                Matrix4x4 mat = new Matrix4x4();
                var tr = mat.compose(p, normQ, new Vector3(1, 1, 1));
                var s = tr.inverse.multiply(this);
                var v3 = new Vector3(0, 0, 0);
                v3.x = me[0];
                v3.y = me[5];
                v3.z = me[10];
                return v3;
            }
        }

        public Quaternion rotation
        {
            get
            {
                return this.getRotation();
            }
        }

        public Vector4 getColumn(int index)
        {
            return new Vector4(this.elements[index * 4], this.elements[index * 4 + 1], this.elements[index * 4 + 2], this.elements[index * 4 + 3]);
        }

        public Vector4 getRow(int index)
        {
            return new Vector4(this.elements[index], this.elements[index + 4], this.elements[index + 8], this.elements[index + 12]);
        }

        public void setColumn(int index, Vector4 v)
        {
            this.elements[index * 4] = v.x;
            this.elements[index * 4 + 1] = v.y;
            this.elements[index * 4 + 2] = v.z;
            this.elements[index * 4 + 3] = v.w;
        }

        public void setRow(int index, Vector4 v)
        {
            this.elements[index] = v.x;
            this.elements[index + 4] = v.y;
            this.elements[index + 8] = v.z;
            this.elements[index + 12] = v.w;
        }

        public Vector4 multiplyVector4(Vector4 v)
        {
            var result = new Vector4(0, 0, 0, 0);
            float[] me = this.elements;
            result.x = v.x * me[0] + v.y * me[4] + v.z * me[8] + v.w * me[12];
            result.y = v.x * me[1] + v.y * me[5] + v.z * me[9] + v.w * me[13];
            result.z = v.x * me[2] + v.y * me[6] + v.z * me[10] + v.w * me[14];
            result.w = v.x * me[3] + v.y * me[7] + v.z * me[11] + v.w * me[15];
            return result;
        }

        public static Vector3 MultiplyPoint(Matrix4x4 mat, Vector3 v3)
        {
            var v = new Vector4(v3.x, v3.y, v3.z, 1);
            var v2 = mat.multiplyVector4(v);

            if (v2.w == 0)
            {
                v2.x = 0;
                v2.y = 0;
                v2.z = 0;
                return new Vector3(0, 0, 0);
            }
            return new Vector3(v2.x / v2.w, v2.y / v2.w, v2.z / v2.w);
        }

        public static Vector4 MultiplyPoint3x4(Matrix4x4 mat, Vector3 v3)
        {
            var v = new Vector4(v3.x, v3.y, v3.z, 1);
            var v2 = mat.multiplyVector4(v);

            if (v2.w == 0)
            {
                v2.x = 0;
                v2.y = 0;
                v2.z = 0;
                return new Vector4(0, 0, 0,1);
            }
            return new Vector4(v2.x / v2.w, v2.y / v2.w, v2.z / v2.w,1);
        }

        public static Vector3 MultiplyVector3(Matrix4x4 mat, Vector3 v3)
        {
            var v = new Vector4(v3.x, v3.y, v3.z, 0);

            var v2 = mat.multiplyVector4(v);
            if (v2.w == 0)
            {
                return new Vector3(v2.x, v2.y, v2.z);
            }
            return new Vector3(v2.x / v2.w, v2.y / v2.w, v2.z / v2.w);
        }

        public static Vector4 MultiplyVector4(Matrix4x4 mat, Vector4 v4)
        {
            var v2 = mat.multiplyVector4(v4);
            if (v2.w == 0)
            {
                return new Vector4(v2.x, v2.y, v2.z,0);
            }
            return new Vector4(v2.x / v2.w, v2.y / v2.w, v2.z / v2.w,0);
        }

        public void setTRS(Vector3 pos, Quaternion q, Vector3 s)
        {
            this.compose(pos, q, s);
        }

        public static Matrix4x4 LookAt(Vector3 from, Vector3 to, Vector3 up)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.lookAt(from, to, up);
            return mat;
        }

        public static Matrix4x4 Ortho(float left, float right, float top, float bottom,
            float near, float far)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat.makeOrthographic(left, right, top, bottom, near, far);
            return mat;
        }

        public static Matrix4x4 Perspective(float fovy, float aspect, float near, float far)
        {
            var top = near * Mathf.Tan(fovy * Mathf.Deg2Rad * 0.5f);

            var bottom = -top;

            var right = top * aspect;

            var left = -right;
            Matrix4x4 mat = new Matrix4x4();
            mat.makePerspective(left, right, top, bottom, near, far);
            return mat;
        }

        public static Matrix4x4 Rotate(Quaternion q)
        {
            var m = Matrix4x4.identity;

            var num = q.x * 2;
            var num2 = q.y * 2;
            var num3 = q.z * 2;
            var num4 = q.x * num;
            var num5 = q.y * num2;
            var num6 = q.z * num3;
            var num7 = q.x * num2;
            var num8 = q.x * num3;
            var num9 = q.y * num3;
            var num10 = q.w * num;
            var num11 = q.w * num2;
            var num12 = q.w * num3;

            m.elements[0] = 1 - (num5 + num6);
            m.elements[4] = num7 - num12;
            m.elements[8] = num8 + num11;
            m.elements[1] = num7 + num12;
            m.elements[5] = 1 - (num4 + num6);
            m.elements[9] = num9 - num10;
            m.elements[2] = num8 - num11;
            m.elements[6] = num9 + num10;
            m.elements[10] = 1 - (num4 + num5);
            return m;
        }

        public static Matrix4x4 Scale(Vector3 s)
        {
            Matrix4x4 m = new Matrix4x4();
            return m.makeScale(s.x, s.y, s.z);
        }

        public static Matrix4x4 Translate(Vector3 p)
        {
            Matrix4x4 m = new Matrix4x4();
            return m.makeTranslation(p.x, p.y, p.z);
        }

        public static Matrix4x4 TRS(Vector3 p, Quaternion q, Vector3 s)
        {
            Matrix4x4 m = new Matrix4x4();
            return m.compose(p, q, s);      
        }
    }
}
