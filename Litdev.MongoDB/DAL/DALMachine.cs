using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Litdev.MongoDB
{
    public class DALMachine : BaseDAL<EntityMachine>, IBaseDAL<EntityMachine>
    {
        public DALMachine()
        {
            this.collectionName = "machine";
        }

        /// <summary>
        /// 添加一台机器
        /// </summary>
        /// <returns></returns>
        public bool AddMachine(string machine_id,string remark)
        {
            if(Exists(Builders<EntityMachine>.Filter.Eq("machine_id",machine_id)))
                return false;

            EntityMachine entity = new EntityMachine();
            entity.add_time = DateTime.Now;
            entity.last_visitor_time = DateTime.Now;
            entity.machine_id = machine_id;
            entity.machine_remark = remark;
            Insert(entity);
            return true;
        }
    }
}
