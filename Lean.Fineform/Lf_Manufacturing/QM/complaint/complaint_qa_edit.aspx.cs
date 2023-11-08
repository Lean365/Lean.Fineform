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

namespace Lean.Fineform.Lf_Manufacturing.QM.complaint
{

    public partial class complaint_qa_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreComplaintQAEdit";
            }
        }

        #endregion

        #region Page_Load
        public string txtRefdoc;
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

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

            BindData();

        }
        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Complaint current = DB.Qm_Complaints.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            Cc_DocNo.Text = current.Cc_DocNo;
            Cc_IssuesNo.Text = current.Cc_IssuesNo;
            Cc_ID.Text = current.Cc_Customer;
            Cc_Model.Text = current.Cc_Model;
            Cc_Order.Text = current.Cc_Order;
            Cc_ReceivingDate.Text = current.Cc_ReceivingDate;
            Cc_DefectsQty.Text = current.Cc_DefectsQty.ToString();
            Cc_Issues.Text = current.Cc_Issues;
            Cc_Serialno.Text = current.Cc_Serialno;
            // string npath = current.Notice_Attachments.Substring(0, 43);
            if (!string.IsNullOrEmpty(current.Cc_Reference.ToString()))
            {
                //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                oldCc_Reference.Text = current.Cc_Reference.ToString().Substring(43, current.Cc_Reference.ToString().Length - 43);
            }
            else

            {
                oldCc_Reference.Text = "-";
            }
            //Cc_Discover.Text= current.Cc_Discover;
            Cc_ReceivedDate.Text = current.Cc_ReceivedDate;
            remark.Text = current.Remark;
            //this.Lineguid.Text = current.GUID.ToString();
            // 添加所有用户



            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeModi = current.Cc_IssuesNo;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "客诉信息", "客诉信息修改", OperateNotes);
        }







        #endregion

        #region Events
        private void Refdoc()
        {
            if (Cc_Reference.HasFile)
            {
                string fileName = Cc_Reference.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    Alert.ShowInTop("无效的文件类型！");
                    return;
                }


                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //判断最后一个.的位置
                int i = fileName.LastIndexOf(".");
                //字串长度
                int f = fileName.Length;
                //截取长度
                int iff = fileName.Length - fileName.LastIndexOf(".");
                fileName = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));

                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_DTA_CC-" + Cc_IssuesNo.Text + fileName;

                Cc_Reference.SaveAs(Server.MapPath("../../Lc_Docs/upload/qadoc/" + fileName));

                txtRefdoc = "../../Lc_Docs/upload/qadoc/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }
        private void CheckData()
        {
            //判断修改内容
            //Guid id = Guid.Parse(GetQueryValue("GUID"));
            //Qm_Complaint current = DB.Qm_Complaints.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string Cc_Issues = current.Cc_Issues;
            //string Cc_ReceivedDate = current.Cc_ReceivedDate;

            //if (this.Cc_Issues.Text == Cc_Issues)
            //{
            //    if (this.Cc_ReceivedDate.Text == Cc_ReceivedDate)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //    {
            //        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
            //        //Alert alert = new Alert();
            //        //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //        //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);

            //        //"Information" Text = "消息"   
            //        //"Warning" Text = "警告"       
            //        //"Question" Text = "问题"          
            //        // "Error" Text = "错误"            
            //        //"Success" Text = "成功"
            //        //alert.IconFont = IconFont.Warning;
            //        //alert.Icon = Icon.Error;
            //        //alert.IconUrl = "~/Lf_Resources/images/warning.png";

            //        //alert.Target = Target.Top;
            //        //Alert.ShowInTop();
            //        //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //    }
            //}


            //    //判断重复
            //    string InputData = Qcpd003.SelectedItem.Text.Trim();


            //    proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            //    if (redata != null)
            //    {
            //        Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //        return;
            //    }
        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Complaint item = DB.Qm_Complaints

                .Where(u => u.GUID == id).FirstOrDefault();



            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.Cc_ReceivingDate = Cc_ReceivingDate.Text;
            item.Cc_Issues = Cc_Issues.Text;
            item.Cc_Serialno = Cc_Serialno.Text;
            Refdoc();

            if (!String.IsNullOrEmpty(txtRefdoc))
            {

                //技联Ec_letterdoc  Ec_letterno
                item.Cc_Reference = txtRefdoc;
            }
            else
            {


                //item.Ec_letterdoc = "";

                if (oldCc_Reference.Text == "-")
                {
                    item.Cc_Reference = "";
                }
                //else
                //{
                //    item.Ec_letterdoc = "../../Lc_Docs/upload/ecndoc/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldEc_letterdoc.Text;
                //}


            }


            item.Cc_ReceivingDate = Cc_ReceivingDate.Text;


            item.Cc_ReceivedDate = Cc_ReceivedDate.Text;
            

            //item.Lineguid = this.Lineguid.Text;

            // 添加所有用户


            item.Remark = remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = Cc_IssuesNo.Text+","+ Cc_Issues.Text + "," + txtRefdoc + "," + Cc_ReceivedDate.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "客诉信息", "客诉信息修改", OperateNotes);


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
