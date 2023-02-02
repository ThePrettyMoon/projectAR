using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using NatSuite.Recorders.Inputs;
using UnityEngine;

namespace ARWorldEditor
{
    public class RecordARScene 
    {
        #region params
        public const int FRAME_COUNT = 30;
        private IMediaRecorder recorder;
        private CameraInput cameraInput;
        private AudioInput audioInput;

        private bool isRunning = false;
        #endregion

        #region custom functions
        public void StartRecording()
        {
            // Start recording
            var clock = new RealtimeClock();

            //audio listener 也需要重新初始化
            var audioListener = GameObject.FindObjectOfType<AudioListener>();
            if (audioListener == null) audioListener = Camera.main.gameObject.AddComponent<AudioListener>();

            string recordPath = NatSuite.Recorders.Internal.Utility.GetPath(@".mp4");

            int sampleRate = AudioSettings.outputSampleRate;
            int channelCount = (int)AudioSettings.speakerMode;
            recorder = new MP4Recorder(recordPath, Screen.width, Screen.height, FRAME_COUNT, sampleRate, channelCount);
            // Create recording inputs
            cameraInput = new CameraInput(recorder, clock, Camera.main);
            audioInput = new AudioInput(recorder, clock, audioListener);

            isRunning = true;
        }

        public async void StopRecording()
        {
            if (!isRunning) return;

            // Stop recording
            audioInput?.Dispose();
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");

            isRunning = false;
        }
        #endregion
    }
}
