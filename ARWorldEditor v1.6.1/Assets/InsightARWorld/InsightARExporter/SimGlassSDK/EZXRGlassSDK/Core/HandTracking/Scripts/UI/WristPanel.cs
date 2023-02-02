using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.UI
{
    public class WristPanel : MonoBehaviour
    {
        public TextMesh battery;
        public TextMesh time;

        // Update is called once per frame
        void Update()
        {
            battery.text = SystemInfo.batteryLevel * 100 + "%";
            time.text = System.DateTime.Now.ToString("HH:mm");
        }
    }
}