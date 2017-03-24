using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;

namespace Litdev.Service.Controller
{
    public class TestController: ApiController
    {
        [Route("api/test/a")]
        [HttpGet]
        public APIResponseEntity<string> GetInfo()
        {
            var ss = Thread.CurrentPrincipal.Identity;

            APIResponseEntity<string> response_entity = new APIResponseEntity<string>();
            response_entity.msg = 1;
            response_entity.msgbox = "线程标识是：" + ss.Name;
            
            return response_entity;
        }
    }
}
