using Fine.Lf_Business.Models.MM;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lc_MM
{

    public partial class material_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMaterialEdit";
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
            Mm_Material current = DB.Mm_Materials.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }




           isDate.SelectedDate = DateTime.ParseExact(current.isDate, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            Plnt.Text = current.Plnt.ToString();
            MatItem.Text = current.MatItem.ToString();
            MatDescription.Text = current.MatDescription.ToString();
            Currency.Text = current.Currency.ToString();
            EvaluationType.Text = current.EvaluationType.ToString();
            PriceUnit.Text = current.PriceUnit.ToString();
            MovingAvg.Text = current.MovingAvg.ToString();



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
            string BeforeModi = current.isDate + "," + current.MatItem + ","+ current.MovingAvg.ToString();
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "移动价格修改", OperateNotes);
        }







        #endregion

        #region Events

        private void CheckData()
        {
            //判断修改内容
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Mm_Material current = DB.Mm_Materials.Find(id);
            //decimal cQcpd005 = current.Qcpd005;
            string checkdata1 = current.MovingAvg.ToString();


            if (this.MovingAvg.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
                //Alert alert = new Alert();
                //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
                //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);
                //alert.Target = Target.Top;
                //Alert.ShowInTop();
                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
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
            Mm_Material item = DB.Mm_Materials

                .Where(u => u.GUID == id).FirstOrDefault();

            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.isDate = isDate.SelectedDate.Value.ToString("yyyyMMdd");
            item.MovingAvg =decimal.Parse( MovingAvg.Text);



            // 添加所有用户


            item.Remark = remark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = isDate.SelectedDate.Value.ToString("yyyyMMdd") + ","+MatItem.Text+"," + MovingAvg.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "移动价格修改", OperateNotes);


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
