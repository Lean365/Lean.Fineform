using FineUIPro;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Admin
{
    public partial class operatelog : PageBase
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
                BindDDLUser();
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("CoreWorklogDelete", btnDeleteSelected);

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            // 点击删除按钮时，至少选中一项
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_OperateLog> q = DB.Adm_OperateLogs;

            // 在错误信息中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(l => l.OperateNotes.Contains(searchText));
            }

            // 过滤错误级别
            string uid = GetIdentityName();
            if (uid != "admin")
            {
                if (ddlSearchOperateUser.SelectedIndex != 0 && ddlSearchOperateUser.SelectedIndex != -1)
                {
                    q = q.Where(l => l.OperateUserId == ddlSearchOperateUser.SelectedValue);
                }
                else
                {
                    q = q.Where(l => l.OperateUserId == uid);
                }
            }
            else
            {
                if (ddlSearchOperateUser.SelectedIndex != 0 && ddlSearchOperateUser.SelectedIndex != -1)
                {
                    q = q.Where(l => l.OperateUserId == ddlSearchOperateUser.SelectedValue);
                }
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
                        q = q.Where(l => l.OperateTime >= today);
                        break;

                    case "LAST3DAYS":
                        q = q.Where(l => l.OperateTime >= Pastday3);
                        break;

                    case "LAST7DAYS":
                        q = q.Where(l => l.OperateTime >= Pastday7);
                        break;

                    case "LASTMONTH":
                        q = q.Where(l => l.OperateTime >= Pastmonth);
                        break;

                    case "LASTYEAR":
                        q = q.Where(l => l.OperateTime >= Pastyear);
                        break;
                }
            }
            //string s = GetIdentityName();
            //q = q.Where(l => l.OperateUserId == s);
            q = q.OrderBy(u => u.OperateUserName);

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和分页
            q = SortAndPage<Adm_OperateLog>(q, Grid1);

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

        protected void ddlSearchOperateUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSearchOperateUser.SelectedIndex != 0 && ddlSearchOperateUser.SelectedIndex != -1)
            {
                BindGrid();
            }
        }

        protected void ddlSearchRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreLogView", Grid1, "viewField");
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        private void BindDDLUser()
        {
            if (GetIdentityName() == "admin")
            {
                var q = (from a in DB.Adm_OperateLogs
                             //where a.OperateUserDept.Contains(dept)
                         join b in DB.Adm_Users on a.OperateUserId equals b.Name
                         select new
                         {
                             b.ChineseName,
                             a.OperateUserId,
                         }).OrderBy(u => u.ChineseName).Distinct().ToList();

                // 绑定到下拉列表（启用模拟树功能）

                ddlSearchOperateUser.DataTextField = "ChineseName";
                ddlSearchOperateUser.DataValueField = "OperateUserId";
                ddlSearchOperateUser.DataSource = q;
                ddlSearchOperateUser.DataBind();

                // 选中根节点
                //ddlSearchOperateUser.SelectedValue = "0";
            }
            else
            {
                string uid = GetIdentityName();
                var q = (from a in DB.Adm_OperateLogs
                         where a.OperateUserId.Contains(uid)
                         join b in DB.Adm_Users on a.OperateUserId equals b.Name
                         select new
                         {
                             b.ChineseName,
                             a.OperateUserId,
                         }).OrderBy(u => u.ChineseName).Distinct().ToList();

                // 绑定到下拉列表（启用模拟树功能）

                ddlSearchOperateUser.DataTextField = "ChineseName";
                ddlSearchOperateUser.DataValueField = "OperateUserId";
                ddlSearchOperateUser.DataSource = q;
                ddlSearchOperateUser.DataBind();
            }

            this.ddlSearchOperateUser.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/operatelog_view.aspx?GUID=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
            }
        }
    }

    #endregion Events
}