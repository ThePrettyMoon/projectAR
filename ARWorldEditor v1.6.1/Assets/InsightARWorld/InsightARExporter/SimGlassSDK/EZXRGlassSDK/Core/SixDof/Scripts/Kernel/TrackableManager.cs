using System;
using UnityEngine;
using System.Collections.Generic;
using EZXR.Glass.SixDof;
using System.Runtime.InteropServices;

namespace EZXR.Glass.SixDof
{
    public class TrackableManager
    {
        public TrackableManager()
        {

        }

        // for Planes
        // TODO: @xuninghao
        // warp EZVIOPlane to Local Style Plane Type PlaneTrackable
        private Dictionary<UInt64, EZVIOPlane> m_PlanesTrackable = new Dictionary<UInt64, EZVIOPlane>();

        public void freshPlanes()
        {
            m_PlanesTrackable.Clear();
        }

        public Dictionary<UInt64, EZVIOPlane> planesTrackable
        {
            get
            {
                return this.m_PlanesTrackable;
            }
        }

        public void UpdatePlane(EZVIOPlane plane)
        {
            EZVIOPlane tmp = plane;

            // about Recenter
            {
                Matrix4x4 tr = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    tr[i] = tmp.transform_unity[i];
                }
                Matrix4x4 tr_tp = tr.transpose;
                Matrix4x4 transformUnity_new = ARFrame.accumulatedRecenterOffset4x4 * tr_tp;
                Matrix4x4 transformUnity_new_tp = transformUnity_new.transpose;
                for (int i = 0; i < 16; i++)
                {
                    tmp.transform_unity[i] = transformUnity_new_tp[i];
                }
            }

            m_PlanesTrackable[(ulong)plane.id] = tmp;
        }
        
        // Planes是全量式维护，故直接对所有Planes进行Recenter。
        public void RecenterPlane()
        {
            foreach(var item in m_PlanesTrackable)
            {
                Matrix4x4 tr = new Matrix4x4();
                for (int i = 0; i < 16; i++)
                {
                    tr[i] = item.Value.transform_unity[i];
                }
                Matrix4x4 tr_tp = tr.transpose;
                Matrix4x4 transformUnity_new = ARFrame.recenterOffset4x4 * tr_tp;
                Matrix4x4 transformUnity_new_tp = transformUnity_new.transpose;
                for (int i = 0; i < 16; i++)
                {
                    item.Value.transform_unity[i] = transformUnity_new_tp[i];
                }
            }
        }

        // for Incremental Mesh
        private Dictionary<Vector3Int, EZXRTrackableMeshChunck> m_ChunksTrackable = new Dictionary<Vector3Int, EZXRTrackableMeshChunck>();

        // chunks to update
        private List<Vector3Int> m_ChunksToUpdate = new List<Vector3Int>();
        // vertexs to update 
        private List<Vector3[]> m_CachedVSToUpdate = new List<Vector3[]>();
        // triangles (faces index) to update
        private List<int[]> m_CachedTSToUpdate = new List<int[]>();
        private List<Vector3[]> m_CachedNMToUpdate = new List<Vector3[]>();

        public List<Vector3Int> chunksToUpdate
        {
            get
            {
                return m_ChunksToUpdate;
            }
        }

        public List<Vector3[]> cachedVSToUpdate
        {
            get
            {
                return m_CachedVSToUpdate;
            }
        }

        public List<int[] > cachedTSTupUpdate
        {
            get
            {
                return m_CachedTSToUpdate;
            }
        }

        public List<Vector3[]> cachedNMToUpdate
        {
            get
            {
                return m_CachedNMToUpdate;
            }
        }

        public void FreshMeshChunks()
        {
            m_ChunksTrackable.Clear();
            m_CachedVSToUpdate.Clear();
            m_CachedTSToUpdate.Clear();
            m_CachedNMToUpdate.Clear();

            m_ChunksToUpdate.Clear();
        }

        /// <summary>
        /// 确认对缓存的chunks，vs（vertexes），ts（triangles）的消耗后，释放相应的部分。
        /// </summary>
        /// <param name="n"> 消耗的chunks数量，并对前n个数量进行释放</param>
        public void CommitCachedChunksUse(int n)
        {
            if (n < 0)
                return;
            int count = Math.Min(m_ChunksToUpdate.Count, n);
            m_ChunksToUpdate.RemoveRange(0, count);
            m_CachedVSToUpdate.RemoveRange(0, count);
            m_CachedTSToUpdate.RemoveRange(0, count);
            m_CachedNMToUpdate.RemoveRange(0, count);
        }

