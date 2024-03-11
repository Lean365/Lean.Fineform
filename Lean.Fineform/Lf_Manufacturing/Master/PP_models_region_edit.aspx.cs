using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_models_region_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreModelsEdit";
            }
        }

        #endregion

        #region Page_Load
        public static string tmpRootDir;
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

            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_SapModelDest current = DB.Pp_SapModelDests.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.Show(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }



            lblD_SAP_DEST_Z001.Text = current.D_SAP_DEST_Z001;
            string qItem= current.D_SAP_DEST_Z001;
            var q = (from a in DB.Mm_Materials
                     where a.MatItem==(qItem)
                     select a);
            var qs = q.ToList();
            if(qs.Any())
            {
                lblD_SAP_ZCA1D_Z005.Text = qs[0].MatDescription.ToString();
            }
            //Pp_SapMaterial ItemText = DB.Pp_SapMaterials.Find(current.D_SAP_DEST_Z001);
            //if (ItemText != null)
            //{
            //    lblD_SAP_ZCA1D_Z005.Text = ItemText.D_SAP_ZCA1D_Z005;
            //}
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            tbxD_SAP_DEST_Z002.Text = current.D_SAP_DEST_Z002;
            tbxD_SAP_DEST_Z003.Text = current.D_SAP_DEST_Z003;

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
            string BeforeModi = current.D_SAP_DEST_Z001;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "机种仕向修改", OperateNotes);
        }







        #endregion

        #region Events

        private void CheckData()
        {
            //判断修改内容
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_SapModelDest current = DB.Pp_SapModelDests.Find(id);
            //decimal cQcpd005 = current.Qcpd005;
            string checkItem = current.D_SAP_DEST_Z001;
            string checkModel = current.D_SAP_DEST_Z002;
            string ckeckRegion = current.D_SAP_DEST_Z003;
            string checkScheduling = current.UDF04.ToString();

            if (this.lblD_SAP_DEST_Z001.Text == checkItem)
            {
                if (this.tbxD_SAP_DEST_Z002.Text == checkModel)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
                {
                    if (this.tbxD_SAP_DEST_Z003.Text == ckeckRegion)


                            Alert.ShowInTop("数据没有被修改", "警告提示", MessageBoxIcon.Information);
                            //Alert alert = new Alert();
                            //alert.Message = "数据没有被修改";
                            //alert.MessageBoxIcon = (MessageBoxIcon)Enum.Parse(typeof(MessageBoxIcon), "Information", true);

                            //"Information" Text = "消息"   
                            //"Warning" Text = "警告"       
                            //"Question" Text = "问题"          
                            // "Error" Text = "错误"            
                            //"Success" Text = "成功"
                            //alert.IconFont = IconFont.Warning;
                            //alert.Icon = Icon.Error;
                            //alert.IconUrl = "~/Lf_Resources/images/warning.png";

                            //alert.Target = Target.Top;
                            //alert.Show();
                            //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

                }
            }


            //    //判断重复
            //    string InputData = Qcpd003.SelectedItem.Text.Trim();


            //    proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            //    if (redata != null)
            //    {
            //        Alert.Show("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //        return;
            //    }
        }
        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_SapModelDest item = DB.Pp_SapModelDests

                .Where(u => u.GUID == id).FirstOrDefault();



            //item.Prolineclass = prolinename.SelectedValue.ToString();
            //item.D_SAP_DEST_Z001 = lblD_SAP_DEST_Z001.Text;
            item.D_SAP_DEST_Z002 = tbxD_SAP_DEST_Z002.Text;
            item.D_SAP_DEST_Z003 = tbxD_SAP_DEST_Z003.Text;


            item.Remark = tbxRemark.Text;
            item.ModifyTime = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = lblD_SAP_DEST_Z001.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "机种仕向修改", OperateNotes);


        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            //string inputUserName = tbxName.Text.Trim();

            //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            //if (user != null)
            //{
            //    Alert.Show("用户 " + inputUserName + " 已经存在！");
            //    return;
            //}
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
        //protected void numUdf004_TextChanged(object sender, EventArgs e)
        //{
        //    tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
            

            
        //    string strKey = lblD_SAP_DEST_Z001.Text;//匹配字符串

        //    DirectoryInfo myDir = new DirectoryInfo(tmpRootDir + "oneFile\\qrcode\\");
        //    FileInfo[] _fileList = myDir.GetFiles();
        //    for (int i = 0; i < _fileList.Length; i++)
        //    {
        //        if (_fileList[i].Name.Contains(strKey))
        //        {
        //            String DelFileName = _fileList[i].Name;

        //            QrcodeHelper.DeleteDirectory(tmpRootDir + "oneFile\\qrcode\\", tmpRootDir + "oneFile\\qrcode\\"+ DelFileName);
        //        }
        //    }



        //    //item.D_SAP_DEST_Z001 = ddlD_SAP_DEST_Z001.SelectedItem.Text;
        //    ////item.Prolineclass = prolinename.SelectedValue.ToString();
        //    //item.D_SAP_DEST_Z002 = tbxD_SAP_DEST_Z002.Text;
        //    //item.D_SAP_DEST_Z003 = tbxD_SAP_DEST_Z003.Text;
        //    //item.Udf004 = Decimal.Parse(numUdf004.Text);


        //    //string imgPath = Request.ApplicationPath;

        //        string ModelQRCode = DateTime.Now.ToString("yyyyMMdd") + "," + lblD_SAP_DEST_Z001.Text +","+ tbxD_SAP_DEST_Z002.Text + "," + tbxD_SAP_DEST_Z003.Text + "," + numUdf004.Text;


        //        QrcodeHelper.QRCodeHandler qr = new QrcodeHelper.QRCodeHandler();
        //        string path = tmpRootDir + "oneFile\\qrcode\\";
        //        string qrString = ModelQRCode;                         //二维码字符串
        //        string logoFilePath = path + "teac.jpg";                                    //Logo路径50*50
        //        string filePath = path + ModelQRCode + ".jpg";                                        //二维码文件名
        //        qr.CreateQRCode(qrString, "Byte", 5, 0, "H", filePath, true, logoFilePath);   //生成
        //        this.imgModelQrcode.ImageUrl = "~/oneFile/qrcode/" + ModelQRCode + ".jpg";
        //        this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
        //        this.imgModelQrcode.ImageHeight = Unit.Pixel(64);


        //}
        #endregion









    }
}
