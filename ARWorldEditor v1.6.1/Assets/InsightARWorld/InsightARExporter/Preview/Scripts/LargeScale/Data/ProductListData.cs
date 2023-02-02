using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储产品数据
/// </summary>
public class ProductListData
{
    #region params
    //ar product
    private List<ProductData> productList;
    #endregion

    public ProductListData()
    {
        productList = new List<ProductData>();
    }

    /// <summary>
    /// add or update product
    /// </summary>
    /// <param name="arProduct"></param>
    public void AddOrUpdateProduct( ArProduct arProduct)
    {
        if (arProduct == null) return;
        ProductData productData = GetProductById(arProduct.Sid, arProduct.Cid);
        if (productData == null)
        {
            productData = new ProductData();
            productList.Add(productData);
        }
        productData.SetProduct(arProduct);
        productData.SetSceneId(arProduct.Sid);
        productData.SetProductId(arProduct.Cid);
        
    }

    public ProductData GetProductById(int sceneId, int productId)
    {
        if (productList == null) return null;
        return productList.Find(p => p.GetProductId() == productId && p.GetSceneId() == sceneId);
    }
}

public class ProductData
{
    private int sId;
    private int cId;
    private ArProduct arProduct;
    private string productFileRoot;

    public void SetSceneId(int sId)
    {
        this.sId = sId;
    }

    public int GetSceneId()
    {
        return sId;
    }

    public int GetProductId()
    {
        return cId;
    }

    public void SetProductId(int pId)
    {
        this.cId = pId;
    }

    public string GetProductFileRoot()
    {
        return productFileRoot;
    }

    public void SetProductFileRoot(string root)
    {
        this.productFileRoot = root;
    }

    public ArProduct GetProduct()
    {
        return arProduct;
    }

    public void SetProduct(ArProduct product)
    {
        this.arProduct = product;
    }
}
