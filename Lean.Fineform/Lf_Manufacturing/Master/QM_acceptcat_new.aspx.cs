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

    public partial class Qm_acceptcat_new: PageBase
    {
        // 

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreInspectCatNew";
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
            labResult.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.检验类别：A,验收类别：B,不良级别：C</div><div>2.只能输入A，B，C。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();



        }







        #endregion

        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {

            //int id = GetQueryIntValue("id");
            //proSalesdata current = DB.proSalesdatas.Find(id);
            //string modi001 = current.Qcsd001;
            //string modi002 = current.Qcsd013.ToString();
            //string modi003 = current.Qcsd006.ToString();
            //string modi004 = (current.Qcsd007 + current.Qcsd008 + current.Qcsd009).ToString();
            //string modi005 = (current.Qcsd010 + current.Qcsd011 + current.Qcsd012).ToString();

            //if (this.Qcsd001.Text == modi001)
            //{
            //    if (decimal.Parse(this.Qcsd002.Text) == decimal.Parse(modi002))
            //    {
            //        if (decimal.Parse(this.Qcsd003.Text) == decimal.Parse(modi003))
            //        {
            //            if (decimal.Parse(this.Qcsd007.Text) + decimal.Parse(this.Qcsd008.Text) + decimal.Parse(this.Qcsd009.Text) == decimal.Parse(modi004))
            //            {
            //                if (decimal.Parse(this.Qcsd010.Text) + decimal.Parse(this.Qcsd011.Text) + decimal.Parse(this.Qcsd012.Text) == decimal.Parse(modi005))
            //                {
            //                    Alert alert = new Alert();
            //                    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    alert.IconFont = IconFont.Warning;
            //                    alert.Target = Target.Top;
            //                    Alert.ShowInTop();
            //                }
            //            }
            //        }
            //    }

            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}


            //int id = GetQueryIntValue("id");
            //proLinestop current = DB.proLinestops.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.Prostoptext;


            //if (this.Prostoptext.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            //string InputData = Qcpd003.Text.Trim();


            //proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            //if (redata != null)
            //{
            //    Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
            string InputData = Checkcntext.Text.Trim();


            Qm_CheckType Redata = DB.Qm_CheckTypes.Where(u => u.Checkcntext == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("基础资料,品管类别< " + InputData + ">已经存在！修改即可", "错误提示", MessageBoxIcon.Error);

                return;
            }

        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            Qm_CheckType item = new Qm_CheckType();
            item.GUID = Guid.NewGuid();
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.Checktype = Checktype.Text;
            item.Checkcntext = Checkcntext.Text;
            item.Checkentext = Checkentext.Text;
            item.Checkjptext = Checkjptext.Text;



            // 添加所有用户

            item.isDelete = 0;
            item.Remark = Remark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Qm_CheckTypes.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Checktype.Text+","+ Checkcntext.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "品管类别新增", OperateNotes);


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
