using System;
using ARWorldEditor;

namespace ARWorldEditor
{
    public class MyProducts
    {
        #region params
        private static GetMyContentsResultData myProduct;

        public static GetMyContentsResultData MyProduct
        {
            get
            {
                return myProduct;
            }
            set
            {
                myProduct = value;
            }
        }

        #endregion

        #region unity functions
        public static void GetMyProducts(Action<GetMyContentsResponseData> onSuccess,Action<string,string> onFail)
        {
            ARWorldEditor.NetDataFetchManager.Instance.GetMyContents(new OnOasisNetworkDataFetchCallback<GetMyContentsResponseData>(
                (GetMyContentsResponseData response) =>
                {
                    OnGetProductsSuccess(response,onSuccess);
                }, (string code, string msg) =>
                 {
                     OnGetProductsFail(code,msg,onFail);
                 }));
        }

        public static void GetMyProducts(Action<MyContentsResponseData> onSuccess, Action<string, string> onFail)
        {
            ARWorldEditor.NetDataFetchManager.Instance.MyContents(new OnOasisNetworkDataFetchCallback<MyContentsResponseData>(
               (MyContentsResponseData response) =>
               {
                   onSuccess?.Invoke(response);
               }, (string code, string msg) =>
               {
                   onFail?.Invoke(code, msg);
               }));
        }

        private static void OnGetProductsSuccess(GetMyContentsResponseData response,Action<GetMyContentsResponseData> OnSuccess)
        {
          //  Debug.Log("get my products success");
            OnSuccess?.Invoke(response);
        }

        private static void OnGetProductsFail(string code, string msg,Action<string,string> onFail)
        {
           // Debug.Log("get my products error " + code + " msg " + msg);
            onFail?.Invoke(code, msg);
        }
        #endregion



    }
}
