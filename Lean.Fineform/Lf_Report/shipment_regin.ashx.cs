﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using FineUIPro;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Fine.Lf_Report
{
    /// <summary>
    /// shipment_regin 的摘要说明
    /// </summary>
    public class shipment_regin : IHttpHandler
    {

        Fine.Lf_Business.Models.YF.LeanSerialEntities DB_Serial = new Fine.Lf_Business.Models.YF.LeanSerialEntities();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();
        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";


            var q = from a in DB_Serial.DTASSET_SCANNER_UP
                    select a; //.Include(u => u.Dept);

            var qs = from a in q
                     where a.OUTS001.Substring(0, 6).CompareTo(atedate) == 0
                     group a by new { OUTS001 = a.OUTS001.Substring(0, 6), OUTS003 = a.OUTS003, } into g
                     select new
                     {
                         Date = g.Key.OUTS001.Substring(0, 6),
                         Regin = g.Key.OUTS003,
                         Qty = g.Sum(a => a.OUTS008),

                     };
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(qs);
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(qs));
            //用来传回去的内容
            // List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Regin"], value = dr["Qty"] };  //key，value
                lists.Add(obj);
            }
            // var jsS = new JavaScriptSerializer();                           //创建json对象
            context.Response.Write(jsS.Serialize(lists));                   //返回数据
        }





        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}