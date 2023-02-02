using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARContentManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GlassContentManager.Instance.SetWristType(WristType.ReturnMainPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
