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
    /// pp_achieve 的摘要说明
    /// </summary>
    public class pp_achieve : IHttpHandler
    {

        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);


            //string edate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);

            string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);


            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from p in DBCharts.Pp_P1d_OutputSubs
                        where p.Prodate.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Prodate.Substring(0, 6).CompareTo(atedate) <= 0
                        where p.isDelete == 0
                        where p.Prorealtime != 0 || p.Prolinestopmin != 0
                        select new
                        {
                            Prodate = p.Prodate.Substring(0, 6),
                            p.Prolinename,
                            p.Prodirect,
                            p.Proindirect,
                            p.Prolot,
                            p.Prohbn,
                            p.Prost,
                            p.Promodel,
                            p.Prorealtime,
                            p.Prostdcapacity,
                            p.Prorealqty,
                        };

            var qs =
                from p in q_all
                group p by new { p.Prodate } into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Proplanqty = g.Sum(p => p.Prostdcapacity),
                    Proworkqty = g.Sum(p => p.Prorealqty),
                    Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                };
            var q = (from a in qs
                     select a).ToList().Select
                   (u => new
                   {
                       u.Prodate,
                       u.Proplanqty,
                       u.Proworkqty,
                       Protargetratio = (u.Prodate.CompareTo("202003") > 0 ? "103" : "98"),
                       Proactivratio = (u.Proactivratio * 100).ToString("0.00")
                   }).OrderBy(u => u.Prodate).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prodate"], value1 = dr["Proplanqty"], value2 = dr["Proworkqty"], value3 = dr["Protargetratio"], value4 = dr["Proactivratio"] };  //key，value
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