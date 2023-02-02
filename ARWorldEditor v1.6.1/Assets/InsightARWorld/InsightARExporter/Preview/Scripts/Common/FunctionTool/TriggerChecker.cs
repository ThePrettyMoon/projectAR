using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerChecker : MonoBehaviour
{
    public string targetName = "";

    public UnityEvent triggerEnter;
    public UnityEvent triggerStay;
    public UnityEvent triggerExit;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.name == targetName)
        {
            triggerEnter?.Invoke();
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (collider.name == targetName)
        {
            triggerStay?.Invoke();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == targetName)
        {
            triggerExit?.Invoke();
        }
    }
}
