using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public static class ButtonExtension
{
    public static ColorBlock colors(this Button button)
    {
        return ColorBlock.defaultColorBlock;
    }

    public static bool getInteractable(this Button button)
    {
        return button.interactable;
    }

    public static void setInteractable(this Button button, bool interactable)
    {
        if (button != null) button.interactable = interactable;
    }

    public static SpriteState spriteState(this Button button)
    {
        return button.spriteState;
    }

    /// <summary>
    /// 实现add click
    /// </summary>
    /// <param name="button"></param>
    public static void addClick(this Button button) {
       
    }

    public static bool getEnabled(this Button button)
    {

        return button.enabled;
    }

    public static void setEnabled(this Button button, bool enbled) {
        if (button != null) { button.enabled = enbled; }
    }

    public static GameObject gameObject(this Button button)
    {
        return button.gameObject;
    }

    public static bool isActiveAndEnabled(this Button button)
    {
        return button.gameObject.activeSelf;
    }

    public static string getName(this Button button)
    {
        return button.gameObject.name;
    }

    public static string getTag(this Button button)
    {
        return button.gameObject.tag;
    }

    public static Transform getTransform(this Button button)
    {
        return button.gameObject.transform;
    }

    public static string toString(this Button button)
    {
        return button.ToString();
    }
}
