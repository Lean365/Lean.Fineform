using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.complaint
{
    public partial class complaint_qa : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreComplaintP1DView";
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
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreComplaintQANew", BtnNew);
            //CheckPowerWithButton("CoreComplaintP1DNew", Btn_P1D_New);
            //CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            BtnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/complaint/complaint_qa_new.aspx", "新增");// + Window1.GetMaximizeReference();
            //Btn_P1D_New.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/complaint/complaint_p1d_new.aspx", "新增") + Window1.GetMaximizeReference();
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                if (rbtnFirstAuto.Checked)
                {
                    var q =
                            (from a in DB.Qm_Complaints
                             where a.p1dModifyDate.ToString() == "" || a.p1dModifyDate == null
                             //where b.Ec_distinction == 1
                             where a.IsDeleted == 0
                             orderby a.Cc_ReceivingDate
                             select new
                             {
                                 a.GUID,
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        a.GUID,
                        a.Cc_IssuesNo,
                        a.Cc_Customer,
                        a.Cc_Model,
                        a.Cc_ReceivingDate,
                        a.Cc_DefectsQty,
                        a.Cc_Issues,
                        a.Cc_Line,
                        a.Cc_ProcessDate,
                        a.Cc_Ddescription,
                        a.Cc_Reasons,
                        a.Cc_Operator,
                        a.Cc_Station,
                        a.Cc_Lot,
                        a.Cc_CorrectActions,
                        a.Cc_Reference,
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
                            (from a in DB.Qm_Complaints
                             where a.p1dModifyDate.ToString() != "" || a.p1dModifyDate != null
                             //where b.Ec_distinction == 1
                             where a.IsDeleted == 0
                             orderby a.Cc_ReceivingDate
                             select new
                             {
                                 a.GUID,
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        a.GUID,
                        a.Cc_IssuesNo,
                        a.Cc_Customer,
                        a.Cc_Model,
                        a.Cc_ReceivingDate,
                        a.Cc_DefectsQty,
                        a.Cc_Issues,
                        a.Cc_Line,
                        a.Cc_ProcessDate,
                        a.Cc_Ddescription,
                        a.Cc_Reasons,
                        a.Cc_Operator,
                        a.Cc_Station,
                        a.Cc_Lot,
                        a.Cc_CorrectActions,
                        a.Cc_Reference,
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
                            (from a in DB.Qm_Complaints
                                 //where a.p1dModifyDate.ToString() == "" || a.p1dModifyDate == null
                                 //where b.Ec_distinction == 1
                             where a.IsDeleted == 0
                             orderby a.Cc_ReceivingDate
                             select new
                             {
                                 a.GUID,
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        a.GUID,
                        a.Cc_IssuesNo,
                        a.Cc_Customer,
                        a.Cc_Model,
                        a.Cc_ReceivingDate,
                        a.Cc_DefectsQty,
                        a.Cc_Issues,
                        a.Cc_Line,
                        a.Cc_ProcessDate,
                        a.Cc_Ddescription,
                        a.Cc_Reasons,
                        a.Cc_Operator,
                        a.Cc_Station,
                        a.Cc_Lot,
                        a.Cc_CorrectActions,
                        a.Cc_Reference,
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

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreComplaintQAEdit", Grid1, "qaeditField");
            CheckPowerWithLinkButtonField("CoreComplaintP1DEdit", Grid1, "p1deditField");
            CheckPowerWithLinkButtonField("CoreComplaintDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
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

        //可选中多项删除
        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreProdataDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }
        //    InsNetOperateNotes();
        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proLines.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "PEdit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/complaint/complaint_p1d_edit.aspx?GUID=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
            }

            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreComplaintDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Complaint current = DB.Qm_Complaints.Find(del_ID);
                string Deltext = current.Cc_DocNo;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "客诉信息删除", OperateNotes);

                current.IsDeleted = 1;
                //current.Endtag = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();

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

        #endregion Events

        #region ExportExcel

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            string Prefix_XlsxName, Export_FileName, SheetName;
            //DataTable Exp = new DataTable();
            //在库明细查询SQL

            try
            {
                string searchText = ttbSearchMessage.Text.Trim();

                if (rbtnFirstAuto.Checked)
                {
                    var q =
                            (from a in DB.Qm_Complaints
                             where a.Cc_ProcessDate.ToString() == "" || a.Cc_ProcessDate == null
                             //where b.Ec_distinction == 1
                             where a.IsDeleted == 0

                             select new
                             {
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        接收日期 = a.Cc_ReceivingDate,
                        客诉编号 = a.Cc_IssuesNo,
                        客户简称 = a.Cc_Customer,
                        机种名称 = a.Cc_Model,

                        不良数量 = a.Cc_DefectsQty,
                        投诉事项 = a.Cc_Issues,
                        班组名称 = a.Cc_Line,
                        处理日期 = a.Cc_ProcessDate,
                        不良症状 = a.Cc_Ddescription,
                        不良原因 = a.Cc_Reasons,
                        作业人员 = a.Cc_Operator,
                        作业工位 = a.Cc_Station,
                        生产批次 = a.Cc_Lot,
                        改善对策 = a.Cc_CorrectActions,
                    }).Distinct();

                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    SheetName = "D" + DateTime.Now.ToString("yyyyMM");
                    Prefix_XlsxName = "cc_unprcessed";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                    //Grid1.AllowPaging = true;
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q =
                            (from a in DB.Qm_Complaints
                             where a.Cc_ProcessDate.ToString() != "" || a.Cc_ProcessDate != null
                             //where b.Ec_distinction == 1
                             where a.IsDeleted == 0

                             select new
                             {
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        接收日期 = a.Cc_ReceivingDate,
                        客诉编号 = a.Cc_IssuesNo,
                        客户简称 = a.Cc_Customer,
                        机种名称 = a.Cc_Model,

                        不良数量 = a.Cc_DefectsQty,
                        投诉事项 = a.Cc_Issues,
                        班组名称 = a.Cc_Line,
                        处理日期 = a.Cc_ProcessDate,
                        不良症状 = a.Cc_Ddescription,
                        不良原因 = a.Cc_Reasons,
                        作业人员 = a.Cc_Operator,
                        作业工位 = a.Cc_Station,
                        生产批次 = a.Cc_Lot,
                        改善对策 = a.Cc_CorrectActions,
                    }).Distinct();

                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Prefix_XlsxName = "cc_prcessed";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                    //Grid1.AllowPaging = true;
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q =
                            (from a in DB.Qm_Complaints
                                 //where a.p1dModifyDate.ToString() == "" || a.p1dModifyDate == null
                                 //where b.Ec_distinction == 1
                             where a.IsDeleted == 0

                             select new
                             {
                                 a.Cc_IssuesNo,
                                 a.Cc_Customer,
                                 a.Cc_Model,
                                 a.Cc_ReceivingDate,
                                 a.Cc_DefectsQty,
                                 a.Cc_Issues,
                                 a.Cc_Line,
                                 a.Cc_ProcessDate,
                                 a.Cc_Ddescription,
                                 a.Cc_Reasons,
                                 a.Cc_Operator,
                                 a.Cc_Station,
                                 a.Cc_Lot,
                                 a.Cc_CorrectActions,
                                 a.Cc_Reference,
                             });

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Cc_Model.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    var qs = q.Select(a =>
                    new
                    {
                        接收日期 = a.Cc_ReceivingDate,
                        客诉编号 = a.Cc_IssuesNo,
                        客户简称 = a.Cc_Customer,
                        机种名称 = a.Cc_Model,

                        不良数量 = a.Cc_DefectsQty,
                        投诉事项 = a.Cc_Issues,
                        班组名称 = a.Cc_Line,
                        处理日期 = a.Cc_ProcessDate,
                        不良症状 = a.Cc_Ddescription,
                        不良原因 = a.Cc_Reasons,
                        作业人员 = a.Cc_Operator,
                        作业工位 = a.Cc_Station,
                        生产批次 = a.Cc_Lot,
                        改善对策 = a.Cc_CorrectActions,
                    }).Distinct();

                    int c = GridHelper.GetTotalCount(qs);
                    ConvertHelper.LinqConvertToDataTable(qs);
                    Prefix_XlsxName = "cc_all";
                    //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
                    Export_FileName = Prefix_XlsxName + ".xlsx";
                    //Grid1.AllowPaging = false;
                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName);
                    //Grid1.AllowPaging = true;
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

        #endregion ExportExcel
    }
}