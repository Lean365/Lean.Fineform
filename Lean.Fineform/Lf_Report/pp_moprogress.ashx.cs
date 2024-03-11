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
    /// Pp_moprogress 的摘要说明
    /// </summary>
    public class Pp_moprogress : IHttpHandler
    {

        FineContext DBCharts = new FineContext();
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
                            // where p.Prodate.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Prodate.Substring(0, 6).CompareTo(atedate) == 0
                        where p.isDelete == 0
                        //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                        //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                        select new
                        {
                            // Prodate = p.Prodate.Substring(0, 6),
                            p.Prolot,
                            p.Proorder,
                            p.Proorderqty,

                            p.Prorealqty,


                        };

            var q_realQty =
                from p in q_all
                group p by new { p.Prolot, p.Proorder, p.Proorderqty } into g
                select new
                {
                    g.Key.Prolot,
                    g.Key.Proorderqty,
                    //EntiretyRate = (g.Sum(p => p.Proorderqty) != 0 ? g.Sum(p => p.Proorderqty) / g.Sum(p => p.Proorderqty) : 0) * 100,
                    Prorealqty = g.Sum(p => p.Prorealqty) //(g.Sum(p => p.Proorderqty) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Proorderqty) : 0)*100,

                };
            var q_allQty = from p in q_realQty
                           group p by new { p.Prolot } into g
                           select new
                           {
                               g.Key.Prolot,
                               //g.Key.Proorderqty,
                               Proorderqty = g.Sum(p => p.Proorderqty), //(g.Sum(p => p.Proorderqty) != 0 ? g.Sum(p => p.Proorderqty) / g.Sum(p => p.Proorderqty) : 0) * 100,
                               Prorealqty = g.Sum(p => p.Prorealqty)    //(g.Sum(p => p.Proorderqty) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Proorderqty) : 0)*100,

                           };

            var q = from p in q_allQty
                    select new
                    {
                        p.Prolot,
                        //g.Key.Proorderqty,
                        p.Proorderqty,
                        p.Prorealqty,
                        ProgressRate = ((p.Proorderqty) != 0 ? (p.Prorealqty) / (p.Proorderqty) : 0) * 100,

                    };

            q = q.OrderBy(u => u.Proorderqty);

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prolot"], value1 = dr["ProgressRate"], value2 = dr["Prorealqty"], value3 = dr["Proorderqty"], value4 = "100" };  //key，value
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