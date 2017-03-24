using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Litdev.MongoDB;

namespace Litdev.Tests
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            DALQueue dal = new DALQueue();
            //EntityQueue entity = new EntityQueue();
            //entity.avatar_url = "https://gss0.bdstatic.com/6LZ1dD3d1sgCo2Kml5_Y_D3/sys/portrait/item/46e96c69746465766a5d";
            //entity.machine_id = "a12345";
            //entity.open_id = "open123";
            //entity.resource_id = "4";
            //entity.user_name = "单元测试";
            //string msg = "";
            //var is_ok = dal.AddQueue(entity, out msg);
            //System.Diagnostics.Debug.WriteLine(msg);
            //Assert.AreEqual(true, is_ok);

            //bool is_exists = dal.ExistsOpenID("a12345", "open123");
            //Assert.AreEqual(true, is_exists);


            var list = dal.GetTopQueueList(3, "a12345");
            Assert.AreEqual(3, list.Count);

            //var is_ok = dal.UserDone("58d4884c6cd8332a1042ad72");
            //Assert.AreEqual(true, is_ok);
        }
    }
}
