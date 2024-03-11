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

namespace Fine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_notice_list : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcNoticeNew";
            }
        }

        #endregion

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
            // btnClose.OnClientClick = ActiveWindow.GetHideReference();

            //btnClose.OnClientClick = PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreNoticeDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreNoticeNew", btnNew);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/plutoCommon/notice_new.aspx", "新增公告通知");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }



        private void BindGrid()
        {

            try
            {

                var q = from a in DB.Qm_Outgoings
                            //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                        where a.isDelete == 0
                        where (from d in DB.Qm_Improvements
                               select d.qmInspector + d.qmProlot + d.qmOrder + d.qmModels + d.qmMaterial + d.qmCheckdate)
                                .Contains(a.qmInspector + a.qmProlot + a.qmOrder + a.qmModels + a.qmMaterial + a.qmCheckdate)
                        //where b.Ec_no == strecn
                        //where a.Prodate == sdate//投入日期
                        select new
                        {
                            a.qmInspector,
                            a.qmOrder,
                            a.qmModels,
                            a.qmMaterial,
                            a.qmProlot,
                            a.qmCheckdate,
                            a.qmLine,
                            a.qmProqty,
                            a.qmRejectqty,
                        };


                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.qmProlot.Contains(searchText)|| u.qmModels.Contains(searchText)|| u.qmMaterial.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q);

                if (Grid1.RecordCount != 0)
                {
                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }

                //Grid1.DataSource = q;
                //Grid1.DataBind();
                //          //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                //          mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason FROM [dbo].[ProSapEngChanges] " +
                //  "    LEFT JOIN [dbo].[Ec_s]   ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 WHERE REPLACE(Ec_no,' ','') IS NULL "+
                // " and D_SAP_ZPABD_Z001 in (SELECT[D_SAP_ZPABD_S001]"+

                //" FROM[OneHerba2019].[dbo].[ProSapMaterials]" +
                //" left join[OneHerba2019].[dbo].[ProSapEngChangeSubs]" +
                //  "     on[D_SAP_ZPABD_S002]=[D_SAP_ZCA1D_Z002])    ";
                //          // 在用户名称中搜索
                //          string searchText = ttbSearchMessage.Text.Trim();
                //          if (!String.IsNullOrEmpty(searchText))
                //          {
                //              mysql = mysql + "  Where ECNNO='" + searchText + "' OR ISSUEDATE='" + searchText + " 'OR ECNTITLE='" + searchText + "' OR ECNMODEL='" + searchText + "' GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013 ORDER BY D_SAP_ZPABD_Z005 DESC;";
                //          }
                //          // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //          Grid1.RecordCount = GetTotalCount();

                //          // 2.获取当前分页数据
                //          DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                //          // 3.绑定到Grid
                //          Grid1.DataSource = table;
                //          Grid1.DataBind();
                //          //if (GetIdentityName() != "admin")
                //          //{)
                //          //    q = q.Where(u => u.Name != "admin");
                //          //}

                //          // 过滤启用状态
                //          //if (rblEnableStatus.SelectedValue != "all")
                //          //{
                //          //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                //          //}

                //          // 在查询添加之后，排序和分页之前获取总记录数
                //          //Grid1.RecordCount = q.Count();

                //          // 排列和数据库分页
                //          //q = SortAndPage<INVMB>(q, Grid1);

                //          //Grid1.DataSource = mydr;
                //          //Grid1.DataBind();
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreNoticeEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreNoticeDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");

        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            CheckPowerWithLinkButtonField("CoreFqcNoticeNew", Grid1, "NoticeAdd");

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
            //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "NoticeAdd")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_action_new.aspx?ECNNO=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }


    }
}