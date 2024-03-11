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
    /// Pp_achieve_model 的摘要说明
    /// </summary>
    public class Pp_achieve_year : IHttpHandler
    {
        FineContext DBCharts = new FineContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);


            //string edate = DateTime.Now.ToString("yyyyMMdd").Substring(0, 6);

            //string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);


            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";

            var qs =
                from p in DBCharts.Pp_P1d_OutputSubs
                where p.isDelete == 0
                where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { Prodate = p.Prodate.Substring(0, 4) } into g

                select new
                {
                    g.Key.Prodate,                    
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
           //Protargetratio = (u.Prodate.CompareTo("202003") > 0 ? "103" : "98"),
           Proactivratio = (u.Proactivratio * 100).ToString("0.00")
       }).OrderByDescending(u => u.Prodate).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prodate"], value1 = dr["Proplanqty"], value2 = dr["Proworkqty"], value3 = dr["Proactivratio"],};  //key，value
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