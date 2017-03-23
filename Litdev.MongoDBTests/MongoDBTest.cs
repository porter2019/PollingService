using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litdev.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Litdev.Tests
{
    [TestClass]
    public class MongoDBTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DALDemo dal = new DALDemo();

            //EntityDemo entity = new EntityDemo();
            //entity.age = 20;
            //entity.amount = 20.22M;
            //entity.brithday = DateTime.Now.AddYears(-20);
            //entity.name = "张三";
            //dal.Insert(entity);
            //var total = dal.GetCount(Builders<EntityDemo>.Filter.Eq("name", "litdev"));
            //Assert.AreEqual(total, 1);

            //var demo = dal.FindSingle(Builders<EntityDemo>.Filter.Eq("name", "litdev"));

            //var demo_list = dal.Find(Builders<EntityDemo>.Filter.Gte("age", 10));


            #region 基础查询语法

            //Query.All("name", "a", "b");//通过多个元素来匹配数组

            //Query.And(Query.EQ("name", "a"), Query.EQ("title", "t"));//同时满足多个条件

            //Query.EQ("name", "a");//等于

            //Query.Exists("type", true);//判断键值是否存在

            //Query.GT("value", 2);//大于>

            //Query.GTE("value", 3);//大于等于>=

            //Query.In("name", "a", "b");//包括指定的所有值,可以指定不同类型的条件和值

            //Query.LT("value", 9);//小于<

            //Query.LTE("value", 8);//小于等于<=

            //Query.Mod("value", 3, 1);//将查询值除以第一个给定值,若余数等于第二个给定值则返回该结果

            //Query.NE("name", "c");//不等于

            //Query.Nor(Array);//不包括数组中的值

            //Query.Not("name");//元素条件语句

            //Query.NotIn("name", "a", 2);//返回与数组中所有条件都不匹配的文档

            //Query.Or(Query.EQ("name", "a"), Query.EQ("title", "t"));//满足其中一个条件

            //Query.Size("name", 2);//给定键的长度

            //Query.Type("_id", BsonType.ObjectId);//给定键的类型

            //Query.Where(BsonJavaScript);//执行JavaScript

            //Query.Matches("Title", str);//模糊查询 相当于sql中like -- str可包含正则表达式 
            #endregion
        }
    }
}
