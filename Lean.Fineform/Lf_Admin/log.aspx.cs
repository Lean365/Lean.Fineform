//using EntityFramework.Extensions;
using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeanFine.Lf_Admin
{
    public partial class log : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreLogView";
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
            //CheckPowerWithButton("CoreLogDelete", btnDeleteSelected);

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            // 点击删除按钮时，至少选中一项
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_Log> q = DB.Adm_Logs;

            // 在错误信息中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(l => l.Message.Contains(searchText));
            }

            // 过滤错误级别
            if (ddlSearchLevel.SelectedValue != "ALL")
            {
                q = q.Where(l => l.Level == ddlSearchLevel.SelectedValue);
            }
            // 过滤错误级别
            string uid = GetIdentityName();
            if (uid != "admin")
            {
                q = q.Where(l => l.Message.Contains(uid));
            }

            // 过滤搜索范围
            if (ddlSearchRange.SelectedValue != "ALL")
            {
                DateTime today = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime Pastday3 = DateTime.Parse(DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd"));
                DateTime Pastday7 = DateTime.Parse(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
                DateTime Pastmonth = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
                DateTime Pastyear = DateTime.Parse(DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"));

                switch (ddlSearchRange.SelectedValue)
                {
                    case "TODAY":
                        q = q.Where(l => l.Date >= today);
                        break;

                    case "LAST3DAYS":
                        q = q.Where(l => l.Date >= Pastday3);
                        break;

                    case "LAST7DAYS":
                        q = q.Where(l => l.Date >= Pastday7);
                        break;

                    case "LASTMONTH":
                        q = q.Where(l => l.Date >= Pastmonth);
                        break;

                    case "LASTYEAR":
                        q = q.Where(l => l.Date >= Pastyear);
                        break;
                }
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和分页
            q = SortAndPage<Adm_Log>(q, Grid1);

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

        protected void ddlSearchLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlSearchRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreLogView", Grid1, "viewField");
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

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreLogDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            DB.Adm_Logs.Where(l => ids.Contains(l.ID)).DeleteFromQuery();

            // 重新绑定表格
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/log_view.aspx?ID=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events
    }
}