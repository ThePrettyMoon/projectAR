#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using ARWorldEditor;
using UnityEditor;
using UnityEngine;

namespace ARWorldEditor
{
    [ExecuteInEditMode]
    public class NavElement : MonoBehaviour
    {
        #region defualt data
        const string TAG = "NavElement";
        const string HEIGHTTAG = "Height";
        const string PATHTAG = "Path";
        const float defaultGroundArrowHeight = 0.4f;
        const float defaultTurnArrowHeight = 1.8f;
        const float defaultUpOrDownArrowHeight = 2.0f;
        const float defaultDestPointHeight = 2.0f;
        #endregion

        #region params
        [SerializeField]
        private float groundArrowHeight = defaultGroundArrowHeight;
        [SerializeField]
        private float turnArrowHeight = defaultTurnArrowHeight;
        [SerializeField]
        private float upOrDownArrowHeight = defaultUpOrDownArrowHeight;
        [SerializeField]
        private float destPointHeight = defaultDestPointHeight;

        [SerializeField]
        private GameObject groundArrowPrefab;
        [SerializeField]
        private GameObject turnArrowPrefab;
        [SerializeField]
        private GameObject upOrDownArrowPrefab;
        [SerializeField]
        private GameObject destPointPrefab;

        private long contentId;

        LocalNavElementData m_localNavElementData;
        #endregion

        #region custom functions
        public void Init(long contentID)
        {
            contentId = contentID;
            //读取本地保存数据
            if (EditorPrefs.HasKey(contentId + TAG+ HEIGHTTAG)) LoadSetting();
            else DefaultSetting();

            SetElementHeight();
            SetArrowPrefab();
            ElementUpdate();
        }

        private void SetElementHeight()
        {
            groundArrowHeight = m_localNavElementData.groundArrowHeight;
            turnArrowHeight = m_localNavElementData.turnArrowHeight;
            upOrDownArrowHeight = m_localNavElementData.upOrDownArrowHeight;
            destPointHeight = m_localNavElementData.destPointHeight;
        }
        private void SetArrowPrefab()
        {
            groundArrowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(m_localNavElementData.groundArrowPrefabPath);
            turnArrowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(m_localNavElementData.turnArrowPrefabPath);
            upOrDownArrowPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(m_localNavElementData.upOrDownArrowPrefabPath);
            destPointPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(m_localNavElementData.destPointPrefabPath);
        }
        private void LoadSetting()
        {
            string json = EditorPrefs.GetString(contentId + TAG + HEIGHTTAG);
            //Debug.LogError(json);
            m_localNavElementData = JsonUtil.Deserialization<LocalNavElementData>(json);
        }
        private void DefaultSetting()
        {
            if (m_localNavElementData == null) m_localNavElementData = new LocalNavElementData();

            m_localNavElementData.groundArrowHeight = defaultGroundArrowHeight;
            m_localNavElementData.turnArrowHeight = defaultTurnArrowHeight;
            m_localNavElementData.upOrDownArrowHeight = defaultUpOrDownArrowHeight;
            m_localNavElementData.destPointHeight = defaultDestPointHeight;

            m_localNavElementData.groundArrowPrefabPath = ConfigGlobal.NAV_PREFAB_PATH + "/" + ConfigGlobal.NAV_ARROW_PATH;
            m_localNavElementData.turnArrowPrefabPath = ConfigGlobal.NAV_PREFAB_PATH + "/" + ConfigGlobal.NAV_TURN_PATH;
            m_localNavElementData.upOrDownArrowPrefabPath = ConfigGlobal.NAV_PREFAB_PATH + "/" + ConfigGlobal.NAV_UPSTAIRS_PATH;
            m_localNavElementData.destPointPrefabPath = ConfigGlobal.NAV_PREFAB_PATH + "/" + ConfigGlobal.NAV_DEST_PATH;
        }
    
        /// <summary>
        /// 重置nav到默认状态
        /// </summary>
        public void ElementReset()
        {
            DefaultSetting();
            SetElementHeight();
            SetArrowPrefab();
            SaveToLocal();
            UpdatePrefab();
        }
        /// <summary>
        /// 
        /// </summary>
        public void ElementUpdate()
        {
            if (m_localNavElementData == null) m_localNavElementData = new LocalNavElementData();

            m_localNavElementData.groundArrowHeight = groundArrowHeight;
            m_localNavElementData.turnArrowHeight = turnArrowHeight;
            m_localNavElementData.upOrDownArrowHeight = upOrDownArrowHeight;
            m_localNavElementData.destPointHeight = destPointHeight;


            m_localNavElementData.groundArrowPrefabPath = AssetDatabase.GetAssetPath(groundArrowPrefab);
            m_localNavElementData.turnArrowPrefabPath = AssetDatabase.GetAssetPath(turnArrowPrefab);
            m_localNavElementData.upOrDownArrowPrefabPath = AssetDatabase.GetAssetPath(upOrDownArrowPrefab);
            m_localNavElementData.destPointPrefabPath = AssetDatabase.GetAssetPath(destPointPrefab);

            //保存到本地
            SaveToLocal();

            //设置 hierarchy 中的预制体
            UpdatePrefab();
        }
        private void UpdatePrefab()
        {
            //设置 hierarchy 中的预制体
            Transform arrowsParent = transform.Find("arrows");
            Transform turnArrowParent = transform.Find("navturn");
            Transform navUpstairsParent = transform.Find("navupstairs");
            Transform navDownstairsParent = transform.Find("navdownstairs");
            Transform navEnd = transform.Find("navend");

            ReplacePrefab(arrowsParent, groundArrowPrefab, groundArrowHeight, "Nav_Arrow");
            ReplacePrefab(turnArrowParent, turnArrowPrefab, turnArrowHeight, "navroadsign");
            ReplacePrefab(navUpstairsParent, upOrDownArrowPrefab, upOrDownArrowHeight, "nav_upstairs");
            ReplacePrefab(navDownstairsParent, upOrDownArrowPrefab, upOrDownArrowHeight, "nav_downstairs");
            ReplacePrefab(navEnd, destPointPrefab, destPointHeight, "navend");
        }

        private void SaveToLocal()
        {
            string json = JsonUtil.Serialize(m_localNavElementData);
            EditorPrefs.SetString(contentId + TAG + HEIGHTTAG, json);
        }
        private void ReplacePrefab(Transform parent,GameObject prefab,float height,string name)
        {
            var deleteList = new List<Transform>();
            int childCount = parent.childCount;
            if (childCount == 0)//nav end
            {
                var obj = GameObject.Instantiate(prefab, Vector3.up * height, Quaternion.identity, parent.parent);
                obj.name = name;
                GameObject.DestroyImmediate(parent.gameObject);
                return;
            }

            for (int i = 0; i < childCount; i++)
            {
                deleteList.Add(parent.GetChild(i));
            }
            foreach (var item in deleteList)
            {
                GameObject.DestroyImmediate(item.gameObject);
            }

            for (int i = 0; i < childCount; i++)
            {
                var obj = GameObject.Instantiate(prefab,Vector3.up * height,Quaternion.identity,parent);
                if (i == 0)
                {
                    obj.name = name;
                }
                else
                {
                    obj.name = name + " (" + i + ")";
                }
            }
        }
        #endregion

    }
    public class LocalNavElementData
    {
        public float groundArrowHeight ;
        public float turnArrowHeight;
        public float upOrDownArrowHeight;
        public float destPointHeight;

        public string groundArrowPrefabPath;
        public string turnArrowPrefabPath;
        public string upOrDownArrowPrefabPath;
        public string destPointPrefabPath;
    }

}
#endif