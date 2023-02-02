using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public class VideoRecorder
    {
      public bool isRecording
        {
            get;
        }

        public float time
        {
            get;
        }

        public void Pause()
        {

        }

        public void Resume()
        {

        }

        public void Stop()
        {

        }

        /// dest_filename : string 文件存储路径
        /// source_camera : Insight.Camera 录制源相机
        /// source_andio : number 声音录制选项（0 - 不录制声音；1 - 录制系统声音； 2 - 录制麦克风声音；3 - 录制系统和麦克风声音）
        public static VideoRecorder Record(string dest_filename, Camera camera, int source_audio)
        {
            return new VideoRecorder();
        }

        /// <summary>
        /// dest_filename : string  文件存储路径
        /// texture_name : string 录制纹理路径
        /// source_andio : number 声音录制选项（0 - 不录制声音；1 - 录制系统声音； 2 - 录制麦克风声音；3 - 录制系统和麦克风声音）
        /// </summary>
        /// <param name="dest_filename"></param>
        /// <param name="camera"></param>
        /// <param name="source_audio"></param>
        /// <returns></returns>
        public static VideoRecorder Record(string dest_filename, string source_texture_name, int source_audio)
        {
            return new VideoRecorder();
        }
    }
}
