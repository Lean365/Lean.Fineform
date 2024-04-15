using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_smt_daily_new : PageBase
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
                return "CoreP2DSmtOutputNew";
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

            string InputData = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + this.Proline.Text + "," + this.Propcba.Text;

            Pp_P2d_Smt_Output redata = DB.Pp_P2d_Smt_Outputs.Where(u => u.Prodate + "," + u.Proline + "," + u.Propcba == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("SMT实绩,物料< " + InputData + ">已经存在！修改即可");
                return;
            }
            //保存数据
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Pp_P2d_Smt_Output item = new Pp_P2d_Smt_Output();
            item.GUID = Guid.NewGuid();
            item.Prodate = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Promodel = this.Promodel.Text.ToUpper();
            item.Proline = this.Proline.Text.ToUpper();
            item.Proitem = this.Proitem.Text.ToUpper();
            item.Proitemtext = this.Proitemtext.Text.ToUpper();
            item.Propcbitem = this.Propcbitem.Text.ToUpper();
            item.Propcbtext = this.Propcbtext.Text.ToUpper();
            item.Propcbshort = int.Parse(this.Propcbshort.Text);
            item.Propcba = this.Propcba.Text.ToUpper();
            item.Propcbatext = this.Propcbatext.Text.ToUpper();
            item.Proconvertshort = int.Parse(this.Proconvertshort.Text);
            item.Promachineshort = int.Parse(Promachineshort.Text);
            item.Promanualshort = int.Parse(Promanualshort.Text);
            item.Proplanqty = int.Parse(Proplanqty.Text);
            item.Prorealqty = int.Parse(Prorealqty.Text);
            item.Prosmtshortqty = int.Parse(Prosmtshortqty.Text);
            item.Promachineshortqty = int.Parse(Promachineshortqty.Text);
            item.UDF51 = 0;
            item.UDF52 = 0;
            item.UDF53 = 0;
            item.UDF54 = 0;
            item.UDF55 = 0;
            item.UDF56 = 0;
            item.isDeleted = 0;
            // 添加所有用户

            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_P2d_Smt_Outputs.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + this.Proline.Text + "," + this.Propcbitem.Text + "," + this.Prorealqty.Text.ToUpper();
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
        protected void Promodel_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Proline_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Propcbitem_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Progpcb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion Events
    }
}