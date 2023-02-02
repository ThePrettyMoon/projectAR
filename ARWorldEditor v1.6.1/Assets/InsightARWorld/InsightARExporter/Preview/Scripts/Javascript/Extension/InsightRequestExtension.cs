using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insight
{
    public static class InsightRequestExtension
    {

        public static string getError(this InsightRequest insightRequest)
        {
            return "";
        }

        public static bool getIsDone(this InsightRequest insightRequest)
        {
            return false;
        }

        public static bool getIsError(this InsightRequest insightRequest)
        {
            return false;
        }

        public static string getRequestID(this InsightRequest insightRequest)
        {
            return "";
        }

        public static int getResponseCode(this InsightRequest insightRequest)
        {
            return 0;
        }


        public static int getDownloadedBytes(this InsightRequest insightRequest)
        {
            return 0;
        }

        public static string getDownloadHandler(this InsightRequest insightRequest)
        {
            return "";
        }

    }
}


