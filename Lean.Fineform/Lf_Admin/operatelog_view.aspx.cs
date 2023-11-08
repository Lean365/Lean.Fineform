using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using FineUIPro;


namespace Lean.Fineform.Lf_Admin
{
    public partial class operatelog_view : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreLogView";
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
            btnClose.OnClientClick = ActiveWindow.GetHideReference();




            BindData();

        }

        #endregion

        #region Events
        private void BindData()
        {
            try
            {
                Guid strGuid =Guid.Parse( GetQueryValue("GUID"));
                //string mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason,[D_SAP_ZPABD_Z004] AS Flag  FROM [dbo].[ProSapEngChanges] " +
                //                "LEFT JOIN  [dbo].[Ec_s] ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 " +
                //                " WHERE REPLACE(Ec_no,' ','') IS NULL  AND D_SAP_ZPABD_Z001='" + ItemMaster + "' " +
                //                " GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013,[D_SAP_ZPABD_Z004] ORDER BY D_SAP_ZPABD_Z005 DESC;";

                var q = from a in DB.Adm_OperateLogs
                            //join b in DB.Pp_SapEcns on a.Ec_no equals b.D_SAP_ZPABD_Z001
                        where a.GUID == strGuid
                        select new
                        {
                            a.OperateNotes,

                        };
                var qs = q.Select(a => new
                {
                    a.OperateNotes,
                }).ToList();
                if (qs.Any())
                {
                    OperateNotes.Text = qs[0].OperateNotes;//发行日期

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

    }
}
