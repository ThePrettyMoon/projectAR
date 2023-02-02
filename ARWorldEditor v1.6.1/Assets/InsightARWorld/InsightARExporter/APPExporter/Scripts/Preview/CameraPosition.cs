using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// 相机位置类
    /// </summary>
    public class CameraPosition
    {
        private Pose cvPose;
        private string imagePath;


        public Pose GetCVPose()
        {
            return cvPose;
        }

        public string GetImagePath()
        {
            return imagePath;
        }

        public long GetImageIndex()
        {
            string[] imagePaths = imagePath.Split(new char[] { '/', '.' });
            if (imagePaths == null || imagePaths.Length < 2) return 0;
            return long.Parse(imagePaths[imagePaths.Length - 2]);
        }


        public CameraPosition(Pose oriPose, string path)
        {
            this.cvPose = oriPose;
            this.imagePath = path;
        }

        /// <summary>
        /// 相机位置解析类
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isRotate"></param>
        /// <returns></returns>
        public static Dictionary<int, List<CameraPosition>> Parse(string path)
        {
            var camPosDict = new Dictionary<int, List<CameraPosition>>();

            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;

            foreach (string line in File.ReadLines(path))
            {
                if (line.Contains("#")) continue;

                string[] positionParameters = line.Split(new char[] { ' ' });

                int cameraId = int.Parse(positionParameters[8]);
                float qw = float.Parse(positionParameters[1]);
                float qx = float.Parse(positionParameters[2]);
                float qy = float.Parse(positionParameters[3]);
                float qz = float.Parse(positionParameters[4]);

                float tx = float.Parse(positionParameters[5]);
                float ty = float.Parse(positionParameters[6]);
                float tz = float.Parse(positionParameters[7]);

                Pose cvPose = new Pose(new Vector3(tx, ty, tz), new Quaternion(qx, qy, qz, qw));
                string imagePath = positionParameters[9];

                CameraPosition camPose = new CameraPosition(cvPose, imagePath);
                if (camPosDict.ContainsKey(cameraId))
                {
                    camPosDict[cameraId].Add(camPose);
                }
                else
                {
                    List<CameraPosition> list = new List<CameraPosition>();
                    list.Add(camPose);
                    camPosDict.Add(cameraId, list);
                }
            }

            //按照图像索引排序
            var dicCoroutine = camPosDict.GetEnumerator();
            while (dicCoroutine.MoveNext())
            {
                var key = dicCoroutine.Current.Key;
                var list = dicCoroutine.Current.Value;
                SortList(ref list);
            }

            return camPosDict;
        }

        /// <summary>
        /// list 排序
        /// </summary>
        /// <param name="positionDatas"></param>
        private static void SortList(ref List<CameraPosition> positionDatas)
        {
            positionDatas.Sort(delegate (CameraPosition data1, CameraPosition data2)
            {
                return data1.GetImageIndex().CompareTo(data2.GetImageIndex());
            });
        }


    }
}
