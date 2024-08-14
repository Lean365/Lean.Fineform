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
    /// fico_costing_invamt 的摘要说明
    /// </summary>
    public class fico_costing_invamt : IHttpHandler
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
            var q = from a in DBCharts.Fico_Monthly_Inventorys
                    where a.Bc_YM.CompareTo(atedate) == 0
                    where a.IsDeleted == 0
                    select a;

            var q_count = from a in q
                          group a by new { a.Bc_Assessment, a.Bc_YM }
                        into g
                          select new
                          {
                              g.Key.Bc_YM,
                              Bc_Assessment = (g.Key.Bc_Assessment.CompareTo("Z792") == 0 ? "成品" : (g.Key.Bc_Assessment.CompareTo("Z300") == 0 ? "原材料" : (g.Key.Bc_Assessment.CompareTo("Z790") == 0 ? "半成品" : g.Key.Bc_Assessment))),
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
                var obj = new { name = dr["Bc_Assessment"], value1 = dr["Bc_Totalinventory"], value2 = dr["Bc_Totalamount"] };  //key，value
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