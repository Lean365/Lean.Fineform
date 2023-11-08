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
    /// pp_reason_stopline 的摘要说明
    /// </summary>
    public class pp_reason_stopline : IHttpHandler
    {

        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";

            //查询在特定日期的全部工单
            var q_all =
                from p in DBCharts.Pp_OutputSubs
                    //join b in DB.proOutputs on p.OPHID equals b.OPHID
                where p.Prodate.Substring(0, 6).CompareTo(atedate) == 0
                where p.Prolinestopmin > 0
                //where p.Probadmemo != "NULL"
                //where !string.IsNullOrEmpty(p.Prostopcou)
                //where !string.IsNullOrEmpty(p.Probadmemo)
                where p.isDelete == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0

                //group p by new { Prodate = p.Prodate.Substring(0, 6), p.Probadcou }
                //into g
                select new
                {
                    Prodate = p.Prodate.Substring(0, 6),
                    Prostopcou = (p.Prostopcou == null ? "其他" : (p.Prostopcou == "其它" ? "其他" : p.Prostopcou)),
                    //numProbadcou = (from a in DBCharts.Pp_OutputSubs select a.Probadcou).Distinct().Count(),

                    p.Prolinestopmin,
                };

            var q = from p in q_all
                    group p by new { p.Prodate, p.Prostopcou }
                      into g
                    select new
                    {
                        g.Key.Prodate,
                        g.Key.Prostopcou,
                        Prolinestopmin = g.Sum(p => p.Prolinestopmin),
                    };


            q = q.OrderByDescending(u => u.Prolinestopmin).Take(5);
            //q.Take(5);

            if (q.Any())
            {
                DataSet ds = new DataSet();
                DataTable dt = ConvertHelper.LinqConvertToDataTable(q);
                ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q));
                //用来传回去的内容
                //List<object> lists = new List<object>();                       //创建object类型的泛型
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new { name = dr["Prostopcou"], value = dr["Prolinestopmin"] };  //key，value
                    lists.Add(obj);
                }
                //var jsS = new JavaScriptSerializer();                           //创建json对象
                context.Response.Write(jsS.Serialize(lists));                   //返回数据
            }
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