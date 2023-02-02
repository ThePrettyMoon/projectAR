using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public static class JObjectUtility 
{
    /// <summary>
    /// parse string
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static string ParseJObjectString(JToken token)
    {
        return token != null ? token.ToString() : "";
    }

    /// <summary>
    /// parse bool
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static bool ParseJObjectBool(JToken token)
    {
       return ParseJObjectString(token) == "true";
    }

    /// <summary>
    /// parse int
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static int ParseJObjectInt(JToken token)
    {
        string str = ParseJObjectString(token);
        int result ;
        if(int.TryParse(str,out result))
        {
            return result;
        }
        else
        {
            return 0;
        };
    }

    /// <summary>
    /// parse float
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static float ParseJObjectFloat(JToken token)
    {
        string str = ParseJObjectString(token);
        float result ;
        if (float.TryParse(str, out result))
        {
            return result;
        }
        else
        {
            return 0.0f;
        };
    }

    /// <summary>
    /// parse double
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static double ParseJObjectDouble(JToken token)
    {
        string str = ParseJObjectString(token);
        double result ;
        if (double.TryParse(str, out result))
        {
            return result;
        }
        else
        {
            return 0.0;
        };
    }

    public static ulong ParseJObjectULong(JToken token)
    {
        string str = ParseJObjectString(token);
        ulong result;
        if ( ulong.TryParse(str, out result))
        {
            return result;
        }
        else
        {
            return 0;
        };
    }
}
