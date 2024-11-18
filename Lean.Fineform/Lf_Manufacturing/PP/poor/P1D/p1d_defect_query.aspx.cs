using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p1d_defect_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DDefectView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string tracestr, Prefix_XlsxName, Export_FileName;

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
            CheckPowerWithButton("CoreFineExport", BtnExport);

            CheckPowerWithButton("CoreFineExport", BtnModel);
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            BindDdlLot();
            // 每页记录数
            //Grid1.PageSize = 1000;
            //ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";

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

            IQueryable<Pp_P1d_Defect> q = DB.Pp_P1d_Defects; //.Include(u => u.Dept);

            // 在下拉列表中搜索

            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            else
            {
                string DdlStringText = DDLlot.SelectedItem.Text;
                if (DDLlot.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolot.Contains(DdlStringText));
                }

                // 在用户名称中搜索
            }

            q = q.Where(u => u.IsDeleted == 0);
            var qs = from a in q
                     select a;

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
            q = SortAndPage<Pp_P1d_Defect>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
        }

        public void BindDdlLot()
        {
            var q = from a in DB.Pp_P1d_Defects
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                    select new
                    {
                        a.Prolot
                    };

            var qs = q.Select(E => new { E.Prolot, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLlot.DataSource = qs;
            DDLlot.DataTextField = "Prolot";
            DDLlot.DataValueField = "Prolot";
            DDLlot.DataBind();

            this.DDLlot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreOphDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
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
        //    InsNetOperateNotes();
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
            //if (e.CommandName == "PrintDefect")
            //{
            //    //ID,Prodate,Prolot
            //    object[] keys = Grid1.DataKeys[e.RowIndex];

            //    if (DDLmodel.SelectedIndex != -1 && DDLmodel.SelectedIndex != 0)
            //    {
            //        tracestr = DDLmodel.SelectedItem.Text;

            //        //labResult.Text = keys[0].ToString();
            //        PageContext.RegisterStartupScript(Window1.GetShowReference("~/onePrint/defect_report.aspx?Prolot=" + tracestr + "&type=1") + Window1.GetMaximizeReference());

            //    }
            //    else
            //    {
            //        Alert.ShowInTop("请选择lot！");
            //        return;
            //    }
            //}
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void DDLlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLlot.SelectedIndex != -1)
            {
                ttbSearchMessage.Text = String.Empty;
                BindGrid();
            }
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

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Prorealqty"]);
                rTotal += Convert.ToDecimal(row["Probadqty"]);
                ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prorealqty", pTotal.ToString("F2"));
            summary.Add("Probadqty", rTotal.ToString("F2"));
            summary.Add("Probadtotal", ratio.ToString("p0"));

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

            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                Prefix_XlsxName = searchText + "_DefectDetail";
                Export_FileName = Prefix_XlsxName + ".xlsx";
            }
            else
            {
                string DdlStringText = DDLlot.SelectedItem.Text;
                if (DDLlot.SelectedIndex != -1)
                {
                    Prefix_XlsxName = DdlStringText + "_DefectDetail";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                }

                // 在用户名称中搜索
            }

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";

            //IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

            //// 在用户名称中搜索

            //if (DDLmodel.SelectedIndex != 0 && DDLmodel.SelectedIndex != -1)
            //{
            //    q = q.Where(u => u.Prolot.ToString().Contains(DDLmodel.SelectedItem.Text));
            //}
            //q = q.Where(u => u.Prongbdel == false);

            //var qs = from g in q
            //         select new
            //         {
            //             生产日期 = g.Prodate,
            //             生产批次 = g.Prolot,
            //             生产班组 = g.Prolinename,
            //             不良区分 = g.Prongdept,
            //             不良件数 = g.Probadqty,
            //             不请症状 = g.Probadnote,
            //             不良原因 = g.Probadreason,

            //         };
            if (Grid1.RecordCount != 0)
            {
                //ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ExportHelper.GetGridDataTable(Grid1), Prefix_XlsxName, Export_FileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnModel_Click(object sender, EventArgs e)
        {
            GridtoExcel();
        }

        private void GridtoExcel()
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                Prefix_XlsxName = searchText + "_DefectDetail";
                Export_FileName = Prefix_XlsxName.ToUpper() + ".xlsx";
            }
            else
            {
                string DdlStringText = DDLlot.SelectedItem.Text;
                if (DDLlot.SelectedIndex != -1)
                {
                    Prefix_XlsxName = DdlStringText + "_DefectDetail";
                    Export_FileName = Prefix_XlsxName.ToUpper() + ".xlsx";
                }

                // 在用户名称中搜索
            }
            List<DataTable> tables = new List<DataTable>();
            DataTable ma = new DataTable();

            var q =
            (from p in DB.Pp_P1d_Defects
                 //where p.Prorealqty==p.Prolotqty
                 //where p.Prodate.Substring(10, 6).CompareTo(strDpdate) <= 0
             select p);

            //qs.Count();

            //IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

            // 在用户名称中搜索

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            else
            {
                string DdlStringText = DDLlot.SelectedItem.Text;
                if (DDLlot.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolot.Contains(DdlStringText));
                }

                // 在用户名称中搜索
            }
            //else
            //{
            //    //当前日期
            //    string dd = DateTime.Now.ToString("yyyyMMdd");
            //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            //}

            //string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");

            var qs = q.Select(E => new
            {
                E.Prolot,
                E.Promodel,
            }).Distinct().AsQueryable();

            ma = ConvertHelper.LinqConvertToDataTable(qs);
            if (q.Any())
            {
                for (int i = 0; i < ma.Rows.Count; i++)
                {
                    string strLot = "";
                    strLot = ma.Rows[i][0].ToString();
                    string strPmodel = "";
                    strPmodel = ma.Rows[i][1].ToString();
                    var q2 =
                            from p in DB.Pp_P1d_Defects
                             .Where(s => s.IsDeleted == 0)
                              .Where(s => s.Prolot.CompareTo(strLot) == 0)
                            .Where(s => s.Promodel.CompareTo(strPmodel) == 0)
                            //.Where(s => s.Prorealqty==s.Proorderqty)
                            .OrderBy(s => s.Prongdept)
                            select new
                            {
                                p.Prolot,
                                p.Promodel,
                                p.Prolinename,
                                p.Prodate,
                                p.Prorealqty,
                                p.Prongdept,
                                p.Probadnote,
                                p.Probadset,
                                p.Probadreason,
                                p.Probadqty,
                            };

                    //IEnumerable 转换IQueryable//AsEnumerable//AsQueryable
                    var qsub = from p in q2
                             .OrderBy(s => s.Prongdept)
                               select new
                               {
                                   批次 = p.Prolot,
                                   机种 = p.Promodel,
                                   生产日期 = p.Prodate,
                                   生产班组 = p.Prolinename,
                                   生产实绩 = p.Prorealqty,
                                   不良区分 = p.Prongdept,
                                   不良症状 = p.Probadnote,
                                   个所 = p.Probadset,
                                   不良原因 = p.Probadreason,
                                   不良件数 = p.Probadqty,
                               };
                    DataTable ex = ConvertHelper.LinqConvertToDataTable(qsub);

                    ex.TableName = strPmodel.Replace("/", "-") + "_" + i;

                    tables.Add(ex);
                }
                ExportHelper.ExportMultipleSheets(tables, ma, Export_FileName);
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}