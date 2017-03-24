using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Litdev.MongoDB
{
    /// <summary>
    /// 机器实体
    /// </summary>
    public class EntityMachine : BaseEntity
    {
        public EntityMachine()
        {
            this.add_time = DateTime.Now;
            this.last_visitor_time = DateTime.Now;
        }

        /// <summary>
        /// 机器码，唯一标识
        /// </summary>
        public string machine_id { get; set; }

        /// <summary>
        /// 机器备注
        /// </summary>
        public string machine_remark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime add_time { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime last_visitor_time { get; set; }

    }
}
