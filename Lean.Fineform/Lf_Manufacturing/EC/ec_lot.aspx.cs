using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_lot : PageBase
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

        public String mysql, ecnno;
        public int selID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //numBEbudgetmoney.Attributes.Add("Value", "0名");
            //numBEbudgetmoney.Attributes.Add("OnFocus", "if(this.value=='0') {this.value=''}");
            //numBEbudgetmoney.Attributes.Add("OnBlur", "if(this.value==''){this.value='0'}");
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            CheckPowerWithButton("CoreFineExport", BtnIssueExport);
            CheckPowerWithButton("CoreFineExport", BtnEntryExport);
            //DateToDDL();

            // 权限检查
            //CheckPowerWithButton("CoreBudgetEdit", btnSave);
            //CheckPowerWithButton("CoreBusubjectDelete", btnDeleteSelected);

            //CheckPowerWithButton("CoreBudgetCheck", btnCheck);
            ////ResolveDeleteButtonForGrid(btnDeleteSelected, Grid2, "确定要删除选中的{0}项记录吗？");

            //CheckPowerWithButton("CoreBudgetunCheck", btnUncheck);
            //CheckPowerWithButton("CoreBudgetAdmit", btnAdmit);
            //btnAdmit.OnClientClick = Window1.GetShowReference("~/oneBudget/Expense/oneExpense_admit.aspx", "审核费用预算");
            //CheckPowerWithButton("CoreBudgetunAdmit", btnunAdmit);
            //btnunAdmit.OnClientClick = Window2.GetShowReference("~/oneBudget/Expense/oneExpense_admit.aspx", "弃审费用预算");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                if (rbtnFirstAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
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
                                b.Ec_pmcdate,
                                b.Ec_pmclot,
                                b.Ec_mmdate,
                                b.Ec_mmlot,
                                b.Ec_p2ddate,
                                b.Ec_p2dlot,
                                b.Ec_p1ddate,
                                b.Ec_p1dlot,
                                b.Ec_qadate,
                                b.Ec_qalot,
                                b.Ec_purdate,
                                b.Ec_iqcdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                a.Ec_documents,
                                a.Ec_letterno,
                                a.Ec_letterdoc,
                                a.Ec_eppletterno,
                                a.Ec_eppletterdoc,
                                a.Ec_teppletterno,
                                a.Ec_teppletterdoc,
                            };

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText));
                        //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
                    }

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                    }

                    var qs = q.Select(E => new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_entrydate,
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
                    }).Distinct();

                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    // 排列和数据库分页
                    // 2.获取当前分页数据
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
                            where b.Ec_newitem != "0"
                            where !b.Ec_pmcmemo.Contains("EOL")
                            where a.Ec_distinction != 4
                            where a.Ec_distinction != 2
                            where !string.IsNullOrEmpty(b.Ec_qadate)
                            orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                            select new
                            {
                                a.Ec_issuedate,
                                a.Ec_leader,
                                a.Ec_distinction,
                                a.Ec_no,
                                b.Ec_entrydate,
                                b.Ec_pmcdate,
                                b.Ec_pmclot,
                                b.Ec_mmdate,
                                b.Ec_mmlot,
                                b.Ec_p2ddate,
                                b.Ec_p2dlot,
                                b.Ec_p1ddate,
                                b.Ec_p1dlot,
                                b.Ec_qadate,
                                b.Ec_qalot,
                                b.Ec_purdate,
                                b.Ec_iqcdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                a.Ec_documents,
                                a.Ec_letterno,
                                a.Ec_letterdoc,
                                a.Ec_eppletterno,
                                a.Ec_eppletterdoc,
                                a.Ec_teppletterno,
                                a.Ec_teppletterdoc,
                            };

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText));
                    }

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                    }

                    //q = q.Where(u=>u.Ec_qadate!=null);

                    var qs = q.Select(E => new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_entrydate,
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
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    // 排列和数据库分页
                    // 2.获取当前分页数据
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
                            where b.Ec_newitem != "0"
                            where !b.Ec_pmcmemo.Contains("EOL")
                            where a.Ec_distinction != 4
                            where a.Ec_distinction != 2
                            //where b.Ec_qadate == ""
                            orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                            select new
                            {
                                a.Ec_issuedate,
                                a.Ec_leader,
                                a.Ec_distinction,
                                b.Ec_entrydate,
                                a.Ec_no,
                                b.Ec_pmcdate,
                                b.Ec_pmclot,
                                b.Ec_mmdate,
                                b.Ec_mmlot,
                                b.Ec_p2ddate,
                                b.Ec_p2dlot,
                                b.Ec_p1ddate,
                                b.Ec_p1dlot,
                                b.Ec_qadate,
                                b.Ec_qalot,
                                b.Ec_purdate,
                                b.Ec_iqcdate,
                                b.Ec_model,
                                b.Ec_bomitem,
                                a.Ec_documents,
                                a.Ec_letterno,
                                a.Ec_letterdoc,
                                a.Ec_eppletterno,
                                a.Ec_eppletterdoc,
                                a.Ec_teppletterno,
                                a.Ec_teppletterdoc,
                            };

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText));
                        //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
                    }

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                    }

                    var qs = q.Select(E => new
                    {
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_distinction,
                        E.Ec_entrydate,
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
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    // 排列和数据库分页
                    // 2.获取当前分页数据
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
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
                //ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                //ttbSearchMessage.Text = "";
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
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid();
            }
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events

        #region Grid1 Events

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

        #endregion Grid1 Events

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #region ExportExcel

        protected void BtnIssueExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            if (rbtnFirstAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
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
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
                    //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
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

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    发行日期 = E.Ec_issuedate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });

                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_unenforced_Issuedate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            if (rbtnSecondAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
                        where b.Ec_newitem != "0"
                        where !b.Ec_pmcmemo.Contains("EOL")
                        where a.Ec_distinction != 4
                        where a.Ec_distinction != 2
                        where !string.IsNullOrEmpty(b.Ec_qadate)

                        orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_distinction,
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
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
                //q = q.Where(u=>u.Ec_qadate!=null);

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    发行日期 = E.Ec_issuedate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_enforced_Issuedate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            if (rbtnThirdAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
                        where b.Ec_newitem != "0"
                        where !b.Ec_pmcmemo.Contains("EOL")
                        where a.Ec_distinction != 4
                        where a.Ec_distinction != 2
                        //where b.Ec_qadate == ""
                        orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_distinction,
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
                    //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
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

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    发行日期 = E.Ec_issuedate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_Issuedate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
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
            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                var q =
                        (from a in DB.Pp_Ecs
                         join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                         where b.isDeleted == 0
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
                    Xlsbomitem = "ec_Unenforced_List";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
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
            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                var q =
                        (from a in DB.Pp_Ecs
                         join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                         where b.isDeleted == 0
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
                    Xlsbomitem = "ec_Implemented_List";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
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

        protected void BtnEntryExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            if (rbtnFirstAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
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
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
                    //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
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

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    登入日期 = E.Ec_entrydate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });

                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_unenforced_Entrydate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            if (rbtnSecondAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
                        where b.Ec_newitem != "0"
                        where !b.Ec_pmcmemo.Contains("EOL")
                        where a.Ec_distinction != 4
                        where a.Ec_distinction != 2
                        where !string.IsNullOrEmpty(b.Ec_qadate)

                        orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_distinction,
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
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
                //q = q.Where(u=>u.Ec_qadate!=null);

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    登入日期 = E.Ec_entrydate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_enforced_Entrydate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            if (rbtnThirdAuto.Checked)
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on b.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where b.isDeleted == 0
                        where b.Ec_newitem != "0"
                        where !b.Ec_pmcmemo.Contains("EOL")
                        where a.Ec_distinction != 4
                        where a.Ec_distinction != 2
                        //where b.Ec_qadate == ""
                        orderby a.Ec_issuedate, b.Ec_model, b.Ec_olditem, b.Ec_newitem
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_distinction,
                            a.Ec_entrydate,
                            a.Ec_no,
                            b.Ec_pmcdate,
                            b.Ec_pmclot,
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,
                            b.Ec_p1ddate,
                            b.Ec_p1dlot,
                            b.Ec_qadate,
                            b.Ec_qalot,
                            b.Ec_purdate,
                            b.Ec_iqcdate,
                            b.Ec_model,
                            b.Ec_bomitem,
                            a.Ec_documents,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.ToString().Contains(searchText));
                    //q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_qalot.ToString().Contains(searchText) || u.Ec_pmclot.ToString().Contains(searchText) || u.Ec_mmlot.ToString().Contains(searchText) || u.Ec_p2dlot.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText) || u.Ec_p1dlot.ToString().Contains(searchText) || u.Ec_letterno.ToString().Contains(searchText) || u.Ec_eppletterno.ToString().Contains(searchText) || u.Ec_teppletterno.ToString().Contains(searchText));
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

                var QsDistinct = q.Select(E => new
                {
                    E.Ec_issuedate,
                    E.Ec_leader,
                    E.Ec_distinction,
                    E.Ec_entrydate,
                    E.Ec_no,
                    E.Ec_pmcdate,
                    E.Ec_pmclot,
                    E.Ec_mmdate,
                    E.Ec_mmlot,
                    E.Ec_p2ddate,
                    E.Ec_p2dlot,
                    E.Ec_p1ddate,
                    E.Ec_p1dlot,
                    E.Ec_qadate,
                    E.Ec_qalot,
                    E.Ec_purdate,
                    E.Ec_iqcdate,
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

                var qs = QsDistinct.Select(E => new
                {
                    登入日期 = E.Ec_entrydate,
                    设变号码 = E.Ec_no,
                    机种 = E.Ec_model,
                    成品 = E.Ec_bomitem,
                    预定投入 = E.Ec_pmcdate,
                    投入批次 = E.Ec_pmclot,
                    出库日期 = E.Ec_mmdate,
                    出库批次 = E.Ec_mmlot,
                    制二课生产日期 = E.Ec_p2ddate,
                    制二课生产批次 = E.Ec_p2dlot,
                    生产日期 = E.Ec_p1ddate,
                    生产批次 = E.Ec_p1dlot,
                    抽样日期 = E.Ec_qadate,
                    抽样批次 = E.Ec_qalot,
                });
                if (qs.Any())
                {
                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Xlsbomitem = "ec_Lot_Entrydate";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                    ExportFileName = Xlsbomitem + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                    //Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion ExportExcel
    }
}