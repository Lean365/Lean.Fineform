using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;
using System.Data;
using System.Linq;

namespace LeanFine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_action_edit : PageBase
    {
        //日志配置文件调用
        //private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcActionEdit";
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
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            BindData();
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Improvement current = DB.Qm_Improvements.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            qmIssueno.Text = current.qmIssueno;
            qmInspector.Text = current.qmInspector;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            qmLine.Text = current.qmLine;
            qmOrder.Text = current.qmOrder;
            qmModels.Text = current.qmModels;
            qmMaterial.Text = current.qmMaterial;
            qmRegion.Text = current.qmRegion;
            qmCheckdate.Text = current.qmCheckdate;
            qmProlot.Text = current.qmProlot;
            qmLotserial.Text = current.qmLotserial;
            qmRejectqty.Text = current.qmRejectqty.ToString();
            qmJudgmentlevel.Text = current.qmJudgmentlevel;
            qmCheckNotes.Text = current.qmCheckNotes.ToString();
            if (current.qmPersonnel.ToString() == "Wait for Reply")
            {
                qmPersonnel.Text = "";
                qmDate.SelectedDate = DateTime.ParseExact(current.qmDate.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                qmPersonnel.Text = current.qmPersonnel.ToString();
                qmDate.Text = DateTime.Now.ToString("yyyyMMdd"); ;
            }
            if (current.qmDirectreason.ToString() == "Wait for Reply")
            {
                qmDirectreason.Text = "";
            }
            else
            {
                qmDirectreason.Text = current.qmDirectreason.ToString();
            }

            if (current.qmIndirectreason.ToString() == "Wait for Reply")
            {
                qmIndirectreason.Text = "";
            }
            else
            {
                qmIndirectreason.Text = current.qmIndirectreason.ToString();
            }

            if (current.qmDisposal.ToString() == "Wait for Reply")
            {
                qmDisposal.Text = "";
            }
            else
            {
                qmDisposal.Text = current.qmDisposal.ToString();
            }

            if (current.qmDirectsolutions.ToString() == "Wait for Reply")

            {
                qmDirectsolutions.Text = "";
            }
            else
            {
                qmDirectsolutions.Text = current.qmDirectsolutions.ToString();
            }

            if (current.qmDirectreason.ToString() == "Wait for Reply")
            {
                qmIndirectsolutions.Text = "";
            }
            else
            {
                qmIndirectsolutions.Text = current.qmIndirectsolutions.ToString();
            }

            if (current.qmVerify.ToString() == "Wait for Reply")
            {
                qmVerify.Text = "";
                qmCarryoutdate.SelectedDate = DateTime.ParseExact(current.qmCarryoutdate.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                qmVerify.Text = current.qmPersonnel.ToString();
                qmCarryoutdate.Text = DateTime.Now.ToString("yyyyMMdd"); ;
            }

            if (current.qmCarryoutverify == true)
            {
                qmCarryoutverify.Checked = true;
            }
            else
            {
                qmCarryoutverify.Checked = false;
            }
            if (current.qmDirectreason.ToString() == "Wait for Reply")
            {
                qmIndirectsolutions.Text = "";
            }
            else
            {
                qmCarryoutverify.Text = current.qmCarryoutverify.ToString();
            }

            if (current.qmDirectreason.ToString() == "Wait for Reply")
            {
                qmIndirectsolutions.Text = "";
            }
            else
            {
                qmCarryoutverify.Text = current.qmCarryoutverify.ToString();
            }

            if (current.qmDirectreason.ToString() == "Wait for Reply")
            {
                qmIndirectsolutions.Text = "";
            }
            else
            {
                qmIndirectsolutions.Text = current.qmIndirectsolutions.ToString();
            }
            qmCarryoutverify.Text = current.qmCarryoutverify.ToString();
            Remark.Text = current.Remark;

            // 添加所有用户

            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);
            //修改前日志
            string BeforeModi = current.qmIssueno + "," + current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmRegion + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmRejectqty + "," + current.qmJudgmentlevel + "," + current.qmCheckNotes + "," + current.qmPersonnel + "," + current.qmDate;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良对策修改", OperateNotes);
        }

        #endregion Page_Load

        //判断修改内容||判断重复
        private void CheckData()
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Improvement current = DB.Qm_Improvements.Find(id);
            string modi001 = current.qmPersonnel;
            string modi002 = current.qmDate;
            string modi003 = current.qmDirectreason;
            string modi004 = current.qmDisposal;
            string modi005 = current.qmCarryoutdate;

            if (this.qmPersonnel.Text == modi001)
            {
                if (this.qmDate.Text == modi002)
                {
                    if (this.qmDirectreason.Text == modi003)
                    {
                        if (this.qmDisposal.Text == modi004)
                        {
                            if (this.qmCarryoutdate.Text == modi005)
                            {
                                Alert.ShowInTop("global::Resources.GlobalResource.sys_Msg_Noedit！", "修改提示", MessageBoxIcon.Warning);
                                //Alert alert = new Alert();
                                //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
                                //alert.IconUrl = "~/Lf_Resources/images/success.png";
                                //alert.Target = Target.Top;
                                //Alert.ShowInTop();
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
            //string InputData = qmCheckNotes.Text.Trim();

            //Qm_Improvement Redata = DB.Qm_Improvements.Where(u => u.qmCheckNotes == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("分析对策数据,判定说明说明方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
        }

        #region Events

        private void SaveItem()//新增质量控制数据
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Improvement item = DB.Qm_Improvements

                .Where(u => u.GUID == id).FirstOrDefault();

            item.qmInspector = qmInspector.Text;
            item.qmLine = qmLine.Text;
            item.qmOrder = qmOrder.Text;
            item.qmModels = qmModels.Text;
            item.qmMaterial = qmMaterial.Text;
            item.qmRegion = qmRegion.Text;
            item.qmCheckdate = qmCheckdate.Text;
            item.qmProlot = qmProlot.Text;
            item.qmLotserial = qmLotserial.Text;
            item.qmRejectqty = decimal.Parse(qmRejectqty.Text);
            item.qmJudgmentlevel = qmJudgmentlevel.Text;
            item.qmCheckNotes = qmCheckNotes.Text;
            item.qmPersonnel = qmPersonnel.Text;
            item.qmDate = qmDate.SelectedDate.Value.ToString("yyyyMMdd");
            item.qmDirectreason = qmDirectreason.Text;
            item.qmIndirectreason = qmIndirectreason.Text;
            item.qmDisposal = qmDisposal.Text;
            item.qmDirectsolutions = qmDirectsolutions.Text;
            item.qmIndirectsolutions = qmIndirectsolutions.Text;
            item.qmVerify = qmVerify.Text;

            item.qmCarryoutdate = qmCarryoutdate.SelectedDate.Value.ToString("yyyyMMdd");

            if (this.qmCarryoutverify.Checked == true)
            {
                item.qmCarryoutverify = true;
            }
            else
            {
                item.qmCarryoutverify = false;
            }
            item.qmSolutionsverify = qmSolutionsverify.Text;
            item.qmNotes = qmNotes.Text;

            item.Remark = Remark.Text;
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = qmIssueno.Text + "," + qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmRegion.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + qmRejectqty.Text + "," + qmJudgmentlevel.Text + "," + qmCheckNotes.Text + "," + qmPersonnel.Text + "," + qmDate.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良对策修改", OperateNotes);
        }

        public decimal qaQTY, mcQTY;

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            ////工单未入库数量
            //string MA002str;

            //MA002str = "SELECT MC009 FROM [dbo].[proOrders]  WHERE MC003='" + this.qmLine.Text + "'";
            //SqlDataAdapter MA002DA = new SqlDataAdapter(MA002str, AppConn);
            //DataSet MA002DS = new DataSet();
            //MA002DA.Fill(MA002DS);

            //mcQTY = decimal.Parse(MA002DS.Tables[0].Rows[0][0].ToString());
            ////QA检验入库数量
            //string MA003str;
            //MA003str = "SELECT SUM(qmDate) AS qaQTY FROM PlutoOA.dbo.Invlas  WHERE qmLine='" + this.qmLine.Text + "'";
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
            //    //登录日志写入
            //    logger.Error(String.Format("用户 - {0} - 验收数量大于工单数量", GetIdentityName() + this.qmLine.Text + qaQTY + qmDirectreason.Text));
            //    Alert.ShowInTop("工单 " + this.qmLine.Text + " 已经超检验入库！");
            //    return;
            //}
            CheckData();
            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events

        #region DDLBindData

        protected void qmDirectreason_TextChanged(object sender, EventArgs e)
        {
            qmDirectreason.Text = StringHelper.FilterSpecial(qmDirectreason.Text.ToString().Trim());//文本框输入的值

            qmDirectreason.Text = StringHelper.ToDBC(qmDirectreason.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDirectreason.Text.ToString().Trim());//大小写转换
            //qmIndirectreason.Text = StringHelper.CNYcurD(qmDirectreason.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmIndirectreason_TextChanged(object sender, EventArgs e)
        {
            qmIndirectreason.Text = StringHelper.FilterSpecial(qmIndirectreason.Text.ToString().Trim());//文本框输入的值
            qmIndirectreason.Text = StringHelper.ToDBC(qmIndirectreason.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmIndirectreason.Text.ToString().Trim());//大小写转换
            //qmIndirectreason.Text = StringHelper.CNYcurD(qmIndirectreason.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmDisposal_TextChanged(object sender, EventArgs e)
        {
            qmDisposal.Text = StringHelper.FilterSpecial(qmDisposal.Text.ToString().Trim());//文本框输入的值
            qmDisposal.Text = StringHelper.ToDBC(qmDisposal.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDisposal.Text.ToString().Trim());//大小写转换
            //qmDisposal.Text = StringHelper.CNYcurD(qmDisposal.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmDirectsolutions_TextChanged(object sender, EventArgs e)
        {
            qmDirectsolutions.Text = StringHelper.FilterSpecial(qmDirectsolutions.Text.ToString().Trim());//文本框输入的值
            qmDirectsolutions.Text = StringHelper.ToDBC(qmDirectsolutions.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDirectsolutions.Text.ToString().Trim());//大小写转换
            //qmDirectsolutions.Text = StringHelper.CNYcurD(qmDirectsolutions.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmIndirectsolutions_TextChanged(object sender, EventArgs e)
        {
            qmIndirectsolutions.Text = StringHelper.FilterSpecial(qmIndirectsolutions.Text.ToString().Trim());//文本框输入的值
            qmIndirectsolutions.Text = StringHelper.ToDBC(qmIndirectsolutions.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmIndirectsolutions.Text.ToString().Trim());//大小写转换
            //qmIndirectsolutions.Text = StringHelper.CNYcurD(qmIndirectsolutions.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmSolutionsverify_TextChanged(object sender, EventArgs e)
        {
            qmSolutionsverify.Text = StringHelper.FilterSpecial(qmSolutionsverify.Text.ToString().Trim());//文本框输入的值
            qmSolutionsverify.Text = StringHelper.ToDBC(qmSolutionsverify.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmSolutionsverify.Text.ToString().Trim());//大小写转换
            //qmSolutionsverify.Text = StringHelper.CNYcurD(qmSolutionsverify.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmNotes_TextChanged(object sender, EventArgs e)
        {
            qmNotes.Text = StringHelper.FilterSpecial(qmNotes.Text.ToString().Trim());//文本框输入的值
            qmNotes.Text = StringHelper.ToDBC(qmNotes.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmNotes.Text.ToString().Trim());//大小写转换
            //qmNotes.Text = StringHelper.CNYcurD(qmNotes.Text.ToString().Trim());//会计金额大写转换
        }

        #endregion DDLBindData
    }
}