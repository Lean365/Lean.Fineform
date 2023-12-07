using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Lean.Fineform.Lf_Manufacturing.SD.salesmanage
{
    public partial class forecast : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSDChart";
            }
        }

        #endregion

        #region Page_Init

        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    InitGrid();
        //}


        //private void InitGrid()
        //{
        //    string FY;
        //    string thisYM = DateTime.Now.ToString("yyyyMM");

        //    var q_ym = (from a in DB.Fico_Periods
        //               where a.Btfm.CompareTo(thisYM) == 0
        //               select a).ToList();

        //    if(q_ym.Any())
        //    {
        //        FY = q_ym[0].Btfy.ToString();

        //        var q = (from a in DB.Sd_PsiDatas
        //                 where a.isDelete == 0
        //                 //where a.Bc_YM.CompareTo(thisQuarter1) >= 0
        //                 where a.Bc_FY.CompareTo(FY) == 0
        //                 //orderby a.Prostime + "~" + a.Proetime
        //                 select new
        //                 {
        //                     FY = a.Bc_FY,
        //                     YM = a.Bc_YM.Substring(4, 2),
        //                     Item = a.Bc_PsiItem,
        //                     Mrp = a.Bc_PsiMrp,
        //                     Qtya = a.Bc_PsiVera_Qty,
        //                     Qtyb = a.Bc_PsiVerb_Qty,
        //                     DiffQty = a.Bc_PsiDiff_Qty,
        //                 }).ToList();


        //        List<string> DimensionList = new List<string>() { "FY", "Item", "Mrp" };

        //        string DynamicColumn = "YM";
        //        List<string> AllDynamicColumn = null;

        //        DataTable qs = ConvertHelper.IEnumerableConvertToDataTable(q);

        //        DataTable result = ConvertHelper.DataTableRowToCol(qs, DimensionList, DynamicColumn, out AllDynamicColumn);


        //        GridHelper.AddDefColumInGrid(result.Columns, Grid1);
        //    }


        //    //string edate = DPend.SelectedDate.Value.ToString("yyyyMM");

        //    string thisYear = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).ToShortDateString();
        //    string thisQuarter1 = DateTime.Parse(thisYear).AddMonths(3).ToString("yyyyMMdd").Substring(0, 6);
        //    string thisQuarter2 = DateTime.Parse(thisYear).AddMonths(6).ToString("yyyyMMdd").Substring(0, 6);
        //    string thisQuarter3 = DateTime.Parse(thisYear).AddMonths(9).ToString("yyyyMMdd").Substring(0, 6);
        //    string thisQuarter4 = DateTime.Parse(thisYear).AddMonths(12).ToString("yyyyMMdd").Substring(0, 6);
        //    //SQL语句



        //}

        #endregion

        #region Page_Load
        public static string FY;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //BindDDLData();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");



            DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);


            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
            BindDDLItem();

        }



        private void BindGrid()
        {


            string thisYM = DateTime.Now.ToString("yyyyMM");

            string edate = DPend.SelectedDate.Value.ToString("yyyyMM");


            var q_ym = (from a in DB.Fico_Periods
                        where a.Btfm.CompareTo(thisYM) == 0
                        select a).ToList();

            if (q_ym.Any())
            {
                FY = q_ym[0].Btfy.ToString();
                HT_FY.HeaderText = q_ym[0].Btfy.ToString();
                var q = from a in DB.Sd_PsiDatas
                        where a.isDelete == 0
                        //where a.Bc_YM.CompareTo(thisQuarter1) >=0
                        where a.Bc_FY.CompareTo(FY) == 0
                        //orderby a.Prostime + "~" + a.Proetime
                        select new
                        {
                            FY = a.Bc_FY,
                            YM = a.Bc_YM.Substring(4, 2),
                            Item = a.Bc_PsiItem,
                            Mrp = a.Bc_PsiMrp,
                            Qtya = a.Bc_PsiVera_Qty,
                        };



                if (DDLItem.SelectedIndex != 0 && DDLItem.SelectedIndex != -1)
                {
                    string SelectItem = this.DDLItem.SelectedItem.Text;
                    q = q.Where(u => u.Item.ToString().Contains(SelectItem));
                }

                if (q.Count() != 0)
                {
                    List<string> DimensionList = new List<string>() { "FY", "Item", "Mrp" };

                    string DynamicColumn = "YM";
                    List<string> AllDynamicColumn = null;

                    DataTable qs = ConvertHelper.IEnumerableConvertToDataTable(q);

                    DataTable result = ConvertHelper.DataTableRowToCol(qs, DimensionList, DynamicColumn, out AllDynamicColumn);



                    Grid1.RecordCount = result.Rows.Count;
                    if (Grid1.RecordCount != 0)
                    {
                        //var q_Grid = from a in result.AsEnumerable().AsQueryable()
                        //         select a;



                        DataTable table = GridHelper.GetPagedDataTabled(Grid1, result);
                        Grid1.DataSource = table;
                        Grid1.DataBind();

                    }
                }
            }


            string thisYear = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).ToShortDateString();
            string thisQuarter1 = DateTime.Parse(thisYear).AddMonths(3).ToString("yyyyMMdd").Substring(0, 6);
            string thisQuarter2 = DateTime.Parse(thisYear).AddMonths(6).ToString("yyyyMMdd").Substring(0, 6);
            string thisQuarter3 = DateTime.Parse(thisYear).AddMonths(9).ToString("yyyyMMdd").Substring(0, 6);
            string thisQuarter4 = DateTime.Parse(thisYear).AddMonths(12).ToString("yyyyMMdd").Substring(0, 6);


            //SQL语句


        }
        private void BindDDLItem()
        {
            var q_ver = from a in DB.Sd_PsiDatas
                        where a.Bc_FY.CompareTo(FY) == 0
                        select new
                        {
                            a.Bc_PsiItem,

                        };

            //查询LINQ去重复

            var q_Items = q_ver.Select(E => new { E.Bc_PsiItem }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLItem.DataSource = q_Items;
            DDLItem.DataTextField = "Bc_PsiItem";
            DDLItem.DataValueField = "Bc_PsiItem";
            DDLItem.DataBind();
            this.DDLItem.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            
        }
        #endregion

        #region Events


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


        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {

                BindGrid();
            }
        }
        protected void DDLItEm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLItem.SelectedIndex != 0 || DDLItem.SelectedIndex != -1)
            {
                BindGrid();
            }
        }
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
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

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P1d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DPend.SelectedDate.Value.ToString("yyyyMM") + "_PSI";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            string thisYM = DateTime.Now.ToString("yyyyMM");

            string edate = DPend.SelectedDate.Value.ToString("yyyyMM");


            var q_ym = (from a in DB.Fico_Periods
                        where a.Btfm.CompareTo(thisYM) == 0
                        select a).ToList();

            if (q_ym.Any())
            {
                FY = q_ym[0].Btfy.ToString();
                HT_FY.HeaderText = q_ym[0].Btfy.ToString();
                var q = from a in DB.Sd_PsiDatas
                        where a.isDelete == 0
                        //where a.Bc_YM.CompareTo(thisQuarter1) >=0
                        where a.Bc_FY.CompareTo(FY) == 0
                        //orderby a.Prostime + "~" + a.Proetime
                        select new
                        {
                            FY = a.Bc_FY,
                            YM = a.Bc_YM.Substring(4, 2),
                            Item = a.Bc_PsiItem,
                            Mrp = a.Bc_PsiMrp,
                            Qtya = a.Bc_PsiVera_Qty,
                        };



                if (DDLItem.SelectedIndex != 0 && DDLItem.SelectedIndex != -1)
                {
                    string SelectItem = this.DDLItem.SelectedItem.Text;
                    q = q.Where(u => u.Item.ToString().Contains(SelectItem));
                }

                if (q.Count() != 0)
                {
                    List<string> DimensionList = new List<string>() { "FY", "Item", "Mrp" };

                    string DynamicColumn = "YM";
                    List<string> AllDynamicColumn = null;

                    DataTable qs = ConvertHelper.IEnumerableConvertToDataTable(q);

                    DataTable result = ConvertHelper.DataTableRowToCol(qs, DimensionList, DynamicColumn, out AllDynamicColumn);


                        Grid1.AllowPaging = false;
                        ExportHelper.EpplustoXLSXfile(result, Xlsbomitem, ExportFileName);
                        Grid1.AllowPaging = true;


                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }




        }
        #endregion


    }

}