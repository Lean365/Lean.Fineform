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
    /// sys_Tab_FICO_Expense_actual 的摘要说明
    /// </summary>
    public class sys_Tab_FICO_Expense_actual : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from p in DBCharts.Fico_Monthly_Actual_Costs
                            //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Bc_YM.Substring(0, 6).CompareTo(atedate) == 0
                        where p.IsDeleted == 0
                        where p.Bc_ExpCategory == "Exs."

                        select new
                        {
                            p.Bc_YM,
                            p.BC_BudgetAmt,
                            p.Bc_ActualAmt,
                            p.Bc_TitleNote,
                        };

            var qs =
                from p in q_all
                group p by new { p.Bc_YM, p.Bc_TitleNote } into g
                select new
                {
                    g.Key.Bc_YM,
                    g.Key.Bc_TitleNote,
                    Bc_ActualAmt = g.Sum(p => p.Bc_ActualAmt),
                };
            var q = (from a in qs
                     select a).ToList().Select(u => new
                     {
                         u.Bc_YM,
                         u.Bc_TitleNote,
                         u.Bc_ActualAmt,
                     });
            q = q.OrderByDescending(u => u.Bc_ActualAmt);
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Bc_TitleNote"], value = dr["Bc_ActualAmt"] };  //key，value
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