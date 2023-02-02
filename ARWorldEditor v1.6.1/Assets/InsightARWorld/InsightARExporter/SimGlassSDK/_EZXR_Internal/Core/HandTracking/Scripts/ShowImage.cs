using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    public RawImage image;
    public Camera myCamera;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (myCamera.targetTexture != null)
        {
            image.texture = myCamera.targetTexture;
        }
    }
}
