using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace Fine.Lf_Manufacturing.Master
{

    public partial class Pp_models_region_new : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreModelsNew";
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
            labResult.Text = "";// String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.班组类别：制二课请输入：P,制一课请输入：M,品保部请输入：Q</div><div>2.班组类别：只能输入P，Q,M。</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDDLHBN();
        }



        private void BindDDLHBN()//物料信息
        {
            var q = from a in DB.Pp_SapMaterials
                    where a.D_SAP_ZCA1D_Z004!="ROH"
                    where a.D_SAP_ZCA1D_Z010!="F"
                    where a.D_SAP_ZCA1D_Z011!="50"
                    where a.D_SAP_ZCA1D_Z002!="" && !(from d in DB.Pp_SapModelDests
                      where d.isDelete == 0

                      where d.D_SAP_DEST_Z001 == a.D_SAP_ZCA1D_Z002
                      select d.D_SAP_DEST_Z001)
                       .Contains(a.D_SAP_ZCA1D_Z002)
                    select new
                    {
                        a.D_SAP_ZCA1D_Z002,

                    };
            var qs = q.Select(E => new { E.D_SAP_ZCA1D_Z002}).ToList().Distinct();



            // 绑定到下拉列表（启用模拟树功能）

            ddlD_SAP_DEST_Z001.DataTextField = "D_SAP_ZCA1D_Z002";
            ddlD_SAP_DEST_Z001.DataValueField = "D_SAP_ZCA1D_Z002";
            ddlD_SAP_DEST_Z001.DataSource = q;
            ddlD_SAP_DEST_Z001.DataBind();

            // 选中根节点
            this.ddlD_SAP_DEST_Z001.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));


        }



        #endregion

        #region Events
        protected void ddlD_SAP_DEST_Z001_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlD_SAP_DEST_Z001.SelectedIndex == -1 && ddlD_SAP_DEST_Z001.SelectedIndex == 0)
            {
                var q = (from a in DB.Pp_SapMaterials
                         where a.D_SAP_ZCA1D_Z002.Contains(ddlD_SAP_DEST_Z001.SelectedItem.Text.Trim())
                         select a).ToList();
                if (q.Any())
                {
                    lblD_SAP_ZCA1D_Z005.Text = q[0].D_SAP_ZCA1D_Z005.ToString();
                }
            }

        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            Pp_SapModelDest item = new Pp_SapModelDest();
            item.D_SAP_DEST_Z001 = ddlD_SAP_DEST_Z001.SelectedItem.Text;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.D_SAP_DEST_Z002 = tbxD_SAP_DEST_Z002.Text;
            item.D_SAP_DEST_Z003 = tbxD_SAP_DEST_Z003.Text;
            //item.Udf001 =  "~/oneFile/qrcode/"+ DateTime.Now.ToString("yyyyMMdd") + "," + ddlD_SAP_DEST_Z001.SelectedItem.Text + "," + tbxD_SAP_DEST_Z002.Text + "," + tbxD_SAP_DEST_Z003.Text + "," + numUdf004.Text+".jpg";
            //item.Udf004 = Decimal.Parse(numUdf004.Text);

            item.GUID = Guid.NewGuid();
            // 添加所有用户
            item.Remark = tbxRemark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_SapModelDests.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = ddlD_SAP_DEST_Z001.SelectedItem.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Contectext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "基础资料", "机种新增", OperateNotes);


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
            string InputData = ddlD_SAP_DEST_Z001.SelectedItem.Text.Trim();


            Pp_SapModelDest redata = DB.Pp_SapModelDests.Where(u => u.D_SAP_DEST_Z001 == InputData).FirstOrDefault();

            if (redata != null)
            {
                Alert.Show("基本信息,此物料机种仕向< " + InputData + ">已经存在！修改即可");
                return;
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


        //protected void numUdf004_TextChanged(object sender, EventArgs e)
        //{

        //    //item.D_SAP_DEST_Z001 = ddlD_SAP_DEST_Z001.SelectedItem.Text;
        //    ////item.Prolineclass = prolinename.SelectedValue.ToString();
        //    //item.D_SAP_DEST_Z002 = tbxD_SAP_DEST_Z002.Text;
        //    //item.D_SAP_DEST_Z003 = tbxD_SAP_DEST_Z003.Text;
        //    //item.Udf004 = Decimal.Parse(numUdf004.Text);
            

        //    //string imgPath = Request.ApplicationPath;
        //    string ModelQRCode =DateTime.Now.ToString("yyyyMMdd")+","+ ddlD_SAP_DEST_Z001.SelectedItem.Text + "," + tbxD_SAP_DEST_Z002.Text + "," + tbxD_SAP_DEST_Z003.Text + "," + numUdf004.Text;

        //    QrcodeHelper.QRCodeHandler qr = new QrcodeHelper.QRCodeHandler();
        //    string path = tmpRootDir + "oneFile\\qrcode\\";
        //    string qrString = ModelQRCode;                         //二维码字符串
        //    string logoFilePath = path + "teac.jpg";                                    //Logo路径50*50
        //    string filePath = path + ModelQRCode + ".jpg";                                        //二维码文件名
        //    qr.CreateQRCode(qrString, "Byte", 5, 0, "H", filePath, true, logoFilePath);   //生成
        //    this.imgModelQrcode.ImageUrl = "~/oneFile/qrcode/" + ModelQRCode + ".jpg";
        //    this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
        //    this.imgModelQrcode.ImageHeight = Unit.Pixel(64);
        //}






        #endregion



    }
}
