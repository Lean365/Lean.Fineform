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


namespace Lean.Fineform.Lf_Manufacturing.QM.cost
{

    public partial class operation_cost_new : PageBase
    {
        //日志配置文件调用

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreOperationCostNew";
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

            this.Qcod001.SelectedDate = DateTime.Now;
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            Qcodqar.Text = GetIdentityName();
            Qcodqcr.Text= GetIdentityName();
            
        }

        #region BindingData


        private void CheckIFRS()
        {
            string sdate = this.Qcod001.SelectedDate.Value.ToString("yyyyMM");
            var q = from a in DB.Qm_Wagerates
                    where a.Qcsd001.Contains(sdate)
                    select a;

            var qs = q.Distinct().ToList();
            if(qs.Any())
            {
                Qcod002.Text = qs[0].Qcsd010.ToString();
            }
            else

            {
                Alert.ShowInTop("请先录入财务数据！", "警告", MessageBoxIcon.Error);

                //return;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }


        }

        #endregion
        #endregion
        #region Events
        //判断修改内容||判断重复
        private void CheckData()
        {
            CheckIFRS();
            //int id = GetQueryIntValue("id");
            //Qm_Unqualified current = DB.Qm_Unqualifieds.Find(id);
            //string modi001 = current.qmInspector;
            //string modi002 = current.qmLine;
            //string modi003 = current.qmOrder;
            //string modi004 = current.qmModels;
            //string modi005 = current.qmMaterial;

            //if (this.qmInspector.Text == modi001)
            //{
            //    if (this.qmLine.Text == modi002)
            //    {
            //        if (this.qmOrder.Text == modi003)
            //        {
            //            if (this.qmModels.Text == modi004)
            //            {
            //                if (this.qmMaterial.Text == modi005)
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
            string InputData = Qcod001.SelectedDate.Value.ToString("yyyyMMdd");


            Qm_Operationdata Redata = DB.Qm_Operationdatas.Where(u => u.Qcod001 == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("品质对应数据,相同日期< " + InputData + ">已经存在！修改即可");
                return;
            }
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


            Qm_Operationdata item = new Qm_Operationdata();
            item.Qcod001 = Qcod001.SelectedDate.Value.ToString("yyyyMM");
            item.Qcod002 = decimal.Parse(Qcod002.Text);
            item.Qcod003 = decimal.Parse(Qcod003.Text);
            item.Qcod004 = int.Parse(Qcod004.Text);
            item.Qcod005 = decimal.Parse(Qcod005.Text);
            item.Qcod006 = decimal.Parse(Qcod006.Text);
            item.Qcod007 = Qcod007.Text;
            item.Qcod008 = decimal.Parse(Qcod008.Text);
            item.Qcod009 = int.Parse(Qcod009.Text);
            item.Qcod010 = decimal.Parse(Qcod010.Text);
            item.Qcod011 = Qcod011.Text;
            item.Qcod012 = decimal.Parse(Qcod012.Text);
            item.Qcod013 = int.Parse(Qcod013.Text);
            item.Qcod014 = decimal.Parse(Qcod014.Text);
            item.Qcod015 = decimal.Parse(Qcod015.Text);
            item.Qcod016 = Qcod016.Text;
            item.Qcod017 = decimal.Parse(Qcod017.Text);
            item.Qcod018 = int.Parse(Qcod018.Text);
            item.Qcod019 = decimal.Parse(Qcod019.Text);
            item.Qcod020 = Qcod020.Text;
            item.Qcodqcr = Qcodqcr.Text;
            item.Qcod021 = decimal.Parse(Qcod021.Text);
            item.Qcod022 = int.Parse(Qcod022.Text);
            item.Qcod023 = decimal.Parse(Qcod023.Text);
            item.Qcod024 = Qcod024.Text;
            item.Qcod025 = decimal.Parse(Qcod025.Text);
            item.Qcod026 = int.Parse(Qcod026.Text);
            item.Qcod027 = decimal.Parse(Qcod027.Text);
            item.Qcod028 = Qcod028.Text;
            item.Qcod029 = decimal.Parse(Qcod029.Text);
            item.Qcod030 = int.Parse(Qcod030.Text);
            item.Qcod031 = decimal.Parse(Qcod031.Text);
            item.Qcod032 = Qcod032.Text;
            item.Qcod033 = decimal.Parse(Qcod033.Text);
            item.Qcod034 = int.Parse(Qcod034.Text);
            item.Qcod035 = decimal.Parse(Qcod035.Text);
            item.Qcod036 = Qcod036.Text;
            item.Qcodqar = Qcodqar.Text;
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateTime = DateTime.Now;
            DB.Qm_Operationdatas.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Qcod001.Text + "," + Qcod002.Text + "," + Qcod003.Text + "," + Qcod004.Text + "," + Qcod005.Text + "," + Qcod006.Text + "," + Qcod007.Text + "," + Qcod008.Text + "," + Qcod009.Text + "," + Qcod010.Text + "," + Qcod011.Text + "," + Qcod012.Text + "," + Qcod013.Text + "," + Qcod014.Text + "," + Qcod015.Text + "," + Qcod016.Text + "," + Qcod017.Text + "," + Qcod018.Text + "," + Qcod019.Text + "," + Qcod020.Text + "," + Qcodqcr.Text + "," + Qcod021.Text + "," + Qcod022.Text + "," + Qcod023.Text + "," + Qcod024.Text + "," + Qcod025.Text + "," + Qcod026.Text + "," + Qcod027.Text + "," + Qcod028.Text + "," + Qcod029.Text + "," + Qcod030.Text + "," + Qcod031.Text + "," + Qcod032.Text + "," + Qcod033.Text + "," + Qcod034.Text + "," + Qcod035.Text + "," + Qcod036.Text + "," + Qcodqar.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量成本", "月品质数据新增", OperateNotes);

        }


        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            ////判断重复
            //string InputDate = Qcod001.SelectedDate.Value.ToString("yyyyMM").Trim();


            //sys_Button_New_Qm_Operationdata operationdata = DB.Qm_Operationdatas.Where(u => u.Qcod001 == InputDate).FirstOrDefault();

            //if (operationdata != null)
            //{
            //    Alert.ShowInTop("质量管理资料,月< " + InputDate + ">已经存在！修改即可");
            //    return;
            //}
            CheckData();
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void Qcod004_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void Qcod002_SelectedIndexChanged(object sender, EventArgs e)
        {



        }
        protected void Qcod001_TextChanged(object sender, EventArgs e)
        {
            CheckIFRS();
        }

        //计算受入检查业务费用
        public void qcod004()
        {
            if (Qcod004.Text != "" && Qcod005.Text != "" && Qcod006.Text != "" && Qcod002.Text != "")
            {
                Qcod003.Text = (decimal.Parse(Qcod004.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod005.Text) + decimal.Parse(Qcod006.Text)).ToString();
            }
        }


        protected void Qcod004_TextChanged(object sender, EventArgs e)
        {
            qcod004();
        }

        protected void Qcod005_TextChanged(object sender, EventArgs e)
        {
            qcod004();
        }

        protected void Qcod006_TextChanged(object sender, EventArgs e)
        {
            qcod004();
        }




        //初期检定.定期检定业务费用
        public void qcod009()
        {
            if (Qcod009.Text != "" && Qcod010.Text != "" && Qcod002.Text != "")
            {
                Qcod008.Text = (decimal.Parse(Qcod009.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod010.Text)).ToString();// + decimal.Parse(Qcod006.Text)).ToString();
            }
        }

        protected void Qcod009_TextChanged(object sender, EventArgs e)
        {
            qcod009();
        }


        protected void Qcod010_TextChanged(object sender, EventArgs e)
        {
            qcod009();
        }
        //测定器校正业务费用
        public void qcod013()
        {
            if (Qcod013.Text != "" && Qcod014.Text != "" && Qcod015.Text != "" && Qcod002.Text != "")
            {
                Qcod012.Text = (decimal.Parse(Qcod013.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod014.Text) + decimal.Parse(Qcod015.Text)).ToString();
            }
        }

        protected void Qcod013_TextChanged(object sender, EventArgs e)
        {
            qcod013();
        }

        protected void Qcod014_TextChanged(object sender, EventArgs e)
        {
            qcod013();
        }

        protected void Qcod015_TextChanged(object sender, EventArgs e)
        {
            qcod013();
        }
        //其他通常业务费用
        public void qcod017()
        {
            if (Qcod013.Text != "" && Qcod019.Text != "" && Qcod002.Text != "")
            {
                Qcod017.Text = (decimal.Parse(Qcod018.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod019.Text)).ToString();
            }
        }

        protected void Qcod018_TextChanged(object sender, EventArgs e)
        {
            qcod017();
        }

        protected void Qcod019_TextChanged(object sender, EventArgs e)
        {
            qcod017();
        }
        //出荷检查业务费用
        public void qcod021()
        {
            if (Qcod022.Text != "" && Qcod023.Text != "" && Qcod002.Text != "")
            {
                Qcod021.Text = (decimal.Parse(Qcod022.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod023.Text)).ToString();
            }
        }

        protected void Qcod022_TextChanged(object sender, EventArgs e)
        {
            qcod021();
        }

        protected void Qcod023_TextChanged(object sender, EventArgs e)
        {
            qcod021();
        }
        //信赖性评价・ORT业务费用
        public void qcod025()
        {
            if (Qcod026.Text != "" && Qcod027.Text != "" && Qcod002.Text != "")
            {
                Qcod025.Text = (decimal.Parse(Qcod026.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod027.Text)).ToString();
            }
        }
        protected void Qcod026_TextChanged(object sender, EventArgs e)
        {
            qcod025();
        }

        protected void Qcod027_TextChanged(object sender, EventArgs e)
        {
            qcod025();
        }
        //顾客品质要求对应业务费用
        public void qcod029()
        {
            if (Qcod030.Text != "" && Qcod031.Text != "" && Qcod002.Text != "")
            {
                Qcod029.Text = (decimal.Parse(Qcod030.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod031.Text)).ToString();
            }
        }
        protected void Qcod030_TextChanged(object sender, EventArgs e)
        {
            qcod029();
        }

        protected void Qcod031_TextChanged(object sender, EventArgs e)
        {
            qcod029();
        }


        //其他通常业务费用
        public void qcod033()
        {
            if (Qcod034.Text != "" && Qcod035.Text != "" && Qcod002.Text != "")
            {
                Qcod033.Text = (decimal.Parse(Qcod034.Text) * decimal.Parse(Qcod002.Text) + decimal.Parse(Qcod035.Text)).ToString();
            }
        }
        protected void Qcod034_TextChanged(object sender, EventArgs e)
        {
            qcod033();
        }
        protected void Qcod035_TextChanged(object sender, EventArgs e)
        {
            qcod033();
        }


        #endregion


    }
}