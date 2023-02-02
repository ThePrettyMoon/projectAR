using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{

    public class InsightRequest 
    {
        private string requestId;
        private string requestHeaders;
        private string requestParams;

        public InsightRequest(string request_id, string headers, string param) {

            requestId = request_id;
            requestHeaders = headers;
            requestParams = param;

        }

        public string error
        {
            get;
        }

        public bool isDone
        {
            get;
        }

        public bool isError
        {
            get;
        }

        public string requestID
        {
            get;
        }

        public int responseCode
        {
            get;
        }


        public int downloadedBytes
        {
            get;
        }

        public string downloadHandler
        {
            get;
        }
    }

}


