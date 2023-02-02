using InsightAR.Internal;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContentStartManager
{
    private const string TAG = "ContentStartManager";
    private static string localPath = "";
    #region ls_sdk
    /// <summary>
    /// 启动内容
    /// 由APP发起，传递内容路径启动,参数是json字符串
    /// {
    ///     cid,sceneName,localPath,loadMode
    /// }
    /// </summary>
    /// <param name="sceneparams"></param>
    public static void lsContentLoaderBridge(string sceneparams) 
    {
        localPath = sceneparams;
        Debug.Log("localPath: " + localPath);
        JObject jObject = JObject.Parse(localPath);
        if (jObject == null){
            InsightDebug.LogError(TAG, "scene start error: " + localPath);
            return;
        }
        string path = JObjectUtility.ParseJObjectString(jObject.SelectToken("localPath"));
        string scene = JObjectUtility.ParseJObjectString(jObject.SelectToken("sceneName"));
        string cid = JObjectUtility.ParseJObjectString(jObject.SelectToken("cid"));
        //LoadSceneMode mode = (LoadSceneMode)JObjectUtility.ParseJObjectInt(jObject.SelectToken("loadMode"));
        JObject jObjectContent = (JObject)jObject.SelectToken("content");
        //兼容v1.2.0及以前的接口参数
        if (jObjectContent == null) {
            SceneController.Instance.lsLoadScene(cid, path, scene);
            return;
        }
        string sid = JObjectUtility.ParseJObjectString(jObjectContent.SelectToken("sid"));
        SceneController.Instance.lsLoadScene(cid, path, scene, sid);
    }
    #endregion

#if false
    public static void arResourceLocalPathHandler(string path)
    {
#if UNITY_EDITOR
        path = "{\"appSecret\":\"HWukfX0b1j\",\"brand\":\"ios\",\"content\":{\"application\":{\"name\":\"预览APP\"},\"navEnabled\":true,\"totalSize\":93720096,\"creator\":{\"nick\":\"EZ0030\",\"avatar\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20201216192919456_097KDOJx.jpg\",\"gender\":0},\"snapshotAuditStatus\":4,\"tags\":[],\"materials\":[{\"size\":11069815,\"path\":\"10001\",\"mid\":1129,\"type\":10001,\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/db477d5c-4ffd-4ecb-9431-bc42deccb22c.zip\",\"md5\":\"9fa6a2ed23a266cffcab2fe11bdcbe1e\"},{\"size\":67884826,\"path\":\"10002\",\"mid\":958,\"type\":10002,\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210115160121760_yKeWNlwl.zip\",\"md5\":\"eab1283ebcdcc96407ce25fe9f57b1b7\"},{\"size\":14585804,\"path\":\"10003\",\"mid\":274,\"type\":10003,\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20201204102338775_5MH14qmk.zip\",\"md5\":\"8c0dc3dfc3a4af0b0ca4df54ccf82c7e\"},{\"size\":30127,\"path\":\"10008\",\"mid\":1131,\"type\":10008,\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/5114cc2b-a350-4bfa-b54c-705ab67a3db1.zip\",\"md5\":\"f519eb498efcfa7904c7c9a46a6651cd\"},{\"size\":149524,\"path\":\"10009\",\"mid\":1054,\"type\":10009,\"url\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/arworld_20210119170729723_yrybIE3O.zip\",\"md5\":\"57bd0bb1fe10d14d46a813602d5e0891\"}],\"promptBeforeDownload\":false,\"updateTime\":1611197305000,\"packageSdkVersion\":\"1.3.0\",\"engineType\":2,\"proposeAlternativePlatform\":false,\"cloudRelocUrl\":\"http:\\/\\/59.111.148.60:8087\\/api\\/alg\\/cloud\\/aw\\/reloc\\/proxy?routeApp=largescene-inner\",\"contentMinSdkVersion\":\"1.3.0\",\"contentOnlineStatus\":0,\"channel\":0,\"name\":\"Unity主场景导航联调\",\"shouldUpdate\":false,\"packageApt\":true,\"sid\":452,\"cid\":223,\"packageOwn\":true,\"packageForPlatform\":true,\"centroid\":{\"longitude\":120.22881922610348,\"latitude\":30.244357912537673},\"orientation\":3,\"downloaded\":true,\"contentType\":1,\"distance\":0,\"snapshotType\":2,\"sarPid\":0,\"address\":\"浙江省杭州市萧山区钱江世纪公园C6\",\"coverImageUrl\":\"https:\\/\\/ar-scene-source.nosdn.127.net\\/b1264196b12479633ce18735b8dca766.png\"},\"appKey\":\"NAR-NRFW-UKL3PX5KOATRE-I-T\",\"localPath\":\"\\/var\\/mobile\\/Containers\\/Data\\/Application\\/9E293770-ADB4-4B33-BFAC-FABEA0ED01B4\\/Documents\\/OasisPreviewApp\\/main\\/223\"}";
#endif

        //InsightDebug.Log(TAG, path);
        localPath = path;

        LSGameManager.Instance.InitARScene();

        ClientInfos.Init();
        GetPhoneBrand(localPath);

        //ClientInfos.SetXnwa(InsightAPPNativeAPI.nbIsWifi()?"wifi":"");
        //UserEventManager.UserEventCallbackHandler(IUserEventType.IUserEventTypeLogLevelNone);

        ContentResPaths.Instance.ResourcePath = GetResourcePath(localPath);

        ArProduct arProduct = GetAndInitArProduct(localPath);
        GameSceneData.Instance.SetArProduct(arProduct);
        InsightConfigManager.Instance.SetMainContentCloudUrl(arProduct.CloudRelocUrl);

        SetAndInitAppConfig(localPath);
        InsightConfigManager.Instance.SetSDKBundleID(ClientInfos.GetBundleID());

        //设置横竖屏

#if UNITY_ANDROID
        if (arProduct.Orientation == (int)ScreenOrientation.Portrait)
            Screen.orientation = ScreenOrientation.Portrait;
        else
            Screen.orientation = ScreenOrientation.LandscapeLeft;
#endif
        GameSceneData.Instance.SetProductOritention(arProduct.Orientation);
        InsightDebug.Log(TAG, "Oritention " + Screen.orientation + "/should oritention " + arProduct.Orientation);

        //初始化导航启动状态
        SetAndInitNavEnabled(localPath);

        CheckDeviceAndStartConfig();
    }

    private static void GetPhoneBrand(string localPath) {
        JObject jObject = JObject.Parse(localPath);
        if (jObject != null)
        {
            var brand = JObjectUtility.ParseJObjectString(jObject.SelectToken("brand"));
            ClientInfos.SetPhoneBrand(brand);
        }
    }

    private static string GetResourcePath(string localPath)
    {
        JObject jObject = JObject.Parse(localPath);
        string resPath = "";
        if (jObject != null)
        {
            resPath = JObjectUtility.ParseJObjectString(jObject.SelectToken("localPath"));
#if UNITY_EDITOR
            resPath = Application.dataPath.Replace("Assets", "EditorProductor/223");
#endif
        }
        return resPath;
    }

    private static ArProduct GetAndInitArProduct(string localPath)
    {
        JObject jObject = JObject.Parse(localPath);

        ArProduct arProduct = new ArProduct();
        JObject jObjectContent = (JObject)jObject.SelectToken("content");
        arProduct.NavEnabled = bool.Parse(jObjectContent.SelectToken("navEnabled").ToString());
        arProduct.TotalSize = long.Parse(jObjectContent.SelectToken("totalSize").ToString());
        arProduct.UpdateTime = long.Parse(jObjectContent.SelectToken("updateTime").ToString());
        arProduct.Orientation = int.Parse(jObjectContent.SelectToken("orientation").ToString());
        arProduct.CloudRelocUrl = JObjectUtility.ParseJObjectString(jObjectContent.SelectToken("cloudRelocUrl"));
        arProduct.Name = JObjectUtility.ParseJObjectString(jObjectContent.SelectToken("name"));
        arProduct.Sid = int.Parse(jObjectContent.SelectToken("sid").ToString());
        arProduct.Cid = int.Parse(jObjectContent.SelectToken("cid").ToString());
        arProduct.SarPid = int.Parse(jObjectContent.SelectToken("sarPid").ToString());
        arProduct.Address = JObjectUtility.ParseJObjectString(jObjectContent.SelectToken("address"));
        arProduct.CoverImageUrl = JObjectUtility.ParseJObjectString(jObjectContent.SelectToken("coverImageUrl"));

        var objTags = (JArray)jObjectContent.SelectToken("tags");
        arProduct.Tags = new List<AW_Tag>();
        for (int i = 0; i < objTags.Count; i++)
        {
            AW_Tag t = new AW_Tag
            {
                mid = JObjectUtility.ParseJObjectInt(objTags[i].SelectToken("mid")),
                name = JObjectUtility.ParseJObjectString(objTags[i].SelectToken("name"))
            };
            arProduct.Tags.Add(t);
        }
        var objMats = (JArray)jObjectContent.SelectToken("materials");
        arProduct.ProductMaterials = new List<ProductMaterial>();
        for (int i = 0; i < objMats.Count; i++)
        {
            ProductMaterial p = new ProductMaterial
            {
                Size = int.Parse(objMats[i].SelectToken("size").ToString()),
                Path = JObjectUtility.ParseJObjectString(objMats[i].SelectToken("path")),
                Mid = int.Parse(objMats[i].SelectToken("mid").ToString()),
                Type = int.Parse(objMats[i].SelectToken("type").ToString()),
                Url = JObjectUtility.ParseJObjectString(objMats[i].SelectToken("url")),
                Md5 = JObjectUtility.ParseJObjectString(objMats[i].SelectToken("md5"))
            };
            p.Path = Path.Combine(ContentResPaths.Instance.ResourcePath, p.Path);
            arProduct.ProductMaterials.Add(p);
        }
        return arProduct;
    }

    private static void SetAndInitNavEnabled(string localPath)
    {
        JObject jObject = JObject.Parse(localPath);
        if (jObject != null)
        {
            JObject jObjectContent = (JObject)jObject.SelectToken("content");
            var enabled = bool.Parse(jObjectContent.SelectToken("navEnabled").ToString());
            GameSceneData.Instance.SetNaviEnabled(enabled);
        }
    }

    private static void SetAndInitAppConfig(string localPath)
    {
        JObject jObject = JObject.Parse(localPath);
        if (jObject != null)
        {
            var appKey = JObjectUtility.ParseJObjectString(jObject.SelectToken("appKey"));
            var appSecret = JObjectUtility.ParseJObjectString(jObject.SelectToken("appSecret"));
            var appDomain = JObjectUtility.ParseJObjectString(jObject.SelectToken("domain"));
            InsightConfigManager.Instance.SetSDKKey(appKey);
            InsightConfigManager.Instance.SetSDKSecret(appSecret);
            InsightConfigManager.Instance.SetApiDomain(appDomain);
        }
    }


    private static void UserLogined()
    {

        GeoDataController.SetAndInitGeoData();
        if (GameSceneData.Instance.GetNaviEnabled())
        {
            //load 2dmap json to native
            NotifyNativeMessage.LoadGeoMapData();
        }
        else
        {
#if UNITY_ANDROID
            //load 2dmap json to native
            NotifyNativeMessage.LoadMapData("");
#endif
            NotifyNativeMessage.SetMapVisibility(0);
        }

        ArProduct arProduct = GameSceneData.Instance.GetArProduct();
        ProductData productData = new ProductData();
        productData.SetSceneId(arProduct.Sid);
        productData.SetProductId(arProduct.Cid);
        productData.SetProduct(arProduct);
        productData.SetProductFileRoot(ContentResPaths.Instance.ResourcePath);
        SceneController.Instance.LoadScene(productData);

    }

    private static void CheckDeviceAndStartConfig()
    {
        bool localActive = CheckLocalContentCache();

        if (localActive){
            UserLogined();
        }

        NetDataFetchManager.Instance.CheckARSupport(new OnOasisNetworkDataFetchCallback<DeviceFeatureResponseData>(
           (DeviceFeatureResponseData data) => {

               InsightConfigManager.Instance.SetArSupport(data.ArSupport);

               //timestamp
               NetDataFetchManager.Instance.QueryTimestamp(new OnOasisNetworkDataFetchCallback<TimestampResponseData>(
                (TimestampResponseData timeData) => {
                    long time = TimeUtility.GetTimeStampMilli();
                    InsightConfigManager.Instance.SetTimetampDelta(timeData.Timestamp - time);
                    InsightConfigManager.Instance.SetSyncTimestamp(true);
                }, (string code, string msg) => {
                    InsightDebug.Log(TAG, "TimetampDelta Error: code == " + code + " msg == " + msg);
                    InsightConfigManager.Instance.SetSyncTimestamp(false);
                }));

               //login
               string appKey = InsightConfigManager.Instance.GetResourceGroupKey();
               string appSecret = InsightConfigManager.Instance.GetResourceGroupSecret();
               string packageName = InsightConfigManager.Instance.GetSDKBundleID();
               var platformId = ClientInfos.GetPlatform();
               NetDataFetchManager.Instance.Login(appKey, appSecret, packageName, platformId, new OnOasisNetworkDataFetchCallback<LoginResponseData>(
                  (LoginResponseData loginData) => {

                      InsightConfigManager.Instance.SetToken(loginData.Token);
                      InsightConfigManager.Instance.SetGroupID(loginData.GroupId);

                      if(!localActive)
                          UserLogined();

                  }, (string code, string msg) => {
                      InsightDebug.Log(TAG, "LoginError: code == " + code + " msg == " + msg);
                  }));
           },
           (string code, string msg) =>
           {
               InsightDebug.Log(TAG, "CheckARSupport Error: code == " + code + " msg == " + msg);

               if (!localActive) {
                   //回调内容加载失败
                   InsightAPPNative.SetAREvent(IAREventType.IAREventTypeLoadMainContentError);
               }
           }));

    }


    private static bool CheckLocalContentCache() {

        var localARSupport = InsightConfigManager.Instance.GetArSupport();
        if (localARSupport){
            var localTimedtampDelta = InsightConfigManager.Instance.GetTimetampDelta();
            var localToken = InsightConfigManager.Instance.GetToken();
            InsightDebug.Log(TAG, "localARSupport: " + localARSupport);
            InsightDebug.Log(TAG, "localtimestamp: " + localTimedtampDelta);
            InsightDebug.Log(TAG, "localToken: " + localToken);
            if (!string.IsNullOrEmpty(localToken)) {
                return true;
            }
        }
        return false;
    }
#endif
}
