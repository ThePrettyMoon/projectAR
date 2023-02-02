using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestoryObjectOnLoad : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

