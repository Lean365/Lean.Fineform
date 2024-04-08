using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.Master
{
    public partial class Pp_model_kanban_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreKanbanNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string tmpRootDir, tmpQRCodePath;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            //labResult.Text = "";// String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            dpkP_Kanban_Date.SelectedDate = DateTime.Now.AddDays(1);//打印将要生产的机种资料
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDDLproLine();
        }

        private void BindDDLproLine()
        {
            var q = from a in DB.Adm_Dicts
                    where a.DictType.Contains("line_type_m")
                    select new
                    {
                        a.DictValue,
                        a.DictLabel
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlP_Kanban_Line.DataSource = qs;
            ddlP_Kanban_Line.DataTextField = "DictLabel";
            ddlP_Kanban_Line.DataValueField = "DictValue";
            ddlP_Kanban_Line.DataBind();
            this.ddlP_Kanban_Line.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindDDLproOrder()//工单信息
        {
            var q = from p in DB.Pp_Orders
                        //where p.Porderhbn.Contains(Pitem)
                        //where p.Prodate.Contains(Prodate) && p.Prolinename.Contains(pline) &&
                        //(from d in DB.pp_defects
                        // where d.Prongbdel == false
                        // where d.Prodate== Prodate
                        // where d.Prolinename== pline
                        // select d.Prolot)
                        //  .Contains(p.Prolot)
                    select new
                    {
                        Porderno = p.Porderno,
                    };

            var qs = q.Select(E => new { E.Porderno }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlP_Kanban_Order.DataSource = qs;
            ddlP_Kanban_Order.DataTextField = "Porderno";
            ddlP_Kanban_Order.DataValueField = "Porderno";
            ddlP_Kanban_Order.DataBind();
            this.ddlP_Kanban_Order.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion Page_Load

        #region Events

        protected void ddlP_Kanban_Line_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlP_Kanban_Line.SelectedIndex != -1 || ddlP_Kanban_Line.SelectedIndex != 0)
            {
                BindDDLproOrder();
            }
        }

        protected void ddlP_Kanban_Order_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlP_Kanban_Order.SelectedIndex != -1 || ddlP_Kanban_Order.SelectedIndex != 0)
            {
                string orderno = ddlP_Kanban_Order.SelectedItem.Text;
                var qOrder = (from a in DB.Pp_Orders
                              where a.Porderno == orderno
                              select a).ToList();
                if (qOrder.Any())
                {
                    lblP_Kanban_Lot.Text = qOrder[0].Porderlot.ToString();
                    lblP_Kanban_Item.Text = qOrder[0].Porderhbn.ToString();
                    string bhn = qOrder[0].Porderhbn.ToString();
                    var qName = (from a in DB.Mm_Materials
                                 where a.MatItem.Contains(bhn)
                                 select a).ToList();
                    if (qName.Any())
                    {
                        lblD_SAP_ZCA1D_Z005.Text = qName[0].MatDescription.ToString();
                    }
                    var qModel = (from a in DB.Pp_SapModelDests
                                  where a.D_SAP_DEST_Z001.Contains(bhn)
                                  select a).ToList();
                    if (qModel.Any())
                    {
                        lblP_Kanban_Model.Text = qModel[0].D_SAP_DEST_Z002.ToString();
                        lblP_Kanban_Region.Text = qModel[0].D_SAP_DEST_Z003.ToString();
                    }
                }
            }
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Pp_Kanban item = new Pp_Kanban();
            item.P_Kanban_Date = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd");
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.P_Kanban_Line = ddlP_Kanban_Line.SelectedItem.Text;
            item.P_Kanban_Order = ddlP_Kanban_Order.SelectedItem.Text;
            item.P_Kanban_Item = lblP_Kanban_Item.Text;
            item.P_Kanban_Lot = lblP_Kanban_Lot.Text;
            item.P_Kanban_Model = lblP_Kanban_Model.Text;
            item.P_Kanban_Region = lblP_Kanban_Region.Text;
            item.P_Kanban_Process = int.Parse(numP_Kanban_Process.Text);
            item.UDF01 = tmpQRCodePath;
            item.GUID = Guid.NewGuid();
            // 添加所有用户
            item.Remark = tbxRemark.Text;
            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_Kanbans.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + ddlP_Kanban_Order.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text + "," + lblP_Kanban_Model.Text + "," + lblP_Kanban_Region.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "机种看板新增", OperateNotes);
        }

        private void CheckData()
        {
            ////判断修改内容
            //int id = GetQueryIntValue("id");
            //proLine current = DB.proLines.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.linename;

            //if (this.linename.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = "数据没有被修改";
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    alert.Show();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}
            //判断重复

            if (ddlP_Kanban_Line.SelectedIndex != 0 || ddlP_Kanban_Line.SelectedIndex != -1)
            {
                if (ddlP_Kanban_Order.SelectedIndex != 0 || ddlP_Kanban_Order.SelectedIndex != -1)
                {
                    string InputData = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + ddlP_Kanban_Line.SelectedItem.Text + ddlP_Kanban_Order.SelectedItem.Text + lblP_Kanban_Lot.Text + lblP_Kanban_Item.Text + lblP_Kanban_Model.Text + lblP_Kanban_Region.Text;

                    Pp_Kanban redata = DB.Pp_Kanbans.Where(u => u.P_Kanban_Date + u.P_Kanban_Line + u.P_Kanban_Order + u.P_Kanban_Lot + u.P_Kanban_Item + u.P_Kanban_Model + u.P_Kanban_Region == (InputData)).FirstOrDefault();

                    if (redata != null)
                    {
                        Alert.Show("基本信息,此机种看板< " + InputData + ">已经存在！修改即可");
                        return;
                    }
                }
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                CheckData();
                SaveItem();
            }
            catch (ArgumentNullException Message)
            {
                Alert.Show("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.Show("使用无效的类:" + Message);
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
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        protected void numP_Kanban_Process_TextChanged(object sender, EventArgs e)
        {
            string ModelQRCode = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text + "," + lblP_Kanban_Model.Text + "," + lblP_Kanban_Region.Text + "," + numP_Kanban_Process.Text;
            string ImgName = (dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text).Replace(" ", "");
            QrcodeHelper.QRCodeHandler qr = new QrcodeHelper.QRCodeHandler();
            string path = tmpRootDir + "oneFile\\qrcode\\";
            string qrString = ModelQRCode;                         //二维码字符串
            string logoFilePath = path + "teac.jpg";                                    //Logo路径50*50
            string filePath = path + ImgName + ".jpg";                                        //二维码文件名
            qr.CreateQRCode(qrString, "Byte", 5, 0, "H", filePath, true, logoFilePath);   //生成
            tmpQRCodePath = "~/oneFile/qrcode/" + ImgName + ".jpg";
            this.imgModelQrcode.ImageUrl = tmpQRCodePath;
            this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
            this.imgModelQrcode.ImageHeight = Unit.Pixel(64);
        }

        #endregion Events
    }
}