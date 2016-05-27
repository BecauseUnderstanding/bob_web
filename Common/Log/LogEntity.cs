using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Log
{
    /// <summary>
    /// 日志实体
    /// </summary>
    public class LogEntity
    {
        /// <summary>
        /// 日志标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 应用程序id
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
    /// <summary>
    /// 操作信息日志实体
    /// </summary>
    public class InfoLogEntity : LogEntity
    {
        public string Message { get; set; }
    }
    /// <summary>
    /// 警告信息日志实体
    /// </summary>
    public class WraningLogEntity : LogEntity
    {
        public string Message { get; set; }
    }
    /// <summary>
    /// 错误信息日志实体
    /// </summary>
    public class ErrorLogEntity : LogEntity
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 日志来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 堆栈信息
        /// </summary>
        public string StackTrace { get; set; }

    }
    /// <summary>
    /// 调试信息日志实体
    /// </summary>
    public class DebugLogEntity : LogEntity
    {
        public string Total { get; set; }

        public string Message { get; set; }
    }

}
