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
    /// sys_Tab_Fico_Expense_deptrequests 的摘要说明
    /// </summary>
    public class sys_Tab_Fico_Expense_deptrequests : IHttpHandler
    {

        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();


        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            var q_all = from p in DBCharts.Fico_Costing_DeptuseCosts
                            //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                        where p.Bc_YM.CompareTo(atedate) == 0
                        where p.Bc_Plnt.CompareTo("C100") == 0
                        // where p.Bc_MoveType.Contains("201")
                        where p.isDelete == 0

                        select new
                        {
                            Bc_YM = p.Bc_YM.Substring(0, 6),
                            p.BC_BudgetAmt,
                            p.Bc_ActualAmt,
                            p.Bc_CostName,

                        };

            var q =
                from p in q_all
                group p by new { p.Bc_YM, p.Bc_CostName } into g
                select new
                {
                    g.Key.Bc_YM,
                    g.Key.Bc_CostName,
                    BC_BudgetAmt = g.Sum(p => p.BC_BudgetAmt),
                    Bc_ActualAmt = g.Sum(p => p.Bc_ActualAmt) * -1.0m,
                    DiffAmt = g.Sum(p => p.Bc_ActualAmt) - g.Sum(p => p.BC_BudgetAmt),
                };
            q = q.OrderBy(u => u.Bc_YM);
            DataSet ds = new DataSet();
            DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
            ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in dt.Rows)
            {
                var obj = new { name = dr["Bc_CostName"], value1 = dr["BC_BudgetAmt"], value2 = dr["Bc_ActualAmt"], value3 = dr["DiffAmt"] };  //key，value
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
