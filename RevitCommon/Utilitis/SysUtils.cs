using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public static class SysUtils
    {
        /// <summary>
        /// 获取当前进程的内存使用量
        /// </summary>
        /// <returns>MB</returns>
        public static double GetMemory()
        {
            return Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024;
        }

        /// <summary>
        /// 获取方法体的运行时常(ms)
        /// </summary>
        /// <param name="action">方法体</param>
        /// <returns>运行时间ms</returns>
        public static double GetRunTime(Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();
            action.Invoke();
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }
    }
}
