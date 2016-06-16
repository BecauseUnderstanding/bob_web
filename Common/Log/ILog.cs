using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
   public interface ILog
    {
       /// <summary>
       /// 写日志
       /// </summary>
       /// <param name="level">日志级别</param>
       /// <param name="title">日志标题</param>
       /// <param name="appId">应用程序id</param>
       /// <param name="ip">IP地址</param>
       /// <param name="userid">用户id</param>
       /// <param name="createTime">创建时间</param>
       /// <param name="total">参数</param>
       /// <param name="message">日志内容</param>
       /// <param name="source">来源</param>
       /// <param name="stackTrace">堆栈信息</param>
        void WriteLog(LogLevel level,string title,int appId,string ip,int userid,DateTime createTime,string total,string message,string source,string stackTrace);
    }
}
