using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Hand
{
    public class HandRayDrawer : MonoBehaviour
    {
        HandInfo handInfo;
        /// <summary>
        /// 手指关节点0
        /// </summary>
        public Transform joint0;
        /// <summary>
        /// 手指关节点1
        /// </summary>
        public Transform joint1;
        /// <summary>
        /// 线
        /// </summary>
        public LineRenderer lineRenderer;
        /// <summary>
        /// 实线和虚线的material
        /// </summary>
        public Material[] materials;
        ///// <summary>
        ///// 线总长度
        ///// </summary>
        //public float length = 5f;
        /// <summary>
        /// 单线段长度
        /// </summary>
        public float eachLength = 0.01f;
        /// <summary>
        /// 是否是实线
        /// </summary>
        bool solidLine;
        /// <summary>
        /// 贝塞尔曲线的p2点
        /// </summary>
        Vector3 p2;
        Vector3 p0;
        Vector3 p1;

        // Start is called before the first frame update
        void Start()
        {

        }

        /// <summary>
        /// 重置lineRenderer为直线
        /// </summary>
        void ResetPositions()
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, new Vector3(0, 0, eachLength * (lineRenderer.positionCount - 1 - i)));
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (joint0 != null)
            {
                //Debug.Log(handInfo.curRayContactingPoint.ToString());
                if (!handInfo.IsRayGrabbing)
                {
                    p2 = joint1.position;
                }

                p0 = joint0.position;
                p1 = joint1.position;
                transform.position = joint0.position;

                lineRenderer.positionCount = Mathf.Clamp(Mathf.CeilToInt(Vector3.Magnitude(joint1.position - joint0.position) / eachLength), 2, 10000);

                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    lineRenderer.SetPosition(i, CalculateBezier(1 - ((float)i / (lineRenderer.positionCount - 1))));
                }
            }
        }

        Vector3 CalculateBezier(float t)
        {
            Vector3 p0p1 = (1 - t) * p0 + t * p1;
            Vector3 p1p2 = (1 - t) * p1 + t * p2;
            Vector3 result = (1 - t) * p0p1 + t * p1p2;
            return result;
        }

        public void SetUp(HandInfo handInfo, Transform joint0, Transform joint1)
        {
            this.handInfo = handInfo;
            this.joint0 = joint0;
            this.joint1 = joint1;

            Gradient gradient = new Gradient();
            gradient.mode = GradientMode.Blend;
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(0, 0.0f), new GradientAlphaKey(0.2f, 0.75f), new GradientAlphaKey(1, 1.0f) }
            );
            lineRenderer.colorGradient = gradient;

            //lineRenderer.positionCount = Mathf.CeilToInt(length / eachLength);

            Vector3[] positions = new Vector3[lineRenderer.positionCount];
            for (int i = 0; i < positions.Length; i++)
            {
                positions[i] = new Vector3(0, 0, eachLength * (positions.Length - 1 - i));
            }
            lineRenderer.SetPositions(positions);
        }

        /// <summary>
        /// 设置线的状态是实线还是虚线
        /// </summary>
        /// <param name="solidLine"></param>
        public void SetLineState(bool solidLine)
        {
            this.solidLine = solidLine;
            if (solidLine)
            {
                lineRenderer.material = materials[0];
                lineRenderer.textureMode = LineTextureMode.Stretch;
            }
            else
            {
                //ResetPositions();
                lineRenderer.material = materials[1];
                lineRenderer.textureMode = LineTextureMode.RepeatPerSegment;
            }
        }

        /// <summary>
        /// 设置贝塞尔曲线用的p2点（最终显示的曲线的终点，是滤波之后的点），p0是射线的起点，p1是贝塞尔曲线用到的折弯点（射线的终点）
        /// </summary>
        /// <param name="p2"></param>
        public void SetLineBezierPoint(Vector3 p2)
        {
            this.p2 = p2;
        }
    }
}