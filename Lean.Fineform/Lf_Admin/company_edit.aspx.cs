using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Admin
{
    public partial class company_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreCompanyEdit";
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
            //labResult.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindData();
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Adm_Institution current = DB.Adm_Institutions.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            //Guid.NewGuid() = current.GUID;
            DDLentCategory.SelectedItem.Text = current.Category;
            TXTentabbrName.Text = current.EnName;
            TXTentShortName.Text = current.ShortName;
            TXTentFullName_cn.Text = current.FullName;
            TXTentFullName_en.Text = current.EnFullName;
            DDLentNature.SelectedItem.Text = current.Nature;
            TXTentOuterPhone.Text = current.OuterPhone;
            TXTentInnerPhone.Text = current.InnerPhone;
            TXTentFax.Text = current.Fax;
            TXTentPostalcode.Text = current.Postalcode;
            TXTentEmail.Text = current.Email;
            TXTentOrgCode.Text = current.OrgCode;
            TXTentCorporate.Text = current.Corporate;
            ProvinceId.Text = current.ProvinceId;
            CityId.Text = current.CityId;
            CountyId.Text = current.CountyId;
            TownId.Text = current.TownId;
            VillageId.Text = current.VillageId;
            TxtentAddress_cn.Text = current.Address;
            TxtentAddress_en.Text = current.EnAddress;
            TXTentWebUrl.Text = current.WebAddress;
            DPKentFoundedTime.SelectedDate = current.FoundedTime;
            TXTentBusinessScope.Text = current.BusinessScope;
            NUMSortCode.Text = current.SortCode.ToString();

            TXTentSlogan_cn.Text = current.Slogan;
            TXTentSlogan_en.Text = current.EnSlogan;
            TXTentSlogan_ja.Text = current.JpSlogan;

            //DPKentFoundedTime.SelectedDate =  DateTime.ParseExact(current.entFoundedTime.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            //TXTentabbrName.Text = current.entBusinessScope;
            //NUMSortCode.Text = current.SortCode.ToString();
            //TXTentSlogan_cn.Text = current.entSlogan_cn;
            //TXTentSlogan_en.Text = current.entSlogan_en;
            //TXTentSlogan_ja.Text = current.entSlogan_ja;
            //TXTentTarget_cn.Text = current.entTarget_cn;
            //TXTentTarget_en.Text = current.entTarget_en;
            //TXTentTarget_ja.Text = current.entTarget_ja;

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
            string BeforeModi = current.EnName;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "机构管理", "机构信息修改", OperateNotes);
        }

        #endregion Page_Load

        #region Events

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Adm_Institution item = DB.Adm_Institutions
                 .Where(u => u.GUID == id).FirstOrDefault();

            item.Category = DDLentCategory.SelectedItem.Text;
            //item.EnName = TXTentabbrName.Text;
            //item.ShortName = TXTentShortName.Text;
            item.FullName = TXTentFullName_cn.Text;
            item.EnFullName = TXTentFullName_en.Text;
            item.Nature = DDLentNature.SelectedItem.Text;
            item.OuterPhone = TXTentOuterPhone.Text;
            item.InnerPhone = TXTentInnerPhone.Text;
            item.Fax = TXTentFax.Text;
            item.Postalcode = TXTentPostalcode.Text;
            item.Email = TXTentEmail.Text;
            //item.OrgCode = TXTentOrgCode.Text;
            item.Corporate = TXTentCorporate.Text;
            item.ProvinceId = ProvinceId.Text;
            item.CityId = CityId.Text;
            item.CountyId = CountyId.Text;
            item.TownId = TownId.Text;
            item.VillageId = VillageId.Text;
            item.Address = TxtentAddress_cn.Text;
            item.EnAddress = TxtentAddress_en.Text;
            item.WebAddress = TXTentWebUrl.Text;
            item.FoundedTime = DateTime.Parse(DPKentFoundedTime.SelectedDate.Value.ToString());
            item.BusinessScope = TXTentBusinessScope.Text;
            item.SortCode = int.Parse(NUMSortCode.Text);

            item.Slogan = TXTentSlogan_cn.Text;
            item.EnSlogan = TXTentSlogan_en.Text;
            item.JpSlogan = TXTentSlogan_ja.Text;

            item.isDeleted = 0;
            item.isEnabled = 0;
            item.Remark = "";
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Adm_Institutions.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = TXTentabbrName.Text;
            string OperateType = "修改";
            string OperateNotes = "New* " + Contectext + " New* 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "机构管理", "机构信息修改", OperateNotes);
        }

        private void CheckData()
        {
            ValidatorTools.IsTelePhoneNumber(TXTentOuterPhone.Text);
            ValidatorTools.IsTelePhoneNumber(TXTentFax.Text);
            ValidatorTools.IsInteger(TXTentInnerPhone.Text);
            ValidatorTools.IsInteger(NUMSortCode.Text);

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
            string InputData = TXTentabbrName.Text.Trim();

            Adm_Institution redata = DB.Adm_Institutions.Where(u => u.EnName == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("机构信息,机构< " + InputData + ">已经存在！修改即可");
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