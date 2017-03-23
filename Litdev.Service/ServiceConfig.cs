using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Litdev.Tools;

namespace Litdev.Service
{
    /// <summary>
    /// 服务配置文件
    /// </summary>
    public class ServiceConfig
    {
        /// <summary>
        /// 3DES密钥
        /// </summary>
        public static readonly string DES3KEY = ConfigurationManager.AppSettings["DES3KEY"].ToString();

        /// <summary>
        /// Web API使用的端口
        /// </summary>
        public static readonly int WebAPIPort = TypeHelper.ObjectToInt(ConfigurationManager.AppSettings["WebAPIPort"].ToString());

        /// <summary>
        /// MongoDB连接字符串
        /// </summary>
        public static readonly string MongoDBConn = ConfigurationManager.AppSettings["MongoDBConn"].ToString();

        /// <summary>
        /// MongoDB使用的数据库名字
        /// </summary>
        public static readonly string MongoDBName = ConfigurationManager.AppSettings["MongoDBName"].ToString();

    }
}
