using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtension 
{
    public static bool getEnabled(this Component component)
    {
        Behaviour behaviour = component as Behaviour;
        if (behaviour != null)
        {
            return behaviour.enabled;
        }
        return true;
    }

    public static bool isActiveAndEnabled(this Component component)
    {
        Behaviour behaviour = component as Behaviour;
        if (behaviour != null)
        {
            return behaviour.enabled;
        }
        return true;
    }

    public static void setEnabled(this Component component,bool enabled)
    {
        Behaviour behaviour = component as Behaviour;
        if (behaviour != null)
        {
            behaviour.enabled = enabled;
        }
    }

    public static string getName(this Component component)
    {
        return component.name;
    }

    public static GameObject getOwner(this Component component)
    {
        return component.gameObject;
    }

    public static string getType(this Component component)
    {
        return component.GetType().ToString();
    }


}
