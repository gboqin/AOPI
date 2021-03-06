﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace NH.JewelryErpMini.Helpers
{
    public static class DataTableHelper
    {

        /// <summary>  
        /// 转化一个DataTable  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合  
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口  
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列  
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例  
                DataRow row = dt.NewRow();
                //给row 赋值  
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable  
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// DataTable 转换为List 集合  
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <param name="ToListColumns">dt列名与list列名对应字典</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt, Dictionary<string, string> ToListColumns) where T : class, new()
        {
            //创建一个属性的列表  
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口  
            Type t = typeof(T);

            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表   
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(ToListColumns[p.Name]) != -1) prlist.Add(p); });

            //创建返回的集合  
            List<T> oblist = new List<T>();
            string cName = "";
            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据 并赋值
                prlist.ForEach(p =>
                {
                    cName = ToListColumns[p.Name];
                    if (row[cName] != DBNull.Value)
                    {   //为属性赋值，并转换键值的类型为该属性的类型
                        p.SetValue(ob, Convert.ChangeType(row[cName], p.PropertyType), null);
                    }
                });
                if (ob != null)
                {
                    oblist.Add(ob);
                }
            }
            return oblist;
        }

        /// <summary>
        /// DataTable 转换为List 集合  
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <param name="ToListColumns">dt列名与list列名对应字典</param>
        /// <returns></returns>
        public static List<T> ToListByContains<T>(this DataTable dt, Dictionary<string, string> ToListColumns) where T : class, new()
        {
            //获取TResult的类型实例  反射的入口  
            Type t = typeof(T);
            //创建一个属性的列表  
            List<PropertyInfo> prlist = new List<PropertyInfo>();

            //获得TResult 的所有的Public 属性(PropertyInfo) 并加入到属性列表   
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => prlist.Add(p));
            //创建返回的集合  
            List<T> oblist = new List<T>();
            //列名
            string cName = "";
            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据 并赋值
                prlist.ForEach(p =>
                {
                    //取得列名
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.Caption.Contains(ToListColumns[p.Name].ToLower()))
                        {
                            cName = column.Caption;
                        }
                    }
                    if ((row[cName] != DBNull.Value) && (row[cName].ToString() != ""))
                    {   //为属性赋值，并转换键值的类型为该属性的类型
                        //p.SetValue(ob, Convert.ChangeType(row[cName], p.PropertyType), null);
                        //if (SetValueHelper.IsType(p.PropertyType, "System.Boolean"))
                        //{
                        //    p.SetValue(ob, Convert.ToBoolean(row[cName].ToString()), null);
                        //}
                        SetValueHelper.SetValue<T>(ob, p, row[cName].ToString());
                    }
                });
                if (ob != null)
                {
                    oblist.Add(ob);
                }
            }
            return oblist;
        }


        /// <summary>  
        /// 将集合类转换成DataTable  
        /// </summary>  
        /// <param name="list">集合</param>  
        /// <returns></returns>  
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }


        /// <summary>  
        /// 将泛型集合类转换成DataTable  
        /// </summary>  
        /// <typeparam name="T">集合项类型</typeparam>  
        /// <param name="list">集合</param>  
        /// <param name="propertyName">需要返回的列的列名</param>  
        /// <returns>数据集(表)</returns>  
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}