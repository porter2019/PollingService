using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Litdev.MongoDB
{
    /// <summary>
    /// 演示实体
    /// </summary>
    public class EntityDemo : BaseEntity
    {
        public string name { get; set; }

        public int age { get; set; }
        public decimal amount { get; set; }
        
        [BsonDateTimeOptions(Kind =DateTimeKind.Local)]
        public DateTime brithday { get; set; }

        public EntityDemo() { }
        public EntityDemo(string name,int age,decimal amount,DateTime brithday)
        {
            this.name = name;
            this.age = 10;
            this.amount = amount;
            this.brithday = brithday;
        }

    }
}
