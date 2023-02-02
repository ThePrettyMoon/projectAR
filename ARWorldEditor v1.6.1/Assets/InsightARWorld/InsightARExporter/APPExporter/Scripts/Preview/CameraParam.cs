using System.Collections.Generic;
using System.IO;

namespace ARWorldEditor
{
    /// <summary>
    /// 相机内参类
    /// </summary>
    public class CameraParam
    {
        private int imageWidth;
        private int imageHeight;
        private double fx;
        private double fy;
        private double cx;
        private double cy;
        private int cameraId;

        public int GetWidth()
        {
            return imageWidth;
        }

        public int GetHeight()
        {
            return imageHeight;
        }

        public double GetFx()
        {
            return fx;
        }

        public double GetFy()
        {
            return fy;
        }

        public double GetCx()
        {
            return cx;
        }

        public double GetCy()
        {
            return cy;
        }

        public int GetCameraId()
        {
            return cameraId;
        }


        public CameraParam(int width, int height, double fx, double fy, double cx, double cy,int cameraId)
        {
            this.imageWidth = width;
            this.imageHeight = height;
            this.fx = fx;
            this.fy = fy;
            this.cx = cx;
            this.cy = cy;
            this.cameraId = cameraId;
        }

        /// <summary>
        /// 相机参数解析类
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<CameraParam> Parse(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return null;
            List<CameraParam> paramList = new List<CameraParam>();

            foreach (string line in File.ReadLines(path))
            {
                if (line.Contains("#")) continue;

                string[] cameraParameters = line.Split(new char[] { ' ' });
                int cameraId = int.Parse(cameraParameters[0]);
                int width = int.Parse(cameraParameters[2]);
                int height = int.Parse(cameraParameters[3]);

                double fx = double.Parse(cameraParameters[4]);
                double fy = double.Parse(cameraParameters[4]);

                double cx = double.Parse(cameraParameters[5]);
                double cy = double.Parse(cameraParameters[6]);

                var cameraParam = new CameraParam(width, height, fx, fy, cx, cy, cameraId);
                paramList.Add(cameraParam);
            }
            return paramList;
        }

        /// <summary>
        /// 返回相机id
        /// </summary>
        /// <param name="cameraParams"></param>
        /// <returns></returns>
        public static List<int> GetCameraIds(List<CameraParam> cameraParams)
        {
            if (cameraParams == null || cameraParams.Count == 0) return null;
            List<int> list = new List<int>();
            for (int i = 0; i < cameraParams.Count; i++)
            {
                list.Add(cameraParams[i].GetCameraId());
            }
            return list;
        }
    }
}
    
