using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.TL
{
    public partial class liaison_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTlEdit";
            }
        }

        #endregion ViewPower

        public static long iFileSizeLimit = Convert.ToInt32(ConfigurationManager.AppSettings["FileSizeLimit"]);

        #region Page_Load

        public static string strWhouse, strPur, IsCheck, strEol, fullname, shortname, txtPbookdoc, txtPpbookdoc, txtPjpbookdoc, txtPdoc, oldEc_distinction, oldIsSopUpdate, oldisComfirm;
        public static string strID, strEc_no, strInv, bitem, sitem, oitem, oitemset, nitem, nitemset;

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

            //strEc_newitem = strgroup[2].ToString().Trim();
            //DDL赋值必须现BindData之前
            BindDdltype();
            BindDdlModel();
            BindDdlModelist();
            BindDdlRegion();
            BindDdlUserlist();
            BindData();

            //guid = Guid.NewGuid().ToString();
            //InitNewItem();
            //InitOldItem();
            //InitModel();
        }

        protected void ddlPbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Ec_letterno.Text = "";
        }

        private void BindDdltype()
        {
            var q = from a in DB.Qm_DocNumbers
                    where a.Doctype == "A"
                    select new
                    {
                        a.Docname,
                        a.Docnumber
                    };
            var qs = q.Select(E => new { E.Docnumber, E.Docname }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPbook.DataSource = qs;
            ddlPbook.DataTextField = "Docnumber";
            ddlPbook.DataValueField = "Docnumber";
            ddlPbook.DataBind();

            this.ddlPbook.SelectedIndex = 0;
        }

        private void BindDdlUserlist()//ERP设变技术担当
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

        private void BindDdlModel()//ERP设变技术担当
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
            this.Ec_model.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
        }

        private void BindDdlModelist()//ERP设变技术担当
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

            var qs = qm.Union(qa);

            Ec_modellist.DataTextField = "Promodel";
            Ec_modellist.DataValueField = "Promodel";
            Ec_modellist.DataSource = qs;
            Ec_modellist.DataBind();

            // 选中根节点
            //this.Ec_modellist.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            //this.Ec_model.Items.Insert(1, new FineUIPro.ListItem("ALL", ""));
        }

        private void BindDdlRegion()//ERP设变技术担当
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

        //#region InitOldItem

        //private void InitOldItem()
        //{
        //    // 打开编辑角色的窗口
        //    string selectJobTitleURL = String.Format("../plutoProinfo/item_select.aspx?ids=<script>{0}</script>", hfSelectedDhbn.GetValueReference());
        //    Ec_olditem.OnClientTriggerClick = Window1.GetSaveStateReference(Ec_olditem.ClientID, hfSelectedDhbn.ClientID)
        //            + Window1.GetShowReference(selectJobTitleURL, "物料");

        //    //string openUrl = String.Format("../plutoProinfo/Ohbn_select.aspx?ids=<script>{0}</script>", hfSelectedDhbn.GetValueReference());

        //    //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(tbSelectedDhbn.ClientID)
        //    //        + Window1.GetShowReference(openUrl));

        //}
        //#endregion
        //#region InitNewItem

        //private void InitNewItem()
        //{
        //    // 打开编辑角色的窗口
        //    string selectJobTitleURL = String.Format("../plutoProinfo/item_select.aspx?ids=<script>{0}</script>", hfSelectedWhbn.GetValueReference());
        //    Ec_newitem.OnClientTriggerClick = Window1.GetSaveStateReference(Ec_newitem.ClientID)
        //            + Window1.GetShowReference(selectJobTitleURL, "物料");

        //    //string openUrl = String.Format("../plutoProinfo/Ohbn_select.aspx?ids=<script>{0}</script>", hfSelectedDhbn.GetValueReference());

        //    //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(tbSelectedDhbn.ClientID)
        //    //        + Window1.GetShowReference(openUrl));

        //}
        //#endregion
        //#region InitModel

        //private void InitModel()
        //{
        //    // 打开编辑角色的窗口
        //    string selectJobTitleURL = String.Format("../plutoProinfo/model_select.aspx?ids=<script>{0}</script>", hfSelectedModel.GetValueReference());
        //    Ec_detail.OnClientTriggerClick = Window1.GetSaveStateReference(Ec_detail.ClientID, hfSelectedModel.ClientID)
        //            + Window1.GetShowReference(selectJobTitleURL, "物料");

        //    //string openUrl = String.Format("../plutoProinfo/Ohbn_select.aspx?ids=<script>{0}</script>", hfSelectedDhbn.GetValueReference());

        //    //PageContext.RegisterStartupScript(Window1.GetSaveStateReference(tbSelectedDhbn.ClientID)
        //    //        + Window1.GetShowReference(openUrl));

        //}
        //#endregion

        #endregion Page_Load

        #region Events

        private void BindData()
        {
            try
            {
                //string mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason,[D_SAP_ZPABD_Z004] AS Flag  FROM [dbo].[ProSapEngChanges] " +
                //                "LEFT JOIN  [dbo].[Ec_s] ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 " +
                //                " WHERE REPLACE(Ec_no,' ','') IS NULL  AND D_SAP_ZPABD_Z001='" + ItemMaster + "' " +
                //                " GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013,[D_SAP_ZPABD_Z004] ORDER BY D_SAP_ZPABD_Z005 DESC;";
                Guid id = Guid.Parse(GetQueryValue("GUID"));
                var q = from a in DB.Pp_Liaisons
                            //join b in DB.Pp_SapEcns on a.Ec_no equals b.D_SAP_ZPABD_Z001
                        where a.GUID == id
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_model,
                            a.Ec_modellist,
                            a.Ec_region,
                            a.Ec_enterdate,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                        };
                var qs = q.Select(a => new
                {
                    a.Ec_issuedate,
                    a.Ec_leader,
                    a.Ec_model,
                    a.Ec_modellist,
                    a.Ec_region,
                    a.Ec_enterdate,
                    Ec_letterno = a.Ec_letterno == null ? "" : a.Ec_letterno,
                    Ec_letterdoc = a.Ec_letterdoc == null ? "" : a.Ec_letterdoc,
                    Ec_eppletterno = a.Ec_eppletterno == null ? "" : a.Ec_eppletterno,
                    Ec_eppletterdoc = a.Ec_eppletterdoc == null ? "" : a.Ec_eppletterdoc,
                    Ec_teppletterno = a.Ec_teppletterno == null ? "" : a.Ec_teppletterno,
                    Ec_teppletterdoc = a.Ec_teppletterdoc == null ? "" : a.Ec_teppletterdoc,
                }).ToList();
                if (qs.Any())
                {
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期

                    Ec_leader.SelectedValue = qs[0].Ec_leader;//担当
                    Ec_model.SelectedValue = qs[0].Ec_model;//机种

                    //多选

                    String[] mArray = qs[0].Ec_modellist.Split(',');

                    Ec_modellist.SelectedValueArray = mArray;//明细

                    //Ec_region.SelectedValueArray = qs[0].Ec_region;//机种
                    String[] rArray = qs[0].Ec_region.Split(',');
                    Ec_region.SelectedValueArray = rArray;//仕向

                    Ec_enterdate.Text = qs[0].Ec_enterdate;//实施日期

                    //ddlPbook.SelectedValue = qs[0].Ec_distinction.ToString();//管理区分
                    //Ec_distinction.SelectedValue = qs[0].Ec_distinction.ToString();//管理区分
                    int book = qs[0].Ec_letterno.Length;
                    if (qs[0].Ec_letterno.Length != 0)
                    {
                        int len = qs[0].Ec_letterno.ToString().Length;
                        int i = qs[0].Ec_letterno.ToString().IndexOf("-") + 1;

                        string dds = qs[0].Ec_letterno.ToString().Substring(0, i - 1);
                        if (qs[0].Ec_letterno.ToString().Substring(0, i - 1) == "DTS")
                        {
                            ddlPbook.SelectedValue = "DTS-";
                        }
                        if (qs[0].Ec_letterno.ToString().Substring(0, i - 1) == "X")
                        {
                            ddlPbook.SelectedValue = "X-";
                        }
                        if (qs[0].Ec_letterno.ToString().Substring(0, i - 1) == "TCJ")
                        {
                            ddlPbook.SelectedValue = "TCJ-";
                        }
                        Ec_letterno.Text = qs[0].Ec_letterno.ToString().Substring(i, len - i);
                    }
                    else
                    {
                        Ec_letterno.Text = qs[0].Ec_letterno.ToString();
                    }
                    //string ss=qs[0].Ec_letterdoc;
                    if (!string.IsNullOrEmpty(qs[0].Ec_letterdoc))
                    {
                        fullname = qs[0].Ec_letterdoc.ToString();
                        // string npath = current.Notice_Attachments.Substring(0, 43);
                        shortname = qs[0].Ec_letterdoc.ToString().Substring(41, qs[0].Ec_letterdoc.ToString().Length - 41);
                        //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                        //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                        this.oldEc_letterdoc.Text = shortname;
                    }
                    //Ec_eppletterno.Text = "";
                    if (qs[0].Ec_eppletterno.Length != 0)
                    {
                        Ec_eppletterno.Text = qs[0].Ec_eppletterno.ToString().Substring(2, qs[0].Ec_eppletterno.ToString().Length - 2);
                    }
                    else
                    {
                        Ec_eppletterno.Text = qs[0].Ec_eppletterno.ToString();
                    }
                    //Ec_eppletterdoc
                    if (!string.IsNullOrEmpty(qs[0].Ec_eppletterdoc))
                    {
                        fullname = qs[0].Ec_eppletterdoc.ToString();
                        // string npath = current.Notice_Attachments.Substring(0, 43);
                        shortname = qs[0].Ec_eppletterdoc.ToString().Substring(46, qs[0].Ec_eppletterdoc.ToString().Length - 46);
                        //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                        //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                        this.oldEc_eppletterdoc.Text = shortname;
                    }
                    //Ec_documents

                    //TCJ P番Ec_teppletterno
                    if (qs[0].Ec_teppletterno.Length != 0)
                    {
                        Ec_teppletterno.Text = qs[0].Ec_teppletterno.ToString().Substring(2, qs[0].Ec_teppletterno.ToString().Length - 2);
                    }
                    else
                    {
                        Ec_teppletterno.Text = qs[0].Ec_teppletterno.ToString();
                    }
                    //Ec_teppletterdoc
                    if (!string.IsNullOrEmpty(qs[0].Ec_teppletterdoc))
                    {
                        fullname = qs[0].Ec_teppletterdoc.ToString();
                        // string npath = current.Notice_Attachments.Substring(0, 43);
                        shortname = qs[0].Ec_teppletterdoc.ToString().Substring(46, qs[0].Ec_teppletterdoc.ToString().Length - 46);
                        //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                        //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                        this.oldEc_teppletterdoc.Text = shortname;
                    }
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);
            }
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            //int id = GetQueryIntValue("id");
            //int id = int.Parse(strID);
            //Convert.ToInt32(strID)
            Pp_Liaison item = DB.Pp_Liaisons

                .Where(u => u.GUID == id).FirstOrDefault();

            item.Ec_issuedate = this.Ec_issuedate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Ec_leader = Ec_leader.SelectedItem.Text;
            //item.Ec_relyno = Ec_relyno.Text;
            //item.Ec_model = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();
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
            //item.Ec_tcjno = Ec_tcjno.Text;
            //item.Ec_modellist = Ec_modellist.SelectedItem.Text;//DTA担当
            //item.Ec_region = Ec_region.SelectedItem.Text;//DTA担当
            item.Ec_model = Ec_model.SelectedItem.Text;//设变内容

            item.Ec_enterdate = this.Ec_enterdate.SelectedDate.Value.ToString("yyyyMMdd"); //金额
                                                                                           //设变Ec_documents

            //技联Ec_letterno

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
                //item.Ec_letterno = ddlPbook.SelectedItem.Text + Ec_letterno.Text.ToUpper();
            }
            else
            {
                item.Ec_letterno = "";
            }
            Pbookdoc();
            if (!String.IsNullOrEmpty(txtPbookdoc))
            {
                //技联Ec_letterdoc  Ec_letterno
                item.Ec_letterdoc = txtPbookdoc;
            }
            else
            {
                //item.Ec_letterdoc = "";

                if (oldEc_letterdoc.Text == "-")
                {
                    item.Ec_letterdoc = "";
                }
                //else
                //{
                //    item.Ec_letterdoc = "../../Lf_Documents/ecdocs/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldEc_letterdoc.Text;
                //}
            }
            //P番Ec_eppletterno
            if (!string.IsNullOrEmpty(Ec_eppletterno.Text.ToUpper()))
            {
                item.Ec_eppletterno = "P-" + Ec_eppletterno.Text.ToUpper();
            }
            else
            {
                item.Ec_eppletterno = "";
            }
            //P番Ec_eppletterdoc
            Ppbookdoc();
            if (!String.IsNullOrEmpty(txtPpbookdoc))
            {
                item.Ec_eppletterdoc = txtPpbookdoc;
            }
            else
            {
                //item.Ec_eppletterdoc = "";
                if (oldEc_eppletterdoc.Text == "-")
                {
                    item.Ec_eppletterdoc = "";
                }
                //else
                //{
                //    item.Ec_eppletterdoc = "../../Lf_Documents/ecdocs/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldEc_eppletterdoc.Text;
                //}
            }

            //P番Ec_eppletterno(TCJ)
            if (!string.IsNullOrEmpty(Ec_teppletterno.Text.ToUpper()))
            {
                item.Ec_teppletterno = "P-" + Ec_teppletterno.Text.ToUpper();
            }
            else
            {
                item.Ec_teppletterno = "";
            }
            //P番Ec_eppletterdoc(TCJ)
            Pjpbookdoc();
            if (!String.IsNullOrEmpty(txtPjpbookdoc))
            {
                item.Ec_teppletterdoc = txtPjpbookdoc;
            }
            else
            {
                //item.Ec_teppletterdoc = "";
                if (oldEc_teppletterdoc.Text == "-")
                {
                    item.Ec_teppletterdoc = "";
                }
                //else
                //{
                //    item.Ec_teppletterdoc = "../../Lf_Documents/ecdocs/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldEc_teppletterdoc.Text;
                //}
            }

            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Pp_Ec_Subs.Add(item);
            DB.SaveChanges();
        }

        //技联
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
                //Alert.ShowInTop(Server.MapPath("~"));

                string ph = Server.MapPath("~");

                Ec_letterdoc.SaveAs(Server.MapPath("../../Lf_Documents/letter/" + fileName));

                txtPbookdoc = "../../Lf_Documents/letter/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        //P番
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

        //P番
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
                if (Ec_letterno.Text.ToUpper() != "")
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
                //if (Ec_letterno.Text.ToUpper() != "")
                //{
                //    if (!String.IsNullOrEmpty("DTS-" + Ec_letterno.Text.ToString().ToUpper().Trim()))
                //    {
                //        //验证字符长度
                //        if (!ValidatorTools.IsStringLength("DTS-" + Ec_letterno.Text.ToString().ToUpper(), 7, 8))
                //        {
                //            Ec_letterno.Text = "";
                //            Ec_letterno.Focus();
                //            Alert.ShowInTop("技术联络书的格式为DTS-数字，请重新输入");
                //            return;
                //        }

                //        //验证输入字串
                //        if (!ValidatorTools.isDTS("DTS-" + Ec_letterno.Text.ToString().ToUpper()))
                //        {
                //            Ec_letterno.Text = "";
                //            Ec_letterno.Focus();
                //            Alert.ShowInTop("技术联络书的格式为DTS-数字，请重新输入");
                //            return;
                //        }
                //    }
                //}

                //if (Ec_eppletterno.Text.ToString()!="")
                //{
                //    if (!String.IsNullOrEmpty("P-" + Ec_eppletterno.Text.ToString().ToUpper().Trim()))
                //    {
                //        //验证字符长度
                //        if (!ValidatorTools.IsStringLength("P-" + Ec_eppletterno.Text.ToString().ToUpper(), 5, 6))
                //        {
                //            Ec_eppletterno.Text = "";
                //            Ec_eppletterno.Focus();
                //            Alert.ShowInTop("P番联络书的格式为P-数字，请重新输入");
                //            return;
                //        }

                //        //验证输入字串
                //        if (!ValidatorTools.isPP("P-" + Ec_eppletterno.Text.ToString().ToUpper()))
                //        {
                //            Ec_eppletterno.Text = "";
                //            Ec_eppletterno.Focus();
                //            Alert.ShowInTop("P番联络书的格式为P-数字，请重新输入");
                //            return;
                //        }
                //    }
                //}

                SaveItem();
                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                //sw.Stop();
                //Alert.ShowInTop(sw.Elapsed.ToString(), "执行信息", MessageBoxIcon.Information);

                InsNetOperateNotes();

                txtPdoc = "";
                txtPjpbookdoc = "";
                txtPpbookdoc = "";
                txtPbookdoc = "";
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
        }

        #endregion Events

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
            //修改日志
            string Newtext = Ec_issuedate.Text + "," + Ec_letterno.Text + "," + Ec_leader.Text + "," + txtPbookdoc + "," + txtPpbookdoc + "," + "," + txtPjpbookdoc;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit技术* " + Newtext + " *Edit技术 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "技联信息修改", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}