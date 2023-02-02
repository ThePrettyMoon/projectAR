using System;
using UnityEngine;
using EZXR.Glass.SixDof;

namespace EZXR.Glass.SixDof
{
    public class EZXRTrackableMeshChunck : EZXRTrackable
    {

        private Vector3Int m_Vec3Id;

        private int m_UpdateCount;


        // TODO: @xuninghao
        // 增加接口通过Native提取mesh信息

        public EZXRTrackableMeshChunck()
        {
            this.m_Vec3Id = new Vector3Int(0, 0, 0);
            this.m_UpdateCount = -1;
        }

        public EZXRTrackableMeshChunck(EZVIOChunkInfo chunkinfo)
        {
            this.m_Vec3Id = new Vector3Int(chunkinfo.id_x, chunkinfo.id_y, chunkinfo.id_z);
            this.m_UpdateCount = chunkinfo.a;
        }

        public void UpdateWithChunkInfo(EZVIOChunkInfo chunkinfo)
        {
            this.m_UpdateCount = chunkinfo.a;
        }

        public Vector3Int vec3Id
        {
            get
            {
                return this.m_Vec3Id;
            }
        }

        public int updateCount
        {
            get
            {
                return this.m_UpdateCount;
            }
        }
    }

}