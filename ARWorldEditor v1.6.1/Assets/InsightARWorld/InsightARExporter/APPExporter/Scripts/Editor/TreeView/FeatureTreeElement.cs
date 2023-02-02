using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class FeatureTreeElement : TreeElement
{
    public string Name;
    public string Type;
    public bool isActive;

    public FeatureTreeElement(string name, int depth, int id) : base(name, depth, id)
    {
        isActive = true;
    }
}
