using System;
using System.Data;
using System.Linq;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_switch_note : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DNoteView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        /// <summary>
        /// 页面加载事件处理函数
        /// </summary>
        /// <param name="sender">事件源</param>
        /// <param name="e">事件参数</param>

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
            // CheckPowerWithButton("CoreNgcodeDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreNgcodeNew", btnNew);
            //CheckPowerWithButton("CoreProlineNew", btnP2d);

            CheckPowerWithButton("CoreP2DNoteNew", btnNew);
            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P2D/p2d_switch_note_new.aspx", "新增");
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/ngcode_new.aspx", "新增不良类别");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            Prodate.SelectedDate = DateTime.Now.AddDays(-1);//DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Pp_P2d_Switch_Note> q = DB.Pp_P2d_Switch_Notes; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                //q = q.Where(u => u.Prodate.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            q = q.Where(u => u.IsDeleted == 0).OrderByDescending(u => u.Prodate);
            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}
            string sdate = Prodate.SelectedDate.Value.ToString("yyyyMM");
            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.Substring(0, 6).CompareTo(sdate) == 0);
            }
            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Pp_P2d_Switch_Note>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        #endregion Page_Load

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
            //CheckPowerWithWindowField("CoreTimeImportEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreTimeImportDelete", Grid1, "deleteField");
            // CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            CheckPowerWithLinkButtonField("CoreP2DNoteEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreP2DNoteDelete", Grid1, "deleteField");
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
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P2D/p2d_switch_note_edit.aspx?GUID=" + keys[0].ToString() + "&type=1"));//+ Window1.GetMaximizeReference()窗口最大化
            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP2DNoteDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                Pp_P2d_Switch_Note current = DB.Pp_P2d_Switch_Notes.Find(del_ID);
                string Deltext = current.GUID + "," + current.Prodate;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "制二课记事删除", OperateNotes);

                current.IsDeleted = 1;
                //current.Endtag = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();
                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            //Alert.ShowInTop("窗体被关闭了。参数：");
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

        protected void Prodate_TextChanged(object sender, EventArgs e)
        {
            if (Prodate.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        #endregion Events

        #region Export

        protected void BtnList_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName, SheetName;
            SheetName = "S" + Prodate.SelectedDate.Value.ToString("yyyyMM");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P2d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = Prodate.SelectedDate.Value.ToString("yyyyMM") + "_p2d_switch_note";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            var q =
                from p in DB.Pp_P2d_Switch_Notes
                    //join b in DB.Pp_P2d_Outputs on p.GUID equals b.GUID
                where p.IsDeleted == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                //group p by new { p.Proorder, p.Proshort, p.Prost, p.Propcbatype, p.Prohandoffnum, p.Prohandofftime, p.Protime, p.Promaketime, p.Prodowntime, p.Prolosstime, p.Proorderqty, p.Propcbaside, p.Prodirect, p.Propcbastated, p.Prorealqty, p.Promodel, p.Prohbn, p.Prolot, p.Prodate, p.Prostime, p.Proetime, p.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin, p.UDF51, p.UDF52, p.UDF53, p.UDF54, p.Prorealtotal }
                //into g
                select new
                {
                    p.Prodate,
                    p.ProSmtSwitchNum,
                    p.ProSmtSwitchTotalTime,
                    p.ProAitSwitchNum,
                    p.ProAiStopTime,
                    p.ProHandSopTime,
                    p.ProHandPerson,
                    p.ProHandSopTotalTime,
                    p.ProHandSwitchNum,
                    p.ProHandSwitchTime,
                    p.ProHandSwitchTotalTime,
                    p.ProRepairSopTime,
                    p.ProRepairPerson,
                    p.ProRepairSopTotalTime,
                    p.ProRepairSwitchNum,
                    p.ProRepairSwitchTime,
                    p.ProRepairSwitchTotalTime,
                };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.ProSmtSwitchNum.ToString().Contains(searchText) || u.ProHandSwitchNum.ToString().Contains(searchText) || u.ProRepairPerson.ToString().Contains(searchText) || u.ProRepairSwitchNum.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            string sdate = Prodate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
            }

            var qs = from p in q
                     orderby p.Prodate
                     select new
                     {
                         生产日期 = p.Prodate,
                         SMT切换次数 = p.ProSmtSwitchNum,
                         SMT总切换时间 = p.ProSmtSwitchTotalTime,
                         自插切换次数 = p.ProAitSwitchNum,
                         自插总切换时间 = p.ProAiStopTime,
                         手插读SOP时间 = p.ProHandSopTime,
                         手插人数 = p.ProHandPerson,
                         手插读SOP总时间 = p.ProHandSopTotalTime,
                         手插切换次数 = p.ProHandSwitchNum,
                         手插切换时间 = p.ProHandSwitchTime,
                         手插切换总时间 = p.ProHandSwitchTotalTime,
                         修正读SOP时间 = p.ProRepairSopTime,
                         修正人数 = p.ProRepairPerson,
                         修正读SOP总时间 = p.ProRepairSopTotalTime,
                         修正切换次数 = p.ProRepairSwitchNum,
                         修正切换时间 = p.ProRepairSwitchTime,
                         修正切换总时间 = p.ProRepairSwitchTotalTime,
                     };
            if (qs.Any())
            {
                //ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs.AsQueryable().Distinct()), SheetName, Export_FileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        #endregion Export
    }
}