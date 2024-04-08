using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class dept_user_addnew : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreDeptUserNew";
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
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Adm_Dept current = DB.Adm_Depts.Find(id);
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlDept();
            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_User> q = DB.Adm_Users.Include(u => u.Dept);

            // 在职务名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Name.Contains(searchText) || u.ChineseName.Contains(searchText) || u.EnglishName.Contains(searchText) || u.Address.Contains(searchText));
            }
            if (this.ddlDept.SelectedIndex != -1 && this.ddlDept.SelectedIndex != 0)
            {
                q = q.Where(u => u.Address.Contains(this.ddlDept.SelectedText));
            }
            q = q.Where(u => u.Name != "admin");

            // 排除所有已经属于某个部门的用户
            q = q.Where(u => u.Dept == null);

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和分页
            q = SortAndPage<Adm_User>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        public void BindDdlDept()
        {
            var q = (from a in DB.Adm_Users

                     select new
                     {
                         a.Address
                     }).ToList();

            //包含子集

            var qs = q.Select(E => new { E.Address, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlDept.DataSource = qs;
            ddlDept.DataTextField = "Address";
            ddlDept.DataValueField = "Address";
            ddlDept.DataBind();

            this.ddlDept.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion Page_Load

        #region Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            int deptID = GetQueryIntValue("id");

            // 跨页保持选中行
            IEnumerable<int> ids = Grid1.SelectedRowIDArray.Select(u => Convert.ToInt32(u));

            Adm_Dept dept = Attach<Adm_Dept>(deptID);

            DB.Adm_Users.Where(u => ids.Contains(u.ID))
                .ToList()
                .ForEach(u => u.Dept = dept);

            DB.SaveChanges();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedIndex != -1 && ddlDept.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        #endregion Events
    }
}