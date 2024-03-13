using FineUIPro;
using System;
using System.Linq;

namespace LeanFine.Lf_Admin
{
    public partial class company : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreCompanyView";
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
            CheckPowerWithButton("CoreCompanyNew", btnNew);
            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Admin/company_new.aspx", "新增");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_Institution> q = DB.Adm_Institutions;

            // 在名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(r => r.ShortName.Contains(searchText));
            }
            q = q.Where(r => r.isDeleted == 0 && r.isEnabled == 0);
            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Adm_Institution>(q, Grid1);

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
            CheckPowerWithWindowField("CoreCompanyEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreCompanyDelete", Grid1, "deleteField");
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
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/company_edit.aspx?GUID=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
            }
            //int del_ID = GetSelectedDataKeyID(Grid1);

            string del_guid = GetSelectedDataKeyGUID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreCompanyDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Adm_Institution current = DB.Adm_Institutions.Find(Guid.Parse(del_guid));

                string Newtext = current.ShortName;
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del管理员* " + Newtext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "公司部门", "公司信息删除", OperateNotes);

                current.isDeleted = 1;
                current.isEnabled = 1;
                DB.SaveChanges();

                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion Events
    }
}