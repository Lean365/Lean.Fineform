using Fine.Lf_Business.Models.QM;
using FineUIPro;
using System;
//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
namespace Fine.Lf_Manufacturing.QM.cost
{
    public partial class wagerate_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreWagesNew";
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
            this.Qcsd001.SelectedDate = DateTime.Now;
            this.Qcsd002.Text = "C100";
            this.Qcsd003.Text = "CNY";
            this.Qcsdrec.Text= GetIdentityName();
            this.Qcsd005.Text = "21.75";
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

        }

        #region BindData
        #endregion
        #endregion

        #region Events
        //判断修改内容||判断重复
        private void CheckData()
        {
            //int id = GetQueryIntValue("id");
            //proCheckInsClass current = DB.proCheckInsClasss.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.LC001;
            //string checkdata2 = current.LC002;
            //string checkdata3 = current.LC003;

            //if (this.LC001.Text == checkdata1 || this.LC002.Text == checkdata2 || this.LC003.Text == checkdata3)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
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
            //                    alert.IconUrl = "~/Lf_Resources/images/success.png";
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
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            //string InputData = Qcpd003.Text.Trim();


            //proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            //if (redata != null)
            //{
            //    Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
            //string InputData = LC001.Text.Trim();


            //proSalesdata Redata = DB.proSalesdatas.Where(u => u.Qcsd001 == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("数据,检验方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}

            string InputData = Qcsd001.SelectedDate.Value.ToString("yyyyMM").Trim();


            Qm_Wagerate Redata = DB.Qm_Wagerates.Where(u => u.Qcsd001 == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("财务数据,月< " + InputData + ">已经存在！修改即可");
                return;
            }
            else
            {
                SaveItem();
            }


        }
        private void SaveItem()//新增质量控制数据
        {


            Qm_Wagerate item = new Qm_Wagerate();
            item.Qcsd001 = Qcsd001.SelectedDate.Value.ToString("yyyyMM");
            item.Qcsd002 = Qcsd002.Text;
            item.Qcsd003 = Qcsd003.Text;
            item.Qcsd004 = decimal.Parse(Qcsd004.Text);
            item.Qcsd005 = decimal.Parse(Qcsd005.Text);
            item.Qcsd006 = decimal.Parse(Qcsd006.Text);
            item.Qcsd007 = decimal.Parse(Qcsd007.Text);
            item.Qcsd008 = decimal.Parse(Qcsd008.Text);
            item.Qcsd009 = decimal.Parse(Qcsd009.Text);
            item.Qcsd010 = decimal.Parse(Qcsd010.Text);
            item.Qcsd011 = decimal.Parse(Qcsd011.Text);
            item.Qcsd012 = decimal.Parse(Qcsd012.Text);
            item.Qcsd013 = decimal.Parse(Qcsd013.Text);
            item.Qcsdrec = Qcsdrec.Text;
            item.GUID = Guid.NewGuid();// Qcsdguid.Text;
            item.Creator = GetIdentityName();
            item.CreateTime = DateTime.Now;
            DB.Qm_Wagerates.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Qcsd001.Text + "," + Qcsd002.Text + "," + Qcsd003.Text + "," + Qcsd004.Text + "," + Qcsd005.Text + "," + Qcsd006.Text + "," + Qcsd007.Text + "," + Qcsd008.Text + "," + Qcsd009.Text + "," + Qcsd010.Text + "," + Qcsd011.Text + "," + Qcsd012.Text + "," + Qcsd013.Text;
            string OperateType ="新增";
            string OperateNotes = "Add* " + Newtext + " *Add 的记录已新增";

            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "工资率数据新增", OperateNotes);
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckData();





            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        public void dQcsd006()
        {
            if (Qcsd005.Text != "" && Qcsd007.Text != "" && Qcsd008.Text != "" && Qcsd009.Text != "" && Qcsd005.Text != "0" && Qcsd007.Text != "0" && Qcsd008.Text != "0" && Qcsd009.Text != "0")
            {

                decimal attime = decimal.Parse(this.Qcsd005.Text) * 480 * decimal.Parse(this.Qcsd007.Text);
                decimal overtime = 0;
                this.Qcsd006.Text = Convert.ToDecimal(decimal.Parse(this.Qcsd009.Text) / (attime + overtime) * 2).ToString("0.00");
            }
        }
        public void dQcsd010()
        {
            if (Qcsd005.Text != "" && Qcsd011.Text != "" && Qcsd012.Text != "" && Qcsd013.Text != "" && Qcsd005.Text != "0" && Qcsd011.Text != "0" && Qcsd012.Text != "0" && Qcsd013.Text != "0")
            {

                decimal attime = decimal.Parse(this.Qcsd005.Text) * 480 * decimal.Parse(this.Qcsd011.Text);
                decimal overtime = 0;
                this.Qcsd010.Text = Convert.ToDecimal(decimal.Parse(this.Qcsd013.Text) / (attime + overtime) * 3).ToString("0.00");

            }
        }


        protected void Qcsd007_TextChanged(object sender, EventArgs e)
        {
            dQcsd006();
        }
        protected void Qcsd005_TextChanged(object sender, EventArgs e)
        {
            dQcsd006();
            dQcsd010();
        }
        protected void Qcsd008_TextChanged(object sender, EventArgs e)
        {
            dQcsd006();
        }

        protected void Qcsd009_TextChanged(object sender, EventArgs e)
        {
            dQcsd006();
        }

        protected void Qcsd011_TextChanged(object sender, EventArgs e)
        {
            dQcsd010();

        }

        protected void Qcsd012_TextChanged(object sender, EventArgs e)
        {
            dQcsd010();
        }

        protected void Qcsd013_TextChanged(object sender, EventArgs e)
        {
            dQcsd010();
        }

        #endregion


    }
}