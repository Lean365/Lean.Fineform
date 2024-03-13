using FineUIPro;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_pm : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcPMView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static String mysql, strecnno, tracestr, strID, strEc_no, strEc_model, strEc_bomitem, strEc_olditem, strEc_newitem;
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
        }

        private void BindGrid1()
        {
            try
            {
                if (rbtnFirstAuto.Checked == true)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDeleted == 0
                            where !string.IsNullOrEmpty(b.Ec_purdate)//b.Ec_purdate != "" || b.Ec_purdate != null

                            where string.IsNullOrEmpty(b.Ec_pmcdate) //== ""  || b.Ec_pmcdate == null
                            //where a.Ec_distinction != 4
                            where b.isConfirm != 0
                            //where a.Remark.Contains("OK") == false
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_distinction,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).Distinct();
                    //qs = qs.OrderByDescending(u => u.Ec_issuedate);

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(qs);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, qs);
                    //}
                    Grid1.DataSource = qs;
                    Grid1.DataBind();

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] WHERE Ec_distinction<>3 ";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + " Where [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
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
                    //Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    //q = SortAndPage<INVMB>(q, Grid1);

                    //Grid1.DataSource = mydr;
                    //Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
                }
                if (rbtnSecondAuto.Checked == true)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDeleted == 0
                            where !string.IsNullOrEmpty(b.Ec_purdate)// != ""|| b.Ec_purdate != null
                            where !string.IsNullOrEmpty(b.Ec_pmcdate)// != ""  || b.Ec_pmcdate != null
                            //where a.Ec_distinction != 4
                            where b.isConfirm != 0
                            //where a.Remark.Contains("OK") == false
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_distinction,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(qs);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, qs);
                    //}
                    Grid1.DataSource = qs;
                    Grid1.DataBind();

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] WHERE Ec_distinction<>3 ";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + " Where [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
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
                    //Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    //q = SortAndPage<INVMB>(q, Grid1);

                    //Grid1.DataSource = mydr;
                    //Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
                }
                if (rbtnThirdAuto.Checked == true)
                {
                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            where b.isDeleted == 0
                            //where b.Ec_purdate != ""|| b.Ec_purdate != null
                            //where b.Ec_pmcdate != ""|| b.Ec_pmcdate != null
                            //where a.Ec_distinction != 4
                            where b.isConfirm != 0
                            //where a.Remark.Contains("OK") == false
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_distinction,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(qs);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, qs);
                    //}
                    Grid1.DataSource = qs;
                    Grid1.DataBind();

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] WHERE Ec_distinction<>3 ";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + " Where [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
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
                    //Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    //q = SortAndPage<INVMB>(q, Grid1);

                    //Grid1.DataSource = mydr;
                    //Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
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

        private void BindGrid2()
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Grid2.DataSource = "";

                Grid2.DataBind();
                return;
            }

            if (!String.IsNullOrEmpty(strecnno))
            {
                if (rbtnFirstAuto.Checked)
                {
                    var sub = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              join c in DB.Pp_SapMaterials on b.Ec_bomsubitem equals c.D_SAP_ZCA1D_Z002
                              where b.isDeleted == 0
                              where a.Ec_no.Contains(strecnno)
                              where !string.IsNullOrEmpty(b.Ec_purdate)
                              where string.IsNullOrEmpty(b.Ec_pmcdate)//==""|| E.Ec_pmcdate == null
                              orderby b.Ec_entrydate
                              select new
                              {
                                  a.Ec_documents,
                                  Ec_pmcdate = b.Ec_pmcdate == null ? "" : b.Ec_pmcdate,
                                  b.Ec_pmclot,
                                  b.Ec_pmcmemo,
                                  b.Ec_pmcnote,
                                  b.Ec_model,
                                  b.Ec_bomitem,
                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem
                              };
                    //查询LINQ去重复
                    var q = sub.Select(E => new
                    {
                        E.Ec_documents,
                        Ec_pmcdate = E.Ec_pmcdate == null ? "" : E.Ec_pmcdate,
                        E.Ec_pmclot,
                        E.Ec_bomitem,
                        E.Ec_pmcmemo,
                        E.Ec_pmcnote,
                        E.Ec_model,
                        //E.Ec_newitem,
                        E.Ec_no,
                        //E.Ec_olditem
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
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
                if (rbtnSecondAuto.Checked)
                {
                    var sub = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              join c in DB.Pp_SapMaterials on b.Ec_bomsubitem equals c.D_SAP_ZCA1D_Z002
                              where b.isDeleted == 0
                              where a.Ec_no.Contains(strecnno)
                              where !string.IsNullOrEmpty(b.Ec_purdate)
                              where !string.IsNullOrEmpty(b.Ec_pmcdate)//==""|| E.Ec_pmcdate == null
                              orderby b.Ec_entrydate
                              select new
                              {
                                  a.Ec_documents,
                                  Ec_pmcdate = b.Ec_pmcdate == null ? "" : b.Ec_pmcdate,
                                  b.Ec_pmclot,
                                  b.Ec_pmcmemo,
                                  b.Ec_pmcnote,
                                  b.Ec_model,
                                  b.Ec_bomitem,
                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem
                              };
                    //查询LINQ去重复
                    var q = sub.Select(E => new
                    {
                        E.Ec_documents,
                        Ec_pmcdate = E.Ec_pmcdate == null ? "" : E.Ec_pmcdate,
                        E.Ec_pmclot,
                        E.Ec_bomitem,
                        E.Ec_pmcmemo,
                        E.Ec_pmcnote,
                        E.Ec_model,
                        //E.Ec_newitem,
                        E.Ec_no,
                        //E.Ec_olditem
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
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
                if (rbtnThirdAuto.Checked)
                {
                    var sub = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              join c in DB.Pp_SapMaterials on b.Ec_bomsubitem equals c.D_SAP_ZCA1D_Z002
                              where a.Ec_no.Contains(strecnno)
                              where b.isDeleted == 0
                              //where !string.IsNullOrEmpty(b.Ec_purdate)
                              //where string.IsNullOrEmpty(b.Ec_pmcdate)//==""|| E.Ec_pmcdate == null
                              orderby b.Ec_entrydate
                              select new
                              {
                                  a.Ec_documents,
                                  Ec_pmcdate = b.Ec_pmcdate == null ? "" : b.Ec_pmcdate,
                                  b.Ec_pmclot,
                                  b.Ec_pmcmemo,
                                  b.Ec_pmcnote,
                                  b.Ec_model,
                                  b.Ec_bomitem,

                                  b.Ec_newitem,
                                  b.Ec_no,
                                  b.Ec_olditem
                              };
                    //查询LINQ去重复
                    var q = sub.Select(E => new
                    {
                        E.Ec_documents,
                        Ec_pmcdate = E.Ec_pmcdate == null ? "" : E.Ec_pmcdate,
                        E.Ec_pmclot,
                        E.Ec_bomitem,

                        E.Ec_pmcmemo,
                        E.Ec_pmcnote,
                        E.Ec_model,
                        //E.Ec_newitem,
                        E.Ec_no,
                        //E.Ec_olditem
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (GridHelper.GetTotalCount(q) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);
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
            }
            else

            {
                Grid2.DataSource = "";

                Grid2.DataBind();
            }
        }

        #endregion Page_Load

        #region Events

        protected void ttbSearchEcnsub_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.ShowTrigger1 = true;
            BindGrid1();
            BindGrid2();
        }

        protected void ttbSearchEcnsub_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.Text = String.Empty;
            ttbSearchEcnsub.ShowTrigger1 = false;
            BindGrid1();
            BindGrid2();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex1 = Grid1.PageIndex;
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid1.PageIndex > Grid1.PageCount - 1)
            {
                Grid1.PageIndex = Grid1.PageCount - 1;
            }

            BindGrid1();
            int pageindex2 = Grid1.PageIndex;
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid2.PageIndex > Grid2.PageCount - 1)
            {
                Grid2.PageIndex = Grid2.PageCount - 1;
            }

            BindGrid2();
        }

        #endregion Events

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
        }

        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];

            strecnno = keys[1].ToString();
            BindGrid2();
        }

        #endregion Grid1 Events

        #region Grid2 Events

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreBudgetDelete", Grid2, "deleteField");

            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript(Grid1);

            CheckPowerWithLinkButtonField("CoreEcPMEdit", Grid2, "editField");
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
            //参数传递
            var q = from a in DB.Pp_Ecs
                    where a.Ec_no.Contains(strecnno)
                    select new
                    {
                        a.Ec_distinction,
                    };

            var qs = q.ToList();
            //ID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem
            object[] keys = Grid2.DataKeys[e.RowIndex];

            if (keys[0] == null)
            {
                tracestr = ",";
            }
            else
            {
                tracestr = keys[0].ToString() + ",";
            }

            if (keys[1] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[1].ToString() + ",";
            }
            if (keys[2] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[2].ToString() + ",";
            }
            if (keys[3] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[3].ToString() + ",";
            }
            if (keys[4] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[4].ToString() + ",";
            }
            if (keys[5] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[5].ToString() + "," + qs[0].Ec_distinction.ToString();
            }

            //selID = Convert.ToInt32(keys[0].ToString());

            //string bitem= keys[2].ToString();
            PageContext.RegisterStartupScript(Window3.GetShowReference("~/Lf_Manufacturing/EC/ec_pm_edit.aspx?Ec_no=" + tracestr + " & type=1") + Window3.GetMaximizeReference());
            //选中ID
            //int DelID = Convert.ToInt32(Grid2.DataKeys[e.RowIndex][0]);//GetSelectedDataKeyID(Grid2);

            //ecnno = keys[1].ToString();

            BindGrid2();
        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
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
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            strecnno = "";
        }

        #endregion Grid2 Events

        protected void Window3_Close(object sender, EventArgs e)
        {
            BindGrid1();
            BindGrid2();
        }
    }
}