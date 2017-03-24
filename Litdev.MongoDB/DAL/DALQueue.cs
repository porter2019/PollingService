using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Litdev.MongoDB
{
    /// <summary>
    /// 队列操作
    /// </summary>
    public class DALQueue : BaseDAL<EntityQueue>, IBaseDAL<EntityQueue>
    {

        public DALQueue()
        {
            this.collectionName = "queue";
        }

        /// <summary>
        /// 添加队列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool AddQueue(EntityQueue entity,out string msg)
        {
            msg = "非法参数";
            if (entity == null)
                return false;
            entity.add_time = DateTime.Now;
            DALMachine dal_mac = new DALMachine();
            if(!dal_mac.Exists(Builders<EntityMachine>.Filter.Eq("machine_id", entity.machine_id)))
            {
                msg = "机器不存在";
                return false;
            }
            Insert(entity);
            return true;
        }
        
        /// <summary>
        /// 判断用户是否在队列中
        /// </summary>
        /// <param name="machine_id">机器码</param>
        /// <param name="open_id">用户开放id</param>
        /// <returns></returns>
        public bool ExistsOpenID(string machine_id,string open_id)
        {
            var filter = Builders<EntityQueue>.Filter;
            //filter.Eq("machine_id", machine_id) & filter.Eq("open_id", open_id)
            return Exists(filter.And(filter.Eq("machine_id", machine_id), filter.Eq("open_id", open_id)));
        }

        /// <summary>
        /// 完成任务,更新一列
        /// </summary>
        /// <param name="object_id"></param>
        /// <returns></returns>
        public bool UserDone(string id)
        {
            var filterBuilder = Builders<EntityQueue>.Update.Set("is_complete", true);
            return Update(id, filterBuilder);
        }

        /// <summary>
        /// 获取指定数量的队列(试戴机需要保存object_id)
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<EntityQueue> GetTopQueueList(int top,string machine_id)
        {
            if(top<1 || top > 100)
                top = 5;
            var filter = Builders<EntityQueue>.Filter;
            //var query = filter.And(filter.Eq("machine_id", machine_id), filter.Eq("is_complete", false));
            //另一种方法
            var query = filter.Eq("machine_id", machine_id) & filter.Eq("is_complete", false);

            IMongoCollection<EntityQueue> collection = GetCollection();
            var data = collection.Find(query).Limit(top).SortBy(p => p.add_time).ToList();
            return data;
        }

    }
}
