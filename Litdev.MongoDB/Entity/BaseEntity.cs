using MongoDB.Bson;

namespace Litdev.MongoDB
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 基类对象的ID，MongoDB要求每个实体类必须有的主键
        /// </summary>
        public ObjectId Id { get; set; }
    }
}
