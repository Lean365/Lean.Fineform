using System;
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
    /// Fico_subjects 的摘要说明
    /// </summary>
    public class Fico_subjects : IHttpHandler
    {
        FineContext DBCharts = new FineContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();


        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from p in DBCharts.Fico_Expenses
                            //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Befm.Substring(0, 6).CompareTo(atedate) == 0
                        where p.isDelete == 0
                        where p.UDF01 == "" || p.UDF01 == null
                        where p.Bedept != "DTA"
                        select new
                        {
                            Befm = p.Befm.Substring(0, 6),
                            p.Beclasssub,
                            p.Bebtmoney,
                            p.Beatmoney,
                            //Bsdept = (p.Bsdept.Contains("SMT") ? "制二课" : (p.Bsdept == "自插" ? "制二课" : (p.Bsdept == "手插" ? "制二课" : (p.Bsdept == "物料" ? "制二课" : (p.Bsdept == "修正" ? "制二课" : (p.Bsdept == "制二课-间接" ? "制二课" : p.Bsdept)))))),

                        };

            var qs =
                from p in q_all
                group p by new { p.Befm, p.Beclasssub } into g
                select new
                {
                    g.Key.Befm,
                    g.Key.Beclasssub,
                    Budgetamount = g.Sum(p => p.Bebtmoney),
                    Actualamount = g.Sum(p => p.Beatmoney),
                    Differenceamount = g.Sum(p => p.Bebtmoney) - g.Sum(p => p.Beatmoney),
                    DifferenceRate = ((g.Sum(p => p.Bebtmoney) - g.Sum(p => p.Beatmoney)) != 0 ? ((g.Sum(p => p.Bebtmoney) - g.Sum(p => p.Beatmoney)) / g.Sum(p => p.Bebtmoney)) * 100 : 0),
                };
            var q = (from a in qs
                     select a).ToList().Select(u => new
                     {
                         u.Befm,
                         u.Beclasssub,
                         u.Budgetamount,
                         u.Actualamount,
                         u.Differenceamount,
                         DifferenceRate = u.DifferenceRate.ToString("0.00")
                     });
            q = q.OrderBy(u => u.Budgetamount);
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Beclasssub"], value1 = dr["Budgetamount"], value2 = dr["Actualamount"], value3 = dr["DifferenceRate"] };  //key，value
                lists.Add(obj);
            }
            //var jsS = new JavaScriptSerializer();                           //创建json对象
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