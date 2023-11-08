using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Data.Entity;
using System.Data.Entity.Validation;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Lean.Fineform.Lf_Manufacturing.MM
{
    public partial class Yf_StockOrder : PageBase
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

        #endregion

        #region Page_Load
        //

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //rbtnFirstAuto.Text=global::Resources.GlobalResource.Unenforced;
            //本月第一天
            H_DPstart.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(-15);
            //本月最后一天
            H_DPend.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-12-31")).AddYears(-14);

            //本月第一天
            C_DPstart.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-01-01")).AddYears(-15);
            //本月最后一天
            C_DPend.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy-12-31")).AddYears(-14);
            // 权限检查
            //CheckPowerWithButton("CoreMMView", Btn_H100_Bo);
            //CheckPowerWithButton("CoreMMView", Btn_C100_Bo);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);
            //Btn_H100_Bo.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/Yf_StockOrder.aspx", "进货") + Window1.GetMaximizeReference();
            //Btn_C100_Bo.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/MM/Yf_StockOrder.aspx", "进货") + Window1.GetMaximizeReference();
            //btnP1d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            //BindGridC();
            //BindGridH();
        }



        private void BindGridC()
        {
            //查询LINQ去重复

            try
            {

                string searchText = C_ttbSearchMessage.Text.Trim().ToUpper();

                Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

                //IQueryable<Lean.Fineform.Lf_Business.Models.YF.PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from tg in DBYF.PURTG
                        join th in DBYF.PURTH
                     on new { TG001 = tg.TG001, TG002 = tg.TG002 }
                     equals new { TG001 = th.TH001, TG002 = th.TH002 }

                     //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {
                            
                            TH001 = th.TH001.Replace(" ", "") + "-" + th.TH002.Replace(" ", "") + "-" + th.TH003.Replace(" ", ""),//进货单号
                            tg.TG003,//进货日期
                            tg.TG004,//工厂
                            tg.TG005,//业者
                            tg.TG007,//币种
                            tg.TG008,//汇率
                            tg.TG026,//总验收数量
                            tg.TG017,//进货金额
                            tg.TG028,//原币货款金额
                            tg.TG019,//原币税额
                            tg.TG031,//本币货款金额
                            tg.TG032,//本币税额
                            tg.TG033,//付款条件
                            th.TH004,//品号
                            th.TH005,//品名
                            th.TH007,//进货数量
                            TH011=th.TH011.Replace(" ", "") + "-" + th.TH012.Replace(" ", "") + "-" + th.TH013.Replace(" ", ""),//采购单号
                            th.TH014,//验收日期
                            th.TH015,//验收数量
                            th.TH016,//计价数量
                            th.TH017,//验退数量
                            th.TH018,//原币单位进价
                            th.TH019,//原币进货金额
                            th.TH202,//对帐品号
                            th.TH203,//对帐数量
                            th.TH204,//对帐原币单价
                            th.TH205,//对帐原币金额

                        };


                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = C_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = C_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TG005.Contains(searchText)|| u.TH004.Contains(searchText));
                }

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.TG003.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.TG003.CompareTo(edate) <= 0);
                    }



                var qs = q.Select(tg =>
                new
                {
                    tg.TH001,//进货单号
                    tg.TG003,//进货日期
                    tg.TG004,//工厂
                    tg.TG005,//业者
                    tg.TG007,//币种
                    tg.TG008,//汇率
                    tg.TG026,//总验收数量
                    tg.TG017,//进货金额
                    tg.TG028,//原币货款金额
                    tg.TG019,//原币税额
                    tg.TG031,//本币货款金额
                    tg.TG032,//本币税额
                    tg.TG033,//付款条件
                    tg.TH004,//品号
                    tg.TH005,//品名
                    tg.TH007,//进货数量
                    tg.TH011,//采购单号
                    tg.TH014,//验收日期
                    tg.TH015,//验收数量
                    tg.TH016,//计价数量
                    tg.TH017,//验退数量
                    tg.TH018,//原币单位进价
                    tg.TH019,//原币进货金额
                    tg.TH202,//对帐品号
                    tg.TH203,//对帐数量
                    tg.TH204,//对帐原币单价
                    tg.TH205,//对帐原币金额

                }).Distinct();

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = GridHelper.GetTotalCount(qs);
                if (Grid2.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_Outputsub>(q, Grid1);

                    // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);

                    Grid2.DataSource = table;
                    Grid2.DataBind();


                }
                else
                {
                    Grid2.DataSource = "";
                    Grid2.DataBind();
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
        protected void C_DPstart_TextChanged(object sender, EventArgs e)
        {
            if (C_DPstart.SelectedDate.HasValue)
            {
                C_ttbSearchMessage.Text = "";
                BindGridC();
            }
        }

        protected void C_DPend_TextChanged(object sender, EventArgs e)
        {
            if (C_DPend.SelectedDate.HasValue)
            {
                C_ttbSearchMessage.Text = "";
                BindGridC();
            }
        }
        private void BindGridH()
        {
            //查询LINQ去重复

            try
            {

                string searchText = H_ttbSearchMessage.Text.Trim().ToUpper();

                Lean.Fineform.Lf_Business.Models.YF.Yifei_TACEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_TACEntities();

                //IQueryable<Lean.Fineform.Lf_Business.Models.YF.PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from tg in DBYF.PURTG
                        join th in DBYF.PURTH
                     on new { TG001 = tg.TG001, TG002 = tg.TG002 }
                     equals new { TG001 = th.TH001, TG002 = th.TH002 }

                     //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {

                            TH001 = th.TH001.Replace(" ", "") + "-" + th.TH002.Replace(" ", "") + "-" + th.TH003.Replace(" ", ""),//进货单号
                            tg.TG003,//进货日期
                            tg.TG004,//工厂
                            tg.TG005,//业者
                            tg.TG007,//币种
                            tg.TG008,//汇率
                            tg.TG026,//总验收数量
                            tg.TG017,//进货金额
                            tg.TG028,//原币货款金额
                            tg.TG019,//原币税额
                            tg.TG031,//本币货款金额
                            tg.TG032,//本币税额
                            tg.TG033,//付款条件
                            th.TH004,//品号
                            th.TH005,//品名
                            th.TH007,//进货数量
                            TH011 = th.TH011.Replace(" ", "") + "-" + th.TH012.Replace(" ", "") + "-" + th.TH013.Replace(" ", ""),//采购单号
                            th.TH014,//验收日期
                            th.TH015,//验收数量
                            th.TH016,//计价数量
                            th.TH017,//验退数量
                            th.TH018,//原币单位进价
                            th.TH019,//原币进货金额
                            th.TH202,//对帐品号
                            th.TH203,//对帐数量
                            th.TH204,//对帐原币单价
                            th.TH205,//对帐原币金额

                        };


                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = H_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = H_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TG005.Contains(searchText) || u.TH004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TG003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TG003.CompareTo(edate) <= 0);
                }



                var qs = q.Select(tg =>
                new
                {
                    tg.TH001,//进货单号
                    tg.TG003,//进货日期
                    tg.TG004,//工厂
                    tg.TG005,//业者
                    tg.TG007,//币种
                    tg.TG008,//汇率
                    tg.TG026,//总验收数量
                    tg.TG017,//进货金额
                    tg.TG028,//原币货款金额
                    tg.TG019,//原币税额
                    tg.TG031,//本币货款金额
                    tg.TG032,//本币税额
                    tg.TG033,//付款条件
                    tg.TH004,//品号
                    tg.TH005,//品名
                    tg.TH007,//进货数量
                    tg.TH011,//采购单号
                    tg.TH014,//验收日期
                    tg.TH015,//验收数量
                    tg.TH016,//计价数量
                    tg.TH017,//验退数量
                    tg.TH018,//原币单位进价
                    tg.TH019,//原币进货金额
                    tg.TH202,//对帐品号
                    tg.TH203,//对帐数量
                    tg.TH204,//对帐原币单价
                    tg.TH205,//对帐原币金额

                }).Distinct();


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                if (Grid1.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_Outputsub>(q, Grid1);

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
        protected void H_DPstart_TextChanged(object sender, EventArgs e)
        {
            if (H_DPstart.SelectedDate.HasValue)
            {
                H_ttbSearchMessage.Text = "";
                BindGridH();
            }
        }

        protected void H_DPend_TextChanged(object sender, EventArgs e)
        {
            if (H_DPend.SelectedDate.HasValue)
            {
                H_ttbSearchMessage.Text = "";
                BindGridH();
            }
        }
        #endregion

        #region Events
        #region Grid1
        protected void H_ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            H_ttbSearchMessage.ShowTrigger1 = true;
            BindGridH();
        }

        protected void H_ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            H_ttbSearchMessage.Text = String.Empty;
            H_ttbSearchMessage.ShowTrigger1 = false;
            BindGridH();
        }



        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGridH();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGridH();
        }
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            //labResult.Text = keys[0].ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/dept/view_desc.aspx?Ec_no=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
        }
        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {


        }
        protected void H_ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGridH();
        }
        #endregion
        #region Grid2
        protected void C_ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            C_ttbSearchMessage.ShowTrigger1 = true;
            BindGridC();
        }

        protected void C_ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            C_ttbSearchMessage.Text = String.Empty;
            C_ttbSearchMessage.ShowTrigger1 = false;
            BindGridC();
        }



        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGridC();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGridC();
        }
        protected void Grid2_RowDataBound(object sender, GridRowEventArgs e)
        {

        }
        protected void Grid2_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid2.DataKeys[e.RowIndex];
            //labResult.Text = keys[0].ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/dept/view_desc.aspx?Ec_no=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
        }
        protected void Grid2_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {


        }
        protected void C_ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGridC();
        }
        #endregion

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGridC();
            BindGridH();

        }






        #endregion
        protected void Btn_dta_Click(object sender, EventArgs e)
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
            Xlsbomitem = "C100"  + "_Bo_List_" + C_DPstart.SelectedDate.Value.ToString("yyyyMMdd") + "~" + C_DPend.SelectedDate.Value.ToString("yyyyMMdd");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //查询LINQ去重复

            try
            {

                string searchText = H_ttbSearchMessage.Text.Trim().ToUpper();

                Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

                //IQueryable<Lean.Fineform.Lf_Business.Models.YF.PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from tg in DBYF.PURTG
                        join th in DBYF.PURTH
                     on new { TG001 = tg.TG001, TG002 = tg.TG002 }
                     equals new { TG001 = th.TH001, TG002 = th.TH002 }

                     //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {

                            TH001 = th.TH001.Replace(" ", "") + "-" + th.TH002.Replace(" ", "") + "-" + th.TH003.Replace(" ", ""),//进货单号
                            tg.TG003,//进货日期
                            tg.TG004,//工厂
                            tg.TG005,//业者
                            tg.TG007,//币种
                            tg.TG008,//汇率
                            tg.TG026,//总验收数量
                            tg.TG017,//进货金额
                            tg.TG028,//原币货款金额
                            tg.TG019,//原币税额
                            tg.TG031,//本币货款金额
                            tg.TG032,//本币税额
                            tg.TG033,//付款条件
                            th.TH004,//品号
                            th.TH005,//品名
                            th.TH007,//进货数量
                            TH011 = th.TH011.Replace(" ", "") + "-" + th.TH012.Replace(" ", "") + "-" + th.TH013.Replace(" ", ""),//采购单号
                            th.TH014,//验收日期
                            th.TH015,//验收数量
                            th.TH016,//计价数量
                            th.TH017,//验退数量
                            th.TH018,//原币单位进价
                            th.TH019,//原币进货金额
                            th.TH202,//对帐品号
                            th.TH203,//对帐数量
                            th.TH204,//对帐原币单价
                            th.TH205,//对帐原币金额

                        };


                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = C_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = C_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TG005.Contains(searchText) || u.TH004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TG003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TG003.CompareTo(edate) <= 0);
                }



                var qs = q.Select(tg =>
                new
                {
                    进货单号=tg.TH001,//进货单号
                    进货日期=tg.TG003,//进货日期
                    工厂=tg.TG004,//工厂
                    业者=tg.TG005,//业者
                    币种=tg.TG007,//币种
                    汇率=tg.TG008,//汇率
                    总验收数量=tg.TG026,//总验收数量
                    进货金额=tg.TG017,//进货金额
                    原币货款金额=tg.TG028,//原币货款金额
                    原币税额=tg.TG019,//原币税额
                    本币货款金额=tg.TG031,//本币货款金额
                    本币税额=tg.TG032,//本币税额
                    付款条件=tg.TG033,//付款条件
                    品号=tg.TH004,//品号
                    品名=tg.TH005,//品名
                    进货数量=tg.TH007,//进货数量
                    采购单号=tg.TH011,//采购单号
                    验收日期=tg.TH014,//验收日期
                    验收数量=tg.TH015,//验收数量
                    计价数量=tg.TH016,//计价数量
                    验退数量=tg.TH017,//验退数量
                    原币单位进价=tg.TH018,//原币单位进价
                    原币进货金额=tg.TH019,//原币进货金额
                    对帐品号=tg.TH202,//对帐品号
                    对帐数量=tg.TH203,//对帐数量
                    对帐原币单价=tg.TH204,//对帐原币单价
                    对帐原币金额=tg.TH205,//对帐原币金额

                }).Distinct();
                if (qs.Any())
                {
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
        protected void Btn_tac_Click(object sender, EventArgs e)
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
            Xlsbomitem = "H100"+"_Bo_List_" + H_DPstart.SelectedDate.Value.ToString("yyyyMMdd") + "~" + H_DPend.SelectedDate.Value.ToString("yyyyMMdd");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //查询LINQ去重复

            try
            {

                string searchText = H_ttbSearchMessage.Text.Trim().ToUpper();

                Lean.Fineform.Lf_Business.Models.YF.Yifei_TACEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_TACEntities();

                //IQueryable<Lean.Fineform.Lf_Business.Models.YF.PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from tg in DBYF.PURTG
                        join th in DBYF.PURTH
                     on new { TG001 = tg.TG001, TG002 = tg.TG002 }
                     equals new { TG001 = th.TH001, TG002 = th.TH002 }

                     //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {

                            TH001 = th.TH001.Replace(" ", "") + "-" + th.TH002.Replace(" ", "") + "-" + th.TH003.Replace(" ", ""),//进货单号
                            tg.TG003,//进货日期
                            tg.TG004,//工厂
                            tg.TG005,//业者
                            tg.TG007,//币种
                            tg.TG008,//汇率
                            tg.TG026,//总验收数量
                            tg.TG017,//进货金额
                            tg.TG028,//原币货款金额
                            tg.TG019,//原币税额
                            tg.TG031,//本币货款金额
                            tg.TG032,//本币税额
                            tg.TG033,//付款条件
                            th.TH004,//品号
                            th.TH005,//品名
                            th.TH007,//进货数量
                            TH011 = th.TH011.Replace(" ", "") + "-" + th.TH012.Replace(" ", "") + "-" + th.TH013.Replace(" ", ""),//采购单号
                            th.TH014,//验收日期
                            th.TH015,//验收数量
                            th.TH016,//计价数量
                            th.TH017,//验退数量
                            th.TH018,//原币单位进价
                            th.TH019,//原币进货金额
                            th.TH202,//对帐品号
                            th.TH203,//对帐数量
                            th.TH204,//对帐原币单价
                            th.TH205,//对帐原币金额

                        };


                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = H_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = H_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TG005.Contains(searchText) || u.TH004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TG003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TG003.CompareTo(edate) <= 0);
                }



                var qs = q.Select(tg =>
                new
                {
                    进货单号 = tg.TH001,//进货单号
                    进货日期 = tg.TG003,//进货日期
                    工厂 = tg.TG004,//工厂
                    业者 = tg.TG005,//业者
                    币种 = tg.TG007,//币种
                    汇率 = tg.TG008,//汇率
                    总验收数量 = tg.TG026,//总验收数量
                    进货金额 = tg.TG017,//进货金额
                    原币货款金额 = tg.TG028,//原币货款金额
                    原币税额 = tg.TG019,//原币税额
                    本币货款金额 = tg.TG031,//本币货款金额
                    本币税额 = tg.TG032,//本币税额
                    付款条件 = tg.TG033,//付款条件
                    品号 = tg.TH004,//品号
                    品名 = tg.TH005,//品名
                    进货数量 = tg.TH007,//进货数量
                    采购单号 = tg.TH011,//采购单号
                    验收日期 = tg.TH014,//验收日期
                    验收数量 = tg.TH015,//验收数量
                    计价数量 = tg.TH016,//计价数量
                    验退数量 = tg.TH017,//验退数量
                    原币单位进价 = tg.TH018,//原币单位进价
                    原币进货金额 = tg.TH019,//原币进货金额
                    对帐品号 = tg.TH202,//对帐品号
                    对帐数量 = tg.TH203,//对帐数量
                    对帐原币单价 = tg.TH204,//对帐原币单价
                    对帐原币金额 = tg.TH205,//对帐原币金额

                }).Distinct();
                if (qs.Any())
                {
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
    }
}
