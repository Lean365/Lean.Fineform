using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Drawing;
namespace Lean.Fineform.Lf_Office.EM
{

    public partial class schedule_edit : PageBase
    {

        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEventEdit";
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
            Em_Event current = DB.Em_Events.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }


            this.attypename.Text = current.attypename;// "1";
            attitle.Text = current.attitle;
            atcontent.Text = current.atcontent;

            this.atsdate.Text = current.atsdate.ToString();
            this.atedate.Text = current.atedate.ToString();
            aturl.Text = current.aturl;


            this.atcolor.Text = current.atcolor;
            this.atbgcolor.Text = current.atbgcolor;
            this.atbdcolor.Text = current.atbdcolor;
            this.attextcolor.Text = current.attextcolor;
            Remark.Text = current.Remark;

            //新增日志

            string Contectext = attitle.Text + "," + GetIdentityName();
            string OperateType = "编辑,";//操作标记
            string OperateNotes = "Edit日程* " + Contectext + " *Edit日程 的记录可能被编辑";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "日程管理", "日程编辑", OperateNotes);


        }


        #endregion

        #region Events
        private void CheckData()
        {
            ////判断修改内容
            //int id = GetQueryIntValue("id");
            //proLine current = DB.proLines.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.linename;


            //if (this.linename.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
            //判断重复
            string InputData = atsdate.Text.Substring(0, 13);
            string taskuser = GetIdentityName();

            Em_Event redata = DB.Em_Events.Where(u => u.atsdate.ToString().Contains(InputData) && u.atuser == taskuser).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("日程管理,相同时间事件<" + taskuser + "+'||'+ " + InputData + ">已经存在！修改即可");
                return;
            }
        }

        //字段赋值，保存
        private void SaveItem()//编辑日程
        {
            int id = GetQueryIntValue("id");
            Em_Event item = DB.Em_Events.Find(id);

            item.atuser = GetIdentityName();
            item.attypename = this.attypename.Text;// "1";
            item.attitle = attitle.Text;
            item.atcontent = atcontent.Text;
            item.atallday = false;
            item.atsdate = DateTime.Parse(this.atsdate.Text);
            item.atedate = DateTime.Parse(this.atedate.Text);
            item.aturl = aturl.Text;
            item.atclassname = "";
            item.ateditable = false;
            item.atsource = "";
            item.atcolor = this.atcolor.Text.ToUpper();
            item.atbgcolor = this.atbgcolor.Text.ToUpper();
            item.atbdcolor = this.atbdcolor.Text.ToUpper();
            item.attextcolor = this.attextcolor.Text.ToUpper();
            item.Remark = Remark.Text;




            // 添加所有用户



            item.ModiTime = DateTime.Now;
            item.ModiUser = GetIdentityName();

            DB.SaveChanges();

            //新增日志

            string Contectext = attitle.Text + "," + GetIdentityName();
            string OperateType = "编辑,";//操作标记
            string OperateNotes = "Add日程* " + Contectext + " *Add日程 的记录已编辑";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "日程管理", "日程编辑", OperateNotes);
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            //string inputUserName = tbxName.Text.Trim();

            //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            //if (user != null)
            //{
            //    Alert.ShowInTop("用户 " + inputUserName + " 已经存在！");
            //    return;
            //}
            CheckData();
            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }







        #endregion


    }
}
