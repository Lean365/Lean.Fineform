using System;
using System.Data;
using System.Linq;
using System.Web;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_report : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string mysql, myrexname;
        public static DataTable table;
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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreEcnDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CorePlutoExport2003", Btn2003);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");
            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                var q =
                        (from a in DB.Pp_Ecs
                         join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                         where b.Ec_qadate == "" && b.Ec_qadate == null
                         // where a.Ec_qadate==null
                         //where b.Ec_distinction == 1
                         where b.isDeleted == 0
                         orderby a.Ec_issuedate, b.Ec_no, b.Ec_model, b.Ec_bomitem
                         //where a.Ec_model.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_no.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_issuedate.Contains(searchText)
                         select new
                         {
                             Ec_issuedate = a.Ec_issuedate,
                             Ec_no = b.Ec_no,
                             Ec_status = a.Ec_status,
                             Ec_details = a.Ec_details,
                             Ec_leader = a.Ec_leader,
                             Ec_lossamount = a.Ec_lossamount,
                             Ec_distinction = a.Ec_distinction,
                             Ec_letterno = a.Ec_letterno,
                             Ec_letterdoc = a.Ec_letterdoc,
                             Ec_eppletterno = a.Ec_eppletterno,
                             Ec_eppletterdoc = a.Ec_eppletterdoc,
                             Ec_documents = a.Ec_documents,
                             Ec_teppletterno = a.Ec_teppletterno,
                             Ec_teppletterdoc = a.Ec_teppletterdoc,
                             Ec_model = b.Ec_model,
                             Ec_bomitem = b.Ec_bomitem,
                             Ec_bomsubitem = b.Ec_bomsubitem,
                             Ec_olditem = b.Ec_olditem,
                             Ec_oldtext = b.Ec_oldtext,
                             Ec_oldqty = b.Ec_oldqty,
                             Ec_oldset = b.Ec_oldset,
                             Ec_newitem = b.Ec_newitem,
                             Ec_newtext = b.Ec_newtext,
                             Ec_newqty = b.Ec_newqty,
                             Ec_newset = b.Ec_newset,
                             Ec_bomno = b.Ec_bomno,
                             Ec_change = b.Ec_change,
                             Ec_local = b.Ec_local,
                             Ec_note = b.Ec_note,
                             Ec_process = b.Ec_process,
                             Ec_bomdate = b.Ec_bomdate,
                             Ec_pmcdate = b.Ec_pmcdate,

                             Ec_pmclot = b.Ec_pmclot,

                             Ec_pmcsn = b.Ec_pmcmemo,
                             Ec_pmcnote = b.Ec_pmcnote,
                             Ec_bstock = b.Ec_bstock,
                             Ec_mmdate = b.Ec_mmdate,
                             Ec_mmlot = b.Ec_mmlot,
                             Ec_mmlotno = b.Ec_mmlotno,
                             Ec_mmnote = b.Ec_mmnote,
                             Ec_purdate = b.Ec_purdate,
                             Ec_purorder = b.Ec_purorder,
                             Ec_pursupplier = b.Ec_pursupplier,
                             Ec_purnote = b.Ec_purnote,
                             Ec_iqcdate = b.Ec_iqcdate,

                             Ec_iqcorder = b.Ec_iqcorder,
                             Ec_iqcnote = b.Ec_iqcnote,
                             Ec_p1ddate = b.Ec_p1ddate,
                             Ec_p1dline = b.Ec_p1dline,
                             Ec_p1dlot = b.Ec_p1dlot,

                             Ec_p2ddate = b.Ec_p2ddate,

                             Ec_p2dlot = b.Ec_p2dlot,

                             Ec_p2dnote = b.Ec_p2dnote,
                             Ec_qadate = b.Ec_qadate,
                             Ec_qalot = b.Ec_qalot,

                             Ec_qanote = b.Ec_qanote,
                             Ec_subdel = b.isDeleted,

                             //Processstatus = (a.Ec_issuedate == "" || a.Ec_issuedate == null ? "◎采购未处理" :
                             //(a.Ec_pmcdate == "" || a.Ec_pmcdate == null ? "◎生管未处理" :
                             //(a.Ec_iqcdate == "" || a.Ec_iqcdate == null ? "◎受检未处理" :
                             //(a.Ec_mmdate == "" || a.Ec_mmdate == null ? "◎部管未处理" :
                             //(a.Ec_p2ddate == "" || a.Ec_p2ddate == null ? "◎制二未处理" :
                             //(a.Ec_p1ddate == "" || a.Ec_p1ddate == null ? "◎制一未处理" :
                             //(a.Ec_qadate == "" || a.Ec_qadate == null ? "◎品管未处理" :
                             //""))))))),

                             Prostatus =
                                        (a.Ec_distinction == 1 ? (b.Ec_qadate == "" || b.Ec_qadate == null ? "◎" : "●") :
                                        (a.Ec_distinction == 2 ? (b.Ec_qadate == "" || b.Ec_qadate == null ? "◎" : "●") :
                                        (a.Ec_distinction == 3 ? (b.Ec_mmdate == "" || b.Ec_mmdate == null ? "◎" : "●") :
                                        (a.Ec_distinction == 4 ? (b.Ec_qadate == "" || b.Ec_qadate == null ? "◎" : "●") :
                                        (a.Ec_distinction == 5 ? (b.Ec_qadate == "" || b.Ec_qadate == null ? "◎" : "●") :
                                        ""))))),
                         });
                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                }
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_status.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
                }
                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //var qs = q
                //    .Where(p => p.Ec_distinction == 3 ? p.Ec_mmdate == "" : p.Ec_qadate == "");

                var qss = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_no,
                    E.Ec_status,
                    E.Ec_details,
                    E.Ec_leader,
                    E.Ec_lossamount,
                    E.Ec_distinction,
                    E.Ec_letterno,
                    E.Ec_letterdoc,
                    E.Ec_eppletterno,
                    E.Ec_eppletterdoc,
                    E.Ec_documents,
                    E.Ec_teppletterno,
                    E.Ec_teppletterdoc,
                    E.Ec_model,
                    E.Ec_bomitem,
                    E.Ec_bomsubitem,
                    E.Ec_olditem,
                    E.Ec_oldtext,
                    E.Ec_oldqty,
                    E.Ec_oldset,
                    E.Ec_newitem,
                    E.Ec_newtext,
                    E.Ec_newqty,
                    E.Ec_newset,
                    E.Ec_bomno,
                    E.Ec_change,
                    E.Ec_local,
                    E.Ec_note,
                    E.Ec_process,
                    E.Ec_bomdate,
                    E.Ec_pmcdate,

                    E.Ec_pmclot,

                    E.Ec_pmcsn,
                    E.Ec_pmcnote,
                    E.Ec_bstock,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_mmlotno,
                    E.Ec_mmnote,
                    E.Ec_purdate,
                    E.Ec_purorder,
                    E.Ec_pursupplier,
                    E.Ec_purnote,
                    E.Ec_iqcdate,

                    E.Ec_iqcorder,
                    E.Ec_iqcnote,
                    E.Ec_p1ddate,
                    E.Ec_p1dline,
                    E.Ec_p1dlot,

                    E.Ec_p2ddate,

                    E.Ec_p2dlot,

                    E.Ec_p2dnote,
                    E.Ec_qadate,
                    E.Ec_qalot,

                    E.Ec_qanote,
                    E.Ec_subdel,
                }).Distinct();
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(qss);
                if (Grid1.RecordCount > 0)
                {
                    // 排列和数据库分页
                    table = GridHelper.GetPagedDataTable(Grid1, qss);
                }
                // 3.绑定到Grid
                Grid1.DataSource = table;
                Grid1.DataBind();
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

        #region ExportExcel

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            Xlsbomitem = "Ec__DetailsData_" + myrexname;

            ExportFileName = Xlsbomitem + ".xlsx";

            //DataTable source = GetDataTable.Getdt(mysql);
            //导出2007格式
            ExportHelper.EpplustoXLSXfile(table, Xlsbomitem, ExportFileName);
        }

        protected void Btn2003_Click(object sender, EventArgs e)
        {
            //写到客户端（下载）
            HttpContext.Current.Response.Clear();
            //asp.net输出的Excel文件名
            //如果文件名是中文的话，需要进行编码转换，否则浏览器看到的下载文件是乱码。
            string fileName = HttpUtility.UrlEncode("Ec__DetailsData_" + myrexname + ".xls");
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.Write((Grid1));// 例：调用第一种方法
            //ep.SaveAs(Response.OutputStream);    第二种方式
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        #endregion ExportExcel
    }
}