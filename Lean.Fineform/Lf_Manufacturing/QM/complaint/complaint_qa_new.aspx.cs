using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.QM.complaint
{
    public partial class complaint_qa_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreComplaintQANew";
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
            //labResult.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            Cc_ReceivingDate.SelectedDate = DateTime.Now;
            Cc_ReceivedDate.SelectedDate = DateTime.Now;
            Cc_Order.Text = "--";
            Cc_Serialno.Text = "--";
            Cc_DefectsQty.Text = "1";
            BindissueNoData();
            BindDDLCustomer();
            BindDDLModel();
        }

        //发行NO
        public void BindissueNoData()
        {
            string sdate = DateTime.Now.ToString("yyyyMMdd");

            var q = from a in DB.Qm_Complaints
                    where a.Cc_ReceivingDate.Contains(sdate)
                    group a by a.Cc_ReceivingDate into p
                    select new
                    {
                        issueNo = p.Max(x => x.Cc_DocNo),
                    };
            var qs = q.Distinct().ToList();

            if (qs.Any())

            {
                Cc_DocNo.Text = (UInt64.Parse(qs[0].issueNo) + 1).ToString();
            }
            else
            {
                Cc_DocNo.Text = sdate + "001";
            }
        }

        public void BindDDLCustomer()
        {
            var q_Customer = from a in DB.Sd_Customers
                             select new
                             {
                                 a.Customer_ID,
                                 a.Customer_Abbr
                             };

            // 绑定到下拉列表（启用模拟树功能）
            // 绑定到下拉列表（启用模拟树功能）
            var qs = q_Customer.Select(E => new { E.Customer_ID, E.Customer_Abbr }).ToList().Distinct();
            Cc_Customer.DataTextField = "Customer_Abbr";
            Cc_Customer.DataValueField = "Customer_Abbr";
            Cc_Customer.DataSource = qs;
            Cc_Customer.DataBind();
            this.Cc_Customer.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        public void BindDDLModel()
        {
            var q_Model = from a in DB.Pp_Manhours
                          join b in DB.Mm_Materials on a.Proitem equals b.MatItem
                          where b.MatType.CompareTo("FERT") == 0
                          select new
                          {
                              Model = a.Promodel
                          };
            q_Model = q_Model.OrderBy(u => u.Model);

            // 绑定到下拉列表（启用模拟树功能）
            // 绑定到下拉列表（启用模拟树功能）
            var qs = q_Model.Select(E => new { E.Model }).ToList().Distinct();
            Cc_Model.DataTextField = "Model";
            Cc_Model.DataValueField = "Model";
            Cc_Model.DataSource = qs;
            Cc_Model.DataBind();
            this.Cc_Model.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

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

        #endregion Page_Load

        #region Events

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Qm_Complaint item = new Qm_Complaint();

            item.GUID = Guid.NewGuid();
            // 添加所有用户
            item.Cc_DocNo = Cc_DocNo.Text;
            item.Cc_IssuesNo = Cc_IssuesNo.Text.ToUpper();
            item.Cc_Customer = Cc_Customer.SelectedValue.ToString();
            item.Cc_Model = Cc_Model.SelectedItem.Text;
            item.Cc_Item = "-";
            item.Cc_Region = "-";
            item.Cc_DefectNotes = "-";
            item.Cc_Rootcauseanalysis = "-";
            item.Cc_Order = Cc_Order.Text;
            item.Cc_ReceivingDate = Cc_ReceivingDate.Text;
            item.Cc_DefectsQty = int.Parse(Cc_DefectsQty.Text);
            item.Cc_Issues = Cc_Issues.Text;
            item.Cc_Serialno = Cc_Serialno.Text;

            Refdoc();
            if (!string.IsNullOrEmpty(txtRefdoc))
            {
                item.Cc_Reference = txtRefdoc;
            }
            else
            {
                item.Cc_Reference = "";
            }
            //item.Cc_Reference = Cc_Reference.Text;

            item.qaModifier = GetIdentityName();
            item.qaModifyDate = DateTime.Now;
            item.Cc_Ddescription = "";
            item.Cc_Reasons = "";
            item.Cc_Operator = "";
            item.Cc_Station = "";
            item.Cc_Lot = "";
            item.Cc_CorrectActions = "";
            //item.p1dModifier = "";
            //item.p1dModifyDate = "";
            item.Cc_Discover = GetIdentityName();
            item.Cc_ReceivedDate = Cc_ReceivedDate.Text;
            item.UDF01 = "";
            item.UDF02 = "";
            item.UDF03 = "";
            item.UDF04 = "";
            item.UDF05 = "";
            item.UDF06 = "";
            item.UDF51 = 0;
            item.UDF52 = 0;
            item.UDF53 = 0;
            item.UDF54 = 0;
            item.UDF55 = 0;
            item.UDF56 = 0;
            item.isDeleted = 0;
            item.Remark = remark.Text;

            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Qm_Complaints.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = Cc_IssuesNo.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "客诉信息", "记录新增", OperateNotes);
        }

        private void CheckData()
        {
            ////判断修改内容
            if (this.Cc_Customer.SelectedIndex == 0 || this.Cc_Customer.SelectedIndex == -1)
            {
                Alert.ShowInTop("没有选择客户！");
                return;
            }
            if (this.Cc_Model.SelectedIndex == 0 || this.Cc_Model.SelectedIndex == -1)
            {
                Alert.ShowInTop("没有选择客户！");
                return;
            }
            //判断重复
            string InputData = Cc_IssuesNo.Text.Trim();

            Qm_Complaint redata = DB.Qm_Complaints.Where(u => u.Cc_IssuesNo == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("客诉信息,IssuesNo< " + InputData + ">已经存在！修改即可");
                return;
            }
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

        #endregion Events
    }
}