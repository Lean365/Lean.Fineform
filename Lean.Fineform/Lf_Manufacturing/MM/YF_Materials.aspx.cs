using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_Materials : PageBase
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

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                string searchText = ttbSearchMessage.Text.Trim().ToUpper();
                if (!String.IsNullOrEmpty(searchText.Trim()))
                {
                    Lf_Business.Models.YF.Yifei_DTA_Entities DBYFdta = new Lf_Business.Models.YF.Yifei_DTA_Entities();

                    var q_C100 = from ta in DBYFdta.INVMB
                                 join mv in DBYFdta.CMSMV on ta.MB067 equals mv.MV001
                                 select new
                                 {
                                     ta.MB001,
                                     ta.MB002,
                                     ta.MB003,
                                     ta.MB032,
                                     MB067 = mv.MV002,
                                     ta.UDF05,
                                     ta.UDF51,
                                     ta.MB080,
                                 };

                    q_C100 = q_C100.Where(u => u.MB001.Contains(searchText.Trim()));

                    var qs_C100 = q_C100.Select(E => new { MB120 = "C100", MB001 = E.MB001.Trim(), E.MB002, E.MB003, E.MB032, E.MB067, E.UDF05, E.UDF51, E.MB080 }).ToList().Distinct();

                    Lf_Business.Models.YF.Yifei_TAC_Entities DBYFtac = new Lf_Business.Models.YF.Yifei_TAC_Entities();
                    var q_H100 = from ta in DBYFtac.INVMB
                                 join mv in DBYFtac.CMSMV on ta.MB067 equals mv.MV001
                                 select new
                                 {
                                     ta.MB001,
                                     ta.MB002,
                                     ta.MB003,
                                     ta.MB032,
                                     MB067 = mv.MV002,
                                     ta.UDF05,
                                     ta.UDF51,
                                     ta.MB080,
                                 };

                    q_H100 = q_H100.Where(u => u.MB001.Contains(searchText));

                    var qs_H100 = q_H100.Select(E => new { MB120 = "H100", MB001 = E.MB001.Trim(), E.MB002, E.MB003, E.MB032, E.MB067, E.UDF05, E.UDF51, E.MB080 }).ToList().Distinct();

                    var q = qs_C100.Union(qs_H100);
                    q = q.OrderBy(a => a.MB001);

                    // 在用户名称中搜索

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
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);
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

        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            //labResult.Text = keys[0].ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lc_Yifei/YF_Materials_view.aspx?MB001=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
        }

        #endregion Events

        #region ExportExcel

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            string searchText = ttbSearchMessage.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText.Trim()))
            {
                Lf_Business.Models.YF.Yifei_DTA_Entities DBYFdta = new Lf_Business.Models.YF.Yifei_DTA_Entities();

                var q_C100 = from ta in DBYFdta.INVMB
                             join mv in DBYFdta.CMSMV on ta.MB067 equals mv.MV001
                             select new
                             {
                                 ta.MB001,
                                 ta.MB002,
                                 ta.MB003,
                                 ta.MB032,
                                 MB067 = mv.MV002,
                                 ta.UDF05,
                                 ta.UDF51,
                                 ta.MB080,
                             };

                q_C100 = q_C100.Where(u => u.MB001.Contains(searchText.Trim()));

                var qs_C100 = q_C100.Select(E => new { 工厂 = "C100", 物料 = E.MB001.Trim(), 描述 = E.MB002, 制造商品名 = E.MB003, 主供应商 = E.MB032, 采购人员 = E.MB067, 最新业者 = E.UDF05, 最新核价 = E.UDF51, 制造商 = E.MB080 }).ToList().Distinct();

                Lf_Business.Models.YF.Yifei_TAC_Entities DBYFtac = new Lf_Business.Models.YF.Yifei_TAC_Entities();
                var q_H100 = from ta in DBYFtac.INVMB
                             join mv in DBYFtac.CMSMV on ta.MB067 equals mv.MV001
                             select new
                             {
                                 ta.MB001,
                                 ta.MB002,
                                 ta.MB003,
                                 ta.MB032,
                                 MB067 = mv.MV002,
                                 ta.UDF05,
                                 ta.UDF51,
                                 ta.MB080,
                             };

                q_H100 = q_H100.Where(u => u.MB001.Contains(searchText));

                var qs_H100 = q_H100.Select(E => new { 工厂 = "H100", 物料 = E.MB001.Trim(), 描述 = E.MB002, 制造商品名 = E.MB003, 主供应商 = E.MB032, 采购人员 = E.MB067, 最新业者 = E.UDF05, 最新核价 = E.UDF51, 制造商 = E.MB080 }).ToList().Distinct();

                var q = qs_C100.Union(qs_H100);
                q = q.OrderBy(a => a.物料);

                // 在用户名称中搜索

                // 在查询添加之后，排序和分页之前获取总记录数
                if (q.AsQueryable().Any())
                {
                    //DataTable Exp = new DataTable();
                    //在库明细查询SQL
                    string Prefix_XlsxName, Export_FileName, SheetName;
                    SheetName = "D" + DateTime.Now.ToString("yyyyMM");
                    // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                    Prefix_XlsxName = searchText + "_YF_Materials";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";

                    ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
                    Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()), Prefix_XlsxName, Export_FileName, "DTA 易飞物料明细");
                    Grid1.AllowPaging = true;
                }
                else
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion ExportExcel
    }
}