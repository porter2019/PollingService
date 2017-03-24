using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Litdev.MongoDB
{
    public class DALDemo : BaseDAL<EntityDemo>, IBaseDAL<EntityDemo>
    {
        public DALDemo()
        {
            this.collectionName = "demo";
        }



    }
}
