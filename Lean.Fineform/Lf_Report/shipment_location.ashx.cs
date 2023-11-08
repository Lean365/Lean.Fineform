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
    /// shipment_location 的摘要说明
    /// </summary>
    public class shipment_location : IHttpHandler
    {

        Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities DB_Serial = new Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";

            var qs = from a in DB_Serial.DTASSET_SCANNER_UP
                     where a.OUTS001.Substring(0, 6).CompareTo(atedate) == 0
                     group a by new { OUTS001 = a.OUTS001.Substring(0, 6), OUTS008 = (a.OUTS007.IndexOf("_") + 1 == 0 ? a.OUTS007 : (a.OUTS007.Substring(0, a.OUTS007.IndexOf("_")))), } into g
                     select new
                     {
                         Date = g.Key.OUTS001.Substring(0, 6),
                         Destination = (g.Key.OUTS008.IndexOf("_") + 1 == 0 ? g.Key.OUTS008 : (g.Key.OUTS008.Substring(0, g.Key.OUTS008.IndexOf("_")))),
                         Qty = g.Sum(a => a.OUTS008),

                     };

            var qss = from a in qs
                      select new
                      {
                          a.Date,
                          Destination = (a.Destination.Contains("ACE") ? "CHINA" : (a.Destination.Contains("BEIJING") ? "CHINA" : (a.Destination.Contains("DCHAV") ? "CHINA" : (a.Destination.Contains("DTA") ? "CHINA" : (a.Destination.Contains("GUANGZHOU") ? "CHINA" : (a.Destination.Contains("GW") ? "CHINA" : (a.Destination.Contains("MUJI") ? "CHINA" : (a.Destination.Contains("MUSICGW") ? "CHINA" : (a.Destination.Contains("NAISHA") ? "CHINA" : (a.Destination.Contains("SHANGHAI") ? "CHINA" : (a.Destination.Contains("SHENGZHEN") ? "CHINA" : (a.Destination.Contains("WUXI") ? "CHINA" : (a.Destination.Contains("TOA") ? "VIETNAM" : (a.Destination.Contains("INCHEON") ? "KOREA" : (a.Destination.Contains("TCA") ? "USA" : (a.Destination.Contains("HOLLAND") ? "NETHERLANDS" : (a.Destination.Contains("ROTTERDAM") ? "NETHERLANDS" : a.Destination))))))))))))))))),
                          a.Qty,

                      };

            var q   =from a in qss
                     group a by new { a.Date,a.Destination} into g
                     select new

                     {
                         g.Key.Date,
                         g.Key.Destination,
                         Qty=g.Sum(a=>a.Qty)
                     };


            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q);
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q));
            //用来传回去的内容
            //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Destination"], value = dr["Qty"] };  //key，value
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