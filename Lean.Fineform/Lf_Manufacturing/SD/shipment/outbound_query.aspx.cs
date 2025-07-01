using System;
using System.Data;
using System.Linq;
using FineUIPro;
using LeanFine.Lf_Business.Models.YF;

namespace LeanFine.Lf_Manufacturing.SD.shipment
{
    public partial class outbound_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreOutboundScanView";
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

            CheckPowerWithButton("CoreFineExport", BtnExport);
            CheckPowerWithButton("CoreFineExport", BtnDestination);
            CheckPowerWithButton("CoreFineExport", BtnProfit);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);
            DpEndDate.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            BtnDestination.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing.PP/manufacturing/shipment/outbound_destination.aspx", "目的地分析");
            BtnProfit.OnClientClick = Window1.GetShowReference("~/Lf_Report/outbound_echarts.aspx", "目的地分析");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            Lf_Business.Models.YF.LeanSerial_Entities DBSerial = new Lf_Business.Models.YF.LeanSerial_Entities();
            var q = from a in DBSerial.DTASSET_SCANNER_OUT_SUB

                    select a; //.Include(u => u.Dept);

            var qs = q.Select(a =>
                        new
                        {
                            a.OUTS001,
                            a.OUTS002,
                            a.OUTS003,
                            a.OUTS004,
                            a.OUTS005,
                            OUTS008 = (a.OUTS008.IndexOf("_") == 0 ? a.OUTS008 : (a.OUTS008.Substring(0, a.OUTS008.IndexOf("_")))),
                            a.OUTS006,
                        });
            // 在用户名称中搜索
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            qs = qs.Where(u => u.OUTS001.CompareTo(edate) == 0);

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(qs);
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
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

            //    current.IsDeleted = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyDate = DateTime.Now;
            //    DB.SaveChanges();

            BindGrid();
            //}
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
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
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            Lf_Business.Models.YF.LeanSerial_Entities DBSerial = new Lf_Business.Models.YF.LeanSerial_Entities();

            IQueryable<DTASSET_SCANNER_OUT_SUB> q = DBSerial.DTASSET_SCANNER_OUT_SUB; //.Include(u => u.Dept);

            var qs = q.Select(a =>
                        new
                        {
                            出货日期 = a.OUTS001,
                            出货发票 = a.OUTS002,
                            仕向别 = a.OUTS003,
                            物料 = a.OUTS004,
                            序列号 = a.OUTS005,
                            数量 = (a.OUTS008.IndexOf("_") == 0 ? a.OUTS008 : (a.OUTS008.Substring(0, a.OUTS008.IndexOf("_")))),
                            目的地 = a.OUTS006,
                        });
            // 在用户名称中搜索
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            qs = qs.Where(u => u.出货日期.CompareTo(edate) == 0);

            if (qs.Any())
            {
                //DataTable Exp = new DataTable();
                //在库明细查询SQL
                string Prefix_XlsxName, Export_FileName, SheetName;
                SheetName = "D" + DateTime.Now.ToString("yyyyMM");
                // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                Prefix_XlsxName = edate + "_Serial_Data";
                //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                Export_FileName = Prefix_XlsxName + ".xlsx";

                ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName, "DTA 序列号明细");
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