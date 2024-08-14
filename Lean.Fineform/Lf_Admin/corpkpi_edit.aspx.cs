using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class corpkpi_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreCorpkpiEdit";
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
            Adm_Corpkpi current = DB.Adm_Corpkpis.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            //Guid.NewGuid() = current.GUID;
            tbxCorpAbbrName.Text = current.CorpAbbrName;
            tbxCorpAnnual.Text = current.CorpAnnual;
            tbxCorpTarget_CN.Text = current.CorpTarget_CN;
            tbxCorpTarget_EN.Text = current.CorpTarget_EN;
            tbxCorpTarget_JA.Text = current.CorpTarget_JA;

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
            string BeforeModi = current.CorpAbbrName + "," + current.CorpAnnual;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "目标管理", "目标信息修改", OperateNotes);
        }

        #endregion Page_Load

        #region Events

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));

            Adm_Corpkpi item = DB.Adm_Corpkpis
                .Where(u => u.GUID == id).FirstOrDefault();

            item.CorpAbbrName = tbxCorpAbbrName.Text;
            //item.EnName = TXTentabbrName.Text;
            //item.ShortName = TXTentShortName.Text;
            item.CorpAnnual = tbxCorpAnnual.Text;
            item.CorpTarget_CN = tbxCorpTarget_CN.Text;
            item.CorpTarget_EN = tbxCorpTarget_EN.Text;
            item.CorpTarget_JA = tbxCorpTarget_JA.Text;

            item.IsDeleted = 0;

            item.Remark = "";
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Adm_Institutions.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = tbxCorpAbbrName.Text + "," + tbxCorpAnnual.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "机构管理", "机构信息修改", OperateNotes);
        }

        private void CheckData()
        {
            ////判断修改内容
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Adm_Corpkpi current = DB.Adm_Corpkpis.Find(id);
            //decimal cQcpd005 = current.Qcpd005;
            string checkCorpTarget_CN = current.CorpTarget_CN;
            string checkCorpTarget_EN = current.CorpTarget_EN;
            string checkCorpTarget_JA = current.CorpTarget_JA;

            if (this.tbxCorpTarget_CN.Text == checkCorpTarget_CN || this.tbxCorpTarget_EN.Text == checkCorpTarget_EN || this.tbxCorpTarget_JA.Text == checkCorpTarget_JA)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "说明", MessageBoxIcon.Information);
            }
            //if (this.tbxCorpTarget_EN.Text == checkCorpTarget_EN)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "说明", MessageBoxIcon.Information);
            //}
            //if (this.tbxCorpTarget_JA.Text == checkCorpTarget_JA)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "说明", MessageBoxIcon.Information);
            //}

            //判断重复
            //string InputData = tbxCorpAbbrName.Text.Trim()+ tbxCorpAnnual.Text.Trim();

            //Adm_Corpkpi redata = DB.Adm_Corpkpis.Where(u => u.CorpAbbrName+u.CorpAnnual== InputData).FirstOrDefault();

            //if (redata != null)
            //{
            //    Alert.ShowInTop("目标信息,目标< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                //CheckData();
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