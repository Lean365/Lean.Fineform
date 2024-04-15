using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.complaint
{
    public partial class complaint_p1d_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreComplaintP1DEdit";
            }
        }

        #endregion ViewPower

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
            Cc_ProcessDate.SelectedDate = DateTime.Now;
            Cc_Operator.Text = "--";
            Cc_Station.Text = "--";
            Cc_Lot.Text = "--";
            BindDdlLine();
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
            Cc_Customer.Text = current.Cc_Customer;
            Cc_Model.Text = current.Cc_Model;
            Cc_Order.Text = current.Cc_Order;
            Cc_ReceivingDate.Text = current.Cc_ReceivingDate;
            Cc_Issues.Text = current.Cc_Issues;
            Cc_Serialno.Text = current.Cc_Serialno;
            Cc_Discover.Text = current.Cc_Discover;
            Cc_ReceivedDate.Text = current.Cc_ReceivedDate;
            Cc_Line.SelectedValue = current.Cc_Line;
            Cc_ProcessDate.Text = current.Cc_ProcessDate;
            if (current.Cc_Ddescription == "")
            {
                Cc_Ddescription.Text = "--";
            }
            else

            {
                Cc_Ddescription.Text = current.Cc_Ddescription;
            }
            if (current.Cc_Ddescription == "")
            {
                Cc_Reasons.Text = "--";
            }
            else
            {
                Cc_Reasons.Text = current.Cc_Reasons;
            }

            Cc_Operator.Text = current.Cc_Operator;
            Cc_Station.Text = current.Cc_Station;
            Cc_Lot.Text = current.Cc_Lot;
            Cc_CorrectActions.Text = current.Cc_CorrectActions;

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

        #endregion Page_Load

        #region Events

        public void BindDdlLine()
        {
            var q_Model = from a in DB.Adm_Dicts
                              //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                              //where b.Proecnno == strecn
                              //where b.Proecnbomitem == stritem
                          where a.DictType.Contains("reason_type_m")
                          select new
                          {
                              a.DictLabel,
                              a.DictValue
                          };
            q_Model = q_Model.OrderBy(u => u.DictValue);

            // 绑定到下拉列表（启用模拟树功能）
            // 绑定到下拉列表（启用模拟树功能）
            var qs = q_Model.Select(E => new { E.DictValue }).ToList().Distinct();
            Cc_Line.DataTextField = "linename";
            Cc_Line.DataValueField = "linename";
            Cc_Line.DataSource = qs;
            Cc_Line.DataBind();
            this.Cc_Line.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void CheckData()
        {
            //判断修改内容
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Complaint current = DB.Qm_Complaints.Find(id);
            //decimal cQcpd005 = current.Qcpd005;
            string Cc_Ddescription = current.Cc_Ddescription;
            string Cc_Reasons = current.Cc_Reasons;

            if (this.Cc_Ddescription.Text == Cc_Ddescription)
            {
                if (this.Cc_Reasons.Text == Cc_Reasons)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
                    //Alert alert = new Alert();
                    //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
                    //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);

                    //"Information" Text = "消息"
                    //"Warning" Text = "警告"
                    //"Question" Text = "问题"
                    // "Error" Text = "错误"
                    //"Success" Text = "成功"
                    //alert.IconFont = IconFont.Warning;
                    //alert.Icon = Icon.Error;
                    //alert.IconUrl = "~/Lf_Resources/images/warning.png";

                    //alert.Target = Target.Top;
                    //Alert.ShowInTop();
                    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
            }

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
            item.Cc_Line = Cc_Line.SelectedItem.Text;
            item.Cc_ProcessDate = Cc_ProcessDate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Cc_Ddescription = Cc_Ddescription.Text;
            item.Cc_Reasons = Cc_Reasons.Text;
            item.Cc_Operator = Cc_Operator.Text;
            item.Cc_Station = Cc_Station.Text;
            item.Cc_Lot = Cc_Lot.Text;
            item.Cc_CorrectActions = Cc_CorrectActions.Text;

            item.Remark = remark.Text;
            item.p1dModifier = GetIdentityName();
            item.p1dModifyDate = DateTime.Now;

            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = Cc_IssuesNo.Text;
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

        #endregion Events
    }
}