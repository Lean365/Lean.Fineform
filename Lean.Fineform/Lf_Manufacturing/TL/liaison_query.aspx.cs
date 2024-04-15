using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.TL
{
    public partial class liaison_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTlView";
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
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreTlNew", btnNew);
            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/TL/liaison_new.aspx", "新增") + Window1.GetMaximizeReference();
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlModel();
            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                var q = from a in DB.Pp_Liaisons
                        select a;

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_issuedate.Contains(searchText) || u.Ec_enterdate.Contains(searchText) || u.Ec_model.Contains(searchText) || u.Ec_modellist.Contains(searchText) || u.Ec_modellist.Contains(searchText) || u.Ec_letterno.Contains(searchText) || u.Ec_teppletterno.Contains(searchText) || u.Ec_eppletterno.Contains(searchText));
                }
                if (this.DDLModel.SelectedIndex != 0 && this.DDLModel.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Ec_model.Contains(DDLModel.SelectedItem.Text));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q);

                if (Grid1.RecordCount != 0)
                {
                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }

                //Grid1.DataSource = q;
                //Grid1.DataBind();
                //          //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                //          mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason FROM [dbo].[ProSapEngChanges] " +
                //  "    LEFT JOIN [dbo].[Ec_s]   ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 WHERE REPLACE(Ec_no,' ','') IS NULL "+
                // " and D_SAP_ZPABD_Z001 in (SELECT[D_SAP_ZPABD_S001]"+

                //" FROM[OneHerba2019].[dbo].[ProSapMaterials]" +
                //" left join[OneHerba2019].[dbo].[ProSapEngChangeSubs]" +
                //  "     on[D_SAP_ZPABD_S002]=[D_SAP_ZCA1D_Z002])    ";
                //          // 在用户名称中搜索
                //          string searchText = ttbSearchMessage.Text.Trim();
                //          if (!String.IsNullOrEmpty(searchText))
                //          {
                //              mysql = mysql + "  Where ECNNO='" + searchText + "' OR ISSUEDATE='" + searchText + " 'OR ECNTITLE='" + searchText + "' OR ECNMODEL='" + searchText + "' GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013 ORDER BY D_SAP_ZPABD_Z005 DESC;";
                //          }
                //          // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //          Grid1.RecordCount = GetTotalCount();

                //          // 2.获取当前分页数据
                //          DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                //          // 3.绑定到Grid
                //          Grid1.DataSource = table;
                //          Grid1.DataBind();
                //          //if (GetIdentityName() != "admin")
                //          //{)
                //          //    q = q.Where(u => u.Name != "admin");
                //          //}

                //          // 过滤启用状态
                //          //if (rblEnableStatus.SelectedValue != "all")
                //          //{
                //          //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                //          //}

                //          // 在查询添加之后，排序和分页之前获取总记录数
                //          //Grid1.RecordCount = q.Count();

                //          // 排列和数据库分页
                //          //q = SortAndPage<INVMB>(q, Grid1);

                //          //Grid1.DataSource = mydr;
                //          //Grid1.DataBind();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        #endregion Page_Load

        #region Events

        private void BindDdlModel()//ERP设变技术担当
        {
            var q = from a in DB.Pp_Liaisons
                    orderby a.Ec_model
                    select new
                    {
                        a.Ec_model
                    };

            // 绑定到下拉列表（启用模拟树功能）
            var qs = q.Select(E => new { E.Ec_model }).ToList().Distinct();
            DDLModel.DataTextField = "Ec_model";
            DDLModel.DataValueField = "Ec_model";
            DDLModel.DataSource = qs;
            DDLModel.DataBind();

            // 选中根节点
            this.DDLModel.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
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
            //CheckPowerWithLinkButtonField("CoreTlEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreTlDelete", Grid1, "deleteField");
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

        protected void Window1_Close(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";
            //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void DDLModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "Edit")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/TL/liaison_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreTlDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    //删除日志
            //    //int userID = GetSelectedDataKeyID(Grid1);
            //    Pp_Liaison current = DB.Pp_Liaisons.Find(del_ID);
            //    string Deltext = current.Ec_letterdoc;
            //    string OperateType = "删除";
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "技联信息删除", OperateNotes);

            //    current.isDeleted = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyDate = DateTime.Now;
            //    DB.SaveChanges();

            //    BindGrid();
            //}
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}