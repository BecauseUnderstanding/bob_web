using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Common.Enum;

namespace Common.CommonFun
{
    public class FileHelper
    {
        /// <summary>
        /// 下载流文件
        /// </summary>
        /// <param name="buffer">二进制流</param>
        /// <param name="fileName"></param>
        public static void Download(byte[] buffer, string fileName)
        {
            HttpContext httpContext = HttpContext.Current;
            httpContext.Response.Clear();
            //字节流形式下载文件
            httpContext.Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开  
            httpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            httpContext.Response.BinaryWrite(buffer);
            httpContext.Response.Flush();
            httpContext.Response.End();

        }



        /// <summary>
        /// 普通下载
        /// </summary>
        /// <param name="filePath"></param>
        private static void DownloadByNormal(string filePath)
        {
            HttpContext context = HttpContext.Current;
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                context.Response.Clear();
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                //设置文件下载方式 attachment 附件形式下载
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileInfo.Name));
                //设置传输编码 binary 二进制方式
                context.Response.AddHeader("Content-Transfer-Encoding", "binary");
                context.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
                context.Response.WriteFile(fileInfo.Name);
                context.Response.Flush();
                context.Response.End();
            }
        }

        /// <summary>
        /// 分块下载
        /// </summary>
        /// <param name="filePath"></param>
        private static void DownloadByChunk(string filePath)
        {
            HttpContext context = HttpContext.Current;
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                const long chunkSize = 102400;//限定读取文件大小，缓解服务器压力
                byte[] buffer = new byte[chunkSize];

                context.Response.Clear();
                FileStream stream = File.OpenRead(filePath);
                long dataLengthToRead = stream.Length;//获取文件总长度
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileInfo.Name));
                while (dataLengthToRead > 0 && context.Response.IsClientConnected)
                {
                    int lengthRead = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));//读取大小
                    context.Response.OutputStream.Write(buffer, 0, lengthRead);
                    context.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                context.Response.Close();
            }
        }

        /// <summary>
        /// 流下载
        /// </summary>
        /// <param name="filePath"></param>
        private static void DownloadByStream(string filePath)
        {
            HttpContext context = HttpContext.Current;
            FileInfo fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                FileStream fs = new FileStream(filePath,FileMode.Open);
                byte[] buffer = new byte[(int)fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Disposition","attachment;filename="+HttpUtility.UrlEncode(fileInfo.Name));
                //将字节数组写入http输出流
                context.Response.BinaryWrite(buffer);
                context.Response.Flush();
                context.Response.End();
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="option">下载方式</param>
        public static void Download(string filePath,DownloadOption option=DownloadOption.Narmol)
        {
            switch (option)
            {
                case DownloadOption.Narmol:
                    DownloadByNormal(filePath);
                    break;
                case DownloadOption.Chunk:
                    DownloadByChunk(filePath);
                    break;
                case DownloadOption.Stream:
                    DownloadByStream(filePath);
                    break;
                default:
                    DownloadByNormal(filePath);
                    break;
            }
        }


    }
}
