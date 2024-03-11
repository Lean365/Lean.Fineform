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
    /// shipment_bucode 的摘要说明
    /// </summary>
    public class shipment_bucode : IHttpHandler
    {
        FineContext DBCharts = new FineContext();


        Fine.Lf_Business.Models.YF.LeanSerialEntities DB_Serial = new Fine.Lf_Business.Models.YF.LeanSerialEntities();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();
        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_salesData = (from a in DB_Serial.DTASSET_SCANNER_UP
                               where a.OUTS001.Substring(0, 6).CompareTo(atedate) == 0
                               select a).ToList(); //.Include(u => u.Dept);

            var q_buCode = (from b in DBCharts.Mm_Materials
                            where b.MatType.Contains("FERT")
                            select b).ToList();

            var q_Merge = from a in q_salesData
                          join b in q_buCode on a.OUTS005 equals b.MatItem

                          group a by new { Date = a.OUTS001.Substring(0, 6), b.ProfitCenter, } into g
                          select new
                          {
                              Date = g.Key.Date.Substring(0, 6),
                              g.Key.ProfitCenter,
                              Qty = g.Sum(a => a.OUTS008),

                          };

            var qss = from a in q_Merge
                      select new
                      {
                          Date = a.Date,
                          ProfitCenter = (a.ProfitCenter.Contains("2U20") ? "PA" : (a.ProfitCenter.Contains("3U10") ? "PRO" : (a.ProfitCenter.Contains("3U20") ? "MI" : (a.ProfitCenter.Contains("4U30") ? "BS" : (a.ProfitCenter.Contains("ODBU") ? "OD" : (a.ProfitCenter.Contains("2U10") ? "ESO" : (a.ProfitCenter.Contains("4U10") ? "VS" : a.ProfitCenter))))))),
                          Qty = a.Qty,
                      };

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(qss.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(qss.AsQueryable()));
            //用来传回去的内容
            // List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["ProfitCenter"], value = dr["Qty"] };  //key，value
                lists.Add(obj);
            }
            // var jsS = new JavaScriptSerializer();                           //创建json对象
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