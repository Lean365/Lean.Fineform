using System;
using System.Data;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Accounting
{
    public partial class costing_grossmargin : PageBase
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

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 每页记录数
            Grid1.PageSize = 50;
            //ddlGridPageSize.SelectedValue = "5000";
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);
            DpEndDate.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            BindGrid();
        }

        private void BindGrid()
        {
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            DateTime dt = DateTime.Parse(DpEndDate.SelectedDate.Value.ToString(""));
            string BomDate = dt.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
            string InvDate = dt.AddMonths(0).ToString("yyyyMMdd").Substring(0, 6);
            string FobDate = dt.AddMonths(-1).ToString("yyyyMMdd").Substring(0, 6);
            //var q_sales = from a in DB.Sd_Forcast
            //              join b in DB.Pp_SapModelDests on a.Bc_Item equals b.D_SAP_DEST_Z001

            //              where a.Bc_YM.CompareTo(FY) == 0
            //              where a.Bc_FiscalPeriod.CompareTo(FM) == 0
            //              select new {
            //              };
            //IQueryable<Sd_MrpData> q = DB.Sd_MrpDatas; //.Include(u => u.Dept);
            var q = from a in DB.Sd_Forcasts
                    select new
                    {
                        a.Bc_YM,
                        a.Bc_ForecastItem,
                        a.Bc_ExchangeRate,
                        a.Bc_MovingAverage,
                        a.Bc_PerUnit,
                        a.Bc_Currency,
                        a.Bc_ForecastItemText,
                        a.Bc_ForecastModelName,
                        a.Bc_ForecastRegional,
                        a.Bc_Discontinued,
                        a.Bc_Balancedate,
                    };

            //string sdate = this.DpStartDate.SelectedDate.Value.ToString("yyyyMM");

            //q.Where(u => u.Prodate.Contains(sdate));

            // 在用户名称中搜索

            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
            }
            q = q.Distinct();
            var qs = from a in q.Distinct()
                     join b in DB.Fico_Monthly_Fobs on a.Bc_ForecastItem equals b.Bc_Item
                     where b.Bc_YM.CompareTo(FobDate) == 0
                     select new
                     {
                         a.Bc_ForecastItem,
                         a.Bc_ForecastItemText,
                         a.Bc_ForecastModelName,
                         a.Bc_Discontinued,
                         b.Bc_TcjFob,
                         b.Bc_TcjCurrency,
                         b.Bc_DtaFob,
                         b.Bc_DtaCurrency,
                         a.Bc_MovingAverage,
                         a.Bc_PerUnit,
                         a.Bc_ExchangeRate,
                         DTA_GrossProfit = b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit),
                         DTA_GrossProfitRate = (b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) / b.Bc_DtaFob,
                         TAC_FOB = b.Bc_TcjFob * a.Bc_ExchangeRate,
                         TAC_GrossProfit = b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob,
                         TAC_GrossProfitRate = (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob) / (b.Bc_TcjFob * a.Bc_ExchangeRate),
                         DTA_TAC_GrossProfit = (b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) + (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob),
                         DTA_TAC_GrossProfitRate = ((b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) + (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob)) / (b.Bc_TcjFob * a.Bc_ExchangeRate)
                     };

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

        #endregion Load

        #region Event

        //protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

        //    BindGrid();
        //}
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

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindGrid();
                //getdate();
                //BindGrid();
                //PageContext.RegisterStartupScript("<script>updateChartInTabStrip();</script>");

                //如果没有就如下代码
                // PageContext.RegisterStartupScript("<script language='javascript'>updateChartInTabStrip();</script>");
            }
        }

        private string getdate()
        {
            string strDate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            return strDate;
        }

        #endregion Event

        #region Export

        protected void BtnExport_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = "DTA-TAC_GrossProfit_" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            DateTime dt = DateTime.Parse(DpEndDate.SelectedDate.Value.ToString(""));
            string BomDate = dt.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
            string InvDate = dt.AddMonths(0).ToString("yyyyMMdd").Substring(0, 6);

            //var q_sales = from a in DB.Sd_Forcast
            //              join b in DB.Pp_SapModelDests on a.Bc_Item equals b.D_SAP_DEST_Z001

            //              where a.Bc_YM.CompareTo(FY) == 0
            //              where a.Bc_FiscalPeriod.CompareTo(FM) == 0
            //              select new {
            //              };
            //IQueryable<Sd_MrpData> q = DB.Sd_MrpDatas; //.Include(u => u.Dept);
            var q = from a in DB.Sd_Forcasts
                    select new
                    {
                        a.Bc_YM,
                        a.Bc_ForecastItem,
                        a.Bc_ExchangeRate,
                        a.Bc_MovingAverage,
                        a.Bc_PerUnit,
                        a.Bc_Currency,
                        a.Bc_ForecastItemText,
                        a.Bc_ForecastModelName,
                        a.Bc_ForecastRegional,
                        a.Bc_Discontinued,
                        a.Bc_Balancedate,
                    };

            //string sdate = this.DpStartDate.SelectedDate.Value.ToString("yyyyMM");

            //q.Where(u => u.Prodate.Contains(sdate));

            // 在用户名称中搜索

            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
            }
            q = q.Distinct();
            var qs = from a in q.Distinct()
                     join b in DB.Fico_Monthly_Fobs on a.Bc_ForecastItem equals b.Bc_Item
                     where b.Bc_YM.CompareTo(BomDate) == 0
                     select new
                     {
                         品目 = a.Bc_ForecastItem,
                         品目テキスト = a.Bc_ForecastItemText,
                         代表機種 = a.Bc_ForecastModelName,
                         EOL = a.Bc_Discontinued,
                         TAC_FOBu = b.Bc_TcjFob,
                         通貨T = b.Bc_TcjCurrency,
                         DTA_FOB = b.Bc_DtaFob,
                         通貨D = b.Bc_DtaCurrency,
                         DTA_移動平均原価 = a.Bc_MovingAverage,
                         per = a.Bc_PerUnit,
                         為替レート = a.Bc_ExchangeRate,
                         DTA粗利額 = b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit),
                         DTA粗利率 = (b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) / b.Bc_DtaFob,
                         TAC_FOBc = b.Bc_TcjFob * a.Bc_ExchangeRate,
                         TAC粗利額 = b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob,
                         TAC粗利率 = (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob) / (b.Bc_TcjFob * a.Bc_ExchangeRate),
                         TACDTA粗利額 = (b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) + (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob),
                         TACDTA粗利率 = ((b.Bc_DtaFob - (a.Bc_MovingAverage / a.Bc_PerUnit)) + (b.Bc_TcjFob * a.Bc_ExchangeRate - b.Bc_DtaFob)) / (b.Bc_TcjFob * a.Bc_ExchangeRate)
                     };
            if (qs.Any())
            {
                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName, "DTA-TAC 利润明细");
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        #endregion Export
    }
}