using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Litdev.Service
{
    /// <summary>
    /// 自定义日志输出
    /// </summary>
    public class LitdevTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Trace.Write(message);
        }

        public override void WriteLine(string message)
        {
            string file_name = "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            string server_path = "\\logs\\";
            string wl_path = System.Threading.Thread.GetDomain().BaseDirectory + server_path;
            if (!Directory.Exists(wl_path))
                Directory.CreateDirectory(wl_path); //如果没有该目录，则创建
            StreamWriter sw = new StreamWriter(wl_path + file_name, true, Encoding.UTF8);
            DateTime dt = DateTime.Now;
            sw.WriteLine("**************************" + dt.ToString() + " begin **************************");
            sw.WriteLine(message);
            sw.WriteLine("/*************************" + dt.ToString() + " end **************************/");
            sw.Close();
        }
    }
}
