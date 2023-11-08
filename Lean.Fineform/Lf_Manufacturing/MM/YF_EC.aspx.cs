using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;

using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Lean.Fineform.Lf_Manufacturing.MM
{
    public partial class Yf_EC : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcMMView";
            }
        }

        #endregion

        #region Page_Load
        public static String mysql, ecnno, tracestr, strecnno;

        public int selID;
        protected void Page_Load(object sender, EventArgs e)
        {

            //numBEbudgetmoney.Attributes.Add("Value", "0名");
            //numBEbudgetmoney.Attributes.Add("OnFocus", "if(this.value=='0') {this.value=''}");
            //numBEbudgetmoney.Attributes.Add("OnBlur", "if(this.value==''){this.value='0'}");
            if (!IsPostBack)
            {
                LoadData();

                //btnDel.OnClientClick = Grid2.GetNoSelectionAlertReference(" 请至少选择一项！") + GetDeleteScript(Grid2); // 删除选中行客户端脚本定义 .就这一句就可以了。（和官网的一至）


            }
        }


        private void LoadData()
        {
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            ddlGridPageSize2.SelectedValue = ConfigHelper.PageSize.ToString();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            BindGrid1();

            // 默认选中第一个角色
            Grid2.SelectedRowIndex = 0;
            // 每页记录数
            Grid2.PageSize = ConfigHelper.PageSize;
            BindGrid2();
            // 默认选中第一个角色
            Grid3.SelectedRowIndex = 0;
            // 每页记录数
            Grid3.PageSize = ConfigHelper.PageSize;
            BindGrid3();


        }



        private void BindGrid1()
        {
            try
            {
                Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

                IQueryable<Lean.Fineform.Lf_Business.Models.YF.BOMTA> q = DBYF.BOMTA; //.Include(u => u.Dept);

                string searchText = ttbSearchEcnsub.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.TA001.ToString().Contains(searchText) || u.TA002.ToString().Contains(searchText) || u.TA003.ToString().Contains(searchText));//|| u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                }
                q = q.OrderByDescending(u => u.TA003);
                var qs = q.Select(E => new { E.TA002, E.TA003 }).ToList().Distinct();

                //// 在查询添加之后，排序和分页之前获取总记录数
                //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                //if (Grid1.RecordCount != 0)
                //{
                //    // 排列和数据库分页
                //    GridHelper.GetPagedDataTable(Grid1, q);
                //}


                Grid1.DataSource = qs;
                Grid1.DataBind();



                Grid1.SelectedRowIndex = 0;
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




        private void BindGrid2()
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Grid2.DataSource = "";

                Grid2.DataBind();
                return;
            }



            if (!String.IsNullOrEmpty(ecnno))
            {

                Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

                IQueryable<Lean.Fineform.Lf_Business.Models.YF.BOMTB> q = DBYF.BOMTB; //.Include(u => u.Dept);

                q = q.Where(u => u.TB002.Contains(ecnno));
                var qs = from a in q
                         join b in DBYF.BOMTA on a.TB002 equals b.TA002
                         select new
                         {
                             
                             a.TB002,
                             a.TB003,
                             a.TB004,
                             a.TB005,
                             a.TB104,
                             a.TB108,
                             b.TA005,
                             b.TA003,

                         };

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = GridHelper.GetTotalCount(qs);
                if (GridHelper.GetTotalCount(qs) > 0)
                {
                    // 排列和数据库分页
                    DataTable table = GridHelper.GetPagedDataTable(Grid2, qs);
                    // 3.绑定到Grid
                    Grid2.DataSource = table;
                    Grid2.DataBind();
                }
                else
                {
                    // 3.绑定到Grid
                    Grid2.DataSource = "";
                    Grid2.DataBind();
                }
            }
            else
            {
                // 3.绑定到Grid
                Grid2.DataSource = "";
                Grid2.DataBind();
            }
        }


        private void BindGrid3()
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Grid3.DataSource = "";

                Grid3.DataBind();
                return;
            }



            if (!String.IsNullOrEmpty(ecnno))
            {

                Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

                IQueryable<Lean.Fineform.Lf_Business.Models.YF.BOMTC> q = DBYF.BOMTC; //.Include(u => u.Dept);

                q = q.Where(u => u.TC002.Contains(ecnno));


                // 在查询添加之后，排序和分页之前获取总记录数
                Grid3.RecordCount = GridHelper.GetTotalCount(q);
                if (GridHelper.GetTotalCount(q) > 0)
                {
                    // 排列和数据库分页
                    DataTable table = GridHelper.GetPagedDataTable(Grid3, q);
                    // 3.绑定到Grid
                    Grid3.DataSource = table;
                    Grid3.DataBind();
                }
                else
                {
                    // 3.绑定到Grid
                    Grid3.DataSource = "";
                    Grid3.DataBind();
                }
            }
            else
            {
                // 3.绑定到Grid
                Grid3.DataSource = "";
                Grid3.DataBind();
            }
        }

        #endregion

        #region Events
        protected void ttbSearchEcnsub_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.ShowTrigger1 = true;
            BindGrid1();
            BindGrid2();
            BindGrid3();
        }

        protected void ttbSearchEcnsub_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.Text = String.Empty;
            ttbSearchEcnsub.ShowTrigger1 = false;
            BindGrid1();
            BindGrid2();
            BindGrid3();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex1 = Grid2.PageIndex;


            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid2.PageIndex > Grid2.PageCount - 1)
            {
                Grid2.PageIndex = Grid2.PageCount - 1;
            }

            BindGrid2();

        }

        protected void ddlGridPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex1 = Grid3.PageIndex;

            Grid3.PageSize = Convert.ToInt32(ddlGridPageSize2.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid3.PageIndex > Grid3.PageCount - 1)
            {
                Grid3.PageIndex = Grid3.PageCount - 1;
            }
            BindGrid3();
        }
        #endregion

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
            BindGrid3();
        }

        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {

            object[] keys = Grid1.DataKeys[e.RowIndex];
            ecnno = keys[0].ToString();
            strecnno = keys[0].ToString();
            BindGrid2();
            BindGrid3();
        }

        #endregion

        #region Grid2 Events

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreBudgetDelete", Grid2, "deleteField");

            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript(Grid1);
           // CheckPowerWithLinkButtonField("CoreEcMMEdit", Grid2, "editField");
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            BindGrid2();

        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {

        }

        #endregion
        #region Grid3 Events

        protected void Grid3_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreBudgetDelete", Grid2, "deleteField");

            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript(Grid1);
            //CheckPowerWithLinkButtonField("CoreEcMMEdit", Grid2, "editField");
        }

        protected void Grid3_Sort(object sender, GridSortEventArgs e)
        {
            Grid3.SortDirection = e.SortDirection;
            Grid3.SortField = e.SortField;
            BindGrid3();
        }

        protected void Grid3_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid3.PageIndex = e.NewPageIndex;
            BindGrid3();
        }

        protected void Grid3_RowCommand(object sender, GridCommandEventArgs e)
        {
            //参数传递

            BindGrid3();

        }

        protected void Grid3_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {

        }

        #endregion






    }
}

