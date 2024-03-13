using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// fico_costing_invamt_bu 的摘要说明
    /// </summary>
    public class fico_costing_invamt_bu : IHttpHandler
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
            var q = from a in DBCharts.Fico_Costing_HistInventorys
                    join b in DBCharts.Mm_Materials on a.Bc_Item equals b.MatItem
                    where a.Bc_YM.CompareTo(atedate) == 0
                    where a.isDeleted == 0
                    select new
                    {
                        b.ProfitCenter,
                        a.Bc_YM,
                        a.Bc_Totalinventory,
                        a.Bc_Totalamount
                    };

            var q_count = from a in q
                          group a by new { a.ProfitCenter, a.Bc_YM }
                        into g
                          select new
                          {
                              g.Key.Bc_YM,
                              ProfitCenter = (g.Key.ProfitCenter.Contains("2U20") ? "PA" : (g.Key.ProfitCenter.Contains("3U10") ? "PRO" : (g.Key.ProfitCenter.Contains("3U20") ? "MI" : (g.Key.ProfitCenter.Contains("4U30") ? "BS" : (g.Key.ProfitCenter.Contains("ODBU") ? "OD" : (g.Key.ProfitCenter.Contains("2U10") ? "ESO" : (g.Key.ProfitCenter.Contains("4U10") ? "VS" : g.Key.ProfitCenter))))))),
                              Bc_Totalinventory = g.Sum(a => a.Bc_Totalinventory),
                              Bc_Totalamount = g.Sum(a => a.Bc_Totalamount),
                          };

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q_count.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_count.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["ProfitCenter"], value1 = dr["Bc_Totalinventory"], value2 = dr["Bc_Totalamount"] };  //key，value
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