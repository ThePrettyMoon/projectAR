#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFrustum : MonoBehaviour
{
    private float FOV = 60;
    private float maxDis = 0.6f;
    private float minDis = 0.3f;
    private float wid_Height_Aspect = 2;
    public string text = "";
    public string selectText = "";



    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == this.gameObject)
        {
           
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, FOV, maxDis*2, minDis, wid_Height_Aspect);
            UnityEditor.Handles.Label(transform.position, selectText);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, FOV, maxDis, minDis, wid_Height_Aspect);

            UnityEditor.Handles.color = Color.white;
            //UnityEditor.Handles.Label(transform.position, text);
        }
    }
}
#endif
