using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.SOP
{
    public partial class pe_asy : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSopView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static String mysql, ecnno, strecnno, tracestr;
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
                if (rbtnFirstAuto.Checked)
                {
                    var q = from a in DB.Pp_Ec_Sops
                                //join b in DB.Pp_Ec_Sops on a.Ec_no equals b.Ec_no
                            where a.IsDeleted == 0
                            where a.Ec_pengadate == ""
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            orderby a.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_model,
                                a.Ec_issuedate,
                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, }).ToList().Distinct();

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
                else if (rbtnSecondAuto.Checked)
                {
                    var q = from a in DB.Pp_Ec_Sops
                                //join b in DB.Pp_Ec_Sops on a.Ec_no equals b.Ec_no
                            where a.IsDeleted == 0
                            where a.Ec_pengadate != ""
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            orderby a.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_model,
                                a.Ec_issuedate,
                            };

                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, }).ToList().Distinct();

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
                else if (rbtnThirdAuto.Checked)
                {
                    var q = from a in DB.Pp_Ec_Sops
                                //join b in DB.Pp_Ec_Sops on a.Ec_no equals b.Ec_no
                            where a.IsDeleted == 0
                            //where a.Ec_qadate == "" || a.Ec_qadate == null
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            orderby a.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                a.Ec_model,
                                a.Ec_issuedate,
                            };
                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    var qs = q.Select(E => new { E.Ec_no, }).ToList().Distinct();
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
            int classId = Convert.ToInt32(Grid1.DataKeys[Grid1.SelectedRowIndex][0]);

            int roleID = GetSelectedDataKeyID(Grid1);

            if (!String.IsNullOrEmpty(ecnno))
            {
                if (rbtnFirstAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ec_Sops
                              where a.IsDeleted == 0
                              where a.Ec_pengadate == ""
                              //where a.Ec_distinction != 4
                              //where a.Remark.Contains("OK") == false
                              orderby a.Ec_entrydate descending
                              select new
                              {
                                  Ec_pengadate = a.Ec_pengadate == null ? "" : a.Ec_pengadate,
                                  Ec_pengpdate = a.Ec_pengpdate == null ? "" : a.Ec_pengpdate,
                                  a.Ec_model,
                                  a.Ec_no,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_pengadate,
                        E.Ec_pengpdate,
                        E.Ec_model,
                        E.Ec_no,
                    }).Distinct();

                    q = q.Where(u => u.Ec_no.Contains(ecnno));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                else if (rbtnSecondAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ec_Sops
                              where a.IsDeleted == 0
                              where a.Ec_pengadate != ""
                              //where a.Ec_distinction != 4
                              //where a.Remark.Contains("OK") == false
                              orderby a.Ec_entrydate descending
                              select new
                              {
                                  Ec_pengadate = a.Ec_pengadate == null ? "" : a.Ec_pengadate,
                                  Ec_pengpdate = a.Ec_pengpdate == null ? "" : a.Ec_pengpdate,
                                  a.Ec_model,
                                  a.Ec_no,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_pengadate,
                        E.Ec_pengpdate,
                        E.Ec_model,
                        E.Ec_no,
                    }).Distinct();

                    q = q.Where(u => u.Ec_no.Contains(ecnno));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                else if (rbtnThirdAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ec_Sops
                                  //join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                              orderby a.Ec_entrydate descending
                              select new
                              {
                                  Ec_pengadate = a.Ec_pengadate == null ? "" : a.Ec_pengadate,
                                  Ec_pengpdate = a.Ec_pengpdate == null ? "" : a.Ec_pengpdate,
                                  a.Ec_model,
                                  a.Ec_no,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_pengadate,
                        E.Ec_pengpdate,
                        E.Ec_model,
                        E.Ec_no,
                    }).Distinct();

                    q = q.Where(u => u.Ec_no.Contains(ecnno));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
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
            //ecnno = "";
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
            ecnno = keys[1].ToString();
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
            CheckPowerWithLinkButtonField("CoreSopEdit", Grid2, "editField");
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
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            //参数传递
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
                tracestr = tracestr + keys[1].ToString();
            }

            PageContext.RegisterStartupScript(Window3.GetShowReference("~/Lf_Manufacturing/SOP/pe_asy_edit.aspx?Ec_no=" + tracestr + "&type=1"));
            //选中ID
            //int DelID = Convert.ToInt32(Grid2.DataKeys[e.RowIndex][0]);//GetSelectedDataKeyID(Grid2);

            BindGrid2();
        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        #endregion Grid2 Events

        protected void Window3_Close(object sender, EventArgs e)
        {
            BindGrid1();
            BindGrid2();
        }
    }
}