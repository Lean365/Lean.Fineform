using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.PP.poor.P2D
{
    public partial class p2d_inspection_defect : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DInspDefectView";
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
            //CheckPowerWithButton("CoreDefectDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreP2DInspDefectNew", btnNew);
            //CheckPowerWithButton("CoreDefectNew", btnP2d);
            CheckPowerWithButton("CoreKitOutput", BtnExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/poor/P2D/p2d_inspection_defect_new.aspx", "新增") + Window1.GetMaximizeReference();
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDefect/defect_p2d_new.aspx", "新增");
            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";
            BindDDLLine();
            BindGrid();
        }

        private void BindGrid()
        {
            var LineType = (from a in DB.Pp_Lines
                            where a.lineclass.Contains("P")
                            select new
                            {
                                a.linename
                            }).ToList();
            IQueryable<Pp_P2d_Inspection_Defect> q = DB.Pp_P2d_Inspection_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Proinspdate.Contains(searchText) || u.Propcbtype.Contains(searchText) || u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            // 在用户名称中搜索

            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Proinspdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Proinspdate.CompareTo(edate) <= 0);
            }

            q = q.Where(u => u.isDeleted == 0);

            //查询包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.linename)).AsQueryable();

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q_include.Count();

            // 排列和数据库分页
            q = SortAndPage<Pp_P2d_Inspection_Defect>(q_include, Grid1);

            Grid1.DataSource = q_include;
            Grid1.DataBind();
            //ttbSearchMessage.Text = "";
            ConvertHelper.LinqConvertToDataTable(q_include);
            // 当前页的合计
            OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q_include));
        }

        public void BindDDLLine()
        {
            var LineType = (from a in DB.Pp_Lines
                            where a.lineclass.Contains("P")
                            select new
                            {
                                a.linename
                            }).ToList();
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_P1d_Defects
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                    where a.Prodate.CompareTo(sdate) >= 0
                    where a.Prodate.CompareTo(edate) <= 0
                    select new
                    {
                        a.Prolinename
                    };

            //包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.linename));

            var qs = q_include.Select(E => new { E.Prolinename, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLline.DataSource = qs;
            DDLline.DataTextField = "Prolinename";
            DDLline.DataValueField = "Prolinename";
            DDLline.DataBind();

            this.DDLline.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion Page_Load

        #region Events

        protected void DDLline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLline.SelectedIndex != -1 && DDLline.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

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
            // CheckPowerWithWindowField("CoreDefectEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreP2DInspDefectDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            CheckPowerWithLinkButtonField("CoreP2DInspDefectEdit", Grid1, "editField");
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/poor/P2D/p2d_inspection_defect_edit.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }

            int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP2DInspDefectDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P2d_Inspection_Defect current = DB.Pp_P2d_Inspection_Defects.Find(del_ID);
                string Contectext = current.ID.ToString() + "," + current.GUID.ToString();
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del生产* " + Contectext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产不良删除标记", OperateNotes);

                DB.Pp_P2d_Inspection_Defects.Where(l => l.ID == del_ID).DeleteFromQuery();

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

        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindGrid();
            }
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
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            Xlsbomitem = DPstart.SelectedDate.Value.ToString("yyyyMM") + "Inspection_defect_Data";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            IQueryable<Pp_P2d_Inspection_Defect> q = DB.Pp_P2d_Inspection_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Proinspdate.Contains(searchText) || u.Propcbtype.Contains(searchText) || u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            // 在用户名称中搜索

            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Proinspdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Proinspdate.CompareTo(edate) <= 0);
            }

            q = q.Where(u => u.isDeleted == 0);
            if (q.Any())
            {
                var qs = from p in q
                         .OrderBy(s => s.Proinspdate)
                         select new
                         {
                             检查日期 = p.Proinspdate,
                             机种 = p.Promodel,
                             板别 = p.Propcbtype,
                             目视 = p.Provisualtype,
                             VC = p.Provctype,
                             A面 = p.Prosideadate,
                             B面 = p.Prosidebdate,
                             班别 = p.Prodshiftname,
                             检查员 = p.Procensor,
                             订单 = p.Proorder,
                             批次 = p.Prolot,
                             数量 = p.Proorderqty,
                             生产数 = p.Prorealqty,
                             检查数 = p.Proispqty,
                             检查状态 = p.Propcbchecktype,
                             线别 = p.Prolinename,
                             检查工数 = p.Proinsqtime,
                             AOI工数 = p.Proaoitime,
                             不良数量 = p.Probadqty,
                             手贴 = p.Prohandle,
                             流水 = p.Probadserial,
                             内容 = p.Probadcontent,
                             个所 = p.Probadtype,
                             不良率 = (p.Proispqty != 0 ? p.Prorealqty / p.Proispqty : 0)
                         };

                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                //Grid1.AllowPaging = false;
                //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                //Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        #endregion ExportExcel

        //合计表格
        private void OutputSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += 0;// Convert.ToDecimal(row["Prorealqty"]);
                rTotal += Convert.ToDecimal(row["Probadqty"]);
                ratio = 0;// rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prorealqty", pTotal.ToString("F2"));
            summary.Add("Probadqty", rTotal.ToString("F2"));
            summary.Add("Probadtotal", ratio.ToString("p0"));

            Grid1.SummaryData = summary;
        }
    }
}