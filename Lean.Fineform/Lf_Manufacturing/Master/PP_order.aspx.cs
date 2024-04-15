using System;
using System.Data;
using System.Linq;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.Master
{
    public partial class Pp_order : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreOrderView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        //

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
            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);
            CheckPowerWithButton("CoreOrderNew", btnNew);
            //CheckPowerWithButton("CoreOrderNew", btnOrderIns);
            //CheckPowerWithButton("CoreOrderNew", btnSerialIns);

            //CheckPowerWithButton("CoreOrderNew", btnNew);
            //btnOrderIns.OnClientClick = Window1.GetShowReference("~/oneProduction/oneBasic/order_import.aspx", "订单批量导入");
            //btnSerialIns.OnClientClick = Window1.GetShowReference("~/oneProduction/oneBasic/order_serial.aspx", "序列号导入");
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_order_new.aspx", "新增");
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/ngcode_new.aspx", "新增不良类别");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Pp_Order> q = DB.Pp_Orders; //.Include(u => u.Dept);

            // 在用户名称中搜索

            //当前日期

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Porderhbn.Contains(searchText) || u.Porderlot.Contains(searchText) || u.Porderno.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}
            q = q.Where(u => u.isDeleted == 0);
            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Pp_Order>(q, Grid1);

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

        #endregion Events

        protected void DDLdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            CheckPowerWithLinkButtonField("CoreOrderEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreOrderDelete", Grid1, "deleteField");
        }

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_order_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreAcceptCatDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                Pp_Order current = DB.Pp_Orders.Find(del_ID);
                string Deltext = current.GUID + "," + current.Porderno + "," + current.Porderqty;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "生产订单删除", OperateNotes);

                current.isDeleted = 1;
                //current.Endtag = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();
                BindGrid();
            }
        }
    }
}