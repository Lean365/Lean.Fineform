using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class rework_cost_new : PageBase
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
                return "CoreReworkCostNew";
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

            Qcrd001.SelectedDate = DateTime.Now;
            Qcrd018.SelectedDate = DateTime.Now;
            Qcrd033.SelectedDate = DateTime.Now;
            Qcrd048.SelectedDate = DateTime.Now;

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

            BindDDLModel();
            this.Tab2.Enabled = false;
            this.Tab3.Enabled = false;
            this.Tab4.Enabled = false;
            Qcrdpcbrec.Text = GetIdentityName();
            Qcrdassrec.Text = GetIdentityName();
            Qcrdmcrec.Text = GetIdentityName();
            Qcrdqarec.Text = GetIdentityName();
        }

        #region BindingData

        private void BindDDLModel()
        {
            //查询LINQ去重复
            var q = from a in DB.Pp_Manhours
                    where (from b in DB.Pp_Orders
                           select b.Porderhbn)
                            .Contains(a.Proitem)
                    //join b in DB.Pp_Orders on a.Proitem equals b.Porderhbn
                    //where b.Ec_no == strecn
                    //where a.lineclass == "M"
                    select new
                    {
                        a.Promodel
                    };

            var qs = q.Select(E => new { E.Promodel }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            Qcrd002.DataSource = qs;
            Qcrd002.DataTextField = "Promodel";
            Qcrd002.DataValueField = "Promodel";
            Qcrd002.DataBind();
            this.Qcrd002.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindDDLLot()
        {
            //查询LINQ去重复
            var q = from a in DB.Pp_Manhours
                    join b in DB.Pp_Orders on a.Proitem equals b.Porderhbn
                    where a.Promodel == this.Qcrd002.SelectedItem.Text
                    //where a.lineclass == "M"
                    select new
                    {
                        b.Porderlot
                    };

            var qs = q.Select(E => new { E.Porderlot }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            Qcrd003.DataSource = qs;
            Qcrd003.DataTextField = "Porderlot";
            Qcrd003.DataValueField = "Porderlot";
            Qcrd003.DataBind();
            this.Qcrd003.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void CheckIFRS()
        {
            string sdate = this.Qcrd001.SelectedDate.Value.ToString("yyyyMM");
            var q = from a in DB.Qm_Wagerates
                    where a.Qcsd001.Contains(sdate)
                    select a;

            var qs = q.Distinct().ToList();
            if (qs.Any())
            {
                Qcrd004.Text = qs[0].Qcsd006.ToString();//直接
                Qcrd005.Text = qs[0].Qcsd010.ToString();//间接
            }
            else

            {
                Alert.ShowInTop("请先录入财务数据！", "警告", MessageBoxIcon.Error);

                //return;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion BindingData

        #endregion Page_Load

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
            string InputData = Qcrd001.SelectedDate.Value.ToString("yyyyMMdd") + Qcrd002.Text.Trim() + Qcrd003.Text.Trim();

            Qm_Rework Redata = DB.Qm_Reworks.Where(u => u.Qcrd001 + u.Qcrd002 + u.Qcrd003 == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("改修对应数据,相同LOT< " + InputData + ">已经存在！修改即可", "错误提示", MessageBoxIcon.Error);
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
            CheckDDLData();
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void CheckDDLData()
        {
            if (Qcrd002.SelectedItem.Text == "请选择")// && LA002.SelectedIndex> 1 && LA003.SelectedIndex > 1 && LA009.SelectedIndex > 1 && LA011.SelectedIndex > 1 && LA012.SelectedIndex > 1)
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }
            if (Qcrd003.SelectedItem.Text == "请选择")// && LA002.SelectedIndex> 1 && LA003.SelectedIndex > 1 && LA009.SelectedIndex > 1 && LA011.SelectedIndex > 1 && LA012.SelectedIndex > 1)
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }
        }

        //字段赋值，保存
        private void SaveItem()//新增质量控制数据
        {
            qQcrd007();
            qQcrd013();
            Qm_Rework item = new Qm_Rework();
            item.Qcrd001 = Qcrd001.SelectedDate.Value.ToString("yyyyMMdd");
            item.Qcrd002 = Qcrd002.SelectedItem.Text;
            item.Qcrd003 = Qcrd003.SelectedItem.Text;
            item.Qcrd004 = decimal.Parse(Qcrd004.Text);
            item.Qcrd005 = decimal.Parse(Qcrd005.Text);
            item.Qcrd006 = Qcrd006.Text;
            item.Qcrd007 = decimal.Parse(Qcrd007.Text);
            item.Qcrd008 = int.Parse(Qcrd008.Text);
            item.Qcrd009 = int.Parse(Qcrd009.Text);
            item.Qcrd010 = int.Parse(Qcrd010.Text);
            item.Qcrd011 = int.Parse(Qcrd011.Text);
            item.Qcrd012 = decimal.Parse(Qcrd012.Text);
            item.Qcrd013 = decimal.Parse(Qcrd013.Text);
            item.Qcrd014 = int.Parse(Qcrd014.Text);
            item.Qcrd015 = decimal.Parse(Qcrd015.Text);
            if (this.Qcrd016.Checked == true)
            {
                item.Qcrd016 = true;
            }
            else
            {
                item.Qcrd016 = false;
            }
            item.Qcrd017 = Qcrd017.Text;
            item.Qcrdqarec = Qcrdqarec.Text;
            item.GUID = Guid.NewGuid();

            item.Qcrd018 = Qcrd018.SelectedDate.Value.ToString("yyyyMMdd");
            item.Qcrd019 = Qcrd019.Text;
            item.Qcrd020 = decimal.Parse(Qcrd020.Text);
            item.Qcrd021 = int.Parse(Qcrd021.Text);
            item.Qcrd022 = int.Parse(Qcrd022.Text);
            item.Qcrd023 = decimal.Parse(Qcrd023.Text);
            item.Qcrd024 = decimal.Parse(Qcrd024.Text);
            item.Qcrd025 = decimal.Parse(Qcrd025.Text);
            item.Qcrd026 = Qcrd026.Text;
            item.Qcrd027 = decimal.Parse(Qcrd027.Text);
            item.Qcrd028 = Qcrd028.Text;
            item.Qcrd029 = Qcrd029.Text;

            item.Qcrd030 = decimal.Parse(Qcrd030.Text);
            item.Qcrd031 = decimal.Parse(Qcrd031.Text);
            item.Qcrdmcrec = Qcrdmcrec.Text;
            item.Qcrd032 = Qcrd032.Text;
            item.Qcrd033 = Qcrd033.SelectedDate.Value.ToString("yyyyMMdd");
            item.Qcrd034 = (Qcrd034.Text);
            item.Qcrd035 = decimal.Parse(Qcrd035.Text);
            item.Qcrd036 = Int64.Parse(Qcrd036.Text);
            item.Qcrd037 = Int64.Parse(Qcrd037.Text);
            item.Qcrd038 = decimal.Parse(Qcrd038.Text);
            item.Qcrd039 = decimal.Parse(Qcrd039.Text);
            item.Qcrd040 = decimal.Parse(Qcrd040.Text);
            item.Qcrd041 = (Qcrd041.Text);
            item.Qcrd042 = decimal.Parse(Qcrd042.Text);
            item.Qcrd043 = Qcrd043.Text;

            item.Qcrd044 = (Qcrd044.Text);
            item.Qcrd045 = decimal.Parse(Qcrd045.Text);
            item.Qcrdassrec = Qcrdassrec.Text;

            item.Qcrd046 = decimal.Parse(Qcrd046.Text);

            item.Qcrd047 = Qcrd047.Text;
            item.Qcrd048 = Qcrd048.SelectedDate.Value.ToString("yyyyMMdd");
            item.Qcrd049 = (Qcrd049.Text);
            item.Qcrd050 = decimal.Parse(Qcrd050.Text);
            item.Qcrd051 = Int64.Parse(Qcrd051.Text);
            item.Qcrd052 = Int64.Parse(Qcrd052.Text);
            item.Qcrd053 = decimal.Parse(Qcrd053.Text);
            item.Qcrd054 = decimal.Parse(Qcrd054.Text);
            item.Qcrd055 = decimal.Parse(Qcrd055.Text);
            item.Qcrd056 = Qcrd056.Text;
            item.Qcrd057 = decimal.Parse(Qcrd057.Text);

            item.Qcrd058 = (Qcrd058.Text);
            item.Qcrd059 = Qcrd059.Text;

            item.Qcrd060 = decimal.Parse(Qcrd060.Text);
            item.Qcrd061 = decimal.Parse(Qcrd061.Text);
            item.Qcrd062 = (Qcrd062.Text);
            item.Qcrdpcbrec = Qcrdpcbrec.Text;

            item.Qcrdpcbrec = Qcrdpcbrec.Text;

            item.Creator = GetIdentityName();
            item.CreateDate = DateTime.Now;
            DB.Qm_Reworks.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Qcrd001.Text + "," + Qcrd002.Text + "," + Qcrd003.Text + "," + Qcrd004.Text + "," + Qcrd005.Text + "," + Qcrd006.Text + "," + Qcrdqarec.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量成本", "改修对应新增", OperateNotes);
        }

        protected void Qcrd002_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDDLLot();
        }

        protected void Qcrd001_TextChanged(object sender, EventArgs e)
        {
            CheckIFRS();
        }

        public void qQcrd007()//检讨・调查・试验费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd008.Text != "" && Qcrd009.Text != "" && Qcrd010.Text != "" && Qcrd011.Text != "" && Qcrd012.Text != "" && Qcrd013.Text != "")
                {
                    //直接
                    //(decimal.Parse(this.Qcrd008.Text) * decimal.Parse(this.Qcrd009.Text)* decimal.Parse(this.Qcrd004.Text)
                    //decimal.Parse(this.Qcrd011.Text) * decimal.Parse(this.Qcrd009.Text) * decimal.Parse(this.Qcrd004.Text)
                    //间接
                    //(decimal.Parse(this.Qcrd008.Text) * decimal.Parse(this.Qcrd010.Text) * decimal.Parse(this.Qcrd005.Text)
                    //decimal.Parse(this.Qcrd011.Text) * decimal.Parse(this.Qcrd010.Text) * decimal.Parse(this.Qcrd005.Text)

                    Qcrd007.Text = (decimal.Parse(this.Qcrd008.Text) * decimal.Parse(this.Qcrd009.Text) * decimal.Parse(this.Qcrd004.Text) + decimal.Parse(this.Qcrd010.Text) * decimal.Parse(this.Qcrd008.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd011.Text) * decimal.Parse(this.Qcrd009.Text) * decimal.Parse(this.Qcrd004.Text) + decimal.Parse(this.Qcrd010.Text) * decimal.Parse(this.Qcrd011.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd012.Text)).ToString();
                }
            }
            //Qcrd008,Qcrd011,Qcrd012,Qcrd013,
        }

        protected void Qcrd008_TextChanged(object sender, EventArgs e)
        {
            qQcrd007();
        }

        protected void Qcrd009_TextChanged(object sender, EventArgs e)
        {
            qQcrd007();
        }

        protected void Qcrd010_TextChanged(object sender, EventArgs e)
        {
            qQcrd007();
        }

        protected void Qcrd011_TextChanged(object sender, EventArgs e)
        {
            qQcrd007();
        }

        protected void Qcrd012_TextChanged(object sender, EventArgs e)
        {
            qQcrd007();
        }

        public void qQcrd013()//其他费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd014.Text != "" && Qcrd015.Text != "")
                {
                    Qcrd013.Text = (decimal.Parse(this.Qcrd014.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd015.Text)).ToString();
                }
            }
            //Qcrd14,Qcrd015
        }

        protected void Qcrd014_TextChanged(object sender, EventArgs e)
        {
            qQcrd013();
        }

        protected void Qcrd015_TextChanged(object sender, EventArgs e)
        {
            qQcrd013();
        }

        public void qQcrd020()//选别・改修费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd021.Text != "" && Qcrd022.Text != "" && Qcrd023.Text != "" && Qcrd024.Text != "" && Qcrd025.Text != "")
                {
                    Qcrd020.Text = (decimal.Parse(this.Qcrd021.Text) * decimal.Parse(this.Qcrd004.Text) + decimal.Parse(this.Qcrd022.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd023.Text) + decimal.Parse(this.Qcrd024.Text) + decimal.Parse(this.Qcrd025.Text)).ToString();
                }
            }
            //Qcrd021,Qcrd022,Qcrd023,Qcrd024,Qcrd025
        }

        protected void Qcrd021_TextChanged(object sender, EventArgs e)
        {
            qQcrd020();
        }

        protected void Qcrd022_TextChanged(object sender, EventArgs e)
        {
            qQcrd020();
        }

        protected void Qcrd023_TextChanged(object sender, EventArgs e)
        {
            qQcrd020();
        }

        protected void Qcrd024_TextChanged(object sender, EventArgs e)
        {
            qQcrd020();
        }

        protected void Qcrd025_TextChanged(object sender, EventArgs e)
        {
            qQcrd020();
        }

        public void qQcrd027()//请求费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd030.Text != "" && Qcrd031.Text != "")
                {
                    Qcrd027.Text = (decimal.Parse(this.Qcrd030.Text) + decimal.Parse(this.Qcrd031.Text)).ToString();
                }
            }
            //Qcrd030,Qcrd031
        }

        protected void Qcrd030_TextChanged(object sender, EventArgs e)
        {
            qQcrd027();
        }

        protected void Qcrd031_TextChanged(object sender, EventArgs e)
        {
            qQcrd027();
        }

        public void qQcrd035()//选别・改修费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd036.Text != "" && Qcrd037.Text != "" && Qcrd038.Text != "" && Qcrd039.Text != "" && Qcrd040.Text != "")
                {
                    Qcrd035.Text = (decimal.Parse(this.Qcrd036.Text) * decimal.Parse(this.Qcrd004.Text) + decimal.Parse(this.Qcrd037.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd038.Text) + decimal.Parse(this.Qcrd039.Text) + decimal.Parse(this.Qcrd040.Text)).ToString();
                }
            }
            //Qcrd036,Qcrd037,Qcrd038,Qcrd039,Qcrd040
        }

        protected void Qcrd036_TextChanged(object sender, EventArgs e)
        {
            qQcrd035();
        }

        protected void Qcrd037_TextChanged(object sender, EventArgs e)
        {
            qQcrd035();
        }

        protected void Qcrd038_TextChanged(object sender, EventArgs e)
        {
            qQcrd035();
        }

        protected void Qcrd039_TextChanged(object sender, EventArgs e)
        {
            qQcrd035();
        }

        protected void Qcrd040_TextChanged(object sender, EventArgs e)
        {
            qQcrd035();
        }

        public void qQcrd042()//请求费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd045.Text != "" && Qcrd046.Text != "")
                {
                    Qcrd042.Text = (decimal.Parse(this.Qcrd045.Text) + decimal.Parse(this.Qcrd046.Text)).ToString();
                }
            }
            //Qcrd045,Qcrd046
        }

        protected void Qcrd045_TextChanged(object sender, EventArgs e)
        {
            qQcrd042();
        }

        protected void Qcrd046_TextChanged(object sender, EventArgs e)
        {
            qQcrd042();
        }

        public void qQcrd050()//选别・改修费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd051.Text != "" && Qcrd051.Text != "" && Qcrd053.Text != "" && Qcrd054.Text != "" && Qcrd055.Text != "")
                {
                    Qcrd050.Text = (decimal.Parse(this.Qcrd051.Text) * decimal.Parse(this.Qcrd004.Text) + decimal.Parse(this.Qcrd052.Text) * decimal.Parse(this.Qcrd005.Text) + decimal.Parse(this.Qcrd053.Text) + decimal.Parse(this.Qcrd054.Text) + decimal.Parse(this.Qcrd055.Text)).ToString();
                }
            }
            //Qcrd051,Qcrd052,Qcrd053,Qcrd054,Qcrd055
        }

        protected void Qcrd051_TextChanged(object sender, EventArgs e)
        {
            qQcrd050();
        }

        protected void Qcrd052_TextChanged(object sender, EventArgs e)
        {
            qQcrd050();
        }

        protected void Qcrd053_TextChanged(object sender, EventArgs e)
        {
            qQcrd050();
        }

        protected void Qcrd054_TextChanged(object sender, EventArgs e)
        {
            qQcrd050();
        }

        protected void Qcrd055_TextChanged(object sender, EventArgs e)
        {
            qQcrd050();
        }

        public void qQcrd057()//请求费用
        {
            if (Qcrd004.Text != "" && Qcrd005.Text != "")
            {
                if (Qcrd060.Text != "" && Qcrd061.Text != "")
                {
                    Qcrd057.Text = (decimal.Parse(this.Qcrd060.Text) + decimal.Parse(this.Qcrd061.Text)).ToString();
                }
            }
            //Qcrd060,Qcrd061
        }

        protected void Qcrd060_TextChanged(object sender, EventArgs e)
        {
            qQcrd057();
        }

        protected void Qcrd061_TextChanged(object sender, EventArgs e)
        {
            qQcrd057();
        }

        #endregion Events

        protected void Qcrd016_CheckedChanged(object sender, CheckedEventArgs e)
        {
            if (Qcrd016.Checked == true)
            {
                if (CheckPower(""))
                {
                    this.Tab2.Enabled = true;
                }
                if (CheckPower(""))
                {
                    this.Tab3.Enabled = true;
                }
                if (CheckPower(""))
                {
                    this.Tab4.Enabled = true;
                }
            }
            else
            {
                this.Tab2.Enabled = false;
                this.Tab3.Enabled = false;
                this.Tab4.Enabled = false;
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckData();
        }
    }
}