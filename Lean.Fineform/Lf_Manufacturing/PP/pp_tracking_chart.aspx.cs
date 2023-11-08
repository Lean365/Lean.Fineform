using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
//using EntityFramework.Extensions;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace Lean.Fineform.Lf_Manufacturing.PP
{
    public partial class pp_tracking_chart : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTrackingView";
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


            //本月第一天
            //DPend.SelectedDate = DateTime.Now;



            BindDDLModel();

        }

        public void BindDDLModel()
        {
            string edate = DateTime.Now.ToString("yyyyMMdd");

            var q_all = from a in DB.Pp_TrackingCounts
                        orderby a.Pro_Date descending
                        select a;
            //q_all.OrderByDescending(u=>u.Pro_Date);
            var q = from a in q_all
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem

                        //where a.Pro_Date.CompareTo(edate) == 0

                    select new
                    {
                         Transtring=a.Pro_Date+","+ a.Pro_Model + "," + a.Pro_Lot + "," + a.Pro_Item ,

                    };




            var qs = q.Select(E => new { E.Transtring, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLModel.DataSource = qs;
            DDLModel.DataTextField = "Transtring";
            DDLModel.DataValueField = "Transtring";
            DDLModel.DataBind();

            this.DDLModel.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));

        }



        #endregion

        #region Events





        #endregion



    }
}
