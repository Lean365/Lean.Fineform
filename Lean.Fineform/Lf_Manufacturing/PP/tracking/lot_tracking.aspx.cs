using System;

//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.PP.tracking
{
    public partial class lot_tracking : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTrackingView";
            }
        }

        #endregion ViewPower

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
            //BindDdlGUID();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            CheckPowerWithButton("CoreFineExport", Button1);
            //CheckPowerWithButton("CoreP1DOutputNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            Button1.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/tracking/lot_tracking_oph.aspx", "图表") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";
            BindDdlLine();
            BindGrid();
        }

        private void BindGrid()
        {
            var q_all = from p in DB.Pp_Tracking_Counts
                        where p.IsDeleted == 0
                        select new
                        {
                            p.Pro_Date,
                            p.Pro_Line,
                            p.Pro_Item,
                            p.Pro_Model,
                            p.Pro_Region,
                            p.Pro_Lot,
                            Pro_Process = (p.Pro_Process.Length == 1 ? "0" + p.Pro_Process : p.Pro_Process),
                            ProcessQty = p.Pro_ProcessQty,
                            p.Pro_MaxTime,
                            p.Pro_MinTime,
                            p.Pro_StdDev,
                        };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q_all = q_all.Where(u => u.Pro_Model.ToString().Contains(searchText) || u.Pro_Process.ToString().Contains(searchText) || u.Pro_Item.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q_all = q_all.Where(u => u.Pro_Date.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q_all = q_all.Where(u => u.Pro_Date.CompareTo(edate) <= 0);
            }
            if (this.DdlLine.SelectedIndex != -1 && this.DdlLine.SelectedIndex != 0)
            {
                q_all = q_all.Where(u => u.Pro_Line.Contains(this.DdlLine.SelectedText));
            }
            if (this.DDLModel.SelectedIndex != -1 && this.DDLModel.SelectedIndex != 0)
            {
                q_all = q_all.Where(u => u.Pro_Model.Contains(this.DDLModel.SelectedText));
            }
            if (this.DDLLot.SelectedIndex != -1 && this.DDLLot.SelectedIndex != 0)
            {
                q_all = q_all.Where(u => u.Pro_Lot.Contains(this.DDLLot.SelectedText));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q_all.AsQueryable());
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q_all.AsQueryable());

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
                Grid1.DataBind();
            }
        }

        public void BindDdlLine()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_Trackings
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                    where a.Pro_Date.CompareTo(sdate) >= 0
                    where a.Pro_Date.CompareTo(edate) <= 0
                    select new
                    {
                        a.Pro_Line
                    };

            var qs = q.Select(E => new { E.Pro_Line, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DdlLine.DataSource = qs;
            DdlLine.DataTextField = "Pro_Line";
            DdlLine.DataValueField = "Pro_Line";
            DdlLine.DataBind();

            this.DdlLine.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        public void BindDdlModel()
        {
            if (DdlLine.SelectedIndex != 0 || DdlLine.SelectedIndex != -1)
            {
                string line = DdlLine.SelectedItem.Text;
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
                var q = from a in DB.Pp_Tracking_Counts
                            //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                        where a.Pro_Date.CompareTo(sdate) >= 0
                        where a.Pro_Date.CompareTo(edate) <= 0
                        where a.Pro_Line.CompareTo(line) == 0
                        select new
                        {
                            a.Pro_Model,
                        };

                var qs = q.Select(E => new { E.Pro_Model, }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                DDLModel.DataSource = qs;
                DDLModel.DataTextField = "Pro_Model";
                DDLModel.DataValueField = "Pro_Model";
                DDLModel.DataBind();

                this.DDLModel.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        public void BindDdlLot()
        {
            if (DDLModel.SelectedIndex != 0 || DDLModel.SelectedIndex != -1)
            {
                string line = DdlLine.SelectedItem.Text;
                string model = DDLModel.SelectedItem.Text;

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
                var q = from a in DB.Pp_Tracking_Counts
                            //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                        where a.Pro_Date.CompareTo(sdate) >= 0
                        where a.Pro_Date.CompareTo(edate) <= 0
                        where a.Pro_Line.CompareTo(line) == 0
                        where a.Pro_Model.CompareTo(model) == 0
                        select new
                        {
                            a.Pro_Lot,
                        };

                var qs = q.Select(E => new { E.Pro_Lot, }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                DDLLot.DataSource = qs;
                DDLLot.DataTextField = "Pro_Lot";
                DDLLot.DataValueField = "Pro_Lot";
                DDLLot.DataBind();

                this.DDLLot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        #endregion Page_Load

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreP1DOutputDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreP1DOutputEdit", Grid1, "subeditField");
            //CheckPowerWithLinkButtonField("CoreP1DOutputEdit", Grid1, "editField");
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
        //    InsNetOperateNotes();
        //    // 执行数据库操作
        //    //DB.Adm_Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Adm_Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.Pp_P1d_OutputSubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.Pp_P1d_Outputs.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            BindGrid();
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

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindDdlLine();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindDdlLine();
            }
        }

        #endregion Events

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlLine.SelectedIndex != -1 && DdlLine.SelectedIndex != 0)
            {
                BindDdlModel();
                BindGrid();
            }
        }

        protected void DDLModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLModel.SelectedIndex != -1 && DDLModel.SelectedIndex != 0)
            {
                BindDdlLot();
                BindGrid();
            }
        }

        protected void DDLLot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLLot.SelectedIndex != -1 && DDLLot.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        protected void BtnList_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName, SheetName;
            SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_DailyList";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            var q =
                from p in DB.Pp_P1d_OutputSubs
                join b in DB.Pp_P1d_Outputs on p.GUID equals b.GUID
                where p.IsDeleted == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { b.Prodirect, b.Prostdcapacity, p.Prorealqty, b.Promodel, b.Prohbn, b.Prolot, b.Prodate, p.Prostime, p.Proetime, b.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin }
                into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Prostime,
                    g.Key.Proetime,
                    g.Key.Prodirect,
                    g.Key.Prolinename,
                    g.Key.Promodel,
                    g.Key.Prohbn,
                    g.Key.Prolot,
                    g.Key.Prostdcapacity,
                    g.Key.Prorealqty,
                    g.Key.Prorealtime,
                    g.Key.Prostopcou,
                    g.Key.Prostopmemo,
                    g.Key.Probadcou,
                    g.Key.Probadmemo,
                    g.Key.Prolinemin,
                    g.Key.Prolinestopmin,
                };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            else
            {
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
                if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
                }
            }

            var qs = from g in q
                     select new
                     {
                         生产日期 = g.Prodate,
                         时间段 = g.Prostime + "-" + g.Proetime,
                         班组 = g.Prolinename,
                         机种 = g.Promodel,
                         物料 = g.Prohbn,
                         批次 = g.Prolot,
                         计划 = g.Prostdcapacity,
                         实绩 = g.Prorealqty,
                         生产工数 = g.Prorealtime,
                         停线原因 = g.Prostopcou,
                         原因说明 = g.Prostopmemo,
                         未达成 = g.Probadcou,
                         未达成原因 = g.Probadmemo,
                         损失工数 = g.Prolinestopmin,
                         投入工数 = g.Prodirect * 60,
                     };
            if (qs.Any())
            {
                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName, "DTA 追溯明细");
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}