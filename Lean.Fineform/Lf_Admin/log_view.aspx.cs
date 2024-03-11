using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;using System.Data.Entity.Validation;
using FineUIPro;


namespace Fine.Lf_Admin
{
    public partial class log_view : PageBase
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

            int id = GetQueryIntValue("id");
            Adm_Log log = DB.Adm_Logs.Find(id);
            if (log == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            //string ss = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>"+ log.Level + "</div><div>2.附件大小规定：单一文件不能超10Mb，如果同时上付多个附件，附件总大小不能超过10Mb，否则请逐个上传</div><div>3.请添加中文翻译内容</div><div>4.担当者不能为空</div><div>4.修改管理区分时，设变单身数据将重新导入，之前各部门填写的资料全部作废</div>");
            LoginLogger.Text = "用户："+ log.UserID+"-"+log.UserName +Environment.NewLine + "日期：" + log.Date.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine +  "级别：" + log.Level + Environment.NewLine + "信息：" + log.Message + Environment.NewLine + "事务：" + log.Exception;

        }

        #endregion

        #region Events
        private void BindData()
        {
            try
            {
                int id = GetQueryIntValue("id");
                //string mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason,[D_SAP_ZPABD_Z004] AS Flag  FROM [dbo].[ProSapEngChanges] " +
                //                "LEFT JOIN  [dbo].[Ec_s] ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 " +
                //                " WHERE REPLACE(Ec_no,' ','') IS NULL  AND D_SAP_ZPABD_Z001='" + ItemMaster + "' " +
                //                " GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013,[D_SAP_ZPABD_Z004] ORDER BY D_SAP_ZPABD_Z005 DESC;";

                var q = from a in DB.Adm_Logs
                            //join b in DB.Pp_SapEcns on a.Ec_no equals b.D_SAP_ZPABD_Z001
                        where a.ID == id
                        select new
                        {
                            a.UserID,
                            a.UserName,
                            a.Date,
                            a.Logger,
                            a.Message,
                            a.Level,
                            a.Thread,
                            a.Exception,


                        };
                var qs = q.Select(a => new
                {
                    a.UserID,
                    a.UserName,
                    a.Date,
                    a.Logger,
                    a.Message,
                    a.Level,
                    a.Thread,
                    a.Exception,
                }).ToList();
                if (qs.Any())
                {
                    LoginLogger.Text = "用户：" + qs[0].UserID + "-" + qs[0].UserName + Environment.NewLine + "日期：" + qs[0].Date.ToString("yyyy-MM-dd HH:mm:ss") + Environment.NewLine + "级别：" + qs[0].Level + Environment.NewLine + "信息：" + qs[0].Message + Environment.NewLine + "事务：" + qs[0].Exception;

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
