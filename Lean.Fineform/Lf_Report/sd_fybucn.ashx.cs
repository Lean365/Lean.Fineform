﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// sd_fybucn 的摘要说明
    /// </summary>
    public class sd_fybucn : IHttpHandler
    {
        //映射
        private LeanFineContext DBCharts = new LeanFineContext();

        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            string sdate = atedate.Substring(0, 4);
            context.Response.ContentType = "text/plain";
            var q_salesData = (from a in DBCharts.Fico_Monthly_Sales
                                   //where a.Bc_YM.Contains(sdate)
                               orderby a.Bc_FY descending
                               select a).ToList(); //.Include(u => u.Dept);

            var q_Merge = from a in q_salesData
                          group a by new { a.Bc_FY, a.Bc_ProfitCenter } into g
                          select new
                          {
                              g.Key.Bc_FY,
                              Bu = g.Key.Bc_ProfitCenter,
                              //Qty = g.Sum(a => a.Bc_SalesQty),
                              Amout = g.Sum(a => a.Bc_BusinessAmount),
                          };

            var qss = (from a in q_Merge

                       select new
                       {
                           FY = a.Bc_FY,
                           Bu = (a.Bu.Contains("2U20") ? "PA" : (a.Bu.Contains("3U10") ? "PRO" : (a.Bu.Contains("3U20") ? "MI" : (a.Bu.Contains("4U30") ? "BS" : (a.Bu.Contains("ODBU") ? "OD" : (a.Bu.Contains("2U10") ? "ESO" : (a.Bu.Contains("4U10") ? "VS" : a.Bu))))))),
                           a.Amout,
                       }).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(qss.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(qss.AsQueryable()));
            //用来传回去的内容
            // List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["FY"], value1 = dr["Bu"], value2 = dr["Amout"] };  //key，value
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