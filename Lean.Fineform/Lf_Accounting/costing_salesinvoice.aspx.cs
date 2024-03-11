using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Fine.Lf_Accounting
{
    public partial class costing_salesinvoice : PageBase
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

        #endregion

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
            DPend.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            BindGrid();

        }

        private void BindGrid()
        {
            try
            {

                if (rbtnFirstAuto.Checked)
                {

                    DateTime dt = DateTime.Parse(DPend.SelectedDate.Value.ToString(""));
                    string BomDate = dt.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
                    string InvDate = dt.AddMonths(0).ToString("yyyyMMdd").Substring(0, 6);

                    var q_BomCost = from a in DB.Fico_Costing_BomCosts
                                    where a.Bc_YM.CompareTo(BomDate) == 0
                                    select a;

                    var q_SalesInv = from b in DB.Fico_Costing_SalesInvoices
                                     where b.Bc_YM.CompareTo(InvDate) == 0
                                     select b;

                    var q_Pivot = from a in q_SalesInv
                                  join b in q_BomCost on a.Bc_SalesItem equals b.Bc_Item
                                  select new
                                  {
                                      a.Bc_SalesItem,
                                      a.Bc_SalesItemText,
                                      a.Bc_ProfitCenter,
                                      a.Bc_SalesQty,
                                      a.Bc_BusinessAmount,
                                      b.Bc_MovingCost,

                                  };

                    var q = from a in q_Pivot
                            group a by new
                            {
                                a.Bc_ProfitCenter,
                                a.Bc_MovingCost,
                            }
                          into g
                            select new
                            {

                                g.Key.Bc_ProfitCenter,


                                Bc_BusinessAmount = g.Sum(p => p.Bc_BusinessAmount),

                                Materialfee = g.Sum(p => p.Bc_SalesQty) * g.Key.Bc_MovingCost,
                                //Materialrate = (g.Sum(p => p.Bc_SalesQty) * g.Key.Bc_MovingCost) / g.Sum(p => p.Bc_BusinessAmount),

                            };

                    var q_count = from a in q
                                  group a by new
                                  {
                                      a.Bc_ProfitCenter
                                  }
                                into g
                                  select new
                                  {
                                      g.Key.Bc_ProfitCenter,
                                      Bc_BusinessAmount = g.Sum(p => p.Bc_BusinessAmount),
                                      Materialfee = g.Sum(p => p.Materialfee),
                                      Materialrate = g.Sum(p =>p.Materialfee)  / g.Sum(p => p.Bc_BusinessAmount),
                                  };



                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q_count);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, q_count);

                        Grid1.DataSource = table;
                        Grid1.DataBind();

                        // 当前页的合计
                        OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q_count));
                    }
                    else
                    {
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }
                }
                if (rbtnSecondAuto.Checked)
                {

                    DateTime dt = DateTime.Parse(DPend.SelectedDate.Value.ToString(""));
                    string BomDate = dt.AddMonths(1).ToString("yyyyMMdd").Substring(0, 6);
                    string InvDate = dt.AddMonths(0).ToString("yyyyMMdd").Substring(0, 6);

                    var q_BomCost = from a in DB.Fico_Costing_BomCosts
                                    where a.Bc_YM.CompareTo(BomDate) == 0
                                    select a;

                    var q_SalesInv = from b in DB.Fico_Costing_SalesInvoices
                                     where b.Bc_YM.CompareTo(InvDate) == 0
                                     select b;

                    var q_Pivot = from a in q_SalesInv
                                  join b in q_BomCost on a.Bc_SalesItem equals b.Bc_Item
                                  select new
                                  {
                                      a.Bc_SalesItem,
                                      a.Bc_SalesItemText,
                                      a.Bc_ProfitCenter,
                                      a.Bc_SalesQty,
                                      a.Bc_BusinessAmount,
                                      b.Bc_MovingCost,

                                  };

                    var q = from a in q_Pivot
                            group a by new
                            {
                                a.Bc_SalesItem,
                                a.Bc_SalesItemText,
                                a.Bc_ProfitCenter,
                                a.Bc_MovingCost,
                            }
                          into g
                            select new
                            {
                                g.Key.Bc_SalesItem,
                                g.Key.Bc_SalesItemText,
                                g.Key.Bc_ProfitCenter,

                                Bc_SalesQty = g.Sum(p => p.Bc_SalesQty),
                                Bc_BusinessAmount = g.Sum(p => p.Bc_BusinessAmount),
                                g.Key.Bc_MovingCost,
                                Materialfee = g.Sum(p => p.Bc_SalesQty) * g.Key.Bc_MovingCost,
                                Materialrate = (g.Sum(p => p.Bc_SalesQty) * g.Key.Bc_MovingCost) / g.Sum(p => p.Bc_BusinessAmount),

                            };

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                        Grid1.DataSource = table;
                        Grid1.DataBind();

                        // 当前页的合计
                        OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));
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
        #endregion

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
        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindGrid();
                //getdate();
                //BindGrid();
                //PageContext.RegisterStartupScript("<script>updateChartInTabStrip();</script>");

                //如果没有就如下代码
                // PageContext.RegisterStartupScript("<script language='javascript'>updateChartInTabStrip();</script>");
            }
        }
        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string checkedValue = String.Empty;
            if (rbtnFirstAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid();
            }

        }
        //合计表格
        private void OutputSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Bc_BusinessAmount"]);
                rTotal += Convert.ToDecimal(row["Materialfee"]);
                ratio = rTotal / pTotal;
            }


            JObject summary = new JObject();
            summary.Add("Bc_SalesQty", "本月材料费比率:");

            summary.Add("Bc_BusinessAmount", pTotal.ToString("F2"));
            summary.Add("Materialfee", rTotal.ToString("F5"));
            summary.Add("Materialrate", ratio.ToString("p2"));

            Grid1.SummaryData = summary;

        }
        #endregion

        #region Export
        protected void BtnExport_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = "MaterialCostRatio_"+DPend.SelectedDate.Value.ToString("yyyyMM") ;
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplustoXLSXfile(Exdt, Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }


    }
        #endregion
    }

}