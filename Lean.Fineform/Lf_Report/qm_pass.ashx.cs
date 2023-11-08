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

namespace Lean.Fineform.Lf_Report
{
    /// <summary>
    /// qm_pass 的摘要说明
    /// </summary>
    public class qm_pass : IHttpHandler
    {

        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime endts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string enddate = endts.ToString();
            string sdate = endts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from a in DBCharts.Qm_Outgoings
                        where a.isDelete == 0
                        where a.qmCheckdate.Substring(0, 6).CompareTo(sdate) >= 0
                        where a.qmCheckdate.Substring(0, 6).CompareTo(atedate) <= 0
                        select a;

            var q_counts = from p in q_all
                           group p by new
                           {
                               // p.qmProlot,
                               qmCheckdate = p.qmCheckdate.Substring(0, 6),

                           }

                                into g
                           select new
                           {
                               Cdate = g.Key.qmCheckdate,
                               //Clot = g.Key.qmProlot,
                               Pqty = g.Sum(p => p.qmProqty),
                               Aqty = g.Sum(p => p.qmAcceptqty),
                               Rqty = g.Sum(p => p.qmRejectqty),
                               Passrate = g.Sum(p => p.qmAcceptqty) / g.Sum(p => p.qmProqty),
                               Failurerate = g.Sum(p => p.qmRejectqty) / g.Sum(p => p.qmProqty),


                           };
            var q = (from a in q_counts
                     select a).ToList()
                   .Select(u => new
                   {
                       Passrate = (u.Passrate * 100).ToString("0"),
                       u.Pqty,
                       u.Rqty,
                       u.Cdate
                   }).OrderBy(u => u.Cdate).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Cdate"], value1 = dr["Rqty"], value2 = dr["Pqty"], value3 = dr["Passrate"] };  //key，value
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