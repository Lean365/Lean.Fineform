using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class waste_cost_new : PageBase
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
                return "CoreWasteCostNew";
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
            this.Qcwd001.SelectedDate = DateTime.Now;
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

            BindDdlModels();

            Qcwdrec.Text = GetIdentityName();
        }

        #region BindDdlData

        //机种选择
        private void BindDdlModels()
        {
            //查询LINQ去重复
            var q = from a in DB.Pp_Manhours
                        //join b in DB.Pp_Orders on a.Proitem equals b.Porderhbn
                        //where a.Promodel == this.Qcrd002.SelectedItem.Text
                        //where a.lineclass == "M"
                    select new
                    {
                        a.Promodel//b.Porderlot
                    };

            var qs = q.Select(E => new { E.Promodel }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            Qcwd002.DataSource = qs;
            Qcwd002.DataTextField = "Promodel";
            Qcwd002.DataValueField = "Promodel";
            Qcwd002.DataBind();

            // 选中根节点
            this.Qcwd002.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //物料选择
        private void BindDdlMatItems()
        {
            if (Qcwd002.SelectedIndex != 0 || Qcwd002.SelectedIndex != -1)
            {
                //查询LINQ去重复
                var q = from a in DB.Mm_Materials
                            //join b in DB.proOrders on a.Proitem equals b.Porderhbn
                            //where a.Promodel == this.Qcrd002.SelectedItem.Text
                        where a.MovingAvg > 0
                        select new
                        {
                            a.MatItem//b.Porderlot
                        };

                var qs = q.Select(E => new { E.MatItem }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                Qcwd004.DataSource = qs;
                Qcwd004.DataTextField = "MatItem";
                Qcwd004.DataValueField = "MatItem";
                Qcwd004.DataBind();

                this.Qcwd004.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        //物料文本
        private void BindDataMatItemTxt()
        {
            if (Qcwd004.SelectedIndex != -1)
            {
                var q = from a in DB.Mm_Materials
                        where a.MatItem.Contains(this.Qcwd004.SelectedItem.Text.Trim())
                        select a;
                var qs = q.Distinct().ToList();
                if (qs.Any())
                {
                    Qcwd005.Text = qs[0].MatDescription;//物料文本
                    Qcwd009.Text = (qs[0].MovingAvg / qs[0].PriceUnit).ToString();//单价
                }
            }
        }

        //判断财务数据存在
        private void CheckIFRS()
        {
            string sdate = this.Qcwd001.SelectedDate.Value.ToString("yyyyMM");
            var q = from a in DB.Qm_Wagerates
                    where a.Qcsd001.Contains(sdate)
                    select a;

            var qs = q.Distinct().ToList();
            if (qs.Any())
            {
                //Qcrd004.Text = qs[0].Qcsd006.ToString();//直接
                Qcwd003.Text = qs[0].Qcsd010.ToString();//间接
            }
            else

            {
                Alert.ShowInTop("请先录入财务数据！", "警告", MessageBoxIcon.Error);

                //return;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        #endregion BindDdlData

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
            string InputData = Qcwd001.SelectedDate.Value.ToString("yyyyMMdd") + Qcwd002.Text.Trim() + Qcwd004.Text.Trim();

            Qm_Waste Redata = DB.Qm_Wastes.Where(u => u.Qcwd001 + u.Qcwd002 + u.Qcwd004 == InputData).FirstOrDefault();

            if (Redata != null)
            {
                Alert.ShowInTop("废弃事故数据,物料< " + InputData + ">已经存在！修改即可");
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
            SaveItem();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void CheckDDLData()
        {
            if (Qcwd002.SelectedItem.Text == "请选择")// && LA002.SelectedIndex> 1 && LA003.SelectedIndex > 1 && LA009.SelectedIndex > 1 && LA011.SelectedIndex > 1 && LA012.SelectedIndex > 1)
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }
            if (Qcwd004.SelectedItem.Text == "请选择")// && LA002.SelectedIndex> 1 && LA003.SelectedIndex > 1 && LA009.SelectedIndex > 1 && LA011.SelectedIndex > 1 && LA012.SelectedIndex > 1)
            {
                Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
                return;
            }
            CheckData();
        }

        //字段赋值，保存
        private void SaveItem()//新增质量控制数据
        {
            Qm_Waste item = new Qm_Waste();
            item.Qcwd001 = Qcwd001.SelectedDate.Value.ToString("yyyyMMdd");
            item.Qcwd002 = Qcwd002.SelectedItem.Text;
            item.Qcwd003 = decimal.Parse(Qcwd003.Text);
            item.Qcwd004 = Qcwd004.SelectedItem.Text;

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
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateDate = DateTime.Now;
            DB.Qm_Wastes.Add(item);
            DB.SaveChanges();

            //新增日志
            string Newtext = Qcwd001.Text + "," + Qcwd002.Text + "," + Qcwd003.Text + "," + Qcwd004.Text + "," + Qcwd005.Text + "," + Qcwd006.Text + "," + Qcwdrec.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量成本", "废弃处理新增", OperateNotes);
        }

        protected void Qcwd002_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDdlMatItems();
        }

        protected void Qcwd004_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataMatItemTxt();
        }

        protected void Qcwd001_TextChanged(object sender, EventArgs e)
        {
            CheckIFRS();
            BindDdlModels();
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
            if (decimal.Parse(Qcwd009.Text) > 0)
            {
                dQcwd007();
            }
            else
            {
                Alert.ShowInTop("请确认单价是否正确！", "错误提示", MessageBoxIcon.Error);
                return;
            }
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
            if (decimal.Parse(Qcwd009.Text) > 0)
            {
                CheckDDLData();
            }
            else
            {
                Alert.ShowInTop("请确认单价是否正确！", "错误提示", MessageBoxIcon.Error);

                return;
            }
        }

        #endregion Events
    }
}