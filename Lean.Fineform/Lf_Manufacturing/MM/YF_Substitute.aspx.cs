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

namespace Lean.Fineform.Lf_Manufacturing.MM
{
    public partial class Yf_Substitute : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMMView";
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

            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);


            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);


            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }



        private void BindGrid()
        {
            Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

            IQueryable<Lean.Fineform.Lf_Business.Models.YF.BOMMB> q = DBYF.BOMMB; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.MB001.Contains(searchText) || u.MB002.Contains(searchText) || u.MB004.Contains(searchText));
            }
            var qs = from a in q
                     select new
                     {
                         a.MB001,
                         a.MB002,
                         MB003 = (a.MB003 == "2" ? "替换料件" : "替代料件"),

                         a.MB004,
                         a.MB005,
                         a.MB006,
                         a.MB008
                     };

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(qs);
            if (GridHelper.GetTotalCount(qs) > 0)
            {
                // 排列和数据库分页
                DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                // 3.绑定到Grid
                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                // 3.绑定到Grid
                Grid1.DataSource = "";
                Grid1.DataBind();
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
            //CheckPowerWithLinkButtonField("CoreLineEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreLineDelete", Grid1, "deleteField");
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
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/Master/Pp_line_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreLineDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }
            //    //删除日志
            //    //int userID = GetSelectedDataKeyID(Grid1);
            //    Pp_Line current = DB.Pp_Lines.Find(del_ID);
            //    string Deltext = current.linename;
            //    string OperateType = "删除";
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "班组删除", OperateNotes);

            //    current.isDelete = 1;
            //    //current.Endtag = 1;
            //    current.Modifier = GetIdentityName();
            //    current.ModifyTime = DateTime.Now;
            //    DB.SaveChanges();

            BindGrid();
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

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {

        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }


        #endregion

        #region ExportExcel

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }



            Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities DBYF = new Lean.Fineform.Lf_Business.Models.YF.Yifei_DTAEntities();

            IQueryable<Lean.Fineform.Lf_Business.Models.YF.BOMMB> q = DBYF.BOMMB; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim().ToUpper();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.MB001.Contains(searchText));
                var qs = from p in q
                         select new
                         {
                             主件 = p.MB001,
                             元件 = p.MB002,
                             替代 = p.MB003,
                             品号 = p.MB004,
                             数量 = p.MB005,
                             生效 = p.MB006,
                             原因 = p.MB008,


                         };
                //DataTable Exp = new DataTable();
                //在库明细查询SQL
                string Xlsbomitem, ExportFileName;

                // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                Xlsbomitem = searchText + "_Yf_Substitute";
                //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                ExportFileName = Xlsbomitem + ".xlsx";


                ConvertHelper.LinqConvertToDataTable(qs);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
        }

        #endregion


    }
}
