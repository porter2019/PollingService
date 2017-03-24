using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Litdev.MongoDB
{
    /// <summary>
    /// 用户队列
    /// </summary>
    public class EntityQueue : BaseEntity
    {
        public EntityQueue()
        {
            this.add_time = DateTime.Now;
        }

        /// <summary>
        /// 机器码，唯一标识
        /// </summary>
        public string machine_id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string avatar_url { get; set; }

        /// <summary>
        /// 用户开放ID
        /// </summary>
        public string open_id { get; set; }

        /// <summary>
        /// 使用的资源标识(哪个短片)
        /// </summary>
        public string resource_id { get; set; }
        
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool is_complete { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime add_time { get; set; }

    }
}
