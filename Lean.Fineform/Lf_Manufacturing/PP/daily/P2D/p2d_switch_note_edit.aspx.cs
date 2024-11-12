using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_switch_note_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DNoteEdit";
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
            Pp_P2d_Switch_Note current = DB.Pp_P2d_Switch_Notes
                .Where(u => u.GUID == guid).FirstOrDefault();

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            this.Prodate.Text = current.Prodate;
            this.ProSmtSwitchNum.Text = current.ProSmtSwitchNum.ToString();
            this.ProSmtSwitchTotalTime.Text = current.ProSmtSwitchTotalTime.ToString();
            this.ProAitSwitchNum.Text = current.ProAitSwitchNum.ToString();
            this.ProAiStopTime.Text = current.ProAiStopTime.ToString();
            this.ProHandSopTime.Text = current.ProHandSopTime.ToString();
            this.ProHandPerson.Text = current.ProHandPerson.ToString();
            this.ProHandSopTotalTime.Text = current.ProHandSopTotalTime.ToString();
            this.ProHandSwitchNum.Text = current.ProHandSwitchNum.ToString();
            this.ProHandSwitchTime.Text = current.ProHandSwitchTime.ToString();
            this.ProHandSwitchTotalTime.Text = current.ProHandSwitchTotalTime.ToString();
            this.ProRepairSopTime.Text = current.ProRepairSopTime.ToString();
            this.ProRepairPerson.Text = current.ProRepairPerson.ToString();
            this.ProRepairSopTotalTime.Text = current.ProRepairSopTotalTime.ToString();
            this.ProRepairSwitchNum.Text = current.ProRepairSwitchNum.ToString();
            this.ProRepairSwitchTime.Text = current.ProRepairSwitchTime.ToString();
            this.ProRepairSwitchTotalTime.Text = current.ProRepairSwitchTotalTime.ToString();

            //修改前日志
            string BeforeModi = current.Prodate + "," + current.ProSmtSwitchTotalTime + "," + current.ProAiStopTime + "," + current.ProHandSwitchTotalTime + "," + current.ProRepairSwitchTotalTime;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "切换记录", "生产切换记录修改", OperateNotes);
        }

        #endregion Page_Load

        #region Events

        private void CheckData()
        {
            //判断修改内容
            Guid guid = Guid.Parse(GetQueryValue("GUID"));
            Pp_P2d_Switch_Note current = DB.Pp_P2d_Switch_Notes
                .Where(u => u.GUID == guid).FirstOrDefault();

            //if (this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            //{
            //    if (this.Proitem.Text == Cmc002)
            //    {
            //        if (Decimal.Parse(this.Proshort.Text) == Decimal.Parse(Cmc004))
            //        {
            //            if (Decimal.Parse(this.Prost.Text) == Decimal.Parse(Cmc005))
            //            {
            //                if (Decimal.Parse(this.Prorate.Text) == Decimal.Parse(Cmc008))
            //                {
            //                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
            //                    //Alert alert = new Alert();
            //                    //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);
            //                    //alert.Target = Target.Top;
            //                    //Alert.ShowInTop();
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
            Pp_P2d_Switch_Note item = DB.Pp_P2d_Switch_Notes
                .Where(u => u.GUID == guid).FirstOrDefault();
            item.ProSmtSwitchNum = int.Parse(this.ProSmtSwitchNum.Text);
            item.ProSmtSwitchTotalTime = int.Parse(this.ProSmtSwitchTotalTime.Text);
            item.ProAitSwitchNum = int.Parse(this.ProAitSwitchNum.Text);
            item.ProAiStopTime = int.Parse(this.ProAiStopTime.Text);
            item.ProHandSopTime = int.Parse(this.ProHandSopTime.Text);
            item.ProHandPerson = int.Parse(this.ProHandPerson.Text);
            item.ProHandSopTotalTime = int.Parse(this.ProHandSopTotalTime.Text);
            item.ProHandSwitchNum = int.Parse(this.ProHandSwitchNum.Text);
            item.ProHandSwitchTime = int.Parse(this.ProHandSwitchTime.Text);
            item.ProHandSwitchTotalTime = int.Parse(this.ProHandSwitchTotalTime.Text);
            item.ProRepairSopTime = int.Parse(this.ProRepairSopTime.Text);
            item.ProRepairPerson = int.Parse(this.ProRepairPerson.Text);
            item.ProRepairSopTotalTime = int.Parse(this.ProRepairSopTotalTime.Text);
            item.ProRepairSwitchNum = int.Parse(this.ProRepairSwitchNum.Text);
            item.ProRepairSwitchTime = int.Parse(this.ProRepairSwitchTime.Text);
            item.ProRepairSwitchTotalTime = int.Parse(this.ProRepairSwitchTotalTime.Text);

            item.Remark = "";
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = Prodate.Text + "," + ProSmtSwitchTotalTime.Text + "," + ProAiStopTime.Text + "," + ProHandSwitchTotalTime.Text + "," + ProRepairSwitchTotalTime.Text;

            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "切换记录", "切换记录修改", OperateNotes);
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

        protected void ProHandPerson_TextChanged(object sender, EventArgs e)
        {
            //decimal rate = (decimal)Prorate / 100;
            ProHandSopTotalTime.Text = (int.Parse(ProHandSopTime.Text) * int.Parse(ProHandPerson.Text)).ToString();
            ProHandSwitchTotalTime.Text = (int.Parse(ProHandSwitchTime.Text) * int.Parse(ProHandPerson.Text)).ToString();
        }

        protected void ProRepairPerson_TextChanged(object sender, EventArgs e)
        {
            //decimal rate = (decimal)Prorate / 100;
            ProRepairSopTotalTime.Text = ((int.Parse(ProRepairSopTime.Text)) * (int.Parse(ProRepairPerson.Text))).ToString();
            ProRepairSwitchTotalTime.Text = ((int.Parse(ProRepairSwitchTime.Text)) * (int.Parse(ProRepairPerson.Text))).ToString();
        }
    }
}