        public void UpdateIncrementalMeshes(EZVIOBackendIncrementalMesh mesh)
        {
            // 提取chunkInfos
            int chunkNum = mesh.chunksCount;
            EZVIOChunkInfo[] chunkInfos = new EZVIOChunkInfo[chunkNum];
            IntPtr p = mesh.chunks;

            float[] vs_nativeChunk = new float[mesh.mesh.vertexCount * 3];
            int[] ts_nativeChunk = new int[mesh.mesh.facesCount * 3];
            float[] nm_nativeChunk = new float[mesh.mesh.normalCount * 3];
            Marshal.Copy(mesh.mesh.vertex, vs_nativeChunk, 0, mesh.mesh.vertexCount * 3);
            Marshal.Copy(mesh.mesh.faces, ts_nativeChunk, 0, mesh.mesh.facesCount * 3);
            Marshal.Copy(mesh.mesh.normal, nm_nativeChunk, 0, mesh.mesh.normalCount * 3);

            for (int i = 0; i < chunkNum; i++)
            {
                chunkInfos[i] = (EZVIOChunkInfo)Marshal.PtrToStructure(p, typeof(EZVIOChunkInfo));
                p += Marshal.SizeOf(typeof(EZVIOChunkInfo));
            }

            // 分析chunkInfos
            for (int i = 0; i<chunkNum; i++)
            {

                EZVIOChunkInfo queryChunk = chunkInfos[i];

                Vector3Int queryVec3Idx = new Vector3Int(queryChunk.id_x, queryChunk.id_y, queryChunk.id_z);

                if (m_ChunksTrackable.ContainsKey(queryVec3Idx))
                {
                    // 已有
                    if (m_ChunksTrackable[queryVec3Idx].updateCount >= queryChunk.a)
                    {
                        // 毋需更新
                        continue;
                    }
                    else
                    {
                        // 更新
                        m_ChunksTrackable[queryVec3Idx].UpdateWithChunkInfo(queryChunk);
                    }
                }
                else
                {
                    // 新增
                    m_ChunksTrackable.Add(queryVec3Idx, new EZXRTrackableMeshChunck(queryChunk));
                }

                Vector3[] vs_curChunk = new Vector3[queryChunk.nv];
                int[] ts_curChunk = new int[queryChunk.ni];
                Vector3[] nm_curChunk = new Vector3[queryChunk.nv];

                // 零碎拷贝，需调试
                //float[] vs_nativeChunk = new float[queryChunk.nv * 3];
                //int[] ts_nativeChunk = new int[queryChunk.ni];
                //Marshal.Copy(mesh.mesh.vertex, vs_nativeChunk, queryChunk.sv*3, queryChunk.nv*3);
                //Marshal.Copy(mesh.mesh.faces, ts_nativeChunk, queryChunk.si, queryChunk.ni);


                // TODO: @xuninghao
                // native解决好坐标系转换，unity层不做坐标转换。 可以节约时间和性能。
                for (int j = 0; j < queryChunk.nv; j++)
                {
                    Vector3 tmp = new Vector3(vs_nativeChunk[(queryChunk.sv + j) * 3 + 0], vs_nativeChunk[(queryChunk.sv + j) * 3 + 2], vs_nativeChunk[(queryChunk.sv + j) * 3 + 1]); // yz互换
                    //vs_curChunk[j] = new Vector3(vs_nativeChunk[(queryChunk.sv + j)* 3+0], vs_nativeChunk[(queryChunk.sv + j) * 3+2], vs_nativeChunk[(queryChunk.sv + j) * 3+1]); // yz互换

                    // about Recenter
                    vs_curChunk[j] = ARFrame.accumulatedRecenterOffset4x4.MultiplyPoint3x4(tmp);

                }
                for(int j = 0; j < queryChunk.ni/3; j++)
                {
                    ts_curChunk[j * 3 + 0] = ts_nativeChunk[queryChunk.si + j * 3 + 0];
                    ts_curChunk[j * 3 + 1] = ts_nativeChunk[queryChunk.si + j * 3 + 2];
                    ts_curChunk[j * 3 + 2] = ts_nativeChunk[queryChunk.si + j * 3 + 1];
                }
                for (int j = 0; j < queryChunk.nv; j++)
                {
                    nm_curChunk[j] = new Vector3(nm_nativeChunk[(queryChunk.sv + j) * 3 + 0], nm_nativeChunk[(queryChunk.sv + j) * 3 + 2], nm_nativeChunk[(queryChunk.sv + j) * 3 + 1]); // yz互换
                }

                m_ChunksToUpdate.Add(queryVec3Idx);
                m_CachedVSToUpdate.Add(vs_curChunk);
                m_CachedTSToUpdate.Add(ts_curChunk);
                m_CachedNMToUpdate.Add(nm_curChunk);
            }
        }

        // IncrementalMeshes不是全量式维护，故只能更新当前缓存着的，以及通知上一级去更新内容缓存。
        public void RecenterIncrementalMeshes()
        {
            foreach(var item in m_CachedVSToUpdate)
            {
                for(int i=0;i<item.Length;i++)
                {
                    Vector3 tmp = item[i];
                    item[i] = ARFrame.recenterOffset4x4.MultiplyPoint3x4(tmp);
                }
            }
            if (recenterIncrementalMeshesListener != null)
                recenterIncrementalMeshesListener(ARFrame.recenterOffset4x4);
        }

        public delegate void RecenterIncrementalMeshesListener(Matrix4x4 recenterOffset);
        public event RecenterIncrementalMeshesListener recenterIncrementalMeshesListener;
    }
}