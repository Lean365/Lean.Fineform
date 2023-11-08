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
using System.Data.Entity.Validation;
namespace Lean.Fineform.Lf_Office.EM
{

    public partial class schedule_new : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEventNew";
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
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindDDLData();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

        }

        private void BindDDLData()
        {
            //查询班组

            var q = from a in DB.Em_Event_Types
                    select a;



            attypename.DataSource = q;
            attypename.DataTextField = "atcntypename";//设置所要读取的数据表里的列名
            attypename.DataValueField = "atcntype";
            attypename.DataBind();




        }

        protected void attypename_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (this.attypename.SelectedItem.Text == "公事事务")
            //{
            //    this.atcolor.Text = "#D2691E";//chocolate
            //    this.atbgcolor.Text = "#D2691E";
            //    this.atbdcolor.Text = "#D2691E";
            //    this.attextcolor.Text = "#F5F5F5";//whitesmoke
            //}
            //else
            //{
            //    this.atcolor.Text = "#006400";//darkgreen
            //    this.atbgcolor.Text = "#006400";
            //    this.atbdcolor.Text = "#006400";
            //    this.attextcolor.Text = "#00008B";//darkblue
            //}
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
        private void SaveItem()//新增日程
        {

            Em_Event item = new Em_Event();
            item.atuser = GetIdentityName();
            item.attypename = this.attypename.SelectedValue;// "1";
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
            item.GUID = Guid.NewGuid();



            // 添加所有用户



            item.CreateTime = DateTime.Now;
            item.CreateUser = GetIdentityName();
            DB.Em_Events.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = attitle.Text + "," + GetIdentityName();
            string OperateType = "新增";//操作标记
            string OperateNotes = "Add日程* " + Contectext + " *Add日程 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "日程管理", "日程新增", OperateNotes);
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData();
                SaveItem();

            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var erroritem in errors)
                    msg += erroritem.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }







        #endregion


    }
}
