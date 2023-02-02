using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
#if ARMiracast
using Unity.RenderStreaming;
using Unity.RenderStreaming.Signaling;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace EZXR.Glass.MiraCast
{
    public class MiracastUI : MonoBehaviour
    {
#if ARMiracast
    InputField inputField;
    Text buttonText;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        inputField = transform.Find("InputField").GetComponent<InputField>();
        buttonText = transform.Find("Button/Text").GetComponent<Text>();

        path = Path.Combine(Application.persistentDataPath, "miracast.cfg");
        if (File.Exists(path))
        {
            inputField.text = File.ReadAllText(path);
            OnButtonClicked();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnButtonClicked()
    {
        if (buttonText.text == "Connect")
        {
            Run();
            buttonText.text = "DisConnect";
            File.WriteAllText(path, inputField.text);
        }
        else if (buttonText.text == "DisConnect")
        {
            Stop();
            buttonText.text = "Connect";
        }
    }

    void Run()
    {
        MiracastManager.Instance.renderStreaming.Run(true, CreateSignaling(typeof(WebSocketSignaling), "ws://" + inputField.text, 5, SynchronizationContext.Current));
    }

    void Stop()
    {
        MiracastManager.Instance.renderStreaming.Stop();
    }

    static ISignaling CreateSignaling(Type type, string url, float interval, SynchronizationContext context)
    {
        Debug.Log(type);
        Type _type = type;
        if (_type == null)
        {
            throw new ArgumentException($"Signaling type is undefined. {type}");
        }
        object[] args = { url, interval, context };
        return (ISignaling)Activator.CreateInstance(_type, args);
    }
#endif
    }
}