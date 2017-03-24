using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Litdev.Service.Models
{
    /// <summary>
    /// 注册机器所需参数
    /// </summary>
    public class ModelRegMachine
    {
        /// <summary>
        /// 机器码
        /// </summary>
        public string mach_id { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

    }
}
