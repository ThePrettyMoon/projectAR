using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public enum TextDirection
{
    Horizontal,
    Vertical
}

public class TextRenderer: MonoBehaviour
{
    public string text = "This is text.";
    private Canvas _canvas;
    private Text _text;
    private Camera _cam;

      
    // Use this for initialization
    void Start()
    {
        _canvas = this.GetComponentInChildren<Canvas>();
        _text = this.GetComponentInChildren<Text>();
        _cam = this.GetComponentInChildren<Camera>();

        _canvas.gameObject.SetActive(false);
        _cam.gameObject.SetActive(false);

        this.RenderText(text);
    }

    public void RenderText(string tex)
    {
        _text.text = tex;
        _cam.gameObject.SetActive(true);
        _canvas.gameObject.SetActive(true);
         
        _cam.Render();

        _cam.gameObject.SetActive(false);
        _canvas.gameObject.SetActive(false);
    }

	
    // Update is called once per frame
    void Update()
    {
    }

}
