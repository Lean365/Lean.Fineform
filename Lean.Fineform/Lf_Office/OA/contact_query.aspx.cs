using Fine.Lf_Business.Models.OA;
using FineUIPro;
using System;
using System.Data;
using System.Linq;

namespace Fine.Lf_Office.OA
{
    public partial class contact_query : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreAddressView";
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
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreAddressNew", btnNew);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Office.OA/address_new.aspx", "新增");
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
                    IQueryable<Oa_Contact> q = DB.Oa_Contacts; //.Include(u => u.Dept);

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Su_Name_CN.Contains(searchText) || u.Su_Name_EN.Contains(searchText) || u.Su_Email.Contains(searchText) || u.Su_Code.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                    }
                    q = q.Where(u => u.isDelete == 0);
                    q = q.Where(u => u.Su_Type.CompareTo("A") == 0);
                    if (rblEnableStatus.SelectedValue != "all")
                    {
                        q = q.Where(u => u.Su_Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    }
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
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Oa_Contact>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnSecondAuto.Checked)
                {
                    IQueryable<Oa_Contact> q = DB.Oa_Contacts; //.Include(u => u.Dept);

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Su_Name_CN.Contains(searchText) || u.Su_Name_EN.Contains(searchText) || u.Su_Email.Contains(searchText) || u.Su_Code.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                    }
                    q = q.Where(u => u.isDelete == 0);
                    q = q.Where(u => u.Su_Type.CompareTo("C") == 0);
                    if (rblEnableStatus.SelectedValue != "all")
                    {
                        q = q.Where(u => u.Su_Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    }
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
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Oa_Contact>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnThirdAuto.Checked)
                {
                    IQueryable<Oa_Contact> q = DB.Oa_Contacts; //.Include(u => u.Dept);

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Su_Name_CN.Contains(searchText) || u.Su_Name_EN.Contains(searchText) || u.Su_Email.Contains(searchText) || u.Su_Code.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                    }
                    q = q.Where(u => u.isDelete == 0);
                    q = q.Where(u => u.Su_Type.CompareTo("B") == 0);
                    if (rblEnableStatus.SelectedValue != "all")
                    {
                        q = q.Where(u => u.Su_Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    }
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
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Oa_Contact>(q, Grid1);

                    Grid1.DataSource = q;
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
            //CheckPowerWithLinkButtonField("CoreAddressEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreAddressDelete", Grid1, "deleteField");
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
            //if (e.CommandName == "Edit")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Office.OA/address.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreAddressDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    //删除日志
            //    //int userID = GetSelectedDataKeyID(Grid1);
            //    Oa_Contact current = DB.Oa_Contacts.Find(del_ID);
            //    string Deltext = current.Su_Name_CN;
            //    string OperateType = "删除";
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "日常办公", "通讯信息删除", OperateNotes);

            //    current.isDelete = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyTime = DateTime.Now;
            //    DB.SaveChanges();

            //    BindGrid();
            //}
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion
        #region ExportExcel



        #endregion
    }
}
