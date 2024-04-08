using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.Master
{
    public partial class Pp_manhour_new : PageBase
    {
        //
        public string DDLValue;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreManhourNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                //this.Orderguid.Text = Guid.NewGuid().ToString();
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

            Prodate.SelectedDate = DateTime.Now;

            this.Proplnt.Text = "C100";
            this.Propset.Text = "P";
            this.Proset.Text = "MIN";
        }

        #endregion Page_Load

        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {
            //int id = GetQueryIntValue("id");
            //proManhour current = DB.Pp_Manhours.Find(id);
            //Cmc001 = current.Prodate;
            //Cmc002 = current.Proitem;
            //Cmc004 = current.Proshort.ToString();
            //Cmc005 = current.Prost.ToString();
            //Cmc008 = current.Prodesc;

            //if (this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            //{
            //    if (this.Proitem.Text == Cmc002)
            //    {
            //        if (this.Proshort.Text == Cmc004)
            //        {
            //            if (this.Prost.Text == Cmc005)
            //            {
            //                if (this.Prodesc.Text == Cmc008)
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

            string InputData = Proitem.Text.Trim();

            Pp_Manhour redata = DB.Pp_Manhours.Where(u => u.Proitem == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
                return;
            }
            //保存数据
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Pp_Manhour item = new Pp_Manhour();
            item.Prodate = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proplnt = this.Proplnt.Text.ToUpper(); ;
            item.Proitem = this.Proitem.Text.ToUpper();
            item.Prowcname = this.Prowcname.Text.ToUpper();
            item.Promodel = this.Promodel.Text.ToUpper(); ;
            item.Protext = this.Protext.Text.ToUpper(); ;
            item.Prowctext = this.Prowctext.Text.ToUpper(); ;
            item.Proshort = decimal.Parse(this.Proshort.Text);
            item.Propset = this.Propset.Text.ToUpper(); ;
            item.Prorate = decimal.Parse(Prorate.Text);
            item.Prost = decimal.Parse(Prost.Text);
            item.Proset = Proset.Text;
            item.Prodesc = Prodesc.Text.ToUpper(); ;

            item.GUID = Guid.NewGuid();
            // 添加所有用户

            item.Remark = Remark.Text;
            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Manhours.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + this.Proitem.Text + "," + this.Prowcname.Text + "," + this.Promodel.Text.ToUpper();
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "工产工时新增", OperateNotes);
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
        }

        //private void SaveOther()
        //{
        //    Pp_SapManhour item = new Pp_SapManhour();
        //    item.D_SAP_ZPBLD_Z001 = this.Proplnt.Text.ToUpper();
        //    item.D_SAP_ZPBLD_Z002 = this.Proitem.Text.ToUpper();
        //    item.D_SAP_ZPBLD_Z003 = this.Prowcname.Text.ToUpper();
        //    item.D_SAP_ZPBLD_Z004 = this.Prowctext.Text.ToUpper();
        //    item.D_SAP_ZPBLD_Z005 = decimal.Parse(this.Proshort.Text);
        //    item.D_SAP_ZPBLD_Z006 = this.Propset.Text.ToUpper();
        //    item.D_SAP_ZPBLD_Z007 = decimal.Parse(Prost.Text);
        //    item.D_SAP_ZPBLD_Z008 = Proset.Text;
        //    item.GUID = Guid.NewGuid();
        //    // 添加所有用户
        //    item.Remark = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd");
        //    item.CreateDate = DateTime.Now;
        //    item.Creator = GetIdentityName();
        //    DB.Pp_SapManhours.Add(item);
        //    DB.SaveChanges();
        //}
        protected void Proitem_TextChanged(object sender, EventArgs e)
        {
            StringHelper.strToUpper(Proitem.Text);
        }

        protected void Prowcname_TextChanged(object sender, EventArgs e)
        {
            StringHelper.strToUpper(Prowcname.Text);
        }

        protected void Promodel_TextChanged(object sender, EventArgs e)
        {
            StringHelper.strToUpper(Promodel.Text);
        }

        protected void Protext_TextChanged(object sender, EventArgs e)
        {
            StringHelper.strToUpper(Protext.Text);
        }

        protected void Prowctext_TextChanged(object sender, EventArgs e)
        {
            StringHelper.strToUpper(Prowctext.Text);
        }

        #endregion Events
    }
}