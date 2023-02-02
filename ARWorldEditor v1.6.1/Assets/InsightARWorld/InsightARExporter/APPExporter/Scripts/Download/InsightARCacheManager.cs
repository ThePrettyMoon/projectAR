using System.IO;
using ARWorldEditor;

namespace ARWorldEditor
{
    /// <summary>
    /// 添加数据缓存管理
    /// </summary>
    public class InsightCacheManager : Singleton<InsightCacheManager>
    {
        #region params
        //缓存数据
        public InsightARCache insightArCache;

        private string filePath;

        #endregion

        #region custom functions
        /// <summary>
        /// 读取缓存数据
        /// </summary>
        public void LoadCache()
        {
            filePath = GetCacheFilePath();
            if (File.Exists(filePath))
            {
                string jsonStr = File.ReadAllText(filePath);
                insightArCache = JsonUtil.Deserialization<InsightARCache>(jsonStr);
            }
            else
            {
                insightArCache = new InsightARCache();
            }
        }

        /// <summary>
        /// 把内存数据写入磁盘
        /// 然后关闭
        /// </summary>
        public void WriteToDisk()
        {
            if (insightArCache == null) return;
            string jsonStr = JsonUtil.Serialize(insightArCache);
            StreamWriter streamWriter = new StreamWriter(filePath);
            streamWriter.Write(jsonStr);
            streamWriter.Flush();
            streamWriter.Close();
        }

        /// <summary>
        ///  add or update
        /// </summary>
        /// <param name="dbData"></param>
        public void AddOrUpdate(BaseDbData dbData)
        {
            insightArCache.AddOrUpdate(dbData);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="dbData"></param>
        public void Delete(BaseDbData dbData)
        {
            insightArCache.Delete(dbData);
        }

        /// <summary>
        /// 查询对应的数据结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query<T>(int id) where T : BaseDbData
        {
            return Query<T>(id.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Query<T>(string id) where T : BaseDbData
        {
            return insightArCache.Query<T>(id);
        }

        /// <summary>
        /// 返回本地cache 文件路径
        /// </summary>
        /// <returns></returns>
        private string GetCacheFilePath()
        {
            string directory = Path.Combine(ConfigGlobal.DataPath, ConfigGlobal.DEFAULT_CACHE_PATH);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory + "/" + ConfigGlobal.CACHE_FILE_NAME;
        }
        #endregion
    }
}


