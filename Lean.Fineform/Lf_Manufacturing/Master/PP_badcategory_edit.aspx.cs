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

    public partial class Pp_badcategory_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreBadReasonEdit";
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
            Pp_DefectCode current = DB.Pp_DefectCodes.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }



            ngclass.Text = current.ngclass;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            cn_classmatter.Text = current.cn_classmatter;
            en_classmatter.Text = current.en_classmatter;
            jp_classmatter.Text = current.jp_classmatter;
            ngcode.Text = current.ngcode;
            cn_ngmatter.Text = current.cn_ngmatter;
            en_ngmatter.Text = current.en_ngmatter;
            jp_ngmatter.Text = current.jp_ngmatter;
            analysisclass.Text = current.analysisclass;
            jp_class.Text = current.jp_class;
            jp_ng.Text = current.jp_ng;
   

            remark.Text = current.Remark;

            // 添加所有用户



            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeModi = current.ngclass + "," + current.ngcode.ToString();
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "赁率修改", OperateNotes);
        }







        #endregion

        #region Events

        private void CheckData()
        {
            //判断修改内容
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_DefectCode current = DB.Pp_DefectCodes.Find(id);

            string modi001 = current.ngclass;
            string modi002 = current.cn_classmatter;
            string modi003 = current.ngcode.ToString();
            string modi004 = current.cn_ngmatter.ToString();
            string modi005 = current.analysisclass.ToString();

            if (this.ngclass.Text == modi001)
            {
                if (this.cn_classmatter.Text == modi002)
                {
                    if (this.ngcode.Text == modi003)
                    {
                        if (this.cn_ngmatter.Text == modi004)
                        {
                            if (this.analysisclass.Text == modi005)
                            {
                                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_DefectCode item = DB.Pp_DefectCodes

                .Where(u => u.GUID == id).FirstOrDefault();


            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.cn_classmatter = cn_classmatter.Text;
            item.en_classmatter = en_classmatter.Text;
            item.jp_classmatter = jp_classmatter.Text;

            item.cn_ngmatter = cn_ngmatter.Text;
            item.en_ngmatter = en_ngmatter.Text;
            item.jp_ngmatter = jp_ngmatter.Text;
            item.analysisclass = analysisclass.Text;
            item.jp_class = jp_class.Text;
            item.jp_ng = jp_ng.Text;


            // 添加所有用户


            item.Remark = remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();



            // 添加所有用户


            item.Remark = remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = ngclass.Text+","+cn_classmatter.Text + "," + ngcode.Text+","+ cn_ngmatter.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "赁率修改", OperateNotes);


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
