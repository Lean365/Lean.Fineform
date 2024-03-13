using FineUIPro;
using System;
using System.Data;
using System.Linq;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_PurchaseOrder : PageBase
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
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreEcnDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

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

                Lf_Business.Models.YF.Yifei_DTA_Entities DBYF = new Lf_Business.Models.YF.Yifei_DTA_Entities();

                //IQueryable<PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from ta in DBYF.PURTC
                        join tb in DBYF.PURTD
                     on new { TC001 = ta.TC001, TC002 = ta.TC002 }
                     equals new { TC001 = tb.TD001, TC002 = tb.TD002 }
                        join mv in DBYF.CMSMV on ta.TC011 equals mv.MV001
                        //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {
                            ta.TC001,//单号
                            ta.TC002,//单号
                            tb.TD003,//单号
                            ta.TC003,//日期
                            ta.TC004,//业者
                            ta.TC005,//币种
                            TC011 = mv.MV002,//采购员
                            tb.TD004,//物料
                            tb.TD005,//描述
                            tb.TD008,//数量
                            tb.TD010,//单价
                            tb.TD011,//金额
                        };

                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = C_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = C_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TD004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TC003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TC003.CompareTo(edate) <= 0);
                }

                var qs = q.Select(ta =>
                new
                {
                    TC001 = ta.TC001.Replace(" ", "") + "-" + ta.TC002.Replace(" ", "") + "-" + ta.TD003.Replace(" ", ""),//单号

                    ta.TC003,//日期
                    ta.TC004,//业者
                    ta.TC005,//币种
                    ta.TC011,//采购员
                    ta.TD004,//物料
                    ta.TD005,//描述
                    ta.TD008,//数量
                    ta.TD010,//单价
                    ta.TD011,//金额
                }).Distinct();

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = GridHelper.GetTotalCount(qs);
                if (Grid2.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

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

                Lf_Business.Models.YF.Yifei_TAC_Entities DBYF = new Lf_Business.Models.YF.Yifei_TAC_Entities();

                //IQueryable<PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from ta in DBYF.PURTC
                        join tb in DBYF.PURTD
                     on new { TC001 = ta.TC001, TC002 = ta.TC002 }
                     equals new { TC001 = tb.TD001, TC002 = tb.TD002 }
                        join mv in DBYF.CMSMV on ta.TC011 equals mv.MV001
                        //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {
                            ta.TC001,//单号
                            ta.TC002,//单号
                            tb.TD003,//单号
                            ta.TC003,//日期
                            ta.TC004,//业者
                            ta.TC005,//币种
                            TC011 = mv.MV002,//采购员
                            tb.TD004,//物料
                            tb.TD005,//描述
                            tb.TD008,//数量
                            tb.TD010,//单价
                            tb.TD011,//金额
                        };

                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = H_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = H_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TD004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TC003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TC003.CompareTo(edate) <= 0);
                }

                var qs = q.Select(ta =>
                new
                {
                    TC001 = ta.TC001.Replace(" ", "") + "-" + ta.TC002.Replace(" ", "") + "-" + ta.TD003.Replace(" ", ""),//单号

                    ta.TC003,//日期
                    ta.TC004,//业者
                    ta.TC005,//币种
                    ta.TC011,//采购员
                    ta.TD004,//物料
                    ta.TD005,//描述
                    ta.TD008,//数量
                    ta.TD010,//单价
                    ta.TD011,//金额
                }).Distinct();

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

        #endregion Page_Load

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

        #endregion Grid1

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

        #endregion Grid2

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGridC();
            BindGridH();
        }

        #endregion Events

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
            Xlsbomitem = "H100" + "_Po_List_" + C_DPstart.SelectedDate.Value.ToString("yyyyMMdd") + "~" + C_DPend.SelectedDate.Value.ToString("yyyyMMdd");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //查询LINQ去重复

            try
            {
                string searchText = H_ttbSearchMessage.Text.Trim().ToUpper();

                Lf_Business.Models.YF.Yifei_DTA_Entities DBYF = new Lf_Business.Models.YF.Yifei_DTA_Entities();

                //IQueryable<PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from ta in DBYF.PURTC
                        join tb in DBYF.PURTD
                     on new { TC001 = ta.TC001, TC002 = ta.TC002 }
                     equals new { TC001 = tb.TD001, TC002 = tb.TD002 }
                        join mv in DBYF.CMSMV on ta.TC011 equals mv.MV001
                        //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {
                            ta.TC001,//单号
                            ta.TC002,//单号
                            tb.TD003,//单号
                            ta.TC003,//日期
                            ta.TC004,//业者
                            ta.TC005,//币种
                            TC011 = mv.MV002,//采购员
                            tb.TD004,//物料
                            tb.TD005,//描述
                            tb.TD008,//数量
                            tb.TD010,//单价
                            tb.TD011,//金额
                        };

                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = C_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = C_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TD004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TC003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TC003.CompareTo(edate) <= 0);
                }

                var qs = q.Select(ta =>
                new
                {
                    TC001 = ta.TC001.Replace(" ", "") + "-" + ta.TC002.Replace(" ", "") + "-" + ta.TD003.Replace(" ", ""),//单号

                    日期 = ta.TC003,//日期
                    业者 = ta.TC004,//业者
                    币种 = ta.TC005,//币种
                    采购员 = ta.TC011,//采购员
                    物料 = ta.TD004,//物料
                    描述 = ta.TD005,//描述
                    数量 = ta.TD008,//数量
                    单价 = ta.TD010,//单价
                    金额 = ta.TD011,//金额
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
            Xlsbomitem = "H100" + "_Po_List_" + H_DPstart.SelectedDate.Value.ToString("yyyyMMdd") + "~" + H_DPend.SelectedDate.Value.ToString("yyyyMMdd");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //查询LINQ去重复

            try
            {
                string searchText = H_ttbSearchMessage.Text.Trim().ToUpper();

                Lf_Business.Models.YF.Yifei_TAC_Entities DBYF = new Lf_Business.Models.YF.Yifei_TAC_Entities();

                //IQueryable<PURTA> q = DBYF.PURTA; //.Include(u => u.Dept);

                var q = from ta in DBYF.PURTC
                        join tb in DBYF.PURTD
                     on new { TC001 = ta.TC001, TC002 = ta.TC002 }
                     equals new { TC001 = tb.TD001, TC002 = tb.TD002 }
                        join mv in DBYF.CMSMV on ta.TC011 equals mv.MV001
                        //   join me in DBYF.CMSME on ta.TA004 equals me.ME001
                        select new

                        {
                            ta.TC001,//单号
                            ta.TC002,//单号
                            tb.TD003,//单号
                            ta.TC003,//日期
                            ta.TC004,//业者
                            ta.TC005,//币种
                            TC011 = mv.MV002,//采购员
                            tb.TD004,//物料
                            tb.TD005,//描述
                            tb.TD008,//数量
                            tb.TD010,//单价
                            tb.TD011,//金额
                        };

                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = H_DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = H_DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TD004.Contains(searchText));
                }

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.TC003.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.TC003.CompareTo(edate) <= 0);
                }

                var qs = q.Select(ta =>
                new
                {
                    TC001 = ta.TC001.Replace(" ", "") + "-" + ta.TC002.Replace(" ", "") + "-" + ta.TD003.Replace(" ", ""),//单号

                    日期 = ta.TC003,//日期
                    业者 = ta.TC004,//业者
                    币种 = ta.TC005,//币种
                    采购员 = ta.TC011,//采购员
                    物料 = ta.TD004,//物料
                    描述 = ta.TD005,//描述
                    数量 = ta.TD008,//数量
                    单价 = ta.TD010,//单价
                    金额 = ta.TD011,//金额
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