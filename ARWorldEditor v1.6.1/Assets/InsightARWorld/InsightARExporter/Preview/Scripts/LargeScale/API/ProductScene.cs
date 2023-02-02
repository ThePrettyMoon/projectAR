using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductScene
{
    #region params
    private int sceneId;
    private int productId;
    //场景类型
    private ProductSceneMode productSceneMode;
    private InsightAttachType attachType;

    private ProductSceneModel sceneModel;

    public int GetProductId()
    {
        return productId;
    }

    public ProductSceneMode GetMode()
    {
        return productSceneMode;
    }

    public InsightAttachType GetAttachType()
    {
        return attachType;
    }

    public void SetAttachType(InsightAttachType type)
    {
        this.attachType = type;
    }

    public ProductSceneModel GetSceneModel()
    {
        return sceneModel;
    }
    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name="product"></param>
    /// <param name="sceneMode"></param>
    public ProductScene( ArProduct product,string productFileRoot, ProductSceneMode sceneMode)
    {
        sceneModel = new ProductSceneModel(product, productFileRoot);
        productSceneMode = sceneMode;
    }
}
