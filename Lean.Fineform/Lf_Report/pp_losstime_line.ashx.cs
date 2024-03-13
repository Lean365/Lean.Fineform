using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// pp_losstime_line 的摘要说明
    /// </summary>
    public class pp_losstime_line : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

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
                where p.Prodate.Substring(0, 6).CompareTo(transdate) == 0
                //where p.Prodate.CompareTo(enddate.ToString()) <= 0
                where p.isDeleted == 0
                where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { Prodate = p.Prodate.Substring(0, 6), p.Prolinename } into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Prolinename,
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
           u.Prolinename,
           u.Proworktime,
           u.Prolosstime,
           u.Prospendtime,
       }).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prolinename"], value1 = dr["Proworktime"], value2 = dr["Prolosstime"], value3 = dr["Prospendtime"], value4 = "100", value5 = dr["Lossrate"] };  //key，value
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