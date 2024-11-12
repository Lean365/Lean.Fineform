using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_Mm_outbound : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcMatView";
            }
        }

        #endregion ViewPower

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
            CheckPowerWithButton("CoreEcMatEdit", btnChangeOutboundItems);

            ResolveEnableStatusButtonForGrid(btnMmManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnMmNoManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnPcbaManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnPcbaNoManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnAssyManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnAssyNoManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnCommonManage, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnCommonNoManage, Grid1, true);

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindDdlModel();
            BindGrid();
        }

        private void ResolveEnableStatusButtonForGrid(MenuButton btn, Grid grid, bool enabled)
        {
            string enabledStr = "需确认";
            if (!enabled)
            {
                enabledStr = "不确认";
            }
            btn.OnClientClick = grid.GetNoSelectionAlertInParentReference("请至少应该选择一项记录！");
            btn.ConfirmText = String.Format("确定要{1}选中的<span class=\"highlight\"><script>{0}</script></span>项记录吗？", grid.GetSelectedCountReference(), enabledStr);
            btn.ConfirmTarget = FineUIPro.Target.Top;
        }

        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                Alert.ShowInTop("物料管理状态已变更！", "处理结果", MessageBoxIcon.Information);
                // 非AJAX回发
                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else if (e.EventArgument == "Confirm_Cancel")
            {
                // AJAX回发
                Alert.ShowInTop("将返回编辑页面！");
            }
        }

        private void BindGrid()
        {
            try
            {
                string searchText = ttbSearchMessage.Text.Trim();
                if (this.rblAuto.SelectedValue == "1")
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             //where a.Ec_qadate== ""
                             //where b.Ec_distinction == 1
                             where b.IsDeleted == 0
                             //where b.IsManage == 1
                             orderby b.IsMmManage, b.IsPcbaManage, b.IsAssyManage
                             select new
                             {
                                 b.GUID,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : (a.Ec_distinction == 2 ? "部管" : (a.Ec_distinction == 3 ? "内部" : (a.Ec_distinction == 4 ? "技术" : "无")))),
                                 a.Ec_entrydate,
                                 a.Ec_no,
                                 b.Ec_model,
                                 a.Ec_documents,
                                 IsManage = (b.IsManage == 0 ? "按部门" : (b.IsManage == 1 ? "全部门" : (b.IsManage == 2 ? "不管理" : "按部门"))),
                                 IsMmManage = (b.IsMmManage == 0 ? "无关" : (b.IsMmManage == 1 ? "有关" : "无关")),
                                 IsPcbaManage = (b.IsPcbaManage == 0 ? "无关" : (b.IsPcbaManage == 1 ? "有关" : "无关")),
                                 IsAssyManage = (b.IsAssyManage == 0 ? "无关" : (b.IsAssyManage == 1 ? "有关" : "无关")),
                                 b.Ec_newitem,
                                 b.Ec_newtext,
                                 b.Ec_procurement,
                                 b.Ec_location,
                                 b.IsCheck,
                                 b.Ec_bomitem,
                                 b.Ec_bomsubitem,
                                 b.Ec_olditem,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_bomitem.Contains(searchText) || u.Ec_bomsubitem.Contains(searchText) || u.Ec_olditem.Contains(searchText) || u.Ec_newitem.Contains(searchText));
                    }

                    //if (GetIdentityName() != "admin")
                    //{
                    //    q = q.Where(u => u.Name != "admin");
                    //}
                    // 过滤启用状态
                    //if (rblEnableStatus.SelectedValue != "all")
                    //{
                    //    q = q.Where(u => u.IsManage.ToString() == (rblEnableStatus.SelectedValue == "mm" ? "1" : rblEnableStatus.SelectedValue == "p2d" ? "1" : "0"));
                    //}

                    //where b.Ec_pmcdate == "" || b.Ec_pmcdate == null
                    //q = q.Where(u => u.Ec_qadate == ""|| u.Ec_qadate == null);
                    q = q.Where(u => u.Ec_no == (Ec_no.SelectedItem.Text));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

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
                }
                if (this.rblAuto.SelectedValue == "2")
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             //where a.Ec_qadate.ToString() == "" || a.Ec_qadate == null
                             //where b.IsManage == 1
                             where b.IsDeleted == 0
                             //where a.Ec_qadate == ""
                             where b.IsMmManage == 1
                             select new
                             {
                                 b.GUID,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : (a.Ec_distinction == 2 ? "部管" : (a.Ec_distinction == 3 ? "内部" : (a.Ec_distinction == 4 ? "技术" : "无")))),
                                 a.Ec_entrydate,
                                 a.Ec_no,
                                 b.Ec_model,
                                 a.Ec_documents,
                                 IsManage = (b.IsManage == 0 ? "按部门" : (b.IsManage == 1 ? "全部门" : (b.IsManage == 2 ? "不管理" : "按部门"))),
                                 IsMmManage = (b.IsMmManage == 0 ? "无关" : (b.IsMmManage == 1 ? "有关" : "无关")),
                                 IsPcbaManage = (b.IsPcbaManage == 0 ? "无关" : (b.IsPcbaManage == 1 ? "有关" : "无关")),
                                 IsAssyManage = (b.IsAssyManage == 0 ? "无关" : (b.IsAssyManage == 1 ? "有关" : "无关")),
                                 b.Ec_newitem,
                                 b.Ec_newtext,
                                 b.Ec_procurement,
                                 b.Ec_location,
                                 b.IsCheck,
                                 b.Ec_bomitem,
                                 b.Ec_bomsubitem,
                                 b.Ec_olditem,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_bomitem.Contains(searchText) || u.Ec_bomsubitem.Contains(searchText) || u.Ec_olditem.Contains(searchText) || u.Ec_newitem.Contains(searchText));
                    }

                    //if (GetIdentityName() != "admin")
                    //{
                    //    q = q.Where(u => u.Name != "admin");
                    //}
                    // 过滤启用状态
                    //if (rblEnableStatus.SelectedValue != "all")
                    //{
                    //    q = q.Where(u => u.IsManage.ToString() == (rblEnableStatus.SelectedValue == "mm" ? "1" : rblEnableStatus.SelectedValue == "p2d" ? "1" : "0"));
                    //}

                    //where b.Ec_pmcdate == "" || b.Ec_pmcdate == null
                    //q = q.Where(u => u.Ec_qadate == ""|| u.Ec_qadate == null);
                    q = q.Where(u => u.Ec_no == (Ec_no.SelectedItem.Text));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

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
                }
                if (this.rblAuto.SelectedValue == "3")
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             //where a.Ec_qadate.ToString() == "" || a.Ec_qadate == null
                             where b.IsPcbaManage == 1
                             where b.IsDeleted == 0
                             //where a.Ec_qadate == ""
                             select new
                             {
                                 b.GUID,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : (a.Ec_distinction == 2 ? "部管" : (a.Ec_distinction == 3 ? "内部" : (a.Ec_distinction == 4 ? "技术" : "无")))),
                                 a.Ec_entrydate,
                                 a.Ec_no,
                                 b.Ec_model,
                                 a.Ec_documents,
                                 IsManage = (b.IsManage == 0 ? "按部门" : (b.IsManage == 1 ? "全部门" : (b.IsManage == 2 ? "不管理" : "按部门"))),
                                 IsMmManage = (b.IsMmManage == 0 ? "无关" : (b.IsMmManage == 1 ? "有关" : "无关")),
                                 IsPcbaManage = (b.IsPcbaManage == 0 ? "无关" : (b.IsPcbaManage == 1 ? "有关" : "无关")),
                                 IsAssyManage = (b.IsAssyManage == 0 ? "无关" : (b.IsAssyManage == 1 ? "有关" : "无关")),
                                 b.Ec_newitem,
                                 b.Ec_newtext,
                                 b.Ec_procurement,
                                 b.Ec_location,
                                 b.IsCheck,
                                 b.Ec_bomitem,
                                 b.Ec_bomsubitem,
                                 b.Ec_olditem,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_bomitem.Contains(searchText) || u.Ec_bomsubitem.Contains(searchText) || u.Ec_olditem.Contains(searchText) || u.Ec_newitem.Contains(searchText));
                    }

                    //if (GetIdentityName() != "admin")
                    //{
                    //    q = q.Where(u => u.Name != "admin");
                    //}
                    // 过滤启用状态
                    //if (rblEnableStatus.SelectedValue != "all")
                    //{
                    //    q = q.Where(u => u.IsManage.ToString() == (rblEnableStatus.SelectedValue == "mm" ? "1" : rblEnableStatus.SelectedValue == "p2d" ? "1" : "0"));
                    //}

                    //where b.Ec_pmcdate == "" || b.Ec_pmcdate == null
                    //q = q.Where(u => u.Ec_qadate == ""|| u.Ec_qadate == null);
                    q = q.Where(u => u.Ec_no == (Ec_no.SelectedItem.Text));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

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
                }
                if (this.rblAuto.SelectedValue == "4")
                {
                    var q =
                            (from a in DB.Pp_Ecs
                             join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                             //where a.Ec_qadate.ToString() == "" || a.Ec_qadate == null
                             where b.IsAssyManage == 1
                             where b.IsDeleted == 0
                             //where a.Ec_qadate == ""
                             select new
                             {
                                 b.GUID,
                                 a.Ec_leader,
                                 Ec_distinction = (a.Ec_distinction == 1 ? "全仕向" : (a.Ec_distinction == 2 ? "部管" : (a.Ec_distinction == 3 ? "内部" : (a.Ec_distinction == 4 ? "技术" : "无")))),
                                 a.Ec_entrydate,
                                 a.Ec_no,
                                 b.Ec_model,
                                 a.Ec_documents,
                                 IsManage = (b.IsManage == 0 ? "按部门" : (b.IsManage == 1 ? "全部门" : (b.IsManage == 2 ? "不管理" : "按部门"))),
                                 IsMmManage = (b.IsMmManage == 0 ? "无关" : (b.IsMmManage == 1 ? "有关" : "无关")),
                                 IsPcbaManage = (b.IsPcbaManage == 0 ? "无关" : (b.IsPcbaManage == 1 ? "有关" : "无关")),
                                 IsAssyManage = (b.IsAssyManage == 0 ? "无关" : (b.IsAssyManage == 1 ? "有关" : "无关")),
                                 b.Ec_newitem,
                                 b.Ec_newtext,
                                 b.Ec_procurement,
                                 b.Ec_location,
                                 b.IsCheck,
                                 b.Ec_bomitem,
                                 b.Ec_bomsubitem,
                                 b.Ec_olditem,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_model.Contains(searchText) || u.Ec_bomitem.Contains(searchText) || u.Ec_bomsubitem.Contains(searchText) || u.Ec_olditem.Contains(searchText) || u.Ec_newitem.Contains(searchText));
                    }

                    //if (GetIdentityName() != "admin")
                    //{
                    //    q = q.Where(u => u.Name != "admin");
                    //}
                    // 过滤启用状态
                    //if (rblEnableStatus.SelectedValue != "all")
                    //{
                    //    q = q.Where(u => u.IsManage.ToString() == (rblEnableStatus.SelectedValue == "mm" ? "1" : rblEnableStatus.SelectedValue == "p2d" ? "1" : "0"));
                    //}

                    //where b.Ec_pmcdate == "" || b.Ec_pmcdate == null
                    //q = q.Where(u => u.Ec_qadate == ""|| u.Ec_qadate == null);
                    q = q.Where(u => u.Ec_no == (Ec_no.SelectedItem.Text));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

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

        private void BindDdlModel()
        {
            var q = from a in DB.Pp_Ecs
                    join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                    //where a.Ec_distinction != 4
                    //where a.Ec_qadate == ""
                    orderby a.Ec_entrydate descending

                    select new
                    {
                        a.Ec_no
                    };

            var qs = q.Select(E => new { E.Ec_no, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            Ec_no.DataSource = qs;
            Ec_no.DataTextField = "Ec_no";
            Ec_no.DataValueField = "Ec_no";
            Ec_no.DataBind();
            this.Ec_no.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
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

        protected void btnCommonNoManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！所有选物料确定变更成<全部门>都不管理状态吗？",
                           "警告",
                           MessageBoxIcon.Question,
                           PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                           PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(0);
        }

        protected void btnCommonManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更物料管理状态吗？",
                           String.Empty,
                           MessageBoxIcon.Question,
                           PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                           PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(1);
        }

        protected void btnMmManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更部管课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Warning,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(2);
        }

        protected void btnMmNoManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更部管课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Warning,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(3);
        }

        protected void btnPcbaManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更制二课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Question,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(4);
        }

        protected void btnPcbaNoManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更制二课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Question,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(5);
        }

        protected void btnAssyManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更制一课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Question,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(6);
        }

        protected void btnAssyNoManage_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！后续部门已输入还是确定要变更制一课管理状态吗？",
                        String.Empty,
                        MessageBoxIcon.Question,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
            SetSelectedItemsEnableStatus(7);
        }

        private void SetSelectedItemsEnableStatus(byte IsManage)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreEcMatEdit"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<string> ListGuid = GetSelectedDataKeyGUIDs(Grid1);

            // 执行数据库操作
            //DB.Adm_Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => u.Enabled = enabled);
            //DB.SaveChanges();
            var q = (from a in ListGuid
                     select a).ToList();
            if (q.Any())
            {
                for (int i = 0; i < q.Count(); i++)
                {
                    UpdateEcSubs(Guid.Parse(q[i].ToString()), IsManage);
                }
            }

            // 重新绑定表格
            BindGrid();
        }

        private void UpdateEcSubs(Guid strGuid, byte IsManages)
        {
            //批量更新管理标记
            if (IsManages == 0)//不管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList0 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = 0,
                                                   IsMmManage = 0,
                                                   IsPcbaManage = 0,
                                                   IsAssyManage = 0,

                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,
                                                   //    //生管
                                                   Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_pmclot = "自然切换",
                                                   Ec_pmcmemo = "自然切换",
                                                   Ec_pmcnote = "自然切换",
                                                   Ec_bstock = 0,
                                                   pmcModifier = GetIdentityName(),
                                                   pmcModifyDate = DateTime.Now,
                                                   //    //部管
                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_mmlot = "自然切换",
                                                   Ec_mmlotno = "4400000",
                                                   Ec_mmnote = "自然切换",
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,
                                                   //    //采购
                                                   Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_purorder = "4300000000",
                                                   Ec_pursupplier = "H200000",
                                                   Ec_purnote = "自然切换",
                                                   ppModifier = GetIdentityName(),
                                                   ppModifyDate = DateTime.Now,
                                                   //    //受检
                                                   Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_iqcorder = "4300000000",
                                                   Ec_iqcnote = "自然切换",
                                                   iqcModifier = GetIdentityName(),
                                                   iqcModifyDate = DateTime.Now,
                                                   //    //制一
                                                   Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_p1dline = "自然切换",
                                                   Ec_p1dlot = "自然切换",
                                                   Ec_p1dnote = "自然切换",
                                                   p1dModifier = GetIdentityName(),
                                                   p1dModifyDate = DateTime.Now,
                                                   //    //制二
                                                   Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_p2dlot = "自然切换",
                                                   Ec_p2dnote = "自然切换",
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,
                                                   //    //品管
                                                   Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_qalot = "自然切换",
                                                   Ec_qanote = "自然切换",
                                                   qaModifier = GetIdentityName(),
                                                   qaModifyDate = DateTime.Now,
                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList0);
                DB.BulkSaveChanges();
            }
            if (IsManages == 1)//共通管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList1 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = 1,
                                                   IsMmManage = 1,
                                                   IsPcbaManage = 1,
                                                   IsAssyManage = 1,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,
                                                   Ec_pmcdate = "",
                                                   Ec_pmclot = "",
                                                   Ec_pmcmemo = "",
                                                   Ec_pmcnote = "",
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = "",
                                                   Ec_p2dlot = "",
                                                   Ec_p2dnote = "",
                                                   p2dModifier = item.p2dModifier,
                                                   p2dModifyDate = item.p2dModifyDate,

                                                   Ec_mmdate = "",
                                                   Ec_mmlot = "",
                                                   Ec_mmlotno = "",
                                                   Ec_mmnote = "",
                                                   mmModifier = item.mmModifier,
                                                   mmModifyDate = item.mmModifyDate,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,
                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,
                                                   Ec_p1ddate = "",
                                                   Ec_p1dline = "",
                                                   Ec_p1dlot = "",
                                                   Ec_p1dnote = "",
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,
                                                   Ec_qadate = "",
                                                   Ec_qalot = "",
                                                   Ec_qanote = "",
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,
                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList1);
                DB.BulkSaveChanges();
            }
            if (IsManages == 2)//部管管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,
                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList2 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = 1,
                                                   IsMmManage = 1,
                                                   IsPcbaManage = item.IsPcbaManage,
                                                   IsAssyManage = item.IsAssyManage,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                   p2dModifier = item.p2dModifier,
                                                   p2dModifyDate = item.p2dModifyDate,

                                                   Ec_mmdate = "",
                                                   Ec_mmlot = "",
                                                   Ec_mmlotno = "",
                                                   Ec_mmnote = "",
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,
                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,
                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                   Ec_p1dline = item.Ec_p1dline,
                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,
                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,
                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList2);
                DB.BulkSaveChanges();
            }
            if (IsManages == 3)//部管不管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList3 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = item.IsManage,
                                                   IsMmManage = 0,
                                                   IsPcbaManage = item.IsPcbaManage,
                                                   IsAssyManage = item.IsAssyManage,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,

                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_mmlot = "与部管无关",
                                                   Ec_mmlotno = "与部管无关",
                                                   Ec_mmnote = "与部管无关",
                                                   mmModifier = item.mmModifier,
                                                   mmModifyDate = item.mmModifyDate,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                   Ec_p1dline = item.Ec_p1dline,
                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList3);
                DB.BulkSaveChanges();
            }
            if (IsManages == 4)//制二管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList4 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = 1,
                                                   IsMmManage = item.IsMmManage,
                                                   IsPcbaManage = 1,
                                                   IsAssyManage = item.IsAssyManage,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = "",
                                                   Ec_p2dlot = "",
                                                   Ec_p2dnote = "",
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,

                                                   Ec_mmdate = item.Ec_mmdate,
                                                   Ec_mmlot = item.Ec_mmlot,
                                                   Ec_mmlotno = item.Ec_mmlotno,
                                                   Ec_mmnote = item.Ec_mmnote,
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                   Ec_p1dline = item.Ec_p1dline,
                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList4);
                DB.BulkSaveChanges();
            }
            if (IsManages == 5)//制二不管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList5 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = item.IsManage,
                                                   IsMmManage = item.IsMmManage,
                                                   IsPcbaManage = 0,
                                                   IsAssyManage = item.IsAssyManage,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_p2dlot = "与制二无关",
                                                   Ec_p2dnote = "与制二无关",
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,

                                                   Ec_mmdate = item.Ec_mmdate,
                                                   Ec_mmlot = item.Ec_mmlot,
                                                   Ec_mmlotno = item.Ec_mmlotno,
                                                   Ec_mmnote = item.Ec_mmnote,
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                   Ec_p1dline = item.Ec_p1dline,
                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList5);
                DB.BulkSaveChanges();
            }
            if (IsManages == 6)//制一课管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList6 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = item.IsManage,
                                                   IsMmManage = item.IsMmManage,
                                                   IsPcbaManage = item.IsPcbaManage,
                                                   IsAssyManage = 1,
                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,

                                                   Ec_mmdate = item.Ec_mmdate,
                                                   Ec_mmlot = item.Ec_mmlot,
                                                   Ec_mmlotno = item.Ec_mmlotno,
                                                   Ec_mmnote = item.Ec_mmnote,
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   Ec_p1ddate = "",
                                                   Ec_p1dline = "",
                                                   Ec_p1dlot = "",
                                                   Ec_p1dnote = "",
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList6);
                DB.BulkSaveChanges();
            }
            if (IsManages == 7)//制一课不管理
            {
                var q = (from a in DB.Pp_Ec_Subs
                         where a.GUID.CompareTo(strGuid) == 0
                         where a.IsDeleted == 0
                         select new
                         {
                             a.GUID,
                             a.Ec_no,
                             a.Ec_model,
                             a.Ec_bomitem,
                             a.Ec_bomsubitem,
                             a.Ec_olditem,
                             a.Ec_oldtext,
                             a.Ec_oldqty,
                             a.Ec_oldset,
                             a.Ec_newitem,
                             a.Ec_newtext,
                             a.Ec_newqty,
                             a.Ec_newset,
                             a.Ec_bomno,
                             a.Ec_change,
                             a.Ec_local,
                             a.Ec_note,
                             a.Ec_process,
                             a.Ec_procurement,
                             a.Ec_location,
                             a.IsCheck,
                             a.IsManage,
                             a.IsMmManage,
                             a.IsPcbaManage,
                             a.IsAssyManage,
                             a.Ec_eol,
                             a.Ec_bomdate,
                             a.Ec_entrydate,
                             a.Ec_pmcdate,
                             a.Ec_pmclot,
                             a.Ec_pmcmemo,
                             a.Ec_pmcnote,
                             a.Ec_bstock,
                             a.pmcModifier,
                             a.pmcModifyDate,
                             a.Ec_p2ddate,
                             a.Ec_p2dlot,
                             a.Ec_p2dnote,
                             a.p2dModifier,
                             a.p2dModifyDate,
                             a.Ec_mmdate,
                             a.Ec_mmlot,
                             a.Ec_mmlotno,
                             a.Ec_mmnote,
                             a.mmModifier,
                             a.mmModifyDate,
                             a.Ec_purdate,
                             a.Ec_purorder,
                             a.Ec_pursupplier,
                             a.Ec_purnote,
                             a.ppModifier,
                             a.ppModifyDate,
                             a.Ec_iqcdate,
                             a.Ec_iqcorder,
                             a.Ec_iqcnote,
                             a.iqcModifier,
                             a.iqcModifyDate,
                             a.Ec_p1ddate,
                             a.Ec_p1dline,
                             a.Ec_p1dlot,
                             a.Ec_p1dnote,
                             a.p1dModifier,
                             a.p1dModifyDate,

                             a.Ec_qadate,
                             a.Ec_qalot,
                             a.Ec_qanote,
                             a.qaModifier,
                             a.qaModifyDate,
                             a.UDF01,
                             a.UDF02,
                             a.UDF03,
                             a.UDF04,
                             a.UDF05,
                             a.UDF06,
                             a.UDF51,
                             a.UDF52,
                             a.UDF53,
                             a.UDF54,
                             a.UDF55,
                             a.UDF56,
                             a.IsDeleted,
                             a.Remark,

                             a.Creator,
                             a.CreateDate,
                             a.Modifier,
                             a.ModifyDate,
                         }).ToList();
                List<Pp_Ec_Sub> UpdateList7 = (from item in q
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = item.IsManage,
                                                   IsMmManage = item.IsMmManage,
                                                   IsPcbaManage = item.IsPcbaManage,
                                                   IsAssyManage = 0,

                                                   Ec_eol = item.Ec_eol,
                                                   Ec_bomdate = item.Ec_bomdate,
                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = item.Ec_bstock,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                   p2dModifier = GetIdentityName(),
                                                   p2dModifyDate = DateTime.Now,

                                                   Ec_mmdate = item.Ec_mmdate,
                                                   Ec_mmlot = item.Ec_mmlot,
                                                   Ec_mmlotno = item.Ec_mmlotno,
                                                   Ec_mmnote = item.Ec_mmnote,
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_p1dline = "与制一无关",
                                                   Ec_p1dlot = "与制一无关",
                                                   Ec_p1dnote = "与制一无关",
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = item.IsDeleted,
                                                   Remark = item.Remark,

                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
                //var deleteList = DB.Pp_Ecs.Where(c => c.Ec_no.Contains(Ec_no.Text) && c.IsDeleted == 0).ToList();
                //deleteList.ForEach(x => x.IsDeleted = 1);
                //deleteList.ForEach(x => x.Endtag = 1);
                //deleteList.ForEach(x => x.Modifier = GetIdentityName());
                //deleteList.ForEach(x => x.ModifyDate = DateTime.Now);
                DB.BulkUpdate(UpdateList7);
                DB.BulkSaveChanges();
            }

            InsNetOperateNotes(strGuid);
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void Ec_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Ec_no.SelectedIndex != 0 && this.Ec_no.SelectedIndex != -1)
            {
                BindGrid();
            }
        }

        #endregion Events

        #region NetOperateNotes

        private void InsNetOperateNotes(Guid strGuid)
        {
            string plog;
            //发送邮件通知
            //Mailto();
            var q = (from a in DB.Pp_Ec_Subs
                     where a.GUID.CompareTo(strGuid) == 0
                     where a.IsDeleted == 0
                     select a).ToList();
            if (q.Any())
            {
                plog = q[0].Ec_no + "," + q[0].Ec_bomitem + "," + q[0].Ec_newitem + ",物料管理区分：" + (q[0].IsManage.ToString() == "1" ? "共通" : (q[0].IsManage.ToString() == "2" ? "部管" : (q[0].IsManage.ToString() == "3" ? "制二" : (q[0].IsManage.ToString() == "0" ? "不管理" : q[0].IsManage.ToString()))));

                //新增日志
                string Newtext = plog;
                string OperateType = "修改";//操作标记
                string OperateNotes = "Edit技术* " + Newtext + " Edit*技术 的记录已修改";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);
            }
        }

        #endregion NetOperateNotes
    }
}