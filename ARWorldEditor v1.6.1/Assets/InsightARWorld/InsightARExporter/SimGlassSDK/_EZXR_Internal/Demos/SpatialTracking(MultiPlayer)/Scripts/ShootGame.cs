using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZXR.Glass.Hand;

public class ShootGame : MonoBehaviour
{
    public GameObject prefab_Bullet;
    Queue<GameObject> objs = new Queue<GameObject>();
    float timer_Right;
    public float force;

    void OnEnable()
    {
        //禁用手部物理
        ARHandManager.leftHand.DisablePhysicsInteraction();
        ARHandManager.rightHand.DisablePhysicsInteraction();

        //禁用默认的远距离和近距离交互
        ARHandManager.leftHand.DisableRaycastInteraction();
        ARHandManager.leftHand.DisableTouchInteraction();
        ARHandManager.rightHand.DisableRaycastInteraction();
        ARHandManager.rightHand.DisableTouchInteraction();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ARHandManager.rightHand.handExist && ARHandManager.rightHand.gestureType == GestureType.OpenHand)
        {
            if (Time.time - timer_Right > 0.5f)
            {
                //手掌方向和视线方向夹角为锐角，其实就是为了确保手掌朝前的时候才发射
                if (Vector3.Dot(ARHandManager.head.forward, ARHandManager.rightHand.palmNormal) > 0)
                {
                    Rigidbody obj = Instantiate(prefab_Bullet, ARHandManager.rightHand.palm.position + ARHandManager.rightHand.palmNormal * 0.1f, Quaternion.identity).GetComponent<Rigidbody>();
                    obj.AddForce(ARHandManager.rightHand.rayDirection * force);
                    objs.Enqueue(obj.gameObject);
                    SpatialTrackingMultiPlayer.Instance.AddToSync(obj.transform, prefab_Bullet);
                    if (objs.Count > 10)
                    {
                        Destroy(objs.Dequeue());
                    }
                }
                timer_Right = Time.time;
            }
        }
    }
}
