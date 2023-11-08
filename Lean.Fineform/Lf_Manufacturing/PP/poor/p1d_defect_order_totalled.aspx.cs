using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
namespace Lean.Fineform.Lf_Manufacturing.PP.poor
{
    public partial class p1d_defect_order_totalled : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DDefectView";
            }
        }

        #endregion

        #region Page_Load



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreDefectDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreDefectNew", btnNew);
            //CheckPowerWithButton("CoreDefectNew", btnP2d);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDefect/defect_new.aspx", "新增") + Window1.GetMaximizeReference();
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDefect/defect_p2d_new.aspx", "新增");
            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";

            BindGrid();
        }



        private void BindGrid()
        {
            try
            {
                //IQueryable<Pp_DefectTotal> q = DB.Pp_DefectTotals; //.Include(u => u.Dept);

                var q = from a in DB.Pp_DefectTotals
                            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
                            //where b.Proecnno == strecn
                            //where (from d in DB.Pp_Outputs
                            //       where d.isDelete == 0
                            //       select d.Prolot)
                            //       .Contains(a.Prolot)//投入日期
                        //where a.Proorder.Substring(0, 2).Contains("44")

                        select a;


                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Prodate.Contains(searchText) || u.Promodel.Contains(searchText) || u.Prolot.Contains(searchText) || u.Proorder.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                }

                // 在用户名称中搜索

                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");


                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.Substring(0, 8).CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.Substring(9, 8).CompareTo(edate) <= 0);
                }

                q = q.Where(u => u.isDelete == 0);

                var qs = (from p in q

                          select new
                          {
                              p.GUID,
                              p.Prolot,
                              p.Prolinename,
                              p.Prodate,
                              p.Proorder,
                              p.Proorderqty,
                              p.Prorealqty,
                              p.Pronobadqty,
                              p.Probadtotal,
                              Prodirectrate = ((p.Prorealqty == 0 ? 0 : (decimal)p.Pronobadqty / p.Prorealqty)),//  保留一位,
                              Probadrate = ((p.Prorealqty == 0 ? 0 : (decimal)p.Probadtotal / p.Prorealqty)),
                              Promodel = p.Promodel,

                          }); ;

                Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                // 排列和数据库分页
                // 2.获取当前分页数据
                if (GridHelper.GetTotalCount(qs) > 0)
                {
                    // 排列和数据库分页
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                    // 3.绑定到Grid
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    // 3.绑定到Grid
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }
                //ttbSearchMessage.Text = "";
                ConvertHelper.LinqConvertToDataTable(qs);
                // 当前页的合计
                OutputSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);

            }
        }

        #endregion

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            // CheckPowerWithWindowField("CoreDefectEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreDefectDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            CheckPowerWithLinkButtonField("CoreP1DDefectEdit", Grid1, "editField");
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
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/poor/p1d_defect_order_totalled_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }

            //string defectID = GetSelectedDataKeyGUID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreDefectDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    //删除日志
            //    int userID = GetSelectedDataKeyID(Grid1);
            //    proDefect current = DB.proDefects.Find(defectID);
            //    string Deltext = current.Prodate + "," + current.Prolinename + "," + current.Prolot;
            //    string OperateType = current.Defectguid;
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    NetCountHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "不具合删除", OperateNotes);

            //    DB.proDefects.Where(l => l.ID == defectID).Delete();


            //    BindGrid();

            //}
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {

            if (DPend.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion
        #region ExportExcel


        protected void BtnExport_Click(object sender, EventArgs e)
        {

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            Xlsbomitem = DPstart.SelectedDate.Value.ToString("yyyyMM") + "DefectRecord_Data";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            IQueryable<Pp_DefectTotal> q = DB.Pp_DefectTotals; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prodate.Contains(searchText) || u.Udf001.Contains(searchText) || u.Prolot.Contains(searchText) || u.Proorder.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            }
            else
            {
                // 在用户名称中搜索

                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");


                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
            }
            q = q.Where(u => u.isDelete == 0);


            var qs = from p in q
                     .OrderBy(s => s.Prodate)
                     select new
                     {
                         生产批次 = p.Prolot,
                         生产班组 = p.Prolinename,
                         生产日期 = p.Prodate,
                         生产数量 = p.Prorealqty,
                         无不良台数 = p.Pronobadqty,
                         生产订单 = p.Proorder,
                         生产机种 = p.Promodel,
                         订单数量 = p.Proorderqty,
                     };

            ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
            //Grid1.AllowPaging = false;
            //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
            //Grid1.AllowPaging = true;
        }



        #endregion
        //合计表格
        private void OutputSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Prorealqty"]);
                rTotal += Convert.ToDecimal(row["Pronobadqty"]);
                ratio += Convert.ToDecimal(row["Probadtotal"]);
            }


            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prorealqty", pTotal.ToString("F0"));
            summary.Add("Pronobadqty", rTotal.ToString("F0"));
            summary.Add("Probadtotal", ratio.ToString("F0"));

            Grid1.SummaryData = summary;

        }


    }
}
