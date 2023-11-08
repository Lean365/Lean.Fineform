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

namespace Lean.Fineform.Lc_MM
{
    public partial class material : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMaterialView";
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
            // CheckPowerWithButton("CoreNgcodeDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreNgcodeNew", btnNew);
            //CheckPowerWithButton("CoreProlineNew", btnP2d);

            CheckPowerWithButton("CoreMMView", btnYFDataView);
            //CheckPowerWithButton("CoreLineNew", btnNew);

            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/MM/material_new.aspx", "新增");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }



        private void BindGrid()
        {
            IQueryable<Mm_Material> q = DB.Mm_Materials; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.MatItem.Contains(searchText) || u.PurGroup.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
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
            q = SortAndPage<Mm_Material>(q, Grid1);

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
            CheckPowerWithLinkButtonField("CoreMaterialEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreMaterialDelete", Grid1, "deleteField");
            // CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");

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








        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/MM/material_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }
            //int del_ID = GetSelectedDataKeyID(Grid1);
            Guid del_guid =Guid.Parse( GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreMaterialDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Mm_Material current = DB.Mm_Materials.Find(del_guid);

                string Newtext =current.GUID+","+ current.MatItem;


                string OperateType = "删除";//操作标记
                string OperateNotes = "Del管理员* " + Newtext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "物料管理", "物料管理信息删除", OperateNotes);

                current.isDelete = 1;
                DB.SaveChanges();


                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            //Alert.ShowInTop("窗体被关闭了。参数：");
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
        protected void btnYFDataView_Click(object sender, EventArgs e)
        {
            string Yf_DataView = global::Resources.GlobalResource.Yf_DataView;



            PageContext.RegisterStartupScript("top.addExampleTab('Yf_DataView','/Lc_Yifei/Yf_Map.aspx','" + Yf_DataView + "', '/res/Menu/yf.png', '', true, ''); ");
        }
        #endregion

        #region ExportExcel

        #endregion



        //protected void btnModelRegion_Click(object sender, EventArgs e)
        //{
        //    string Query_Model = global::Resources.GlobalResource.Query_Model;



        //    PageContext.RegisterStartupScript("top.addExampleTab('Query_Model','/Cube_PP/baseinfo/Pp_models_region.aspx','" + Query_Model + "', '/res/Menu/asset.png', '', true, ''); ");
        //}
    }
}
