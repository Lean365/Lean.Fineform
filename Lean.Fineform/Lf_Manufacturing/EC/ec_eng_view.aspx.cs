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
    public partial class ec_eng_view : PageBase
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
            //CheckPowerWithButton("CorePlutoExport2007", BtnExport);
            //CheckPowerWithButton("CorePlutoExport2003", Btn2003);
            CheckPowerWithButton("CoreEcENGNew", btnNew);
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/EC/ec_eng.aspx", "新增") + Window1.GetMaximizeReference();
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            Window1.GetMaximizeReference();
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
            try {
                if (rbtnFirstAuto.Checked)
                {
                    IQueryable<Pp_Ec> q = DB.Pp_Ecs; //.Include(u => u.Dept);


                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_issuedate.Contains(searchText) || u.Ec_leader.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_status.Contains(searchText));
                    }

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
                    q = q.Where(u => u.Ec_status.Contains("Fixed"));

                    q = q.Where(u => u.isDelete==0);
                    q = q.OrderByDescending(u => u.Ec_issuedate);

                    var qs = q.Select(E => new 
                   {
                       E.GUID,
                       E.Ec_no,
                       E.Ec_issuedate,
                       E.Ec_documents,
                       E.UDF06,
                       E.Ec_leader,
                       Ec_distinction=(E.Ec_distinction==1? "全仕向": (E.Ec_distinction == 2?"部管":(E.Ec_distinction == 3?"内部":"技术"))),
                        isConfirm = (E.isConfirm == 0 ? "制二不管理" :  "制二管理"),
                        isModifysop = (E.isModifysop == 0 ? "不更新" : "需更新"),
                        Ec_status = (E.Ec_status.Contains("Work in Process") ? "WIP" :(E.Ec_status.Contains("Change at P.P.") ? "Change": E.Ec_status)),
                       E.Ec_letterno,
                       E.Ec_letterdoc,
                       E.Ec_eppletterno,
                       E.Ec_eppletterdoc,
                       E.Ec_teppletterno,
                       E.Ec_teppletterdoc,
                       E.CreateTime,


                   });

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
                else if (rbtnSecondAuto.Checked)
                {
                    IQueryable<Pp_Ec> q = DB.Pp_Ecs; //.Include(u => u.Dept);


                    // 在用户名称中搜索

                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_issuedate.Contains(searchText) || u.Ec_leader.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_status.Contains(searchText));
                    }
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
                    //string yy = DateTime.Now.Year.ToString();//获取当前年份
                    //q = q.Where(u => u.Ec_issuedate.Substring(0, 4).Contains(yy));

                    q = q.Where(u => u.Ec_status.Contains("Work in Process"));
                    q = q.Where(u => u.isDelete == 0);
                    q = q.OrderByDescending(u => u.Ec_issuedate);
                    var qs = q.Select(E => new
                    {
                        E.GUID,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_documents,
                        E.UDF06,
                        E.Ec_leader,
                        Ec_distinction = (E.Ec_distinction == 1 ? "全仕向" : (E.Ec_distinction == 2 ? "部管" : (E.Ec_distinction == 3 ? "内部" : "技术"))),
                        isConfirm = (E.isConfirm == 0 ? "制二不管理" : "制二管理"),
                        isModifysop = (E.isModifysop == 0 ? "不更新" : "需更新"),
                        Ec_status = (E.Ec_status.Contains("Work in Process") ? "WIP" : (E.Ec_status.Contains("Change at P.P.") ? "Change" : E.Ec_status)),
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.CreateTime,


                    });
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
                else if (rbtnThirdAuto.Checked)
                {
                    IQueryable<Pp_Ec> q = DB.Pp_Ecs; //.Include(u => u.Dept);


                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_issuedate.Contains(searchText) || u.Ec_leader.Contains(searchText) || u.Ec_no.Contains(searchText) || u.Ec_status.Contains(searchText));
                    }


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
                    q = q.Where(u => u.isDelete == 0);
                    q = q.OrderByDescending(u => u.Ec_issuedate);
                    q = q.OrderByDescending(u => u.Ec_issuedate);
                    var qs = q.Select(E => new
                    {
                        E.GUID,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_documents,
                        E.UDF06,
                        E.Ec_leader,
                        Ec_distinction = (E.Ec_distinction == 1 ? "全仕向" : (E.Ec_distinction == 2 ? "部管" : (E.Ec_distinction == 3 ? "内部" : "技术"))),
                        isConfirm = (E.isConfirm == 0 ? "制二不管理" : "制二管理"),
                        isModifysop = (E.isModifysop == 0 ? "不更新" : "需更新"),
                        Ec_status = (E.Ec_status.Contains("Work in Process") ? "WIP" : (E.Ec_status.Contains("Change at P.P.") ? "Change" : E.Ec_status)),
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.CreateTime,


                    }); ;
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
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            //catch (Exception Message)
            //{
            //    Alert.ShowInTop("异常3:" + Message);

            //}
            catch (DbEntityValidationException ex)
            {
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("异常3:" + msg);
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
            //CheckPowerWithWindowField("CoreProbadEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreEcENGDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            // 设置LinkButtonField的点击客户端事件
            // LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;

            CheckPowerWithLinkButtonField("CoreEcENGEdit", Grid1, "editField");
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
        protected void Grid1_RowDoubleClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            //labResult.Text = keys[0].ToString();
            PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/dept/view_desc.aspx?Ec_no=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
        }
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];

                //selID = Convert.ToInt32(keys[0].ToString());
                string ss = keys[0].ToString() + ',' + keys[1].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/EC/ec_eng_edit.aspx?Ec_no=" + ss + "&type=1") + Window1.GetMaximizeReference());
                //选中ID
                //string DelID = (Grid1.DataKeyNames[e.RowIndex][0]).ToString();//GetSelectedDataKeyID(Grid2);
            }

            //查询选中行ID
            Guid del_ID =Guid.Parse( GetSelectedDataKeyGUID(Grid1));
            //判断权限
            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_Ec current = DB.Pp_Ecs.Find(del_ID);
                string Contectext = current.Ec_no; 
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del技术* " + Contectext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变删除标记", OperateNotes);

                //var Ec_n = DB.Pp_EcSubs.Where(c => c.GUID == (del_ID));

                //foreach (var Pp_EcSubs in Ec_n)
                //{
                //    Pp_EcSubs.isDelete = 1;
                //    Pp_EcSubs.Endtag = 1;
                //    Pp_EcSubs.Modifier = GetIdentityName();
                //    Pp_EcSubs.ModifyTime = DateTime.Now;
                //}
                //DB.SaveChanges();

                current.isDelete = 1;
                current.Modifier = GetIdentityName();
                current.ModifyTime = DateTime.Now;
                DB.SaveChanges();

                //批量更新删除标记

                var Pp_EcSub = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Contectext));
                foreach (var Pp_Ecs in Pp_EcSub)
                {
                    Pp_Ecs.isDelete = 1;
                    Pp_Ecs.Modifier = GetIdentityName();
                    Pp_Ecs.ModifyTime = DateTime.Now;
                }
                DB.SaveChanges();


                //DB.Pp_Ecs
                //        .Where(t => t.Ec_no == Contectext)
                //        .Update(t => new Pp_EcSub { isDelete = 1 });
                //DB.SaveChanges();


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
        #region ExportExcel
        #endregion


    }
}
