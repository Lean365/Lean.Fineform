﻿using System;

//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.PP
{
    public partial class Pp_tracking_chart : PageBase
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
            //本月第一天
            //DpEndDate.SelectedDate = DateTime.Now;

            BindDdlModel();
        }

        public void BindDdlModel()
        {
            string edate = DateTime.Now.ToString("yyyyMMdd");

            var q_all = from a in DB.Pp_Tracking_Counts
                        orderby a.Pro_Date descending
                        select a;
            //q_all.OrderByDescending(u=>u.Pro_Date);
            var q = from a in q_all
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem

                        //where a.Pro_Date.CompareTo(edate) == 0

                    select new
                    {
                        Transtring = a.Pro_Date + "," + a.Pro_Model + "," + a.Pro_Lot + "," + a.Pro_Item,
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

        #endregion Page_Load
    }
}