using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec : PageBase
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

        public static string mysql, myrexname, xlsname;
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
            //rbtnFirstAuto.Text=global::Resources.GlobalResource.Unenforced;
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreEcnDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            CheckPowerWithButton("CoreFineExport", BtnIssueExport);
            CheckPowerWithButton("CoreFineExport", BtnEntryExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            //查询LINQ去重复

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                if (rbtnFirstAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             where string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem

                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 a.Ec_distinction,
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
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
                if (rbtnSecondAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             where !string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 a.Ec_distinction,
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
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
                if (rbtnThirdAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             //where string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 a.Ec_distinction,
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
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
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            if (rbtnFirstAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnThirdAuto.Checked)
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

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            if (row != null)
            {
                if (e.Values[3].ToString() == "◎未处理")
                {
                    e.Values[3] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[3]);
                }
                if (e.Values[4].ToString() == "◎未处理")
                {
                    e.Values[4] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[4]);
                }
                if (e.Values[5].ToString() == "◎未处理")
                {
                    e.Values[5] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[5]);
                }
                if (e.Values[6].ToString() == "◎未处理")
                {
                    e.Values[6] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[6]);
                }
                if (e.Values[7].ToString() == "◎未处理")
                {
                    e.Values[7] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[7]);
                }
                if (e.Values[8].ToString() == "◎未处理")
                {
                    e.Values[8] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[8]);
                }
                if (e.Values[9].ToString() == "◎未处理")
                {
                    e.Values[9] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[9]);
                }
                if (e.Values[10].ToString() == "◎未处理")
                {
                    e.Values[10] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[10]);
                }
                if (e.Values[11].ToString() == "◎未处理")
                {
                    e.Values[11] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[11]);
                }
                if (e.Values[12].ToString() == "◎未处理")
                {
                    e.Values[12] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[12]);
                }
            }
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
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

        protected void BtnIssueExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Prefix_XlsxName, Export_FileName, SheetName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                if (rbtnFirstAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             where string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                    }).Distinct();

                    var qs = Q_Distinct.Select(E =>
                            new
                            {
                                技术担当 = E.Ec_leader,
                                登录日期 = E.Ec_entrydate,
                                发行日期 = E.Ec_issuedate,
                                设变号码 = E.Ec_no,
                                机种名称 = E.Ec_model,
                                成品物料 = E.Ec_bomitem,
                                品管登录 = E.Ec_qadate,
                                生管登录 = E.Ec_pmcdate,
                                部管登录 = E.Ec_mmdate,
                                采购登录 = E.Ec_purdate,
                                受检登录 = E.Ec_iqcdate,
                                制一登录 = E.Ec_p1ddate,
                                制二登录 = E.Ec_p2ddate,
                                管理区分 = E.Ec_distinction,
                            });

                    // 在查询添加之后，排序和分页之前获取总记录数
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                        Prefix_XlsxName = "ec_unenforced_Issuedate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             where !string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                    }).Distinct();

                    var qs = Q_Distinct.Select(E =>
                            new
                            {
                                技术担当 = E.Ec_leader,
                                登录日期 = E.Ec_entrydate,
                                发行日期 = E.Ec_issuedate,
                                设变号码 = E.Ec_no,
                                机种名称 = E.Ec_model,
                                成品物料 = E.Ec_bomitem,
                                品管登录 = E.Ec_qadate,
                                生管登录 = E.Ec_pmcdate,
                                部管登录 = E.Ec_mmdate,
                                采购登录 = E.Ec_purdate,
                                受检登录 = E.Ec_iqcdate,
                                制一登录 = E.Ec_p1ddate,
                                制二登录 = E.Ec_p2ddate,
                                管理区分 = E.Ec_distinction,
                            });

                    // 在查询添加之后，排序和分页之前获取总记录数
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        Prefix_XlsxName = "ec_enforced_Issuedate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             //where string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_qadate == null
                             orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_no,
                        E.Ec_entrydate,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                    }).Distinct();

                    var qs = Q_Distinct.Select(E =>
                            new
                            {
                                技术担当 = E.Ec_leader,
                                登录日期 = E.Ec_entrydate,
                                发行日期 = E.Ec_issuedate,
                                设变号码 = E.Ec_no,
                                机种名称 = E.Ec_model,
                                成品物料 = E.Ec_bomitem,
                                品管登录 = E.Ec_qadate,
                                生管登录 = E.Ec_pmcdate,
                                部管登录 = E.Ec_mmdate,
                                采购登录 = E.Ec_purdate,
                                受检登录 = E.Ec_iqcdate,
                                制一登录 = E.Ec_p1ddate,
                                制二登录 = E.Ec_p2ddate,
                                管理区分 = E.Ec_distinction,
                            });
                    // 在查询添加之后，排序和分页之前获取总记录数
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        Prefix_XlsxName = "ec_Issuedate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        protected void BtnEntryExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Prefix_XlsxName, Export_FileName, SheetName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                if (rbtnFirstAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_distinction == 1
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2

                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_entrydate,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_distinction
                    }).Distinct();
                    var qs = Q_Distinct.Select(E =>
                    new
                    {
                        技术担当 = E.Ec_leader,
                        登录日期 = E.Ec_entrydate,
                        发行日期 = E.Ec_issuedate,
                        设变号码 = E.Ec_no,
                        机种名称 = E.Ec_model,
                        成品物料 = E.Ec_bomitem,
                        品管登录 = E.Ec_qadate,
                        生管登录 = E.Ec_pmcdate,
                        部管登录 = E.Ec_mmdate,
                        采购登录 = E.Ec_purdate,
                        受检登录 = E.Ec_iqcdate,
                        制一登录 = E.Ec_p1ddate,
                        制二登录 = E.Ec_p2ddate,
                        管理区分 = E.Ec_distinction,
                    });
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                        Prefix_XlsxName = "ec_unenforced_Entrydate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where !string.IsNullOrEmpty(b.Ec_qadate)
                             //where b.Ec_distinction == 1
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             //where a.Endtag == 1
                             //where a.Ec_model.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_no.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_issuedate.Contains(searchText)
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_entrydate,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_distinction
                    }).Distinct();
                    var qs = Q_Distinct.Select(E =>
                    new
                    {
                        技术担当 = E.Ec_leader,
                        登录日期 = E.Ec_entrydate,
                        发行日期 = E.Ec_issuedate,
                        设变号码 = E.Ec_no,
                        机种名称 = E.Ec_model,
                        成品物料 = E.Ec_bomitem,
                        品管登录 = E.Ec_qadate,
                        生管登录 = E.Ec_pmcdate,
                        部管登录 = E.Ec_mmdate,
                        采购登录 = E.Ec_purdate,
                        受检登录 = E.Ec_iqcdate,
                        制一登录 = E.Ec_p1ddate,
                        制二登录 = E.Ec_p2ddate,
                        管理区分 = E.Ec_distinction,
                    });
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        Prefix_XlsxName = "ec_enforced_Entrydate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             where b.IsDeleted == 0
                             where b.Ec_newitem != "0"
                             where !b.Ec_pmcmemo.Contains("EOL")
                             where a.Ec_distinction != 4
                             where a.Ec_distinction != 2
                             //where a.Ec_model.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_no.Contains(searchText) || a.Ec_bomitem.Contains(searchText) || b.Ec_issuedate.Contains(searchText)
                             select new
                             {
                                 a.Ec_issuedate,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                                 a.Ec_no,
                                 b.Ec_entrydate,
                                 Ec_pmcdate = (b.Ec_pmcdate.ToString() == "" ? "◎未处理" : b.Ec_pmcdate == null ? "◎未处理" : b.Ec_pmcdate.ToString()),
                                 Ec_mmdate = (b.Ec_mmdate.ToString() == "" ? "◎未处理" : b.Ec_mmdate == null ? "◎未处理" : b.Ec_mmdate.ToString()),
                                 Ec_purdate = (b.Ec_purdate.ToString() == "" ? "◎未处理" : b.Ec_purdate == null ? "◎未处理" : b.Ec_purdate.ToString()),
                                 Ec_iqcdate = (b.Ec_iqcdate.ToString() == "" ? "◎未处理" : b.Ec_iqcdate == null ? "◎未处理" : b.Ec_iqcdate.ToString()),
                                 Ec_p1ddate = (b.Ec_p1ddate.ToString() == "" ? "◎未处理" : b.Ec_p1ddate == null ? "◎未处理" : b.Ec_p1ddate.ToString()),
                                 Ec_p2ddate = (b.Ec_p2ddate.ToString() == "" ? "◎未处理" : b.Ec_p2ddate == null ? "◎未处理" : b.Ec_p2ddate.ToString()),
                                 Ec_qadate = (b.Ec_qadate.ToString() == "" ? "◎未处理" : b.Ec_qadate == null ? "◎未处理" : b.Ec_qadate.ToString()),
                                 b.Ec_pmclot,
                                 b.Ec_mmlot,
                                 b.Ec_p2dlot,
                                 b.Ec_p1dlot,
                                 b.Ec_qalot,
                                 b.Ec_model,
                                 b.Ec_bomitem,
                                 a.Ec_documents,
                                 a.Ec_letterno,
                                 a.Ec_letterdoc,
                                 a.Ec_eppletterno,
                                 a.Ec_eppletterdoc,
                                 a.Ec_teppletterno,
                                 a.Ec_teppletterdoc,
                             });
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }
                    }
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));

                    var Q_Distinct = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_entrydate,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_qadate,
                        E.Ec_pmcdate,
                        E.Ec_mmdate,
                        E.Ec_purdate,
                        E.Ec_iqcdate,
                        E.Ec_p1ddate,
                        E.Ec_p2ddate,
                        E.Ec_pmclot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dlot,
                        E.Ec_qalot,
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_distinction
                    }).Distinct();
                    var qs = Q_Distinct.Select(E =>
                    new
                    {
                        技术担当 = E.Ec_leader,
                        登录日期 = E.Ec_entrydate,
                        发行日期 = E.Ec_issuedate,
                        设变号码 = E.Ec_no,
                        机种名称 = E.Ec_model,
                        成品物料 = E.Ec_bomitem,
                        品管登录 = E.Ec_qadate,
                        生管登录 = E.Ec_pmcdate,
                        部管登录 = E.Ec_mmdate,
                        采购登录 = E.Ec_purdate,
                        受检登录 = E.Ec_iqcdate,
                        制一登录 = E.Ec_p1ddate,
                        制二登录 = E.Ec_p2ddate,
                        管理区分 = E.Ec_distinction,
                    });
                    // 在查询添加之后，排序和分页之前获取总记录数
                    if (qs.Any())
                    {
                        int c = GridHelper.GetTotalCount(qs);
                        ConvertHelper.LinqConvertToDataTable(qs);
                        Prefix_XlsxName = "ec_Entrydate";
                        //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                        Export_FileName = Prefix_XlsxName + ".xlsx";
                        //Grid1.AllowPaging = false;
                        ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                        //Grid1.AllowPaging = true;
                    }
                    else

                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        protected void BtnUnenforced_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Prefix_XlsxName, Export_FileName, SheetName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                var q =
                        (from a in DB.Pp_Ecs
                         join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                         where b.IsDeleted == 0
                         where b.Ec_newitem != "0"
                         where !b.Ec_pmcmemo.Contains("EOL")
                         where a.Ec_distinction != 4
                         where a.Ec_distinction != 2
                         //where string.IsNullOrEmpty(b.Ec_qadate)
                         //where b.Ec_qadate == null

                         orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                         select new
                         {
                             a.Ec_issuedate,
                             a.Ec_leader,
                             Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                             a.Ec_no,
                             b.Ec_entrydate,
                             b.Ec_pmcdate,
                             b.Ec_mmdate,
                             b.Ec_purdate,
                             b.Ec_iqcdate,
                             b.Ec_p1ddate,
                             b.Ec_p2ddate,
                             b.Ec_qadate,
                             b.Ec_model,
                             b.Ec_bomitem,
                         });
                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                }
                else
                {
                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                    }
                }

                var Q_Distinct = q.Select(E =>
                        new
                        {
                            E.Ec_issuedate,
                            E.Ec_leader,
                            E.Ec_distinction,
                            E.Ec_no,
                            E.Ec_entrydate,
                            E.Ec_qadate,
                            E.Ec_pmcdate,
                            E.Ec_mmdate,
                            E.Ec_purdate,
                            E.Ec_iqcdate,
                            E.Ec_p1ddate,
                            E.Ec_p2ddate,
                            E.Ec_model,
                            E.Ec_bomitem,
                        }).Distinct();

                //查询完全实施点数

                var q_All = from a in Q_Distinct
                            where !(from d in DB.Pp_Ec_Subs
                                    where !string.IsNullOrEmpty(d.Ec_qadate)
                                    select d.Ec_no)
                                 .Contains(a.Ec_no)
                            select a;
                var qs = q_All.Select(E =>
                            new
                            {
                                技术担当 = E.Ec_leader,
                                登录日期 = E.Ec_entrydate,
                                发行日期 = E.Ec_issuedate,
                                设变号码 = E.Ec_no,
                                机种名称 = E.Ec_model,
                                成品物料 = E.Ec_bomitem,
                                品管登录 = E.Ec_qadate,
                                生管登录 = E.Ec_pmcdate,
                                部管登录 = E.Ec_mmdate,
                                采购登录 = E.Ec_purdate,
                                受检登录 = E.Ec_iqcdate,
                                制一登录 = E.Ec_p1ddate,
                                制二登录 = E.Ec_p2ddate,
                                管理区分 = E.Ec_distinction,
                            });

                // 在查询添加之后，排序和分页之前获取总记录数
                // 在查询添加之后，排序和分页之前获取总记录数
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    Prefix_XlsxName = "ec_Unenforced_List";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        protected void BtnImplemented_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Prefix_XlsxName, Export_FileName, SheetName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                var q =
                        (from a in DB.Pp_Ecs
                         join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                         where b.IsDeleted == 0
                         where b.Ec_newitem != "0"
                         where !b.Ec_pmcmemo.Contains("EOL")
                         where a.Ec_distinction != 4
                         where a.Ec_distinction != 2
                         //where string.IsNullOrEmpty(b.Ec_qadate)
                         //where b.Ec_qadate == null

                         orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                         select new
                         {
                             a.Ec_issuedate,
                             a.Ec_leader,
                             Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : a.Ec_distinction == 2 ? "部管" : a.Ec_distinction == 3 ? "内部" : a.Ec_distinction == 4 ? "技术" : a.Ec_distinction.ToString()),
                             a.Ec_no,
                             b.Ec_entrydate,
                             b.Ec_pmcdate,
                             b.Ec_mmdate,
                             b.Ec_purdate,
                             b.Ec_iqcdate,
                             b.Ec_p1ddate,
                             b.Ec_p2ddate,
                             b.Ec_qadate,
                             b.Ec_model,
                             b.Ec_bomitem,
                         });
                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                }
                else
                {
                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                    }
                }

                var Q_Distinct = q.Select(E =>
                        new
                        {
                            E.Ec_issuedate,
                            E.Ec_leader,
                            E.Ec_distinction,
                            E.Ec_no,
                            E.Ec_entrydate,
                            E.Ec_qadate,
                            E.Ec_pmcdate,
                            E.Ec_mmdate,
                            E.Ec_purdate,
                            E.Ec_iqcdate,
                            E.Ec_p1ddate,
                            E.Ec_p2ddate,
                            E.Ec_model,
                            E.Ec_bomitem,
                        }).Distinct();

                //查询完全实施点数

                var q_All = from a in Q_Distinct
                            where (from d in DB.Pp_Ec_Subs
                                   where !string.IsNullOrEmpty(d.Ec_qadate)
                                   select d.Ec_no)
                                 .Contains(a.Ec_no)
                            select a;
                var qs = q_All.Select(E =>
                            new
                            {
                                技术担当 = E.Ec_leader,
                                登录日期 = E.Ec_entrydate,
                                发行日期 = E.Ec_issuedate,
                                设变号码 = E.Ec_no,
                                机种名称 = E.Ec_model,
                                成品物料 = E.Ec_bomitem,
                                品管登录 = E.Ec_qadate,
                                生管登录 = E.Ec_pmcdate,
                                部管登录 = E.Ec_mmdate,
                                采购登录 = E.Ec_purdate,
                                受检登录 = E.Ec_iqcdate,
                                制一登录 = E.Ec_p1ddate,
                                制二登录 = E.Ec_p2ddate,
                                管理区分 = E.Ec_distinction,
                            });

                // 在查询添加之后，排序和分页之前获取总记录数
                // 在查询添加之后，排序和分页之前获取总记录数
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    Prefix_XlsxName = "ec_Implemented_List";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }

        #endregion ExportExcel
    }
}