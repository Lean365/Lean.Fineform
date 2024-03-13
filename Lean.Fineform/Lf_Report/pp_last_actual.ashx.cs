using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// pp_last_actual 的摘要说明
    /// </summary>
    public class pp_last_actual : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = "20200516"; DateTime.Now.AddDays(-1).ToString("yyyyMMdd");//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from p in DBCharts.Pp_P1d_OutputSubs
                        where p.Prodate.Contains(atedate)
                        where p.isDeleted == 0
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
                group p by new { p.Prodate, p.Prolinename } into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prolinename = g.Key.Prolinename,
                    Proplanqty = g.Sum(p => p.Prostdcapacity),
                    Proworkqty = g.Sum(p => p.Prorealqty),
                    Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? (g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity)) * 100 : 0),
                };
            var q = (from a in qs
                     select a).ToList().Select(u => new
                     {
                         u.Prolinename,
                         u.Proplanqty,
                         u.Proworkqty,
                         Proactivratio = u.Proactivratio.ToString("0")
                     });
            q = q.OrderBy(u => u.Prolinename);
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Prolinename"], value1 = dr["Proplanqty"], value2 = dr["Proworkqty"], value3 = dr["Proactivratio"] };  //key，value
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