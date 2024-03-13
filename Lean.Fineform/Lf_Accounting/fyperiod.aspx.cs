using FineUIPro;
using LeanFine.Lf_Business.Models.FICO;
using System;
using System.Data;
using System.Linq;

namespace LeanFine.Lf_Accounting
{
    public partial class fyperiod : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFICOChart";
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

            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);
            DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            string SelectDate = DPend.SelectedDate.Value.ToString("yyyy");
            //Lf_Business.Models.YF.LeanSerial_Entities DBSerial = new Lf_Business.Models.YF.LeanSerial_Entities();

            IQueryable<Fico_Period> q = DB.Fico_Periods; //.Include(u => u.Dept);

            // 在用户名称中搜索

            q = q.Where(u => u.UDF01.CompareTo(SelectDate) == 0);

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Fico_Period>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        #endregion Page_Load

        #region Events

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

            //    current.isDeleted = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyDate = DateTime.Now;
            //    DB.SaveChanges();

            BindGrid();
            //}
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
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

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events

        #region ExportExcel

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            IQueryable<Fico_Costing_Actual_Cost> q = DB.Fico_Costing_Actual_Costs; //.Include(u => u.Dept);

            // 在用户名称中搜索
            q = q.Where(u => u.Bc_ExpCategory.CompareTo("DTA") == 0);
            var qs = from p in q
                     select new
                     {
                         成本中心 = p.Bc_CostCode,
                         成本中心名称 = p.Bc_CostName,
                         科目 = p.Bc_TitleCode,
                         名称 = p.Bc_TitleName,
                     };
            if (qs.Any())
            {
                //DataTable Exp = new DataTable();
                //在库明细查询SQL
                string Xlsbomitem, ExportFileName;

                // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                Xlsbomitem = "Cost element";
                //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                ExportFileName = Xlsbomitem + ".xlsx";

                ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        #endregion ExportExcel
    }
}