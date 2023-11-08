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
using System.Data.Entity.Validation;

namespace Lean.Fineform.Lf_Manufacturing.Master
{

    public partial class Pp_line_new : PageBase
    {

        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreLineNew";
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
            labResult.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

        }







        #endregion

        #region Events


        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            Pp_Line item = new Pp_Line();
            item.lineclass = lineclass.Text;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.linecode = linecode.Text;
            item.linename = linename.Text;
            item.en_linename = en_linename.Text;
            item.jp_linename = jp_linename.Text;

            item.GUID = Guid.NewGuid();
            // 添加所有用户


            item.Remark = remark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Lines.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = linename.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "班组新增", OperateNotes);


        }
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
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
            //判断重复
            string InputData = linename.Text.Trim();


            Pp_Line redata = DB.Pp_Lines.Where(u => u.linename == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("基本信息,班组< " + InputData + ">已经存在！修改即可");
                return;
            }
        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try { 
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
