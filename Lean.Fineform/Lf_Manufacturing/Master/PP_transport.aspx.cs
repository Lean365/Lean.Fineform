using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Lean.Fineform.Lf_Manufacturing.Master
{
    public partial class Pp_transport : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTransportView";
            }
        }

        #endregion

        #region Page_Load

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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreTransportNew", btnNew);
            //CheckPowerWithButton("CoreProlineNew", btnP2d);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_transport_new.aspx", "新增");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }



        private void BindGrid()
        {
            IQueryable<Pp_Transport> q = DB.Pp_Transports; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Transportype.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            }
            q = q.Where(u => u.isDelete == 0);
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
            q = SortAndPage<Pp_Transport>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
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
            CheckPowerWithLinkButtonField("CoreTransportEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreTransportDelete", Grid1, "deleteField");

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

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreProdataDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    #region NetOperateNotes
        //    int ID = GetSelectedDataKeyID(Grid1);
        //    proReason current = DB.proReasons.Find(ID);
        //    NetCountHelper.InsNetOperateNotes(GetIdentityName(), "未达成原因", "CoreProdataDelete", "未达成原因,记录<" + current.ID+current.Proreasontext + ">已删除");//();
        //    #endregion

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proReasons.Where(u => ids.Contains(u.ID)).Delete();


        //    // 重新绑定表格
        //    BindGrid();
        //}






        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_transport_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreTransportDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                Pp_Transport current = DB.Pp_Transports.Find(del_ID);
                string Deltext = current.Transportype+","+current.Transportcntext;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "运输方式删除", OperateNotes);


                current.isDelete = 1;
                //current.Endtag = 1;
                current.Modifier = GetIdentityName();
                current.ModifyTime = DateTime.Now;
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

        #endregion
        #region ExportExcel



        #endregion


    }
}
