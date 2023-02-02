using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoxColliderExtension
{

    public static bool isTrigger(this BoxCollider boxCollider) {

        return boxCollider.isTrigger;
    }

    public static string toString(this BoxCollider boxCollider) {
        return boxCollider.toString();
    }

    public static bool getEnabled(this BoxCollider boxCollider) {
        return boxCollider.enabled;
    }

    public static void setEnabled(this BoxCollider boxCollider, bool enabled)
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = enabled;
        }
    }

    public static GameObject gameObject(this BoxCollider boxCollider) {
        return boxCollider.gameObject;
    }


    public static bool isActiveAndEnabled(this BoxCollider boxCollider)
    {
        return boxCollider.gameObject.activeSelf;
    }

    public static string getName(this BoxCollider boxCollider)
    {
        return boxCollider.gameObject.name;
    }

    public static string getTag(this BoxCollider boxCollider)
    {
        return boxCollider.gameObject.tag;
    }

    public static Transform getTransform(this BoxCollider boxCollider)
    {
        return boxCollider.gameObject.transform;
    }

}
