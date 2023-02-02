using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public class RaycastHit
    {
        public string toString()
        {
            return string.Format("raycasthit " + "point " + point + " collider " + collider);
        }

        public Insight.Vector3 point
        {
            get; set;
        }

        public UnityEngine.Collider collider
        {
            get; set;
        }

        public float distance
        {
            get; set;
        }

        public Transform transform
        {
            get; set;
        }

        public void Copy(UnityEngine.RaycastHit hit)
        {
            this.point = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            this.distance = hit.distance;
            this.collider = hit.collider;
            this.transform = hit.transform;
        }

        public static Insight.RaycastHit FromRaycast(UnityEngine.RaycastHit hit)
        {
            Insight.RaycastHit raycastHit = new RaycastHit();
            raycastHit.point = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            raycastHit.distance = hit.distance;
            raycastHit.collider = hit.collider;
            raycastHit.transform = hit.transform;
            return raycastHit;
        }
    }
}
