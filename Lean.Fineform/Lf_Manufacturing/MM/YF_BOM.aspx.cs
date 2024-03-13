using FineUIPro;
using LeanFine.Lf_Business.Models.MM;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_BOM : PageBase
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

            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";

            BindDDLHbn();
        }

        private void BindDDLHbn()
        {
            var q = from a in DB.Yf_Billofmaterials
                    select new
                    {
                        Serialno = a.Serialno.Substring(0, a.Serialno.Length - 4),
                    };

            var qs = q.Select(E => new { E.Serialno }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            SerialNo.DataSource = qs;
            SerialNo.DataTextField = "Serialno";
            SerialNo.DataValueField = "Serialno";
            SerialNo.DataBind();
            this.SerialNo.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindGrid()
        {
            //Lf_Business.Models.YF.Yifei_DTA_Entities DBYF = new Lf_Business.Models.YF.Yifei_DTA_Entities();

            IQueryable<Yf_Billofmaterial> q = DB.Yf_Billofmaterials; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = SerialNo.SelectedItem.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Serialno.Contains(searchText));
                q = q.OrderBy(u => u.Serialno);

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和数据库分页
                //q = SortAndPage<YF_Billofmaterial>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();
            }
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

            IQueryable<Yf_Billofmaterial> q = DB.Yf_Billofmaterials; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = SerialNo.SelectedItem.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Serialno.Contains(searchText));
                q = q.OrderBy(u => u.Serialno);
                var qs = from p in q
                         select new
                         {
                             序号 = p.Serialno,
                             层级 = p.Stratum,
                             设变 = p.Ecno,
                             用途 = p.SubMatAttribute,
                             物料组 = p.SubMatGroup,
                             工程顺 = p.EngSequences,
                             位置 = p.EngLocation,
                             上阶物料 = p.TopMaterial,
                             子物料 = p.SubMaterial,
                             用量 = p.Usageqty,
                             供应商 = p.Supplier,
                             币种 = p.Currency,
                             最新进价 = p.LastPrice,
                             库存 = p.Inventory,
                             采购 = p.Purchaser,
                             物料描述 = p.SubMatText,
                             最小交货时间 = p.Leadtime,
                             最小交货数量 = p.Moq,
                             制造商 = p.Maker,
                             制造物料 = p.MakerMaterial,
                             制造描述 = p.MakerMatText,
                             采购区域 = p.ProcurementRegion,
                             物料属性 = p.SubMatCategory,
                         };
                //DataTable Exp = new DataTable();
                //在库明细查询SQL
                string Xlsbomitem, ExportFileName;

                // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                Xlsbomitem = searchText + "_YF_BOMList";
                //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                ExportFileName = Xlsbomitem + ".xlsx";

                ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
        }

        #endregion ExportExcel

        protected void SerialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SerialNo.SelectedIndex != 0 && SerialNo.SelectedIndex != -1)
            {
                BindGrid();
            }
        }
    }
}