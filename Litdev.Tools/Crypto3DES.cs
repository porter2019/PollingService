using System;
using System.Security.Cryptography;
using System.Text;

namespace Litdev.Tools
{
    /// <summary>
    /// C#/PHP/JSP 3DES 加密与解密（只支持UTF-8编码）
    /// </summary>
    public class Crypto3DES
    {
        /// <summary>
        /// 默认密钥
        /// </summary>
        private string Keys;

        /// <summary>
        /// 密钥与加密字符串不足8字符时的填充字符
        /// </summary>
        private char paddingChar = ' ';

        /// <summary>
        /// 实例化 Crypto3DES 类
        /// </summary>
        /// <param name="key">密钥</param>
        public Crypto3DES(string key)
        {
            this.Keys = key;
        }

        /// <summary>
        /// 获取密钥，不足8字符的补满8字符，超过8字符的截取前8字符
        /// </summary>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        private string GetKeyCode(string key)
        {
            if (key.Length > 8)
                return key.Substring(0, 8);
            else
                return key.PadRight(8, paddingChar);
        }

        /// <summary>
        /// 获取加密字符串，不足8字符的补满8字符
        /// </summary>
        /// <param name="strString">The STR string.</param>
        /// <returns></returns>
        private string GetString(string strString)
        {
            if (strString.Length < 8)
                return strString.PadRight(8, paddingChar);
            return strString;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="strString">加密字符串</param>
        /// <returns></returns>
        public string Encrypt(string strString)
        {
            try
            {
                strString = this.GetString(strString);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = Encoding.UTF8.GetBytes(this.GetKeyCode(this.Keys));
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.Zeros;
                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                byte[] Buffer = Encoding.UTF8.GetBytes(strString);
                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="strString">解密字符串</param>
        /// <returns></returns>
        public string Decrypt(string strString)
        {
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = Encoding.UTF8.GetBytes(this.GetKeyCode(this.Keys));
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.Zeros;
                ICryptoTransform DESDecrypt = DES.CreateDecryptor();
                byte[] Buffer = Convert.FromBase64String(strString);
                return UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)).Replace("\0", "").Trim();
            }
            catch (Exception ex) { return ex.Message; }
        }

        #region DESEnCode DESDeCode
        /// <summary>
        /// 加密 与Java通用加密
        /// </summary>
        /// <param name="pToEncrypt">需要加密的字符</param>
        /// <param name="cryptKey">密钥，8位的ASCII字符</param>
        /// <returns></returns>
        public string DESEnCode(string pToEncrypt)
        {
            if (string.IsNullOrEmpty(pToEncrypt)) return string.Empty;

            try
            {
                pToEncrypt = System.Web.HttpUtility.HtmlEncode(pToEncrypt);
                string key = this.Keys;

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(pToEncrypt);
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }

                cs.Close();
                cs.Dispose();
                ms.Close();
                ms.Dispose();

                return ret.ToString();
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }

        }
        /// <summary> 
        /// 解密数据  与Java通用解密
        /// </summary> 
        /// <param name="pToEncrypt">解密的字符</param>
        /// <param name="cryptKey">密钥，8位的ASCII字符</param>
        /// <returns></returns> 
        public string DESDeCode(string pToEncrypt)
        {
            if (string.IsNullOrEmpty(pToEncrypt)) return string.Empty;

            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                int len = pToEncrypt.Length / 2;
                byte[] inputByteArray = new byte[len];
                int x, i;

                for (x = 0; x < len; x++)
                {
                    i = Convert.ToInt32(pToEncrypt.Substring(x * 2, 2), 16);
                    inputByteArray[x] = (byte)i;
                }

                string key = this.Keys;

                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                string ret = System.Web.HttpUtility.HtmlDecode(System.Text.Encoding.Default.GetString(ms.ToArray()));

                cs.Close();
                cs.Dispose();
                ms.Close();
                ms.Dispose();

                return ret;
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
