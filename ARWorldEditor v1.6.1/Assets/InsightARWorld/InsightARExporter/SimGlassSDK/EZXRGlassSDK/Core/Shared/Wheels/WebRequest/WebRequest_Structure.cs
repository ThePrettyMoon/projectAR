using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Wheels.Unity
{
    public partial class UnityWebRequest :MonoBehaviour
    {
        [Serializable]
        /// <summary>
    /// 请求数据用的基础请求类
    /// </summary>
        public class ReqBase
        {
            /// <summary>
            /// 用户登陆后的标识，登录接口本字段可为空
            /// </summary>
            public string token;
            /// <summary>
            /// 请求包签名，防止重放攻击，生成规则为md5(secret+|+t)，其中secret是用户成功登录后返回的，t是请求url中携带的时间戳。
            /// </summary>
            public string signature;
            /// <summary>
            /// 客户端当前版本号，格式x.x.x
            /// </summary>
            public string versionno;
            /// <summary>
            /// 版本协议号,格式x.x，默认为1.0
            /// </summary>
            public string protocolno;
            /// <summary>
            /// 网络接入点名称，如uninet，cmnet，uniw
            /// </summary>
            public string apn;
            public string environment;
            public string bizid;

            public ReqBase()
            {
                token = "";
                signature = "";
                versionno = "1.0.0";
                protocolno = "1.0";
                apn = "uniw";
                environment = "kr1r4ndd";
                bizid = "mhlaqph8";
            }

            public ReqBase(string _token, string _signature, string _versionno, string _protocolno, string _apn, string _environment, string _bizid)
            {
                token = _token;
                signature = _signature;
                versionno = _versionno;
                protocolno = _protocolno;
                apn = _apn;
                environment = _environment;
                bizid = _bizid;
            }
        }

        [Serializable]
        public class RespBase
        {
            /// <summary>
            /// 返回状态码
            /// </summary>
            public string code;
            /// <summary>
            /// 服务器返回描述
            /// </summary>
            public string desc;
        }


        #region GetProductCategories，获得产品分类列表
        #region GetProductCategories_Request
        [Serializable]
        public class GetProductCategories_Request_Root
        {
            public ReqBase reqbase = new ReqBase();
        }
        #endregion

        #region GetProductCategories_Response
        [Serializable]
        public class GetProductCategories_RespparamItem:IComparable
        {
            /// <summary>
            /// 决定此Item在列表中的显示位置
            /// </summary>
            public int cid;
            /// <summary>
            /// 分类名称
            /// </summary>
            public string name;
            /// <summary>
            /// 分类图标路径
            /// </summary>
            public string pic;

            public int CompareTo(object obj)
            {
                int result;
                try
                {
                    GetProductCategories_RespparamItem gpcri = obj as GetProductCategories_RespparamItem;
                    if (this.cid > gpcri.cid)
                    {
                        result = 1;
                    }
                    else if (this.cid < gpcri.cid)
                    {
                        result = -1;
                    }
                    else
                    {
                        result = 0;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        [Serializable]
        public class GetProductCategories_Response_Root
        {
            public RespBase respbase = new RespBase();
            public List <GetProductCategories_RespparamItem > respparam = new List<GetProductCategories_RespparamItem>();
        }
        #endregion
        #endregion


        #region GetProductList，获得具体分类下的产品列表
        #region GetProductList_Request
        [Serializable]
        public class GetProductList_Request_Root
        {
            public ReqBase reqbase = new ReqBase();
            public int reqparam;
        }
        #endregion

        #region GetProductList_Response
        /// <summary>
        /// 产品模型信息
        /// </summary>
        [Serializable]
        public class GetProductList_JZModel
        {
            public int mid;
            public string name;
            public string url;
            /// <summary>
            /// 算法类型，如果类型是模型，则本字段不为空 1.SLAM 2.3DLM
            /// </summary>
            public string algotype;
            /// <summary>
            /// 模型大小
            /// </summary>
            public int size;
        }

        /// <summary>
        /// 产品纹理信息
        /// </summary>
        [Serializable]
        public class GetProductList_JZTexture
        {
            /// <summary>
            /// 贴图的tid
            /// </summary>
            public int tid;
            /// <summary>
            /// 更改颜色列表中显示的小图icon
            /// </summary>
            public string pic;
            /// <summary>
            /// 模型真正用到的贴图
            /// </summary>
            public string chartlet;
            /// <summary>
            /// 纹理名称
            /// </summary>
            public string name;
        }

        /// <summary>
        /// 这个产品属于哪个分类
        /// </summary>
        [Serializable]
        public class Category
        {
            /// <summary>
            /// 
            /// </summary>
            public int cid;

            /// <summary>
            /// 其他
            /// </summary>
            public string name;
        }

        /// <summary>
        /// 单个产品信息
        /// </summary>
        [Serializable]
        public class GetProductList_RespparamItem:IComparable
        {
            public Category category = new Category();
            /// <summary>
            /// 产品id
            /// </summary>
            public int pid;
            /// <summary>
            /// 产品名称
            /// </summary>
            public string name;
            /// <summary>
            /// 产品的图标地址
            /// </summary>
            public List<string> piclist = new List<string>();
            /// <summary>
            /// 产品的长宽高
            /// </summary>
            public string dimension;
            /// <summary>
            /// 产品价格
            /// </summary>
            public int price;
            public GetProductList_JZModel model;
            public UnityEngine.Object obj;
            /// <summary>
            /// 产品拥有的纹理信息
            /// </summary>
            public List<GetProductList_JZTexture> textures = new List<GetProductList_JZTexture>();
            /// <summary>
            /// 是否被收藏了
            /// </summary>
            public bool collected;

            public int CompareTo(object obj)
            {
                int result;
                try
                {
                    GetProductList_RespparamItem gplri = obj as GetProductList_RespparamItem;
                    if (this.pid > gplri.pid)
                    {
                        result = 1;
                    }
                    else if (this.pid < gplri.pid)
                    {
                        result = -1;
                    }
                    else
                    {
                        result = 0;
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        [Serializable]
        public class GetProductList_Response_Root
        {
            public RespBase respbase = new RespBase();
            public List <GetProductList_RespparamItem > respparam = new List<GetProductList_RespparamItem>();
        }
        #endregion
        #endregion

        #region ProductCollect，产品收藏
        #region ProductCollect_Request
        [Serializable]
        public class ProductCollect_Reqparam
        {
            public int pid;
            public bool collect;

            public ProductCollect_Reqparam()
            {
            }

            public ProductCollect_Reqparam(int pid, bool collect)
            {
                this.pid = pid;
                this.collect = collect;
            }
        }

        [Serializable]
        public class ProductCollect_Request_Root
        {
            public ReqBase reqbase = new ReqBase();
            public ProductCollect_Reqparam reqparam = new ProductCollect_Reqparam();
        }
        #endregion
        #endregion
    }
}