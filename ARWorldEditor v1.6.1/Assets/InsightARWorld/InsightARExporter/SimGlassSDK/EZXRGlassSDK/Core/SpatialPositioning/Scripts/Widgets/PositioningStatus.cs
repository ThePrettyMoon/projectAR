using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.SixDof;
using UnityEngine.UI;

namespace EZXR.Glass.SpatialPositioning
{
    public class PositioningStatus : MonoBehaviour
    {
        public Text positioningStatus;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            positioningStatus.text = "Positioning Status : " + (SpatialPositioningManager.IsLocationSuccess ? "Success" : "Positioning...");
        }
    }
}