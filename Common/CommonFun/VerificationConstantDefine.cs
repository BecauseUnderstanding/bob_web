using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonFun
{
    public class VerificationConstantDefine
    {
        /// <summary>
        ///  非空支付串
        /// </summary>
        public const string NonEmptyPatten = @"^\s*$";

        /// <summary>
        /// 验证汉字
        /// </summary>
        public const string ChinesePatten = @"^[\u4e00-\u9fa5]{0,}$";

        /// <summary>
        /// 数字验证
        /// </summary>
        public const string NumberPatten = @"^[-]?[0-9]*$";

        /// <summary>
        /// 正整数验证
        /// </summary>
        public const string PositiveIntegerPatten = @"^[1-9]?[0-9]*$";

        /// <summary>
        /// 邮件验证
        /// </summary>
        public const string EmailPatten = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        /// <summary>
        /// 手机号码验证
        /// </summary>
        public const string TelPhonelPatten = @"^([0\+]\d{2,3}-)?(\d{11})$";


        /// <summary>
        /// 时间验证格式(yyyy-MM-dd  HH:mm:ss)或者是yyyy/MM/dd  HH:mm:ss
        /// </summary>
        public const string DateTimePatten = @"^[1-9][0-9]{3}[-//][0-9]{2}[-//][0-9]{2}\s+[0-9]{2}:[0-9]{2}:[0-9]{2}$";

        /// <summary>
        /// 日期验证(yyyy-MM-dd)或者是yyyy/MM/dd
        /// </summary>
        public const string DatePatten = @"^[1-9][0-9]{3}[-//][0-9]{2}[-//][0-9]{2}";


        /// <summary>
        /// Url验证
        /// </summary>
        public const string UrlPatten = @"^http|https:\S+\.{1}\S+$";

        /// <summary>
        /// 英语验证
        /// </summary>
        public const string EnglisPatten = @"^[A-Za-z]+$";

        /// <summary>
        /// double验证
        /// </summary>
        public const string DoublePatten = @"^[-]?\d+(\.\d+)?$";

        /// <summary>
        /// 身份证验证 
        /// </summary>
        public const string IdCard = @"\d{15}|\d{17}[0-9Xx]";

        /// <summary>
        /// 身份证后六位
        /// </summary>
        public const string IdCardEndVerify = @"\d{5}[0-9Xx]";
    }
}
