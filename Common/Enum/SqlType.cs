using System.ComponentModel.DataAnnotations;

namespace Common.Enum
{
    public enum SqlType
    {
        [Display(Name = "添加")]
        Insert = 0,
        [Display(Name = "更新")]
        Update = 1,
        [Display(Name = "删除")]
        Delete = 2
    }
}
