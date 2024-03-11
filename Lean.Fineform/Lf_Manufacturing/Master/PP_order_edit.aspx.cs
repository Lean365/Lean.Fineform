using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_order_edit : PageBase
    {
        public string Cmc001, Cmc002, Cmc004, Cmc005, Cmc008;
        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreOrderEdit";
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
            Guid guid = Guid.Parse(GetQueryValue("GUID"));
            //int id = GetQueryIntValue("id");
            Pp_Order current = DB.Pp_Orders
                .Where(u => u.GUID == guid).FirstOrDefault();




            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }


            this.Porderdate.Text = current.Porderdate;
            this.Porderhbn.Text = current.Porderhbn;
            this.Porderlot.Text = current.Porderlot;
            this.Porderqty.Text = current.Porderqty.ToString();
            this.Porderreal.Text = current.Porderreal.ToString();
            this.Porderno.Text = current.Porderno.ToString();
            this.Porderroute.Text = current.Porderroute.ToString();
            this.Porderserial.Text = current.Porderserial;
            this.Remark.Text = current.Remark;



            //修改前日志
            string BeforeModi = current.Porderdate+","+ current.Porderno + "," + current.Porderhbn + "," + current.Porderlot + "," + current.Porderqty;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "订单修改", OperateNotes);

        }
        #endregion
        #region Events
        //判断修改内容||判断重复
        private void CheckData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();
            Guid guid = Guid.Parse(GetQueryValue("GUID"));
            //int id = GetQueryIntValue("id");
            Pp_Order current = DB.Pp_Orders
                .Where(u => u.GUID == guid).FirstOrDefault();
            string modi001 = current.Porderdate;
            string modi002 = current.Porderlot;
            string modi003 = current.Porderqty.ToString();
            string modi004 = current.Porderno.ToString();
            string modi005 = current.Porderserial;

            if (this.Porderdate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            {
                if (this.Porderlot.Text == Cmc002)
                {
                    if (this.Porderno.Text == Cmc004)
                    {
                        if (decimal.Parse(this.Porderqty.Text) == decimal.Parse(Cmc005))
                        {
                            if (this.Porderserial.Text == Cmc008)
                            {
                                Alert.ShowInTop("global::Resources.GlobalResource.sys_Msg_Noedit！", "修改提示", MessageBoxIcon.Warning);
                            }
                        }
                    }
                }

                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            //int id = GetQueryIntValue("id");
            //proMovingpricedata current = DB.proMovingpricedatas.Find(id);
            //string modi001 = current.Qcpd002;
            //string modi002 = current.Qcpd003;
            //string modi003 = current.Qcpd004.ToString();
            //string modi004 = current.Qcpd005.ToString();
            //string modi005 = current.Qcpd006.ToString();

            //if (this.Qcpd002.Text == modi001)
            //{
            //    if (this.Qcpd003.Text == modi002)
            //    {
            //        if (this.Qcpd003.Text == modi003)
            //        {
            //            if (decimal.Parse(this.Qcpd004.Text) == decimal.Parse(modi004))
            //            {
            //                if (decimal.Parse(this.Qcpd005.Text) == decimal.Parse(modi005))
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


        }


        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            int id = GetQueryIntValue("id");
            Guid ids = Guid.Parse(GetQueryValue("GUID"));
            Pp_Order item = DB.Pp_Orders

                .Where(u => u.GUID == ids).FirstOrDefault();


            item.Porderdate = this.Porderdate.SelectedDate.Value.ToString("yyyyMMdd");
            
            item.Porderlot = this.Porderlot.Text.ToUpper();
            item.Porderqty = decimal.Parse(this.Porderqty.Text);
            item.Porderreal = decimal.Parse(this.Porderreal.Text);
            item.Porderserial = this.Porderserial.Text.ToUpper(); 
            
            item.Remark = Remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = this.Porderdate.SelectedDate.Value.ToString("yyyyMMdd")+","+Porderno.Text+"," + Porderhbn.Text + "," + Porderlot.Text + "," + this.Porderqty.Text;

            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "订单修改", OperateNotes);


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
