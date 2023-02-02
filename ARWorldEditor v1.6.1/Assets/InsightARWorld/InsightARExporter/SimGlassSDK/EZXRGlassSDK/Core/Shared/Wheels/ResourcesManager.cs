using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.Glass.Common
{
    public class ResourcesManager : MonoBehaviour
    {
        public static Dictionary<string, Object> objects = new Dictionary<string, Object>();

        public static T Load<T>(string path) where T : Object
        {
            if (!objects.ContainsKey(path))
            {
                objects.Add(path, Resources.Load<T>(path));
            }
            return objects[path] as T;
        }
    }
}