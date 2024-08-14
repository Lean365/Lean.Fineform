using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string tracestr;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //BindDdlData();
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

            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlLline();
            BindGrid();
        }

        private void BindGrid()
        {
            //var qs =
            //    from p in DB.proDefects
            //    where p.Prongbdel == false
            //    where p.Prorealqty != 0
            //    where p.Probadtotal != 0
            //    group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Prongdept, p.Proclassmatter, p.Prongmatter, p.Prorealqty, p.Probadtotal } into g
            //    select new
            //    {
            //        Prodate = g.Key.Prodate,
            //        Prolinename = g.Key.Prolinename,
            //        Prolot = g.Key.Prolot,
            //        Prongdept = g.Key.Prongdept,
            //        Proclassmatter = g.Key.Proclassmatter,
            //        Prongmatter = g.Key.Prongmatter,
            //        Prorealqty = g.Key.Prorealqty,
            //        Probadtotal = g.Key.Probadtotal,
            //        Rate = g.Key.Probadtotal / g.Key.Prorealqty
            //    };

            //var qss =
            //    from p in DB.proOutputsubs
            //    group p by new
            //    {
            //        p.Prolinename,
            //        p.Prodate,
            //        p.Prodirect,
            //        p.Prolot,
            //        p.Prohbn,
            //        p.Promodel,
            //        p.Prorealqty,
            //        p.Prorealtime,
            //    }
            //    into g
            //    select new
            //    {
            //        g.Key,
            //        Qty = g.Sum(p => p.Prorealqty),
            //        Min = g.Sum(p => p.Prorealtime),
            //    };

            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            // 在用户名称中搜索

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmLine.ToString().Contains(DdlLine.SelectedItem.Text));
            }
            if (DDLlot.SelectedIndex != 0 && DDLlot.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmProlot.ToString().Contains(DDLlot.SelectedItem.Text));
            }
            if (DDLhbn.SelectedIndex != 0 && DDLhbn.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmMaterial.ToString().Contains(DDLhbn.SelectedItem.Text));
            }

            q = q.Where(u => u.IsDeleted == 0);
            // q = q.Where(u => u.Promodel != "0");
            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}

            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Qm_Outgoing>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
        }

        #endregion Page_Load

        #region Events

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreOphDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
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

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreOphDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);
        //    NetLogRecord();
        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proOutputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.proOutputs.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "PrintCheck")
            {
                //ID,Prodate,Prolot
                object[] keys = Grid1.DataKeys[e.RowIndex];

                if (DDLlot.SelectedIndex != -1 && DDLlot.SelectedIndex != 0)
                {
                    tracestr = DDLlot.SelectedItem.Text;

                    //labResult.Text = keys[0].ToString();
                    PageContext.RegisterStartupScript(Window1.GetShowReference("~/onePrint/defect_report.aspx?Prolot=" + tracestr + "&type=1") + Window1.GetMaximizeReference());
                }
                else
                {
                    Alert.ShowInTop("请选择lot！");
                    return;
                }
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events

        public void BindDdlLline()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            //string slot = DDLlot.SelectedItem.Text;
            //查询LINQ去重复
            var q = from a in DB.Qm_Outgoings
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    //where a.qmProlot.Contains(slot)

                    select new
                    {
                        a.qmLine
                    };

            var qs = q.Select(E => new { E.qmLine }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DdlLine.DataSource = qs;
            DdlLine.DataTextField = "qmLine";
            DdlLine.DataValueField = "qmLine";
            DdlLine.DataBind();

            this.DdlLine.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        public void BindDdlLot()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            string sline = DdlLine.SelectedItem.Text;
            //查询LINQ去重复
            var q = from a in DB.Qm_Outgoings
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    where a.qmLine.Contains(sline)

                    select new
                    {
                        a.qmProlot
                    };

            var qs = q.Select(E => new { E.qmProlot }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLlot.DataSource = qs;
            DDLlot.DataTextField = "qmProlot";
            DDLlot.DataValueField = "qmProlot";
            DDLlot.DataBind();

            this.DDLlot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        public void BindDdlhbn()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            string sline = DdlLine.SelectedItem.Text;
            string slot = DDLlot.SelectedItem.Text;
            //查询LINQ去重复
            var q = from a in DB.Qm_Outgoings
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    where a.qmLine.Contains(sline)
                    where a.qmProlot.Contains(slot)

                    select new
                    {
                        a.qmMaterial
                    };

            var qs = q.Select(E => new { E.qmMaterial }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLhbn.DataSource = qs;
            DDLhbn.DataTextField = "qmMaterial";
            DDLhbn.DataValueField = "qmMaterial";
            DDLhbn.DataBind();

            this.DDLhbn.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlLine.SelectedIndex != -1 && DdlLine.SelectedIndex != 0)
            {
                BindDdlLot();

                BindGrid();
            }
        }

        protected void DDLlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLlot.SelectedIndex != -1 && DDLlot.SelectedIndex != 0)
            {
                BindDdlhbn();
                BindGrid();
            }
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DDLhbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["qmAcceptqty"]);
                rTotal += Convert.ToDecimal(row["qmRejectqty"]);
                if (pTotal != 0)
                {
                    ratio = rTotal / pTotal;
                }
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("qmAcceptqty", pTotal.ToString("F2"));
            summary.Add("qmRejectqty", rTotal.ToString("F2"));
            summary.Add("Rate", ratio.ToString("p2"));

            Grid1.SummaryData = summary;
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_Outgoing_Inspection_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            // 在用户名称中搜索

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmLine.ToString().Contains(DdlLine.SelectedItem.Text));
            }
            if (DDLlot.SelectedIndex != 0 && DDLlot.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmProlot.ToString().Contains(DDLlot.SelectedItem.Text));
            }
            if (DDLhbn.SelectedIndex != 0 && DDLhbn.SelectedIndex != -1)
            {
                q = q.Where(u => u.qmMaterial.ToString().Contains(DDLhbn.SelectedItem.Text));
            }

            q = q.Where(u => u.IsDeleted == 0);

            var qs = from g in q
                     select new
                     {
                         批次 = g.qmProlot,
                         班组 = g.qmLine,
                         日期 = g.qmCheckdate,
                         验收数 = g.qmAcceptqty,
                         物料 = g.qmMaterial,
                         验退数 = g.qmRejectqty,
                         查验号码 = g.qmCheckNotes,
                         检查员 = g.qmInspector,
                     };
            if (qs.Any())
            {
                ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else
            {
                Alert.ShowInTop("没有数据被导出！", "警告提示", MessageBoxIcon.Warning);
            }
        }
    }
}