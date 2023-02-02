using System;
using Proj4Net;
using UnityEngine;

namespace ARWorldEditor
{
    /// <summary>
    /// utm 和wgs84坐标系转换
    /// </summary>
    public class CoordinateTransformUtility
    {
        #region params
        static readonly string pj_latlong_cmd = "+title=long/lat:WGS84 +proj=longlat +datum=WGS84 +no_defs";
        static readonly string pj_utm_cmd  = "+proj=utm +zone=51 +ellps=WGS84 +datum=WGS84 +units=m +no_defs";

        //utm組合
        static readonly string utm1 = "+proj=utm +zone=";
        static string utm2 = "51";// c6:51  
        static readonly string utm3 = " +ellps=WGS84 +datum=WGS84 +units=m +no_defs";

        private static bool verbose = false;
        private readonly static CoordinateTransformFactory ctFactory = new CoordinateTransformFactory();
        private readonly static CoordinateReferenceSystemFactory crsFactory = new CoordinateReferenceSystemFactory();

        private static CoordinateReferenceSystem pj_latlong = crsFactory.CreateFromParameters("WGS84", pj_latlong_cmd);
        private static CoordinateReferenceSystem pj_utm = crsFactory.CreateFromParameters("UTM", utm1+ utm2+utm3);

        private static ProjCoordinate srcCoord = new ProjCoordinate();
        private static ProjCoordinate destCoord = new ProjCoordinate();

