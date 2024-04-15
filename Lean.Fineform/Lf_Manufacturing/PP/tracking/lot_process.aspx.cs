using System;

//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.PP.tracking
{
    public partial class lot_process : PageBase
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
            CheckPowerWithButton("CoreTrackingNew", btnNew);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/tracking/lot_process_new.aspx", "新增") + Window1.GetMaximizeReference();

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlLine();
            BindGrid();
        }

        private void BindGrid()
        {
            var q = from p in DB.Pp_Tracking_Times
                        //join b in DB.Pp_Tracking_Times on new { p.Pro_Item, p.Pro_Process } equals new { b.Pro_Item, b.Pro_Process }
                    where p.isDeleted == 0
                    select new
                    {
                        p.Pro_Item,
                        p.Pro_Model,
                        p.Pro_Region,
                        p.Pro_Manhour,
                        Pro_Process = (p.Pro_Process.Length == 1 ? "0" + p.Pro_Process : p.Pro_Process),
                        p.Pro_Tractime,
                    };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Pro_Item.ToString().Contains(searchText) || u.Pro_Process.ToString().Contains(searchText) || u.Pro_Model.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            if (this.DDLModel.SelectedIndex != -1 && this.DDLModel.SelectedIndex != 0)
            {
                q = q.Where(u => u.Pro_Model.Contains(this.DDLModel.SelectedText));
            }
            q.Distinct().ToList();

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q.AsQueryable());
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q.AsQueryable());

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
            var q = from a in DB.Pp_P1d_Outputs

                    select new
                    {
                        a.Promodel
                    };

            var qs = q.Select(E => new { E.Promodel, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLModel.DataSource = qs;
            DDLModel.DataTextField = "Promodel";
            DDLModel.DataValueField = "Promodel";
            DDLModel.DataBind();

            this.DDLModel.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion Events

        protected void DDLModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLModel.SelectedIndex != -1 && DDLModel.SelectedIndex != 0)
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
            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            Xlsbomitem = "Lot_Process_Times";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            var q = from p in DB.Pp_Tracking_Times
                        //join b in DB.Pp_Tracking_Times on new { p.Pro_Item, p.Pro_Process } equals new { b.Pro_Item, b.Pro_Process }
                    where p.isDeleted == 0
                    select new
                    {
                        ItemMaster = p.Pro_Item,
                        ModelName = p.Pro_Model,
                        Region = p.Pro_Region,
                        ST = p.Pro_Manhour,
                        Process = (p.Pro_Process.Length == 1 ? "0" + p.Pro_Process : p.Pro_Process),
                        Tractime = p.Pro_Tractime,
                    };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.ItemMaster.ToString().Contains(searchText) || u.Process.ToString().Contains(searchText) || u.ModelName.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            if (this.DDLModel.SelectedIndex != -1 && this.DDLModel.SelectedIndex != 0)
            {
                q = q.Where(u => u.ModelName.Contains(this.DDLModel.SelectedText));
            }

            if (q.Any())
            {
                ConvertHelper.LinqConvertToDataTable(q);

                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(q), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}