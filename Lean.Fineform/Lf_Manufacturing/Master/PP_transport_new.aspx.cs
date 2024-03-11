using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_transport_new : PageBase
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
                return "CoreTransportNew";
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


        }







        #endregion

        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {

            //int id = GetQueryIntValue("id");
            //proScannerDest current = DB.proScannerDest.Find(id);
            //string modi001 = current.LF001;
            //string modi002 = current.LF001;
            //string modi003 = current.LF001;
            //string modi004 = current.LF001;
            //string modi005 = current.LF002;

            //if (this.LF001.Text == modi001)
            //{
            //    if (this.LF001.Text == modi002)
            //    {
            //        if (this.LF001.Text == modi003)
            //        {
            //            if (this.LF001.Text == modi004)
            //            {
            //                if (this.LF002.Text == modi005)
            //                {
            //                    Alert alert = new Alert();
            //                    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    alert.IconUrl = "~/Lf_Resources/images/success.png";
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
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
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
            string InputData = Transportcntext.Text.Trim();


            Pp_Transport Redata = DB.Pp_Transports.Where(u => u.Transportcntext == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("数据,运输方式< " + InputData + ">已经存在！修改即可");
                return;
            }

        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            Pp_Transport item = new Pp_Transport();
            item.GUID =Guid.NewGuid();
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.Transportype = Transportype.Text;
            item.Transportcntext = Transportcntext.Text;
            item.Transportentext = Transportentext.Text;
            item.Transportjptext = Transportjptext.Text;




            // 添加所有用户

            item.isDelete = 0;
            item.Remark = remark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Transports.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Transportype.Text+","+ Transportcntext.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "运输方式新增", OperateNotes);


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
