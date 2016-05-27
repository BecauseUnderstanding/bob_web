using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Log
{
    public enum LogLevel
    {
        [Display(Name = "操作日志")]
        Info,
        [Display(Name = "警告日志")]
        Warn,
        [Display(Name = "错误日志")]
        Error,
        [Display(Name = "调试日志")]
        Debug
    }
}
