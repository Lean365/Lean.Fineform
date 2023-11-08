using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using FineUIPro.Design;
using System.Linq;
using System.Data.Entity;

using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace Lean.Fineform.Lf_Manufacturing.QM.fqc
{

    public partial class fqc_notice_edit : PageBase
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
                return "CoreFqcNoticeEdit";
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

            BindData();

        }


        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Guid id =Guid.Parse( GetQueryValue("GUID"));
            Qm_Unqualified current = DB.Qm_Unqualifieds.Find(id);

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

            qmCheckNotes.Text= current.qmCheckNotes;


            qmLotserial.Text = current.qmLotserial;
            qmRejectqty.Text = current.qmRejectqty.ToString();
            qmJudgmentlevel.Text = current.qmJudgmentlevel;


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
            string BeforeModi = current.qmIssueno+","+ current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmRegion + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmRejectqty + "," + current.qmJudgmentlevel + "," + current.qmCheckNotes;
            string OperateType ="修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品检验修改", OperateNotes);
        }




        #endregion

        #region Events
        //判断修改内容||判断重复
        private void CheckData()
        {

            Guid id =Guid.Parse( GetQueryValue("GUID"));
            Qm_Unqualified current = DB.Qm_Unqualifieds.Find(id);
            string modi001 = current.qmInspector;
            string modi002 = current.qmLine;
            string modi003 = current.qmOrder;
            string modi004 = current.qmModels;
            string modi005 = current.qmMaterial;

            if (this.qmInspector.Text == modi001)
            {
                if (this.qmLine.Text == modi002)
                {
                    if (this.qmOrder.Text == modi003)
                    {
                        if (this.qmModels.Text == modi004)
                        {
                            if (this.qmMaterial.Text == modi005)
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
            //string InputData = qmCheckNotes.Text.Trim();


            //Qm_Improvement Redata = DB.Qm_Improvements.Where(u => u.qmCheckNotes == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("分析对策数据,判定说明说明方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}

        }
        //字段赋值，保存
        private void SaveItem()//新增质量控制数据
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Unqualified item = DB.Qm_Unqualifieds

                .Where(u => u.GUID == id).FirstOrDefault();
            item.qmProlot = qmProlot.Text;
            item.qmLotserial = qmLotserial.Text;
            item.qmRejectqty = decimal.Parse(qmRejectqty.Text);
            item.qmJudgmentlevel = qmJudgmentlevel.Text;

            item.Remark = Remark.Text;

            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = qmIssueno.Text + "," + qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmRegion.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + qmRejectqty.Text + "," + qmJudgmentlevel.Text + "," + qmCheckNotes.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不合格通知书修改", OperateNotes);


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
            //MA003str = "SELECT SUM(Qano014) AS qaQTY FROM PlutoOA.dbo.Invlas  WHERE qmLine='" + this.qmLine.Text + "'";
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
            //    logger.Error(String.Format("用户 - {0} - 验收数量大于工单数量", GetIdentityName() + this.qmLine.Text + qaQTY + Qano015.Text));
            //    Alert.ShowInTop("工单 " + this.qmLine.Text + " 已经超检验入库！");
            //    return;
            //}

            SaveItem();



            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion
        #region BindData




        #endregion



    }
}
