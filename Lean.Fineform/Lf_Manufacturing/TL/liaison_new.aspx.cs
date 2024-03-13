using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.TL
{
    public partial class liaison_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限,空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTlNew";
            }
        }

        #endregion ViewPower

        public static string strMana, isCheck, strMailto, strWhouse, strPur, strEol, strEcnno, strInv, bitem, sitem, oitem, oitemset, nitem, nitemset, fileName, txtPbookdoc, txtPpbookdoc, txtPjpbookdoc, txtPdoc;
        public static long iFileSizeLimit = Convert.ToInt32(ConfigurationManager.AppSettings["FileSizeLimit"]);

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
            // MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.管理区分：全仕向,部管课,内部管理,技术课</div><div>2.附件大小规定：单一文件不能超5Mb,如果同时上付多个附件,附件总大小不能超过20Mb,否则请逐个上传</div><div>3.请添加中文翻译内容</div><div>4.担当者不能为空</div><div>4.修改管理区分时,设变单身数据将重新导入,之前各部门填写的资料全部作废</div><div style=\"margin-bottom:10px;color:red;\">5.关于制二课管理与否：物料状态分为4种：0--不管理，1--共通(部管制二都需填写)，2--部管，3--制二</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            Ec_issuedate.SelectedDate = DateTime.Now;
            Ec_enterdate.SelectedDate = DateTime.Now;
            BindDDLtype();
            BindDDLModel();
            BindDDLModelist();
            BindDDLRegion();
            BindDDLUserlist();
        }

        private void BindDDLtype()
        {
            var q = from a in DB.Qm_DocNumbers
                    orderby a.Docnumber
                    where a.Doctype == "A"
                    select new
                    {
                        a.Docname,
                        a.Docnumber
                    };
            var qs = q.Select(E => new { E.Docname, E.Docnumber }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPbook.DataSource = qs;
            ddlPbook.DataTextField = "Docnumber";
            ddlPbook.DataValueField = "Docnumber";
            ddlPbook.DataBind();

            this.ddlPbook.SelectedValue = "DTS";
        }

        #endregion Page_Load

        #region Events

        private void BindDDLUserlist()//ERP设变技术担当
        {
            var q_user = from a in DB.Adm_Users
                         join b in DB.Adm_Depts on a.Dept.ID equals b.ID
                         where b.Name.CompareTo("技术课") == 0
                         select new
                         {
                             a.ChineseName
                         };

            // 绑定到下拉列表（启用模拟树功能）
            var qs = q_user.Select(E => new { E.ChineseName }).ToList().Distinct();
            Ec_leader.DataTextField = "ChineseName";
            Ec_leader.DataValueField = "ChineseName";
            Ec_leader.DataSource = qs;
            Ec_leader.DataBind();

            // 选中根节点
            this.Ec_leader.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindDDLModel()//ERP设变技术担当
        {
            var q_model = from a in DB.Pp_Manhours
                          orderby a.Promodel
                          select new
                          {
                              a.Promodel
                          };
            var q_all = from b in DB.Pp_Manhours
                        orderby b.Promodel
                        where b.Prodesc.CompareTo("ALL") == 0
                        select new
                        {
                            Promodel = b.Prodesc
                        };

            // 绑定到下拉列表（启用模拟树功能）
            var qm = q_model.Select(E => new { E.Promodel }).ToList().Distinct();
            var qa = q_all.Select(E => new { E.Promodel }).ToList().Distinct();

            var qs = qa.Union(qm);

            Ec_model.DataTextField = "Promodel";
            Ec_model.DataValueField = "Promodel";
            Ec_model.DataSource = qs;
            Ec_model.DataBind();

            // 选中根节点
            //this.Ec_model.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
        }

        private void BindDDLModelist()//ERP设变技术担当
        {
            var q_model = from a in DB.Pp_Manhours
                          orderby a.Promodel
                          select new
                          {
                              a.Promodel
                          };
            var q_all = from b in DB.Pp_Manhours
                        orderby b.Promodel
                        where b.Prodesc.CompareTo("ALL") == 0
                        select new
                        {
                            Promodel = b.Prodesc
                        };

            // 绑定到下拉列表（启用模拟树功能）
            var qm = q_model.Select(E => new { E.Promodel }).ToList().Distinct();
            var qa = q_all.Select(E => new { E.Promodel }).ToList().Distinct();

            var qs = qa.Union(qm);

            Ec_modellist.DataTextField = "Promodel";
            Ec_modellist.DataValueField = "Promodel";
            Ec_modellist.DataSource = qs;
            Ec_modellist.DataBind();

            // 选中根节点
            //this.Ec_modellist.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
        }

        private void BindDDLRegion()//ERP设变技术担当
        {
            var q_region = from a in DB.Pp_Manhours
                           orderby a.Prodesc
                           select new
                           {
                               a.Prodesc
                           };

            // 绑定到下拉列表（启用模拟树功能）
            var qs = q_region.Select(E => new { E.Prodesc }).ToList().Distinct();

            Ec_region.DataTextField = "Prodesc";
            Ec_region.DataValueField = "Prodesc";
            Ec_region.DataSource = qs;
            Ec_region.DataBind();

            // 选中根节点
            //this.Ec_region.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
        }

        protected void ddlPbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ec_letterno.Text = "";
        }

        //字段赋值,保存
        private void SaveItem()//新增设变单头
        {
            //判断重复
            string inputNo = Ec_letterno.Text.Trim();

            if (!string.IsNullOrEmpty(inputNo))
            {
                Pp_Liaison tl = DB.Pp_Liaisons.Where(u => u.Ec_letterno == inputNo && u.isDeleted == 0).FirstOrDefault();

                if (tl == null)
                {
                    //查询设变从表并循环添加

                    Pp_Liaison item = new Pp_Liaison();
                    item.Ec_issuedate = this.Ec_issuedate.SelectedDate.Value.ToString("yyyyMMdd");
                    item.Ec_leader = Ec_leader.SelectedItem.Text;//DTA担当
                    if (Ec_modellist.SelectedItem != null)
                    {
                        List<string> modeltexts = new List<string>();
                        List<string> modelvalues = new List<string>();
                        foreach (FineUIPro.ListItem models in Ec_modellist.SelectedItemArray)
                        {
                            modeltexts.Add(models.Text);
                            modelvalues.Add(models.Value);
                        }

                        item.Ec_modellist = String.Format(
                            String.Join(",", modeltexts.ToArray()));
                    }
                    else
                    {
                        item.Ec_modellist = "";
                    }
                    if (Ec_region.SelectedItem != null)
                    {
                        List<string> regiontexts = new List<string>();
                        List<string> regionvalues = new List<string>();
                        foreach (FineUIPro.ListItem regions in Ec_region.SelectedItemArray)
                        {
                            regiontexts.Add(regions.Text);
                            regionvalues.Add(regions.Value);
                        }

                        item.Ec_region = String.Format(
                            String.Join(",", regiontexts.ToArray()));
                    }
                    else
                    {
                        item.Ec_region = "";
                    }
                    item.Ec_model = Ec_model.SelectedItem.Text;//机种
                    item.Ec_enterdate = this.Ec_enterdate.SelectedDate.Value.ToString("yyyyMMdd");
                    //技联NO Ec_letterno
                    if (!string.IsNullOrEmpty(Ec_letterno.Text.ToUpper()))
                    {
                        if (ddlPbook.SelectedItem.Text == "TCJ-")
                        {
                            item.Ec_letterno = Ec_letterno.Text.ToUpper();
                        }
                        else

                        {
                            item.Ec_letterno = ddlPbook.SelectedItem.Text + Ec_letterno.Text.ToUpper();
                        }
                        Pbookdoc();
                        if (!string.IsNullOrEmpty(txtPbookdoc))
                        {
                            item.Ec_letterdoc = txtPbookdoc;
                        }
                        else
                        {
                            item.Ec_letterdoc = "";
                        }
                    }
                    else
                    {
                        item.Ec_letterno = "";
                    }

                    if (!string.IsNullOrEmpty(Ec_eppletterno.Text.ToUpper()))
                    {
                        item.Ec_eppletterno = "P-" + Ec_eppletterno.Text.ToUpper();
                        Ppbookdoc();
                        if (!string.IsNullOrEmpty(txtPpbookdoc))
                        {
                            item.Ec_eppletterdoc = txtPpbookdoc;
                        }
                        else
                        {
                            item.Ec_eppletterdoc = "";
                        }
                    }
                    else
                    {
                        item.Ec_eppletterno = "";
                    }
                    if (!string.IsNullOrEmpty(Ec_teppletterno.Text.ToUpper()))
                    {
                        item.Ec_teppletterno = "P-" + Ec_teppletterno.Text.ToUpper();
                        Pjpbookdoc();
                        if (!string.IsNullOrEmpty(txtPjpbookdoc))
                        {
                            item.Ec_teppletterdoc = txtPjpbookdoc;
                        }
                        else
                        {
                            item.Ec_teppletterdoc = "";
                        }
                    }
                    else
                    {
                        item.Ec_teppletterno = "";
                    }

                    item.isDeleted = 0;

                    item.GUID = Guid.NewGuid();
                    item.CreateDate = DateTime.Now;
                    item.Creator = GetIdentityName();
                    DB.Pp_Liaisons.Add(item);
                    DB.SaveChanges();
                }
                else
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_TL_Repeat, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_TL_Empty, MessageBoxIcon.Error);
                return;
            }
        }

        private void Pbookdoc()
        {
            if (Ec_letterdoc.HasFile)
            {
                string fileName = Ec_letterdoc.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    Alert.ShowInTop("无效的文件类型！");
                    return;
                }

                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //判断最后一个.的位置
                int i = fileName.LastIndexOf(".");
                //字串长度
                int f = fileName.Length;
                //截取长度
                int iff = fileName.Length - fileName.LastIndexOf(".");
                fileName = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));

                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_DTS-" + Ec_letterno.Text + fileName;

                Ec_letterdoc.SaveAs(Server.MapPath("../../Lf_Documents/letter/" + fileName));

                txtPbookdoc = "../../Lf_Documents/letter/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        private void Ppbookdoc()
        {
            if (Ec_eppletterdoc.HasFile)
            {
                string fileName = Ec_eppletterdoc.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    Alert.ShowInTop("无效的文件类型！");
                    return;
                }

                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //判断最后一个.的位置
                int i = fileName.LastIndexOf(".");
                //字串长度
                int f = fileName.Length;
                //截取长度
                int iff = fileName.Length - fileName.LastIndexOf(".");
                fileName = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));

                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_P-" + Ec_eppletterno.Text + fileName;
                Ec_eppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/letter/" + fileName));

                txtPpbookdoc = "../../Lf_Documents/letter/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        private void Pjpbookdoc()
        {
            if (Ec_teppletterdoc.HasFile)
            {
                string fileName = Ec_teppletterdoc.ShortFileName;

                if (!ValidateFileType(fileName))
                {
                    Alert.ShowInTop("无效的文件类型！");
                    return;
                }

                fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\\", "_").Replace("/", "_");
                //判断最后一个.的位置
                int i = fileName.LastIndexOf(".");
                //字串长度
                int f = fileName.Length;
                //截取长度
                int iff = fileName.Length - fileName.LastIndexOf(".");
                fileName = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf("."));

                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_P-" + Ec_teppletterno.Text + fileName;
                Ec_teppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/letter/" + fileName));

                txtPjpbookdoc = "../../Lf_Documents/letter/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            try
            {
                if (!string.IsNullOrEmpty(Ec_letterdoc.FileName))
                {
                    //技术联络文件大小
                    if (Ec_letterdoc.PostedFile.ContentLength > iFileSizeLimit)
                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_UploadFile, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(Ec_eppletterdoc.FileName))
                {
                    //P番（DTA)大小
                    if (Ec_eppletterdoc.PostedFile.ContentLength > iFileSizeLimit)
                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_UploadFile, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (!string.IsNullOrEmpty(Ec_teppletterdoc.FileName))
                {
                    //P番(TCJ)大小
                    if (Ec_teppletterdoc.PostedFile.ContentLength > iFileSizeLimit)
                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_UploadFile, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (Ec_leader.SelectedItem.Text.Contains("选择"))
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect, MessageBoxIcon.Information);
                    return;
                }
                if (Ec_model.SelectedItem.Text.Contains("选择"))
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect, MessageBoxIcon.Information);
                    return;
                }
                if (Ec_modellist.SelectedItem.Text.Contains("选择"))
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect, MessageBoxIcon.Information);
                    return;
                }
                if (Ec_region.SelectedItem.Text.Contains("选择"))
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect, MessageBoxIcon.Information);
                    return;
                }
                if (!string.IsNullOrEmpty(Ec_letterno.Text.ToUpper()))
                {
                    if (this.ddlPbook.SelectedItem.Text == "DTS-")
                    {
                        Regex reg = new Regex(@" ^\d{3,4}$", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                        if (!reg.IsMatch(Ec_letterno.Text.Trim()))
                        {
                            Alert.ShowInTop("DTA技联请输入3或4位数字!");
                            Ec_letterno.Focus();
                            return;
                        }
                    }

                    if (this.ddlPbook.SelectedItem.Text == "X-")
                    {
                        Regex reg = new Regex(@" ^\d{3,4}$", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                        if (!reg.IsMatch(Ec_letterno.Text.Trim()))
                        {
                            Alert.ShowInTop("TCJ技联请输入3或4位数字!");
                            Ec_letterno.Focus();
                            return;
                        }
                    }

                    if (this.ddlPbook.SelectedItem.Text == "TCJ-")
                    {
                        Regex reg = new Regex(@"^\d{4}-\d{3,4}$", RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
                        if (!reg.IsMatch(Ec_letterno.Text.Trim()))
                        {
                            Alert.ShowInTop("其它技联请输入1250-888或1250-8888的格式!");
                            Ec_letterno.Focus();
                            return;
                        }
                    }
                }
                //if (Ec_letterno.Text.ToUpper() != "")
                //{
                //    if (!String.IsNullOrEmpty("DTS-" + Ec_letterno.Text.ToString().ToUpper().Trim()))
                //    {
                //        //验证字符长度
                //        if (!ValidatorTools.IsStringLength("DTS-" + Ec_letterno.Text.ToString().ToUpper(), 7, 8))
                //        {
                //            Ec_letterno.Text = "";
                //            Ec_letterno.Focus();
                //            Alert.ShowInTop("技术联络书的格式为DTS-数字,请重新输入");
                //            return;
                //        }

                //        //验证输入字串
                //        if (!ValidatorTools.isDTS("DTS-" + Ec_letterno.Text.ToString().ToUpper()))
                //        {
                //            Ec_letterno.Text = "";
                //            Ec_letterno.Focus();
                //            Alert.ShowInTop("技术联络书的格式为DTS-数字,请重新输入");
                //            return;
                //        }
                //    }
                //}

                //if (Ec_eppletterno.Text.ToString() != "")
                //{
                //    if (!String.IsNullOrEmpty("P-" + Ec_eppletterno.Text.ToString().ToUpper().Trim()))
                //    {
                //        //验证字符长度
                //        if (!ValidatorTools.IsStringLength("P-" + Ec_eppletterno.Text.ToString().ToUpper(), 5, 6))
                //        {
                //            Ec_eppletterno.Text = "";
                //            Ec_eppletterno.Focus();
                //            Alert.ShowInTop("P番联络书的格式为P-数字,请重新输入");
                //            return;
                //        }

                //        //验证输入字串
                //        if (!ValidatorTools.isPP("P-" + Ec_eppletterno.Text.ToString().ToUpper()))
                //        {
                //            Ec_eppletterno.Text = "";
                //            Ec_eppletterno.Focus();
                //            Alert.ShowInTop("P番联络书的格式为P-数字,请重新输入");
                //            return;
                //        }
                //    }
                //}

                SaveItem();
                InsNetOperateNotes();
                txtPdoc = "";
                txtPjpbookdoc = "";
                txtPpbookdoc = "";
                txtPbookdoc = "";

                //UpdateEcSubs();

                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
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
                foreach (var item in errors)
                    msg += item.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
            //sw.Stop();
            //Alert.ShowInTop(sw.Elapsed.ToString(), "执行信息", MessageBoxIcon.Information);
        }

        #endregion Events

        private void Mailto()
        {
            //var q_user = from a in DB.Adm_Users
            //             join b in DB.Adm_Depts on a.Dept.ID equals b.ID
            //             where b.Name.CompareTo("采购课") == 0
            //             where a.Email != "123@teac.com.cn"
            //             select new
            //             {
            //                 a.Email,
            //             };
            //if (q_user.Any())
            //{
            //    var qs = q_user.ToList();
            //    for (int i = 0; i < q_user.Count(); i++)
            //    {
            //        strMailto += qs[i].Email.ToString() + ",";
            //    }
            //}
            //strMailto = strMailto.Remove(strMailto.LastIndexOf(","));
            //if (Ec_distinction.SelectedItem.Text == "全部")
            //{
            //    strMana = "全部";
            //}
            //if (Ec_distinction.SelectedItem.Text == "部管课")
            //{
            //    strMana = "部管课";
            //}
            //if (Ec_distinction.SelectedItem.Text == "内部管理")
            //{
            //    strMana = "内部";
            //}
            //if (Ec_distinction.SelectedItem.Text != "技术课")
            //{
            //    strMana = "技术课";
            //}

            //string mailTitle = "设变发行：" + Ec_no.Text + "担当：" + Ec_leader.SelectedItem.Text + "管理区分：" + strMana;
            //string mailBody = "Dear All,\r\n" + "\r\n" + "此设变技术部门已处理。\r\n" + "请贵部门担当者及时处理为盼。\r\n" + "\r\n" + "よろしくお願いいたします。\r\n" + "\r\n" + "\r\n" + "「" + GetIdentityName() + "\r\n" + DateTime.Now.ToString() + "」\r\n" + "このメッセージはWebSiteから自動で送信されている。\r\n\n";  //发送邮件的正文
            //MailHelper.SendEmail(strMailto, mailTitle, mailBody);
            strMailto = "";
        }

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();

            //新增日志
            string Newtext = Ec_issuedate.Text + "," + Ec_letterdoc.Text + "," + Ec_leader.SelectedItem.Text + "," + txtPbookdoc + "," + txtPpbookdoc + "," + "," + txtPjpbookdoc;
            string OperateType = "新增";//操作标记
            string OperateNotes = "New技术* " + Newtext + " New*技术 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变新增", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}