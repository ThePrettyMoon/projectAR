using EZXR.Glass.SixDof;
using AOT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class StereoRenderer : MonoBehaviour
{
    private static DisplaySize ds = new DisplaySize();
    private static CamParams cp = new CamParams();


    // 解决屏幕的问题
    // 备用，防止Native渲染链路无法建立。
    private static int mainDisplayIdx = -1;
    private static int targetDisplayIdx = -1;

    /// <summary> Renders the event delegate described by eventID. </summary>
    /// <param name="eventID"> Identifier for the event.</param>
    private delegate void RenderEventDelegate(int eventID);
    /// <summary> Handle of the render thread. </summary>
    private static RenderEventDelegate RenderThreadHandle = new RenderEventDelegate(RunOnRenderThread);
    /// <summary> The render thread handle pointer. </summary>
    private static IntPtr RenderThreadHandlePtr = Marshal.GetFunctionPointerForDelegate(RenderThreadHandle);

    private const int SETRENDERTEXTUREEVENT = 0x0001;
    private const int STARTNATIVERENDEREVENT = 0x0002;
    private const int RESUMENATIVERENDEREVENT = 0x0003;
    private const int PAUSENATIVERENDEREVENT = 0x0004;
    private const int STOPNATIVERENDEREVENT = 0x0005;

    /// <summary> Gets or sets the native renderring. </summary>
    /// <value> The m native renderring. </value>
    private static EZXRNativeRenderring m_NativeRenderring;
    static EZXRNativeRenderring NativeRenderring
    {
        get
        {
            if (m_NativeRenderring == null)
            {
                m_NativeRenderring = new EZXRNativeRenderring();
            }

            return m_NativeRenderring;
        }
        set
        {
            m_NativeRenderring = value;
        }
    }


    /// <summary> Values that represent eyes. </summary>
    public enum Eyes
    {
        /// <summary> An enum constant representing the left option. </summary>
        Left = 0,
        /// <summary> An enum constant representing the right option. </summary>
        Right = 1,
        /// <summary> An enum constant representing the count option. </summary>
        Count = 2
    }

    private static int _TextureBufferSize = 4;
    /// <summary> Number of eye textures. </summary>
    private static int EyeTextureCount = _TextureBufferSize * (int)Eyes.Count;
    /// <summary> The eye textures. </summary>
    private RenderTexture[] eyeTextures;
    /// <summary> Dictionary of rights. </summary>
    private Dictionary<RenderTexture, IntPtr> m_RTDict = new Dictionary<RenderTexture, IntPtr>();

    /// <summary> Values that represent renderer states. </summary>
    public enum RendererState
    {
        UnInitialized,
        Initialized,
        InitializedFailed,
        Running,
        Paused,
        Destroyed
    }

    /// <summary> The current state. </summary>
    private RendererState m_CurrentState = RendererState.UnInitialized;

    /// <summary> Gets the current state. </summary>
    /// <value> The current state. </value>
    public RendererState currentState
    {
        get
        {
            return m_CurrentState;
        }
    }

    /// <summary> Values that represent renderer modes. </summary>
    public enum RenderMode
    {
        RenderInternal,
        RenderExternal
    }

    private int currentEyeTextureIdx = 0;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        Debug.Log("=============Unity Log===============   UICamController -- Awake   display length " + Display.displays.Length);

        for (int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

    private void Start()
    {
        if (!Application.isEditor)
        {
            StartCoroutine(StartUp());
        }
    }

    private void Update()
    {
        if (!Application.isEditor)
        {
            if (m_CurrentState == RendererState.Running && ARFrame.SessionStatus == EZVIOState.EZVIOCameraState_Tracking)
            {
                HMDPoseTracker.Instance.leftCamera.targetTexture = eyeTextures[currentEyeTextureIdx];
                HMDPoseTracker.Instance.rightCamera.targetTexture = eyeTextures[currentEyeTextureIdx + 1];
                currentEyeTextureIdx = ((currentEyeTextureIdx + 2) % EyeTextureCount + EyeTextureCount) % EyeTextureCount;

                Matrix4x4 leftProj = new Matrix4x4();
                Matrix4x4 rightProj = new Matrix4x4();
                for(int i=0;i<4;i++)
                    for(int j=0;j<4;j++)
                    {
                        leftProj[i, j] = cp.leftProjection[i * 4 + j];
                        rightProj[i, j] = cp.rightProjection[i * 4 + j];
                    }
                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  left proj "
                //    + leftProj[0, 0] + " " + leftProj[0, 1] + " " + leftProj[0, 2] + " " + leftProj[0, 3] + " "
                //     + leftProj[1, 0] + " " + leftProj[1, 1] + " " + leftProj[1, 2] + " " + leftProj[1, 3] + " "
                //      + leftProj[2, 0] + " " + leftProj[2, 1] + " " + leftProj[2, 2] + " " + leftProj[2, 3] + " "
                //       + leftProj[3, 0] + " " + leftProj[3, 1] + " " + leftProj[3, 2] + " " + leftProj[3, 3]);
                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  right proj "
                //    + rightProj[0, 0] + " " + rightProj[0, 1] + " " + rightProj[0, 2] + " " + rightProj[0, 3] + " "
                //     + rightProj[1, 0] + " " + rightProj[1, 1] + " " + rightProj[1, 2] + " " + rightProj[1, 3] + " "
                //      + rightProj[2, 0] + " " + rightProj[2, 1] + " " + rightProj[2, 2] + " " + rightProj[2, 3] + " "
                //       + rightProj[3, 0] + " " + rightProj[3, 1] + " " + rightProj[3, 2] + " " + rightProj[3, 3]);

                HMDPoseTracker.Instance.leftCamera.projectionMatrix = leftProj;
                HMDPoseTracker.Instance.rightCamera.projectionMatrix = rightProj;

                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  HMDPoseTracker.Instance.leftCamera.projectionMatrix "
                //    + HMDPoseTracker.Instance.leftCamera.projectionMatrix[0, 0] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[0, 1] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[0, 2] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[0, 3] + " "
                //     + HMDPoseTracker.Instance.leftCamera.projectionMatrix[1, 0] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[1, 1] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[1, 2] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[1, 3] + " "
                //      + HMDPoseTracker.Instance.leftCamera.projectionMatrix[2, 0] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[2, 1] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[2, 2] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[2, 3] + " "
                //       + HMDPoseTracker.Instance.leftCamera.projectionMatrix[3, 0] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[3, 1] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[3, 2] + " " + HMDPoseTracker.Instance.leftCamera.projectionMatrix[3, 3]);
                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  right proj "
                //    + HMDPoseTracker.Instance.rightCamera.projectionMatrix[0, 0] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[0, 1] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[0, 2] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[0, 3] + " "
                //     + HMDPoseTracker.Instance.rightCamera.projectionMatrix[1, 0] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[1, 1] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[1, 2] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[1, 3] + " "
                //      + HMDPoseTracker.Instance.rightCamera.projectionMatrix[2, 0] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[2, 1] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[2, 2] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[2, 3] + " "
                //       + HMDPoseTracker.Instance.rightCamera.projectionMatrix[3, 0] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[3, 1] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[3, 2] + " " + HMDPoseTracker.Instance.rightCamera.projectionMatrix[3, 3]);

                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  leftCamera.fieldOfView "
                //    + HMDPoseTracker.Instance.leftCamera.fieldOfView);
                //Debug.Log("=============Unity Log===============   StereoRenderer Update,  rightCamera.fieldOfView "
                //    + HMDPoseTracker.Instance.rightCamera.fieldOfView);
            }
        }
    }

    private void OnDestroy()
    {
        if (m_CurrentState == RendererState.Destroyed)
        {
            return;
        }

        //GL.IssuePluginEvent(RenderThreadHandlePtr, STOPNATIVERENDEREVENT);
        NativeRenderring?.Stop();
        NativeRenderring = null;

        m_CurrentState = RendererState.Destroyed;
    }

    /// <summary> Prepares this object for use. </summary>
    /// <returns> An IEnumerator. </returns>
    private IEnumerator StartUp()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        while(!NativeTracking.GetIsARSessionInited())
        {
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("=============Unity Log===============   UICamController -- StartUp   get started");

        NativeTracking.GetGlassDisplaySize(ref ds);
        NativeTracking.GetCameraParams(ref cp);

        Debug.Log("=============Unity Log===============   UICamController -- StartUp   desired glass size: " + ds.width + " " + ds.height);

        CreateRenderTextures();

        StartCoroutine(RenderCoroutine());

        m_CurrentState = RendererState.Running;
        GL.IssuePluginEvent(RenderThreadHandlePtr, STARTNATIVERENDEREVENT);
    }

    /// <summary> Pause render. </summary>
    public void Pause()
    {
        if (m_CurrentState != RendererState.Running)
        {
            return;
        }
        m_CurrentState = RendererState.Paused;
        GL.IssuePluginEvent(RenderThreadHandlePtr, PAUSENATIVERENDEREVENT);
    }

    /// <summary> Resume render. </summary>
    public void Resume()
    {
        Invoke("DelayResume", 0.3f);
    }

    /// <summary> Delay resume. </summary>
    private void DelayResume()
    {
        if (m_CurrentState != RendererState.Paused)
        {
            return;
        }
        m_CurrentState = RendererState.Running;
        GL.IssuePluginEvent(RenderThreadHandlePtr, RESUMENATIVERENDEREVENT);
    }

    /// <summary> Creates render textures. </summary>
    private void CreateRenderTextures()
    {
        EyeTextureCount = _TextureBufferSize * (int)Eyes.Count;
        eyeTextures = new RenderTexture[EyeTextureCount];
        for (int i = 0; i < EyeTextureCount; i++)
        {
            eyeTextures[i] = EZGlassARUtility.CreateRenderTexture(ds.width / 2, ds.height, 24, RenderTextureFormat.Default, false);  // default may ARGB
            m_RTDict.Add(eyeTextures[i], eyeTextures[i].GetNativeTexturePtr());
        }

        HMDPoseTracker.Instance.leftCamera.targetTexture = eyeTextures[0];
        HMDPoseTracker.Instance.rightCamera.targetTexture = eyeTextures[1];
    }

    /// <summary> Renders the coroutine. </summary>
    /// <returns> An IEnumerator. </returns>
    private IEnumerator RenderCoroutine()
    {
        WaitForEndOfFrame delay = new WaitForEndOfFrame();
        yield return delay;

        while (true)
        {
            yield return delay;

            //Debug.Log("=============Unity Log===============   UICamController -- RenderCoroutine  m_CurrentState " + m_CurrentState);
            //Debug.Log("=============Unity Log===============   UICamController -- RenderCoroutine  SessionStatus " + ARFrame.SessionStatus);

            if (m_CurrentState != RendererState.Running)
            {
                continue;
            }

            if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking)
            {
                continue;
            }


            double curSystimeS = NativeTracking.GetSystemTime() / 1e9;
            FrameInfo info = new FrameInfo(IntPtr.Zero, IntPtr.Zero, ARFrame.OriginVIOResult.Twc, ARFrame.OriginVIOResult.timestamp, curSystimeS, ARFrame.VIOResultFetchTimeNS / 1e9);

            IntPtr left_target, right_target;
            if (!m_RTDict.TryGetValue(HMDPoseTracker.Instance.leftCamera.targetTexture, out left_target)) continue;
            if (!m_RTDict.TryGetValue(HMDPoseTracker.Instance.rightCamera.targetTexture, out right_target)) continue;
            info.leftTex = left_target;
            info.rightTex = right_target;

            //Debug.Log("=============Unity Log===============   UICamController -- RenderCoroutine  before SetRenderFrameInfo");

            SetRenderFrameInfo(info);
        }
    }

    /// <summary> Sets render frame information. </summary>
    /// <param name="frame"> The frame.</param>
    private static void SetRenderFrameInfo(FrameInfo frame)
    {
        //Debug.Log("=============Unity Log===============   UICamController -- SetRenderFrameInfo frameinfo " + frame.leftTex + " " + frame.rightTex + " " + frame.Twc + " " + frame.TwcTime);

        NativeRenderring.WriteFrameData(frame);
        GL.IssuePluginEvent(RenderThreadHandlePtr, SETRENDERTEXTUREEVENT);
    }

    /// <summary> Executes the 'on render thread' operation. </summary>
    /// <param name="eventID"> Identifier for the event.</param>
    [MonoPInvokeCallback(typeof(RenderEventDelegate))]
    private static void RunOnRenderThread(int eventID)
    {
        if (eventID == STARTNATIVERENDEREVENT)
        {
            int result = NativeRenderring.Start();
            //int result = -1;
            Debug.Log("=============Unity Log===============   UICamController -- RunOnRenderThread STARTNATIVERENDEREVENT result: " + result);
        }
        else if (eventID == RESUMENATIVERENDEREVENT)
        {
            //NativeRenderring?.Resume();
            Debug.Log("=============Unity Log===============   UICamController -- RunOnRenderThread RESUMENATIVERENDEREVENT resumeMTP ");
#if EZXRCS
            NativeTracking.resumeMtp();
#endif
        }
        else if (eventID == PAUSENATIVERENDEREVENT)
        {
            Debug.Log("=============Unity Log===============   UICamController -- RunOnRenderThread PAUSENATIVERENDEREVENT pauseMtp ");
#if EZXRCS
            NativeTracking.pauseMtp();
#endif
            //NativeRenderring?.Pause();
        }
        else if (eventID == STOPNATIVERENDEREVENT)
        {
            //NativeRenderring?.Stop();
            //NativeRenderring = null;
        }
        else if (eventID == SETRENDERTEXTUREEVENT)
        {
            NativeRenderring?.DoExtendedRenderring();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (!Application.isEditor)
        {
            if (pause)
            {
                GL.IssuePluginEvent(RenderThreadHandlePtr, PAUSENATIVERENDEREVENT);
            }
            else // resume
            {
                GL.IssuePluginEvent(RenderThreadHandlePtr, RESUMENATIVERENDEREVENT);
            }
        }
    }

    public void OnClickReCenterButton()
    {
        Debug.Log("=============Unity Log===============   UICamController -- OnClickReCenterButton");
        if (ARFrame.SessionStatus != EZVIOState.EZVIOCameraState_Tracking)
            return;

        Debug.Log("=============Unity Log===============   UICamController -- Start ReCenter");
        ARFrame.ReCenter();
    }
}

