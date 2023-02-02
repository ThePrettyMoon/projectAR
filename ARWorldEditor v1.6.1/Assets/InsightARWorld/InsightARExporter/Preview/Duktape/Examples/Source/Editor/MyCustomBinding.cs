
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace MyProject
{
    using UnityEngine;
    using UnityEditor;
    using Duktape;
    
    public class MyCustomBinding : AbstractBindingProcess
    {
        public override void OnPreExporting(BindingManager bindingManager)
        {
            // 添加导出
            bindingManager.AddExportedType(typeof(List<String>));

            //bindingManager.AddExportedType(typeof(UnityEngine.Vector2));
            //bindingManager.AddExportedType(typeof(UnityEngine.Vector3));
            //  bindingManager.AddExportedType(typeof(UnityEngine.Quaternion));
            //bindingManager.AddExportedType(typeof(UnityEngine.GameObject), true);
            //  bindingManager.AddExportedType(typeof(UnityEngine.Mathf));
            //  bindingManager.AddExportedType(typeof(UnityEngine.PrimitiveType));
            //  bindingManager.AddExportedType(typeof(UnityEngine.Color));
            //  bindingManager.AddExportedType(typeof(UnityEngine.MonoBehaviour), true)
            //      .SetMemberBlocked("runInEditMode");
            // bindingManager.AddExportedType(typeof(UnityEngine.Transform), true);
            //  bindingManager.AddExportedType(typeof(UnityEngine.UI.Text), true)
            //     .SetMemberBlocked("OnRebuildRequested");
            //  bindingManager.AddExportedType(typeof(UnityEngine.UI.Graphic))
            //     .SetMemberBlocked("OnRebuildRequested");
            // bindingManager.AddExportedType(typeof(UnityEngine.UI.Button), true);
            // bindingManager.AddExportedType(typeof(UnityEngine.UI.Image), true);
            // bindingManager.AddExportedType(typeof(UnityEngine.Sprite), true);
            //  bindingManager.AddExportedType(typeof(UnityEngine.UI.Button.ButtonClickedEvent), true);
            // bindingManager.AddExportedType(typeof(UnityEngine.Random));
            // bindingManager.AddExportedType(typeof(UnityEngine.Camera), true);
            // bindingManager.AddExportedType(typeof(UnityEngine.Time));
            // bindingManager.AddExportedType(typeof(UnityEngine.KeyCode));
            //bindingManager.AddExportedType(typeof(UnityEngine.Profiling.CustomSampler));
            // bindingManager.AddExportedType(typeof(UnityEngine.Profiling.Profiler));
            // bindingManager.AddExportedType(typeof(UnityEngine.Input))
            //   .SetMemberBlocked("IsJoystickPreconfigured");
            // bindingManager.AddExportedType(typeof(UnityEngine.WaitForSeconds));
            // bindingManager.AddExportedType(typeof(UnityEngine.WaitForFixedUpdate));
            //bindingManager.AddExportedType(typeof(UnityEngine.WaitForEndOfFrame));

            // bindingManager.AddExportedType(typeof(Dictionary<String, String>))
            //     .SetMethodBlocked("Remove", typeof(string), typeof(string));
            //     ...

            //custom add by wy
            // bindingManager.AddExportedType(typeof(UnityEngine.Vector4));
            // bindingManager.AddExportedType(typeof(Insight.Matrix4));

            //  bindingManager.AddExportedType(typeof(Vector2Extension));
            //bindingManager.AddExportedType(typeof(Vector3Extension));
            //bindingManager.AddExportedType(typeof(Vector4Extension));
            //  bindingManager.AddExportedType(typeof(QuaternionExtension));

            //bindingManager.AddExportedType(typeof(UnityEngine.Bounds));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.Button));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.SpriteState));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.ColorBlock));
            //bindingManager.AddExportedType(typeof(UnityEngine.Canvas));
            //bindingManager.AddExportedType(typeof(UnityEngine.Collider));
            //bindingManager.AddExportedType(typeof(UnityEngine.Camera));
            //bindingManager.AddExportedType(typeof(System.DateTime));
            // bindingManager.AddExportedType(typeof(UnityEngine.Debug));


            //bindingManager.AddExportedType(typeof(UnityEngine.GUI));


            //bindingManager.AddExportedType(typeof(UnityEngine.Material));

            //bindingManager.AddExportedType(typeof(UnityEngine.SkinnedMeshRenderer));
            //bindingManager.AddExportedType(typeof(UnityEngine.Video.VideoPlayer));            
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.Text));
            //bindingManager.AddExportedType(typeof(UnityEngine.RectTransform));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.InputField));
            //bindingManager.AddExportedType(typeof(UnityEngine.Sprite));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.SpriteState));
            //bindingManager.AddExportedType(typeof(UnityEngine.PlayerPrefs));
            //bindingManager.AddExportedType(typeof(UnityEngine.Screen));
            //bindingManager.AddExportedType(typeof(UnityEngine.Time));

            //bindingManager.AddExportedType(typeof(Insight.Tracking));
            //bindingManager.AddExportedType(typeof(Insight.VideoRecorder));
            //bindingManager.AddExportedType(typeof(UnityEngine.GameObject));

            //test
            //  bindingManager.AddExportedType(typeof(InsightOld.Quaternion));
            // bindingManager.AddExportedType(typeof(Quaternion));
            //  bindingManager.AddExportedType(typeof(QuaternionExtension));

            // bindingManager.AddExportedType(typeof(InsightOld.Debug));
            // add by zhl
            #region
            //bindingManager.AddExportedType(typeof(UnityEngine.BoxCollider));
            //bindingManager.AddExportedType(typeof(BoxColliderExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.ColorBlock));
            //bindingManager.AddExportedType(typeof(ColorBlockExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.SpriteState));
            //bindingManager.AddExportedType(typeof(SpriteStateExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.Button));
            //bindingManager.AddExportedType(typeof(UIButtonExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Camera));
            //bindingManager.AddExportedType(typeof(Insight.CameraExtension));
            //bindingManager.AddExportedType(typeof(Insight.Tracking));
            //bindingManager.AddExportedType(typeof(Insight.TrackingResult));
            //bindingManager.AddExportedType(typeof(UnityEngine.Debug));
            //bindingManager.AddExportedType(typeof(InsightOld.Canvas));
            //bindingManager.AddExportedType(typeof(Insight.CanvasExtension));
            //bindingManager.AddExportedType(typeof(Insight.ColliderExtension));
            //bindingManager.AddExportedType(typeof(Insight.Dotween));
            //bindingManager.AddExportedType(typeof(Insight.Ease));
            //bindingManager.AddExportedType(typeof(Insight.EngineType));
            //bindingManager.AddExportedType(typeof(Insight.Event));
            //bindingManager.AddExportedType(typeof(Insight.ForceMode));
            //bindingManager.AddExportedType(typeof(UnityEngine.JointLimits));
            //bindingManager.AddExportedType(typeof(Insight.JointLimitsExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.JointMotor));
            //bindingManager.AddExportedType(typeof(Insight.JointMotorExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.HingeJoint));
            //bindingManager.AddExportedType(typeof(Insight.HingeJointExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.Image));
            //bindingManager.AddExportedType(typeof(Insight.ImageExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Input));
            //bindingManager.AddExportedType(typeof(Insight.Logo));
            //bindingManager.AddExportedType(typeof(Insight.LogoPosition));
            //bindingManager.AddExportedType(typeof(UnityEngine.Light));
            //bindingManager.AddExportedType(typeof(Insight.LightExtension));
            //bindingManager.AddExportedType(typeof(Insight.LoopType));
            //bindingManager.AddExportedType(typeof(Insight.NetworkReachability));
            //bindingManager.AddExportedType(typeof(Insight.OperatingSystemType));
            //bindingManager.AddExportedType(typeof(UnityEngine.ParticleSystem));
            //bindingManager.AddExportedType(typeof(Insight.ParticleSystemExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Physics));
            //bindingManager.AddExportedType(typeof(UnityEngine.PlayerPrefs));
            //bindingManager.AddExportedType(typeof(Insight.PlayerPrefs));
            //bindingManager.AddExportedType(typeof(UnityEngine.Ray));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.RawImage));
            //bindingManager.AddExportedType(typeof(Insight.RawImageExtension));
            //bindingManager.AddExportedType(typeof(Insight.TextureExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.RaycastHit));
            //bindingManager.AddExportedType(typeof(Insight.RaycastHitExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.RectTransform));
            //bindingManager.AddExportedType(typeof(Insight.RectTransformExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Screen));
            //bindingManager.AddExportedType(typeof(Insight.ScreenExtension));
            //bindingManager.AddExportedType(typeof(Insight.SkinnedMeshRendererExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.SphereCollider));
            //bindingManager.AddExportedType(typeof(Insight.SphereColliderExtension));
            //bindingManager.AddExportedType(typeof(Insight.SpriteExtension));
            //bindingManager.AddExportedType(typeof(Insight.SystemInfo));
            //bindingManager.AddExportedType(typeof(Insight.SystemInfoExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.Text));
            //bindingManager.AddExportedType(typeof(Insight.TextExtension));
            //bindingManager.AddExportedType(typeof(Insight.TextAlignment));
            //bindingManager.AddExportedType(typeof(Insight.TextDirection));
            //bindingManager.AddExportedType(typeof(Insight.TextFlag));
            //bindingManager.AddExportedType(typeof(UnityEngine.Time));
            //bindingManager.AddExportedType(typeof(Insight.Time));
            //bindingManager.AddExportedType(typeof(UnityEngine.Touch));
            //bindingManager.AddExportedType(typeof(Insight.TouchExtension));
            //bindingManager.AddExportedType(typeof(Insight.TouchPhase));
            //bindingManager.AddExportedType(typeof(UnityEngine.Video.VideoPlayer));
            //bindingManager.AddExportedType(typeof(Insight.VideoPlayerExtension));
            //bindingManager.AddExportedType(typeof(Insight.CanvasRendererExtension));
            //bindingManager.AddExportedType(typeof(Insight.DateTimeExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Material));
            //bindingManager.AddExportedType(typeof(Insight.MaterialExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.Rigidbody));
            //bindingManager.AddExportedType(typeof(Insight.RigidbodyExtension));
            //bindingManager.AddExportedType(typeof(Insight.InputField));
            //bindingManager.AddExportedType(typeof(Insight.InputFieldExtension));
            //bindingManager.AddExportedType(typeof(UnityEngine.UI.InputField));
            //bindingManager.AddExportedType(typeof(InsightOld.Button));
            //bindingManager.AddExportedType(typeof(UnityEngine.Networking.UnityWebRequest));

            //bindingManager.AddExportedType(typeof(Insight.InsightClient));
            //bindingManager.AddExportedType(typeof(Insight.InsightClientExtension));

            //bindingManager.AddExportedType(typeof(Insight.InsightRequest));
            //bindingManager.AddExportedType(typeof(Insight.InsightRequestExtension));

            //bindingManager.AddExportedType(typeof(UnityEngine.Renderer));
            //bindingManager.AddExportedType(typeof(Insight.PerformanceStatistics));
            //bindingManager.AddExportedType(typeof(Insight.Vector2));
            //bindingManager.AddExportedType(typeof(Insight.Quaternion));
            // bindingManager.AddExportedType(typeof(Insight.Vector4));
            bindingManager.AddExportedType(typeof(Insight.Matrix4x4));
            // bindingManager.AddExportedType(typeof(Insight.Quaternion));
            #endregion
        }

        public override void OnCleanup(BindingManager bindingManager)
        {
            Debug.Log($"finish @ {DateTime.Now}");
        }
    }
}
