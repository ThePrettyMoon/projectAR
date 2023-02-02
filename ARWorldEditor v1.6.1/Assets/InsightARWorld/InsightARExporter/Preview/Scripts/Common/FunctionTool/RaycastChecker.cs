using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaycastChecker : MonoBehaviour
{
    public string targetName = "";
    public Camera camera;

    public UnityEvent triggerEnter;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin,ray.direction,out hit,1000000))
            {
                if (targetName == hit.collider.name)
                {
                    triggerEnter?.Invoke();
                }
            }
        }
    }
}
