using System;
using System.Data;
using System.Linq;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class waste_cost_edit : PageBase
    {
        //日志配置文件调用
        // private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreWasteCostEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public Guid strGuid;

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

            //获取通过窗体传递的值
            string strtransmit = GetQueryValue("GUID");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');
            strGuid = Guid.Parse(strgroup[0].ToString().Trim());

            BindData();
        }

        #region BindDdlData

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Qm_Waste current = DB.Qm_Wastes.Find(strGuid);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            Qcwd001.Text = current.Qcwd001;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            Qcwd002.Text = current.Qcwd002;
            Qcwd003.Text = current.Qcwd003.ToString();
            Qcwd004.Text = current.Qcwd004.ToString();
            Qcwd005.Text = current.Qcwd005.ToString();
            Qcwd006.Text = current.Qcwd006.ToString();
            Qcwd007.Text = current.Qcwd007.ToString();
            Qcwd008.Text = current.Qcwd008.ToString();
            Qcwd009.Text = current.Qcwd009.ToString();
            Qcwd010.Text = current.Qcwd010.ToString();
            Qcwd011.Text = current.Qcwd011.ToString();
            Qcwd012.Text = current.Qcwd012.ToString();
            Qcwd013.Text = current.Qcwd013.ToString();
            Qcwd014.Text = current.Qcwd014.ToString();
            Qcwd015.Text = current.Qcwd015.ToString();

            Qcwdrec.Text = current.Qcwdrec.ToString();

            // 添加所有用户

            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);
            //修改前日志
            string BeforeModi = current.Qcwd001 + "," + current.Qcwd002 + "," + current.Qcwd003 + "," + current.Qcwd004 + "," + current.Qcwd005 + "," + current.Qcwd006 + "," + Qcwdrec.Text;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量成本", "废弃事故修改", OperateNotes);
        }

        #endregion BindDdlData

        #endregion Page_Load

        #region Events

        //判断修改内容||判断重复
        private void CheckData()
        {
            Qm_Waste current = DB.Qm_Wastes.Find(strGuid);
            string modi001 = current.Qcwd001;
            string modi002 = current.Qcwd002;
            string modi003 = current.Qcwd004;
            string modi004 = current.Qcwd004;
            string modi005 = current.Qcwd004;

            if (this.Qcwd001.Text == modi001)
            {
                if (this.Qcwd002.Text == modi002)
                {
                    if (this.Qcwd004.Text == modi003)
                    {
                        if (this.Qcwd004.Text == modi004)
                        {
                            if (this.Qcwd004.Text == modi005)
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
            //string InputData = Qcrd001.SelectedDate.Value.ToString("yyyyMMdd") + Qcrd002.Text.Trim() + Qcrd003.Text.Trim();

            //sys_Button_New_Qm_Reworkdata Redata = DB.Qm_Reworks.Where(u => u.Qcrd001 + u.Qcrd002 + u.Qcrd003 == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("改修对应数据,相同LOT< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
            //工单数量
            //string MA002str;

            //MA002str = "SELECT MC005 FROM [dbo].[proOrders]  WHERE MC006='" + this.LA003.SelectedItem.Text + "'";
            //SqlDataAdapter MA002DA = new SqlDataAdapter(MA002str, AppConn);
            //DataSet MA002DS = new DataSet();
            //MA002DA.Fill(MA002DS);
            //mcQTY = decimal.Parse(MA002DS.Tables[0].Rows[0][0].ToString());

            ////QA检验入库数量
            //string MA003str;
            //MA003str = "SELECT SUM(LA015) AS qaQTY FROM [dbo].[proCheckdatas]  WHERE LA002='" + this.LA003.SelectedItem.Text + "'";
            //SqlDataAdapter MA003DA = new SqlDataAdapter(MA003str, AppConn);
            //DataSet MA003DS = new DataSet();
            //MA003DA.Fill(MA003DS);

            //if (MA003DS.Tables[0].Rows[0][0].ToString().Length != 0)
            //{
            //    qaQTY = decimal.Parse(MA003DS.Tables[0].Rows[0][0].ToString());
            //}
            //else
            //{
            //    qaQTY = 0;
            //}

            //if (qaQTY > mcQTY)
            //{
            //    //入库超出日志
            //    string Newtext = this.LA003.SelectedItem.Text + "," + qaQTY + "," + mcQTY;
            //    string OperateType = this.Qacheckguid.Text;
            //    string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品验收超出", OperateNotes);

            //    Alert.ShowInTop("工单 " + this.LA002.SelectedItem.Text + " 已经超检验入库！");
            //    return;
            //}
        }

        //字段赋值，保存
        private void SaveItem()//新增质量控制数据
        {
            Qm_Waste item = DB.Qm_Wastes

                .Where(u => u.GUID == strGuid).FirstOrDefault();
            item.Qcwd001 = Qcwd001.Text;
            item.Qcwd002 = Qcwd002.Text;
            item.Qcwd003 = decimal.Parse(Qcwd003.Text);
            item.Qcwd004 = Qcwd004.Text;
            item.Qcwd005 = Qcwd005.Text;
            item.Qcwd006 = Qcwd006.Text;
            item.Qcwd007 = decimal.Parse(Qcwd007.Text);
            item.Qcwd008 = decimal.Parse(Qcwd008.Text);
            item.Qcwd009 = decimal.Parse(Qcwd009.Text);
            item.Qcwd010 = decimal.Parse(Qcwd010.Text);
            item.Qcwd011 = decimal.Parse(Qcwd011.Text);
            item.Qcwd012 = decimal.Parse(Qcwd012.Text);
            item.Qcwd013 = int.Parse(Qcwd013.Text);
            item.Qcwd014 = decimal.Parse(Qcwd014.Text);
            item.Qcwd015 = decimal.Parse(Qcwd015.Text);

            item.Qcwdrec = Qcwdrec.Text;

            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = Qcwd001.Text + "," + Qcwd002.Text + "," + Qcwd003.Text + "," + Qcwd004.Text + "," + Qcwd005.Text + "," + Qcwd006.Text + "," + Qcwdrec.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量成本", "废弃事故修改", OperateNotes);
        }

        public void dQcwd007()
        {
            if (Qcwd008.Text != "" && Qcwd009.Text != "")
            {
                Qcwd007.Text = (decimal.Parse(Qcwd008.Text) * decimal.Parse(Qcwd009.Text) + decimal.Parse(Qcwd010.Text) + decimal.Parse(Qcwd011.Text)).ToString();
            }
        }

        protected void Qcwd008_TextChanged(object sender, EventArgs e)
        {
            dQcwd007();
        }

        protected void Qcwd010_TextChanged(object sender, EventArgs e)
        {
            dQcwd007();
        }

        protected void Qcwd011_TextChanged(object sender, EventArgs e)
        {
            dQcwd007();
        }

        public void dQcwd012()
        {
            if (Qcwd003.Text != "" && Qcwd013.Text != "")
            {
                Qcwd012.Text = (decimal.Parse(Qcwd003.Text) * decimal.Parse(Qcwd013.Text) + decimal.Parse(Qcwd014.Text) + decimal.Parse(Qcwd015.Text)).ToString();
            }
        }

        protected void Qcwd013_TextChanged(object sender, EventArgs e)
        {
            dQcwd012();
        }

        protected void Qcwd014_TextChanged(object sender, EventArgs e)
        {
            dQcwd012();
        }

        protected void Qcwd015_TextChanged(object sender, EventArgs e)
        {
            dQcwd012();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckData();
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}