        private static Matrix4x4 T_UW_SS = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 1));
        #endregion

        #region utm & wgs84
        /// <summary>
        /// wgs84 to utm
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void VertexFromWgs84ToUtm(double lon, double lat, ref double x, ref double y)
        {
            CheckTransform(pj_latlong, lon, lat, pj_utm, ref x, ref y, 0.0001);
        }

        /// <summary>
        /// utm to wgs84
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        public static void VertexFromUtmToWgs84(double x, double y, ref double lon, ref double lat)
        {
            CheckTransform(pj_utm, x, y, pj_latlong, ref lon, ref lat, 0.0001);
        }

        /// <summary>
        /// check transform
        /// </summary>
        /// <param name="srcCRS"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="tgtCRS"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        private static bool CheckTransform(
            CoordinateReferenceSystem srcCRS, double x1, double y1,
            CoordinateReferenceSystem tgtCRS, ref double x2, ref double y2,
            double tolerance)
        {
            var trans = ctFactory.CreateTransform(srcCRS, tgtCRS);

            srcCoord.X = x1;
            srcCoord.Y = y1;
            trans.Transform(srcCoord, destCoord);

            var dx = Math.Abs(destCoord.X - x2);
            var dy = Math.Abs(destCoord.Y - y2);
            var delta = Math.Max(dx, dy);

            x2 = destCoord.X;
            y2 = destCoord.Y;

            if (verbose)
            {
                Debug.LogFormat("{0}=>{1}", srcCRS.Name, tgtCRS.Name);
            }

            var isInTol = delta <= tolerance;

            if (verbose)
            {
                if (!isInTol)
                {
                    Debug.Log(" ... FAILED");
                    var source = srcCoord.ToShortString();
                    Debug.LogFormat("\t{0} -> {1}", source, destCoord.ToShortString());

                    var result = new ProjCoordinate(x2, y2);
                    var offset = new ProjCoordinate(destCoord.X - x2, destCoord.Y - y2);
                    Debug.LogFormat("\t{0}    {1},  (tolerance={2}, max delta={3})",
                        new string(' ', source.Length), result.ToShortString(),
                        tolerance, delta);
                    Debug.LogFormat("\tSource CRS: " + srcCRS.GetParameterString());
                    Debug.LogFormat("\tTarget CRS: " + tgtCRS.GetParameterString());
                }
                else
                    Debug.LogFormat(" ... PASSED");
            }
            return isInTol;
        }
        #endregion

        #region 点云坐标系和wgs84坐标系互转
        /// <summary>
        /// 点云坐标系转到wgs84坐标系
        /// </summary>
        /// <param name="t_geojson_feature"></param>
        /// <param name="local_pose"></param>
        /// <param name="utm_offset_east"></param>
        /// <param name="utm_offset_north"></param>
        /// <returns></returns>
        public static double[] ConvertCV3DPoseTo2DMapPoseByTransform(Matrix4x4 t_geojson_feature, Matrix4x4 local_pose
            , double utm_offset_east, double utm_offset_north)
        {
            double[] geojson_position = new double[] { 0, 0, 0, 1 };
            double[] feature_position = new double[] { local_pose.m03, local_pose.m13, local_pose.m23, 1 };

            GetGeoJsonPositionByFeaturePosition(t_geojson_feature, feature_position, ref geojson_position);
            double x_utm = geojson_position[0] + utm_offset_east;
            double y_utm = geojson_position[1] + utm_offset_north;
            double lon = 0, lat = 0;
            VertexFromUtmToWgs84(x_utm, y_utm, ref lon, ref lat);
            return new double[] { lon, lat, 0 };
        }

        /// <summary>
        /// wgs84坐标系转到3D点云坐标系
        /// </summary>
        /// <param name="t_geojson_feature"></param>
        /// <param name="global_pose"></param>
        /// <param name="utm_offset_east"></param>
        /// <param name="utm_offset_north"></param>
        /// <returns></returns>
        public static double[] Convert2DMapPoseTo3DPoseByTransform(Matrix4x4 t_geojson_feature, double[] global_pose,
            double utm_offset_east, double utm_offset_north)
        {
            double x_utm = 0, y_utm = 0;
            VertexFromWgs84ToUtm(global_pose[0], global_pose[1], ref x_utm, ref y_utm);
            double[] feature_position = new double[] { 0, 0, 0, 1 };
            double[] geojson_position = new double[] { 0, 0, 0, 1 };
            geojson_position[0] = x_utm - utm_offset_east;
            geojson_position[1] = y_utm - utm_offset_north;
            geojson_position[2] = 0;
            geojson_position[3] = 1;
            Matrix4x4 t_feature_geojson = Matrix4x4.Inverse(t_geojson_feature);
            GetFeaturePositionByGeojsonPosition(t_feature_geojson, geojson_position, ref feature_position);
            double[] local_pose = new double[3] { 0, 0, 0 };
            local_pose[0] = feature_position[0];
            local_pose[1] = feature_position[1];
            local_pose[2] = 0;
            return local_pose;
        }

        /// <summary>
        /// VIO转UTM
        /// </summary>
        /// <param name="t_geojson_feature"></param>
        /// <param name="feature_position"></param>
        /// <param name="geojson_position"></param>
        private static void GetGeoJsonPositionByFeaturePosition(Matrix4x4 t_geojson_feature, double[] feature_position, ref double[] geojson_position)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    geojson_position[i] += t_geojson_feature[i, j] * feature_position[j];
                }
            }
        }

        /// <summary>
        /// UTM转VIO
        /// </summary>
        /// <param name="t_feature_geojson"></param>
        /// <param name="geojson_position"></param>
        /// <param name="feature_position"></param>
        private static void GetFeaturePositionByGeojsonPosition(Matrix4x4 t_feature_geojson, double[] geojson_position, ref double[] feature_position)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    feature_position[i] += t_feature_geojson[i, j] * geojson_position[j];
                }
            }
        }
        #endregion


        #region unity 坐标系和点云坐标系
        /// <summary>
        /// cv 坐标系转到unity坐标系
        /// cv 本体坐标系x轴朝前，z朝上，y朝右
        /// cv 世界坐标系x朝左，z朝上，y朝前
        /// unity 世界和相机坐标系，x朝右，y朝上，z朝前
        /// </summary>
        public static Matrix4x4 ConvertCV3DPoseToUnity3DPose(Matrix4x4 cvPose)
        {
            //Matrix4x4 T_UW_SS = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 1));
            // Matrix4x4 T_P_UC = new Matrix4x4(new Vector4(0, -1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(1, 0, 0, 0), new Vector4(0, 0, 0, 1));
            Matrix4x4 T_P_UC = T_UW_SS;
            Matrix4x4 T_UW_UC = T_UW_SS * cvPose * T_P_UC;
            return T_UW_UC;
        }

        /// <summary>
        /// unity坐标系转到cv坐标系
        /// </summary>
        /// cv 坐标系转到unity坐标系
        /// cv 本体坐标系x轴朝前，z朝上，y朝右
        /// cv 世界坐标系x朝左，z朝上，y朝前
        /// unity 世界和相机坐标系，x朝右，y朝上，z朝前
        public static Matrix4x4 ConverUnity3DPoseToCV3DPose(Matrix4x4 u3dPose)
        {
            // Matrix4x4 T_SS_UW = new Matrix4x4(new Vector4(1, 0, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 1));
            // Matrix4x4 T_UC_P = new Matrix4x4(new Vector4(0, 0, 1, 0), new Vector4(-1, 0, 0, 0), new Vector4(0, 1, 0, 0), new Vector4(0, 0, 0, 1));
            Matrix4x4 T_UC_P = T_UW_SS;
            Matrix4x4 T_SS_UW = Matrix4x4.Inverse(T_UW_SS);
            Matrix4x4 T_SS_P = T_SS_UW * u3dPose * T_UC_P;
            return T_SS_P;
        }
        #endregion

        #region 设置utm
        public static void SetUTMZone(int utmZone)
        {
            utm2 = utmZone.ToString();
            pj_utm = crsFactory.CreateFromParameters("UTM", utm1 + utm2 + utm3);
        }
        #endregion
    }
}
