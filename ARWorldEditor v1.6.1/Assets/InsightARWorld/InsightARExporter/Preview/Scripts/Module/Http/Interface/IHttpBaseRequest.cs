using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EZXR.NET
{
    public interface IHttpBaseRequest 
    {
        void Abort();
        IEnumerator Send();

        void Dispose();
    }
}