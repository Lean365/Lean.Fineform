using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_model_kanban_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreKanbanEdit";
            }
        }

        #endregion

        #region Page_Load
        public static string tmpRootDir,tmpQRCodePath;
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
            //tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            //labResult.Text = "";// String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            
            BindDDLproLine();
            BindData();
        }
        private void BindData()
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Kanban current = DB.Pp_Kanbans.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.Show(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            dpkP_Kanban_Date.SelectedDate = DateTime.ParseExact(current.P_Kanban_Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            ddlP_Kanban_Line.SelectedValue = current.P_Kanban_Line;
            lblP_Kanban_Order.Text = current.P_Kanban_Order;
            lblP_Kanban_Item.Text = current.P_Kanban_Item;
            lblP_Kanban_Lot.Text = current.P_Kanban_Lot;
            lblP_Kanban_Model.Text = current.P_Kanban_Model;
            lblP_Kanban_Region.Text = current.P_Kanban_Region;
            numP_Kanban_Process.Text = current.P_Kanban_Process.ToString();
            this.imgModelQrcode.ImageUrl = current.UDF01.Replace(" ", ""); ;
            this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
            this.imgModelQrcode.ImageHeight = Unit.Pixel(64);


            string qItem = current.P_Kanban_Item;
            var q = (from a in DB.Mm_Materials
                     where a.MatItem == (qItem)
                     select a);
            var qs = q.ToList();
            if (qs.Any())
            {
                lblD_SAP_ZCA1D_Z005.Text = qs[0].MatDescription.ToString();
            }

            tbxRemark.Text = current.Remark;
            //this.Lineguid.Text = current.GUID.ToString();
            // 添加所有用户



            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeModi = current.P_Kanban_Date + "," + current.P_Kanban_Line + "," + current.P_Kanban_Item + "," + current.P_Kanban_Lot + "," + current.P_Kanban_Model + "," + current.P_Kanban_Region;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "机种看板修改", OperateNotes);
        }

        private void BindDDLproLine()
        {

            var q = from p in DB.Pp_Lines
                    where p.lineclass.Contains("M")
                    //where p.Prodate.Contains(Prodate) && p.Prolinename.Contains(pline) && 
                    //(from d in DB.Pp_Defects
                    // where d.Prongbdel == false
                    // where d.Prodate== Prodate
                    // where d.Prolinename== pline
                    // select d.Prolot)
                    //  .Contains(p.Prolot)
                    select new
                    {
                        Line = p.linename,

                    };

            var qs = q.Select(E => new { E.Line }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlP_Kanban_Line.DataSource = qs;
            ddlP_Kanban_Line.DataTextField = "Line";
            ddlP_Kanban_Line.DataValueField = "Line";
            ddlP_Kanban_Line.DataBind();
            this.ddlP_Kanban_Line.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));

        }




        #endregion

        #region Events



        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Kanban item = DB.Pp_Kanbans

                .Where(u => u.GUID == id).FirstOrDefault();

            item.P_Kanban_Date = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd");
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.P_Kanban_Line = ddlP_Kanban_Line.SelectedItem.Text;
            item.P_Kanban_Process = int.Parse(numP_Kanban_Process.Text);
            item.UDF01 = tmpQRCodePath;
            //item.GUID = Guid.NewGuid();
            // 添加所有用户
            item.Remark = tbxRemark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            //DB.Pp_Kanbans.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text + "," + lblP_Kanban_Model.Text + "," + lblP_Kanban_Region.Text;
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

            if (ddlP_Kanban_Line.SelectedIndex == 0 && ddlP_Kanban_Line.SelectedIndex == -1)
            {

                string InputData = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + ddlP_Kanban_Line.SelectedItem.Text + lblP_Kanban_Item.Text + lblP_Kanban_Lot.Text + lblP_Kanban_Model.Text + lblP_Kanban_Region.Text;


                Pp_Kanban redata = DB.Pp_Kanbans.Where(u => u.P_Kanban_Date + u.P_Kanban_Line + u.P_Kanban_Item + u.P_Kanban_Lot + u.P_Kanban_Model + u.P_Kanban_Region == (InputData)).FirstOrDefault();

                if (redata != null)
                {
                    Alert.Show("基本信息,此机种看板< " + InputData + ">已经存在！修改即可");
                    return;
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
            string strKey = (dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text).Replace(" ", ""); ;//匹配字符串

            DirectoryInfo myDir = new DirectoryInfo(tmpRootDir + "oneFile\\qrcode\\");
            FileInfo[] _fileList = myDir.GetFiles();
            for (int i = 0; i < _fileList.Length; i++)
            {
                if (_fileList[i].Name.Contains(strKey))
                {
                    String DelFileName = _fileList[i].Name;

                    QrcodeHelper.DeleteDirectory(tmpRootDir + "oneFile\\qrcode\\", tmpRootDir + "oneFile\\qrcode\\" + DelFileName);
                }
            }



            //item.D_SAP_DEST_Z001 = ddlD_SAP_DEST_Z001.SelectedItem.Text;
            ////item.Prolineclass = prolinename.SelectedValue.ToString();
            //item.D_SAP_DEST_Z002 = tbxD_SAP_DEST_Z002.Text;
            //item.D_SAP_DEST_Z003 = tbxD_SAP_DEST_Z003.Text;
            //item.Udf004 = Decimal.Parse(numUdf004.Text);


            //string imgPath = Request.ApplicationPath;

            string ModelQRCode = dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text + "," + lblP_Kanban_Model.Text + "," + lblP_Kanban_Region.Text + "," + numP_Kanban_Process.Text;
            string ImgName = (dpkP_Kanban_Date.SelectedDate.Value.ToString("yyyyMMdd") + "," + ddlP_Kanban_Line.SelectedItem.Text + "," + lblP_Kanban_Item.Text + "," + lblP_Kanban_Lot.Text).Replace(" ", ""); ;
            QrcodeHelper.QRCodeHandler qr = new QrcodeHelper.QRCodeHandler();
            string path = tmpRootDir + "oneFile\\qrcode\\";
            string qrString = ModelQRCode;                         //二维码字符串
            string logoFilePath = path + "teac.jpg";                                    //Logo路径50*50
            string filePath = path + ImgName + ".jpg";                                        //二维码文件名
            qr.CreateQRCode(qrString, "Byte", 5, 0, "H", filePath, true, logoFilePath);   //生成
            tmpQRCodePath= "~/oneFile/qrcode/" + ImgName + ".jpg";
            this.imgModelQrcode.ImageUrl = tmpQRCodePath;
            this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
            this.imgModelQrcode.ImageHeight = Unit.Pixel(64);
        }






        #endregion


    }
}
