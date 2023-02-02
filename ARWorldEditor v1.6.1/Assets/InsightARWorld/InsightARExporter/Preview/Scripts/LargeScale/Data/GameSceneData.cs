using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


/// <summary>
/// 场景数据
/// </summary>
public class GameSceneData
{
    private static GameSceneData _instance;
    public static GameSceneData Instance{
        get{
            if (_instance == null){
                _instance = new GameSceneData();
            }
            return _instance;
        }
    }
    #region ls game scene data
    private string contentId;
    private string contentPath;
    public string GetContentId()
    {
        return contentId;
    }

    public void SetContentId(string cid) {
        contentId = cid;
    }

    public string GetContentPath()
    {
        return contentPath;
    }

    public void SetContentPath(string path)
    {
        contentPath = path;
    }

    #endregion



    private int sceneId;
    private int productId;
    private ArProduct arProduct;
    private ProductListData productList;

    private int productOritention;

    private string poiInfos;
    private bool naviEnabled;
    private string naviPoiInput;
    private string naviPathString;
    private INavigationType navigationType;

    private Dictionary<string, POIStatusData> poiStatusDataList;
    public Action<string> onPOIStatusChanged;

    public void SetNaviEnabled(bool navi)
    {
        naviEnabled = navi;
    }

    public bool GetNaviEnabled()
    {
        return naviEnabled;
    }

    public void SetPoiInfos(string str)
    {
        this.poiInfos = str;
    }

    public string GetPoiInfos()
    {
        return poiInfos;
    }

    public void SetNaviPoiInput(string str)
    {
        this.naviPoiInput = str;
    }

    public string GetNaviPoiInput()
    {
        return naviPoiInput;
    }


    public void SetNavigationType(INavigationType navi)
    {
        navigationType = navi;
    }

    public INavigationType GetNavigationType()
    {
        return navigationType;
    }

    public void SetNaviPathString(string str)
    {
        this.naviPathString = str;
    }

    public string GetNaviPathString()
    {
        return naviPathString;
    }


    public GameSceneData()
    {
        productList = new ProductListData();
        poiStatusDataList = new Dictionary<string, POIStatusData>();
    }

    public ProductListData GetProductList()
    {
        return productList;
    }

    /// <summary>
    /// 设置当前场景
    /// </summary>
    /// <param name="sceneId"></param>
    /// <param name="productId"></param>
    public void SetSelectId(int sId,int pId)
    {
        this.sceneId = sId;
        this.productId = pId;
    }

    public int GetSceneId()
    {
        return sceneId;
    }

    public int GetProductId()
    {
        return productId;
    }

    public void SetArProduct(ArProduct product) {
        arProduct = product;
    }

    public ArProduct GetArProduct() {

        return arProduct;
    }

    public void SetProductOritention(int ori) {
        productOritention = ori;
    }

    public int GetProductOritention() {
        return productOritention;
    }

    public void UpdatePOINeedUpdate(string cid, POINeedUpdateData updateState) {

        POIStatusData poiStatusData = new POIStatusData();
        if (poiStatusDataList.ContainsKey(cid)) {
            poiStatusDataList.TryGetValue(cid, out poiStatusData);
            poiStatusData.NeedUpdate = ((int)updateState).ToString();
            poiStatusDataList[cid] = poiStatusData;
        }
        else {
            poiStatusData.Cid = cid;
            poiStatusData.NeedUpdate = ((int)updateState).ToString();
            poiStatusData.DownloadResult = "0";
            poiStatusDataList.Add(cid, poiStatusData);
        }

        onPOIStatusChanged?.Invoke(JsonUtil.Serialize(poiStatusData));

    }

    public void UpdatePOIDownloadResult(string cid, POIDownloadResult downloadResult)
    {
        var needUpdate = POINeedUpdateData.NEEDUPDATE;
        if (downloadResult == POIDownloadResult.DOWNLOAD_DONE) {
            needUpdate = POINeedUpdateData.NO_NEEDUPDATE;
        }
        POIStatusData poiStatusData = new POIStatusData();
        if (poiStatusDataList.ContainsKey(cid))
        {
            poiStatusDataList.TryGetValue(cid, out poiStatusData);
            poiStatusData.NeedUpdate = ((int)needUpdate).ToString();
            poiStatusData.DownloadResult = ((int)downloadResult).ToString();
            poiStatusDataList[cid] = poiStatusData;
        }
        else
        {
            poiStatusData.Cid = cid;
            poiStatusData.NeedUpdate = ((int)needUpdate).ToString();
            poiStatusData.DownloadResult = ((int)downloadResult).ToString();
            poiStatusDataList.Add(cid, poiStatusData);
        }

        onPOIStatusChanged?.Invoke(JsonUtil.Serialize(poiStatusData));
    }

    // 清空数据
    public void Clear()
    {
        poiStatusDataList.Clear();
    }

    public void ClearPOISenceData() 
    {
        onPOIStatusChanged = null;
    }
}


