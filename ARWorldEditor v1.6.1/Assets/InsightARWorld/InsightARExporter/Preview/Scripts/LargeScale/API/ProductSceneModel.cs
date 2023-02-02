using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum ProductSceneMode
{
    PRODUCT_SCENE_MODE_NONE = 0,
    PRODUCT_SCENE_MODE_MAIN = 1,
    PRODUCT_SCENE_MODE_LANDMARK = 2,
    PRODUCT_SCENE_MODE_POI = 3
}

/// <summary>
/// 场景数据
/// </summary>
public class ProductSceneModel
{
    #region params
    private string sceneName;
    private int productId;
    private ArProduct arProduct;
    //存储material路径
    private Dictionary<ProductMaterialType, string> materialPathDict = new Dictionary<ProductMaterialType, string>();
    private SceneParser sceneParser;

    private string productFileRoot;
    #endregion

    #region custom function
    /// <summary>
    /// product scene model
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name="product"></param>
    /// <param name="sceneMode"></param>
    public ProductSceneModel( ArProduct product, string fileRoot)
    {
        this.arProduct = product;
        this.productId = product.Cid;
        this.productFileRoot = fileRoot;

        this.InitMaterialPath();

        this.sceneParser = new SceneParser();
        //scene数据解析
        this.ParseScene();
    }

    /// <summary>
    /// get scene parser
    /// </summary>
    /// <returns></returns>
    public SceneParser GetSceneParse()
    {
        return sceneParser;
    }

    /// <summary>
    /// 场景名称
    /// </summary>
    /// <returns></returns>
    public string GetSceneName()
    {
        return sceneName;
    }

    private void InitMaterialPath()
    {
        if (arProduct == null) return;
        List<ProductMaterial> materialList = arProduct.ProductMaterials;
        if (materialList == null || materialList.Count == 0) return;
        for(int i = 0; i < materialList.Count; i++)
        {
            AddOrUpdateMaterialPath((ProductMaterialType) materialList[i].Type);
        }
    }

    /// <summary>
    /// 添加或者更新Material Path
    /// </summary>
    /// <param name="materialType"></param>
    public void AddOrUpdateMaterialPath(ProductMaterialType materialType)
    {
        string materialPath = GetMaterialPath(productId, (int)materialType);
        if (materialPathDict.ContainsKey(materialType))
        {
            materialPathDict[materialType] = materialPath;
        }
        else
        {
            materialPathDict.Add(materialType, materialPath);
        }
    }

    /// <summary>
    /// 返回scene path
    /// </summary>
    public string GetScenePath()
    {
        return productFileRoot;
    }

    /// <summary>
    /// 返回scene path
    /// </summary>
    public string GetAssetsPath()
    {
        return Path.Combine(productFileRoot, ContentResPaths.SceneFileDesc);
    }

    /// <summary>
    /// 算法路径
    /// </summary>
    /// <returns></returns>
    public string GetAlgPath()
    {
        return Path.Combine(productFileRoot, ContentResPaths.AlgFileDesc);
    }

    /// <summary>
    /// scene resource 路径
    /// </summary>
    /// <returns></returns>
    public string GetSceneResoucePath()
    {
       return Path.Combine(GetAssetsPath(), sceneName + ".unity3d");
    }
    #endregion

    #region util
    /// <summary>
    /// get material path
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="materialType"></param>
    /// <returns></returns>
    private string GetMaterialPath(int productId, int materialType)
    {
        return Path.Combine(ConstPath.RootDirectory(), ConstPath.ProductDirectory(), productId.ToString(), materialType.ToString());
    }

    /// <summary>
    /// scene解析
    /// </summary>
    private void ParseScene()
    {
        string scenePath = GetAssetsPath();
        sceneParser.ParseScene(scenePath);
        sceneName = sceneParser.GetSceneName();
        //InsightDebug.Log("ParseScene", sceneName);
    }
    #endregion
}
