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
namespace Lean.Fineform.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_notice : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcNoticeView";
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
            // CheckPowerWithButton("CoreQacheckDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreFqcNoticeNew", btnNew);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);

            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_notice_new.aspx", "新增");
            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_new.aspx", "新增P1D生产日报");
            //btnP2dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p2d_new.aspx", "新增P2D生产日报");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }



        private void BindGrid()
        {
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            IQueryable<Qm_Unqualified> q = DB.Qm_Unqualifieds; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.qmModels.ToString().Contains(searchText) || u.qmMaterial.ToString().Contains(searchText));
            }
            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.qmCheckdate.CompareTo(edate) <= 0);
            }
            if (this.DDLline.SelectedIndex != -1 && this.DDLline.SelectedIndex != 0)
            {

                q = q.Where(u => u.qmLine.Contains(this.DDLline.SelectedText));
            }

            q = q.Where(u => u.qmRejectqty > 0);
            //q = q.Where(u => u.LA002 > 0);

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
            q = SortAndPage<Qm_Unqualified>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

        }
        public void BindDDLLine()
        {
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Qm_Unqualifieds
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                    where a.qmCheckdate.CompareTo(sdate) >= 0
                    where a.qmCheckdate.CompareTo(edate) <= 0
                    select new
                    {
                        a.qmLine

                    };




            var qs = q.Select(E => new { E.qmLine, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLline.DataSource = qs;
            DDLline.DataTextField = "qmLine";
            DDLline.DataValueField = "qmLine";
            DDLline.DataBind();
            this.DDLline.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }
        #endregion

        #region Events
        protected void DDLline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLline.SelectedIndex != -1 && DDLline.SelectedIndex != 0)
            {

                BindGrid();
            }
        }
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
        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindDDLLine();
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindDDLLine();
                BindGrid();
            }
        }
        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
            CheckPowerWithLinkButtonField("CoreFqcNoticeEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreFqcNoticeDelete", Grid1, "deleteField");
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
        //    if (!CheckPower("CoreQacheckDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.pqQachecks.Where(u => ids.Contains(u.ID)).Delete();


        //    // 重新绑定表格
        //    BindGrid();
        //    NetLogRecord();
        //}






        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            Guid strGUID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));
            if (e.CommandName == "Edit")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcNoticeEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_notice_edit.aspx?GUID=" + strGUID + "&type=1") + Window1.GetMaximizeReference());

            }
            if (e.CommandName == "Print")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreKitPrint"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/fqc/fqc_notice_edit.aspx?GUID=" + strGUID + "&type=1") + Window1.GetMaximizeReference());

            }


            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcNoticeDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Unqualified current = DB.Qm_Unqualifieds.Find(strGUID);
                string Deltext = current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmRegion + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmRejectqty + "," + current.qmJudgmentlevel;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良联络删除", OperateNotes);

                current.isDelete = 0;
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







    }
}
