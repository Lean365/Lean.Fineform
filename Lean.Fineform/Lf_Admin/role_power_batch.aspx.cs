using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using FineUIPro;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;


namespace Fine.Lf_Admin
{
    public partial class role_power_batch : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreRolePowerEdit";
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


        }
        #endregion
        #region Event

        protected void BtnBatch_Click(object sender, EventArgs e)
        {
            if (TextArea1.Text != "")
            {
                if (TextArea2.Text != "")
                {            //Regex.Replace(textStr, @"[/n/r]", "");
                             //用户代码去换行
                    string strUser = Regex.Replace(TextArea1.Text.ToString(), @"[\n\r]", "");
                    //去最后字符
                    string strUsers = strUser.Substring(0, strUser.Length - 1);

                    string[] strUserArray = strUsers.Split(','); //字符串转数组

                    //权限代码
                    string strPower = Regex.Replace(TextArea2.Text.ToString(), @"[\n\r]", "");
                    //去最后字符
                    string strPowers = strPower.Substring(0, strPower.Length - 1);
                    string[] strPowerArray = strPowers.Split(','); //字符串转数组

                    foreach (string i in strUserArray)
                    {
                        int f = Convert.ToInt32(i);
                        // 当前角色新的权限列表
                        List<int> newPowerIDs = new List<int>();


                        foreach (string item in strPowerArray)
                        {
                            if (item.Any())
                            {
                                newPowerIDs.Add(Convert.ToInt32(item));
                            }
                        }



                        Adm_Role role = DB.Adm_Roles.Include(r => r.Powers).Where(r => r.ID == f).FirstOrDefault();

                        ReplaceEntities<Adm_Power>(role.Powers, newPowerIDs.ToArray());

                        DB.SaveChanges();
                        //Alert.ShowInTop("批量更新角色权限成功！");

                    }

                }
            }

        }

        private void ClonePower()
        {

        }
        #endregion
    }
}