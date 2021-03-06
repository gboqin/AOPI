﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NH.JewelryErpMini.Helpers
{
    public class SetValueHelper
    {
        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <typeparam name="T">实体类型的泛型</typeparam>
        /// <param name="ob">实体</param>
        /// <param name="propertyInfo">属性</param>
        /// <param name="fieldValue">属性值</param>
        public static void SetValue<T>(T ob, PropertyInfo propertyInfo, string fieldValue)
        {

            if (IsType(propertyInfo.PropertyType, "System.String"))
            {
                propertyInfo.SetValue(ob, fieldValue, null);
            }

            if (IsType(propertyInfo.PropertyType, "System.Boolean") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Boolean]"))
            {
                propertyInfo.SetValue(ob, Boolean.Parse(fieldValue), null);
            }

            if (IsType(propertyInfo.PropertyType, "System.Int32") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Int32]"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(ob, int.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(ob, 0, null);
            }

            if (IsType(propertyInfo.PropertyType, "System.Decimal") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Decimal]"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(ob, Decimal.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(ob, new Decimal(0), null);
            }

            if (IsType(propertyInfo.PropertyType, "System.DateTime") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.DateTime]"))
            {
                if (fieldValue != "")
                {
                    propertyInfo.SetValue(ob, Convert.ToDateTime(fieldValue), null);
                }
                else propertyInfo.SetValue(ob, null, null);

            }

        }

        /// <summary>
        /// 设置相应属性的值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldName">属性名</param>
        /// <param name="fieldValue">属性值</param>
        public static void SetValue(object entity, string fieldName, string fieldValue)
        {
            Type entityType = entity.GetType();

            PropertyInfo propertyInfo = entityType.GetProperty(fieldName);

            if (IsType(propertyInfo.PropertyType, "System.String"))
            {
                propertyInfo.SetValue(entity, fieldValue, null);

            }

            if (IsType(propertyInfo.PropertyType, "System.Boolean") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Boolean]"))
            {
                propertyInfo.SetValue(entity, Boolean.Parse(fieldValue), null);

            }

            if (IsType(propertyInfo.PropertyType, "System.Int32") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Int32]"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, int.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, 0, null);

            }

            if (IsType(propertyInfo.PropertyType, "System.Decimal") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.Decimal]"))
            {
                if (fieldValue != "")
                    propertyInfo.SetValue(entity, Decimal.Parse(fieldValue), null);
                else
                    propertyInfo.SetValue(entity, new Decimal(0), null);

            }

            if (IsType(propertyInfo.PropertyType, "System.DateTime") || IsType(propertyInfo.PropertyType, "System.Nullable`1[System.DateTime]"))
            {
                if (fieldValue != "")
                {
                    try
                    {
                        propertyInfo.SetValue(
                             entity,
                             (DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd HH:mm:ss", null), null);
                    }
                    catch
                    {
                        propertyInfo.SetValue(entity, (DateTime?)DateTime.ParseExact(fieldValue, "yyyy-MM-dd", null), null);
                    }
                }
                else
                    propertyInfo.SetValue(entity, null, null);

            }

        }
        /// <summary>
        /// 类型匹配
        /// </summary>
        /// <param name="type"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsType(Type type, string typeName)
        {
            if (type.ToString() == typeName)
                return true;
            if (type.ToString() == "System.Object")
                return false;

            return IsType(type.BaseType, typeName);
        }
    }
}