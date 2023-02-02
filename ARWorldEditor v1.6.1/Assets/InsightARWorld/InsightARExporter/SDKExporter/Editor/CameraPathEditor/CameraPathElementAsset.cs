using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[CreateAssetMenu(fileName = "TreeDataAsset", menuName = "Tree Asset", order = 1)]
public class CameraPathElementAsset : ScriptableObject
{
	[SerializeField] List<CameraPathElement> m_TreeElements = new List<CameraPathElement>();
	internal List<CameraPathElement> treeElements
	{
		get { return m_TreeElements; }
		set { m_TreeElements = value; }
	}
}

static class CameraPathElementAssetFactory
{
    public static CameraPathElementAsset CreateCameraPathElementAsset()
    {
        CameraPathElementAsset asset = ScriptableObject.CreateInstance<CameraPathElementAsset>();
        var scene = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene();
        var path = scene.path.Substring(0,scene.path.Length - (scene.name.Length+6));
        Debug.Log(path);
        //检查路径
        path = path + "/Path";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fullPath = path + "/" + "TestData.asset";
        UnityEditor.AssetDatabase.DeleteAsset(fullPath);
        UnityEditor.AssetDatabase.CreateAsset(asset, fullPath);
        return asset;

    }
}






static class CameraPathElementGenerator
{
    static int IDCounter;
    static int minNumChildren = 5;
    static int maxNumChildren = 10;
    static float probabilityOfBeingLeaf = 0.5f;

    public static List<CameraPathElement> InitGenerateRandomTree()
    {
        int numTotalElements = 3;
        var treeElements = new List<CameraPathElement>(numTotalElements);
        IDCounter = 0;
        int allowedDepth = 0;
        var root = new CameraPathElement("Root", -1, IDCounter);
        treeElements.Add(root);
        var child1 = new CameraPathElement("Point " + 1, allowedDepth, ++IDCounter);
        var child2 = new CameraPathElement("Point " + 1, allowedDepth, ++IDCounter);
        var child3 = new CameraPathElement("Point " + 1, allowedDepth, ++IDCounter);
        treeElements.Add(child1);
        treeElements.Add(child2);
        treeElements.Add(child3);
        return treeElements;
    }

    public static List<CameraPathElement> GenerateRandomTree(int numTotalElements)
    {
        int numRootChildren = numTotalElements / 4;
        IDCounter = 0;
        var treeElements = new List<CameraPathElement>(numTotalElements);

        var root = new CameraPathElement("Root", -1, IDCounter);
        treeElements.Add(root);
        for (int i = 0; i < numRootChildren; ++i)
        {
            int allowedDepth = 0;
            AddChildrenRecursive(root, Random.Range(minNumChildren, maxNumChildren), true, numTotalElements, ref allowedDepth, treeElements);
        }

        return treeElements;
    }
    static void AddChildrenRecursive(TreeElement element, int numChildren, bool force, int numTotalElements, ref int allowedDepth, List<CameraPathElement> treeElements)
    {
        if (element.depth >= allowedDepth)
        {
            allowedDepth = 0;
            return;
        }

        for (int i = 0; i < numChildren; ++i)
        {
            if (IDCounter > numTotalElements)
                return;

            var child = new CameraPathElement("Point " + IDCounter, element.depth + 1, ++IDCounter);
            treeElements.Add(child);

            if (!force && Random.value < probabilityOfBeingLeaf)
                continue;

            AddChildrenRecursive(child, Random.Range(minNumChildren, maxNumChildren), false, numTotalElements, ref allowedDepth, treeElements);
        }
    }
}

