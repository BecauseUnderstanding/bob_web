using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.CommonFun
{
    public delegate bool ValidateMethod();


    /***业务逻辑验证功能***/
    public static class ModelValidate
    {
        #region 验证逻辑控制
        /// <summary>
        /// 判断是否继续验证，如果True则继续验证，false则跳出验证
        /// </summary>
        /// <param name="res"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool GoOn(this bool res, ValidateMethod action)
        {
            if (res)
            {
                var a = action();
                return a;
            }
            else
            {
                return false;
            }
        }


        private static void AppendWithEnter(string errorMessage, StringBuilder outMessage)
        {
            outMessage.Append(errorMessage).Append(";");
        }



        #endregion



        #region 值类型验证

        #region Greater Than

        public static bool Gt(this int strValue, int inputValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.Gt(inputValue);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }
        public static bool Gt(this int strValue, int inputValue)
        {
            return strValue > inputValue ? true : false;
        }
        #endregion

        #region Less Than

        public static bool Lt(this int strValue, int inputValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.Lt(inputValue);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool Lt(this int strValue, int inputValue)
        {
            return strValue < inputValue ? true : false;
        }

        #endregion


        #region Great Than Or Equal To

        public static bool GtOrEqual(this int strValue, int inputValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.GtOrEqual(inputValue);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool GtOrEqual(this int strValue, int inputValue)
        {
            return strValue >= inputValue ? true : false;
        }

        #endregion


        #region Less Than Or Equal To


        public static bool LtOrEqual(this int strValue, int inputValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.LtOrEqual(inputValue);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool LtOrEqual(this int strValue, int inputValue)
        {
            return strValue <= inputValue ? true : false;
        }

        #endregion



        #region !Zero !Empty

        public static bool HasValue(this int strValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.HasValue();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool HasValue(this int strValue)
        {
            return strValue == 0 ? false : true;
        }

        public static bool HasValue(this double strValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.HasValue();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool HasValue(this double strValue)
        {
            return strValue == 0 ? false : true;
        }

        #endregion
        #endregion

        #region 字符串验证

        #region String


        public static bool HasValue(this string strMessage, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.HasValue();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool HasValue(this string messageValue)
        {
            if (string.IsNullOrEmpty(messageValue.Trim()))
                return false;
            return true;
        }
        #endregion

        #region DateTime?



        public static bool HasValue(this DateTime? strMessage, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.HasValue();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool HasValue(this DateTime? strMessage)
        {
            return strMessage == DateTime.MinValue ? false : true;
        }
        #endregion

        #region MaxLength

        public static bool MaxLength(this string strMessage, int length, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.MaxLength(length);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool MaxLength(this string strMessage, int length)
        {
            return strMessage.Length > length ? false : true;
        }

        #endregion

        #region MinLength

        public static bool MinLength(this string strMessage, int length, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.MinLength(length);
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool MinLength(this string strMessage, int length)
        {
            return strMessage.Length < length ? false : true;
        }


        #endregion

        #endregion


        #region 正则验证

        #region 正整数验证

        public static bool IsNum(this string strValue, string errorMessage, StringBuilder outMessage)
        {
            var res = strValue.IsNum();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool IsNum(this string strValue)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strValue, @"^[0-9]*[1-9][0-9]*$");
        }

        #endregion

        #region Email验证

        public static bool CheckEamil(this string strMessage, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.CheckEamil();
            if (!res)
                AppendWithEnter(errorMessage, outMessage);
            return res;
        }

        public static bool CheckEamil(this string strMessage)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strMessage, @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}");
        }

        #endregion

        #region 邮箱验证

        public static bool CheckMailbox(this string strMessage, string errorMessage, StringBuilder outMessage)
        {
            var res = strMessage.CheckMailbox();
            if (!res)
                AppendWithEnter(strMessage, outMessage);
            return res;
        }

        public static bool CheckMailbox(this string strMessage)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(strMessage,
                @"^[A-Za-zd]+([-_.][A-Za-zd]+)*@([A-Za-zd]+[-.])+[A-Za-zd]{2,5}$ ");
        }

        #endregion

        #endregion





    }
}
