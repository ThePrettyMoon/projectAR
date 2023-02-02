using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    bool start;
    public Transform a;
    public Transform b;
    public Transform target;
    public Transform parent;
    Quaternion originRotation;
    Quaternion originLocalRotation;
    Vector3 originDirection;
    Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        originRotation = target.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            originDirection = b.position - a.position;
            offset = target.position - parent.position;
            Transform temp = transform.parent;
            transform.parent = parent;
            originLocalRotation = transform.localRotation;
            transform.parent = temp;
            start = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            start = false;
        }
        if (start)
        {
            target.position = parent.position + offset;
            target.rotation = Quaternion.FromToRotation(originDirection, b.position - a.position) * (parent.rotation * originLocalRotation);
        }
    }
}
