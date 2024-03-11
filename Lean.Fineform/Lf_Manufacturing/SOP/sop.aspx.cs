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
namespace Fine.Lf_Manufacturing.SOP
{
    public partial class sop : PageBase
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

        #endregion

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
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreEcnDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            CheckPowerWithButton("CoreKitOutput", BtnExport);


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
                            (from a in DB.Pp_EcSops
                             //where b.Ec_distinction == 1
                             where a.isDelete == 0
                             where string.IsNullOrEmpty( a.Ec_pengadate)|| string.IsNullOrEmpty(a.Ec_pengpdate)

                             orderby a.Ec_entrydate descending
                             select new
                             {
                                 a.Ec_leader,
                                 a.Ec_issuedate,
                                 a.Ec_entrydate,
                                 //Ec_pmcdate = (a.Ec_pmcdate.ToString() == "" ? "◎未处理" : a.Ec_pmcdate == null ? "◎未处理" : a.Ec_pmcdate.ToString()),
                                 Ec_pengadate = (a.Ec_pengadate.ToString() == "" ? "◎未处理" : a.Ec_pengadate == null ? "◎未处理" : a.Ec_pengadate.ToString()),
                                 Ec_pengpdate = (a.Ec_pengpdate.ToString() == "" ? "◎未处理" : a.Ec_pengpdate == null ? "◎未处理" : a.Ec_pengpdate.ToString()),

                                 a.Ec_no,
                                 a.Ec_model,


                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }

                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }



                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_entrydate,
                        //E.Ec_pmcdate,
                        E.Ec_pengadate,
                        E.Ec_pengpdate,
                        //E.Ec_bomitem,
                        //E.Ec_bomsubitem,

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
                            (from a in DB.Pp_EcSops

                             //where b.Ec_distinction == 1
                             where a.isDelete == 0
                             where !string.IsNullOrEmpty(a.Ec_pengadate) || !string.IsNullOrEmpty(a.Ec_pengpdate)
                             orderby a.Ec_entrydate descending
                             select new
                             {
                                 a.Ec_leader,
                                 a.Ec_issuedate,
                                 a.Ec_entrydate,                                 
                                 Ec_pengadate = (a.Ec_pengadate.ToString() == "" ? "◎未处理" : a.Ec_pengadate == null ? "◎未处理" : a.Ec_pengadate.ToString()),
                                 Ec_pengpdate = (a.Ec_pengpdate.ToString() == "" ? "◎未处理" : a.Ec_pengpdate == null ? "◎未处理" : a.Ec_pengpdate.ToString()),

                                 a.Ec_no,
                                 a.Ec_model,


                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }

                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }



                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_entrydate,
                        E.Ec_pengadate,
                        E.Ec_pengpdate,

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
                            (from a in DB.Pp_EcSops
                             //where c.Ec_qadate.ToString() == "" || c.Ec_qadate == null
                             //where b.Ec_distinction == 1
                             where a.isDelete == 0
                             orderby a.Ec_entrydate descending
                             select new
                             {
                                 a.Ec_leader,
                                 a.Ec_issuedate,
                                 a.Ec_entrydate,
                                 //Ec_pmcdate = (a.Ec_pmcdate.ToString() == "" ? "◎未处理" : a.Ec_pmcdate == null ? "◎未处理" : a.Ec_pmcdate.ToString()),
                                 Ec_pengadate = (a.Ec_pengadate.ToString() == "" ? "◎未处理" : a.Ec_pengadate == null ? "◎未处理" : a.Ec_pengadate.ToString()),
                                 Ec_pengpdate = (a.Ec_pengpdate.ToString() == "" ? "◎未处理" : a.Ec_pengpdate == null ? "◎未处理" : a.Ec_pengpdate.ToString()),

                                 a.Ec_no,
                                 a.Ec_model,
                                 


                             });
                    //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                    string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_issuedate.Contains(searchText));
                    }

                        if (!string.IsNullOrEmpty(sdate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(sdate) >= 0);
                        }
                        if (!string.IsNullOrEmpty(edate))
                        {
                            q = q.Where(u => u.Ec_issuedate.CompareTo(edate) <= 0);
                        }



                    var qs = q.Select(E =>
                    new
                    {
                        E.Ec_leader,
                        E.Ec_issuedate,
                        E.Ec_no,
                        E.Ec_model,
                        E.Ec_entrydate,
                        //E.Ec_pmcdate,
                        E.Ec_pengadate,
                        E.Ec_pengpdate,


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
        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
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
        #endregion

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

                if (e.Values[7].ToString() == "◎未处理")
                {

                    e.Values[7] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[7]);
                }
                if (e.Values[8].ToString() == "◎未处理")
                {

                    e.Values[8] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[8]);
                }
                //if (e.Values[9].ToString() == "◎未处理")
                //{

                //    e.Values[9] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[9]);
                //}
                //if (e.Values[10].ToString() == "◎未处理")
                //{

                //    e.Values[10] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[10]);
                //}
                //if (e.Values[11].ToString() == "◎未处理")
                //{

                //    e.Values[11] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[11]);
                //}
                //if (e.Values[12].ToString() == "◎未处理")
                //{

                //    e.Values[12] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[12]);
                //}
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

        #endregion
        #region ExportExcel
        protected void BtnExport_Click(object sender, EventArgs e)
        {

            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Xlsbomitem, ExportFileName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            Xlsbomitem = "ec_" + xlsname;
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            Grid1.AllowPaging = false;
            ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
            Grid1.AllowPaging = true;

        }








        #endregion
    }
}
