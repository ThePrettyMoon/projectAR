using EZXR.Glass.SixDof;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using System;

namespace EZXR.Glass.SixDof
{
    /**************************************************************************//**
    * EZVIOMesh：后端Mesh(稠密)可视化数据结构
    * -------------------------------------------------------------------------
    * vertex
    *   用于生成mesh的世界坐标系3D点位置(x1,y1,z1,x2,y2,z2,...)
    * vertexLength
    *   顶点buffer长度
    * vertexCount
    *   顶点数目
    * normal
    *   法向量值(nx1,ny1,nz1,nx2,ny2,nz2,...)
    * normalLength
    *   法向量buffer长度
    * normalCount
    *   法向量数目
    * faces
    *   三角面片定点ID
    * facesLength
    *   面片buffer长度
    * facesCount
    *   面片数目
    ******************************************************************************/
    public struct EZVIOBackendMesh
    {
        public IntPtr vertex;                //vertex points
        public int vertexLength;          //vertex buffer length
        public int vertexCount;           //vertex num
        public IntPtr normal;                //vertex normal
        public int normalLength;          //normal buffer length
        public int normalCount;           //normal num
        public IntPtr faces;                //triangle id
        public int facesLength;           //triangle buffer length
        public int facesCount;            //triangle num
    }

    public struct EZVIOChunkInfo
    {
        public int id_x;
        public int id_y;
        public int id_z;

        public int nv;
        public int sv;
        public int ni;
        public int si;
        public int a;
    }

    public struct EZVIOBackendIncrementalMesh
    {
        public EZVIOBackendMesh mesh;
        public IntPtr chunks;
        public int chunksCount;
        public int chunksLength;
    }
}
