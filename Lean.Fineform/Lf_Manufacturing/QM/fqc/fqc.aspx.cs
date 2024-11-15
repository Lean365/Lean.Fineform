﻿using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc : PageBase
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

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                BindDdlLine();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            // CheckPowerWithButton("CoreQacheckDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreFqcNew", btnNew);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);
            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            btnNew.OnClientClick = "F.control_enable_ajax=false;";
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_new.aspx", "新增") + Window1.GetMaximizeReference();
            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_new.aspx", "新增P1D生产日报");
            //btnP2dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p2d_new.aspx", "新增P2D生产日报");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            q = q.Where(u => u.IsDeleted == 0);
            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.qmModels.ToString().Contains(searchText) || u.qmMaterial.ToString().Contains(searchText));
            }
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (this.DdlLine.SelectedIndex != -1 && this.DdlLine.SelectedIndex != 0)
            {
                q = q.Where(u => u.qmLine.Contains(this.DdlLine.SelectedText));
            }

            //q = q.Where(u => u.qmLine > 0);

            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}

            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Qm_Outgoing>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        public void BindDdlLine()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Qm_Outgoings
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    select new
                    {
                        a.qmLine
                    };

            var qs = q.Select(E => new { E.qmLine, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DdlLine.DataSource = qs;
            DdlLine.DataTextField = "qmLine";
            DdlLine.DataValueField = "qmLine";
            DdlLine.DataBind();
            this.DdlLine.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion Page_Load

        #region Events

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlLine.SelectedIndex != -1 && DdlLine.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindDdlLine();
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindDdlLine();
                BindGrid();
            }
        }

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
            //CheckPowerWithWindowField("CoreQacheckEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreFqcDelete", Grid1, "deleteField");
            CheckPowerWithLinkButtonField("CoreFqcEdit", Grid1, "editField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
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
        //    if (!CheckPower("CoreQacheckDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.pqQachecks.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();
        //    NetLogRecord();
        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int str_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Edit")
            {
                //object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_edit.aspx?ID=" + str_ID + "&type=1") + Window1.GetMaximizeReference());
            }

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Outgoing current = DB.Qm_Outgoings.Find(str_ID);
                string Deltext = current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmCheckmethod + "," + current.qmSamplingQty + "," + current.qmJudgment + "," + current.qmJudgmentlevel + "," + current.qmRejectqty + "," + current.qmJudgment + "," + current.qmAcceptqty + "," + current.qmOrderqty + "," + current.qmCheckout + "," + current.qmProqty;
                string OperateType = "删除";
                string OperateNotes = "Del产品检验记录* " + Deltext + "*Del产品检验记录 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品检验记录删除", OperateNotes);

                current.IsDeleted = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();

                BindGrid();
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

        #endregion Events

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
            string Xlsbomitem, ExportFileName;
            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            q = q.Where(u => u.IsDeleted == 0);
            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.qmModels.ToString().Contains(searchText) || u.qmMaterial.ToString().Contains(searchText));
            }
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (this.DdlLine.SelectedIndex != -1 && this.DdlLine.SelectedIndex != 0)
            {
                q = q.Where(u => u.qmLine.Contains(this.DdlLine.SelectedText));
            }
            var qs = from a in q
                     select new
                     {
                         检查人员 = a.qmInspector,
                         班别 = a.qmLine,
                         生产工单 = a.qmOrder,
                         机种 = a.qmModels,
                         物料 = a.qmMaterial,
                         查检日期 = a.qmCheckdate,
                         生产批号 = a.qmProlot,
                         批号说明 = a.qmLotserial,
                         检验方式 = a.qmCheckmethod,
                         抽样数 = a.qmSamplingQty,
                         判定 = a.qmJudgment,
                         不良级别 = a.qmJudgmentlevel,
                         验退数 = a.qmRejectqty,
                         判定说明 = a.qmJudgment,
                         验收数 = a.qmAcceptqty,
                         生产订单数量 = a.qmOrderqty,
                         检验次数 = a.qmCheckout,
                         生产数 = a.qmProqty,
                     };
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_Outgoing_Inspection_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            DataTable ExportTB = ConvertHelper.LinqConvertToDataTable(qs);
            //直接用这个判断即可，是不是很简单
            //ExportTB.Rows[i][j] == ExportTB.Value

            //下面一句也可以用，但是不知道那个效率更好，有兴趣的同学对比一下
            //if (!Convert.IsDBNull(ExportTB.Rows[r][i]))

            if (ExportTB != null && ExportTB.Rows.Count > 0)
            {
                ExportHelper.EpplustoXLSXfile(ExportTB, Xlsbomitem, ExportFileName);
            }
            else
            {
                Alert.ShowInTop("没有数据被导出！", "警告提示", MessageBoxIcon.Warning);
            }
        }
    }
}