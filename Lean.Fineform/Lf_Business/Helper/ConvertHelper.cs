using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.WebControls;

namespace LeanFine

{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>
    public sealed class ConvertHelper
    {
        #region 补足位数

        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            string temp = "";

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                temp += "0";
            }

            //连接text
            temp += text;

            //返回补足0的字符串
            return temp;
        }

        #endregion 补足位数

        #region 各进制数间转换

        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;

                        case 6:
                            result = "00" + result;
                            break;

                        case 5:
                            result = "000" + result;
                            break;

                        case 4:
                            result = "0000" + result;
                            break;

                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return "0";
            }
        }

        #endregion 各进制数间转换

        #region 使用指定字符集将string转换成byte[]

        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        #endregion 使用指定字符集将string转换成byte[]

        #region 使用指定字符集将byte[]转换成string

        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        #endregion 使用指定字符集将byte[]转换成string

        #region 将byte[]转换成int

        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }

        #endregion 将byte[]转换成int

        #region 返回数据表

        /// <summary>
        /// LINQ转换为DataTable类型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static DataTable LinqConvertToDataTable(IQueryable query)
        {
            DataTable dtList = new DataTable();
            bool isAdd = false;
            PropertyInfo[] objProterties = null;
            foreach (var item in query)
            {
                if (!isAdd)
                {
                    objProterties = item.GetType().GetProperties();
                    foreach (var itemProterty in objProterties)
                    {
                        Type type = null;
                        if (itemProterty.PropertyType != typeof(string) && itemProterty.PropertyType != typeof(int) && itemProterty.PropertyType != typeof(DateTime) && itemProterty.PropertyType != typeof(Decimal))
                        {
                            type = typeof(string);
                        }
                        else
                        {
                            type = itemProterty.PropertyType;
                        }
                        dtList.Columns.Add(itemProterty.Name, type);
                    }
                    isAdd = true;
                }
                var row = dtList.NewRow();
                foreach (var pi in objProterties)
                {
                    row[pi.Name] = pi.GetValue(item, null);
                }
                dtList.Rows.Add(row);
            }

            return dtList;
        }

        /// <summary>
        /// 将IEnumerable<T>类型的集合转换为DataTable类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="varlist"></param>
        /// <returns></returns>
        public static DataTable IEnumerableConvertToDataTable<T>(IEnumerable<T> varlist)
        {   //定义要返回的DataTable对象
            DataTable dtReturn = new DataTable();
            // 保存列集合的属性信息数组
            PropertyInfo[] oProps = null;
            if (varlist == null) return dtReturn;//安全性检查
                                                 //循环遍历集合，使用反射获取类型的属性信息
            foreach (T rec in varlist)
            {
                //使用反射获取T类型的属性信息，返回一个PropertyInfo类型的集合
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    //循环PropertyInfo数组
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;//得到属性的类型
                                                       //如果属性为泛型类型
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {   //获取泛型类型的参数
                            colType = colType.GetGenericArguments()[0];
                        }
                        //将类型的属性名称与属性类型作为DataTable的列数据
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                //新建一个用于添加到DataTable中的DataRow对象
                DataRow dr = dtReturn.NewRow();
                //循环遍历属性集合
                foreach (PropertyInfo pi in oProps)
                {   //为DataRow中的指定列赋值
                    dr[pi.Name] = pi.GetValue(rec, null) == null ?
                        DBNull.Value : pi.GetValue(rec, null);
                }
                //将具有结果值的DataRow添加到DataTable集合中
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;//返回DataTable对象
        }

        /// <summary>
        /// SQL转换为DataTable类型
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(String sql)//DataTable  为返回类型
        {
            //获取要执行的命令
            //string sql = "SELECT top 200 MB001,MB002,MB005,MB025,MB032,MB050,MB064,MB065,MB067 FROM DTA.dbo.INVMB";
            SqlDataReader mydr = SqlHelper.ExecuteReader(SqlHelper.GetConnSting(),
                                 CommandType.Text, sql, null);

            DataTable mydt = new DataTable();
            mydt.Load(mydr);

            return mydt;//返回dt
        }

        #endregion 返回数据表

        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ListConvertToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();

                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }
                foreach (object t in list)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(t, null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }

        /// <summary>
        /// List转DT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list)
        {
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            DataTable dt = new DataTable();
            foreach (var item in proInfos)
            {
                //解决DataSet不支持System.Nullable<>问题
                Type colType = item.PropertyType;
                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    colType = colType.GetGenericArguments()[0];
                }
                //添加列明及对应类型
                dt.Columns.Add(item.Name, colType);
            }
            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item);
                    if (obj == null)
                    {
                        continue;
                    }
                    if (proInfo.PropertyType == typeof(DateTime) && Convert.ToDateTime(obj) < Convert.ToDateTime("1753-01-01"))
                    {
                        continue;
                    }
                    dr[proInfo.Name] = obj;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 动态Linq方式实现行转列
        /// </summary>
        /// <param name="list">数据</param>
        /// <param name="DimensionList">维度列</param>
        /// <param name="DynamicColumn">动态列</param>
        /// <returns>行转列后数据</returns>
        public static DataTable DataTableRowToCol(DataTable dt, List<string> DimensionList, string DynamicColumn, out List<string> AllDynamicColumn)
        {
            //获取所有动态列
            AllDynamicColumn = new List<string>();
            foreach (DataRow dr in dt.DefaultView.ToTable(true, DynamicColumn).Rows)
            {
                if (dr[DynamicColumn] != null && !string.IsNullOrEmpty(dr[DynamicColumn].ToString()))
                {
                    AllDynamicColumn.Add(dr[DynamicColumn].ToString());
                }
            }

            //数值列
            Dictionary<string, Type> AllNumberColumn = new Dictionary<string, Type>();
            foreach (DataColumn item in dt.Columns)
            {
                if (item.DataType == typeof(int) || item.DataType == typeof(double) || item.DataType == typeof(float) || item.DataType == typeof(decimal))
                {
                    AllNumberColumn.Add(item.ColumnName, item.DataType);
                }
            }

            //结果DataTable创建
            DataTable dtResult = new DataTable();
            foreach (var item in DimensionList)
            {
                dtResult.Columns.Add(item, typeof(string));
            }
            //动态列
            foreach (var dynamicValue in AllDynamicColumn)
            {
                foreach (var item in AllNumberColumn.Keys)
                {
                    dtResult.Columns.Add(item + "'" + dynamicValue, AllNumberColumn[item]);
                }
            }

            //分组-优化性能
            Dictionary<string, List<DataRow>> dict = new Dictionary<string, List<DataRow>>();
            List<DataRow> drList = null;
            string groupKey = "";
            foreach (DataRow dr in dt.Rows)
            {
                groupKey = "";
                foreach (var item in DimensionList)
                {
                    groupKey += dr[item] + "#";
                }
                if (!dict.TryGetValue(groupKey, out drList))
                {
                    drList = new List<DataRow>();
                    dict[groupKey] = drList;
                }
                drList.Add(dr);
            }

            DataRow drReult = null;
            DataTable dtTemp = null;
            Dictionary<object, DataTable> dictTable = null;
            foreach (var kv in dict)
            {
                drReult = dtResult.NewRow();
                var arrKey = kv.Key.Split('#');
                int i = 0;
                foreach (var key in DimensionList)
                {
                    drReult[key] = arrKey[i];
                    i++;
                }
                dictTable = (from p in kv.Value.AsEnumerable()
                             group p by p.Field<object>(DynamicColumn) into g
                             select g).ToDictionary(e => e.Key, e => e.CopyToDataTable());
                foreach (var dynamicValue in AllDynamicColumn)
                {
                    if (dictTable.TryGetValue(dynamicValue, out dtTemp))
                    {
                        foreach (var numColumn in AllNumberColumn.Keys)
                        {
                            drReult[numColumn + "'" + dynamicValue] = dtTemp.Compute("sum(" + numColumn + ")", "");
                        }
                    }
                    else
                    {
                        foreach (var numColumn in AllNumberColumn.Keys)
                        {
                            drReult[numColumn + "'" + dynamicValue] = 0;
                        }
                    }
                }
                dtResult.Rows.Add(drReult);
            }
            return dtResult;
        }
    }
}