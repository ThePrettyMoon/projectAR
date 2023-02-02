using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EZXR.Glass.Hand
{
    public class SpacialObject : MonoBehaviour
    {
        private static Dictionary<Collider, SpacialObject> All = new Dictionary<Collider, SpacialObject>();

        //TODO: 改为EventTrigger "Add Event"形式
        [SerializeField]
        private UnityEvent OnHandEnter;
        [SerializeField]
        private UnityEvent OnHandStay;
        [SerializeField]
        private UnityEvent OnHandExit;
        [SerializeField]
        private UnityEvent OnHandGrab;
        [SerializeField]
        private UnityEvent OnHandRelease;
        [SerializeField]
        private UnityEvent OnHandTriggerEnter;
        [SerializeField]
        private UnityEvent OnHandTriggerStay;
        [SerializeField]
        private UnityEvent OnHandTriggerExit;
        [SerializeField]
        private UnityEvent OnHandTriggerGrab;
        [SerializeField]
        private UnityEvent OnHandTriggerRelease;
        [SerializeField]
        private UnityEvent OnHandRayEnter;
        [SerializeField]
        private UnityEvent OnHandRayStay;
        [SerializeField]
        private UnityEvent OnHandRayExit;
        [SerializeField]
        private UnityEvent OnHandRayGrab;
        [SerializeField]
        private UnityEvent OnHandRayRelease;

        private void Awake()
        {
            gameObject.tag = "SpacialObject";
            All.Add(GetComponent<Collider>(), this);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region HandTriggerEvent
        public static void PerformOnHandTriggerEnter(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandTriggerEnter.Invoke();
                All[other].OnHandEnter.Invoke();
            }
        }

        public static void PerformOnHandTriggerStay(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandTriggerStay.Invoke();
                All[other].OnHandStay.Invoke();
            }
        }

        public static void PerformOnHandTriggerExit(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandTriggerExit.Invoke();
                All[other].OnHandExit.Invoke();
            }
        }

        public static void PerformOnHandTriggerGrab(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandTriggerGrab.Invoke();
                All[other].OnHandGrab.Invoke();
            }
        }

        public static void PerformOnHandTriggerRelease(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandTriggerRelease.Invoke();
                All[other].OnHandRelease.Invoke();
            }
        }
        #endregion

        #region HandRayEvent
        public static void PerformOnHandRayEnter(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandRayEnter.Invoke();
                All[other].OnHandEnter.Invoke();
            }
        }

        public static void PerformOnHandRayStay(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandRayStay.Invoke();
                All[other].OnHandStay.Invoke();
            }
        }

        public static void PerformOnHandRayExit(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandRayExit.Invoke();
                All[other].OnHandExit.Invoke();
            }
        }

        public static void PerformOnHandRayGrab(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandRayGrab.Invoke();
                All[other].OnHandGrab.Invoke();
            }
        }

        public static void PerformOnHandRayRelease(Collider other)
        {
            if (All.ContainsKey(other))
            {
                All[other].OnHandRayRelease.Invoke();
                All[other].OnHandRelease.Invoke();
            }
        }
        #endregion
    }
}