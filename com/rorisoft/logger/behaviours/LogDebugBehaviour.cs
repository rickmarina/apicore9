using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.rorisoft.log2.behaviours
{
    public class LogDebugBehaviour : ILogBehaviour
    {
        public int log(ILogData data)
        {
            Debug.WriteLine($"{data.className} | {DateTime.Now.ToString("yyyyMMdd HH:mm:ss")} | method {data.method} > {data.message} ");
            return 0;
        }

        public int log(string msg, ILogData data)
        {
            data.message = msg; 
            return log(data);
        }

    }
}
