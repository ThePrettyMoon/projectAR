using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public interface IHttpRequestCreate
{
    UnityWebRequest GetWebRequest();

    void CreateWebRequest();

    void Dispose();
}
