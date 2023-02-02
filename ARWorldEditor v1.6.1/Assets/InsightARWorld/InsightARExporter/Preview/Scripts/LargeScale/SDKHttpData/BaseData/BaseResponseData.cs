using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseResponseData
{
    public string code;
    public string msg;
    public object result;


    public string GetCode() {
        return code;
    }

    public string GetMsg() {
        return msg;
    }

    public object GetResult() {
        return result;
    }
}
