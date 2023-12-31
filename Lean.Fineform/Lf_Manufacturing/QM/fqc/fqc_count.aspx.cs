﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Lean.Fineform.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_count : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcView";
            }
        }

        #endregion

        #region Page_Load
        public static string tracestr;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //BindDDLData();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");


            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);


            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDDLLline();
            BindGrid();

        }



        private void BindGrid()
        {
            //第三个为最终结果,将不为空的数据合并到所有集合中

            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");




            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            // 在用户名称中搜索




            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (DDLline.SelectedIndex != 0 && DDLline.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmLine.ToString().Contains(DDLline.SelectedItem.Text));
            }
            if (DDLlot.SelectedIndex != 0 && DDLlot.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmProlot.ToString().Contains(DDLlot.SelectedItem.Text));
            }
            if (DDLhbn.SelectedIndex != 0 && DDLhbn.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmMaterial.ToString().Contains(DDLhbn.SelectedItem.Text));
            }

            q = q.Where(u => u.isDelete == 0);
            var qs = from p in q
                     group p by new
                     {
                         p.qmProlot,

                         qmCheckdate = p.qmCheckdate.Substring(0, 6),

                     }

                    into g
                     select new
                     {
                         Cdate = g.Key.qmCheckdate,
                         Clot = g.Key.qmProlot,
                         Pqty = g.Sum(p => p.qmProqty),
                         Aqty = g.Sum(p => p.qmAcceptqty),
                         Rqty = g.Sum(p => p.qmRejectqty),
                         Passrate = g.Sum(p => p.qmAcceptqty) / g.Sum(p => p.qmProqty),
                         Failurerate = g.Sum(p => p.qmRejectqty) / g.Sum(p => p.qmProqty),


                     };

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(qs);

            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                GridHelper.GetPagedDataTable(Grid1, qs);
            }
            Grid1.DataSource = qs;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(qs);
            // 当前页的合计
            OutputSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
        }

        #endregion

        #region Events



        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreOphDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {


        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{

        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreOphDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);
        //    NetLogRecord();
        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proOutputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.proOutputs.Where(u => ids.Contains(u.ID)).Delete();





        //    // 重新绑定表格
        //    BindGrid();

        //}


        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {


            if (e.CommandName == "PrintCheck")
            {
                //ID,Prodate,Prolot
                object[] keys = Grid1.DataKeys[e.RowIndex];

                if (DDLlot.SelectedIndex != -1 && DDLlot.SelectedIndex != 0)
                {

                    tracestr = DDLlot.SelectedItem.Text;

                    //labResult.Text = keys[0].ToString();
                    PageContext.RegisterStartupScript(Window1.GetShowReference("~/onePrint/defect_report.aspx?Prolot=" + tracestr + "&type=1") + Window1.GetMaximizeReference());

                }
                else
                {
                    Alert.ShowInTop("请选择lot！");
                    return;
                }
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }


        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }




        #endregion

        public void BindDDLLline()
        {


                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
                //string slot = DDLlot.SelectedItem.Text;
                //查询LINQ去重复
                var q = from a in DB.Qm_Outgoings
                        where a.qmCheckdate.CompareTo(sdate) >= 0
                        where a.qmCheckdate.CompareTo(edate) <= 0
                        //where a.qmProlot.Contains(slot)


                        select new
                        {

                            a.qmLine

                        };

                var qs = q.Select(E => new { E.qmLine }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                DDLline.DataSource = qs;
                DDLline.DataTextField = "qmLine";
                DDLline.DataValueField = "qmLine";
                DDLline.DataBind();

            this.DDLline.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }
  

        public void BindDDLLot()
        {

            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            string sline = DDLline.SelectedItem.Text;
            //查询LINQ去重复
            var q = from a in DB.Qm_Outgoings
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    where a.qmLine.Contains(sline)

                    select new
                    {

                        a.qmProlot

                    };

            var qs = q.Select(E => new { E.qmProlot }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLlot.DataSource = qs;
            DDLlot.DataTextField = "qmProlot";
            DDLlot.DataValueField = "qmProlot";
            DDLlot.DataBind();

            this.DDLlot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }
        public void BindDDLhbn()
        {
            if (DDLline.SelectedIndex != -1 && DDLline.SelectedIndex != 0)
            {
                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
                string sline = DDLline.SelectedItem.Text;
                string slot = DDLlot.SelectedItem.Text;
                //查询LINQ去重复
                var q = from a in DB.Qm_Outgoings
                        where a.qmCheckdate.CompareTo(sdate) >= 0
                        where a.qmCheckdate.CompareTo(edate) <= 0
                        where a.qmLine.Contains(sline)
                        where a.qmProlot.Contains(slot)

                        select new
                        {

                            a.qmMaterial

                        };

                var qs = q.Select(E => new { E.qmMaterial }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                DDLhbn.DataSource = qs;
                DDLhbn.DataTextField = "qmMaterial";
                DDLhbn.DataValueField = "qmMaterial";
                DDLhbn.DataBind();

                this.DDLhbn.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }
        protected void DDLline_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DDLline.SelectedIndex != -1 && DDLline.SelectedIndex != 0)
            {
                BindDDLLot();
                BindGrid();

            }
        }
        protected void DDLlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLlot.SelectedIndex != -1 && DDLlot.SelectedIndex != 0)
            {

                BindDDLhbn();
                BindGrid();

            }
        }
        protected void DDLhbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();

        }
        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindDDLLline();
                DDLlot.Items.Clear();
                DDLhbn.Items.Clear();
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindDDLLline();
                DDLlot.Items.Clear();
                DDLhbn.Items.Clear();

                BindGrid();
            }
        }

        //合计表格
        private void OutputSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal aTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal pratio = 0.0m;
            Decimal fratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Pqty"]);
                aTotal += Convert.ToDecimal(row["Aqty"]);
                rTotal += Convert.ToDecimal(row["Rqty"]);
                pratio = aTotal / pTotal;
                fratio = rTotal / pTotal;
            }


            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Pqty", pTotal.ToString("F2"));
            summary.Add("Aqty", aTotal.ToString("F2"));
            summary.Add("Rqty", rTotal.ToString("F2"));
            summary.Add("Passrate", pratio.ToString("p2"));
            summary.Add("Failurerate", fratio.ToString("p2"));

            Grid1.SummaryData = summary;

        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = "DTA_sdate_Qualified rate";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";











            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            // 在用户名称中搜索




            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (DDLline.SelectedIndex != 0 && DDLline.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmLine.ToString().Contains(DDLline.SelectedItem.Text));
            }
            if (DDLlot.SelectedIndex != 0 && DDLlot.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmProlot.ToString().Contains(DDLlot.SelectedItem.Text));
            }
            if (DDLhbn.SelectedIndex != 0 && DDLhbn.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmMaterial.ToString().Contains(DDLhbn.SelectedItem.Text));
            }

            q = q.Where(u => u.isDelete == 0);
            var qs = from p in q
                     group p by new
                     {
                         p.qmProlot,

                         qmCheckdate = p.qmCheckdate.Substring(0, 6),

                     }

                    into g
                     select new
                     {
                         日期 = g.Key.qmCheckdate,
                         批次 = g.Key.qmProlot,
                         生产数 = g.Sum(p => p.qmProqty),
                         验收数 = g.Sum(p => p.qmAcceptqty),
                         验退数 = g.Sum(p => p.qmRejectqty),
                         合格率 = g.Sum(p => p.qmAcceptqty) / g.Sum(p => p.qmProqty),
                         不合格率 = g.Sum(p => p.qmRejectqty) / g.Sum(p => p.qmProqty),


                     };



            ConvertHelper.LinqConvertToDataTable(qs);
            Grid1.AllowPaging = false;
            ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
            Grid1.AllowPaging = true;
        }


    }
}
