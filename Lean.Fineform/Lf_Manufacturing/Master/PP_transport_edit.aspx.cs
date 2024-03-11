using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Linq;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_transport_edit : PageBase
    {

        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTransportEdit";
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

            Guid id =Guid.Parse( GetQueryValue("GUID"));
            Pp_Transport current = DB.Pp_Transports.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }



            Transportype.Text = current.Transportype.ToString();
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            Transportcntext.Text = current.Transportcntext;
            Transportentext.Text = current.Transportentext;
            Transportjptext.Text = current.Transportjptext;
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
            string BeforeModi = current.Transportype + "," + current.Transportcntext;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "运输方式修改", OperateNotes);
        }







        #endregion


        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {


            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Transport current = DB.Pp_Transports.Find(id);
            string modi001 = current.Transportype;
            string modi002 = current.Transportcntext;
            string modi003 = current.Transportentext;
            string modi004 = current.Transportjptext;
            string modi005 = current.Transportjptext;

            if (this.Transportype.Text == modi001)
            {
                if (this.Transportcntext.Text == modi002)
                {
                    if (this.Transportentext.Text == modi003)
                    {
                        if (this.Transportjptext.Text == modi004)
                        {
                            if (this.Transportjptext.Text == modi005)
                            {
                                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Noedit, "警告提示", MessageBoxIcon.Information);
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
            //string InputData = Proreasontext.Text.Trim();


            //proScannerDest Redata = DB.proScannerDest.Where(u => u.Proreasontext == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("数据,检验方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}

        }

        //字段赋值，保存
        private void SaveItem()//修改品管类别
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Transport item = DB.Pp_Transports

                .Where(u => u.GUID == id).FirstOrDefault();

            item.Transportcntext = Transportcntext.Text;
            item.Transportentext = Transportentext.Text;
            item.Transportjptext = Transportjptext.Text;

            // 添加所有用户
            item.Remark = remark.Text;
            // 添加所有用户
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();

            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = Transportype.Text.Trim() + "," + Transportcntext.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "运输方式修改", OperateNotes);
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
            CheckData();
            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion









    }
}
