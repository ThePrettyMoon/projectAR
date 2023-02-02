using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.SixDof
{
    public enum PlaneFindingMode
    {
        Disable,
        Enable,
    }

    public enum MeshFindingMode
    {
        Disable,
        Enable,
    }

    public enum PointCloudFindingMode
    {
        Disable,
        Enable,
    }

    public enum RelocalizationMode
    {
        Disable,
        RuntimeMap,
        PrebuiltMap,
    }

    public enum HandsFindingMode
    {
        Disable,
        Enable,
    }

    public enum MarkerFindingMode
    {
        Disable,
        Enable,
    }
    public class ARConfig
    {
        private static ARConfig defaultConfig;
        public static ARConfig DefaultConfig
        {
            get
            {
                if (defaultConfig == null)
                {
                    defaultConfig = new ARConfig();
                }
                return defaultConfig;
            }
        }

        private PlaneFindingMode mPlaneFindingMode;
        private MeshFindingMode mMeshFindingMode;
        private HandsFindingMode mHandsFindingMode;
        private PointCloudFindingMode mPointCloudFindingMode;
        private RelocalizationMode mRelocalizationMode;
        private MarkerFindingMode mMarkerFindingMode;
        private int mMTPMode;

        private ARConfig()
        {
            mPlaneFindingMode = PlaneFindingMode.Disable;
            mMeshFindingMode = MeshFindingMode.Disable;
            mHandsFindingMode = HandsFindingMode.Disable;
            mPointCloudFindingMode = PointCloudFindingMode.Disable;
            mRelocalizationMode = RelocalizationMode.Disable;
            mMarkerFindingMode = MarkerFindingMode.Disable;
            mMTPMode = (int)(MTPModeOptions.openMTP | MTPModeOptions.warping_1 | MTPModeOptions.singlebuffer);
        }

        public PlaneFindingMode PlaneFindingMode
        {
            set { mPlaneFindingMode = value; }
            get { return mPlaneFindingMode; }
        }
        public MeshFindingMode MeshFindingMode
        {
            set { mMeshFindingMode = value; }
            get { return mMeshFindingMode; }
        }
        public HandsFindingMode HandsFindingMode
        {
            set { mHandsFindingMode = value; }
            get { return mHandsFindingMode; }
        }
        public PointCloudFindingMode PointCloudFindingMode
        {
            set { mPointCloudFindingMode = value; }
            get { return mPointCloudFindingMode; }
        }
        public RelocalizationMode RelocalizationMode
        {
            set { mRelocalizationMode = value; }
            get { return mRelocalizationMode; }
        }
        public MarkerFindingMode MarkerFindingMode
        {
            set { mMarkerFindingMode = value; }
            get { return mMarkerFindingMode; }
        }

        public int MTPMode
        {
            get { return mMTPMode; }
        }

        /// <summary>
        /// seperate MTP mode option,
        /// </summary>
        public enum MTPModeOptions {
            /// <summary>
            /// the whole MTP mode open or not;
            /// if set mtp open disable, the rest of all options would be invalid.
            /// </summary>
            openMTP      = 0x01<<0,

            /// <summary>
            /// MTP use warping or not.
            /// </summary>
            warping_1    = 0x01<<1,

            /// <summary>
            /// MTP treat queuebuffer as single or not.
            /// </summary>
            singlebuffer = 0x01<<2
        };

        /// <summary>
        /// set: enable option; unset: disable option;  get: check option.
        /// </summary>
        public void setMTPMode_openMTP()   { mMTPMode |= (int)MTPModeOptions.openMTP; }
        public void unsetMTPMode_openMTP() { mMTPMode &= (~(int)MTPModeOptions.openMTP); }
        public bool getMTPMode_openMTP()   { return (mMTPMode & (int)MTPModeOptions.openMTP) != 0; }

        /// <summary>
        /// set: enable option; unset: disable option;  get: check option.
        /// </summary>
        public void setMTPMode_warping_1()   { mMTPMode |= (int)MTPModeOptions.warping_1; }
        public void unsetMTPMode_warping_1() { mMTPMode &= (~(int)MTPModeOptions.warping_1); }
        public bool getMTPMode_warping_1()   { return (mMTPMode & (int)MTPModeOptions.warping_1) != 0; }

        /// <summary>
        /// set: enable option; unset: disable option;  get: check option.
        /// </summary>
        public void setMTPMode_singlebuffer()   { mMTPMode |= (int)MTPModeOptions.singlebuffer; }
        public void unsetMTPMode_singlebuffer() { mMTPMode &= (~(int)MTPModeOptions.singlebuffer); }
        public bool getMTPMode_singlebuffer()   { return (mMTPMode & (int)MTPModeOptions.singlebuffer) != 0; }

    }
}
