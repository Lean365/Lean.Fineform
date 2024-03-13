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
    /// sd_fycn 的摘要说明
    /// </summary>
    public class sd_fycn : IHttpHandler
    {
        //映射
        private LeanFineContext DBCharts = new LeanFineContext();

        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_salesData = (from a in DBCharts.Fico_Costing_Sales_Billings
                               orderby a.Bc_YM descending
                               select a).ToList(); //.Include(u => u.Dept);

            var q_Merge = from a in q_salesData
                          group a by new { Bc_YM = a.Bc_YM.Substring(0, 4) } into g
                          select new
                          {
                              YM = g.Key.Bc_YM,
                              Qty = g.Sum(a => a.Bc_SalesQty),
                              Amout = g.Sum(a => a.Bc_BusinessAmount),
                          };

            var qss = (from a in q_Merge
                       orderby a.YM descending
                       select a).Take(10).ToList();

            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(qss.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(qss.AsQueryable()));
            //用来传回去的内容
            // List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["YM"], value1 = dr["Qty"], value2 = dr["Amout"] };  //key，value
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