using System;
using System.Data;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_Special_Material : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMMView";
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

            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
        }

        private void BindGrid()
        {
            var qs = from a in DB.Yf_Billofmaterials
                     join b in DB.Pp_SapModelDests on a.Serialno.Substring(0, a.Serialno.Length - 4) equals b.D_SAP_DEST_Z001
                     where a.ProcurementRegion == "D"
                     group a by new
                     {
                         b.D_SAP_DEST_Z002,
                         a.SubMaterial,
                         a.SubMatText,
                     } into g
                     select new
                     {
                         SubModel = g.Key.D_SAP_DEST_Z002,
                         g.Key.SubMaterial,
                         g.Key.SubMatText,
                     };
            //var qss=from a in qs
            //        group a by new
            //        {
            //            a.SubModel,
            //            a.SubMaterial,
            //            a.SubMatText,
            //        } into g
            //        select new
            //        {
            //            g.Key.SubModel,
            //            g.Key.SubMaterial,
            //            g.Key.SubMatText,
            //        };

            var qsss = from a in qs
                       group a by new
                       {
                           a.SubMaterial
                       }
                       into g
                       select new
                       {
                           Count = g.Count(),
                           g.Key.SubMaterial
                       } into b
                       where b.Count == 1
                       select b;

            var q = from a in qs
                    where (from b in qsss
                           where b.SubMaterial == a.SubMaterial
                           //where b.SubMaterial.Contains("3E95100052A")
                           select b.SubMaterial)
                        .Contains(a.SubMaterial)
                    select new
                    {
                        a.SubModel,
                        a.SubMaterial,
                        a.SubMatText,
                    };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.SubMaterial.Contains(searchText));

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q);
                if (GridHelper.GetTotalCount(q) > 0)
                {
                    // 排列和数据库分页
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q);
                    // 3.绑定到Grid
                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    // 3.绑定到Grid
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }
            }
            else
            {
                // 3.绑定到Grid
                Grid1.DataSource = "";
                Grid1.DataBind();
            }
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
            //CheckPowerWithLinkButtonField("CoreLineEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreLineDelete", Grid1, "deleteField");
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

        //可选中多项删除
        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreProdataDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }
        //    InsNetOperateNotes();
        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proLines.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "Edit")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_line_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreLineDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    //删除日志
            //    //int userID = GetSelectedDataKeyID(Grid1);
            //    Pp_Line current = DB.Pp_Lines.Find(del_ID);
            //    string Deltext = current.linename;
            //    string OperateType = "删除";
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "班组删除", OperateNotes);

            //    current.IsDeleted = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyDate = DateTime.Now;
            //    DB.SaveChanges();

            BindGrid();
            //}
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events
    }
}