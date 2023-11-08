using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;using System.Data.Entity.Validation;

using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Lean.Fineform.Lf_Manufacturing.EC
{
    public partial class ec_eng :  PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcENGView";
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

            try {
                //停产机种不显示
                var qss = from a in DB.Pp_SapEcnSubs
                              //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                          join b in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals b.D_SAP_DEST_Z001
                          where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                                             where d.D_SAP_ZCA1D_Z034 == ""
                                                             select d.D_SAP_ZCA1D_Z002)
                                                          .Contains(a.D_SAP_ZPABD_S002)
                          //where b.Ec_no == strecn
                          //where a.Prodate == sdate//投入日期
                          select new
                          {
                              a.D_SAP_ZPABD_S001,
                              a.D_SAP_ZPABD_S002,
                              b.D_SAP_DEST_Z002
                          };
                var ss = from b in qss
                         where b.D_SAP_DEST_Z002 != ""
                         where b.D_SAP_ZPABD_S001 != "" && !(from d in DB.Pp_EcSubs
                                                             where d.isDelete == 0
                                                             select d.Ec_no)
                                                          .Contains(b.D_SAP_ZPABD_S001)
                         select new
                         {
                             b.D_SAP_ZPABD_S001
                         };



                var q = from a in DB.Pp_SapEcns
                        where a.D_SAP_ZPABD_Z001 != "" && (from b in ss

                                                           select b.D_SAP_ZPABD_S001)
                                                          .Contains(a.D_SAP_ZPABD_Z001)
                        //where b.Ec_no == strecn
                        //where a.Prodate == sdate//投入日期
                        select new
                        {
                            a.D_SAP_ZPABD_Z001,
                            a.D_SAP_ZPABD_Z005,
                            a.D_SAP_ZPABD_Z003,
                            a.D_SAP_ZPABD_Z002,
                            a.D_SAP_ZPABD_Z027,
                            a.D_SAP_ZPABD_Z025,
                            a.D_SAP_ZPABD_Z012,
                            a.D_SAP_ZPABD_Z013
                        };

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.D_SAP_ZPABD_Z001.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                }
                //q = q.Where(u => u.D_SAP_ZPABD_Z005.CompareTo("20190701") > 0);
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
            CheckPowerWithLinkButtonField("CoreEcENGNew", Grid1, "EcnAdd");

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
            ttbSearchMessage.Text = "";
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
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "EcnAdd")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/ec_eng_new.aspx?ECNNO=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }
        }
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            //labResult.Text = keys[0].ToString();

            //PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/ec_eng_new.aspx?ECNNO=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //this.Response.Write("<script language=javascript>window.open('~/plutoErpinfo/bomlist_view.aspx?ItemMaster=" + keys[1].ToString() + "','newwindow','width=200,height=200')</script>");
            //Window2.GetShowReference("~/plutoErpinfo/bomlist_view.aspx", "弹出窗口二");
            //Response.Redirect("~/plutoErpinfo/bomlist_view.aspx?ItemMaster=" + keys[1].ToString() + "");


            //labResult.Text = keys[0].ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/dept/view_desc.aspx?Ec_no=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion




    }
}