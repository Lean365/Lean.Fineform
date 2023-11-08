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

namespace Lean.Fineform.Lf_Manufacturing.EC.dept
{
    public partial class mm : PageBase
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
            //CheckPowerWithButton("CoreKitOutput", BtnExport);


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




                var q =
                        (from b in DB.Pp_Ecs
                             join a in DB.Pp_EcSubs on b.Ec_no equals a.Ec_no
                             //where a.Ec_qadate.ToString() == "" || a.Ec_qadate == null
                             //where b.Ec_distinction == 1
                         where a.isDelete == 0

                         select new
                         {
                             a.Ec_no,
                             b.Ec_issuedate,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_newitem,
                             a.Ec_note,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyTime,
                             Ec_documents = "../" + b.Ec_documents,

                         });
                //q.Select(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                //q.Where(s => s.Endtag == 0 && s.Ec_model.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_no.Contains(searchText) || s.Ec_bomitem.Contains(searchText) || s.Ec_issuedate.Contains(searchText));
                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Ec_no.Contains(searchText) || u.Ec_model.Contains(searchText) || u.Ec_issuedate.Contains(searchText) || u.Ec_model.Contains(searchText) || u.Ec_bomitem.Contains(searchText) || u.Ec_bomsubitem.Contains(searchText) || u.Ec_olditem.Contains(searchText) || u.Ec_newitem.Contains(searchText));
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

                var qs = q.Select(a =>
                new
                {
                    a.Ec_no,
                    a.Ec_issuedate,
                    a.Ec_model,
                    a.Ec_bomitem,
                    a.Ec_bomsubitem,
                    a.Ec_olditem,
                    a.Ec_newitem,
                    a.Ec_note,
                    a.Ec_mmdate,
                    a.Ec_mmlot,
                    a.Ec_mmlotno,
                    a.Ec_mmnote,
                    a.mmModifier,
                    a.mmModifyTime,
                    a.Ec_documents,

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
                //if (e.Values[3].ToString() == "◎未处理")
                //{

                //    e.Values[3] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[3]);
                //}
                //if (e.Values[4].ToString() == "◎未处理")
                //{

                //    e.Values[4] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[4]);
                //}
                //if (e.Values[5].ToString() == "◎未处理")
                //{

                //    e.Values[5] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[5]);
                //}
                //if (e.Values[6].ToString() == "◎未处理")
                //{

                //    e.Values[6] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[6]);
                //}
                //if (e.Values[7].ToString() == "◎未处理")
                //{

                //    e.Values[7] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[7]);
                //}
                //if (e.Values[8].ToString() == "◎未处理")
                //{

                //    e.Values[8] = String.Format(" <span><font color='red'>{0}</font></span>", e.Values[8]);
                //}
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
