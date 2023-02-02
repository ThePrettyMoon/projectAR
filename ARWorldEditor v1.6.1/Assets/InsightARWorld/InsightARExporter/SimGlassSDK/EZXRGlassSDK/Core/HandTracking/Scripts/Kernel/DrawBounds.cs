using EZXR.Glass.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public class DrawBounds : MonoBehaviour
    {
        /// <summary>
        /// 得到目标模型的Mesh
        /// </summary>
        public MeshFilter meshFilter;
        /// <summary>
        /// 用于实例化线的Prefab
        /// </summary>
        GameObject prefab_Line;
        /// <summary>
        /// （测试用）顶点Prefab
        /// </summary>
        GameObject prefab_Vertice;
        /// <summary>
        /// Bound的缩放系数
        /// </summary>
        public float scaleFactor = 1;
        /// <summary>
        /// Mesh.bounds的各个顶点在物体坐标系下的坐标，点序如下：
        ///    1    0
        /// 5    4
        ///    2    3
        /// 6    7
        /// </summary>
        Vector3[] localVertices = new Vector3[8];
        /// <summary>
        /// Mesh.bounds的各个顶点在世界坐标系下的坐标
        /// </summary>
        [HideInInspector]
        public Vector3[] vertices = new Vector3[8];
        /// <summary>
        /// 每个bound的长度
        /// </summary>
        float[] linesLength = new float[12];
        /// <summary>
        /// 每个bound的中点在物体坐标系下的坐标
        /// </summary>
        Vector3[] localMidPoints = new Vector3[12];
        /// <summary>
        /// 每个bound的中点在世界坐标系下的坐标
        /// </summary>
        [HideInInspector]
        public Vector3[] midPoints = new Vector3[12];
        /// <summary>
        /// 所有的线
        /// </summary>
        [HideInInspector]
        public Transform[] lines = new Transform[16];

        // Start is called before the first frame update
        void Awake()
        {
            if (meshFilter == null)
            {
                meshFilter = GetComponent<MeshFilter>();
            }
            if (prefab_Line == null)
            {
                prefab_Line = ResourcesManager.Load<GameObject>("DrawBounds/BoundLine");
            }
            if (prefab_Vertice == null)
            {
                prefab_Vertice = ResourcesManager.Load<GameObject>("DrawBounds/BoundVertice");
            }

            localVertices[0] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(meshFilter.mesh.bounds.extents.x, meshFilter.mesh.bounds.extents.y, meshFilter.mesh.bounds.extents.z));
            localVertices[1] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(-meshFilter.mesh.bounds.extents.x, meshFilter.mesh.bounds.extents.y, meshFilter.mesh.bounds.extents.z));
            localVertices[2] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(-meshFilter.mesh.bounds.extents.x, -meshFilter.mesh.bounds.extents.y, meshFilter.mesh.bounds.extents.z));
            localVertices[3] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(meshFilter.mesh.bounds.extents.x, -meshFilter.mesh.bounds.extents.y, meshFilter.mesh.bounds.extents.z));
            localVertices[4] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(meshFilter.mesh.bounds.extents.x, meshFilter.mesh.bounds.extents.y, -meshFilter.mesh.bounds.extents.z));
            localVertices[5] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(-meshFilter.mesh.bounds.extents.x, meshFilter.mesh.bounds.extents.y, -meshFilter.mesh.bounds.extents.z));
            localVertices[6] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(-meshFilter.mesh.bounds.extents.x, -meshFilter.mesh.bounds.extents.y, -meshFilter.mesh.bounds.extents.z));
            localVertices[7] = scaleFactor * (meshFilter.mesh.bounds.center + new Vector3(meshFilter.mesh.bounds.extents.x, -meshFilter.mesh.bounds.extents.y, -meshFilter.mesh.bounds.extents.z));

            vertices[0] = meshFilter.transform.TransformPoint(localVertices[0]);
            vertices[1] = meshFilter.transform.TransformPoint(localVertices[1]);
            vertices[2] = meshFilter.transform.TransformPoint(localVertices[2]);
            vertices[3] = meshFilter.transform.TransformPoint(localVertices[3]);
            vertices[4] = meshFilter.transform.TransformPoint(localVertices[4]);
            vertices[5] = meshFilter.transform.TransformPoint(localVertices[5]);
            vertices[6] = meshFilter.transform.TransformPoint(localVertices[6]);
            vertices[7] = meshFilter.transform.TransformPoint(localVertices[7]);
            //Instantiate(prefab_Vertice, vertices[0], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[1], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[2], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[3], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[4], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[5], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[6], Quaternion.identity);
            //Instantiate(prefab_Vertice, vertices[7], Quaternion.identity);

            localMidPoints[0] = (localVertices[0] + localVertices[1]) / 2.0f;
            localMidPoints[1] = (localVertices[1] + localVertices[2]) / 2.0f;
            localMidPoints[2] = (localVertices[2] + localVertices[3]) / 2.0f;
            localMidPoints[3] = (localVertices[3] + localVertices[0]) / 2.0f;
            localMidPoints[4] = (localVertices[4] + localVertices[5]) / 2.0f;
            localMidPoints[5] = (localVertices[5] + localVertices[6]) / 2.0f;
            localMidPoints[6] = (localVertices[6] + localVertices[7]) / 2.0f;
            localMidPoints[7] = (localVertices[7] + localVertices[4]) / 2.0f;
            localMidPoints[8] = (localVertices[0] + localVertices[4]) / 2.0f;
            localMidPoints[9] = (localVertices[1] + localVertices[5]) / 2.0f;
            localMidPoints[10] = (localVertices[2] + localVertices[6]) / 2.0f;
            localMidPoints[11] = (localVertices[3] + localVertices[7]) / 2.0f;

            midPoints[0] = meshFilter.transform.TransformPoint(localMidPoints[0]);
            midPoints[1] = meshFilter.transform.TransformPoint(localMidPoints[1]);
            midPoints[2] = meshFilter.transform.TransformPoint(localMidPoints[2]);
            midPoints[3] = meshFilter.transform.TransformPoint(localMidPoints[3]);
            midPoints[4] = meshFilter.transform.TransformPoint(localMidPoints[4]);
            midPoints[5] = meshFilter.transform.TransformPoint(localMidPoints[5]);
            midPoints[6] = meshFilter.transform.TransformPoint(localMidPoints[6]);
            midPoints[7] = meshFilter.transform.TransformPoint(localMidPoints[7]);
            midPoints[8] = meshFilter.transform.TransformPoint(localMidPoints[8]);
            midPoints[9] = meshFilter.transform.TransformPoint(localMidPoints[9]);
            midPoints[10] = meshFilter.transform.TransformPoint(localMidPoints[10]);
            midPoints[11] = meshFilter.transform.TransformPoint(localMidPoints[11]);

            linesLength[0] = prefab_Line.transform.localScale.x + (vertices[0] - vertices[1]).magnitude;
            linesLength[1] = prefab_Line.transform.localScale.x + (vertices[1] - vertices[2]).magnitude;
            linesLength[2] = prefab_Line.transform.localScale.x + (vertices[2] - vertices[3]).magnitude;
            linesLength[3] = prefab_Line.transform.localScale.x + (vertices[3] - vertices[0]).magnitude;
            linesLength[4] = prefab_Line.transform.localScale.x + (vertices[4] - vertices[5]).magnitude;
            linesLength[5] = prefab_Line.transform.localScale.x + (vertices[5] - vertices[6]).magnitude;
            linesLength[6] = prefab_Line.transform.localScale.x + (vertices[6] - vertices[7]).magnitude;
            linesLength[7] = prefab_Line.transform.localScale.x + (vertices[7] - vertices[4]).magnitude;
            linesLength[8] = prefab_Line.transform.localScale.x + (vertices[0] - vertices[4]).magnitude;
            linesLength[9] = prefab_Line.transform.localScale.x + (vertices[1] - vertices[5]).magnitude;
            linesLength[10] = prefab_Line.transform.localScale.x + (vertices[2] - vertices[6]).magnitude;
            linesLength[11] = prefab_Line.transform.localScale.x + (vertices[3] - vertices[7]).magnitude;

            for (int i = 0; i < linesLength.Length; i++)
            {
                lines[i] = Instantiate(prefab_Line, midPoints[i], Quaternion.identity).transform;
                lines[i].localScale = new Vector3(prefab_Line.transform.localScale.x, prefab_Line.transform.localScale.y, linesLength[i]);
                lines[i].parent = transform;
            }
            lines[0].LookAt(vertices[0], transform.up);
            lines[1].LookAt(vertices[1], transform.forward);
            lines[2].LookAt(vertices[2], transform.up);
            lines[3].LookAt(vertices[3], transform.forward);
            lines[4].LookAt(vertices[4], transform.up);
            lines[5].LookAt(vertices[5], transform.forward);
            lines[6].LookAt(vertices[6], transform.up);
            lines[7].LookAt(vertices[7], transform.forward);
            lines[8].LookAt(vertices[0], transform.up);
            lines[9].LookAt(vertices[1], transform.up);
            lines[10].LookAt(vertices[2], transform.up);
            lines[11].LookAt(vertices[3], transform.up);
        }
    }
}