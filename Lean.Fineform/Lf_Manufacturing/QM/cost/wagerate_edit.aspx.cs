using Fine.Lf_Business.Models.QM;
using FineUIPro;
using System;
//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
namespace Fine.Lf_Manufacturing.QM.cost
{
    public partial class wagerate_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreWagesEdit";
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
            //mocToDropDownList();

            BindTxtData();

        }

        #region BindingData

        private void BindTxtData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id =Guid.Parse(GetQueryValue("GUID"));
            Qm_Wagerate current = DB.Qm_Wagerates.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }



            Qcsd001.Text = current.Qcsd001;//年月
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            Qcsd002.Text = current.Qcsd002;//工厂
            Qcsd003.Text = current.Qcsd003;//币种
            Qcsd004.Text = current.Qcsd004.ToString();//月销售额
            Qcsd005.Text = current.Qcsd005.ToString();//总工作时日
            Qcsd006.Text = current.Qcsd006.ToString();//直接人员赁率
            Qcsdrec.Text = current.Qcsdrec.ToString();//财务担当
            Qcsd007.Text = current.Qcsd007.ToString();//直接人员
            Qcsd008.Text = current.Qcsd008.ToString();//直接人员加班
            Qcsd009.Text = current.Qcsd009.ToString();//直接人员工资
            Qcsd010.Text = current.Qcsd010.ToString();//间接人员赁率
            Qcsd011.Text = current.Qcsd011.ToString();//间接人员
            Qcsd012.Text = current.Qcsd012.ToString();//间接人员加班
            Qcsd013.Text = current.Qcsd013.ToString();//间接人员工资


            // 添加所有用户



            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);
            //修改前日志          


            string BeforeModi = current.Qcsd001 + "," + current.Qcsd002 + "," + current.Qcsd003 + "," + current.Qcsd004 + "," + current.Qcsd005 + "," + current.Qcsd006 + "," + Qcsdrec.Text + "," + current.Qcsd008 + "," + current.Qcsd009 + "," + current.Qcsd010 + "," + current.Qcsd011 + "," + current.Qcsd012 + "," + current.Qcsd013;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "财务管理", "销售数据修改", OperateNotes);
        }

        #endregion


        #endregion

        #region Events
        //判断修改内容,判断重复
        private void CheckData()
        {

            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Wagerate current = DB.Qm_Wagerates.Find(id);
            string modi001 = current.Qcsd001;
            string modi002 = current.Qcsd005.ToString();
            string modi003 = current.Qcsd004.ToString();
            string modi004 = current.Qcsd006.ToString();
            string modi005 = current.Qcsd010.ToString();

            if (this.Qcsd001.Text == modi001)
            {
                if (decimal.Parse(this.Qcsd005.Text) == decimal.Parse(modi002))
                {
                    if (decimal.Parse(this.Qcsd004.Text) == decimal.Parse(modi003))
                    {
                        if (decimal.Parse(this.Qcsd006.Text) == decimal.Parse(modi004))
                        {
                            if (decimal.Parse(this.Qcsd010.Text) == decimal.Parse(modi005))
                            {
                                //Alert alert = new Alert();
                                //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
                                //alert.IconUrl = "~/Lf_Resources/images/success.png";
                                //alert.Target = Target.Top;

                                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }

                //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }


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


            //proCheckInsClass Redata = DB.proCheckInsClasss.Where(u => u.LC001 == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("数据,检验方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}

        }
        private void SaveItem()//新增质量控制数据
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Wagerate item = DB.Qm_Wagerates

                .Where(u => u.GUID == id).FirstOrDefault();
            item.Qcsd001 = Qcsd001.Text;
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
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string AfterModi = Qcsd001.Text + "," + Qcsd002.Text + "," + Qcsd003.Text + "," + Qcsd004.Text + "," + Qcsd005.Text + "," + Qcsd006.Text + "," + Qcsd007.Text + "," + Qcsd008.Text + "," + Qcsd009.Text + "," + Qcsd010.Text + "," + Qcsd011.Text + "," + Qcsd012.Text + "," + Qcsd013.Text;

            string OperateType = "修改";
            string OperateNotes = "beEdit* " + AfterModi + " *beEdit 的记录已经被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "财务管理", "销售数据修改", OperateNotes);


        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckData();
            SaveItem();
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
