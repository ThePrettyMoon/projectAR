using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ARWorldEditor
{
    public static class EncodeUtility
    {
        private const int Md5ReadLen = 16 * 1024;       // 一次读取长度 16384 = 16*kb
        /// <summary>
        /// 返回md5字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        /// <summary>
        /// sha256
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string Sha256(string pass)
        {
            if (pass == null || pass == string.Empty) { return null; }
            byte[] buffer = Encoding.UTF8.GetBytes(pass);

            byte[] hash = SHA256Managed.Create().ComputeHash(buffer);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("X2"));
            }
            return builder.ToString();
        }

        /// <summary>
        /// 读取文件返回md5值
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            byte[] buffer = new byte[Md5ReadLen];
            int readLength = 0;//每次读取长度  
            var output = new byte[Md5ReadLen];

            using (Stream inputStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider())
                {
                    while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        //计算MD5  
                        hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
                    }
                    //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)  
                    hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
                    byte[] retVal = hashAlgorithm.Hash;

                    StringBuilder sb = new StringBuilder(32);
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }

                    hashAlgorithm.Clear();
                    inputStream.Close();
                    return sb.ToString();
                }
            }
        }
    }
}
