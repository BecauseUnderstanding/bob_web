using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum DownloadOption
    {
        [Display(Name = "普通下载")]
        Narmol=0,
        [Display(Name = "分块下载")]
        Chunk=1,
        [Display(Name = "流文件下载")]
        Stream=2
    }
}
