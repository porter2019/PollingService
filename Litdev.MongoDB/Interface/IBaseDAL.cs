using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace Litdev.MongoDB
{
    interface IBaseDAL<T> where T : BaseEntity, new()
    {
        IMongoDatabase CreateDatabase();
        IMongoCollection<T> GetCollection();

        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        long GetCount(FilterDefinition<T> filter);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        bool Exists(FilterDefinition<T> filter);

        T FindByID(string id);

        T FindSingle(FilterDefinition<T> filter);

        /// <summary>
        /// 查询多个
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter);


        void Insert(T t);
        //批量插入
        void InsertBatch(IEnumerable<T> list);
        //整个替换更新
        bool Update(T t, string id);
        //部分列更新
        bool Update(string id, UpdateDefinition<T> update);
        //根据指定对象的ID，从数据库中删除指定对象
        bool Delete(string id);
        //批量删除
        bool DeleteBatch(List<ObjectId> idList);
        //根据指定条件，从数据库中删除指定对象
        bool DeleteByQuery(FilterDefinition<T> query);

    }
}
