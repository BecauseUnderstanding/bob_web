
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Common;

namespace Common.CommonFun
{
    public static class EnumExtend
    {

        public static string GetEnumDescription(object e)
        {

            return "";
        }

        /// <summary>
        /// 将枚举名称转换为下拉选择数据
        /// </summary>
        /// <returns></returns>
        public static List<SelectedTree> EnumTreeData(this Type enumType)
        {
            var data = new List<SelectedTree>();

            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.GetField | BindingFlags.DeclaredOnly | BindingFlags.Static;


            foreach (FieldInfo field in enumType.GetFields(bindingFlags))
            {
                var fieldValue = field.GetRawConstantValue().ToString();

                var filedName = GetDisplayName(field);
                var item = new SelectedTree()
                {
                    Id = fieldValue,
                    Name = filedName,
                    ParentId = "0"
                };


                data.Add(item);

            }
            return data;

        }


        private static string GetDisplayName(FieldInfo field)
        {
            var display = field.GetCustomAttribute<DisplayAttribute>(inherit: false);
            if (display != null)
            {
                string name = display.GetName();
                if (!String.IsNullOrEmpty(name))
                {
                    return name;
                }
            }

            return field.Name;
        }


    }
}
