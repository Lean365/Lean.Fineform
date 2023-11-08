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
    /// Fico_subjects_tree 的摘要说明
    /// </summary>
    public class Fico_subjects_tree : IHttpHandler
    {
        LeanContext DBCharts = new LeanContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();


        public void ProcessRequest(HttpContext context)
        {
            //string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //DateTime dts = DateTime.ParseExact(atedate + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            //string sdate = dts.AddYears(-1).AddMonths(+1).ToString("yyyyMMdd").Substring(0, 6);
            //获取一同发送过来的参数
            //string command = context.Request["cmd",;
            context.Response.ContentType = "text/plain";
            var q_pid = from p in DBCharts.Fico_Costing_ActualCosts
                            //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                        //where p.Bc_YM.Substring(0, 6).CompareTo(atedate) == 0
                        where p.isDelete == 0
                        where p.Bc_ExpCategory.Contains("DTA")                        
                        select new
                        {
                            p.Bc_CorpCode,
                            p.Bc_CorpName
                            //Befm = p.Befm.Substring(0, 6),
                            //Budgetamount = p.Bebtmoney,
                            //Actualamount = p.Beatmoney * -1,
                            //Bsdept = (p.Bsdept.Contains("SMT") ? "制二课" : (p.Bsdept.Contains("自插") ? "制二课" : (p.Bsdept.Contains("手插") ? "制二课" : (p.Bsdept.Contains("物料") ? "制二课" : (p.Bsdept.Contains("修正") ? "制二课" : (p.Bsdept.Contains("制二课-间接") ? "制二课" : (p.Bsdept.Contains("总经") ? "总经室" : (p.Bsdept.Contains("制造技术") ? "制技课" : (p.Bsdept.Contains("OEM") ? "OEM" : p.Bsdept))))))))),

                        };


            q_pid = q_pid.Distinct();
            DataSet q_pidds = new DataSet();
            DataTable q_piddt = ConvertHelper.LinqConvertToDataTable(q_pid.AsQueryable());
            q_pidds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_pid.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in q_piddt.Rows)
            {
                var obj = new { id = dr["Bc_CorpCode"], name = dr["Bc_CorpName"] };  //key，value
                lists.Add(obj);
            }

            var q_id = from p in DBCharts.Fico_Costing_ActualCosts
                           //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                       //where p.Bc_YM.Substring(0, 6).CompareTo(atedate) == 0
                       where p.isDelete == 0
                       where p.Bc_ExpCategory.Contains("DTA")
                       select new
                       {
                           p.Bc_CorpCode,
                           p.Bc_CorpName,
                           p.Bc_CostCode,
                           p.Bc_CostName,
                           //Befm = p.Befm.Substring(0, 6),
                           //Budgetamount = p.Bebtmoney,
                           //Actualamount = p.Beatmoney * -1,
                           //Bsdept = (p.Bsdept.Contains("SMT") ? "制二课" : (p.Bsdept.Contains("自插") ? "制二课" : (p.Bsdept.Contains("手插") ? "制二课" : (p.Bsdept.Contains("物料") ? "制二课" : (p.Bsdept.Contains("修正") ? "制二课" : (p.Bsdept.Contains("制二课-间接") ? "制二课" : (p.Bsdept.Contains("总经") ? "总经室" : (p.Bsdept.Contains("制造技术") ? "制技课" : (p.Bsdept.Contains("OEM") ? "OEM" : p.Bsdept))))))))),

                       };


            q_id = q_id.Distinct();
            DataSet q_idds = new DataSet();
            DataTable q_iddt = ConvertHelper.LinqConvertToDataTable(q_id.AsQueryable());
            q_idds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_id.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in q_iddt.Rows)
            {
                var obj = new { id = dr["Bc_CostCode"], name = dr["Bc_CostName"], pid = dr["Bc_CorpCode"], pname = dr["Bc_CorpName"], };  //key，value
                lists.Add(obj);
            }
            var q_all = from p in DBCharts.Fico_Costing_ActualCosts
                            //where p.Befm.Substring(0, 6).CompareTo(sdate) >= 0
                        //where p.Bc_YM.Substring(0, 6).CompareTo(atedate) == 0
                        where p.isDelete == 0
                        where p.Bc_ExpCategory.Contains("DTA")
                        select new
                        {
                            p.Bc_CostCode,
                            p.Bc_CostName,
                            p.Bc_TitleCode,
                            
                            Bc_TitleName = p.Bc_TitleName.Substring(p.Bc_TitleName.IndexOf("-")+1, p.Bc_TitleName.Length - p.Bc_TitleName.IndexOf("-"))
                            //p.BC_BudgetAmt,
                            //Befm = p.Befm.Substring(0, 6),
                            //Budgetamount = p.Bebtmoney,
                            //Actualamount = p.Beatmoney * -1,
                            //Bsdept = (p.Bsdept.Contains("SMT") ? "制二课" : (p.Bsdept.Contains("自插") ? "制二课" : (p.Bsdept.Contains("手插") ? "制二课" : (p.Bsdept.Contains("物料") ? "制二课" : (p.Bsdept.Contains("修正") ? "制二课" : (p.Bsdept.Contains("制二课-间接") ? "制二课" : (p.Bsdept.Contains("总经") ? "总经室" : (p.Bsdept.Contains("制造技术") ? "制技课" : (p.Bsdept.Contains("OEM") ? "OEM" : p.Bsdept))))))))),

                        };

            q_all = q_all.Distinct();
            DataSet q_allds = new DataSet();
            DataTable q_alldt = ConvertHelper.LinqConvertToDataTable(q_all.AsQueryable());
            q_allds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_all.AsQueryable()));
            //用来传回去的内容
            //List<object> lists = new List<object>();                       //创建object类型的泛型
            foreach (DataRow dr in q_alldt.Rows)
            {
                var obj = new { id = dr["Bc_TitleCode"], name = dr["Bc_TitleName"], pid = dr["Bc_CostCode"], pname = dr["Bc_CostName"] };  //key，value
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