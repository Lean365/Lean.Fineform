﻿using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_action : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcActionView";
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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            // CheckPowerWithButton("CoreQacheckDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreFqcActionNew", btnNew);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);

            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_action_list.aspx", "新增") + Window1.GetMaximizeReference();
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
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            IQueryable<Qm_Improvement> q = DB.Qm_Improvements; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.qmModels.ToString().Contains(searchText) || u.qmMaterial.ToString().Contains(searchText));
            }
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
            //q = q.Where(u => u.LA002 > 0);

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
            q = SortAndPage<Qm_Improvement>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        public void BindDdlLine()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Qm_Improvements
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
            CheckPowerWithLinkButtonField("CoreFqcActionEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreFqcActionDelete", Grid1, "deleteField");
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
            Guid strGUID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));
            if (e.CommandName == "Edit")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcActionEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_action_edit.aspx?GUID=" + strGUID + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "Pint")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFinePrint"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_action_edit.aspx?GUID=" + strGUID + "&type=1") + Window1.GetMaximizeReference());
            }

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcActionDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Improvement current = DB.Qm_Improvements.Find(strGUID);
                string Deltext = current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmRegion + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmRejectqty + "," + current.qmJudgmentlevel;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良对策删除", OperateNotes);

                current.IsDeleted = 0;
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
    }
}