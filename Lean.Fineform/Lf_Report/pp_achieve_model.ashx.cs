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
    /// pp_achieve_model 的摘要说明
    /// </summary>
    public class pp_achieve_model : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

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
                            //where p.Prodate.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Prodate.Substring(0, 6).CompareTo(atedate) == 0
                        //join b in DB.PP_Outputs on p.Parent.ID equals b.ID
                        where p.IsDeleted == 0
                        where p.Prorealtime != 0 || p.Prolinestopmin != 0
                        select new
                        {
                            p.Prodate,
                            p.Promodel,
                            p.Prostdcapacity,
                            p.Prorealqty,
                        };

            var q_count =
                from p in q_all
                group p by new { Prodate = p.Prodate.Substring(0, 6), p.Promodel } into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Promodel,
                    Proplanqty = g.Sum(p => p.Prostdcapacity),
                    Proworkqty = g.Sum(p => p.Prorealqty),
                    Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                };
            var q_dist = q_count.Select(E => new
            {
                E.Prodate,
                E.Promodel,
                E.Proplanqty,
                E.Proworkqty,
                E.Proactivratio
            }).Where(u => u.Proplanqty != u.Proworkqty).ToList().Distinct();

            var q_exceed = from a in q_dist
                           where a.Proactivratio > 1
                           orderby a.Proactivratio descending
                           select a;

            var q_less = from a in q_dist
                         where a.Proactivratio < 1
                         orderby a.Proactivratio
                         select a;

            var q_Count = q_exceed.Take(10).Union(q_less.Take(10));

            var q = (from a in q_Count
                     select a).ToList().Select
       (u => new
       {
           u.Prodate,
           u.Promodel,
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
                var obj = new { name = dr["Promodel"], value1 = dr["Proplanqty"], value2 = dr["Proworkqty"], value3 = dr["Protargetratio"], value4 = dr["Proactivratio"] };  //key，value
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