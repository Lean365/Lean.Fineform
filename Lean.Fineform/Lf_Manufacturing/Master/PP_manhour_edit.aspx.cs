using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.Master
{
    public partial class Pp_manhour_edit : PageBase
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
                return "CoreManhourEdit";
            }
        }

        #endregion ViewPower

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
            Pp_Manhour current = DB.Pp_Manhours
                .Where(u => u.GUID == guid).FirstOrDefault();

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            this.Prodate.Text = current.Prodate;
            this.Proplnt.Text = current.Proplnt;
            this.Proitem.Text = current.Proitem;
            this.Prowcname.Text = current.Prowcname;
            this.Promodel.Text = current.Promodel;
            this.Protext.Text = current.Protext;
            this.Prowctext.Text = current.Prowctext;
            this.Proshort.Text = current.Proshort.ToString();
            this.Propset.Text = current.Propset;
            Prorate.Text = current.Prorate.ToString();
            Prost.Text = current.Prost.ToString();
            Proset.Text = current.Proset;
            Prodesc.Text = current.Prodesc;

            //修改前日志
            string BeforeModi = current.Prodate + "," + current.Proitem + "," + current.Proshort + "," + current.Prorate + "," + current.Prost;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "生产工时修改", OperateNotes);
        }

        #endregion Page_Load

        #region Events

        private void CheckData()
        {
            //判断修改内容
            Guid guid = Guid.Parse(GetQueryValue("GUID"));
            Pp_Manhour current = DB.Pp_Manhours
                .Where(u => u.GUID == guid).FirstOrDefault();

            Cmc001 = current.Prodate;
            Cmc002 = current.Proitem;
            Cmc004 = current.Proshort.ToString();
            Cmc005 = current.Prost.ToString();
            Cmc008 = current.Prorate.ToString();

            if (this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            {
                if (this.Proitem.Text == Cmc002)
                {
                    if (Decimal.Parse(this.Proshort.Text) == Decimal.Parse(Cmc004))
                    {
                        if (Decimal.Parse(this.Prost.Text) == Decimal.Parse(Cmc005))
                        {
                            if (Decimal.Parse(this.Prorate.Text) == Decimal.Parse(Cmc008))
                            {
                                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
                                //Alert alert = new Alert();
                                //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
                                //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);
                                //alert.Target = Target.Top;
                                //Alert.ShowInTop();
                            }
                        }
                    }
                }

                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }

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
            //判断重复
            //string InputData = Proitem.Text.Trim();

            //proManhour redata = DB.Pp_Manhours.Where(u => u.Proitem == InputData).FirstOrDefault();

            //if (redata != null)
            //{
            //    Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            //int id = GetQueryIntValue("id");
            Guid guid = Guid.Parse(GetQueryValue("GUID"));
            Pp_Manhour item = DB.Pp_Manhours
                .Where(u => u.GUID == guid).FirstOrDefault();

            item.Prodate = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proshort = decimal.Parse(this.Proshort.Text);
            item.Prorate = decimal.Parse(Prorate.Text);
            item.Prost = decimal.Parse(Prost.Text);

            item.Remark = Remark.Text;
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") + Proitem.Text + Proshort.Text + "," + this.Prorate.Text + "," + this.Prost.Text;

            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "生产工时修改", OperateNotes);
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

        #endregion Events
    }
}