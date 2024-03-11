using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_order_new : PageBase
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
                return "CoreOrderNew";
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

            BindDDLModel();
            Porderdate.SelectedDate = DateTime.Now;
            Porderdate.MinDate = DateTime.Now;

        }

        private void BindDDLModel()
        {


            var list = (from c in DB.Pp_Manhours
                        group c by new { c.Promodel } into g
                        //where c.Promodel.Contains(this.MC003.SelectedItem.Text)
                        select new { Peo = g.Key }).ToList();

            //var list = (from emp in DB.Pp_Manhours
            //            group emp by new { emp.Promodel} into g
            //            select new { Peo = g.Key }).ToList();

            //3.2.将数据绑定到下拉框
            MC003.DataSource = list;
            MC003.DataTextField = "Promodel";
            MC003.DataValueField = "Promodel";
            MC003.DataBind();



        }

        private void BindDDLItem()
        {
            var list = (from c in DB.Pp_Manhours
                        where c.Promodel.Contains(this.MC003.SelectedItem.Text)
                        select c.Proitem).ToList();

            //3.2.将数据绑定到下拉框
            Porderhbn.DataSource = list;
            Porderhbn.DataTextField = "Proitem";
            Porderhbn.DataValueField = "Proitem";
            Porderhbn.DataBind();


        }


        #endregion

        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {

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

            string InputData = Porderdate.Text.Trim() + Porderhbn.Text.Trim() + Porderlot.Text.Trim();


            Pp_Order redata = DB.Pp_Orders.Where(u => u.Porderdate + u.Porderhbn + u.Porderlot == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop("基本信息,相同订单< " + InputData + ">已经存在！修改即可", "错误提示", MessageBoxIcon.Error);
                return;
            }

            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        private void CheckDDLData()
        {

            if (this.MC003.SelectedItem.Text == "请选择")
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }
            if (this.Porderhbn.SelectedItem.Text == "请选择")
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }

            CheckData();
        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            Pp_Order item = new Pp_Order();

            item.Porderno = this.Porderno.Text.ToUpper();
            item.Porderhbn = this.Porderhbn.SelectedText;
            item.Porderlot = this.Porderlot.Text.ToUpper(); 

            item.Porderqty = decimal.Parse(this.Porderqty.Text);
            item.Porderreal = 0;
            item.Porderdate = this.Porderdate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Porderroute = this.Porderroute.Text;
            item.Porderserial = this.Porderserial.Text.ToUpper();
            item.GUID = Guid.NewGuid();
            // 添加所有用户

            item.Remark = Remark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Orders.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = this.Porderdate.SelectedDate.Value.ToString("yyyyMMdd") + "," + this.Porderhbn.SelectedText + "," + this.MC003.SelectedText + "," + this.Porderqty.Text.ToUpper() + "," + this.MC005.Text + "," + this.Porderlot.Text + "," + this.Porderserial.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "订单新增", OperateNotes);


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
            CheckDDLData();
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

        protected void MC003_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDDLItem();

        }

        protected void Porderhbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Porderhbn.SelectedIndex != -1 && Porderhbn.SelectedIndex != 0)
            {

                Pp_Manhour item = DB.Pp_Manhours

                    .Where(u => u.Proitem == this.Porderhbn.SelectedText).FirstOrDefault();
                //品名，st
                this.MC009.Text = item.Protext;
                this.MC005.Text = item.Prodesc;
                //仕向け地
                //Regex.IsMatch(this.MC008.SelectedText.Substring(8,2), @"^[+-]?\d*[.]?\d*$");
                //string mc = this.MC008.SelectedText.Substring(8, 2);
                //int inx = mc.LastIndexOf(@"^[+-]?\d*[.]?\d*$");
                //if (Regex.IsMatch(this.MC008.SelectedText.Substring(8, 2), @"^[+-]?\d*[.]?\d*$")==true)
                //{
                //    pqOrigin items = DB.pqOrigins

                //     .Where(u => u.XB002 == this.MC008.SelectedText.Substring(7, 2)).FirstOrDefault();
                //    this.MC005.Text = items.XB001;
                //}
                //else
                //{
                //    pqOrigin itema = DB.pqOrigins

                //    .Where(u => u.XB002 == this.MC008.SelectedText.Substring(8, 2)).FirstOrDefault();
                //    this.MC005.Text = itema.XB001;
                //}

            }
        }
        #endregion


    }
}
