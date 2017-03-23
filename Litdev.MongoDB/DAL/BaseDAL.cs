using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace Litdev.MongoDB
{
    public partial class BaseDAL<T> : IBaseDAL<T> where T : BaseEntity, new()
    {
        #region 属性字段

        private string entitysname;

        /// <summary>
        /// 集合名称
        /// </summary>
        public string entitysName
        {
            get { return entitysname; }
            set { entitysname = value; }
        }

        #endregion

        public BaseDAL() { }

        /// <summary>
        /// 指定集合名字
        /// </summary>
        /// <param name="collectionName"></param>
        public BaseDAL(string collectionName)
        {
            this.entitysname = collectionName;
        }

        public T obj;

        public BaseDAL(T obj)
        {
            this.obj = obj;
        }


        /// <summary>
        /// 根据数据库配置信息创建MongoDatabase对象，如果不指定配置信息，则从默认信息创建
        /// </summary>
        /// <returns></returns>
        public virtual IMongoDatabase CreateDatabase()
        {
            //从配置文件中获取对应的连接信息
            string connectionString = ConfigurationManager.AppSettings["MongoDBConn"];
            var client = new MongoClient(connectionString);
            string dbName = ConfigurationManager.AppSettings["MongoDBName"];
            var database = client.GetDatabase(dbName);
            return database;
        }
        

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T FindByID(string id)
        {
            IMongoCollection<T> collection = GetCollection();
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", new ObjectId(id));
            var data = collection.Find<T>(filter).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// 根据自定义条件查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public T FindSingle(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();

            return collection.Find(filter).FirstOrDefault();
        }

        /// <summary>
        /// 根据条件查询多条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<T> Find(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();

            return collection.Find(filter).ToList();
        }

        /// <summary>
        /// 获取操作对象的IMongoCollection集合,强类型对象集合
        /// </summary>
        /// <returns></returns>
        public virtual IMongoCollection<T> GetCollection()
        {
            var database = CreateDatabase();
            return database.GetCollection<T>(this.entitysName);
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="t"></param>
        public virtual void Insert(T t)
        {
            IMongoCollection<T> collection = GetCollection();
            collection.InsertOne(t);
        }

        /// <summary>
        /// 插入多条
        /// </summary>
        /// <param name="list"></param>
        public virtual void InsertBatch(IEnumerable<T> list)
        {
            IMongoCollection<T> collection = GetCollection();
            collection.InsertMany(list);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public virtual bool Update(string id, UpdateDefinition<T> update)
        {
            IMongoCollection<T> collection = GetCollection();
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", new ObjectId(id));
            var result = collection.UpdateOne(filter, update, new UpdateOptions() { IsUpsert = true });
            return result != null && result.ModifiedCount > 0;
        }

        public virtual bool Update(T t, string id)
        {
            bool result = false;
            IMongoCollection<T> collection = GetCollection();
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", new ObjectId(id));
            var update = collection.ReplaceOne(filter, t, new UpdateOptions() { IsUpsert = true });
            result = update != null && update.ModifiedCount > 0;
            return result;
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(string id)
        {
            IMongoCollection<T> collection = GetCollection();
            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq("Id", new ObjectId(id));
            var result = collection.DeleteOne(filter);
            return result != null && result.DeletedCount > 0;
        }

        public virtual bool DeleteBatch(List<ObjectId> idList)
        {
            IMongoCollection<T> collection = GetCollection();

            var filterBuilder = Builders<T>.Filter;

            var filter = filterBuilder.In("Id", idList);
            var result = collection.DeleteMany(filter);
            return result != null && result.DeletedCount > 0;
        }

        public virtual bool DeleteByQuery(FilterDefinition<T> query)
        {
            IMongoCollection<T> collection = GetCollection();
            var result = collection.DeleteMany(query);
            return result != null && result.DeletedCount > 0;
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public long GetCount(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            return collection.Count(filter);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public bool Exists(FilterDefinition<T> filter)
        {
            IMongoCollection<T> collection = GetCollection();
            return collection.Count(filter) > 0;
        }
    }
}
