using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Log
{
    public abstract class BaseLog : IDisposable, ILog
    {
        //线程安全队列
        public static ConcurrentQueue<LogEntity> LogQueueCache = new ConcurrentQueue<LogEntity>();

        public static Thread DeamonThread;
        [ThreadStatic]
        private static LogEntity LastLog;
        [ThreadStatic]
        private static DateTime LastLogTime;

        private static object locker = new object();

        protected BaseLog()
        {
            if (DeamonThread==null)
            {
                lock (locker)
                {
                    if (DeamonThread == null)
                    {
                        lock (locker)
                        {
                            Thread.MemoryBarrier();
                            DeamonThread=new Thread(r =>
                            {
                                LogWrirtePorcess();
                            });
                            DeamonThread.Start();
                        }
                    }
                }
            }
        }



        protected abstract void Log(LogEntity entity);
        /// <summary>
        /// 处理日志队列中的日志
        /// </summary>
        private void LogWrirtePorcess()
        {
            while (true)
            {
                if (LogQueueCache.Count > 0)
                {
                    LogEntity log = null;
                    try
                    {
                        if (LogQueueCache.TryPeek(out log))
                        {
                            Log(log);
                        }
                        LogQueueCache.TryDequeue(out log);
                    }
                    catch (Exception)
                    {
                        if (LogQueueCache.Count > 0)
                        {
                            LogQueueCache.TryDequeue(out log);
                        }
                        Thread.Sleep(300);
                    }
                }
                else
                {
                    Thread.Sleep(300);
                }
            }
        }

        public void WriteLog(LogLevel level, string title, int appId, string ip, int userid, DateTime createTime, string total, string message, string source, string stackTrace)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 析构
        /// </summary>
        ~BaseLog()
        {
            try
            {
                if (DeamonThread != null && DeamonThread.ThreadState == ThreadState.Running)
                {
                    //  deamonThread.Abort();
                }
            }
            catch
            {
            }
        }

        //垃圾回收
        public void Dispose()
        {
            GC.Collect();
        }


    }
}
