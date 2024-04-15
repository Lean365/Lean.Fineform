using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.EC.dept
{
    public partial class view_desc : PageBase
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

        #endregion ViewPower

        #region Page_Load

        public static string strEcnNO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');
            //ID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem
            strEcnNO = (strgroup[0].ToString().Trim());

            BindDdlItemlist();
            BindData();
            BindGrid();
        }

        #endregion Page_Load

        #region Events

        private void BindData()
        {
            try
            {
                //string mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason,[D_SAP_ZPABD_Z004] AS Flag  FROM [dbo].[ProSapEngChanges] " +
                //                "LEFT JOIN  [dbo].[Ec_s] ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 " +
                //                " WHERE REPLACE(Ec_no,' ','') IS NULL  AND D_SAP_ZPABD_Z001='" + ItemMaster + "' " +
                //                " GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013,[D_SAP_ZPABD_Z004] ORDER BY D_SAP_ZPABD_Z005 DESC;";

                var q = from a in DB.Pp_SapEcns
                            //join b in DB.Pp_SapEcns on a.Ec_no equals b.D_SAP_ZPABD_Z001
                        where a.D_SAP_ZPABD_Z001 == strEcnNO
                        select new
                        {
                            Ec_leader = a.D_SAP_ZPABD_Z027,
                        };
                var qs = q.Select(a => new
                {
                    a.Ec_leader,
                }).ToList();
                if (qs.Any())
                {
                    EcnDesc.Text = qs[0].Ec_leader;//发行日期
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void DDL_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Item.SelectedIndex != -1 && DDL_Item.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
        }

        private void BindDdlItemlist()//物料信息
        {
            #region 停产机种不导入

            var q_All = from a in DB.Pp_SapEcnSubs
                        where a.D_SAP_ZPABD_S001.Contains(strEcnNO)
                        select new
                        {
                            a.D_SAP_ZPABD_S002
                        };

            var q_item = from a in q_All
                         select new

                         {
                             a.D_SAP_ZPABD_S002
                         };
            var qs = q_item.Select(E =>
                new
                {
                    E.D_SAP_ZPABD_S002,
                }).Distinct();

            #endregion 停产机种不导入

            // 绑定到下拉列表（启用模拟树功能）

            DDL_Item.DataTextField = "D_SAP_ZPABD_S002";
            DDL_Item.DataValueField = "D_SAP_ZPABD_S002";
            DDL_Item.DataSource = qs;
            DDL_Item.DataBind();

            // 选中根节点
            this.DDL_Item.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindGrid()//管理确认
        {
            #region 停产机种不导入

            var q_All = from a in DB.Pp_SapEcnSubs
                        where a.D_SAP_ZPABD_S001.Contains(strEcnNO)
                        select a;

            if (DDL_Item.SelectedIndex != -1 && DDL_Item.SelectedIndex != 0)
            {
                q_All = q_All.Where(u => u.D_SAP_ZPABD_S002.Contains(this.DDL_Item.SelectedItem.Text));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q_All);
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q_All);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
                Grid1.DataBind();
            }

            #endregion 停产机种不导入
        }

        #endregion Events

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
        }

        #endregion NetOperateNotes
    }
}