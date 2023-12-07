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
    /// pp_losstime 的摘要说明
    /// </summary>
    public class pp_losstime : IHttpHandler
    {
        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string transdate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime enddate = DateTime.ParseExact(transdate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string startdate = enddate.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);


            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all =
                from p in DBCharts.Pp_P1d_OutputSubs
                where p.Prodate.Substring(0, 6).CompareTo(startdate) >= 0
                where p.Prodate.Substring(0, 6).CompareTo(transdate) <= 0
                where p.isDelete == 0
                where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { Prodate = p.Prodate.Substring(0, 6) } into g
                select new
                {
                    g.Key.Prodate,
                    Proworktime = g.Sum(p => p.Prorealtime),
                    Prolosstime = g.Sum(p => p.Prolinestopmin),
                    Prospendtime = (Decimal)g.Sum(p => p.Prorealtime) + (Decimal)g.Sum(p => p.Prolinestopmin),
                    Lossrate = g.Sum(p => p.Prolinestopmin) / ((Decimal)g.Sum(p => p.Prorealtime) + (Decimal)g.Sum(p => p.Prolinestopmin)),

                };
            var q = (from a in q_all
                     select a).ToList()
       .Select(u => new
       {
           Lossrate = (u.Lossrate * 100).ToString("0.00"),
           u.Prodate,
           u.Proworktime,
           u.Prolosstime,
           u.Prospendtime,
       }).OrderByDescending(u => u.Prodate).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prodate"], value1 = dr["Proworktime"], value2 = dr["Prolosstime"], value3 = dr["Prospendtime"], value4 = "100", value5 = dr["Lossrate"] };  //key，value
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
