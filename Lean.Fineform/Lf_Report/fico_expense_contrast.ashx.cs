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
    /// sys_Tab_FICO_Expense_contrast 的摘要说明
    /// </summary>
    public class sys_Tab_FICO_Expense_contrast : IHttpHandler
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
                        where p.isDeleted == 0
                        where p.Bc_ExpCategory == "Exs."

                        select new
                        {
                            Bc_YM = p.Bc_YM.Substring(0, 6),
                            p.BC_BudgetAmt,
                            p.Bc_ActualAmt,
                            Bc_CostName = (p.Bc_CostName.Contains("SMT") ? "制二课" : (p.Bc_CostName.Contains("自插") ? "制二课" : (p.Bc_CostName.Contains("手插") ? "制二课" : (p.Bc_CostName.Contains("物料") ? "制二课" : (p.Bc_CostName.Contains("修正") ? "制二课" : (p.Bc_CostName.Contains("制二课-间接") ? "制二课" : (p.Bc_CostName.Contains("总经") ? "总经室" : (p.Bc_CostName.Contains("制造技术") ? "制技课" : (p.Bc_CostName.Contains("OEM") ? "OEM" : p.Bc_CostName))))))))),
                        };

            var qs =
                from p in q_all
                group p by new { p.Bc_YM, p.Bc_CostName } into g
                select new
                {
                    g.Key.Bc_YM,
                    g.Key.Bc_CostName,
                    BC_BudgetAmt = g.Sum(p => p.BC_BudgetAmt),
                    Bc_ActualAmt = g.Sum(p => p.Bc_ActualAmt),
                    DiffAmt = g.Sum(p => p.BC_BudgetAmt) - g.Sum(p => p.Bc_ActualAmt),
                    DiffRate = ((g.Sum(p => p.BC_BudgetAmt) - g.Sum(p => p.Bc_ActualAmt)) != 0 ? ((g.Sum(p => p.BC_BudgetAmt) - g.Sum(p => p.Bc_ActualAmt)) / g.Sum(p => p.BC_BudgetAmt)) * 100 : 0),
                };
            var q = (from a in qs
                     select a).ToList().Select(u => new
                     {
                         u.Bc_YM,
                         u.Bc_CostName,
                         u.BC_BudgetAmt,
                         Bc_ActualAmt = u.Bc_ActualAmt * -1.0m,
                         u.DiffAmt,
                         DiffRate = u.DiffRate.ToString("0.00")
                     });
            q = q.OrderByDescending(u => u.Bc_ActualAmt);
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Bc_CostName"], value1 = dr["BC_BudgetAmt"], value2 = dr["Bc_ActualAmt"], value3 = dr["DiffRate"] };  //key，value
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