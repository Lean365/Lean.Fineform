﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

//using EntityFramework.Extensions;
namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_eng_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcENGEdit";
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
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.管理区分：全仕向，部管课，内部管理，技术课</div><div>2.附件大小规定：单一文件不能超5Mb，如果同时上付多个附件，附件总大小不能超过20Mb，否则请逐个上传</div><div>3.请添加中文翻译内容</div><div>4.担当者不能为空</div><div>4.修改管理区分时，设变单身数据将重新导入，之前各部门填写的资料全部作废</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');
            //ID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem
            strID = strgroup[0].ToString().Trim();
            strEc_no = strgroup[1].ToString().Trim();
            //strEc_newitem = strgroup[2].ToString().Trim();
            //DDL赋值必须现BindData之前
            BindDdltype();
            BindDdluser();
            BindDdladmindist();
            BindData();
            BindDdlItemlist();
            BindGrid();
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

                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_SapEcns on a.Ec_no equals b.D_SAP_ZPABD_Z001
                        where a.Ec_no == strEc_no
                        select new
                        {
                            a.Ec_issuedate,
                            a.Ec_no,
                            a.Ec_status,
                            a.Ec_details,
                            a.Ec_leader,
                            a.Ec_title,
                            a.Ec_lossamount,
                            a.Ec_distinction,
                            a.Ec_letterno,
                            a.Ec_letterdoc,
                            a.Ec_eppletterno,
                            a.Ec_eppletterdoc,
                            a.Ec_documents,
                            a.Ec_teppletterno,
                            a.Ec_teppletterdoc,
                            b.D_SAP_ZPABD_Z003,
                            b.D_SAP_ZPABD_Z012,
                            b.D_SAP_ZPABD_Z013,
                            b.D_SAP_ZPABD_Z002,
                            b.D_SAP_ZPABD_Z027,
                            b.D_SAP_ZPABD_Z025,
                            //a.IsSopUpdate,
                            //b.IsManage,
                        };
                var qs = q.Select(a => new
                {
                    a.Ec_issuedate,
                    a.Ec_no,
                    a.Ec_status,
                    a.Ec_details,
                    a.Ec_leader,
                    a.Ec_title,
                    a.Ec_lossamount,
                    a.Ec_distinction,
                    Ec_letterno = a.Ec_letterno == null ? "" : a.Ec_letterno,
                    Ec_letterdoc = a.Ec_letterdoc == null ? "" : a.Ec_letterdoc,
                    Ec_eppletterno = a.Ec_eppletterno == null ? "" : a.Ec_eppletterno,
                    Ec_eppletterdoc = a.Ec_eppletterdoc == null ? "" : a.Ec_eppletterdoc,
                    Ec_documents = a.Ec_documents == null ? "" : a.Ec_documents,
                    Ec_teppletterno = a.Ec_teppletterno == null ? "" : a.Ec_teppletterno,
                    Ec_teppletterdoc = a.Ec_teppletterdoc == null ? "" : a.Ec_teppletterdoc,
                    a.D_SAP_ZPABD_Z003,
                    a.D_SAP_ZPABD_Z012,
                    a.D_SAP_ZPABD_Z013,
                    a.D_SAP_ZPABD_Z002,
                    a.D_SAP_ZPABD_Z027,
                    a.D_SAP_ZPABD_Z025,
                    //a.IsSopUpdate,
                    //b.IsManage,
                }).ToList();
                if (qs.Any())
                {
                    Ec_title.Text = qs[0].Ec_title;
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期
                    Ec_no.Text = qs[0].Ec_no;//设变号码
                    Ec_title.Text = qs[0].D_SAP_ZPABD_Z003;//设变标题
                    mReason.Text = qs[0].D_SAP_ZPABD_Z012;//设变原因
                    sReason.Text = qs[0].D_SAP_ZPABD_Z013;//设变原因
                    //if (qs[0].IsSopUpdate.ToString() == "1")
                    //{
                    //    IsSopUpdate.SelectedValue = "1";
                    //}
                    //else

                    //{
                    //    IsSopUpdate.SelectedValue = "0";
                    //}
                    //if (qs[0].IsManage.ToString() == "1")
                    //{
                    //    IsManage.SelectedValue = "1";
                    //}
                    //else

                    //{
                    //    IsManage.SelectedValue = "0";
                    //}
                    Ec_model.Text = qs[0].D_SAP_ZPABD_Z002;//设变机种
                    if (qs[0].D_SAP_ZPABD_Z027.Length > 1000)
                    {
                        Ec_detailstent.Text = qs[0].D_SAP_ZPABD_Z027.Substring(0, 900);
                    }
                    else
                    {
                        Ec_detailstent.Text = qs[0].D_SAP_ZPABD_Z027;
                    }
                    Ec_details.Text = qs[0].D_SAP_ZPABD_Z027;//设变内容
                                                             //Ec_detailstent.Text = StripHTML(qs[i].D_SAP_ZPABD_Z027);//DTA设变内容
                    Ec_lossamount.Text = qs[0].D_SAP_ZPABD_Z025;//仕损金额
                    Ec_status.Text = qs[0].Ec_status;//状态
                    Ec_leader.SelectedValue = qs[0].Ec_leader;//担当
                    Ec_distinction.SelectedValue = qs[0].Ec_distinction.ToString();//管理区分
                    oldEc_distinction = qs[0].Ec_distinction.ToString();
                    //oldisComfirm = qs[0].IsManage.ToString();
                    //oldIsSopUpdate = qs[0].IsSopUpdate.ToString();
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
                        //获取文件名，总长减去固定文本长度
                        shortname = qs[0].Ec_eppletterdoc.ToString().Substring(41, qs[0].Ec_eppletterdoc.ToString().Length - 41);
                        //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                        //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                        this.oldEc_eppletterdoc.Text = shortname;
                    }
                    //Ec_documents
                    if (!string.IsNullOrEmpty(qs[0].Ec_documents))
                    {
                        fullname = qs[0].Ec_documents.ToString();
                        // string npath = current.Notice_Attachments.Substring(0, 43);
                        shortname = qs[0].Ec_documents.ToString().Substring(41, qs[0].Ec_documents.ToString().Length - 41);
                        //this.txtUploadFile.Text = Base64DEncrypt.EncodeBase64(npath);
                        //this.txtUploadFile.Text = this.txtUploadFile.Text + nfile;
                        this.oldEc_documents.Text = shortname;
                    }
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
                        shortname = qs[0].Ec_teppletterdoc.ToString().Substring(41, qs[0].Ec_teppletterdoc.ToString().Length - 41);
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

        private void BindDdluser()//ERP设变技术担当
        {
            var q_user = from a in DB.Adm_Users
                         join b in DB.Adm_Depts on a.Dept.ID equals b.ID
                         where b.Name.CompareTo("技术课") == 0
                         select new
                         {
                             a.ChineseName
                         };

            var qs = q_user.Select(E => new { E.ChineseName, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            Ec_leader.DataSource = qs;
            Ec_leader.DataTextField = "ChineseName";
            Ec_leader.DataValueField = "ChineseName";
            Ec_leader.DataBind();
            this.Ec_leader.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string strEc, strModel, strItem;
            object[] keys = Grid1.DataKeys[e.RowIndex];

            if (keys[0] == null)
            {
                strEc = "";
            }
            else
            {
                strEc = keys[0].ToString();
            }

            if (keys[1] == null)
            {
                strModel = "";
            }
            else
            {
                strModel = keys[1].ToString();
            }
            if (keys[2] == null)
            {
                strItem = "";
            }
            else
            {
                strItem = keys[2].ToString();
            }
            //if (keys[3] == null)
            //{
            //    tracestr = tracestr + ",";
            //}
            //else
            //{
            //    tracestr = tracestr + keys[3].ToString() + ",";
            //}
            //if (keys[4] == null)
            //{
            //    tracestr = tracestr + ",";
            //}
            //else
            //{
            //    tracestr = tracestr + keys[4].ToString() + ",";
            //}
            //if (keys[5] == null)
            //{
            //    tracestr = tracestr + ",";
            //}
            //else
            //{
            //    tracestr = tracestr + keys[5].ToString() + ",";
            //}
            //if (keys[6] == null)
            //{
            //    tracestr = tracestr + ",";
            //}
            //else
            //{
            //    tracestr = tracestr + keys[6].ToString() + ",";
            //}
            //if (keys[7] == null)
            //{
            //    tracestr = tracestr + ",";
            //}
            //else
            //{
            //    tracestr = tracestr + keys[7].ToString() + ",";
            //}
            //labResult.Text = String.Format("你点击了第 {0} 行，第 {1} 列，行命令是 {2}", e.RowIndex + 1, e.ColumnIndex + 1, e.CommandName) +
            //       "<br />" +
            //       String.Format("当前行数据 - 编号：{0}，姓名：{1}", keys[0], keys[1]);
            if (e.CommandName == "EditMm")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                var q = (from a in DB.Pp_Ec_Subs
                             //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                             //join b in DB.Pp_Ec_Subs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                             //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                             //                                   select d.D_SAP_ZCA1D_Z002)
                             //                                .Contains(a.D_SAP_ZPABD_S002)
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                             //                                   select d.D_SAP_DEST_Z001)
                             //                                 .Contains(a.D_SAP_ZPABD_S002)
                         where a.Ec_no == (strEc)
                         where a.Ec_model == (strModel)
                         where a.Ec_bomitem == (strItem)
                         //where a.Ec_bomitem.Contains(strEc_bomitem)
                         //where a.Ec_olditem.Contains(strEc_olditem)
                         //where a.Ec_newitem.Contains(strEc_newitem)
                         //where b.Ec_no == strecn
                         //where a.Prodate == sdate//投入日期
                         select a).ToList();
                List<Pp_Ec_Sub> UpdateList = (from item in q
                                              select new Pp_Ec_Sub
                                              {
                                                  GUID = item.GUID,
                                                  Ec_no = item.Ec_no,
                                                  Ec_model = item.Ec_model,
                                                  Ec_bomitem = item.Ec_bomitem,
                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                  Ec_olditem = item.Ec_olditem,
                                                  Ec_oldtext = item.Ec_oldtext,
                                                  Ec_oldqty = item.Ec_oldqty,
                                                  Ec_oldset = item.Ec_oldset,
                                                  Ec_newitem = item.Ec_newitem,
                                                  Ec_newtext = item.Ec_newtext,
                                                  Ec_newqty = item.Ec_newqty,
                                                  Ec_newset = item.Ec_newset,
                                                  Ec_bomno = item.Ec_bomno,
                                                  Ec_change = item.Ec_change,
                                                  Ec_local = item.Ec_local,
                                                  Ec_note = item.Ec_note,
                                                  Ec_process = item.Ec_process,
                                                  Ec_procurement = item.Ec_procurement,
                                                  Ec_location = item.Ec_location,
                                                  Ec_eol = item.Ec_eol,
                                                  IsCheck = item.IsCheck,
                                                  IsManage = item.IsManage,
                                                  IsMmManage = 0,
                                                  IsAssyManage = item.IsAssyManage,
                                                  IsPcbaManage = item.IsPcbaManage,
                                                  Ec_bomdate = item.Ec_bomdate,
                                                  Ec_entrydate = item.Ec_entrydate,
                                                  Ec_pmcdate = item.Ec_pmcdate,
                                                  Ec_pmclot = item.Ec_pmclot,
                                                  Ec_pmcmemo = item.Ec_pmcmemo,
                                                  Ec_pmcnote = item.Ec_pmcnote,
                                                  Ec_bstock = item.Ec_bstock,
                                                  pmcModifier = item.pmcModifier,
                                                  pmcModifyDate = item.pmcModifyDate,
                                                  Ec_p2ddate = item.Ec_p2ddate,
                                                  Ec_p2dlot = item.Ec_p2dlot,
                                                  Ec_p2dnote = item.Ec_p2dnote,
                                                  p2dModifier = item.p2dModifier,
                                                  p2dModifyDate = item.p2dModifyDate,
                                                  Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),//投入日期
                                                  Ec_mmlot = "与部管无关",
                                                  Ec_mmlotno = "4400000",
                                                  Ec_mmnote = "与部管无关",
                                                  mmModifier = GetIdentityName(),
                                                  mmModifyDate = DateTime.Now,
                                                  Ec_purdate = item.Ec_purdate,
                                                  Ec_purorder = item.Ec_purorder,
                                                  Ec_pursupplier = item.Ec_pursupplier,
                                                  Ec_purnote = item.Ec_purnote,
                                                  ppModifier = item.ppModifier,
                                                  ppModifyDate = item.ppModifyDate,
                                                  Ec_iqcdate = item.Ec_iqcdate,
                                                  Ec_iqcorder = item.Ec_iqcorder,
                                                  Ec_iqcnote = item.Ec_iqcnote,
                                                  iqcModifier = item.iqcModifier,
                                                  iqcModifyDate = item.iqcModifyDate,
                                                  Ec_p1ddate = item.Ec_p1ddate,
                                                  Ec_p1dline = item.Ec_p1dline,
                                                  Ec_p1dlot = item.Ec_p1dlot,
                                                  Ec_p1dnote = item.Ec_p1dnote,
                                                  p1dModifier = item.p1dModifier,
                                                  p1dModifyDate = item.p1dModifyDate,
                                                  Ec_qadate = item.Ec_qadate,
                                                  Ec_qalot = item.Ec_qalot,
                                                  Ec_qanote = item.Ec_qanote,
                                                  qaModifier = item.qaModifier,
                                                  qaModifyDate = item.qaModifyDate,
                                                  UDF01 = item.UDF01,
                                                  UDF02 = item.UDF02,
                                                  UDF03 = item.UDF03,
                                                  UDF04 = item.UDF04,
                                                  UDF05 = item.UDF05,
                                                  UDF06 = item.UDF06,
                                                  UDF51 = item.UDF51,
                                                  UDF52 = item.UDF52,
                                                  UDF53 = item.UDF53,
                                                  UDF54 = item.UDF54,
                                                  UDF55 = item.UDF55,
                                                  UDF56 = item.UDF56,
                                                  IsDeleted = item.IsDeleted,
                                                  Remark = item.Remark,
                                                  Creator = item.Creator,
                                                  CreateDate = item.CreateDate,
                                                  Modifier = item.Modifier,
                                                  ModifyDate = item.ModifyDate,
                                              }).ToList();
                DB.BulkUpdate(UpdateList);
                DB.BulkSaveChanges();

                InsNetOperateNotes();
                //DB.Pp_Ec_Subs
                //   .Where(it => it.Ec_no == strEc && it.Ec_model == strModel && it.Ec_bomitem == strItem)
                //   .ToList()
                //   .ForEach(x =>
                //   {
                //       x.Ec_mmdate = DateTime.Now.ToString("yyyyMMdd");//投入日期
                //       x.Ec_mmlot = "与部管无关";
                //       x.Ec_mmlotno = "4400000";
                //       x.Ec_mmnote = "与部管无关";
                //       // x.IsDeleted = 1;
                //       x.Modifier = GetIdentityName();
                //       x.ModifyDate = DateTime.Now;
                //   });

                //labResult.Text = keys[0].ToString();
                // PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Report/daily_report.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "EditAssy")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                var q = (from a in DB.Pp_Ec_Subs
                             //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                             //join b in DB.Pp_Ec_Subs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                             //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                             //                                   select d.D_SAP_ZCA1D_Z002)
                             //                                .Contains(a.D_SAP_ZPABD_S002)
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                             //                                   select d.D_SAP_DEST_Z001)
                             //                                 .Contains(a.D_SAP_ZPABD_S002)
                         where a.Ec_no == (strEc)
                         where a.Ec_model == (strModel)
                         where a.Ec_bomitem == (strItem)
                         //where a.Ec_bomitem.Contains(strEc_bomitem)
                         //where a.Ec_olditem.Contains(strEc_olditem)
                         //where a.Ec_newitem.Contains(strEc_newitem)
                         //where b.Ec_no == strecn
                         //where a.Prodate == sdate//投入日期
                         select a).ToList();
                List<Pp_Ec_Sub> UpdateList = (from item in q
                                              select new Pp_Ec_Sub
                                              {
                                                  GUID = item.GUID,
                                                  Ec_no = item.Ec_no,
                                                  Ec_model = item.Ec_model,
                                                  Ec_bomitem = item.Ec_bomitem,
                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                  Ec_olditem = item.Ec_olditem,
                                                  Ec_oldtext = item.Ec_oldtext,
                                                  Ec_oldqty = item.Ec_oldqty,
                                                  Ec_oldset = item.Ec_oldset,
                                                  Ec_newitem = item.Ec_newitem,
                                                  Ec_newtext = item.Ec_newtext,
                                                  Ec_newqty = item.Ec_newqty,
                                                  Ec_newset = item.Ec_newset,
                                                  Ec_bomno = item.Ec_bomno,
                                                  Ec_change = item.Ec_change,
                                                  Ec_local = item.Ec_local,
                                                  Ec_note = item.Ec_note,
                                                  Ec_process = item.Ec_process,
                                                  Ec_procurement = item.Ec_procurement,
                                                  Ec_location = item.Ec_location,
                                                  Ec_eol = item.Ec_eol,
                                                  IsCheck = item.IsCheck,
                                                  IsManage = item.IsManage,
                                                  IsMmManage = item.IsMmManage,
                                                  IsAssyManage = 0,
                                                  IsPcbaManage = item.IsPcbaManage,
                                                  Ec_bomdate = item.Ec_bomdate,
                                                  Ec_entrydate = item.Ec_entrydate,
                                                  Ec_pmcdate = item.Ec_pmcdate,
                                                  Ec_pmclot = item.Ec_pmclot,
                                                  Ec_pmcmemo = item.Ec_pmcmemo,
                                                  Ec_pmcnote = item.Ec_pmcnote,
                                                  Ec_bstock = item.Ec_bstock,
                                                  pmcModifier = item.pmcModifier,
                                                  pmcModifyDate = item.pmcModifyDate,
                                                  Ec_p2ddate = item.Ec_p2ddate,
                                                  Ec_p2dlot = item.Ec_p2dlot,
                                                  Ec_p2dnote = item.Ec_p2dnote,
                                                  p2dModifier = item.p2dModifier,
                                                  p2dModifyDate = item.p2dModifyDate,

                                                  //Ec_p2ddate = Ec_p2ddate.SelectedDate.Value.ToString("yyyyMMdd"),
                                                  //Ec_p2dlot = "与制二无关",
                                                  //Ec_p2dnote = "与制二无关",
                                                  //p2dModifier = GetIdentityName(),
                                                  //p2dModifyDate = DateTime.Now,

                                                  Ec_mmdate = item.Ec_mmdate,//投入日期
                                                  Ec_mmlot = item.Ec_mmlot,
                                                  Ec_mmlotno = item.Ec_mmlotno,
                                                  Ec_mmnote = item.Ec_mmnote,
                                                  mmModifier = item.mmModifier,
                                                  mmModifyDate = item.mmModifyDate,
                                                  Ec_purdate = item.Ec_purdate,
                                                  Ec_purorder = item.Ec_purorder,
                                                  Ec_pursupplier = item.Ec_pursupplier,
                                                  Ec_purnote = item.Ec_purnote,
                                                  ppModifier = item.ppModifier,
                                                  ppModifyDate = item.ppModifyDate,
                                                  Ec_iqcdate = item.Ec_iqcdate,
                                                  Ec_iqcorder = item.Ec_iqcorder,
                                                  Ec_iqcnote = item.Ec_iqcnote,
                                                  iqcModifier = item.iqcModifier,
                                                  iqcModifyDate = item.iqcModifyDate,

                                                  Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                  Ec_p1dline = "与制一无关",
                                                  Ec_p1dlot = "与制一无关",
                                                  Ec_p1dnote = "与制一无关",
                                                  p1dModifier = GetIdentityName(),
                                                  p1dModifyDate = DateTime.Now,

                                                  //Ec_p1ddate = item.Ec_p1ddate,
                                                  //Ec_p1dline = item.Ec_p1dline,
                                                  //Ec_p1dlot = item.Ec_p1dlot,
                                                  //Ec_p1dnote = item.Ec_p1dnote,
                                                  //p1dModifier = item.p1dModifier,
                                                  //p1dModifyDate = item.p1dModifyDate,
                                                  Ec_qadate = item.Ec_qadate,
                                                  Ec_qalot = item.Ec_qalot,
                                                  Ec_qanote = item.Ec_qanote,
                                                  qaModifier = item.qaModifier,
                                                  qaModifyDate = item.qaModifyDate,
                                                  UDF01 = item.UDF01,
                                                  UDF02 = item.UDF02,
                                                  UDF03 = item.UDF03,
                                                  UDF04 = item.UDF04,
                                                  UDF05 = item.UDF05,
                                                  UDF06 = item.UDF06,
                                                  UDF51 = item.UDF51,
                                                  UDF52 = item.UDF52,
                                                  UDF53 = item.UDF53,
                                                  UDF54 = item.UDF54,
                                                  UDF55 = item.UDF55,
                                                  UDF56 = item.UDF56,
                                                  IsDeleted = item.IsDeleted,
                                                  Remark = item.Remark,

                                                  Creator = item.Creator,
                                                  CreateDate = item.CreateDate,
                                                  Modifier = item.Modifier,
                                                  ModifyDate = item.ModifyDate,
                                              }).ToList();
                DB.BulkUpdate(UpdateList);
                DB.BulkSaveChanges();
                InsNetOperateNotes();
            }
            if (e.CommandName == "EditPcba")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                var q = (from a in DB.Pp_Ec_Subs
                             //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                             //join b in DB.Pp_Ec_Subs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                             //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                             //                                   select d.D_SAP_ZCA1D_Z002)
                             //                                .Contains(a.D_SAP_ZPABD_S002)
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                             //                                   select d.D_SAP_DEST_Z001)
                             //                                 .Contains(a.D_SAP_ZPABD_S002)
                         where a.Ec_no == (strEc)
                         where a.Ec_model == (strModel)
                         where a.Ec_bomitem == (strItem)
                         //where a.Ec_bomitem.Contains(strEc_bomitem)
                         //where a.Ec_olditem.Contains(strEc_olditem)
                         //where a.Ec_newitem.Contains(strEc_newitem)
                         //where b.Ec_no == strecn
                         //where a.Prodate == sdate//投入日期
                         select a).ToList();
                List<Pp_Ec_Sub> UpdateList = (from item in q
                                              select new Pp_Ec_Sub
                                              {
                                                  GUID = item.GUID,
                                                  Ec_no = item.Ec_no,
                                                  Ec_model = item.Ec_model,
                                                  Ec_bomitem = item.Ec_bomitem,
                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                  Ec_olditem = item.Ec_olditem,
                                                  Ec_oldtext = item.Ec_oldtext,
                                                  Ec_oldqty = item.Ec_oldqty,
                                                  Ec_oldset = item.Ec_oldset,
                                                  Ec_newitem = item.Ec_newitem,
                                                  Ec_newtext = item.Ec_newtext,
                                                  Ec_newqty = item.Ec_newqty,
                                                  Ec_newset = item.Ec_newset,
                                                  Ec_bomno = item.Ec_bomno,
                                                  Ec_change = item.Ec_change,
                                                  Ec_local = item.Ec_local,
                                                  Ec_note = item.Ec_note,
                                                  Ec_process = item.Ec_process,
                                                  Ec_procurement = item.Ec_procurement,
                                                  Ec_location = item.Ec_location,
                                                  Ec_eol = item.Ec_eol,
                                                  IsCheck = item.IsCheck,
                                                  IsManage = item.IsManage,
                                                  IsMmManage = item.IsMmManage,
                                                  IsAssyManage = item.IsAssyManage,
                                                  IsPcbaManage = 0,
                                                  Ec_bomdate = item.Ec_bomdate,
                                                  Ec_entrydate = item.Ec_entrydate,
                                                  Ec_pmcdate = item.Ec_pmcdate,
                                                  Ec_pmclot = item.Ec_pmclot,
                                                  Ec_pmcmemo = item.Ec_pmcmemo,
                                                  Ec_pmcnote = item.Ec_pmcnote,
                                                  Ec_bstock = item.Ec_bstock,
                                                  pmcModifier = item.pmcModifier,
                                                  pmcModifyDate = item.pmcModifyDate,

                                                  Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                  Ec_p2dlot = "与制二无关",
                                                  Ec_p2dnote = "与制二无关",
                                                  p2dModifier = GetIdentityName(),
                                                  p2dModifyDate = DateTime.Now,

                                                  Ec_mmdate = item.Ec_mmdate,//投入日期
                                                  Ec_mmlot = item.Ec_mmlot,
                                                  Ec_mmlotno = item.Ec_mmlotno,
                                                  Ec_mmnote = item.Ec_mmnote,
                                                  mmModifier = item.mmModifier,
                                                  mmModifyDate = item.mmModifyDate,
                                                  Ec_purdate = item.Ec_purdate,
                                                  Ec_purorder = item.Ec_purorder,
                                                  Ec_pursupplier = item.Ec_pursupplier,
                                                  Ec_purnote = item.Ec_purnote,
                                                  ppModifier = item.ppModifier,
                                                  ppModifyDate = item.ppModifyDate,
                                                  Ec_iqcdate = item.Ec_iqcdate,
                                                  Ec_iqcorder = item.Ec_iqcorder,
                                                  Ec_iqcnote = item.Ec_iqcnote,
                                                  iqcModifier = item.iqcModifier,
                                                  iqcModifyDate = item.iqcModifyDate,
                                                  Ec_p1ddate = item.Ec_p1ddate,
                                                  Ec_p1dline = item.Ec_p1dline,
                                                  Ec_p1dlot = item.Ec_p1dlot,
                                                  Ec_p1dnote = item.Ec_p1dnote,
                                                  p1dModifier = item.p1dModifier,
                                                  p1dModifyDate = item.p1dModifyDate,
                                                  Ec_qadate = item.Ec_qadate,
                                                  Ec_qalot = item.Ec_qalot,
                                                  Ec_qanote = item.Ec_qanote,
                                                  qaModifier = item.qaModifier,
                                                  qaModifyDate = item.qaModifyDate,
                                                  UDF01 = item.UDF01,
                                                  UDF02 = item.UDF02,
                                                  UDF03 = item.UDF03,
                                                  UDF04 = item.UDF04,
                                                  UDF05 = item.UDF05,
                                                  UDF06 = item.UDF06,
                                                  UDF51 = item.UDF51,
                                                  UDF52 = item.UDF52,
                                                  UDF53 = item.UDF53,
                                                  UDF54 = item.UDF54,
                                                  UDF55 = item.UDF55,
                                                  UDF56 = item.UDF56,
                                                  IsDeleted = item.IsDeleted,
                                                  Remark = item.Remark,

                                                  Creator = item.Creator,
                                                  CreateDate = item.CreateDate,
                                                  Modifier = item.Modifier,
                                                  ModifyDate = item.ModifyDate,
                                              }).ToList();
                DB.BulkUpdate(UpdateList);
                DB.BulkSaveChanges();
                InsNetOperateNotes();
            }
            if (e.CommandName == "EditPe")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGEdit"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                SaveItemSop();
                //DB.Pp_Ec_Subs
                //       .Where(it => it.Ec_no == strEc && it.Ec_model == strModel && it.Ec_bomitem == strItem)
                //       .ToList()
                //       .ForEach(x =>
                //       {
                //           x.Ec_mmdate = DateTime.Now.ToString("yyyyMMdd");//投入日期
                //           x.Ec_mmlot = "与部管无关";
                //           x.Ec_mmlotno = "4400000";
                //           x.Ec_mmnote = "与部管无关";
                //           x.IsDeleted = 1;
                //           x.Modifier = GetIdentityName();
                //           x.ModifyDate = DateTime.Now;
                //       });
                //labResult.Text = keys[0].ToString();
                //PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Report/daily_report.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            //int del_ID = GetSelectedDataKeyID(Grid1);
            if (e.CommandName == "EditDel")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreEcENGDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                //var q = from a in DB.Pp_Ec_Subs
                //        where a.Ec_no == keys[0].ToString()
                //        where a.Ec_model == keys[1].ToString()
                //        where a.Ec_bomitem == keys[2].ToString()
                //        select a;

                // Pp_Ec_Sub current = DB.Pp_Ec_Subs.Find(del_ID);
                string Contectext = strEc + "," + strModel + "," + strItem;
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del生产* " + Contectext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "设变删除标记", OperateNotes);

                DB.Pp_Ec_Subs
                    .Where(it => it.Ec_no == strEc && it.Ec_model == strModel && it.Ec_bomitem == strItem)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.IsDeleted = 1;
                        x.Modifier = GetIdentityName();
                        x.ModifyDate = DateTime.Now;
                    });
                //DB.Pp_Ec_Subs.Where(it => it.Ec_no == keys[0].ToString() && it.Ec_model == keys[1].ToString() && it.Ec_bomitem == keys[2].ToString()).DeleteFromQuery();
                //DB.Pp_P1d_Outputs.Where(l => l.ID == del_ID).DeleteFromQuery();

                //更新订单已生产数量
                //UpdatingHelper.DelUpdateOrderRealQty(current.Proorder, GetIdentityName());

                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), "修改", "生产管理", "设变修改", OperateNotes);
            }
            BindGrid();
        }

        protected void DDL_Item_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Item.SelectedIndex != -1 && DDL_Item.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {
            //int id = GetQueryIntValue("id");
            //int id = int.Parse(strID);
            //Convert.ToInt32(strID)
            Pp_Ec item = DB.Pp_Ecs

                .Where(u => u.GUID.ToString() == strID).FirstOrDefault();

            item.Ec_issuedate = Ec_issuedate.Text;
            item.Ec_no = Ec_no.Text;
            //item.Ec_relyno = Ec_relyno.Text;
            //item.Ec_model = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();

            //item.Ec_tcjno = Ec_tcjno.Text;

            item.Ec_details = Ec_details.Text;//设变内容
            item.Ec_leader = Ec_leader.SelectedItem.Text;//DTA担当
            item.Ec_distinction = int.Parse(Ec_distinction.SelectedValue);
            item.Ec_entrydate = DateTime.Now.ToString("yyyyMMdd");
            //20210109修改BUG
            if (!string.IsNullOrEmpty(Ec_lossamount.Text))
            {
                item.Ec_lossamount = decimal.Parse(Ec_lossamount.Text);//金额
            }
            else
            {
                item.Ec_lossamount = 0;
            }
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
            Pdoc();
            if (!String.IsNullOrEmpty(txtPdoc))
            {
                item.Ec_documents = txtPdoc;
            }
            else
            {
                if (oldEc_documents.Text == "-")
                {
                    item.Ec_documents = "";
                }
                //else
                //{
                //    item.Ec_documents = "../../Lf_Documents/ecdocs/" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + oldEc_documents.Text;
                //}
            }
            //if (IsSopUpdate.SelectedValue == "1")
            //{
            //    item.IsSopUpdate = 1;
            //}
            //else

            //{
            //    item.IsSopUpdate = 0;
            //}
            //if (IsManage.SelectedValue == "1")
            //{
            //    item.IsManage = 1;
            //}
            //else

            //{
            //    item.IsManage = 0;
            //}
            item.Ec_title = Ec_title.Text;
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
                Alert.ShowInTop(Server.MapPath("~"));

                string ph = Server.MapPath("~");

                Ec_letterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

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
                Ec_eppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPpbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

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
                Ec_teppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPjpbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        //设变
        private void Pdoc()
        {
            if (Ec_documents.HasFile)
            {
                string fileName = Ec_documents.ShortFileName;

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

                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Ec_no.Text + fileName;
                Ec_documents.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPdoc = "../../Lf_Documents/ecdocs/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

        private void BindDdladmindist()
        {
            IQueryable<Adm_Dict> q = DB.Adm_Dicts;
            q = q.Where(u => u.DictType.Contains("app_ec_mgtype"));

            // 绑定到下拉列表（启用模拟树功能）

            Ec_distinction.DataTextField = "DictLabel";
            Ec_distinction.DataValueField = "DictValue";
            Ec_distinction.DataSource = q;
            Ec_distinction.DataBind();

            // 选中根节点
            Ec_distinction.SelectedValue = "0";
        }

        private void SaveItemSub()//新增设变单身
        {
            try

            {
                //"&&"= and "||"= or
                if (oldEc_distinction != Ec_distinction.SelectedValue)
                {
                    //批量删除单身
                    DeleteEcSubs();

                    #region 停产机种不导入

                    var q_NotEollist = from a in DB.Pp_SapEcnSubs
                                       where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                                                          where d.D_SAP_ZCA1D_Z034 == ""
                                                                          select d.D_SAP_ZCA1D_Z002)
                                                                       .Contains(a.D_SAP_ZPABD_S002)
                                       where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                                                          select d.D_SAP_DEST_Z001)
                                                                        .Contains(a.D_SAP_ZPABD_S002)
                                       where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                                       select a;

                    #endregion 停产机种不导入

                    if (this.Ec_distinction.SelectedValue == "1")//全仕向
                    {
                        //if (this.IsManage.SelectedValue == "1")
                        //{
                        //Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课需要管理！", "提示信息", MessageBoxIcon.Information);

                        #region 1.非采购件

                        //1.非采购件
                        var New_NonPurchase = from a in q_NotEollist
                                              join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                              join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                              where b.D_SAP_ZCA1D_Z010 == "E"
                                              select new
                                              {
                                                  Ec_no = a.D_SAP_ZPABD_S001,
                                                  Ec_model = c.D_SAP_DEST_Z002,
                                                  Ec_bomitem = a.D_SAP_ZPABD_S002,
                                                  Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                                  Ec_olditem = a.D_SAP_ZPABD_S004,
                                                  Ec_oldtext = a.D_SAP_ZPABD_S005,
                                                  Ec_oldqty = a.D_SAP_ZPABD_S006,
                                                  Ec_oldset = a.D_SAP_ZPABD_S007,
                                                  Ec_newitem = a.D_SAP_ZPABD_S008,
                                                  Ec_newtext = a.D_SAP_ZPABD_S009,
                                                  Ec_newqty = a.D_SAP_ZPABD_S010,
                                                  Ec_newset = a.D_SAP_ZPABD_S011,
                                                  Ec_bomno = a.D_SAP_ZPABD_S012,
                                                  Ec_change = a.D_SAP_ZPABD_S013,
                                                  Ec_local = a.D_SAP_ZPABD_S014,
                                                  Ec_note = a.D_SAP_ZPABD_S015,
                                                  Ec_process = a.D_SAP_ZPABD_S016,

                                                  Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                                  Ec_location = b.D_SAP_ZCA1D_Z030,
                                                  Ec_eol = "",
                                                  IsCheck = b.D_SAP_ZCA1D_Z019,
                                              };

                        var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                                                               select new Pp_Ec_Sub
                                                               {
                                                                   GUID = Guid.NewGuid(),
                                                                   Ec_no = item.Ec_no,
                                                                   Ec_model = item.Ec_model,
                                                                   Ec_bomitem = item.Ec_bomitem,
                                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                                   Ec_olditem = item.Ec_olditem,
                                                                   Ec_oldtext = item.Ec_oldtext,
                                                                   Ec_oldqty = item.Ec_oldqty,
                                                                   Ec_oldset = item.Ec_oldset,
                                                                   Ec_newitem = item.Ec_newitem,
                                                                   Ec_newtext = item.Ec_newtext,
                                                                   Ec_newqty = item.Ec_newqty,
                                                                   Ec_newset = item.Ec_newset,
                                                                   Ec_bomno = item.Ec_bomno,
                                                                   Ec_change = item.Ec_change,
                                                                   Ec_local = item.Ec_local,
                                                                   Ec_note = item.Ec_note,
                                                                   Ec_process = item.Ec_process,
                                                                   Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_procurement = item.Ec_procurement,
                                                                   Ec_location = item.Ec_location,
                                                                   IsCheck = item.IsCheck,
                                                                   IsManage = 1,
                                                                   IsMmManage = 0,
                                                                   IsAssyManage = 1,
                                                                   IsPcbaManage = 1,
                                                                   Ec_eol = item.Ec_eol,

                                                                   Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_pmclot = "虚设件变更",
                                                                   Ec_pmcmemo = "虚设件变更",
                                                                   Ec_pmcnote = "虚设件变更",
                                                                   Ec_bstock = 0,
                                                                   pmcModifier = GetIdentityName(),
                                                                   pmcModifyDate = DateTime.Now,

                                                                   //    //制二
                                                                   Ec_p2ddate = "",
                                                                   Ec_p2dlot = "",
                                                                   Ec_p2dnote = "",
                                                                   p2dModifier = GetIdentityName(),
                                                                   p2dModifyDate = DateTime.Now,

                                                                   //    //部管
                                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_mmlot = "与部管无关",
                                                                   Ec_mmlotno = "与部管无关",
                                                                   Ec_mmnote = "与部管无关",
                                                                   mmModifier = GetIdentityName(),
                                                                   mmModifyDate = DateTime.Now,

                                                                   //    //采购
                                                                   Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_purorder = "4300000000",
                                                                   Ec_pursupplier = "H200000",
                                                                   Ec_purnote = "与采购无关",
                                                                   ppModifier = GetIdentityName(),
                                                                   ppModifyDate = DateTime.Now,

                                                                   //    //受检
                                                                   Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_iqcorder = "4300000000",
                                                                   Ec_iqcnote = "与受检无关",
                                                                   iqcModifier = GetIdentityName(),
                                                                   iqcModifyDate = DateTime.Now,

                                                                   //    //制一
                                                                   Ec_p1ddate = "",
                                                                   Ec_p1dline = "",
                                                                   Ec_p1dlot = "",
                                                                   Ec_p1dnote = "",
                                                                   p1dModifier = GetIdentityName(),
                                                                   p1dModifyDate = DateTime.Now,

                                                                   //    //品管
                                                                   Ec_qadate = "",
                                                                   Ec_qalot = "",
                                                                   Ec_qanote = "",
                                                                   qaModifier = GetIdentityName(),
                                                                   qaModifyDate = DateTime.Now,

                                                                   UDF01 = "NonPurchase",
                                                                   UDF02 = "",
                                                                   UDF03 = "",
                                                                   UDF04 = "",
                                                                   UDF05 = "",
                                                                   UDF06 = "",
                                                                   UDF51 = 0,
                                                                   UDF52 = 0,
                                                                   UDF53 = 0,
                                                                   UDF54 = 0,
                                                                   UDF55 = 0,
                                                                   UDF56 = 0,
                                                                   IsDeleted = 0,

                                                                   Remark = "管理区分全仕向",
                                                                   Creator = GetIdentityName(),
                                                                   CreateDate = DateTime.Now,
                                                               }).Distinct().ToList();
                        DB.BulkInsert(New_NonPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 1.非采购件

                        #region 2.新物料为空

                        var NonItem = from a in q_NotEollist
                                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                      where a.D_SAP_ZPABD_S008 == "0"
                                      //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                      //                                   where d.D_SAP_ZCA1D_Z034 == ""
                                      //                                   select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                      //                                   select d.D_SAP_DEST_Z001)
                                      //                                 .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                                      //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                                      //                                    select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S008)
                                      where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                                      //where b.Ec_no == strecn
                                      //where a.Prodate == sdate//投入日期
                                      select new
                                      {
                                          Ec_no = a.D_SAP_ZPABD_S001,
                                          Ec_model = c.D_SAP_DEST_Z002,
                                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                          Ec_olditem = a.D_SAP_ZPABD_S004,
                                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                                          Ec_oldset = a.D_SAP_ZPABD_S007,
                                          Ec_newitem = a.D_SAP_ZPABD_S008,
                                          Ec_newtext = a.D_SAP_ZPABD_S009,
                                          Ec_newqty = a.D_SAP_ZPABD_S010,
                                          Ec_newset = a.D_SAP_ZPABD_S011,
                                          Ec_bomno = a.D_SAP_ZPABD_S012,
                                          Ec_change = a.D_SAP_ZPABD_S013,
                                          Ec_local = a.D_SAP_ZPABD_S014,
                                          Ec_note = a.D_SAP_ZPABD_S015,
                                          Ec_process = a.D_SAP_ZPABD_S016,
                                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                          Ec_location = b.D_SAP_ZCA1D_Z030,
                                          Ec_eol = "",
                                          IsCheck = "",
                                      };
                        var result_NonItem = NonItem.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = Guid.NewGuid(),
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               Ec_eol = item.Ec_eol,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = 1,
                                                               IsMmManage = 0,
                                                               IsAssyManage = 1,
                                                               IsPcbaManage = 1,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                                                               //    //生管
                                                               Ec_pmcdate = "",
                                                               Ec_pmclot = "",
                                                               Ec_pmcmemo = "",
                                                               Ec_pmcnote = "",
                                                               Ec_bstock = 0,
                                                               pmcModifier = GetIdentityName(),
                                                               pmcModifyDate = DateTime.Now,
                                                               //    //部管
                                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_mmlot = "自然切换",
                                                               Ec_mmlotno = "4400000",
                                                               Ec_mmnote = "自然切换",
                                                               mmModifier = GetIdentityName(),
                                                               mmModifyDate = DateTime.Now,
                                                               //    //采购
                                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_purorder = "4300000000",
                                                               Ec_pursupplier = "H200000",
                                                               Ec_purnote = "自然切换",
                                                               ppModifier = GetIdentityName(),
                                                               ppModifyDate = DateTime.Now,
                                                               //    //受检
                                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_iqcorder = "4300000000",
                                                               Ec_iqcnote = "自然切换",
                                                               iqcModifier = GetIdentityName(),
                                                               iqcModifyDate = DateTime.Now,
                                                               //    //制一
                                                               Ec_p1ddate = "",
                                                               Ec_p1dline = "",
                                                               Ec_p1dlot = "",
                                                               Ec_p1dnote = "",
                                                               p1dModifier = GetIdentityName(),
                                                               p1dModifyDate = DateTime.Now,
                                                               //    //制二
                                                               Ec_p2ddate = "",
                                                               Ec_p2dlot = "",
                                                               Ec_p2dnote = "",
                                                               p2dModifier = GetIdentityName(),
                                                               p2dModifyDate = DateTime.Now,
                                                               //    //品管
                                                               Ec_qadate = "",
                                                               Ec_qalot = "",
                                                               Ec_qanote = "",
                                                               qaModifier = GetIdentityName(),
                                                               qaModifyDate = DateTime.Now,

                                                               UDF01 = "NonItems",
                                                               UDF02 = "",
                                                               UDF03 = "",
                                                               UDF04 = "",
                                                               UDF05 = "",
                                                               UDF06 = "",
                                                               UDF51 = 0,
                                                               UDF52 = 0,
                                                               UDF53 = 0,
                                                               UDF54 = 0,
                                                               UDF55 = 0,
                                                               UDF56 = 0,
                                                               IsDeleted = 0,
                                                               Remark = "管理区分全仕向",
                                                               Creator = GetIdentityName(),
                                                               CreateDate = DateTime.Now,
                                                           }).Distinct().ToList();
                        DB.BulkInsert(New_NonItemList);
                        DB.BulkSaveChanges();

                        #endregion 2.新物料为空

                        #region 3.采购件非C003

                        //1.采购件非C003

                        var MMPurchase = from a in q_NotEollist

                                         join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                         join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                         where b.D_SAP_ZCA1D_Z010 == "F"
                                         where b.D_SAP_ZCA1D_Z030 != "C003"
                                         select new
                                         {
                                             Ec_no = a.D_SAP_ZPABD_S001,
                                             Ec_model = c.D_SAP_DEST_Z002,
                                             Ec_bomitem = a.D_SAP_ZPABD_S002,
                                             Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                             Ec_olditem = a.D_SAP_ZPABD_S004,
                                             Ec_oldtext = a.D_SAP_ZPABD_S005,
                                             Ec_oldqty = a.D_SAP_ZPABD_S006,
                                             Ec_oldset = a.D_SAP_ZPABD_S007,
                                             Ec_newitem = a.D_SAP_ZPABD_S008,
                                             Ec_newtext = a.D_SAP_ZPABD_S009,
                                             Ec_newqty = a.D_SAP_ZPABD_S010,
                                             Ec_newset = a.D_SAP_ZPABD_S011,
                                             Ec_bomno = a.D_SAP_ZPABD_S012,
                                             Ec_change = a.D_SAP_ZPABD_S013,
                                             Ec_local = a.D_SAP_ZPABD_S014,
                                             Ec_note = a.D_SAP_ZPABD_S015,
                                             Ec_process = a.D_SAP_ZPABD_S016,

                                             Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                             Ec_location = b.D_SAP_ZCA1D_Z030,
                                             Ec_eol = "",
                                             IsCheck = b.D_SAP_ZCA1D_Z019,
                                         };

                        var result_MMPurchase = MMPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                                                              select new Pp_Ec_Sub
                                                              {
                                                                  GUID = Guid.NewGuid(),
                                                                  Ec_no = item.Ec_no,
                                                                  Ec_model = item.Ec_model,
                                                                  Ec_bomitem = item.Ec_bomitem,
                                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                                  Ec_olditem = item.Ec_olditem,
                                                                  Ec_oldtext = item.Ec_oldtext,
                                                                  Ec_oldqty = item.Ec_oldqty,
                                                                  Ec_oldset = item.Ec_oldset,
                                                                  Ec_newitem = item.Ec_newitem,
                                                                  Ec_newtext = item.Ec_newtext,
                                                                  Ec_newqty = item.Ec_newqty,
                                                                  Ec_newset = item.Ec_newset,
                                                                  Ec_bomno = item.Ec_bomno,
                                                                  Ec_change = item.Ec_change,
                                                                  Ec_local = item.Ec_local,
                                                                  Ec_note = item.Ec_note,
                                                                  Ec_process = item.Ec_process,
                                                                  Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_procurement = item.Ec_procurement,
                                                                  Ec_location = item.Ec_location,
                                                                  IsCheck = item.IsCheck,
                                                                  IsManage = 1,
                                                                  IsMmManage = 1,
                                                                  IsAssyManage = 1,
                                                                  IsPcbaManage = 1,
                                                                  Ec_eol = item.Ec_eol,

                                                                  Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_pmcdate = "",
                                                                  Ec_pmclot = "",
                                                                  Ec_pmcmemo = "",
                                                                  Ec_pmcnote = "",
                                                                  Ec_bstock = 0,
                                                                  pmcModifier = GetIdentityName(),
                                                                  pmcModifyDate = DateTime.Now,

                                                                  //    //制二
                                                                  Ec_p2ddate = "",
                                                                  Ec_p2dlot = "",
                                                                  Ec_p2dnote = "",
                                                                  p2dModifier = GetIdentityName(),
                                                                  p2dModifyDate = DateTime.Now,

                                                                  //    //部管
                                                                  Ec_mmdate = "",
                                                                  Ec_mmlot = "",
                                                                  Ec_mmlotno = "",
                                                                  Ec_mmnote = "",
                                                                  mmModifier = GetIdentityName(),
                                                                  mmModifyDate = DateTime.Now,

                                                                  //    //采购
                                                                  Ec_purdate = "",
                                                                  Ec_purorder = "",
                                                                  Ec_pursupplier = "",
                                                                  Ec_purnote = "",
                                                                  ppModifier = GetIdentityName(),
                                                                  ppModifyDate = DateTime.Now,

                                                                  //    //受检
                                                                  Ec_iqcdate = "",
                                                                  Ec_iqcorder = "",
                                                                  Ec_iqcnote = "",
                                                                  iqcModifier = GetIdentityName(),
                                                                  iqcModifyDate = DateTime.Now,

                                                                  //    //制一
                                                                  Ec_p1ddate = "",
                                                                  Ec_p1dline = "",
                                                                  Ec_p1dlot = "",
                                                                  Ec_p1dnote = "",
                                                                  p1dModifier = GetIdentityName(),
                                                                  p1dModifyDate = DateTime.Now,

                                                                  //    //品管
                                                                  Ec_qadate = "",
                                                                  Ec_qalot = "",
                                                                  Ec_qanote = "",
                                                                  qaModifier = GetIdentityName(),
                                                                  qaModifyDate = DateTime.Now,

                                                                  UDF01 = "MMPurchase",
                                                                  UDF02 = "",
                                                                  UDF03 = "",
                                                                  UDF04 = "",
                                                                  UDF05 = "",
                                                                  UDF06 = "",
                                                                  UDF51 = 0,
                                                                  UDF52 = 0,
                                                                  UDF53 = 0,
                                                                  UDF54 = 0,
                                                                  UDF55 = 0,
                                                                  UDF56 = 0,
                                                                  IsDeleted = 0,
                                                                  Remark = "管理区分全仕向",
                                                                  Creator = GetIdentityName(),
                                                                  CreateDate = DateTime.Now,
                                                              }).Distinct().ToList();
                        DB.BulkInsert(New_MMPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 3.采购件非C003

                        #region 4.采购件C003

                        //1.采购件C003
                        //1.采购件非C003

                        var P2dPurchase = from a in q_NotEollist

                                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                          where b.D_SAP_ZCA1D_Z010 == "F"
                                          where b.D_SAP_ZCA1D_Z030 == "C003"
                                          select new
                                          {
                                              Ec_no = a.D_SAP_ZPABD_S001,
                                              Ec_model = c.D_SAP_DEST_Z002,
                                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                              Ec_olditem = a.D_SAP_ZPABD_S004,
                                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                                              Ec_oldset = a.D_SAP_ZPABD_S007,
                                              Ec_newitem = a.D_SAP_ZPABD_S008,
                                              Ec_newtext = a.D_SAP_ZPABD_S009,
                                              Ec_newqty = a.D_SAP_ZPABD_S010,
                                              Ec_newset = a.D_SAP_ZPABD_S011,
                                              Ec_bomno = a.D_SAP_ZPABD_S012,
                                              Ec_change = a.D_SAP_ZPABD_S013,
                                              Ec_local = a.D_SAP_ZPABD_S014,
                                              Ec_note = a.D_SAP_ZPABD_S015,
                                              Ec_process = a.D_SAP_ZPABD_S016,

                                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                              Ec_location = b.D_SAP_ZCA1D_Z030,
                                              Ec_eol = "",
                                              IsCheck = b.D_SAP_ZCA1D_Z019,
                                          };

                        var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                                                                      select new Pp_Ec_Sub
                                                                      {
                                                                          GUID = Guid.NewGuid(),
                                                                          Ec_no = item.Ec_no,
                                                                          Ec_model = item.Ec_model,
                                                                          Ec_bomitem = item.Ec_bomitem,
                                                                          Ec_bomsubitem = item.Ec_bomsubitem,
                                                                          Ec_olditem = item.Ec_olditem,
                                                                          Ec_oldtext = item.Ec_oldtext,
                                                                          Ec_oldqty = item.Ec_oldqty,
                                                                          Ec_oldset = item.Ec_oldset,
                                                                          Ec_newitem = item.Ec_newitem,
                                                                          Ec_newtext = item.Ec_newtext,
                                                                          Ec_newqty = item.Ec_newqty,
                                                                          Ec_newset = item.Ec_newset,
                                                                          Ec_bomno = item.Ec_bomno,
                                                                          Ec_change = item.Ec_change,
                                                                          Ec_local = item.Ec_local,
                                                                          Ec_note = item.Ec_note,
                                                                          Ec_process = item.Ec_process,
                                                                          Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_procurement = item.Ec_procurement,
                                                                          Ec_location = item.Ec_location,
                                                                          IsCheck = item.IsCheck,
                                                                          IsManage = 1,
                                                                          IsMmManage = 0,
                                                                          IsAssyManage = 1,
                                                                          IsPcbaManage = 1,
                                                                          Ec_eol = item.Ec_eol,

                                                                          Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_pmcdate = "",
                                                                          Ec_pmclot = "",
                                                                          Ec_pmcmemo = "",
                                                                          Ec_pmcnote = "",
                                                                          Ec_bstock = 0,
                                                                          pmcModifier = GetIdentityName(),
                                                                          pmcModifyDate = DateTime.Now,

                                                                          //    //制二
                                                                          Ec_p2ddate = "",
                                                                          Ec_p2dlot = "",
                                                                          Ec_p2dnote = "",
                                                                          p2dModifier = GetIdentityName(),
                                                                          p2dModifyDate = DateTime.Now,

                                                                          //    //部管
                                                                          Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_mmlot = "与部管无关",
                                                                          Ec_mmlotno = "与部管无关",
                                                                          Ec_mmnote = "与部管无关",
                                                                          mmModifier = GetIdentityName(),
                                                                          mmModifyDate = DateTime.Now,

                                                                          //    //采购
                                                                          Ec_purdate = "",
                                                                          Ec_purorder = "",
                                                                          Ec_pursupplier = "",
                                                                          Ec_purnote = "",
                                                                          ppModifier = GetIdentityName(),
                                                                          ppModifyDate = DateTime.Now,

                                                                          //    //受检
                                                                          Ec_iqcdate = "",
                                                                          Ec_iqcorder = "",
                                                                          Ec_iqcnote = "",
                                                                          iqcModifier = GetIdentityName(),
                                                                          iqcModifyDate = DateTime.Now,

                                                                          //    //制一
                                                                          Ec_p1ddate = "",
                                                                          Ec_p1dline = "",
                                                                          Ec_p1dlot = "",
                                                                          Ec_p1dnote = "",
                                                                          p1dModifier = GetIdentityName(),
                                                                          p1dModifyDate = DateTime.Now,

                                                                          //    //品管
                                                                          Ec_qadate = "",
                                                                          Ec_qalot = "",
                                                                          Ec_qanote = "",
                                                                          qaModifier = GetIdentityName(),
                                                                          qaModifyDate = DateTime.Now,

                                                                          UDF01 = "P2DPurchase",
                                                                          UDF02 = "",
                                                                          UDF03 = "",
                                                                          UDF04 = "",
                                                                          UDF05 = "",
                                                                          UDF06 = "",
                                                                          UDF51 = 0,
                                                                          UDF52 = 0,
                                                                          UDF53 = 0,
                                                                          UDF54 = 0,
                                                                          UDF55 = 0,
                                                                          UDF56 = 0,
                                                                          IsDeleted = 0,
                                                                          Remark = "管理区分全仕向",
                                                                          Creator = GetIdentityName(),
                                                                          CreateDate = DateTime.Now,
                                                                      }).Distinct().ToList();
                        DB.BulkInsert(New_result_P2dPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 4.采购件C003

                        //}
                        //if (this.IsManage.SelectedValue == "0")
                        //{
                        //    // Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课不需要管理！", "提示信息", MessageBoxIcon.Information);

                        //    #region 1.非采购件

                        //    //1.非采购件
                        //    var New_NonPurchase = from a in q_NotEollist
                        //                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                          where b.D_SAP_ZCA1D_Z010 == "E"
                        //                          select new
                        //                          {
                        //                              Ec_no = a.D_SAP_ZPABD_S001,
                        //                              Ec_model = c.D_SAP_DEST_Z002,
                        //                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                              Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                              Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                              Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                              Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                              Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                              Ec_newset = a.D_SAP_ZPABD_S011,
                        //                              Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                              Ec_change = a.D_SAP_ZPABD_S013,
                        //                              Ec_local = a.D_SAP_ZPABD_S014,
                        //                              Ec_note = a.D_SAP_ZPABD_S015,
                        //                              Ec_process = a.D_SAP_ZPABD_S016,

                        //                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                              Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                              Ec_eol = "",
                        //                              IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                          };

                        //    var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                        //                                           select new Pp_Ec_Sub
                        //                                           {
                        //                                               GUID = Guid.NewGuid(),
                        //                                               Ec_no = item.Ec_no,
                        //                                               Ec_model = item.Ec_model,
                        //                                               Ec_bomitem = item.Ec_bomitem,
                        //                                               Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                               Ec_olditem = item.Ec_olditem,
                        //                                               Ec_oldtext = item.Ec_oldtext,
                        //                                               Ec_oldqty = item.Ec_oldqty,
                        //                                               Ec_oldset = item.Ec_oldset,
                        //                                               Ec_newitem = item.Ec_newitem,
                        //                                               Ec_newtext = item.Ec_newtext,
                        //                                               Ec_newqty = item.Ec_newqty,
                        //                                               Ec_newset = item.Ec_newset,
                        //                                               Ec_bomno = item.Ec_bomno,
                        //                                               Ec_change = item.Ec_change,
                        //                                               Ec_local = item.Ec_local,
                        //                                               Ec_note = item.Ec_note,
                        //                                               Ec_process = item.Ec_process,
                        //                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                               Ec_procurement = item.Ec_procurement,
                        //                                               Ec_location = item.Ec_location,
                        //                                               IsCheck = item.IsCheck,
                        //                                               IsManage = 3,
                        //                                               Ec_eol = item.Ec_eol,

                        //                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                               Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_pmclot = "虚设件变更",
                        //                                               Ec_pmcmemo = "虚设件变更",
                        //                                               Ec_pmcnote = "虚设件变更",
                        //                                               Ec_bstock = 0,
                        //                                               pmcModifier = GetIdentityName(),
                        //                                               pmcModifyDate = DateTime.Now,

                        //                                               //    //制二
                        //                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_p2dlot = "与制二无关",
                        //                                               Ec_p2dnote = "与制二无关",
                        //                                               p2dModifier = GetIdentityName(),
                        //                                               p2dModifyDate = DateTime.Now,

                        //                                               //    //部管
                        //                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_mmlot = "与部管无关",
                        //                                               Ec_mmlotno = "与部管无关",
                        //                                               Ec_mmnote = "与部管无关",
                        //                                               mmModifier = GetIdentityName(),
                        //                                               mmModifyDate = DateTime.Now,

                        //                                               //    //采购
                        //                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_purorder = "4300000000",
                        //                                               Ec_pursupplier = "H200000",
                        //                                               Ec_purnote = "与采购无关",
                        //                                               ppModifier = GetIdentityName(),
                        //                                               ppModifyDate = DateTime.Now,

                        //                                               //    //受检
                        //                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_iqcorder = "4300000000",
                        //                                               Ec_iqcnote = "与受检无关",
                        //                                               iqcModifier = GetIdentityName(),
                        //                                               iqcModifyDate = DateTime.Now,

                        //                                               //    //制一
                        //                                               Ec_p1ddate = "",
                        //                                               Ec_p1dline = "",
                        //                                               Ec_p1dlot = "",
                        //                                               Ec_p1dnote = "",
                        //                                               p1dModifier = GetIdentityName(),
                        //                                               p1dModifyDate = DateTime.Now,

                        //                                               //    //品管
                        //                                               Ec_qadate = "",
                        //                                               Ec_qalot = "",
                        //                                               Ec_qanote = "",
                        //                                               qaModifier = GetIdentityName(),
                        //                                               qaModifyDate = DateTime.Now,

                        //                                               UDF01 = "NonPurchase",
                        //                                               UDF02 = "",
                        //                                               UDF03 = "",
                        //                                               UDF04 = "",
                        //                                               UDF05 = "",
                        //                                               UDF06 = "",
                        //                                               UDF51 = 0,
                        //                                               UDF52 = 0,
                        //                                               UDF53 = 0,
                        //                                               UDF54 = 0,
                        //                                               UDF55 = 0,
                        //                                               UDF56 = 0,
                        //                                               IsDeleted = 0,
                        //                                               Remark = "管理区分全仕向",
                        //                                               Creator = GetIdentityName(),
                        //                                               CreateDate = DateTime.Now,
                        //                                           }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 1.非采购件

                        //    #region 2.新物料为空

                        //    var NonItem = from a in q_NotEollist
                        //                  join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                        //                  join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                  where a.D_SAP_ZPABD_S008 == "0"
                        //                  //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                  //                                   where d.D_SAP_ZCA1D_Z034 == ""
                        //                  //                                   select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                        //                  //                                   select d.D_SAP_DEST_Z001)
                        //                  //                                 .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                        //                  //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                        //                  //                                    select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S008)
                        //                  where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                        //                  //where b.Ec_no == strecn
                        //                  //where a.Prodate == sdate//投入日期
                        //                  select new
                        //                  {
                        //                      Ec_no = a.D_SAP_ZPABD_S001,
                        //                      Ec_model = c.D_SAP_DEST_Z002,
                        //                      Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                      Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                      Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                      Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                      Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                      Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                      Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                      Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                      Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                      Ec_newset = a.D_SAP_ZPABD_S011,
                        //                      Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                      Ec_change = a.D_SAP_ZPABD_S013,
                        //                      Ec_local = a.D_SAP_ZPABD_S014,
                        //                      Ec_note = a.D_SAP_ZPABD_S015,
                        //                      Ec_process = a.D_SAP_ZPABD_S016,
                        //                      Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                      Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                      Ec_eol = "",
                        //                      IsCheck = "",
                        //                  };
                        //    var result_NonItem = NonItem.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                        //                                       select new Pp_Ec_Sub
                        //                                       {
                        //                                           GUID = Guid.NewGuid(),
                        //                                           Ec_no = item.Ec_no,
                        //                                           Ec_model = item.Ec_model,
                        //                                           Ec_bomitem = item.Ec_bomitem,
                        //                                           Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                           Ec_olditem = item.Ec_olditem,
                        //                                           Ec_oldtext = item.Ec_oldtext,
                        //                                           Ec_oldqty = item.Ec_oldqty,
                        //                                           Ec_oldset = item.Ec_oldset,
                        //                                           Ec_newitem = item.Ec_newitem,
                        //                                           Ec_newtext = item.Ec_newtext,
                        //                                           Ec_newqty = item.Ec_newqty,
                        //                                           Ec_newset = item.Ec_newset,
                        //                                           Ec_procurement = item.Ec_procurement,
                        //                                           Ec_location = item.Ec_location,
                        //                                           Ec_eol = item.Ec_eol,
                        //                                           IsCheck = item.IsCheck,
                        //                                           IsManage = IsManages,
                        //                                           Ec_bomno = item.Ec_bomno,
                        //                                           Ec_change = item.Ec_change,
                        //                                           Ec_local = item.Ec_local,
                        //                                           Ec_note = item.Ec_note,
                        //                                           Ec_process = item.Ec_process,
                        //                                           Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           //    //生管
                        //                                           Ec_pmcdate = "",
                        //                                           Ec_pmclot = "",
                        //                                           Ec_pmcmemo = "",
                        //                                           Ec_pmcnote = "",
                        //                                           Ec_bstock = 0,
                        //                                           pmcModifier = GetIdentityName(),
                        //                                           pmcModifyDate = DateTime.Now,
                        //                                           //    //部管
                        //                                           Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_mmlot = "自然切换",
                        //                                           Ec_mmlotno = "4400000",
                        //                                           Ec_mmnote = "自然切换",
                        //                                           mmModifier = GetIdentityName(),
                        //                                           mmModifyDate = DateTime.Now,
                        //                                           //    //采购
                        //                                           Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_purorder = "4300000000",
                        //                                           Ec_pursupplier = "H200000",
                        //                                           Ec_purnote = "自然切换",
                        //                                           ppModifier = GetIdentityName(),
                        //                                           ppModifyDate = DateTime.Now,
                        //                                           //    //受检
                        //                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_iqcorder = "4300000000",
                        //                                           Ec_iqcnote = "自然切换",
                        //                                           iqcModifier = GetIdentityName(),
                        //                                           iqcModifyDate = DateTime.Now,
                        //                                           //    //制一
                        //                                           Ec_p1ddate = "",
                        //                                           Ec_p1dline = "",
                        //                                           Ec_p1dlot = "",
                        //                                           Ec_p1dnote = "",
                        //                                           p1dModifier = GetIdentityName(),
                        //                                           p1dModifyDate = DateTime.Now,
                        //                                           //    //制二
                        //                                           Ec_p2ddate = "",
                        //                                           Ec_p2dlot = "",
                        //                                           Ec_p2dnote = "",
                        //                                           p2dModifier = GetIdentityName(),
                        //                                           p2dModifyDate = DateTime.Now,
                        //                                           //    //品管
                        //                                           Ec_qadate = "",
                        //                                           Ec_qalot = "",
                        //                                           Ec_qanote = "",
                        //                                           qaModifier = GetIdentityName(),
                        //                                           qaModifyDate = DateTime.Now,

                        //                                           UDF01 = "NonItems",
                        //                                           UDF02 = "",
                        //                                           UDF03 = "",
                        //                                           UDF04 = "",
                        //                                           UDF05 = "",
                        //                                           UDF06 = "",
                        //                                           UDF51 = 0,
                        //                                           UDF52 = 0,
                        //                                           UDF53 = 0,
                        //                                           UDF54 = 0,
                        //                                           UDF55 = 0,
                        //                                           UDF56 = 0,
                        //                                           IsDeleted = 0,
                        //                                           Remark = "管理区分全仕向",
                        //                                           Creator = GetIdentityName(),
                        //                                           CreateDate = DateTime.Now,
                        //                                       }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonItemList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 2.新物料为空

                        //    #region 3.采购件非C003

                        //    //1.采购件非C003

                        //    var MMPurchase = from a in q_NotEollist

                        //                     join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                     join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                     where b.D_SAP_ZCA1D_Z010 == "F"
                        //                     where b.D_SAP_ZCA1D_Z030 != "C003"
                        //                     select new
                        //                     {
                        //                         Ec_no = a.D_SAP_ZPABD_S001,
                        //                         Ec_model = c.D_SAP_DEST_Z002,
                        //                         Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                         Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                         Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                         Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                         Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                         Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                         Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                         Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                         Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                         Ec_newset = a.D_SAP_ZPABD_S011,
                        //                         Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                         Ec_change = a.D_SAP_ZPABD_S013,
                        //                         Ec_local = a.D_SAP_ZPABD_S014,
                        //                         Ec_note = a.D_SAP_ZPABD_S015,
                        //                         Ec_process = a.D_SAP_ZPABD_S016,

                        //                         Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                         Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                         Ec_eol = "",
                        //                         IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                     };

                        //    var result_MMPurchase = MMPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                        //                                          select new Pp_Ec_Sub
                        //                                          {
                        //                                              GUID = Guid.NewGuid(),
                        //                                              Ec_no = item.Ec_no,
                        //                                              Ec_model = item.Ec_model,
                        //                                              Ec_bomitem = item.Ec_bomitem,
                        //                                              Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                              Ec_olditem = item.Ec_olditem,
                        //                                              Ec_oldtext = item.Ec_oldtext,
                        //                                              Ec_oldqty = item.Ec_oldqty,
                        //                                              Ec_oldset = item.Ec_oldset,
                        //                                              Ec_newitem = item.Ec_newitem,
                        //                                              Ec_newtext = item.Ec_newtext,
                        //                                              Ec_newqty = item.Ec_newqty,
                        //                                              Ec_newset = item.Ec_newset,
                        //                                              Ec_bomno = item.Ec_bomno,
                        //                                              Ec_change = item.Ec_change,
                        //                                              Ec_local = item.Ec_local,
                        //                                              Ec_note = item.Ec_note,
                        //                                              Ec_process = item.Ec_process,
                        //                                              Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_procurement = item.Ec_procurement,
                        //                                              Ec_location = item.Ec_location,
                        //                                              IsCheck = item.IsCheck,
                        //                                              IsManage = 2,
                        //                                              Ec_eol = item.Ec_eol,

                        //                                              Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_pmcdate = "",
                        //                                              Ec_pmclot = "",
                        //                                              Ec_pmcmemo = "",
                        //                                              Ec_pmcnote = "",
                        //                                              Ec_bstock = 0,
                        //                                              pmcModifier = GetIdentityName(),
                        //                                              pmcModifyDate = DateTime.Now,

                        //                                              //    //制二
                        //                                              Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_p2dlot = "与制二无关",
                        //                                              Ec_p2dnote = "与制二无关",
                        //                                              p2dModifier = GetIdentityName(),
                        //                                              p2dModifyDate = DateTime.Now,

                        //                                              //    //部管
                        //                                              Ec_mmdate = "",
                        //                                              Ec_mmlot = "",
                        //                                              Ec_mmlotno = "",
                        //                                              Ec_mmnote = "",
                        //                                              mmModifier = GetIdentityName(),
                        //                                              mmModifyDate = DateTime.Now,

                        //                                              //    //采购
                        //                                              Ec_purdate = "",
                        //                                              Ec_purorder = "",
                        //                                              Ec_pursupplier = "",
                        //                                              Ec_purnote = "",
                        //                                              ppModifier = GetIdentityName(),
                        //                                              ppModifyDate = DateTime.Now,

                        //                                              //    //受检
                        //                                              Ec_iqcdate = "",
                        //                                              Ec_iqcorder = "",
                        //                                              Ec_iqcnote = "",
                        //                                              iqcModifier = GetIdentityName(),
                        //                                              iqcModifyDate = DateTime.Now,

                        //                                              //    //制一
                        //                                              Ec_p1ddate = "",
                        //                                              Ec_p1dline = "",
                        //                                              Ec_p1dlot = "",
                        //                                              Ec_p1dnote = "",
                        //                                              p1dModifier = GetIdentityName(),
                        //                                              p1dModifyDate = DateTime.Now,

                        //                                              //    //品管
                        //                                              Ec_qadate = "",
                        //                                              Ec_qalot = "",
                        //                                              Ec_qanote = "",
                        //                                              qaModifier = GetIdentityName(),
                        //                                              qaModifyDate = DateTime.Now,

                        //                                              UDF01 = "MMPurchase",
                        //                                              UDF02 = "",
                        //                                              UDF03 = "",
                        //                                              UDF04 = "",
                        //                                              UDF05 = "",
                        //                                              UDF06 = "",
                        //                                              UDF51 = 0,
                        //                                              UDF52 = 0,
                        //                                              UDF53 = 0,
                        //                                              UDF54 = 0,
                        //                                              UDF55 = 0,
                        //                                              UDF56 = 0,
                        //                                              IsDeleted = 0,
                        //                                              Remark = "管理区分全仕向",
                        //                                              Creator = GetIdentityName(),
                        //                                              CreateDate = DateTime.Now,
                        //                                          }).Distinct().ToList();
                        //    DB.BulkInsert(New_MMPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 3.采购件非C003

                        //    #region 4.采购件C003

                        //    //1.采购件C003
                        //    //1.采购件非C003

                        //    var P2dPurchase = from a in q_NotEollist

                        //                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                      where b.D_SAP_ZCA1D_Z010 == "F"
                        //                      where b.D_SAP_ZCA1D_Z030 == "C003"
                        //                      select new
                        //                      {
                        //                          Ec_no = a.D_SAP_ZPABD_S001,
                        //                          Ec_model = c.D_SAP_DEST_Z002,
                        //                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                          Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                          Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                          Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                          Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                          Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                          Ec_newset = a.D_SAP_ZPABD_S011,
                        //                          Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                          Ec_change = a.D_SAP_ZPABD_S013,
                        //                          Ec_local = a.D_SAP_ZPABD_S014,
                        //                          Ec_note = a.D_SAP_ZPABD_S015,
                        //                          Ec_process = a.D_SAP_ZPABD_S016,

                        //                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                          Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                          Ec_eol = "",
                        //                          IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                      };

                        //    var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                        //                                                  select new Pp_Ec_Sub
                        //                                                  {
                        //                                                      GUID = Guid.NewGuid(),
                        //                                                      Ec_no = item.Ec_no,
                        //                                                      Ec_model = item.Ec_model,
                        //                                                      Ec_bomitem = item.Ec_bomitem,
                        //                                                      Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                                      Ec_olditem = item.Ec_olditem,
                        //                                                      Ec_oldtext = item.Ec_oldtext,
                        //                                                      Ec_oldqty = item.Ec_oldqty,
                        //                                                      Ec_oldset = item.Ec_oldset,
                        //                                                      Ec_newitem = item.Ec_newitem,
                        //                                                      Ec_newtext = item.Ec_newtext,
                        //                                                      Ec_newqty = item.Ec_newqty,
                        //                                                      Ec_newset = item.Ec_newset,
                        //                                                      Ec_bomno = item.Ec_bomno,
                        //                                                      Ec_change = item.Ec_change,
                        //                                                      Ec_local = item.Ec_local,
                        //                                                      Ec_note = item.Ec_note,
                        //                                                      Ec_process = item.Ec_process,
                        //                                                      Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_procurement = item.Ec_procurement,
                        //                                                      Ec_location = item.Ec_location,
                        //                                                      IsCheck = item.IsCheck,
                        //                                                      IsManage = IsManages,
                        //                                                      Ec_eol = item.Ec_eol,

                        //                                                      Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_pmclot = "自然切换",
                        //                                                      Ec_pmcmemo = "自然切换",
                        //                                                      Ec_pmcnote = "自然切换",
                        //                                                      Ec_bstock = 0,
                        //                                                      pmcModifier = GetIdentityName(),
                        //                                                      pmcModifyDate = DateTime.Now,

                        //                                                      //    //制二
                        //                                                      Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_p2dlot = "自然切换",
                        //                                                      Ec_p2dnote = "自然切换",
                        //                                                      p2dModifier = GetIdentityName(),
                        //                                                      p2dModifyDate = DateTime.Now,

                        //                                                      //    //部管
                        //                                                      Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_mmlot = "自然切换",
                        //                                                      Ec_mmlotno = "自然切换",
                        //                                                      Ec_mmnote = "自然切换",
                        //                                                      mmModifier = GetIdentityName(),
                        //                                                      mmModifyDate = DateTime.Now,

                        //                                                      //    //采购
                        //                                                      Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_purorder = "4300000000",
                        //                                                      Ec_pursupplier = "H200000",
                        //                                                      Ec_purnote = "自然切换",
                        //                                                      ppModifier = GetIdentityName(),
                        //                                                      ppModifyDate = DateTime.Now,

                        //                                                      //    //受检
                        //                                                      Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_iqcorder = "4300000000",
                        //                                                      Ec_iqcnote = "自然切换",
                        //                                                      iqcModifier = GetIdentityName(),
                        //                                                      iqcModifyDate = DateTime.Now,

                        //                                                      //    //制一
                        //                                                      Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_p1dline = "自然切换",
                        //                                                      Ec_p1dlot = "自然切换",
                        //                                                      Ec_p1dnote = "自然切换",
                        //                                                      p1dModifier = GetIdentityName(),
                        //                                                      p1dModifyDate = DateTime.Now,

                        //                                                      //    //品管
                        //                                                      Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_qalot = "自然切换",
                        //                                                      Ec_qanote = "自然切换",
                        //                                                      qaModifier = GetIdentityName(),
                        //                                                      qaModifyDate = DateTime.Now,

                        //                                                      UDF01 = "P2DPurchase",
                        //                                                      UDF02 = "",
                        //                                                      UDF03 = "",
                        //                                                      UDF04 = "",
                        //                                                      UDF05 = "",
                        //                                                      UDF06 = "",
                        //                                                      UDF51 = 0,
                        //                                                      UDF52 = 0,
                        //                                                      UDF53 = 0,
                        //                                                      UDF54 = 0,
                        //                                                      UDF55 = 0,
                        //                                                      UDF56 = 0,
                        //                                                      IsDeleted = 0,
                        //                                                      Remark = "管理区分全仕向",
                        //                                                      Creator = GetIdentityName(),
                        //                                                      CreateDate = DateTime.Now,
                        //                                                  }).Distinct().ToList();
                        //    DB.BulkInsert(New_result_P2dPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 4.采购件C003
                        //}
                    }
                    if (this.Ec_distinction.SelectedValue == "2")//部管
                    {
                        //if (this.IsManage.SelectedValue == "1")
                        //{

                        #region 1.非采购件

                        //1.非采购件
                        var New_NonPurchase = from a in q_NotEollist
                                              join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                              join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                              where b.D_SAP_ZCA1D_Z010 == "E"
                                              select new
                                              {
                                                  Ec_no = a.D_SAP_ZPABD_S001,
                                                  Ec_model = c.D_SAP_DEST_Z002,
                                                  Ec_bomitem = a.D_SAP_ZPABD_S002,
                                                  Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                                  Ec_olditem = a.D_SAP_ZPABD_S004,
                                                  Ec_oldtext = a.D_SAP_ZPABD_S005,
                                                  Ec_oldqty = a.D_SAP_ZPABD_S006,
                                                  Ec_oldset = a.D_SAP_ZPABD_S007,
                                                  Ec_newitem = a.D_SAP_ZPABD_S008,
                                                  Ec_newtext = a.D_SAP_ZPABD_S009,
                                                  Ec_newqty = a.D_SAP_ZPABD_S010,
                                                  Ec_newset = a.D_SAP_ZPABD_S011,
                                                  Ec_bomno = a.D_SAP_ZPABD_S012,
                                                  Ec_change = a.D_SAP_ZPABD_S013,
                                                  Ec_local = a.D_SAP_ZPABD_S014,
                                                  Ec_note = a.D_SAP_ZPABD_S015,
                                                  Ec_process = a.D_SAP_ZPABD_S016,

                                                  Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                                  Ec_location = b.D_SAP_ZCA1D_Z030,
                                                  Ec_eol = "",
                                                  IsCheck = b.D_SAP_ZCA1D_Z019,
                                              };

                        var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                                                               select new Pp_Ec_Sub
                                                               {
                                                                   GUID = Guid.NewGuid(),
                                                                   Ec_no = item.Ec_no,
                                                                   Ec_model = item.Ec_model,
                                                                   Ec_bomitem = item.Ec_bomitem,
                                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                                   Ec_olditem = item.Ec_olditem,
                                                                   Ec_oldtext = item.Ec_oldtext,
                                                                   Ec_oldqty = item.Ec_oldqty,
                                                                   Ec_oldset = item.Ec_oldset,
                                                                   Ec_newitem = item.Ec_newitem,
                                                                   Ec_newtext = item.Ec_newtext,
                                                                   Ec_newqty = item.Ec_newqty,
                                                                   Ec_newset = item.Ec_newset,
                                                                   Ec_bomno = item.Ec_bomno,
                                                                   Ec_change = item.Ec_change,
                                                                   Ec_local = item.Ec_local,
                                                                   Ec_note = item.Ec_note,
                                                                   Ec_process = item.Ec_process,
                                                                   Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_procurement = item.Ec_procurement,
                                                                   Ec_location = item.Ec_location,
                                                                   IsCheck = item.IsCheck,
                                                                   IsManage = 1,
                                                                   IsMmManage = 1,
                                                                   IsAssyManage = 0,
                                                                   IsPcbaManage = 0,
                                                                   Ec_eol = item.Ec_eol,

                                                                   Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_pmclot = "虚设件变更",
                                                                   Ec_pmcmemo = "虚设件变更",
                                                                   Ec_pmcnote = "虚设件变更",
                                                                   Ec_bstock = 0,
                                                                   pmcModifier = GetIdentityName(),
                                                                   pmcModifyDate = DateTime.Now,

                                                                   //    //制二
                                                                   Ec_p2ddate = "",
                                                                   Ec_p2dlot = "",
                                                                   Ec_p2dnote = "",
                                                                   p2dModifier = GetIdentityName(),
                                                                   p2dModifyDate = DateTime.Now,

                                                                   //    //部管
                                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_mmlot = "与部管无关",
                                                                   Ec_mmlotno = "与部管无关",
                                                                   Ec_mmnote = "与部管无关",
                                                                   mmModifier = GetIdentityName(),
                                                                   mmModifyDate = DateTime.Now,

                                                                   //    //采购
                                                                   Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_purorder = "4300000000",
                                                                   Ec_pursupplier = "H200000",
                                                                   Ec_purnote = "与采购无关",
                                                                   ppModifier = GetIdentityName(),
                                                                   ppModifyDate = DateTime.Now,

                                                                   //    //受检
                                                                   Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_iqcorder = "4300000000",
                                                                   Ec_iqcnote = "与受检无关",
                                                                   iqcModifier = GetIdentityName(),
                                                                   iqcModifyDate = DateTime.Now,

                                                                   //    //制一
                                                                   Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_p1dline = "到部管为止",
                                                                   Ec_p1dlot = "到部管为止",
                                                                   Ec_p1dnote = "到部管为止",
                                                                   p1dModifier = GetIdentityName(),
                                                                   p1dModifyDate = DateTime.Now,

                                                                   //    //品管
                                                                   Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_qalot = "到部管为止",
                                                                   Ec_qanote = "到部管为止",
                                                                   qaModifier = GetIdentityName(),
                                                                   qaModifyDate = DateTime.Now,

                                                                   UDF01 = "NonPurchase",
                                                                   UDF02 = "",
                                                                   UDF03 = "",
                                                                   UDF04 = "",
                                                                   UDF05 = "",
                                                                   UDF06 = "",
                                                                   UDF51 = 0,
                                                                   UDF52 = 0,
                                                                   UDF53 = 0,
                                                                   UDF54 = 0,
                                                                   UDF55 = 0,
                                                                   UDF56 = 0,
                                                                   IsDeleted = 0,
                                                                   Remark = "管理区分部管课",
                                                                   Creator = GetIdentityName(),
                                                                   CreateDate = DateTime.Now,
                                                               }).Distinct().ToList();
                        DB.BulkInsert(New_NonPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 1.非采购件

                        #region 2.新物料为空

                        var NonItem = from a in q_NotEollist
                                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                      where a.D_SAP_ZPABD_S008 == "0"
                                      //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                      //                                   where d.D_SAP_ZCA1D_Z034 == ""
                                      //                                   select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                      //                                   select d.D_SAP_DEST_Z001)
                                      //                                 .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                                      //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                                      //                                    select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S008)
                                      where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                                      //where b.Ec_no == strecn
                                      //where a.Prodate == sdate//投入日期
                                      select new
                                      {
                                          Ec_no = a.D_SAP_ZPABD_S001,
                                          Ec_model = c.D_SAP_DEST_Z002,
                                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                          Ec_olditem = a.D_SAP_ZPABD_S004,
                                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                                          Ec_oldset = a.D_SAP_ZPABD_S007,
                                          Ec_newitem = a.D_SAP_ZPABD_S008,
                                          Ec_newtext = a.D_SAP_ZPABD_S009,
                                          Ec_newqty = a.D_SAP_ZPABD_S010,
                                          Ec_newset = a.D_SAP_ZPABD_S011,
                                          Ec_bomno = a.D_SAP_ZPABD_S012,
                                          Ec_change = a.D_SAP_ZPABD_S013,
                                          Ec_local = a.D_SAP_ZPABD_S014,
                                          Ec_note = a.D_SAP_ZPABD_S015,
                                          Ec_process = a.D_SAP_ZPABD_S016,
                                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                          Ec_location = b.D_SAP_ZCA1D_Z030,
                                          Ec_eol = "",
                                          IsCheck = "",
                                      };
                        var result_NonItem = NonItem.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = Guid.NewGuid(),
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               Ec_eol = item.Ec_eol,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = 1,
                                                               IsMmManage = 1,
                                                               IsAssyManage = 0,
                                                               IsPcbaManage = 0,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                                                               //    //生管
                                                               Ec_pmcdate = "",
                                                               Ec_pmclot = "",
                                                               Ec_pmcmemo = "",
                                                               Ec_pmcnote = "",
                                                               Ec_bstock = 0,
                                                               pmcModifier = GetIdentityName(),
                                                               pmcModifyDate = DateTime.Now,
                                                               //    //部管
                                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_mmlot = "到部管为止",
                                                               Ec_mmlotno = "4400000",
                                                               Ec_mmnote = "到部管为止",
                                                               mmModifier = GetIdentityName(),
                                                               mmModifyDate = DateTime.Now,
                                                               //    //采购
                                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_purorder = "4300000000",
                                                               Ec_pursupplier = "H200000",
                                                               Ec_purnote = "到部管为止",
                                                               ppModifier = GetIdentityName(),
                                                               ppModifyDate = DateTime.Now,
                                                               //    //受检
                                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_iqcorder = "4300000000",
                                                               Ec_iqcnote = "到部管为止",
                                                               iqcModifier = GetIdentityName(),
                                                               iqcModifyDate = DateTime.Now,
                                                               //    //制一
                                                               Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_p1dline = "到部管为止",
                                                               Ec_p1dlot = "到部管为止",
                                                               Ec_p1dnote = "到部管为止",
                                                               p1dModifier = GetIdentityName(),
                                                               p1dModifyDate = DateTime.Now,
                                                               //    //制二
                                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_p2dlot = "到部管为止",
                                                               Ec_p2dnote = "到部管为止",
                                                               p2dModifier = GetIdentityName(),
                                                               p2dModifyDate = DateTime.Now,
                                                               //    //品管
                                                               Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_qalot = "到部管为止",
                                                               Ec_qanote = "到部管为止",
                                                               qaModifier = GetIdentityName(),
                                                               qaModifyDate = DateTime.Now,

                                                               UDF01 = "NonItems",
                                                               UDF02 = "",
                                                               UDF03 = "",
                                                               UDF04 = "",
                                                               UDF05 = "",
                                                               UDF06 = "",
                                                               UDF51 = 0,
                                                               UDF52 = 0,
                                                               UDF53 = 0,
                                                               UDF54 = 0,
                                                               UDF55 = 0,
                                                               UDF56 = 0,
                                                               IsDeleted = 0,
                                                               Remark = "管理区分部管课",
                                                               Creator = GetIdentityName(),
                                                               CreateDate = DateTime.Now,
                                                           }).Distinct().ToList();
                        DB.BulkInsert(New_NonItemList);
                        DB.BulkSaveChanges();

                        #endregion 2.新物料为空

                        #region 3.采购件非C003

                        //1.采购件非C003

                        var MMPurchase = from a in q_NotEollist

                                         join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                         join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                         where b.D_SAP_ZCA1D_Z010 == "F"
                                         where b.D_SAP_ZCA1D_Z030 != "C003"
                                         select new
                                         {
                                             Ec_no = a.D_SAP_ZPABD_S001,
                                             Ec_model = c.D_SAP_DEST_Z002,
                                             Ec_bomitem = a.D_SAP_ZPABD_S002,
                                             Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                             Ec_olditem = a.D_SAP_ZPABD_S004,
                                             Ec_oldtext = a.D_SAP_ZPABD_S005,
                                             Ec_oldqty = a.D_SAP_ZPABD_S006,
                                             Ec_oldset = a.D_SAP_ZPABD_S007,
                                             Ec_newitem = a.D_SAP_ZPABD_S008,
                                             Ec_newtext = a.D_SAP_ZPABD_S009,
                                             Ec_newqty = a.D_SAP_ZPABD_S010,
                                             Ec_newset = a.D_SAP_ZPABD_S011,
                                             Ec_bomno = a.D_SAP_ZPABD_S012,
                                             Ec_change = a.D_SAP_ZPABD_S013,
                                             Ec_local = a.D_SAP_ZPABD_S014,
                                             Ec_note = a.D_SAP_ZPABD_S015,
                                             Ec_process = a.D_SAP_ZPABD_S016,

                                             Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                             Ec_location = b.D_SAP_ZCA1D_Z030,
                                             Ec_eol = "",
                                             IsCheck = b.D_SAP_ZCA1D_Z019,
                                         };

                        var result_MMPurchase = MMPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                                                              select new Pp_Ec_Sub
                                                              {
                                                                  GUID = Guid.NewGuid(),
                                                                  Ec_no = item.Ec_no,
                                                                  Ec_model = item.Ec_model,
                                                                  Ec_bomitem = item.Ec_bomitem,
                                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                                  Ec_olditem = item.Ec_olditem,
                                                                  Ec_oldtext = item.Ec_oldtext,
                                                                  Ec_oldqty = item.Ec_oldqty,
                                                                  Ec_oldset = item.Ec_oldset,
                                                                  Ec_newitem = item.Ec_newitem,
                                                                  Ec_newtext = item.Ec_newtext,
                                                                  Ec_newqty = item.Ec_newqty,
                                                                  Ec_newset = item.Ec_newset,
                                                                  Ec_bomno = item.Ec_bomno,
                                                                  Ec_change = item.Ec_change,
                                                                  Ec_local = item.Ec_local,
                                                                  Ec_note = item.Ec_note,
                                                                  Ec_process = item.Ec_process,
                                                                  Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_procurement = item.Ec_procurement,
                                                                  Ec_location = item.Ec_location,
                                                                  IsCheck = item.IsCheck,
                                                                  IsManage = 1,
                                                                  IsMmManage = 1,
                                                                  IsAssyManage = 0,
                                                                  IsPcbaManage = 0,
                                                                  Ec_eol = item.Ec_eol,

                                                                  Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_pmcdate = "",
                                                                  Ec_pmclot = "",
                                                                  Ec_pmcmemo = "",
                                                                  Ec_pmcnote = "",
                                                                  Ec_bstock = 0,
                                                                  pmcModifier = GetIdentityName(),
                                                                  pmcModifyDate = DateTime.Now,

                                                                  //    //制二
                                                                  Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p2dlot = "与制二无关",
                                                                  Ec_p2dnote = "与制二无关",
                                                                  p2dModifier = GetIdentityName(),
                                                                  p2dModifyDate = DateTime.Now,

                                                                  //    //部管
                                                                  Ec_mmdate = "",
                                                                  Ec_mmlot = "",
                                                                  Ec_mmlotno = "",
                                                                  Ec_mmnote = "",
                                                                  mmModifier = GetIdentityName(),
                                                                  mmModifyDate = DateTime.Now,

                                                                  //    //采购
                                                                  Ec_purdate = "",
                                                                  Ec_purorder = "",
                                                                  Ec_pursupplier = "",
                                                                  Ec_purnote = "",
                                                                  ppModifier = GetIdentityName(),
                                                                  ppModifyDate = DateTime.Now,

                                                                  //    //受检
                                                                  Ec_iqcdate = "",
                                                                  Ec_iqcorder = "",
                                                                  Ec_iqcnote = "",
                                                                  iqcModifier = GetIdentityName(),
                                                                  iqcModifyDate = DateTime.Now,

                                                                  //    //制一
                                                                  Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p1dline = "到部管为止",
                                                                  Ec_p1dlot = "到部管为止",
                                                                  Ec_p1dnote = "到部管为止",
                                                                  p1dModifier = GetIdentityName(),
                                                                  p1dModifyDate = DateTime.Now,

                                                                  //    //品管
                                                                  Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_qalot = "到部管为止",
                                                                  Ec_qanote = "到部管为止",
                                                                  qaModifier = GetIdentityName(),
                                                                  qaModifyDate = DateTime.Now,

                                                                  UDF01 = "MMPurchase",
                                                                  UDF02 = "",
                                                                  UDF03 = "",
                                                                  UDF04 = "",
                                                                  UDF05 = "",
                                                                  UDF06 = "",
                                                                  UDF51 = 0,
                                                                  UDF52 = 0,
                                                                  UDF53 = 0,
                                                                  UDF54 = 0,
                                                                  UDF55 = 0,
                                                                  UDF56 = 0,
                                                                  IsDeleted = 0,
                                                                  Remark = "管理区分部管课",
                                                                  Creator = GetIdentityName(),
                                                                  CreateDate = DateTime.Now,
                                                              }).Distinct().ToList();
                        DB.BulkInsert(New_MMPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 3.采购件非C003

                        #region 4.采购件C003

                        //1.采购件C003
                        //1.采购件非C003

                        var P2dPurchase = from a in q_NotEollist

                                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                          where b.D_SAP_ZCA1D_Z010 == "F"
                                          where b.D_SAP_ZCA1D_Z030 == "C003"
                                          select new
                                          {
                                              Ec_no = a.D_SAP_ZPABD_S001,
                                              Ec_model = c.D_SAP_DEST_Z002,
                                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                              Ec_olditem = a.D_SAP_ZPABD_S004,
                                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                                              Ec_oldset = a.D_SAP_ZPABD_S007,
                                              Ec_newitem = a.D_SAP_ZPABD_S008,
                                              Ec_newtext = a.D_SAP_ZPABD_S009,
                                              Ec_newqty = a.D_SAP_ZPABD_S010,
                                              Ec_newset = a.D_SAP_ZPABD_S011,
                                              Ec_bomno = a.D_SAP_ZPABD_S012,
                                              Ec_change = a.D_SAP_ZPABD_S013,
                                              Ec_local = a.D_SAP_ZPABD_S014,
                                              Ec_note = a.D_SAP_ZPABD_S015,
                                              Ec_process = a.D_SAP_ZPABD_S016,

                                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                              Ec_location = b.D_SAP_ZCA1D_Z030,
                                              Ec_eol = "",
                                              IsCheck = b.D_SAP_ZCA1D_Z019,
                                          };

                        var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                                                                      select new Pp_Ec_Sub
                                                                      {
                                                                          GUID = Guid.NewGuid(),
                                                                          Ec_no = item.Ec_no,
                                                                          Ec_model = item.Ec_model,
                                                                          Ec_bomitem = item.Ec_bomitem,
                                                                          Ec_bomsubitem = item.Ec_bomsubitem,
                                                                          Ec_olditem = item.Ec_olditem,
                                                                          Ec_oldtext = item.Ec_oldtext,
                                                                          Ec_oldqty = item.Ec_oldqty,
                                                                          Ec_oldset = item.Ec_oldset,
                                                                          Ec_newitem = item.Ec_newitem,
                                                                          Ec_newtext = item.Ec_newtext,
                                                                          Ec_newqty = item.Ec_newqty,
                                                                          Ec_newset = item.Ec_newset,
                                                                          Ec_bomno = item.Ec_bomno,
                                                                          Ec_change = item.Ec_change,
                                                                          Ec_local = item.Ec_local,
                                                                          Ec_note = item.Ec_note,
                                                                          Ec_process = item.Ec_process,
                                                                          Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_procurement = item.Ec_procurement,
                                                                          Ec_location = item.Ec_location,
                                                                          IsCheck = item.IsCheck,
                                                                          IsManage = 1,
                                                                          IsMmManage = 0,
                                                                          IsAssyManage = 0,
                                                                          IsPcbaManage = 1,
                                                                          Ec_eol = item.Ec_eol,

                                                                          Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_pmcdate = "",
                                                                          Ec_pmclot = "",
                                                                          Ec_pmcmemo = "",
                                                                          Ec_pmcnote = "",
                                                                          Ec_bstock = 0,
                                                                          pmcModifier = GetIdentityName(),
                                                                          pmcModifyDate = DateTime.Now,

                                                                          //    //制二
                                                                          Ec_p2ddate = "",
                                                                          Ec_p2dlot = "",
                                                                          Ec_p2dnote = "",
                                                                          p2dModifier = GetIdentityName(),
                                                                          p2dModifyDate = DateTime.Now,

                                                                          //    //部管
                                                                          Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_mmlot = "与部管无关",
                                                                          Ec_mmlotno = "与部管无关",
                                                                          Ec_mmnote = "与部管无关",
                                                                          mmModifier = GetIdentityName(),
                                                                          mmModifyDate = DateTime.Now,

                                                                          //    //采购
                                                                          Ec_purdate = "",
                                                                          Ec_purorder = "",
                                                                          Ec_pursupplier = "",
                                                                          Ec_purnote = "",
                                                                          ppModifier = GetIdentityName(),
                                                                          ppModifyDate = DateTime.Now,

                                                                          //    //受检
                                                                          Ec_iqcdate = "",
                                                                          Ec_iqcorder = "",
                                                                          Ec_iqcnote = "",
                                                                          iqcModifier = GetIdentityName(),
                                                                          iqcModifyDate = DateTime.Now,

                                                                          //    //制一
                                                                          Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_p1dline = "到部管为止",
                                                                          Ec_p1dlot = "到部管为止",
                                                                          Ec_p1dnote = "到部管为止",
                                                                          p1dModifier = GetIdentityName(),
                                                                          p1dModifyDate = DateTime.Now,

                                                                          //    //品管
                                                                          Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_qalot = "到部管为止",
                                                                          Ec_qanote = "到部管为止",
                                                                          qaModifier = GetIdentityName(),
                                                                          qaModifyDate = DateTime.Now,

                                                                          UDF01 = "P2DPurchase",
                                                                          UDF02 = "",
                                                                          UDF03 = "",
                                                                          UDF04 = "",
                                                                          UDF05 = "",
                                                                          UDF06 = "",
                                                                          UDF51 = 0,
                                                                          UDF52 = 0,
                                                                          UDF53 = 0,
                                                                          UDF54 = 0,
                                                                          UDF55 = 0,
                                                                          UDF56 = 0,
                                                                          IsDeleted = 0,
                                                                          Remark = "管理区分部管课",
                                                                          Creator = GetIdentityName(),
                                                                          CreateDate = DateTime.Now,
                                                                      }).Distinct().ToList();
                        DB.BulkInsert(New_result_P2dPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 4.采购件C003

                        //}
                        //if (this.IsManage.SelectedValue == "0")
                        //{
                        //    #region 1.非采购件

                        //    //1.非采购件
                        //    var New_NonPurchase = from a in q_NotEollist
                        //                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                          where b.D_SAP_ZCA1D_Z010 == "E"
                        //                          select new
                        //                          {
                        //                              Ec_no = a.D_SAP_ZPABD_S001,
                        //                              Ec_model = c.D_SAP_DEST_Z002,
                        //                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                              Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                              Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                              Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                              Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                              Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                              Ec_newset = a.D_SAP_ZPABD_S011,
                        //                              Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                              Ec_change = a.D_SAP_ZPABD_S013,
                        //                              Ec_local = a.D_SAP_ZPABD_S014,
                        //                              Ec_note = a.D_SAP_ZPABD_S015,
                        //                              Ec_process = a.D_SAP_ZPABD_S016,

                        //                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                              Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                              Ec_eol = "",
                        //                              IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                          };

                        //    var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                        //                                           select new Pp_Ec_Sub
                        //                                           {
                        //                                               GUID = Guid.NewGuid(),
                        //                                               Ec_no = item.Ec_no,
                        //                                               Ec_model = item.Ec_model,
                        //                                               Ec_bomitem = item.Ec_bomitem,
                        //                                               Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                               Ec_olditem = item.Ec_olditem,
                        //                                               Ec_oldtext = item.Ec_oldtext,
                        //                                               Ec_oldqty = item.Ec_oldqty,
                        //                                               Ec_oldset = item.Ec_oldset,
                        //                                               Ec_newitem = item.Ec_newitem,
                        //                                               Ec_newtext = item.Ec_newtext,
                        //                                               Ec_newqty = item.Ec_newqty,
                        //                                               Ec_newset = item.Ec_newset,
                        //                                               Ec_bomno = item.Ec_bomno,
                        //                                               Ec_change = item.Ec_change,
                        //                                               Ec_local = item.Ec_local,
                        //                                               Ec_note = item.Ec_note,
                        //                                               Ec_process = item.Ec_process,
                        //                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                               Ec_procurement = item.Ec_procurement,
                        //                                               Ec_location = item.Ec_location,
                        //                                               IsCheck = item.IsCheck,
                        //                                               IsManage = IsManages,
                        //                                               Ec_eol = item.Ec_eol,

                        //                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_pmclot = "虚设件变更",
                        //                                               Ec_pmcmemo = "虚设件变更",
                        //                                               Ec_pmcnote = "虚设件变更",
                        //                                               Ec_bstock = 0,
                        //                                               pmcModifier = GetIdentityName(),
                        //                                               pmcModifyDate = DateTime.Now,

                        //                                               //    //制二
                        //                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_p2dlot = "到部管为止",
                        //                                               Ec_p2dnote = "到部管为止",
                        //                                               p2dModifier = GetIdentityName(),
                        //                                               p2dModifyDate = DateTime.Now,

                        //                                               //    //部管
                        //                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_mmlot = "到部管为止",
                        //                                               Ec_mmlotno = "到部管为止",
                        //                                               Ec_mmnote = "到部管为止",
                        //                                               mmModifier = GetIdentityName(),
                        //                                               mmModifyDate = DateTime.Now,

                        //                                               //    //采购
                        //                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_purorder = "4300000000",
                        //                                               Ec_pursupplier = "H200000",
                        //                                               Ec_purnote = "自然切换",
                        //                                               ppModifier = GetIdentityName(),
                        //                                               ppModifyDate = DateTime.Now,

                        //                                               //    //受检
                        //                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_iqcorder = "4300000000",
                        //                                               Ec_iqcnote = "自然切换",
                        //                                               iqcModifier = GetIdentityName(),
                        //                                               iqcModifyDate = DateTime.Now,

                        //                                               //    //制一
                        //                                               Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_p1dline = "到部管为止",
                        //                                               Ec_p1dlot = "到部管为止",
                        //                                               Ec_p1dnote = "到部管为止",
                        //                                               p1dModifier = GetIdentityName(),
                        //                                               p1dModifyDate = DateTime.Now,

                        //                                               //    //品管
                        //                                               Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_qalot = "到部管为止",
                        //                                               Ec_qanote = "到部管为止",
                        //                                               qaModifier = GetIdentityName(),
                        //                                               qaModifyDate = DateTime.Now,

                        //                                               UDF01 = "NonPurchase",
                        //                                               UDF02 = "",
                        //                                               UDF03 = "",
                        //                                               UDF04 = "",
                        //                                               UDF05 = "",
                        //                                               UDF06 = "",
                        //                                               UDF51 = 0,
                        //                                               UDF52 = 0,
                        //                                               UDF53 = 0,
                        //                                               UDF54 = 0,
                        //                                               UDF55 = 0,
                        //                                               UDF56 = 0,
                        //                                               IsDeleted = 0,
                        //                                               Remark = "管理区分部管课",
                        //                                               Creator = GetIdentityName(),
                        //                                               CreateDate = DateTime.Now,
                        //                                           }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 1.非采购件

                        //    #region 2.新物料为空

                        //    var NonItem = from a in q_NotEollist
                        //                  join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                        //                  join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                  where a.D_SAP_ZPABD_S008 == "0"
                        //                  //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                  //                                   where d.D_SAP_ZCA1D_Z034 == ""
                        //                  //                                   select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                        //                  //                                   select d.D_SAP_DEST_Z001)
                        //                  //                                 .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                        //                  //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                        //                  //                                    select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S008)
                        //                  where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                        //                  //where b.Ec_no == strecn
                        //                  //where a.Prodate == sdate//投入日期
                        //                  select new
                        //                  {
                        //                      Ec_no = a.D_SAP_ZPABD_S001,
                        //                      Ec_model = c.D_SAP_DEST_Z002,
                        //                      Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                      Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                      Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                      Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                      Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                      Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                      Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                      Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                      Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                      Ec_newset = a.D_SAP_ZPABD_S011,
                        //                      Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                      Ec_change = a.D_SAP_ZPABD_S013,
                        //                      Ec_local = a.D_SAP_ZPABD_S014,
                        //                      Ec_note = a.D_SAP_ZPABD_S015,
                        //                      Ec_process = a.D_SAP_ZPABD_S016,
                        //                      Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                      Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                      Ec_eol = "",
                        //                      IsCheck = "",
                        //                  };
                        //    var result_NonItem = NonItem.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                        //                                       select new Pp_Ec_Sub
                        //                                       {
                        //                                           GUID = Guid.NewGuid(),
                        //                                           Ec_no = item.Ec_no,
                        //                                           Ec_model = item.Ec_model,
                        //                                           Ec_bomitem = item.Ec_bomitem,
                        //                                           Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                           Ec_olditem = item.Ec_olditem,
                        //                                           Ec_oldtext = item.Ec_oldtext,
                        //                                           Ec_oldqty = item.Ec_oldqty,
                        //                                           Ec_oldset = item.Ec_oldset,
                        //                                           Ec_newitem = item.Ec_newitem,
                        //                                           Ec_newtext = item.Ec_newtext,
                        //                                           Ec_newqty = item.Ec_newqty,
                        //                                           Ec_newset = item.Ec_newset,
                        //                                           Ec_procurement = item.Ec_procurement,
                        //                                           Ec_location = item.Ec_location,
                        //                                           Ec_eol = item.Ec_eol,
                        //                                           IsCheck = item.IsCheck,
                        //                                           IsManage = 3,
                        //                                           Ec_bomno = item.Ec_bomno,
                        //                                           Ec_change = item.Ec_change,
                        //                                           Ec_local = item.Ec_local,
                        //                                           Ec_note = item.Ec_note,
                        //                                           Ec_process = item.Ec_process,
                        //                                           Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           //    //生管
                        //                                           Ec_pmcdate = "",
                        //                                           Ec_pmclot = "",
                        //                                           Ec_pmcmemo = "",
                        //                                           Ec_pmcnote = "",
                        //                                           Ec_bstock = 0,
                        //                                           pmcModifier = GetIdentityName(),
                        //                                           pmcModifyDate = DateTime.Now,
                        //                                           //    //部管
                        //                                           Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_mmlot = "到部管为止",
                        //                                           Ec_mmlotno = "4400000",
                        //                                           Ec_mmnote = "到部管为止",
                        //                                           mmModifier = GetIdentityName(),
                        //                                           mmModifyDate = DateTime.Now,
                        //                                           //    //采购
                        //                                           Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_purorder = "4300000000",
                        //                                           Ec_pursupplier = "H200000",
                        //                                           Ec_purnote = "到部管为止",
                        //                                           ppModifier = GetIdentityName(),
                        //                                           ppModifyDate = DateTime.Now,
                        //                                           //    //受检
                        //                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_iqcorder = "4300000000",
                        //                                           Ec_iqcnote = "到部管为止",
                        //                                           iqcModifier = GetIdentityName(),
                        //                                           iqcModifyDate = DateTime.Now,
                        //                                           //    //制一
                        //                                           Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_p1dline = "到部管为止",
                        //                                           Ec_p1dlot = "到部管为止",
                        //                                           Ec_p1dnote = "到部管为止",
                        //                                           p1dModifier = GetIdentityName(),
                        //                                           p1dModifyDate = DateTime.Now,
                        //                                           //    //制二
                        //                                           Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_p2dlot = "到部管为止",
                        //                                           Ec_p2dnote = "到部管为止",
                        //                                           p2dModifier = GetIdentityName(),
                        //                                           p2dModifyDate = DateTime.Now,
                        //                                           //    //品管
                        //                                           Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_qalot = "到部管为止",
                        //                                           Ec_qanote = "到部管为止",
                        //                                           qaModifier = GetIdentityName(),
                        //                                           qaModifyDate = DateTime.Now,

                        //                                           UDF01 = "NonItems",
                        //                                           UDF02 = "",
                        //                                           UDF03 = "",
                        //                                           UDF04 = "",
                        //                                           UDF05 = "",
                        //                                           UDF06 = "",
                        //                                           UDF51 = 0,
                        //                                           UDF52 = 0,
                        //                                           UDF53 = 0,
                        //                                           UDF54 = 0,
                        //                                           UDF55 = 0,
                        //                                           UDF56 = 0,
                        //                                           IsDeleted = 0,
                        //                                           Remark = "管理区分部管课",
                        //                                           Creator = GetIdentityName(),
                        //                                           CreateDate = DateTime.Now,
                        //                                       }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonItemList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 2.新物料为空

                        //    #region 3.采购件非C003

                        //    //1.采购件非C003

                        //    var MMPurchase = from a in q_NotEollist

                        //                     join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                     join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                     where b.D_SAP_ZCA1D_Z010 == "F"
                        //                     where b.D_SAP_ZCA1D_Z030 != "C003"
                        //                     select new
                        //                     {
                        //                         Ec_no = a.D_SAP_ZPABD_S001,
                        //                         Ec_model = c.D_SAP_DEST_Z002,
                        //                         Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                         Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                         Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                         Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                         Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                         Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                         Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                         Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                         Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                         Ec_newset = a.D_SAP_ZPABD_S011,
                        //                         Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                         Ec_change = a.D_SAP_ZPABD_S013,
                        //                         Ec_local = a.D_SAP_ZPABD_S014,
                        //                         Ec_note = a.D_SAP_ZPABD_S015,
                        //                         Ec_process = a.D_SAP_ZPABD_S016,

                        //                         Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                         Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                         Ec_eol = "",
                        //                         IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                     };

                        //    var result_MMPurchase = MMPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                        //                                          select new Pp_Ec_Sub
                        //                                          {
                        //                                              GUID = Guid.NewGuid(),
                        //                                              Ec_no = item.Ec_no,
                        //                                              Ec_model = item.Ec_model,
                        //                                              Ec_bomitem = item.Ec_bomitem,
                        //                                              Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                              Ec_olditem = item.Ec_olditem,
                        //                                              Ec_oldtext = item.Ec_oldtext,
                        //                                              Ec_oldqty = item.Ec_oldqty,
                        //                                              Ec_oldset = item.Ec_oldset,
                        //                                              Ec_newitem = item.Ec_newitem,
                        //                                              Ec_newtext = item.Ec_newtext,
                        //                                              Ec_newqty = item.Ec_newqty,
                        //                                              Ec_newset = item.Ec_newset,
                        //                                              Ec_bomno = item.Ec_bomno,
                        //                                              Ec_change = item.Ec_change,
                        //                                              Ec_local = item.Ec_local,
                        //                                              Ec_note = item.Ec_note,
                        //                                              Ec_process = item.Ec_process,
                        //                                              Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_procurement = item.Ec_procurement,
                        //                                              Ec_location = item.Ec_location,
                        //                                              IsCheck = item.IsCheck,
                        //                                              IsManage = 2,
                        //                                              Ec_eol = item.Ec_eol,

                        //                                              Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_pmcdate = "",
                        //                                              Ec_pmclot = "",
                        //                                              Ec_pmcmemo = "",
                        //                                              Ec_pmcnote = "",
                        //                                              Ec_bstock = 0,
                        //                                              pmcModifier = GetIdentityName(),
                        //                                              pmcModifyDate = DateTime.Now,

                        //                                              //    //制二
                        //                                              Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_p2dlot = "与制二无关",
                        //                                              Ec_p2dnote = "与制二无关",
                        //                                              p2dModifier = GetIdentityName(),
                        //                                              p2dModifyDate = DateTime.Now,

                        //                                              //    //部管
                        //                                              Ec_mmdate = "",
                        //                                              Ec_mmlot = "",
                        //                                              Ec_mmlotno = "",
                        //                                              Ec_mmnote = "",
                        //                                              mmModifier = GetIdentityName(),
                        //                                              mmModifyDate = DateTime.Now,

                        //                                              //    //采购
                        //                                              Ec_purdate = "",
                        //                                              Ec_purorder = "",
                        //                                              Ec_pursupplier = "",
                        //                                              Ec_purnote = "",
                        //                                              ppModifier = GetIdentityName(),
                        //                                              ppModifyDate = DateTime.Now,

                        //                                              //    //受检
                        //                                              Ec_iqcdate = "",
                        //                                              Ec_iqcorder = "",
                        //                                              Ec_iqcnote = "",
                        //                                              iqcModifier = GetIdentityName(),
                        //                                              iqcModifyDate = DateTime.Now,

                        //                                              //    //制一
                        //                                              Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_p1dline = "到部管为止",
                        //                                              Ec_p1dlot = "到部管为止",
                        //                                              Ec_p1dnote = "到部管为止",
                        //                                              p1dModifier = GetIdentityName(),
                        //                                              p1dModifyDate = DateTime.Now,

                        //                                              //    //品管
                        //                                              Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_qalot = "到部管为止",
                        //                                              Ec_qanote = "到部管为止",
                        //                                              qaModifier = GetIdentityName(),
                        //                                              qaModifyDate = DateTime.Now,

                        //                                              UDF01 = "MMPurchase",
                        //                                              UDF02 = "",
                        //                                              UDF03 = "",
                        //                                              UDF04 = "",
                        //                                              UDF05 = "",
                        //                                              UDF06 = "",
                        //                                              UDF51 = 0,
                        //                                              UDF52 = 0,
                        //                                              UDF53 = 0,
                        //                                              UDF54 = 0,
                        //                                              UDF55 = 0,
                        //                                              UDF56 = 0,
                        //                                              IsDeleted = 0,
                        //                                              Remark = "管理区分部管课",
                        //                                              Creator = GetIdentityName(),
                        //                                              CreateDate = DateTime.Now,
                        //                                          }).Distinct().ToList();
                        //    DB.BulkInsert(New_MMPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 3.采购件非C003

                        //    #region 4.采购件C003

                        //    //1.采购件C003
                        //    //1.采购件非C003

                        //    var P2dPurchase = from a in q_NotEollist

                        //                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                      where b.D_SAP_ZCA1D_Z010 == "F"
                        //                      where b.D_SAP_ZCA1D_Z030 == "C003"
                        //                      select new
                        //                      {
                        //                          Ec_no = a.D_SAP_ZPABD_S001,
                        //                          Ec_model = c.D_SAP_DEST_Z002,
                        //                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                          Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                          Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                          Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                          Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                          Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                          Ec_newset = a.D_SAP_ZPABD_S011,
                        //                          Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                          Ec_change = a.D_SAP_ZPABD_S013,
                        //                          Ec_local = a.D_SAP_ZPABD_S014,
                        //                          Ec_note = a.D_SAP_ZPABD_S015,
                        //                          Ec_process = a.D_SAP_ZPABD_S016,

                        //                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                          Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                          Ec_eol = "",
                        //                          IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                      };

                        //    var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                        //                                                  select new Pp_Ec_Sub
                        //                                                  {
                        //                                                      GUID = Guid.NewGuid(),
                        //                                                      Ec_no = item.Ec_no,
                        //                                                      Ec_model = item.Ec_model,
                        //                                                      Ec_bomitem = item.Ec_bomitem,
                        //                                                      Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                                      Ec_olditem = item.Ec_olditem,
                        //                                                      Ec_oldtext = item.Ec_oldtext,
                        //                                                      Ec_oldqty = item.Ec_oldqty,
                        //                                                      Ec_oldset = item.Ec_oldset,
                        //                                                      Ec_newitem = item.Ec_newitem,
                        //                                                      Ec_newtext = item.Ec_newtext,
                        //                                                      Ec_newqty = item.Ec_newqty,
                        //                                                      Ec_newset = item.Ec_newset,
                        //                                                      Ec_bomno = item.Ec_bomno,
                        //                                                      Ec_change = item.Ec_change,
                        //                                                      Ec_local = item.Ec_local,
                        //                                                      Ec_note = item.Ec_note,
                        //                                                      Ec_process = item.Ec_process,
                        //                                                      Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_procurement = item.Ec_procurement,
                        //                                                      Ec_location = item.Ec_location,
                        //                                                      IsCheck = item.IsCheck,
                        //                                                      IsManage = 3,
                        //                                                      Ec_eol = item.Ec_eol,

                        //                                                      Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_pmcdate = "",
                        //                                                      Ec_pmclot = "",
                        //                                                      Ec_pmcmemo = "",
                        //                                                      Ec_pmcnote = "",
                        //                                                      Ec_bstock = 0,
                        //                                                      pmcModifier = GetIdentityName(),
                        //                                                      pmcModifyDate = DateTime.Now,

                        //                                                      //    //制二
                        //                                                      Ec_p2ddate = "",
                        //                                                      Ec_p2dlot = "",
                        //                                                      Ec_p2dnote = "",
                        //                                                      p2dModifier = GetIdentityName(),
                        //                                                      p2dModifyDate = DateTime.Now,

                        //                                                      //    //部管
                        //                                                      Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_mmlot = "与部管无关",
                        //                                                      Ec_mmlotno = "与部管无关",
                        //                                                      Ec_mmnote = "与部管无关",
                        //                                                      mmModifier = GetIdentityName(),
                        //                                                      mmModifyDate = DateTime.Now,

                        //                                                      //    //采购
                        //                                                      Ec_purdate = "",
                        //                                                      Ec_purorder = "",
                        //                                                      Ec_pursupplier = "",
                        //                                                      Ec_purnote = "",
                        //                                                      ppModifier = GetIdentityName(),
                        //                                                      ppModifyDate = DateTime.Now,

                        //                                                      //    //受检
                        //                                                      Ec_iqcdate = "",
                        //                                                      Ec_iqcorder = "",
                        //                                                      Ec_iqcnote = "",
                        //                                                      iqcModifier = GetIdentityName(),
                        //                                                      iqcModifyDate = DateTime.Now,

                        //                                                      //    //制一
                        //                                                      Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_p1dline = "到部管为止",
                        //                                                      Ec_p1dlot = "到部管为止",
                        //                                                      Ec_p1dnote = "到部管为止",
                        //                                                      p1dModifier = GetIdentityName(),
                        //                                                      p1dModifyDate = DateTime.Now,

                        //                                                      //    //品管
                        //                                                      Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_qalot = "到部管为止",
                        //                                                      Ec_qanote = "到部管为止",
                        //                                                      qaModifier = GetIdentityName(),
                        //                                                      qaModifyDate = DateTime.Now,

                        //                                                      UDF01 = "P2DPurchase",
                        //                                                      UDF02 = "",
                        //                                                      UDF03 = "",
                        //                                                      UDF04 = "",
                        //                                                      UDF05 = "",
                        //                                                      UDF06 = "",
                        //                                                      UDF51 = 0,
                        //                                                      UDF52 = 0,
                        //                                                      UDF53 = 0,
                        //                                                      UDF54 = 0,
                        //                                                      UDF55 = 0,
                        //                                                      UDF56 = 0,
                        //                                                      IsDeleted = 0,
                        //                                                      Remark = "管理区分部管课",
                        //                                                      Creator = GetIdentityName(),
                        //                                                      CreateDate = DateTime.Now,
                        //                                                  }).Distinct().ToList();
                        //    DB.BulkInsert(New_result_P2dPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 4.采购件C003
                        //}
                    }
                    if (this.Ec_distinction.SelectedValue == "3")//内部
                    {
                        //if (this.IsManage.SelectedValue == "1")
                        //{

                        #region 1.非采购件

                        //1.非采购件
                        var New_NonPurchase = from a in q_NotEollist
                                              join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                              join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                              where b.D_SAP_ZCA1D_Z010 == "E"
                                              select new
                                              {
                                                  Ec_no = a.D_SAP_ZPABD_S001,
                                                  Ec_model = c.D_SAP_DEST_Z002,
                                                  Ec_bomitem = a.D_SAP_ZPABD_S002,
                                                  Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                                  Ec_olditem = a.D_SAP_ZPABD_S004,
                                                  Ec_oldtext = a.D_SAP_ZPABD_S005,
                                                  Ec_oldqty = a.D_SAP_ZPABD_S006,
                                                  Ec_oldset = a.D_SAP_ZPABD_S007,
                                                  Ec_newitem = a.D_SAP_ZPABD_S008,
                                                  Ec_newtext = a.D_SAP_ZPABD_S009,
                                                  Ec_newqty = a.D_SAP_ZPABD_S010,
                                                  Ec_newset = a.D_SAP_ZPABD_S011,
                                                  Ec_bomno = a.D_SAP_ZPABD_S012,
                                                  Ec_change = a.D_SAP_ZPABD_S013,
                                                  Ec_local = a.D_SAP_ZPABD_S014,
                                                  Ec_note = a.D_SAP_ZPABD_S015,
                                                  Ec_process = a.D_SAP_ZPABD_S016,

                                                  Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                                  Ec_location = b.D_SAP_ZCA1D_Z030,
                                                  Ec_eol = "",
                                                  IsCheck = b.D_SAP_ZCA1D_Z019,
                                              };

                        var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                                                               select new Pp_Ec_Sub
                                                               {
                                                                   GUID = Guid.NewGuid(),
                                                                   Ec_no = item.Ec_no,
                                                                   Ec_model = item.Ec_model,
                                                                   Ec_bomitem = item.Ec_bomitem,
                                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                                   Ec_olditem = item.Ec_olditem,
                                                                   Ec_oldtext = item.Ec_oldtext,
                                                                   Ec_oldqty = item.Ec_oldqty,
                                                                   Ec_oldset = item.Ec_oldset,
                                                                   Ec_newitem = item.Ec_newitem,
                                                                   Ec_newtext = item.Ec_newtext,
                                                                   Ec_newqty = item.Ec_newqty,
                                                                   Ec_newset = item.Ec_newset,
                                                                   Ec_bomno = item.Ec_bomno,
                                                                   Ec_change = item.Ec_change,
                                                                   Ec_local = item.Ec_local,
                                                                   Ec_note = item.Ec_note,
                                                                   Ec_process = item.Ec_process,
                                                                   Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_procurement = item.Ec_procurement,
                                                                   Ec_location = item.Ec_location,
                                                                   IsCheck = item.IsCheck,
                                                                   IsManage = 0,
                                                                   IsMmManage = 0,
                                                                   IsAssyManage = 0,
                                                                   IsPcbaManage = 1,
                                                                   Ec_eol = item.Ec_eol,

                                                                   Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_pmclot = "虚设件变更",
                                                                   Ec_pmcmemo = "虚设件变更",
                                                                   Ec_pmcnote = "虚设件变更",
                                                                   Ec_bstock = 0,
                                                                   pmcModifier = GetIdentityName(),
                                                                   pmcModifyDate = DateTime.Now,

                                                                   //    //制二
                                                                   Ec_p2ddate = "",
                                                                   Ec_p2dlot = "",
                                                                   Ec_p2dnote = "",
                                                                   p2dModifier = GetIdentityName(),
                                                                   p2dModifyDate = DateTime.Now,

                                                                   //    //部管
                                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_mmlot = "与部管无关",
                                                                   Ec_mmlotno = "与部管无关",
                                                                   Ec_mmnote = "与部管无关",
                                                                   mmModifier = GetIdentityName(),
                                                                   mmModifyDate = DateTime.Now,

                                                                   //    //采购
                                                                   Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_purorder = "4300000000",
                                                                   Ec_pursupplier = "H200000",
                                                                   Ec_purnote = "与采购无关",
                                                                   ppModifier = GetIdentityName(),
                                                                   ppModifyDate = DateTime.Now,

                                                                   //    //受检
                                                                   Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_iqcorder = "4300000000",
                                                                   Ec_iqcnote = "与受检无关",
                                                                   iqcModifier = GetIdentityName(),
                                                                   iqcModifyDate = DateTime.Now,

                                                                   //    //制一
                                                                   Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_p1dline = "内部管理",
                                                                   Ec_p1dlot = "内部管理",
                                                                   Ec_p1dnote = "内部管理",
                                                                   p1dModifier = GetIdentityName(),
                                                                   p1dModifyDate = DateTime.Now,

                                                                   //    //品管
                                                                   Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_qalot = "内部管理",
                                                                   Ec_qanote = "内部管理",
                                                                   qaModifier = GetIdentityName(),
                                                                   qaModifyDate = DateTime.Now,

                                                                   UDF01 = "NonPurchase",
                                                                   UDF02 = "",
                                                                   UDF03 = "",
                                                                   UDF04 = "",
                                                                   UDF05 = "",
                                                                   UDF06 = "",
                                                                   UDF51 = 0,
                                                                   UDF52 = 0,
                                                                   UDF53 = 0,
                                                                   UDF54 = 0,
                                                                   UDF55 = 0,
                                                                   UDF56 = 0,
                                                                   IsDeleted = 0,
                                                                   Remark = "管理区分内部",
                                                                   Creator = GetIdentityName(),
                                                                   CreateDate = DateTime.Now,
                                                               }).Distinct().ToList();
                        DB.BulkInsert(New_NonPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 1.非采购件

                        #region 2.新物料为空

                        var NonItem = from a in q_NotEollist
                                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                      where a.D_SAP_ZPABD_S008 == "0"
                                      //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                      //                                   where d.D_SAP_ZCA1D_Z034 == ""
                                      //                                   select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                      //                                   select d.D_SAP_DEST_Z001)
                                      //                                 .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                                      //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                                      //                                    select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S008)
                                      where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                                      //where b.Ec_no == strecn
                                      //where a.Prodate == sdate//投入日期
                                      select new
                                      {
                                          Ec_no = a.D_SAP_ZPABD_S001,
                                          Ec_model = c.D_SAP_DEST_Z002,
                                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                          Ec_olditem = a.D_SAP_ZPABD_S004,
                                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                                          Ec_oldset = a.D_SAP_ZPABD_S007,
                                          Ec_newitem = a.D_SAP_ZPABD_S008,
                                          Ec_newtext = a.D_SAP_ZPABD_S009,
                                          Ec_newqty = a.D_SAP_ZPABD_S010,
                                          Ec_newset = a.D_SAP_ZPABD_S011,
                                          Ec_bomno = a.D_SAP_ZPABD_S012,
                                          Ec_change = a.D_SAP_ZPABD_S013,
                                          Ec_local = a.D_SAP_ZPABD_S014,
                                          Ec_note = a.D_SAP_ZPABD_S015,
                                          Ec_process = a.D_SAP_ZPABD_S016,
                                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                          Ec_location = b.D_SAP_ZCA1D_Z030,
                                          Ec_eol = "",
                                          IsCheck = "",
                                      };
                        var result_NonItem = NonItem.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = Guid.NewGuid(),
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               Ec_eol = item.Ec_eol,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = 0,
                                                               IsMmManage = 0,
                                                               IsAssyManage = 0,
                                                               IsPcbaManage = 1,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                               Ec_pmcdate = "",
                                                               Ec_pmclot = "",
                                                               Ec_pmcmemo = "",
                                                               Ec_pmcnote = "",
                                                               Ec_bstock = 0,
                                                               pmcModifier = GetIdentityName(),
                                                               pmcModifyDate = DateTime.Now,

                                                               //    //制二
                                                               Ec_p2ddate = "",
                                                               Ec_p2dlot = "",
                                                               Ec_p2dnote = "",
                                                               p2dModifier = GetIdentityName(),
                                                               p2dModifyDate = DateTime.Now,

                                                               //    //部管
                                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_mmlot = "内部管理",
                                                               Ec_mmlotno = "内部管理",
                                                               Ec_mmnote = "内部管理",
                                                               mmModifier = GetIdentityName(),
                                                               mmModifyDate = DateTime.Now,

                                                               //    //采购
                                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_purorder = "内部管理",
                                                               Ec_pursupplier = "内部管理",
                                                               Ec_purnote = "内部管理",
                                                               ppModifier = GetIdentityName(),
                                                               ppModifyDate = DateTime.Now,

                                                               //    //受检
                                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_iqcorder = "内部管理",
                                                               Ec_iqcnote = "内部管理",
                                                               iqcModifier = GetIdentityName(),
                                                               iqcModifyDate = DateTime.Now,

                                                               //    //制一
                                                               Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_p1dline = "内部管理",
                                                               Ec_p1dlot = "内部管理",
                                                               Ec_p1dnote = "内部管理",
                                                               p1dModifier = GetIdentityName(),
                                                               p1dModifyDate = DateTime.Now,

                                                               //    //品管
                                                               Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_qalot = "内部管理",
                                                               Ec_qanote = "内部管理",
                                                               qaModifier = GetIdentityName(),
                                                               qaModifyDate = DateTime.Now,

                                                               UDF01 = "NonItems",
                                                               UDF02 = "",
                                                               UDF03 = "",
                                                               UDF04 = "",
                                                               UDF05 = "",
                                                               UDF06 = "",
                                                               UDF51 = 0,
                                                               UDF52 = 0,
                                                               UDF53 = 0,
                                                               UDF54 = 0,
                                                               UDF55 = 0,
                                                               UDF56 = 0,
                                                               IsDeleted = 0,
                                                               Remark = "管理区分内部",
                                                               Creator = GetIdentityName(),
                                                               CreateDate = DateTime.Now,
                                                           }).Distinct().ToList();
                        DB.BulkInsert(New_NonItemList);
                        DB.BulkSaveChanges();

                        #endregion 2.新物料为空

                        #region 3.采购件非C003

                        //1.采购件非C003

                        var MMPurchase = from a in q_NotEollist

                                         join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                         join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                         where b.D_SAP_ZCA1D_Z010 == "F"
                                         where b.D_SAP_ZCA1D_Z030 != "C003"
                                         select new
                                         {
                                             Ec_no = a.D_SAP_ZPABD_S001,
                                             Ec_model = c.D_SAP_DEST_Z002,
                                             Ec_bomitem = a.D_SAP_ZPABD_S002,
                                             Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                             Ec_olditem = a.D_SAP_ZPABD_S004,
                                             Ec_oldtext = a.D_SAP_ZPABD_S005,
                                             Ec_oldqty = a.D_SAP_ZPABD_S006,
                                             Ec_oldset = a.D_SAP_ZPABD_S007,
                                             Ec_newitem = a.D_SAP_ZPABD_S008,
                                             Ec_newtext = a.D_SAP_ZPABD_S009,
                                             Ec_newqty = a.D_SAP_ZPABD_S010,
                                             Ec_newset = a.D_SAP_ZPABD_S011,
                                             Ec_bomno = a.D_SAP_ZPABD_S012,
                                             Ec_change = a.D_SAP_ZPABD_S013,
                                             Ec_local = a.D_SAP_ZPABD_S014,
                                             Ec_note = a.D_SAP_ZPABD_S015,
                                             Ec_process = a.D_SAP_ZPABD_S016,

                                             Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                             Ec_location = b.D_SAP_ZCA1D_Z030,
                                             Ec_eol = "",
                                             IsCheck = b.D_SAP_ZCA1D_Z019,
                                         };

                        var result_MMPurchase = MMPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                                                              select new Pp_Ec_Sub
                                                              {
                                                                  GUID = Guid.NewGuid(),
                                                                  Ec_no = item.Ec_no,
                                                                  Ec_model = item.Ec_model,
                                                                  Ec_bomitem = item.Ec_bomitem,
                                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                                  Ec_olditem = item.Ec_olditem,
                                                                  Ec_oldtext = item.Ec_oldtext,
                                                                  Ec_oldqty = item.Ec_oldqty,
                                                                  Ec_oldset = item.Ec_oldset,
                                                                  Ec_newitem = item.Ec_newitem,
                                                                  Ec_newtext = item.Ec_newtext,
                                                                  Ec_newqty = item.Ec_newqty,
                                                                  Ec_newset = item.Ec_newset,
                                                                  Ec_bomno = item.Ec_bomno,
                                                                  Ec_change = item.Ec_change,
                                                                  Ec_local = item.Ec_local,
                                                                  Ec_note = item.Ec_note,
                                                                  Ec_process = item.Ec_process,
                                                                  Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_procurement = item.Ec_procurement,
                                                                  Ec_location = item.Ec_location,
                                                                  IsCheck = item.IsCheck,
                                                                  IsManage = 0,
                                                                  IsMmManage = 0,
                                                                  IsAssyManage = 0,
                                                                  IsPcbaManage = 1,
                                                                  Ec_eol = item.Ec_eol,

                                                                  Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_pmcdate = "",
                                                                  Ec_pmclot = "",
                                                                  Ec_pmcmemo = "",
                                                                  Ec_pmcnote = "",
                                                                  Ec_bstock = 0,
                                                                  pmcModifier = GetIdentityName(),
                                                                  pmcModifyDate = DateTime.Now,

                                                                  //    //制二
                                                                  Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p2dlot = "与制二无关",
                                                                  Ec_p2dnote = "与制二无关",
                                                                  p2dModifier = GetIdentityName(),
                                                                  p2dModifyDate = DateTime.Now,

                                                                  //    //部管
                                                                  Ec_mmdate = "",
                                                                  Ec_mmlot = "",
                                                                  Ec_mmlotno = "",
                                                                  Ec_mmnote = "",
                                                                  mmModifier = GetIdentityName(),
                                                                  mmModifyDate = DateTime.Now,

                                                                  //    //采购
                                                                  Ec_purdate = "",
                                                                  Ec_purorder = "",
                                                                  Ec_pursupplier = "",
                                                                  Ec_purnote = "",
                                                                  ppModifier = GetIdentityName(),
                                                                  ppModifyDate = DateTime.Now,

                                                                  //    //受检
                                                                  Ec_iqcdate = "",
                                                                  Ec_iqcorder = "",
                                                                  Ec_iqcnote = "",
                                                                  iqcModifier = GetIdentityName(),
                                                                  iqcModifyDate = DateTime.Now,

                                                                  //    //制一
                                                                  Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p1dline = "内部管理",
                                                                  Ec_p1dlot = "内部管理",
                                                                  Ec_p1dnote = "内部管理",
                                                                  p1dModifier = GetIdentityName(),
                                                                  p1dModifyDate = DateTime.Now,

                                                                  //    //品管
                                                                  Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_qalot = "内部管理",
                                                                  Ec_qanote = "内部管理",
                                                                  qaModifier = GetIdentityName(),
                                                                  qaModifyDate = DateTime.Now,

                                                                  UDF01 = "MMPurchase",
                                                                  UDF02 = "",
                                                                  UDF03 = "",
                                                                  UDF04 = "",
                                                                  UDF05 = "",
                                                                  UDF06 = "",
                                                                  UDF51 = 0,
                                                                  UDF52 = 0,
                                                                  UDF53 = 0,
                                                                  UDF54 = 0,
                                                                  UDF55 = 0,
                                                                  UDF56 = 0,
                                                                  IsDeleted = 0,
                                                                  Remark = "管理区分内部",
                                                                  Creator = GetIdentityName(),
                                                                  CreateDate = DateTime.Now,
                                                              }).Distinct().ToList();
                        DB.BulkInsert(New_MMPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 3.采购件非C003

                        #region 4.采购件C003

                        //1.采购件C003
                        //1.采购件非C003

                        var P2dPurchase = from a in q_NotEollist

                                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                          where b.D_SAP_ZCA1D_Z010 == "F"
                                          where b.D_SAP_ZCA1D_Z030 == "C003"
                                          select new
                                          {
                                              Ec_no = a.D_SAP_ZPABD_S001,
                                              Ec_model = c.D_SAP_DEST_Z002,
                                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                              Ec_olditem = a.D_SAP_ZPABD_S004,
                                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                                              Ec_oldset = a.D_SAP_ZPABD_S007,
                                              Ec_newitem = a.D_SAP_ZPABD_S008,
                                              Ec_newtext = a.D_SAP_ZPABD_S009,
                                              Ec_newqty = a.D_SAP_ZPABD_S010,
                                              Ec_newset = a.D_SAP_ZPABD_S011,
                                              Ec_bomno = a.D_SAP_ZPABD_S012,
                                              Ec_change = a.D_SAP_ZPABD_S013,
                                              Ec_local = a.D_SAP_ZPABD_S014,
                                              Ec_note = a.D_SAP_ZPABD_S015,
                                              Ec_process = a.D_SAP_ZPABD_S016,

                                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                              Ec_location = b.D_SAP_ZCA1D_Z030,
                                              Ec_eol = "",
                                              IsCheck = b.D_SAP_ZCA1D_Z019,
                                          };

                        var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                                                                      select new Pp_Ec_Sub
                                                                      {
                                                                          GUID = Guid.NewGuid(),
                                                                          Ec_no = item.Ec_no,
                                                                          Ec_model = item.Ec_model,
                                                                          Ec_bomitem = item.Ec_bomitem,
                                                                          Ec_bomsubitem = item.Ec_bomsubitem,
                                                                          Ec_olditem = item.Ec_olditem,
                                                                          Ec_oldtext = item.Ec_oldtext,
                                                                          Ec_oldqty = item.Ec_oldqty,
                                                                          Ec_oldset = item.Ec_oldset,
                                                                          Ec_newitem = item.Ec_newitem,
                                                                          Ec_newtext = item.Ec_newtext,
                                                                          Ec_newqty = item.Ec_newqty,
                                                                          Ec_newset = item.Ec_newset,
                                                                          Ec_bomno = item.Ec_bomno,
                                                                          Ec_change = item.Ec_change,
                                                                          Ec_local = item.Ec_local,
                                                                          Ec_note = item.Ec_note,
                                                                          Ec_process = item.Ec_process,
                                                                          Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_procurement = item.Ec_procurement,
                                                                          Ec_location = item.Ec_location,
                                                                          IsCheck = item.IsCheck,
                                                                          IsManage = 0,
                                                                          IsMmManage = 0,
                                                                          IsAssyManage = 0,
                                                                          IsPcbaManage = 1,
                                                                          Ec_eol = item.Ec_eol,

                                                                          Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_pmcdate = "",
                                                                          Ec_pmclot = "",
                                                                          Ec_pmcmemo = "",
                                                                          Ec_pmcnote = "",
                                                                          Ec_bstock = 0,
                                                                          pmcModifier = GetIdentityName(),
                                                                          pmcModifyDate = DateTime.Now,

                                                                          //    //制二
                                                                          Ec_p2ddate = "",
                                                                          Ec_p2dlot = "",
                                                                          Ec_p2dnote = "",
                                                                          p2dModifier = GetIdentityName(),
                                                                          p2dModifyDate = DateTime.Now,

                                                                          //    //部管
                                                                          Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_mmlot = "与部管无关",
                                                                          Ec_mmlotno = "与部管无关",
                                                                          Ec_mmnote = "与部管无关",
                                                                          mmModifier = GetIdentityName(),
                                                                          mmModifyDate = DateTime.Now,

                                                                          //    //采购
                                                                          Ec_purdate = "",
                                                                          Ec_purorder = "",
                                                                          Ec_pursupplier = "",
                                                                          Ec_purnote = "",
                                                                          ppModifier = GetIdentityName(),
                                                                          ppModifyDate = DateTime.Now,

                                                                          //    //受检
                                                                          Ec_iqcdate = "",
                                                                          Ec_iqcorder = "",
                                                                          Ec_iqcnote = "",
                                                                          iqcModifier = GetIdentityName(),
                                                                          iqcModifyDate = DateTime.Now,

                                                                          //    //制一
                                                                          Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_p1dline = "内部管理",
                                                                          Ec_p1dlot = "内部管理",
                                                                          Ec_p1dnote = "内部管理",
                                                                          p1dModifier = GetIdentityName(),
                                                                          p1dModifyDate = DateTime.Now,

                                                                          //    //品管
                                                                          Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_qalot = "内部管理",
                                                                          Ec_qanote = "内部管理",
                                                                          qaModifier = GetIdentityName(),
                                                                          qaModifyDate = DateTime.Now,

                                                                          UDF01 = "P2DPurchase",
                                                                          UDF02 = "",
                                                                          UDF03 = "",
                                                                          UDF04 = "",
                                                                          UDF05 = "",
                                                                          UDF06 = "",
                                                                          UDF51 = 0,
                                                                          UDF52 = 0,
                                                                          UDF53 = 0,
                                                                          UDF54 = 0,
                                                                          UDF55 = 0,
                                                                          UDF56 = 0,
                                                                          IsDeleted = 0,
                                                                          Remark = "管理区分内部",
                                                                          Creator = GetIdentityName(),
                                                                          CreateDate = DateTime.Now,
                                                                      }).Distinct().ToList();
                        DB.BulkInsert(New_result_P2dPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 4.采购件C003

                        //}
                        //if (this.IsManage.SelectedValue == "0")
                        //{
                        //    #region 1.非采购件

                        //    //1.非采购件
                        //    var New_NonPurchase = from a in q_NotEollist
                        //                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                          where b.D_SAP_ZCA1D_Z010 == "E"
                        //                          select new
                        //                          {
                        //                              Ec_no = a.D_SAP_ZPABD_S001,
                        //                              Ec_model = c.D_SAP_DEST_Z002,
                        //                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                              Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                              Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                              Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                              Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                              Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                              Ec_newset = a.D_SAP_ZPABD_S011,
                        //                              Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                              Ec_change = a.D_SAP_ZPABD_S013,
                        //                              Ec_local = a.D_SAP_ZPABD_S014,
                        //                              Ec_note = a.D_SAP_ZPABD_S015,
                        //                              Ec_process = a.D_SAP_ZPABD_S016,

                        //                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                              Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                              Ec_eol = "",
                        //                              IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                          };

                        //    var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                        //                                           select new Pp_Ec_Sub
                        //                                           {
                        //                                               GUID = Guid.NewGuid(),
                        //                                               Ec_no = item.Ec_no,
                        //                                               Ec_model = item.Ec_model,
                        //                                               Ec_bomitem = item.Ec_bomitem,
                        //                                               Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                               Ec_olditem = item.Ec_olditem,
                        //                                               Ec_oldtext = item.Ec_oldtext,
                        //                                               Ec_oldqty = item.Ec_oldqty,
                        //                                               Ec_oldset = item.Ec_oldset,
                        //                                               Ec_newitem = item.Ec_newitem,
                        //                                               Ec_newtext = item.Ec_newtext,
                        //                                               Ec_newqty = item.Ec_newqty,
                        //                                               Ec_newset = item.Ec_newset,
                        //                                               Ec_bomno = item.Ec_bomno,
                        //                                               Ec_change = item.Ec_change,
                        //                                               Ec_local = item.Ec_local,
                        //                                               Ec_note = item.Ec_note,
                        //                                               Ec_process = item.Ec_process,
                        //                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                               Ec_procurement = item.Ec_procurement,
                        //                                               Ec_location = item.Ec_location,
                        //                                               IsCheck = item.IsCheck,
                        //                                               IsManage = 3,
                        //                                               Ec_eol = item.Ec_eol,

                        //                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                               Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_pmclot = "虚设件变更",
                        //                                               Ec_pmcmemo = "虚设件变更",
                        //                                               Ec_pmcnote = "虚设件变更",
                        //                                               Ec_bstock = 0,
                        //                                               pmcModifier = GetIdentityName(),
                        //                                               pmcModifyDate = DateTime.Now,

                        //                                               //    //制二
                        //                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_p2dlot = "内部管理",
                        //                                               Ec_p2dnote = "内部管理",
                        //                                               p2dModifier = GetIdentityName(),
                        //                                               p2dModifyDate = DateTime.Now,

                        //                                               //    //部管
                        //                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_mmlot = "内部管理",
                        //                                               Ec_mmlotno = "内部管理",
                        //                                               Ec_mmnote = "内部管理",
                        //                                               mmModifier = GetIdentityName(),
                        //                                               mmModifyDate = DateTime.Now,

                        //                                               //    //采购
                        //                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_purorder = "4300000000",
                        //                                               Ec_pursupplier = "H200000",
                        //                                               Ec_purnote = "内部管理",
                        //                                               ppModifier = GetIdentityName(),
                        //                                               ppModifyDate = DateTime.Now,

                        //                                               //    //受检
                        //                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_iqcorder = "4300000000",
                        //                                               Ec_iqcnote = "内部管理",
                        //                                               iqcModifier = GetIdentityName(),
                        //                                               iqcModifyDate = DateTime.Now,

                        //                                               //    //制一
                        //                                               Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_p1dline = "内部管理",
                        //                                               Ec_p1dlot = "内部管理",
                        //                                               Ec_p1dnote = "内部管理",
                        //                                               p1dModifier = GetIdentityName(),
                        //                                               p1dModifyDate = DateTime.Now,

                        //                                               //    //品管
                        //                                               Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                               Ec_qalot = "内部管理",
                        //                                               Ec_qanote = "内部管理",
                        //                                               qaModifier = GetIdentityName(),
                        //                                               qaModifyDate = DateTime.Now,
                        //                                               UDF01 = "NonPurchase",
                        //                                               UDF02 = "",
                        //                                               UDF03 = "",
                        //                                               UDF04 = "",
                        //                                               UDF05 = "",
                        //                                               UDF06 = "",
                        //                                               UDF51 = 0,
                        //                                               UDF52 = 0,
                        //                                               UDF53 = 0,
                        //                                               UDF54 = 0,
                        //                                               UDF55 = 0,
                        //                                               UDF56 = 0,
                        //                                               IsDeleted = 0,
                        //                                               Remark = "管理区分内部",
                        //                                               Creator = GetIdentityName(),
                        //                                               CreateDate = DateTime.Now,
                        //                                           }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 1.非采购件

                        //    #region 2.新物料为空

                        //    var NonItem = from a in q_NotEollist
                        //                  join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                        //                  join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                  where a.D_SAP_ZPABD_S008 == "0"
                        //                  //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                  //                                   where d.D_SAP_ZCA1D_Z034 == ""
                        //                  //                                   select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                        //                  //                                   select d.D_SAP_DEST_Z001)
                        //                  //                                 .Contains(a.D_SAP_ZPABD_S002)
                        //                  //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                        //                  //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                        //                  //                                    select d.D_SAP_ZCA1D_Z002)
                        //                  //                                .Contains(a.D_SAP_ZPABD_S008)
                        //                  where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                        //                  //where b.Ec_no == strecn
                        //                  //where a.Prodate == sdate//投入日期
                        //                  select new
                        //                  {
                        //                      Ec_no = a.D_SAP_ZPABD_S001,
                        //                      Ec_model = c.D_SAP_DEST_Z002,
                        //                      Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                      Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                      Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                      Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                      Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                      Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                      Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                      Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                      Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                      Ec_newset = a.D_SAP_ZPABD_S011,
                        //                      Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                      Ec_change = a.D_SAP_ZPABD_S013,
                        //                      Ec_local = a.D_SAP_ZPABD_S014,
                        //                      Ec_note = a.D_SAP_ZPABD_S015,
                        //                      Ec_process = a.D_SAP_ZPABD_S016,
                        //                      Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                      Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                      Ec_eol = "",
                        //                      IsCheck = "",
                        //                  };
                        //    var result_NonItem = NonItem.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                        //                                       select new Pp_Ec_Sub
                        //                                       {
                        //                                           GUID = Guid.NewGuid(),
                        //                                           Ec_no = item.Ec_no,
                        //                                           Ec_model = item.Ec_model,
                        //                                           Ec_bomitem = item.Ec_bomitem,
                        //                                           Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                           Ec_olditem = item.Ec_olditem,
                        //                                           Ec_oldtext = item.Ec_oldtext,
                        //                                           Ec_oldqty = item.Ec_oldqty,
                        //                                           Ec_oldset = item.Ec_oldset,
                        //                                           Ec_newitem = item.Ec_newitem,
                        //                                           Ec_newtext = item.Ec_newtext,
                        //                                           Ec_newqty = item.Ec_newqty,
                        //                                           Ec_newset = item.Ec_newset,
                        //                                           Ec_procurement = item.Ec_procurement,
                        //                                           Ec_location = item.Ec_location,
                        //                                           Ec_eol = item.Ec_eol,
                        //                                           IsCheck = item.IsCheck,
                        //                                           IsManage = 3,
                        //                                           Ec_bomno = item.Ec_bomno,
                        //                                           Ec_change = item.Ec_change,
                        //                                           Ec_local = item.Ec_local,
                        //                                           Ec_note = item.Ec_note,
                        //                                           Ec_process = item.Ec_process,
                        //                                           Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           //    //生管
                        //                                           Ec_pmcdate = "",
                        //                                           Ec_pmclot = "",
                        //                                           Ec_pmcmemo = "",
                        //                                           Ec_pmcnote = "",
                        //                                           Ec_bstock = 0,
                        //                                           pmcModifier = GetIdentityName(),
                        //                                           pmcModifyDate = DateTime.Now,
                        //                                           //    //部管
                        //                                           Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_mmlot = "内部管理",
                        //                                           Ec_mmlotno = "4400000",
                        //                                           Ec_mmnote = "内部管理",
                        //                                           mmModifier = GetIdentityName(),
                        //                                           mmModifyDate = DateTime.Now,
                        //                                           //    //采购
                        //                                           Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_purorder = "4300000000",
                        //                                           Ec_pursupplier = "H200000",
                        //                                           Ec_purnote = "内部管理",
                        //                                           ppModifier = GetIdentityName(),
                        //                                           ppModifyDate = DateTime.Now,
                        //                                           //    //受检
                        //                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_iqcorder = "4300000000",
                        //                                           Ec_iqcnote = "内部管理",
                        //                                           iqcModifier = GetIdentityName(),
                        //                                           iqcModifyDate = DateTime.Now,
                        //                                           //    //制一
                        //                                           Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_p1dline = "内部管理",
                        //                                           Ec_p1dlot = "内部管理",
                        //                                           Ec_p1dnote = "内部管理",
                        //                                           p1dModifier = GetIdentityName(),
                        //                                           p1dModifyDate = DateTime.Now,
                        //                                           //    //制二
                        //                                           Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_p2dlot = "内部管理",
                        //                                           Ec_p2dnote = "内部管理",
                        //                                           p2dModifier = GetIdentityName(),
                        //                                           p2dModifyDate = DateTime.Now,
                        //                                           //    //品管
                        //                                           Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_qalot = "内部管理",
                        //                                           Ec_qanote = "内部管理",
                        //                                           qaModifier = GetIdentityName(),
                        //                                           qaModifyDate = DateTime.Now,

                        //                                           UDF01 = "NonItems",
                        //                                           UDF02 = "",
                        //                                           UDF03 = "",
                        //                                           UDF04 = "",
                        //                                           UDF05 = "",
                        //                                           UDF06 = "",
                        //                                           UDF51 = 0,
                        //                                           UDF52 = 0,
                        //                                           UDF53 = 0,
                        //                                           UDF54 = 0,
                        //                                           UDF55 = 0,
                        //                                           UDF56 = 0,
                        //                                           IsDeleted = 0,
                        //                                           Remark = "管理区分内部",
                        //                                           Creator = GetIdentityName(),
                        //                                           CreateDate = DateTime.Now,
                        //                                       }).Distinct().ToList();
                        //    DB.BulkInsert(New_NonItemList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 2.新物料为空

                        //    #region 3.采购件非C003

                        //    //1.采购件非C003

                        //    var MMPurchase = from a in q_NotEollist

                        //                     join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                     join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                     where b.D_SAP_ZCA1D_Z010 == "F"
                        //                     where b.D_SAP_ZCA1D_Z030 != "C003"
                        //                     select new
                        //                     {
                        //                         Ec_no = a.D_SAP_ZPABD_S001,
                        //                         Ec_model = c.D_SAP_DEST_Z002,
                        //                         Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                         Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                         Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                         Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                         Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                         Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                         Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                         Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                         Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                         Ec_newset = a.D_SAP_ZPABD_S011,
                        //                         Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                         Ec_change = a.D_SAP_ZPABD_S013,
                        //                         Ec_local = a.D_SAP_ZPABD_S014,
                        //                         Ec_note = a.D_SAP_ZPABD_S015,
                        //                         Ec_process = a.D_SAP_ZPABD_S016,

                        //                         Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                         Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                         Ec_eol = "",
                        //                         IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                     };

                        //    var result_MMPurchase = MMPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                        //                                          select new Pp_Ec_Sub
                        //                                          {
                        //                                              GUID = Guid.NewGuid(),
                        //                                              Ec_no = item.Ec_no,
                        //                                              Ec_model = item.Ec_model,
                        //                                              Ec_bomitem = item.Ec_bomitem,
                        //                                              Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                              Ec_olditem = item.Ec_olditem,
                        //                                              Ec_oldtext = item.Ec_oldtext,
                        //                                              Ec_oldqty = item.Ec_oldqty,
                        //                                              Ec_oldset = item.Ec_oldset,
                        //                                              Ec_newitem = item.Ec_newitem,
                        //                                              Ec_newtext = item.Ec_newtext,
                        //                                              Ec_newqty = item.Ec_newqty,
                        //                                              Ec_newset = item.Ec_newset,
                        //                                              Ec_bomno = item.Ec_bomno,
                        //                                              Ec_change = item.Ec_change,
                        //                                              Ec_local = item.Ec_local,
                        //                                              Ec_note = item.Ec_note,
                        //                                              Ec_process = item.Ec_process,
                        //                                              Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_procurement = item.Ec_procurement,
                        //                                              Ec_location = item.Ec_location,
                        //                                              IsCheck = item.IsCheck,
                        //                                              IsManage = 2,
                        //                                              Ec_eol = item.Ec_eol,

                        //                                              Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                              Ec_pmcdate = "",
                        //                                              Ec_pmclot = "",
                        //                                              Ec_pmcmemo = "",
                        //                                              Ec_pmcnote = "",
                        //                                              Ec_bstock = 0,
                        //                                              pmcModifier = GetIdentityName(),
                        //                                              pmcModifyDate = DateTime.Now,

                        //                                              //    //制二
                        //                                              Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_p2dlot = "与制二无关",
                        //                                              Ec_p2dnote = "与制二无关",
                        //                                              p2dModifier = GetIdentityName(),
                        //                                              p2dModifyDate = DateTime.Now,

                        //                                              //    //部管
                        //                                              Ec_mmdate = "",
                        //                                              Ec_mmlot = "",
                        //                                              Ec_mmlotno = "",
                        //                                              Ec_mmnote = "",
                        //                                              mmModifier = GetIdentityName(),
                        //                                              mmModifyDate = DateTime.Now,

                        //                                              //    //采购
                        //                                              Ec_purdate = "",
                        //                                              Ec_purorder = "",
                        //                                              Ec_pursupplier = "",
                        //                                              Ec_purnote = "",
                        //                                              ppModifier = GetIdentityName(),
                        //                                              ppModifyDate = DateTime.Now,

                        //                                              //    //受检
                        //                                              Ec_iqcdate = "",
                        //                                              Ec_iqcorder = "",
                        //                                              Ec_iqcnote = "",
                        //                                              iqcModifier = GetIdentityName(),
                        //                                              iqcModifyDate = DateTime.Now,

                        //                                              //    //制一
                        //                                              Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_p1dline = "内部管理",
                        //                                              Ec_p1dlot = "内部管理",
                        //                                              Ec_p1dnote = "内部管理",
                        //                                              p1dModifier = GetIdentityName(),
                        //                                              p1dModifyDate = DateTime.Now,

                        //                                              //    //品管
                        //                                              Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                              Ec_qalot = "内部管理",
                        //                                              Ec_qanote = "内部管理",
                        //                                              qaModifier = GetIdentityName(),
                        //                                              qaModifyDate = DateTime.Now,

                        //                                              UDF01 = "MMPurchase",
                        //                                              UDF02 = "",
                        //                                              UDF03 = "",
                        //                                              UDF04 = "",
                        //                                              UDF05 = "",
                        //                                              UDF06 = "",
                        //                                              UDF51 = 0,
                        //                                              UDF52 = 0,
                        //                                              UDF53 = 0,
                        //                                              UDF54 = 0,
                        //                                              UDF55 = 0,
                        //                                              UDF56 = 0,
                        //                                              IsDeleted = 0,
                        //                                              Remark = "管理区分内部",
                        //                                              Creator = GetIdentityName(),
                        //                                              CreateDate = DateTime.Now,
                        //                                          }).Distinct().ToList();
                        //    DB.BulkInsert(New_MMPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 3.采购件非C003

                        //    #region 4.采购件C003

                        //    //1.采购件C003
                        //    //1.采购件非C003

                        //    var P2dPurchase = from a in q_NotEollist

                        //                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                        //                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //                      where b.D_SAP_ZCA1D_Z010 == "F"
                        //                      where b.D_SAP_ZCA1D_Z030 == "C003"
                        //                      select new
                        //                      {
                        //                          Ec_no = a.D_SAP_ZPABD_S001,
                        //                          Ec_model = c.D_SAP_DEST_Z002,
                        //                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                        //                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        //                          Ec_olditem = a.D_SAP_ZPABD_S004,
                        //                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                        //                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                        //                          Ec_oldset = a.D_SAP_ZPABD_S007,
                        //                          Ec_newitem = a.D_SAP_ZPABD_S008,
                        //                          Ec_newtext = a.D_SAP_ZPABD_S009,
                        //                          Ec_newqty = a.D_SAP_ZPABD_S010,
                        //                          Ec_newset = a.D_SAP_ZPABD_S011,
                        //                          Ec_bomno = a.D_SAP_ZPABD_S012,
                        //                          Ec_change = a.D_SAP_ZPABD_S013,
                        //                          Ec_local = a.D_SAP_ZPABD_S014,
                        //                          Ec_note = a.D_SAP_ZPABD_S015,
                        //                          Ec_process = a.D_SAP_ZPABD_S016,

                        //                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        //                          Ec_location = b.D_SAP_ZCA1D_Z030,
                        //                          Ec_eol = "",
                        //                          IsCheck = b.D_SAP_ZCA1D_Z019,
                        //                      };

                        //    var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        //    List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                        //                                                  select new Pp_Ec_Sub
                        //                                                  {
                        //                                                      GUID = Guid.NewGuid(),
                        //                                                      Ec_no = item.Ec_no,
                        //                                                      Ec_model = item.Ec_model,
                        //                                                      Ec_bomitem = item.Ec_bomitem,
                        //                                                      Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                                      Ec_olditem = item.Ec_olditem,
                        //                                                      Ec_oldtext = item.Ec_oldtext,
                        //                                                      Ec_oldqty = item.Ec_oldqty,
                        //                                                      Ec_oldset = item.Ec_oldset,
                        //                                                      Ec_newitem = item.Ec_newitem,
                        //                                                      Ec_newtext = item.Ec_newtext,
                        //                                                      Ec_newqty = item.Ec_newqty,
                        //                                                      Ec_newset = item.Ec_newset,
                        //                                                      Ec_bomno = item.Ec_bomno,
                        //                                                      Ec_change = item.Ec_change,
                        //                                                      Ec_local = item.Ec_local,
                        //                                                      Ec_note = item.Ec_note,
                        //                                                      Ec_process = item.Ec_process,
                        //                                                      Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_procurement = item.Ec_procurement,
                        //                                                      Ec_location = item.Ec_location,
                        //                                                      IsCheck = item.IsCheck,
                        //                                                      IsManage = IsManages,
                        //                                                      Ec_eol = item.Ec_eol,

                        //                                                      Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                        //                                                      Ec_pmcdate = "",
                        //                                                      Ec_pmclot = "",
                        //                                                      Ec_pmcmemo = "",
                        //                                                      Ec_pmcnote = "",
                        //                                                      Ec_bstock = 0,
                        //                                                      pmcModifier = GetIdentityName(),
                        //                                                      pmcModifyDate = DateTime.Now,

                        //                                                      //    //制二
                        //                                                      Ec_p2ddate = "",
                        //                                                      Ec_p2dlot = "",
                        //                                                      Ec_p2dnote = "",
                        //                                                      p2dModifier = GetIdentityName(),
                        //                                                      p2dModifyDate = DateTime.Now,

                        //                                                      //    //部管
                        //                                                      Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_mmlot = "内部管理",
                        //                                                      Ec_mmlotno = "内部管理",
                        //                                                      Ec_mmnote = "内部管理",
                        //                                                      mmModifier = GetIdentityName(),
                        //                                                      mmModifyDate = DateTime.Now,

                        //                                                      //    //采购
                        //                                                      Ec_purdate = "",
                        //                                                      Ec_purorder = "",
                        //                                                      Ec_pursupplier = "",
                        //                                                      Ec_purnote = "",
                        //                                                      ppModifier = GetIdentityName(),
                        //                                                      ppModifyDate = DateTime.Now,

                        //                                                      //    //受检
                        //                                                      Ec_iqcdate = "",
                        //                                                      Ec_iqcorder = "",
                        //                                                      Ec_iqcnote = "",
                        //                                                      iqcModifier = GetIdentityName(),
                        //                                                      iqcModifyDate = DateTime.Now,

                        //                                                      //    //制一
                        //                                                      Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_p1dline = "内部管理",
                        //                                                      Ec_p1dlot = "内部管理",
                        //                                                      Ec_p1dnote = "内部管理",
                        //                                                      p1dModifier = GetIdentityName(),
                        //                                                      p1dModifyDate = DateTime.Now,

                        //                                                      //    //品管
                        //                                                      Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                                      Ec_qalot = "内部管理",
                        //                                                      Ec_qanote = "内部管理",
                        //                                                      qaModifier = GetIdentityName(),
                        //                                                      qaModifyDate = DateTime.Now,

                        //                                                      UDF01 = "P2DPurchase",
                        //                                                      UDF02 = "",
                        //                                                      UDF03 = "",
                        //                                                      UDF04 = "",
                        //                                                      UDF05 = "",
                        //                                                      UDF06 = "",
                        //                                                      UDF51 = 0,
                        //                                                      UDF52 = 0,
                        //                                                      UDF53 = 0,
                        //                                                      UDF54 = 0,
                        //                                                      UDF55 = 0,
                        //                                                      UDF56 = 0,
                        //                                                      IsDeleted = 0,
                        //                                                      Remark = "管理区分内部",
                        //                                                      Creator = GetIdentityName(),
                        //                                                      CreateDate = DateTime.Now,
                        //                                                  }).Distinct().ToList();
                        //    DB.BulkInsert(New_result_P2dPurchaseList);
                        //    DB.BulkSaveChanges();

                        //    #endregion 4.采购件C003
                        //}
                    }
                    if (this.Ec_distinction.SelectedValue == "4")//技术
                    {
                        //if (this.IsManage.SelectedValue == "1")
                        //{

                        #region 1.非采购件

                        //1.非采购件
                        var New_NonPurchase = from a in q_NotEollist
                                              join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                              join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                              where b.D_SAP_ZCA1D_Z010 == "E"
                                              select new
                                              {
                                                  Ec_no = a.D_SAP_ZPABD_S001,
                                                  Ec_model = c.D_SAP_DEST_Z002,
                                                  Ec_bomitem = a.D_SAP_ZPABD_S002,
                                                  Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                                  Ec_olditem = a.D_SAP_ZPABD_S004,
                                                  Ec_oldtext = a.D_SAP_ZPABD_S005,
                                                  Ec_oldqty = a.D_SAP_ZPABD_S006,
                                                  Ec_oldset = a.D_SAP_ZPABD_S007,
                                                  Ec_newitem = a.D_SAP_ZPABD_S008,
                                                  Ec_newtext = a.D_SAP_ZPABD_S009,
                                                  Ec_newqty = a.D_SAP_ZPABD_S010,
                                                  Ec_newset = a.D_SAP_ZPABD_S011,
                                                  Ec_bomno = a.D_SAP_ZPABD_S012,
                                                  Ec_change = a.D_SAP_ZPABD_S013,
                                                  Ec_local = a.D_SAP_ZPABD_S014,
                                                  Ec_note = a.D_SAP_ZPABD_S015,
                                                  Ec_process = a.D_SAP_ZPABD_S016,

                                                  Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                                  Ec_location = b.D_SAP_ZCA1D_Z030,
                                                  Ec_eol = "",
                                                  IsCheck = b.D_SAP_ZCA1D_Z019,
                                              };

                        var result_NonPurchase = New_NonPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                                                               select new Pp_Ec_Sub
                                                               {
                                                                   GUID = Guid.NewGuid(),
                                                                   Ec_no = item.Ec_no,
                                                                   Ec_model = item.Ec_model,
                                                                   Ec_bomitem = item.Ec_bomitem,
                                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                                   Ec_olditem = item.Ec_olditem,
                                                                   Ec_oldtext = item.Ec_oldtext,
                                                                   Ec_oldqty = item.Ec_oldqty,
                                                                   Ec_oldset = item.Ec_oldset,
                                                                   Ec_newitem = item.Ec_newitem,
                                                                   Ec_newtext = item.Ec_newtext,
                                                                   Ec_newqty = item.Ec_newqty,
                                                                   Ec_newset = item.Ec_newset,
                                                                   Ec_bomno = item.Ec_bomno,
                                                                   Ec_change = item.Ec_change,
                                                                   Ec_local = item.Ec_local,
                                                                   Ec_note = item.Ec_note,
                                                                   Ec_process = item.Ec_process,
                                                                   Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_procurement = item.Ec_procurement,
                                                                   Ec_location = item.Ec_location,
                                                                   IsCheck = item.IsCheck,
                                                                   IsManage = 3,
                                                                   IsMmManage = 0,
                                                                   IsAssyManage = 0,
                                                                   IsPcbaManage = 0,
                                                                   Ec_eol = item.Ec_eol,

                                                                   Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                   Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_pmclot = "自然切换",
                                                                   Ec_pmcmemo = "自然切换",
                                                                   Ec_pmcnote = "自然切换",
                                                                   Ec_bstock = 0,
                                                                   pmcModifier = GetIdentityName(),
                                                                   pmcModifyDate = DateTime.Now,

                                                                   //    //制二
                                                                   Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_p2dlot = "自然切换",
                                                                   Ec_p2dnote = "自然切换",
                                                                   p2dModifier = GetIdentityName(),
                                                                   p2dModifyDate = DateTime.Now,

                                                                   //    //部管
                                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_mmlot = "自然切换",
                                                                   Ec_mmlotno = "自然切换",
                                                                   Ec_mmnote = "自然切换",
                                                                   mmModifier = GetIdentityName(),
                                                                   mmModifyDate = DateTime.Now,

                                                                   //    //采购
                                                                   Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_purorder = "4300000000",
                                                                   Ec_pursupplier = "H200000",
                                                                   Ec_purnote = "自然切换",
                                                                   ppModifier = GetIdentityName(),
                                                                   ppModifyDate = DateTime.Now,

                                                                   //    //受检
                                                                   Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_iqcorder = "4300000000",
                                                                   Ec_iqcnote = "自然切换",
                                                                   iqcModifier = GetIdentityName(),
                                                                   iqcModifyDate = DateTime.Now,

                                                                   //    //制一
                                                                   Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_p1dline = "自然切换",
                                                                   Ec_p1dlot = "自然切换",
                                                                   Ec_p1dnote = "自然切换",
                                                                   p1dModifier = GetIdentityName(),
                                                                   p1dModifyDate = DateTime.Now,

                                                                   //    //品管
                                                                   Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                   Ec_qalot = "自然切换",
                                                                   Ec_qanote = "自然切换",
                                                                   qaModifier = GetIdentityName(),
                                                                   qaModifyDate = DateTime.Now,

                                                                   UDF01 = "NonPurchase",
                                                                   UDF02 = "",
                                                                   UDF03 = "",
                                                                   UDF04 = "",
                                                                   UDF05 = "",
                                                                   UDF06 = "",
                                                                   UDF51 = 0,
                                                                   UDF52 = 0,
                                                                   UDF53 = 0,
                                                                   UDF54 = 0,
                                                                   UDF55 = 0,
                                                                   UDF56 = 0,
                                                                   IsDeleted = 0,
                                                                   Remark = "管理区分技术课",
                                                                   Creator = GetIdentityName(),
                                                                   CreateDate = DateTime.Now,
                                                               }).Distinct().ToList();
                        DB.BulkInsert(New_NonPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 1.非采购件

                        #region 2.新物料为空

                        var NonItem = from a in q_NotEollist
                                      join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S004 equals b.D_SAP_ZCA1D_Z002
                                      join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                      where a.D_SAP_ZPABD_S008 == "0"
                                      //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                      //                                   where d.D_SAP_ZCA1D_Z034 == ""
                                      //                                   select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                      //                                   select d.D_SAP_DEST_Z001)
                                      //                                 .Contains(a.D_SAP_ZPABD_S002)
                                      //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                                      //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                                      //                                    select d.D_SAP_ZCA1D_Z002)
                                      //                                .Contains(a.D_SAP_ZPABD_S008)
                                      where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                                      //where b.Ec_no == strecn
                                      //where a.Prodate == sdate//投入日期
                                      select new
                                      {
                                          Ec_no = a.D_SAP_ZPABD_S001,
                                          Ec_model = c.D_SAP_DEST_Z002,
                                          Ec_bomitem = a.D_SAP_ZPABD_S002,
                                          Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                          Ec_olditem = a.D_SAP_ZPABD_S004,
                                          Ec_oldtext = a.D_SAP_ZPABD_S005,
                                          Ec_oldqty = a.D_SAP_ZPABD_S006,
                                          Ec_oldset = a.D_SAP_ZPABD_S007,
                                          Ec_newitem = a.D_SAP_ZPABD_S008,
                                          Ec_newtext = a.D_SAP_ZPABD_S009,
                                          Ec_newqty = a.D_SAP_ZPABD_S010,
                                          Ec_newset = a.D_SAP_ZPABD_S011,
                                          Ec_bomno = a.D_SAP_ZPABD_S012,
                                          Ec_change = a.D_SAP_ZPABD_S013,
                                          Ec_local = a.D_SAP_ZPABD_S014,
                                          Ec_note = a.D_SAP_ZPABD_S015,
                                          Ec_process = a.D_SAP_ZPABD_S016,
                                          Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                          Ec_location = b.D_SAP_ZCA1D_Z030,
                                          Ec_eol = "",
                                          IsCheck = "",
                                      };
                        var result_NonItem = NonItem.Distinct().ToList();
                        List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = Guid.NewGuid(),
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               Ec_eol = item.Ec_eol,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = 3,
                                                               IsMmManage = 0,
                                                               IsAssyManage = 0,
                                                               IsPcbaManage = 0,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),
                                                               //    //生管
                                                               Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_pmclot = "自然切换",
                                                               Ec_pmcmemo = "自然切换",
                                                               Ec_pmcnote = "自然切换",
                                                               Ec_bstock = 0,
                                                               pmcModifier = GetIdentityName(),
                                                               pmcModifyDate = DateTime.Now,
                                                               //    //部管
                                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_mmlot = "自然切换",
                                                               Ec_mmlotno = "4400000",
                                                               Ec_mmnote = "自然切换",
                                                               mmModifier = GetIdentityName(),
                                                               mmModifyDate = DateTime.Now,
                                                               //    //采购
                                                               Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_purorder = "4300000000",
                                                               Ec_pursupplier = "H200000",
                                                               Ec_purnote = "自然切换",
                                                               ppModifier = GetIdentityName(),
                                                               ppModifyDate = DateTime.Now,
                                                               //    //受检
                                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_iqcorder = "4300000000",
                                                               Ec_iqcnote = "自然切换",
                                                               iqcModifier = GetIdentityName(),
                                                               iqcModifyDate = DateTime.Now,
                                                               //    //制一
                                                               Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_p1dline = "自然切换",
                                                               Ec_p1dlot = "自然切换",
                                                               Ec_p1dnote = "自然切换",
                                                               p1dModifier = GetIdentityName(),
                                                               p1dModifyDate = DateTime.Now,
                                                               //    //制二
                                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_p2dlot = "自然切换",
                                                               Ec_p2dnote = "自然切换",
                                                               p2dModifier = GetIdentityName(),
                                                               p2dModifyDate = DateTime.Now,
                                                               //    //品管
                                                               Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_qalot = "自然切换",
                                                               Ec_qanote = "自然切换",
                                                               qaModifier = GetIdentityName(),
                                                               qaModifyDate = DateTime.Now,

                                                               UDF01 = "NonItems",
                                                               UDF02 = "",
                                                               UDF03 = "",
                                                               UDF04 = "",
                                                               UDF05 = "",
                                                               UDF06 = "",
                                                               UDF51 = 0,
                                                               UDF52 = 0,
                                                               UDF53 = 0,
                                                               UDF54 = 0,
                                                               UDF55 = 0,
                                                               UDF56 = 0,
                                                               IsDeleted = 0,
                                                               Remark = "管理区分技术课",
                                                               Creator = GetIdentityName(),
                                                               CreateDate = DateTime.Now,
                                                           }).Distinct().ToList();
                        DB.BulkInsert(New_NonItemList);
                        DB.BulkSaveChanges();

                        #endregion 2.新物料为空

                        #region 3.采购件非C003

                        //1.采购件非C003

                        var MMPurchase = from a in q_NotEollist

                                         join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                         join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                         where b.D_SAP_ZCA1D_Z010 == "F"
                                         where b.D_SAP_ZCA1D_Z030 != "C003"
                                         select new
                                         {
                                             Ec_no = a.D_SAP_ZPABD_S001,
                                             Ec_model = c.D_SAP_DEST_Z002,
                                             Ec_bomitem = a.D_SAP_ZPABD_S002,
                                             Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                             Ec_olditem = a.D_SAP_ZPABD_S004,
                                             Ec_oldtext = a.D_SAP_ZPABD_S005,
                                             Ec_oldqty = a.D_SAP_ZPABD_S006,
                                             Ec_oldset = a.D_SAP_ZPABD_S007,
                                             Ec_newitem = a.D_SAP_ZPABD_S008,
                                             Ec_newtext = a.D_SAP_ZPABD_S009,
                                             Ec_newqty = a.D_SAP_ZPABD_S010,
                                             Ec_newset = a.D_SAP_ZPABD_S011,
                                             Ec_bomno = a.D_SAP_ZPABD_S012,
                                             Ec_change = a.D_SAP_ZPABD_S013,
                                             Ec_local = a.D_SAP_ZPABD_S014,
                                             Ec_note = a.D_SAP_ZPABD_S015,
                                             Ec_process = a.D_SAP_ZPABD_S016,

                                             Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                             Ec_location = b.D_SAP_ZCA1D_Z030,
                                             Ec_eol = "",
                                             IsCheck = b.D_SAP_ZCA1D_Z019,
                                         };

                        var result_MMPurchase = MMPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                                                              select new Pp_Ec_Sub
                                                              {
                                                                  GUID = Guid.NewGuid(),
                                                                  Ec_no = item.Ec_no,
                                                                  Ec_model = item.Ec_model,
                                                                  Ec_bomitem = item.Ec_bomitem,
                                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                                  Ec_olditem = item.Ec_olditem,
                                                                  Ec_oldtext = item.Ec_oldtext,
                                                                  Ec_oldqty = item.Ec_oldqty,
                                                                  Ec_oldset = item.Ec_oldset,
                                                                  Ec_newitem = item.Ec_newitem,
                                                                  Ec_newtext = item.Ec_newtext,
                                                                  Ec_newqty = item.Ec_newqty,
                                                                  Ec_newset = item.Ec_newset,
                                                                  Ec_bomno = item.Ec_bomno,
                                                                  Ec_change = item.Ec_change,
                                                                  Ec_local = item.Ec_local,
                                                                  Ec_note = item.Ec_note,
                                                                  Ec_process = item.Ec_process,
                                                                  Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_procurement = item.Ec_procurement,
                                                                  Ec_location = item.Ec_location,
                                                                  IsCheck = item.IsCheck,
                                                                  IsManage = 3,
                                                                  IsMmManage = 0,
                                                                  IsAssyManage = 0,
                                                                  IsPcbaManage = 0,
                                                                  Ec_eol = item.Ec_eol,

                                                                  Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                  Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_pmclot = "自然切换",
                                                                  Ec_pmcmemo = "自然切换",
                                                                  Ec_pmcnote = "自然切换",
                                                                  Ec_bstock = 0,
                                                                  pmcModifier = GetIdentityName(),
                                                                  pmcModifyDate = DateTime.Now,

                                                                  //    //制二
                                                                  Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p2dlot = "自然切换",
                                                                  Ec_p2dnote = "自然切换",
                                                                  p2dModifier = GetIdentityName(),
                                                                  p2dModifyDate = DateTime.Now,

                                                                  //    //部管
                                                                  Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_mmlot = "自然切换",
                                                                  Ec_mmlotno = "自然切换",
                                                                  Ec_mmnote = "自然切换",
                                                                  mmModifier = GetIdentityName(),
                                                                  mmModifyDate = DateTime.Now,

                                                                  //    //采购
                                                                  Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_purorder = "4300000000",
                                                                  Ec_pursupplier = "H200000",
                                                                  Ec_purnote = "自然切换",
                                                                  ppModifier = GetIdentityName(),
                                                                  ppModifyDate = DateTime.Now,

                                                                  //    //受检
                                                                  Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_iqcorder = "4300000000",
                                                                  Ec_iqcnote = "自然切换",
                                                                  iqcModifier = GetIdentityName(),
                                                                  iqcModifyDate = DateTime.Now,

                                                                  //    //制一
                                                                  Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_p1dline = "自然切换",
                                                                  Ec_p1dlot = "自然切换",
                                                                  Ec_p1dnote = "自然切换",
                                                                  p1dModifier = GetIdentityName(),
                                                                  p1dModifyDate = DateTime.Now,

                                                                  //    //品管
                                                                  Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_qalot = "自然切换",
                                                                  Ec_qanote = "自然切换",
                                                                  qaModifier = GetIdentityName(),
                                                                  qaModifyDate = DateTime.Now,

                                                                  UDF01 = "MMPurchase",
                                                                  UDF02 = "",
                                                                  UDF03 = "",
                                                                  UDF04 = "",
                                                                  UDF05 = "",
                                                                  UDF06 = "",
                                                                  UDF51 = 0,
                                                                  UDF52 = 0,
                                                                  UDF53 = 0,
                                                                  UDF54 = 0,
                                                                  UDF55 = 0,
                                                                  UDF56 = 0,
                                                                  IsDeleted = 0,
                                                                  Remark = "管理区分技术课",
                                                                  Creator = GetIdentityName(),
                                                                  CreateDate = DateTime.Now,
                                                              }).Distinct().ToList();
                        DB.BulkInsert(New_MMPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 3.采购件非C003

                        #region 4.采购件C003

                        //1.采购件C003
                        //1.采购件非C003

                        var P2dPurchase = from a in q_NotEollist

                                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                                          where b.D_SAP_ZCA1D_Z010 == "F"
                                          where b.D_SAP_ZCA1D_Z030 == "C003"
                                          select new
                                          {
                                              Ec_no = a.D_SAP_ZPABD_S001,
                                              Ec_model = c.D_SAP_DEST_Z002,
                                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                                              Ec_olditem = a.D_SAP_ZPABD_S004,
                                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                                              Ec_oldset = a.D_SAP_ZPABD_S007,
                                              Ec_newitem = a.D_SAP_ZPABD_S008,
                                              Ec_newtext = a.D_SAP_ZPABD_S009,
                                              Ec_newqty = a.D_SAP_ZPABD_S010,
                                              Ec_newset = a.D_SAP_ZPABD_S011,
                                              Ec_bomno = a.D_SAP_ZPABD_S012,
                                              Ec_change = a.D_SAP_ZPABD_S013,
                                              Ec_local = a.D_SAP_ZPABD_S014,
                                              Ec_note = a.D_SAP_ZPABD_S015,
                                              Ec_process = a.D_SAP_ZPABD_S016,

                                              Ec_procurement = b.D_SAP_ZCA1D_Z010,
                                              Ec_location = b.D_SAP_ZCA1D_Z030,
                                              Ec_eol = "",
                                              IsCheck = b.D_SAP_ZCA1D_Z019,
                                          };

                        var result_P2dPurchase = P2dPurchase.Distinct().ToList();
                        List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                                                                      select new Pp_Ec_Sub
                                                                      {
                                                                          GUID = Guid.NewGuid(),
                                                                          Ec_no = item.Ec_no,
                                                                          Ec_model = item.Ec_model,
                                                                          Ec_bomitem = item.Ec_bomitem,
                                                                          Ec_bomsubitem = item.Ec_bomsubitem,
                                                                          Ec_olditem = item.Ec_olditem,
                                                                          Ec_oldtext = item.Ec_oldtext,
                                                                          Ec_oldqty = item.Ec_oldqty,
                                                                          Ec_oldset = item.Ec_oldset,
                                                                          Ec_newitem = item.Ec_newitem,
                                                                          Ec_newtext = item.Ec_newtext,
                                                                          Ec_newqty = item.Ec_newqty,
                                                                          Ec_newset = item.Ec_newset,
                                                                          Ec_bomno = item.Ec_bomno,
                                                                          Ec_change = item.Ec_change,
                                                                          Ec_local = item.Ec_local,
                                                                          Ec_note = item.Ec_note,
                                                                          Ec_process = item.Ec_process,
                                                                          Ec_bomdate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_procurement = item.Ec_procurement,
                                                                          Ec_location = item.Ec_location,
                                                                          IsCheck = item.IsCheck,
                                                                          IsManage = 3,
                                                                          IsMmManage = 0,
                                                                          IsAssyManage = 0,
                                                                          IsPcbaManage = 0,
                                                                          Ec_eol = item.Ec_eol,

                                                                          Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                                          Ec_pmcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_pmclot = "自然切换",
                                                                          Ec_pmcmemo = "自然切换",
                                                                          Ec_pmcnote = "自然切换",
                                                                          Ec_bstock = 0,
                                                                          pmcModifier = GetIdentityName(),
                                                                          pmcModifyDate = DateTime.Now,

                                                                          //    //制二
                                                                          Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_p2dlot = "自然切换",
                                                                          Ec_p2dnote = "自然切换",
                                                                          p2dModifier = GetIdentityName(),
                                                                          p2dModifyDate = DateTime.Now,

                                                                          //    //部管
                                                                          Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_mmlot = "自然切换",
                                                                          Ec_mmlotno = "自然切换",
                                                                          Ec_mmnote = "自然切换",
                                                                          mmModifier = GetIdentityName(),
                                                                          mmModifyDate = DateTime.Now,

                                                                          //    //采购
                                                                          Ec_purdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_purorder = "4300000000",
                                                                          Ec_pursupplier = "H200000",
                                                                          Ec_purnote = "自然切换",
                                                                          ppModifier = GetIdentityName(),
                                                                          ppModifyDate = DateTime.Now,

                                                                          //    //受检
                                                                          Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_iqcorder = "4300000000",
                                                                          Ec_iqcnote = "自然切换",
                                                                          iqcModifier = GetIdentityName(),
                                                                          iqcModifyDate = DateTime.Now,

                                                                          //    //制一
                                                                          Ec_p1ddate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_p1dline = "自然切换",
                                                                          Ec_p1dlot = "自然切换",
                                                                          Ec_p1dnote = "自然切换",
                                                                          p1dModifier = GetIdentityName(),
                                                                          p1dModifyDate = DateTime.Now,

                                                                          //    //品管
                                                                          Ec_qadate = DateTime.Now.ToString("yyyyMMdd"),
                                                                          Ec_qalot = "自然切换",
                                                                          Ec_qanote = "自然切换",
                                                                          qaModifier = GetIdentityName(),
                                                                          qaModifyDate = DateTime.Now,

                                                                          UDF01 = "P2DPurchase",
                                                                          UDF02 = "",
                                                                          UDF03 = "",
                                                                          UDF04 = "",
                                                                          UDF05 = "",
                                                                          UDF06 = "",
                                                                          UDF51 = 0,
                                                                          UDF52 = 0,
                                                                          UDF53 = 0,
                                                                          UDF54 = 0,
                                                                          UDF55 = 0,
                                                                          UDF56 = 0,
                                                                          IsDeleted = 0,
                                                                          Remark = "管理区分技术课",
                                                                          Creator = GetIdentityName(),
                                                                          CreateDate = DateTime.Now,
                                                                      }).Distinct().ToList();
                        DB.BulkInsert(New_result_P2dPurchaseList);
                        DB.BulkSaveChanges();

                        #endregion 4.采购件C003
                    }
                    if (this.Ec_distinction.SelectedValue != "4")
                    {
                        #region 5.更新免检&要检

                        var CheckItem = from a in DB.Pp_Ec_Subs
                                        where a.Ec_no.Contains(Ec_no.Text)
                                        where a.Ec_newitem != "0"
                                        where a.IsDeleted == 0
                                        select a;
                        var UpdateIsCheck_NO = from a in CheckItem
                                               join b in DB.Pp_SapMaterials on a.Ec_newitem equals b.D_SAP_ZCA1D_Z002
                                               //where b.D_SAP_ZCA1D_Z019 != "X"
                                               select new
                                               {
                                                   a.GUID,
                                                   a.Ec_no,
                                                   a.Ec_model,
                                                   a.Ec_bomitem,
                                                   a.Ec_bomsubitem,
                                                   a.Ec_olditem,
                                                   a.Ec_oldtext,
                                                   a.Ec_oldqty,
                                                   a.Ec_oldset,
                                                   a.Ec_newitem,
                                                   a.Ec_newtext,
                                                   a.Ec_newqty,
                                                   a.Ec_newset,
                                                   a.Ec_bomno,
                                                   a.Ec_procurement,
                                                   a.Ec_location,
                                                   a.IsCheck,
                                                   a.IsManage,
                                                   a.IsMmManage,
                                                   a.IsPcbaManage,
                                                   a.IsAssyManage,
                                                   a.Ec_eol,
                                                   a.Ec_change,
                                                   a.Ec_local,
                                                   a.Ec_note,
                                                   a.Ec_process,
                                                   a.Ec_bomdate,
                                                   a.Ec_entrydate,
                                                   a.Ec_pmcdate,
                                                   a.Ec_pmclot,
                                                   a.Ec_pmcmemo,
                                                   a.Ec_pmcnote,
                                                   a.Ec_bstock,
                                                   a.pmcModifier,
                                                   a.pmcModifyDate,
                                                   a.Ec_p2ddate,
                                                   a.Ec_p2dlot,
                                                   a.Ec_p2dnote,
                                                   a.p2dModifier,
                                                   a.p2dModifyDate,
                                                   a.Ec_mmdate,
                                                   a.Ec_mmlot,
                                                   a.Ec_mmlotno,
                                                   a.Ec_mmnote,
                                                   a.mmModifier,
                                                   a.mmModifyDate,
                                                   a.Ec_purdate,
                                                   a.Ec_purorder,
                                                   a.Ec_pursupplier,
                                                   a.Ec_purnote,
                                                   a.ppModifier,
                                                   a.ppModifyDate,
                                                   a.Ec_iqcdate,
                                                   a.Ec_iqcorder,
                                                   a.Ec_iqcnote,
                                                   a.iqcModifier,
                                                   a.iqcModifyDate,
                                                   a.Ec_p1ddate,
                                                   a.Ec_p1dline,
                                                   a.Ec_p1dlot,
                                                   a.Ec_p1dnote,
                                                   a.p1dModifier,
                                                   a.p1dModifyDate,
                                                   a.Ec_qadate,
                                                   a.Ec_qalot,
                                                   a.Ec_qanote,
                                                   a.qaModifier,
                                                   a.qaModifyDate,
                                                   a.UDF01,
                                                   a.UDF02,
                                                   a.UDF03,
                                                   a.UDF04,
                                                   a.UDF05,
                                                   a.UDF06,
                                                   a.UDF51,
                                                   a.UDF52,
                                                   a.UDF53,
                                                   a.UDF54,
                                                   a.UDF55,
                                                   a.UDF56,
                                                   a.IsDeleted,
                                                   a.Remark,
                                                   a.Creator,
                                                   a.CreateDate,
                                                   a.Modifier,
                                                   a.ModifyDate,
                                               };

                        var IsCheckList_NO = UpdateIsCheck_NO.ToList().Distinct();
                        List<Pp_Ec_Sub> UpdateCheckList_NO = (from item in IsCheckList_NO
                                                              select new Pp_Ec_Sub
                                                              {
                                                                  GUID = item.GUID,
                                                                  Ec_no = item.Ec_no,
                                                                  Ec_model = item.Ec_model,
                                                                  Ec_bomitem = item.Ec_bomitem,
                                                                  Ec_bomsubitem = item.Ec_bomsubitem,
                                                                  Ec_olditem = item.Ec_olditem,
                                                                  Ec_oldtext = item.Ec_oldtext,
                                                                  Ec_oldqty = item.Ec_oldqty,
                                                                  Ec_oldset = item.Ec_oldset,
                                                                  Ec_newitem = item.Ec_newitem,
                                                                  Ec_newtext = item.Ec_newtext,
                                                                  Ec_newqty = item.Ec_newqty,
                                                                  Ec_newset = item.Ec_newset,
                                                                  Ec_bomno = item.Ec_bomno,
                                                                  Ec_change = item.Ec_change,
                                                                  Ec_local = item.Ec_local,
                                                                  Ec_note = item.Ec_note,
                                                                  Ec_process = item.Ec_process,
                                                                  Ec_bomdate = item.Ec_bomdate,

                                                                  Ec_procurement = item.Ec_procurement,
                                                                  Ec_location = item.Ec_location,
                                                                  IsCheck = item.IsCheck,
                                                                  IsManage = item.IsManage,
                                                                  IsMmManage = item.IsMmManage,
                                                                  IsPcbaManage = item.IsPcbaManage,
                                                                  IsAssyManage = item.IsAssyManage,
                                                                  Ec_eol = item.Ec_eol,

                                                                  Ec_entrydate = item.Ec_entrydate,
                                                                  Ec_pmcdate = item.Ec_pmcdate,
                                                                  Ec_pmclot = item.Ec_pmclot,
                                                                  Ec_pmcmemo = item.Ec_pmcmemo,
                                                                  Ec_pmcnote = item.Ec_pmcnote,
                                                                  Ec_bstock = item.Ec_bstock,
                                                                  pmcModifier = item.pmcModifier,
                                                                  pmcModifyDate = item.pmcModifyDate,
                                                                  Ec_p2ddate = item.Ec_p2ddate,
                                                                  Ec_p2dlot = item.Ec_p2dlot,
                                                                  Ec_p2dnote = item.Ec_p2dnote,
                                                                  p2dModifier = item.p2dModifier,
                                                                  p2dModifyDate = item.p2dModifyDate,
                                                                  Ec_mmdate = item.Ec_mmdate,
                                                                  Ec_mmlot = item.Ec_mmlot,
                                                                  Ec_mmlotno = item.Ec_mmlotno,
                                                                  Ec_mmnote = item.Ec_mmnote,
                                                                  mmModifier = item.mmModifier,
                                                                  mmModifyDate = item.mmModifyDate,
                                                                  Ec_purdate = item.Ec_purdate,
                                                                  Ec_purorder = item.Ec_purorder,
                                                                  Ec_pursupplier = item.Ec_pursupplier,
                                                                  Ec_purnote = item.Ec_purnote,
                                                                  ppModifier = item.ppModifier,
                                                                  ppModifyDate = item.ppModifyDate,
                                                                  Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                  Ec_iqcorder = "4300000000",
                                                                  Ec_iqcnote = "免检",
                                                                  iqcModifier = item.iqcModifier,
                                                                  iqcModifyDate = item.iqcModifyDate,
                                                                  Ec_p1ddate = item.Ec_p1ddate,
                                                                  Ec_p1dline = item.Ec_p1dline,
                                                                  Ec_p1dlot = item.Ec_p1dlot,
                                                                  Ec_p1dnote = item.Ec_p1dnote,
                                                                  p1dModifier = item.p1dModifier,
                                                                  p1dModifyDate = item.p1dModifyDate,
                                                                  Ec_qadate = item.Ec_qadate,
                                                                  Ec_qalot = item.Ec_qalot,
                                                                  Ec_qanote = item.Ec_qanote,
                                                                  qaModifier = item.qaModifier,
                                                                  qaModifyDate = item.qaModifyDate,
                                                                  UDF01 = item.UDF01,
                                                                  UDF02 = item.UDF02,
                                                                  UDF03 = item.UDF03,
                                                                  UDF04 = item.UDF04,
                                                                  UDF05 = item.UDF05,
                                                                  UDF06 = item.UDF06,
                                                                  UDF51 = item.UDF51,
                                                                  UDF52 = item.UDF52,
                                                                  UDF53 = item.UDF53,
                                                                  UDF54 = item.UDF54,
                                                                  UDF55 = item.UDF55,
                                                                  UDF56 = item.UDF56,
                                                                  IsDeleted = item.IsDeleted,
                                                                  Remark = item.Remark,
                                                                  Creator = item.Creator,
                                                                  CreateDate = item.CreateDate,
                                                                  Modifier = item.Modifier,
                                                                  ModifyDate = item.ModifyDate,
                                                              }).ToList();

                        DB.BulkUpdate(UpdateCheckList_NO);
                        DB.BulkSaveChanges();
                        var UpdateIsCheck_Yes = from a in CheckItem
                                                join b in DB.Pp_SapMaterials on a.Ec_newitem equals b.D_SAP_ZCA1D_Z002
                                                where b.D_SAP_ZCA1D_Z019 == "X"
                                                select new
                                                {
                                                    a.GUID,
                                                    a.Ec_no,
                                                    a.Ec_model,
                                                    a.Ec_bomitem,
                                                    a.Ec_bomsubitem,
                                                    a.Ec_olditem,
                                                    a.Ec_oldtext,
                                                    a.Ec_oldqty,
                                                    a.Ec_oldset,
                                                    a.Ec_newitem,
                                                    a.Ec_newtext,
                                                    a.Ec_newqty,
                                                    a.Ec_newset,
                                                    a.Ec_bomno,
                                                    a.Ec_procurement,
                                                    a.Ec_location,
                                                    a.IsCheck,
                                                    a.IsManage,
                                                    a.IsMmManage,
                                                    a.IsPcbaManage,
                                                    a.IsAssyManage,
                                                    a.Ec_eol,
                                                    a.Ec_change,
                                                    a.Ec_local,
                                                    a.Ec_note,
                                                    a.Ec_process,
                                                    a.Ec_bomdate,
                                                    a.Ec_entrydate,
                                                    a.Ec_pmcdate,
                                                    a.Ec_pmclot,
                                                    a.Ec_pmcmemo,
                                                    a.Ec_pmcnote,
                                                    a.Ec_bstock,
                                                    a.pmcModifier,
                                                    a.pmcModifyDate,
                                                    a.Ec_p2ddate,
                                                    a.Ec_p2dlot,
                                                    a.Ec_p2dnote,
                                                    a.p2dModifier,
                                                    a.p2dModifyDate,
                                                    a.Ec_mmdate,
                                                    a.Ec_mmlot,
                                                    a.Ec_mmlotno,
                                                    a.Ec_mmnote,
                                                    a.mmModifier,
                                                    a.mmModifyDate,
                                                    a.Ec_purdate,
                                                    a.Ec_purorder,
                                                    a.Ec_pursupplier,
                                                    a.Ec_purnote,
                                                    a.ppModifier,
                                                    a.ppModifyDate,
                                                    a.Ec_iqcdate,
                                                    a.Ec_iqcorder,
                                                    a.Ec_iqcnote,
                                                    a.iqcModifier,
                                                    a.iqcModifyDate,
                                                    a.Ec_p1ddate,
                                                    a.Ec_p1dline,
                                                    a.Ec_p1dlot,
                                                    a.Ec_p1dnote,
                                                    a.p1dModifier,
                                                    a.p1dModifyDate,
                                                    a.Ec_qadate,
                                                    a.Ec_qalot,
                                                    a.Ec_qanote,
                                                    a.qaModifier,
                                                    a.qaModifyDate,
                                                    a.UDF01,
                                                    a.UDF02,
                                                    a.UDF03,
                                                    a.UDF04,
                                                    a.UDF05,
                                                    a.UDF06,
                                                    a.UDF51,
                                                    a.UDF52,
                                                    a.UDF53,
                                                    a.UDF54,
                                                    a.UDF55,
                                                    a.UDF56,
                                                    a.IsDeleted,
                                                    a.Remark,
                                                    a.Creator,
                                                    a.CreateDate,
                                                    a.Modifier,
                                                    a.ModifyDate,
                                                };

                        var IsCheckList_Yes = UpdateIsCheck_Yes.ToList().Distinct();
                        List<Pp_Ec_Sub> UpdateCheckList_Yes = (from item in IsCheckList_Yes
                                                               select new Pp_Ec_Sub
                                                               {
                                                                   GUID = item.GUID,
                                                                   Ec_no = item.Ec_no,
                                                                   Ec_model = item.Ec_model,
                                                                   Ec_bomitem = item.Ec_bomitem,
                                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                                   Ec_olditem = item.Ec_olditem,
                                                                   Ec_oldtext = item.Ec_oldtext,
                                                                   Ec_oldqty = item.Ec_oldqty,
                                                                   Ec_oldset = item.Ec_oldset,
                                                                   Ec_newitem = item.Ec_newitem,
                                                                   Ec_newtext = item.Ec_newtext,
                                                                   Ec_newqty = item.Ec_newqty,
                                                                   Ec_newset = item.Ec_newset,
                                                                   Ec_bomno = item.Ec_bomno,
                                                                   Ec_change = item.Ec_change,
                                                                   Ec_local = item.Ec_local,
                                                                   Ec_note = item.Ec_note,
                                                                   Ec_process = item.Ec_process,
                                                                   Ec_bomdate = item.Ec_bomdate,

                                                                   Ec_procurement = item.Ec_procurement,
                                                                   Ec_location = item.Ec_location,
                                                                   IsCheck = item.IsCheck,
                                                                   IsManage = item.IsManage,
                                                                   IsMmManage = item.IsMmManage,
                                                                   IsPcbaManage = item.IsPcbaManage,
                                                                   IsAssyManage = item.IsAssyManage,
                                                                   Ec_eol = item.Ec_eol,

                                                                   Ec_entrydate = item.Ec_entrydate,
                                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                                   Ec_pmclot = item.Ec_pmclot,
                                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                                   Ec_bstock = item.Ec_bstock,
                                                                   pmcModifier = item.pmcModifier,
                                                                   pmcModifyDate = item.pmcModifyDate,
                                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                                   p2dModifier = item.p2dModifier,
                                                                   p2dModifyDate = item.p2dModifyDate,
                                                                   Ec_mmdate = item.Ec_mmdate,
                                                                   Ec_mmlot = item.Ec_mmlot,
                                                                   Ec_mmlotno = item.Ec_mmlotno,
                                                                   Ec_mmnote = item.Ec_mmnote,
                                                                   mmModifier = item.mmModifier,
                                                                   mmModifyDate = item.mmModifyDate,
                                                                   Ec_purdate = item.Ec_purdate,
                                                                   Ec_purorder = item.Ec_purorder,
                                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                                   Ec_purnote = item.Ec_purnote,
                                                                   ppModifier = item.ppModifier,
                                                                   ppModifyDate = item.ppModifyDate,
                                                                   Ec_iqcdate = "",
                                                                   Ec_iqcorder = "",
                                                                   Ec_iqcnote = "",
                                                                   iqcModifier = item.iqcModifier,
                                                                   iqcModifyDate = item.iqcModifyDate,
                                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                                   Ec_p1dline = item.Ec_p1dline,
                                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                                   p1dModifier = item.p1dModifier,
                                                                   p1dModifyDate = item.p1dModifyDate,
                                                                   Ec_qadate = item.Ec_qadate,
                                                                   Ec_qalot = item.Ec_qalot,
                                                                   Ec_qanote = item.Ec_qanote,
                                                                   qaModifier = item.qaModifier,
                                                                   qaModifyDate = item.qaModifyDate,
                                                                   UDF01 = item.UDF01,
                                                                   UDF02 = item.UDF02,
                                                                   UDF03 = item.UDF03,
                                                                   UDF04 = item.UDF04,
                                                                   UDF05 = item.UDF05,
                                                                   UDF06 = item.UDF06,
                                                                   UDF51 = item.UDF51,
                                                                   UDF52 = item.UDF52,
                                                                   UDF53 = item.UDF53,
                                                                   UDF54 = item.UDF54,
                                                                   UDF55 = item.UDF55,
                                                                   UDF56 = item.UDF56,
                                                                   IsDeleted = item.IsDeleted,
                                                                   Remark = item.Remark,
                                                                   Creator = item.Creator,
                                                                   CreateDate = item.CreateDate,
                                                                   Modifier = item.Modifier,
                                                                   ModifyDate = item.ModifyDate,
                                                               }).ToList();

                        DB.BulkUpdate(UpdateCheckList_Yes);
                        DB.BulkSaveChanges();

                        #endregion 5.更新免检&要检

                        #region 7.新物料为空时更新旧物料管理

                        //if (this.IsManage.SelectedValue == "1")
                        //{
                        var P2DPur = from a in DB.Pp_Ec_Subs
                                     where a.Ec_no.Contains(Ec_no.Text)
                                     where a.Ec_newitem == "0"
                                     where a.Ec_procurement == "F"
                                     where a.Ec_location == "C003"
                                     where a.IsDeleted == 0
                                     select a;
                        var P2DPurList = P2DPur.ToList().Distinct();
                        List<Pp_Ec_Sub> UpdateP2DPurList = (from item in P2DPurList
                                                            select new Pp_Ec_Sub
                                                            {
                                                                GUID = item.GUID,
                                                                Ec_no = item.Ec_no,
                                                                Ec_model = item.Ec_model,
                                                                Ec_bomitem = item.Ec_bomitem,
                                                                Ec_bomsubitem = item.Ec_bomsubitem,
                                                                Ec_olditem = item.Ec_olditem,
                                                                Ec_oldtext = item.Ec_oldtext,
                                                                Ec_oldqty = item.Ec_oldqty,
                                                                Ec_oldset = item.Ec_oldset,
                                                                Ec_newitem = item.Ec_newitem,
                                                                Ec_newtext = item.Ec_newtext,
                                                                Ec_newqty = item.Ec_newqty,
                                                                Ec_newset = item.Ec_newset,
                                                                Ec_bomno = item.Ec_bomno,
                                                                Ec_change = item.Ec_change,
                                                                Ec_local = item.Ec_local,
                                                                Ec_note = item.Ec_note,
                                                                Ec_process = item.Ec_process,
                                                                Ec_bomdate = item.Ec_bomdate,

                                                                Ec_procurement = item.Ec_procurement,
                                                                Ec_location = item.Ec_location,
                                                                IsCheck = item.IsCheck,
                                                                IsManage = item.IsManage,
                                                                IsMmManage = item.IsMmManage,
                                                                IsPcbaManage = item.IsPcbaManage,
                                                                IsAssyManage = item.IsAssyManage,
                                                                Ec_eol = item.Ec_eol,

                                                                Ec_entrydate = item.Ec_entrydate,
                                                                Ec_pmcdate = item.Ec_pmcdate,
                                                                Ec_pmclot = item.Ec_pmclot,
                                                                Ec_pmcmemo = item.Ec_pmcmemo,
                                                                Ec_pmcnote = item.Ec_pmcnote,
                                                                Ec_bstock = item.Ec_bstock,
                                                                pmcModifier = item.pmcModifier,
                                                                pmcModifyDate = item.pmcModifyDate,
                                                                Ec_p2ddate = "",
                                                                Ec_p2dlot = "",
                                                                Ec_p2dnote = "",
                                                                p2dModifier = item.p2dModifier,
                                                                p2dModifyDate = item.p2dModifyDate,
                                                                Ec_mmdate = item.Ec_mmdate,
                                                                Ec_mmlot = item.Ec_mmlot,
                                                                Ec_mmlotno = item.Ec_mmlotno,
                                                                Ec_mmnote = item.Ec_mmnote,
                                                                mmModifier = item.mmModifier,
                                                                mmModifyDate = item.mmModifyDate,
                                                                Ec_purdate = item.Ec_purdate,
                                                                Ec_purorder = item.Ec_purorder,
                                                                Ec_pursupplier = item.Ec_pursupplier,
                                                                Ec_purnote = item.Ec_purnote,
                                                                ppModifier = item.ppModifier,
                                                                ppModifyDate = item.ppModifyDate,
                                                                Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                                Ec_iqcorder = "4300000000",
                                                                Ec_iqcnote = "免检",
                                                                iqcModifier = item.iqcModifier,
                                                                iqcModifyDate = item.iqcModifyDate,
                                                                Ec_p1ddate = item.Ec_p1ddate,
                                                                Ec_p1dline = item.Ec_p1dline,
                                                                Ec_p1dlot = item.Ec_p1dlot,
                                                                Ec_p1dnote = item.Ec_p1dnote,
                                                                p1dModifier = item.p1dModifier,
                                                                p1dModifyDate = item.p1dModifyDate,
                                                                Ec_qadate = item.Ec_qadate,
                                                                Ec_qalot = item.Ec_qalot,
                                                                Ec_qanote = item.Ec_qanote,
                                                                qaModifier = item.qaModifier,
                                                                qaModifyDate = item.qaModifyDate,
                                                                UDF01 = item.UDF01,
                                                                UDF02 = item.UDF02,
                                                                UDF03 = item.UDF03,
                                                                UDF04 = item.UDF04,
                                                                UDF05 = item.UDF05,
                                                                UDF06 = item.UDF06,
                                                                UDF51 = item.UDF51,
                                                                UDF52 = item.UDF52,
                                                                UDF53 = item.UDF53,
                                                                UDF54 = item.UDF54,
                                                                UDF55 = item.UDF55,
                                                                UDF56 = item.UDF56,
                                                                IsDeleted = item.IsDeleted,
                                                                Remark = item.Remark,
                                                                Creator = item.Creator,
                                                                CreateDate = item.CreateDate,
                                                                Modifier = item.Modifier,
                                                                ModifyDate = item.ModifyDate,
                                                            }).ToList();

                        DB.BulkUpdate(UpdateP2DPurList);
                        DB.BulkSaveChanges();
                        var MMPur = from a in DB.Pp_Ec_Subs
                                    where a.Ec_no.Contains(Ec_no.Text)
                                    where a.Ec_newitem == "0"
                                    where a.Ec_procurement == "F"
                                    where a.Ec_location != "C003"
                                    where a.IsDeleted == 0
                                    select a;
                        var MMPurList = MMPur.ToList().Distinct();
                        List<Pp_Ec_Sub> UpdateMMPurList = (from item in MMPurList
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = item.GUID,
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = item.Ec_bomdate,

                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = item.IsManage,
                                                               IsMmManage = item.IsMmManage,
                                                               IsPcbaManage = item.IsPcbaManage,
                                                               IsAssyManage = item.IsAssyManage,
                                                               Ec_eol = item.Ec_eol,

                                                               Ec_entrydate = item.Ec_entrydate,
                                                               Ec_pmcdate = item.Ec_pmcdate,
                                                               Ec_pmclot = item.Ec_pmclot,
                                                               Ec_pmcmemo = item.Ec_pmcmemo,
                                                               Ec_pmcnote = item.Ec_pmcnote,
                                                               Ec_bstock = item.Ec_bstock,
                                                               pmcModifier = item.pmcModifier,
                                                               pmcModifyDate = item.pmcModifyDate,
                                                               Ec_p2ddate = item.Ec_p2ddate,
                                                               Ec_p2dlot = item.Ec_p2dlot,
                                                               Ec_p2dnote = item.Ec_p2dnote,
                                                               p2dModifier = item.p2dModifier,
                                                               p2dModifyDate = item.p2dModifyDate,
                                                               Ec_mmdate = "",
                                                               Ec_mmlot = "",
                                                               Ec_mmlotno = "",
                                                               Ec_mmnote = "",
                                                               mmModifier = item.mmModifier,
                                                               mmModifyDate = item.mmModifyDate,
                                                               Ec_purdate = item.Ec_purdate,
                                                               Ec_purorder = item.Ec_purorder,
                                                               Ec_pursupplier = item.Ec_pursupplier,
                                                               Ec_purnote = item.Ec_purnote,
                                                               ppModifier = item.ppModifier,
                                                               ppModifyDate = item.ppModifyDate,
                                                               Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                               Ec_iqcorder = "4300000000",
                                                               Ec_iqcnote = "免检",
                                                               iqcModifier = item.iqcModifier,
                                                               iqcModifyDate = item.iqcModifyDate,
                                                               Ec_p1ddate = item.Ec_p1ddate,
                                                               Ec_p1dline = item.Ec_p1dline,
                                                               Ec_p1dlot = item.Ec_p1dlot,
                                                               Ec_p1dnote = item.Ec_p1dnote,
                                                               p1dModifier = item.p1dModifier,
                                                               p1dModifyDate = item.p1dModifyDate,
                                                               Ec_qadate = item.Ec_qadate,
                                                               Ec_qalot = item.Ec_qalot,
                                                               Ec_qanote = item.Ec_qanote,
                                                               qaModifier = item.qaModifier,
                                                               qaModifyDate = item.qaModifyDate,
                                                               UDF01 = item.UDF01,
                                                               UDF02 = item.UDF02,
                                                               UDF03 = item.UDF03,
                                                               UDF04 = item.UDF04,
                                                               UDF05 = item.UDF05,
                                                               UDF06 = item.UDF06,
                                                               UDF51 = item.UDF51,
                                                               UDF52 = item.UDF52,
                                                               UDF53 = item.UDF53,
                                                               UDF54 = item.UDF54,
                                                               UDF55 = item.UDF55,
                                                               UDF56 = item.UDF56,
                                                               IsDeleted = item.IsDeleted,
                                                               Remark = item.Remark,
                                                               Creator = item.Creator,
                                                               CreateDate = item.CreateDate,
                                                               Modifier = item.Modifier,
                                                               ModifyDate = item.ModifyDate,
                                                           }).ToList();

                        DB.BulkUpdate(UpdateMMPurList);
                        DB.BulkSaveChanges();
                        var P2DNoPur = from a in DB.Pp_Ec_Subs
                                       where a.Ec_no.Contains(Ec_no.Text)
                                       where a.Ec_newitem == "0"
                                       where a.Ec_procurement == "E"
                                       //where a.Ec_location == "C003"
                                       where a.IsDeleted == 0
                                       select a;
                        var P2DNoPurList = P2DNoPur.ToList().Distinct();
                        List<Pp_Ec_Sub> UpdateP2DNoPur = (from item in P2DNoPurList
                                                          select new Pp_Ec_Sub
                                                          {
                                                              GUID = item.GUID,
                                                              Ec_no = item.Ec_no,
                                                              Ec_model = item.Ec_model,
                                                              Ec_bomitem = item.Ec_bomitem,
                                                              Ec_bomsubitem = item.Ec_bomsubitem,
                                                              Ec_olditem = item.Ec_olditem,
                                                              Ec_oldtext = item.Ec_oldtext,
                                                              Ec_oldqty = item.Ec_oldqty,
                                                              Ec_oldset = item.Ec_oldset,
                                                              Ec_newitem = item.Ec_newitem,
                                                              Ec_newtext = item.Ec_newtext,
                                                              Ec_newqty = item.Ec_newqty,
                                                              Ec_newset = item.Ec_newset,
                                                              Ec_bomno = item.Ec_bomno,
                                                              Ec_change = item.Ec_change,
                                                              Ec_local = item.Ec_local,
                                                              Ec_note = item.Ec_note,
                                                              Ec_process = item.Ec_process,
                                                              Ec_bomdate = item.Ec_bomdate,

                                                              Ec_procurement = item.Ec_procurement,
                                                              Ec_location = item.Ec_location,
                                                              IsCheck = item.IsCheck,
                                                              IsManage = item.IsManage,
                                                              IsMmManage = item.IsMmManage,
                                                              IsPcbaManage = item.IsPcbaManage,
                                                              IsAssyManage = item.IsAssyManage,
                                                              Ec_eol = item.Ec_eol,

                                                              Ec_entrydate = item.Ec_entrydate,
                                                              Ec_pmcdate = item.Ec_pmcdate,
                                                              Ec_pmclot = item.Ec_pmclot,
                                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                                              Ec_pmcnote = item.Ec_pmcnote,
                                                              Ec_bstock = item.Ec_bstock,
                                                              pmcModifier = item.pmcModifier,
                                                              pmcModifyDate = item.pmcModifyDate,
                                                              Ec_p2ddate = "",
                                                              Ec_p2dlot = "",
                                                              Ec_p2dnote = "",
                                                              p2dModifier = item.p2dModifier,
                                                              p2dModifyDate = item.p2dModifyDate,
                                                              Ec_mmdate = item.Ec_mmdate,
                                                              Ec_mmlot = item.Ec_mmlot,
                                                              Ec_mmlotno = item.Ec_mmlotno,
                                                              Ec_mmnote = item.Ec_mmnote,
                                                              mmModifier = item.mmModifier,
                                                              mmModifyDate = item.mmModifyDate,
                                                              Ec_purdate = item.Ec_purdate,
                                                              Ec_purorder = item.Ec_purorder,
                                                              Ec_pursupplier = item.Ec_pursupplier,
                                                              Ec_purnote = item.Ec_purnote,
                                                              ppModifier = item.ppModifier,
                                                              ppModifyDate = item.ppModifyDate,
                                                              Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                              Ec_iqcorder = "4300000000",
                                                              Ec_iqcnote = "免检",
                                                              iqcModifier = item.iqcModifier,
                                                              iqcModifyDate = item.iqcModifyDate,
                                                              Ec_p1ddate = item.Ec_p1ddate,
                                                              Ec_p1dline = item.Ec_p1dline,
                                                              Ec_p1dlot = item.Ec_p1dlot,
                                                              Ec_p1dnote = item.Ec_p1dnote,
                                                              p1dModifier = item.p1dModifier,
                                                              p1dModifyDate = item.p1dModifyDate,
                                                              Ec_qadate = item.Ec_qadate,
                                                              Ec_qalot = item.Ec_qalot,
                                                              Ec_qanote = item.Ec_qanote,
                                                              qaModifier = item.qaModifier,
                                                              qaModifyDate = item.qaModifyDate,
                                                              UDF01 = item.UDF01,
                                                              UDF02 = item.UDF02,
                                                              UDF03 = item.UDF03,
                                                              UDF04 = item.UDF04,
                                                              UDF05 = item.UDF05,
                                                              UDF06 = item.UDF06,
                                                              UDF51 = item.UDF51,
                                                              UDF52 = item.UDF52,
                                                              UDF53 = item.UDF53,
                                                              UDF54 = item.UDF54,
                                                              UDF55 = item.UDF55,
                                                              UDF56 = item.UDF56,
                                                              IsDeleted = item.IsDeleted,
                                                              Remark = item.Remark,
                                                              Creator = item.Creator,
                                                              CreateDate = item.CreateDate,
                                                              Modifier = item.Modifier,
                                                              ModifyDate = item.ModifyDate,
                                                          }).ToList();

                        DB.BulkUpdate(UpdateP2DNoPur);
                        DB.BulkSaveChanges();
                        //}
                        //if (this.IsManage.SelectedValue == "0")
                        //{
                        //    var P2DPur = from a in DB.Pp_Ec_Subs
                        //                 where a.Ec_no.Contains(Ec_no.Text)
                        //                 where a.Ec_newitem == "0"
                        //                 where a.Ec_procurement == "F"
                        //                 where a.Ec_location == "C003"
                        //                 where a.IsDeleted == 0
                        //                 select a;
                        //    var P2DPurList = P2DPur.ToList().Distinct();
                        //    List<Pp_Ec_Sub> UpdateP2DPurList = (from item in P2DPurList
                        //                                        select new Pp_Ec_Sub
                        //                                        {
                        //                                            GUID = item.GUID,
                        //                                            Ec_no = item.Ec_no,
                        //                                            Ec_model = item.Ec_model,
                        //                                            Ec_bomitem = item.Ec_bomitem,
                        //                                            Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                            Ec_olditem = item.Ec_olditem,
                        //                                            Ec_oldtext = item.Ec_oldtext,
                        //                                            Ec_oldqty = item.Ec_oldqty,
                        //                                            Ec_oldset = item.Ec_oldset,
                        //                                            Ec_newitem = item.Ec_newitem,
                        //                                            Ec_newtext = item.Ec_newtext,
                        //                                            Ec_newqty = item.Ec_newqty,
                        //                                            Ec_newset = item.Ec_newset,
                        //                                            Ec_bomno = item.Ec_bomno,
                        //                                            Ec_change = item.Ec_change,
                        //                                            Ec_local = item.Ec_local,
                        //                                            Ec_note = item.Ec_note,
                        //                                            Ec_process = item.Ec_process,
                        //                                            Ec_bomdate = item.Ec_bomdate,

                        //                                            Ec_procurement = item.Ec_procurement,
                        //                                            Ec_location = item.Ec_location,
                        //                                            IsCheck = item.IsCheck,
                        //                                            IsManage = IsManages,
                        //                                            Ec_eol = item.Ec_eol,

                        //                                            Ec_entrydate = item.Ec_entrydate,
                        //                                            Ec_pmcdate = item.Ec_pmcdate,
                        //                                            Ec_pmclot = item.Ec_pmclot,
                        //                                            Ec_pmcmemo = item.Ec_pmcmemo,
                        //                                            Ec_pmcnote = item.Ec_pmcnote,
                        //                                            Ec_bstock = item.Ec_bstock,
                        //                                            pmcModifier = item.pmcModifier,
                        //                                            pmcModifyDate = item.pmcModifyDate,
                        //                                            Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                            Ec_p2dlot = "与制二无关",
                        //                                            Ec_p2dnote = "与制二无关",
                        //                                            p2dModifier = GetIdentityName(),
                        //                                            p2dModifyDate = DateTime.Now,
                        //                                            Ec_mmdate = item.Ec_mmdate,
                        //                                            Ec_mmlot = item.Ec_mmlot,
                        //                                            Ec_mmlotno = item.Ec_mmlotno,
                        //                                            Ec_mmnote = item.Ec_mmnote,
                        //                                            mmModifier = item.mmModifier,
                        //                                            mmModifyDate = item.mmModifyDate,
                        //                                            Ec_purdate = item.Ec_purdate,
                        //                                            Ec_purorder = item.Ec_purorder,
                        //                                            Ec_pursupplier = item.Ec_pursupplier,
                        //                                            Ec_purnote = item.Ec_purnote,
                        //                                            ppModifier = item.ppModifier,
                        //                                            ppModifyDate = item.ppModifyDate,
                        //                                            Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                            Ec_iqcorder = "4300000000",
                        //                                            Ec_iqcnote = "免检",
                        //                                            iqcModifier = GetIdentityName(),
                        //                                            iqcModifyDate = DateTime.Now,
                        //                                            Ec_p1ddate = item.Ec_p1ddate,
                        //                                            Ec_p1dline = item.Ec_p1dline,
                        //                                            Ec_p1dlot = item.Ec_p1dlot,
                        //                                            Ec_p1dnote = item.Ec_p1dnote,
                        //                                            p1dModifier = item.p1dModifier,
                        //                                            p1dModifyDate = item.p1dModifyDate,
                        //                                            Ec_qadate = item.Ec_qadate,
                        //                                            Ec_qalot = item.Ec_qalot,
                        //                                            Ec_qanote = item.Ec_qanote,
                        //                                            qaModifier = item.qaModifier,
                        //                                            qaModifyDate = item.qaModifyDate,
                        //                                            UDF01 = item.UDF01,
                        //                                            UDF02 = item.UDF02,
                        //                                            UDF03 = item.UDF03,
                        //                                            UDF04 = item.UDF04,
                        //                                            UDF05 = item.UDF05,
                        //                                            UDF06 = item.UDF06,
                        //                                            UDF51 = item.UDF51,
                        //                                            UDF52 = item.UDF52,
                        //                                            UDF53 = item.UDF53,
                        //                                            UDF54 = item.UDF54,
                        //                                            UDF55 = item.UDF55,
                        //                                            UDF56 = item.UDF56,
                        //                                            IsDeleted = item.IsDeleted,
                        //                                            Remark = item.Remark,
                        //                                            Creator = item.Creator,
                        //                                            CreateDate = item.CreateDate,
                        //                                            Modifier = item.Modifier,
                        //                                            ModifyDate = item.ModifyDate,
                        //                                        }).ToList();

                        //    DB.BulkUpdate(UpdateP2DPurList);
                        //    var MMPur = from a in DB.Pp_Ec_Subs
                        //                where a.Ec_no.Contains(Ec_no.Text)
                        //                where a.Ec_newitem == "0"
                        //                where a.Ec_procurement == "F"
                        //                where a.Ec_location != "C003"
                        //                where a.IsDeleted == 0
                        //                select a;
                        //    var MMPurList = MMPur.ToList().Distinct();
                        //    List<Pp_Ec_Sub> UpdateMMPurList = (from item in MMPurList
                        //                                       select new Pp_Ec_Sub
                        //                                       {
                        //                                           GUID = item.GUID,
                        //                                           Ec_no = item.Ec_no,
                        //                                           Ec_model = item.Ec_model,
                        //                                           Ec_bomitem = item.Ec_bomitem,
                        //                                           Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                           Ec_olditem = item.Ec_olditem,
                        //                                           Ec_oldtext = item.Ec_oldtext,
                        //                                           Ec_oldqty = item.Ec_oldqty,
                        //                                           Ec_oldset = item.Ec_oldset,
                        //                                           Ec_newitem = item.Ec_newitem,
                        //                                           Ec_newtext = item.Ec_newtext,
                        //                                           Ec_newqty = item.Ec_newqty,
                        //                                           Ec_newset = item.Ec_newset,
                        //                                           Ec_bomno = item.Ec_bomno,
                        //                                           Ec_change = item.Ec_change,
                        //                                           Ec_local = item.Ec_local,
                        //                                           Ec_note = item.Ec_note,
                        //                                           Ec_process = item.Ec_process,
                        //                                           Ec_bomdate = item.Ec_bomdate,

                        //                                           Ec_procurement = item.Ec_procurement,
                        //                                           Ec_location = item.Ec_location,
                        //                                           IsCheck = item.IsCheck,
                        //                                           IsManage = 2,
                        //                                           Ec_eol = item.Ec_eol,

                        //                                           Ec_entrydate = item.Ec_entrydate,
                        //                                           Ec_pmcdate = item.Ec_pmcdate,
                        //                                           Ec_pmclot = item.Ec_pmclot,
                        //                                           Ec_pmcmemo = item.Ec_pmcmemo,
                        //                                           Ec_pmcnote = item.Ec_pmcnote,
                        //                                           Ec_bstock = item.Ec_bstock,
                        //                                           pmcModifier = item.pmcModifier,
                        //                                           pmcModifyDate = item.pmcModifyDate,
                        //                                           Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_p2dlot = "与制二无关",
                        //                                           Ec_p2dnote = "与制二无关",
                        //                                           p2dModifier = GetIdentityName(),
                        //                                           p2dModifyDate = DateTime.Now,
                        //                                           Ec_mmdate = "",
                        //                                           Ec_mmlot = "",
                        //                                           Ec_mmlotno = "",
                        //                                           Ec_mmnote = "",
                        //                                           mmModifier = item.mmModifier,
                        //                                           mmModifyDate = item.mmModifyDate,
                        //                                           Ec_purdate = item.Ec_purdate,
                        //                                           Ec_purorder = item.Ec_purorder,
                        //                                           Ec_pursupplier = item.Ec_pursupplier,
                        //                                           Ec_purnote = item.Ec_purnote,
                        //                                           ppModifier = item.ppModifier,
                        //                                           ppModifyDate = item.ppModifyDate,
                        //                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                           Ec_iqcorder = "4300000000",
                        //                                           Ec_iqcnote = "免检",
                        //                                           iqcModifier = GetIdentityName(),
                        //                                           iqcModifyDate = DateTime.Now,
                        //                                           Ec_p1ddate = item.Ec_p1ddate,
                        //                                           Ec_p1dline = item.Ec_p1dline,
                        //                                           Ec_p1dlot = item.Ec_p1dlot,
                        //                                           Ec_p1dnote = item.Ec_p1dnote,
                        //                                           p1dModifier = item.p1dModifier,
                        //                                           p1dModifyDate = item.p1dModifyDate,
                        //                                           Ec_qadate = item.Ec_qadate,
                        //                                           Ec_qalot = item.Ec_qalot,
                        //                                           Ec_qanote = item.Ec_qanote,
                        //                                           qaModifier = item.qaModifier,
                        //                                           qaModifyDate = item.qaModifyDate,
                        //                                           UDF01 = item.UDF01,
                        //                                           UDF02 = item.UDF02,
                        //                                           UDF03 = item.UDF03,
                        //                                           UDF04 = item.UDF04,
                        //                                           UDF05 = item.UDF05,
                        //                                           UDF06 = item.UDF06,
                        //                                           UDF51 = item.UDF51,
                        //                                           UDF52 = item.UDF52,
                        //                                           UDF53 = item.UDF53,
                        //                                           UDF54 = item.UDF54,
                        //                                           UDF55 = item.UDF55,
                        //                                           UDF56 = item.UDF56,
                        //                                           IsDeleted = item.IsDeleted,
                        //                                           Remark = item.Remark,
                        //                                           Creator = item.Creator,
                        //                                           CreateDate = item.CreateDate,
                        //                                           Modifier = item.Modifier,
                        //                                           ModifyDate = item.ModifyDate,
                        //                                       }).ToList();

                        //    DB.BulkUpdate(UpdateMMPurList);

                        //    var P2DNoPur = from a in DB.Pp_Ec_Subs
                        //                   where a.Ec_no.Contains(Ec_no.Text)
                        //                   where a.Ec_newitem == "0"
                        //                   where a.Ec_procurement == "E"
                        //                   //where a.Ec_location == "C003"
                        //                   where a.IsDeleted == 0
                        //                   select a;
                        //    var P2DNoPurList = P2DNoPur.ToList().Distinct();
                        //    List<Pp_Ec_Sub> UpdateP2DNoPur = (from item in P2DNoPurList
                        //                                      select new Pp_Ec_Sub
                        //                                      {
                        //                                          GUID = item.GUID,
                        //                                          Ec_no = item.Ec_no,
                        //                                          Ec_model = item.Ec_model,
                        //                                          Ec_bomitem = item.Ec_bomitem,
                        //                                          Ec_bomsubitem = item.Ec_bomsubitem,
                        //                                          Ec_olditem = item.Ec_olditem,
                        //                                          Ec_oldtext = item.Ec_oldtext,
                        //                                          Ec_oldqty = item.Ec_oldqty,
                        //                                          Ec_oldset = item.Ec_oldset,
                        //                                          Ec_newitem = item.Ec_newitem,
                        //                                          Ec_newtext = item.Ec_newtext,
                        //                                          Ec_newqty = item.Ec_newqty,
                        //                                          Ec_newset = item.Ec_newset,
                        //                                          Ec_bomno = item.Ec_bomno,
                        //                                          Ec_change = item.Ec_change,
                        //                                          Ec_local = item.Ec_local,
                        //                                          Ec_note = item.Ec_note,
                        //                                          Ec_process = item.Ec_process,
                        //                                          Ec_bomdate = item.Ec_bomdate,

                        //                                          Ec_procurement = item.Ec_procurement,
                        //                                          Ec_location = item.Ec_location,
                        //                                          IsCheck = item.IsCheck,
                        //                                          IsManage = IsManages,
                        //                                          Ec_eol = item.Ec_eol,

                        //                                          Ec_entrydate = item.Ec_entrydate,
                        //                                          Ec_pmcdate = item.Ec_pmcdate,
                        //                                          Ec_pmclot = item.Ec_pmclot,
                        //                                          Ec_pmcmemo = item.Ec_pmcmemo,
                        //                                          Ec_pmcnote = item.Ec_pmcnote,
                        //                                          Ec_bstock = item.Ec_bstock,
                        //                                          pmcModifier = item.pmcModifier,
                        //                                          pmcModifyDate = item.pmcModifyDate,
                        //                                          Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                          Ec_p2dlot = "与制二无关",
                        //                                          Ec_p2dnote = "与制二无关",
                        //                                          p2dModifier = GetIdentityName(),
                        //                                          p2dModifyDate = DateTime.Now,
                        //                                          Ec_mmdate = item.Ec_mmdate,
                        //                                          Ec_mmlot = item.Ec_mmlot,
                        //                                          Ec_mmlotno = item.Ec_mmlotno,
                        //                                          Ec_mmnote = item.Ec_mmnote,
                        //                                          mmModifier = item.mmModifier,
                        //                                          mmModifyDate = item.mmModifyDate,
                        //                                          Ec_purdate = item.Ec_purdate,
                        //                                          Ec_purorder = item.Ec_purorder,
                        //                                          Ec_pursupplier = item.Ec_pursupplier,
                        //                                          Ec_purnote = item.Ec_purnote,
                        //                                          ppModifier = item.ppModifier,
                        //                                          ppModifyDate = item.ppModifyDate,
                        //                                          Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                        //                                          Ec_iqcorder = "4300000000",
                        //                                          Ec_iqcnote = "免检",
                        //                                          iqcModifier = GetIdentityName(),
                        //                                          iqcModifyDate = DateTime.Now,
                        //                                          Ec_p1ddate = item.Ec_p1ddate,
                        //                                          Ec_p1dline = item.Ec_p1dline,
                        //                                          Ec_p1dlot = item.Ec_p1dlot,
                        //                                          Ec_p1dnote = item.Ec_p1dnote,
                        //                                          p1dModifier = item.p1dModifier,
                        //                                          p1dModifyDate = item.p1dModifyDate,
                        //                                          Ec_qadate = item.Ec_qadate,
                        //                                          Ec_qalot = item.Ec_qalot,
                        //                                          Ec_qanote = item.Ec_qanote,
                        //                                          qaModifier = item.qaModifier,
                        //                                          qaModifyDate = item.qaModifyDate,
                        //                                          UDF01 = item.UDF01,
                        //                                          UDF02 = item.UDF02,
                        //                                          UDF03 = item.UDF03,
                        //                                          UDF04 = item.UDF04,
                        //                                          UDF05 = item.UDF05,
                        //                                          UDF06 = item.UDF06,
                        //                                          UDF51 = item.UDF51,
                        //                                          UDF52 = item.UDF52,
                        //                                          UDF53 = item.UDF53,
                        //                                          UDF54 = item.UDF54,
                        //                                          UDF55 = item.UDF55,
                        //                                          UDF56 = item.UDF56,
                        //                                          IsDeleted = item.IsDeleted,
                        //                                          Remark = item.Remark,
                        //                                          Creator = item.Creator,
                        //                                          CreateDate = item.CreateDate,
                        //                                          Modifier = item.Modifier,
                        //                                          ModifyDate = item.ModifyDate,
                        //                                      }).ToList();

                        //    DB.BulkUpdate(UpdateP2DNoPur);
                        //}

                        #endregion 7.新物料为空时更新旧物料管理
                    }

                    #region 6.更新旧品库存

                    //var ItemStock = from a in DB.Pp_Ec_Subs
                    //                where a.Ec_no.Contains(Ec_no.Text)
                    //                where a.Ec_olditem != "0"
                    //                select a;
                    //var UpdateItemStock = from a in ItemStock
                    //                      join b in DB.Pp_SapMaterials on a.Ec_olditem equals b.D_SAP_ZCA1D_Z002
                    //                      where b.D_SAP_ZCA1D_Z033 != 0
                    //                      select new
                    //                      {
                    //                          a.GUID,
                    //                          a.Ec_no,
                    //                          a.Ec_model,
                    //                          a.Ec_bomitem,
                    //                          a.Ec_bomsubitem,
                    //                          a.Ec_olditem,
                    //                          a.Ec_oldtext,
                    //                          a.Ec_oldqty,
                    //                          a.Ec_oldset,
                    //                          a.Ec_newitem,
                    //                          a.Ec_newtext,
                    //                          a.Ec_newqty,
                    //                          a.Ec_newset,
                    //                          a.Ec_bomno,
                    //                          a.Ec_change,
                    //                          a.Ec_local,
                    //                          a.Ec_note,
                    //                          a.Ec_process,
                    //                          a.Ec_procurement,
                    //                          a.Ec_location,
                    //                          a.IsCheck,
                    //                          b.IsManage,
                    //                          a.Ec_eol,
                    //                          a.Ec_bomdate,
                    //                          a.Ec_entrydate,
                    //                          a.Ec_pmcdate,
                    //                          a.Ec_pmclot,
                    //                          a.Ec_pmcmemo,
                    //                          a.Ec_pmcnote,
                    //                          Ec_bstock = b.D_SAP_ZCA1D_Z033,
                    //                          a.pmcModifier,
                    //                          a.pmcModifyDate,
                    //                          a.Ec_p2ddate,
                    //                          a.Ec_p2dlot,
                    //                          a.Ec_p2dnote,
                    //                          a.p2dModifier,
                    //                          a.p2dModifyDate,
                    //                          a.Ec_mmdate,
                    //                          a.Ec_mmlot,
                    //                          a.Ec_mmlotno,
                    //                          a.Ec_mmnote,
                    //                          a.mmModifier,
                    //                          a.mmModifyDate,
                    //                          a.Ec_purdate,
                    //                          a.Ec_purorder,
                    //                          a.Ec_pursupplier,
                    //                          a.Ec_purnote,
                    //                          a.ppModifier,
                    //                          a.ppModifyDate,
                    //                          a.Ec_iqcdate,
                    //                          a.Ec_iqcorder,
                    //                          a.Ec_iqcnote,
                    //                          a.iqcModifier,
                    //                          a.iqcModifyDate,
                    //                          a.Ec_p1ddate,
                    //                          a.Ec_p1dline,
                    //                          a.Ec_p1dlot,
                    //                          a.Ec_p1dnote,
                    //                          a.p1dModifier,
                    //                          a.p1dModifyDate,

                    //                          a.Ec_qadate,
                    //                          a.Ec_qalot,
                    //                          a.Ec_qanote,
                    //                          a.qaModifier,
                    //                          a.qaModifyDate,
                    //                          a.UDF01,
                    //                          a.UDF02,
                    //                          a.UDF03,
                    //                          a.UDF04,
                    //                          a.UDF05,
                    //                          a.UDF06,
                    //                          a.UDF51,
                    //                          a.UDF52,
                    //                          a.UDF53,
                    //                          a.UDF54,
                    //                          a.UDF55,
                    //                          a.UDF56,
                    //                          a.IsDeleted,
                    //                          a.Remark,
                    //                          a.Creator,
                    //                          a.CreateDate,
                    //                          a.Modifier,
                    //                          a.ModifyDate,
                    //                      };

                    //var ItemStockList = UpdateItemStock.ToList().Distinct();
                    //List<Pp_Ec_Sub> UpdateStockList = (from item in ItemStockList
                    //                                   select new Pp_Ec_Sub
                    //                                   {
                    //                                       GUID = item.GUID,
                    //                                       Ec_no = item.Ec_no,
                    //                                       Ec_model = item.Ec_model,
                    //                                       Ec_bomitem = item.Ec_bomitem,
                    //                                       Ec_bomsubitem = item.Ec_bomsubitem,
                    //                                       Ec_olditem = item.Ec_olditem,
                    //                                       Ec_oldtext = item.Ec_oldtext,
                    //                                       Ec_oldqty = item.Ec_oldqty,
                    //                                       Ec_oldset = item.Ec_oldset,
                    //                                       Ec_newitem = item.Ec_newitem,
                    //                                       Ec_newtext = item.Ec_newtext,
                    //                                       Ec_newqty = item.Ec_newqty,
                    //                                       Ec_newset = item.Ec_newset,
                    //                                       Ec_bomno = item.Ec_bomno,
                    //                                       Ec_change = item.Ec_change,
                    //                                       Ec_local = item.Ec_local,
                    //                                       Ec_note = item.Ec_note,
                    //                                       Ec_process = item.Ec_process,
                    //                                       Ec_bomdate = item.Ec_bomdate,

                    //                                       Ec_procurement = item.Ec_procurement,
                    //                                       Ec_location = item.Ec_location,
                    //                                       IsCheck = item.IsCheck,
                    //                                       IsManage = item.IsManage,
                    //                                       Ec_eol = item.Ec_eol,

                    //                                       Ec_entrydate = item.Ec_entrydate,
                    //                                       Ec_pmcdate = item.Ec_pmcdate,
                    //                                       Ec_pmclot = item.Ec_pmclot,
                    //                                       Ec_pmcmemo = item.Ec_pmcmemo,
                    //                                       Ec_pmcnote = item.Ec_pmcnote,
                    //                                       Ec_bstock = item.Ec_bstock,
                    //                                       pmcModifier = item.pmcModifier,
                    //                                       pmcModifyDate = item.pmcModifyDate,
                    //                                       Ec_p2ddate = item.Ec_p2ddate,
                    //                                       Ec_p2dlot = item.Ec_p2dlot,
                    //                                       Ec_p2dnote = item.Ec_p2dnote,
                    //                                       p2dModifier = item.p2dModifier,
                    //                                       p2dModifyDate = item.p2dModifyDate,
                    //                                       Ec_mmdate = item.Ec_mmdate,
                    //                                       Ec_mmlot = item.Ec_mmlot,
                    //                                       Ec_mmlotno = item.Ec_mmlotno,
                    //                                       Ec_mmnote = item.Ec_mmnote,
                    //                                       mmModifier = item.mmModifier,
                    //                                       mmModifyDate = item.mmModifyDate,
                    //                                       Ec_purdate = item.Ec_purdate,
                    //                                       Ec_purorder = item.Ec_purorder,
                    //                                       Ec_pursupplier = item.Ec_pursupplier,
                    //                                       Ec_purnote = item.Ec_purnote,
                    //                                       ppModifier = item.ppModifier,
                    //                                       ppModifyDate = item.ppModifyDate,
                    //                                       Ec_iqcdate = item.Ec_iqcdate,
                    //                                       Ec_iqcorder = item.Ec_iqcorder,
                    //                                       Ec_iqcnote = item.Ec_iqcnote,
                    //                                       iqcModifier = item.iqcModifier,
                    //                                       iqcModifyDate = item.iqcModifyDate,
                    //                                       Ec_p1ddate = item.Ec_p1ddate,
                    //                                       Ec_p1dline = item.Ec_p1dline,
                    //                                       Ec_p1dlot = item.Ec_p1dlot,
                    //                                       Ec_p1dnote = item.Ec_p1dnote,
                    //                                       p1dModifier = item.p1dModifier,
                    //                                       p1dModifyDate = item.p1dModifyDate,
                    //                                       Ec_qadate = item.Ec_qadate,
                    //                                       Ec_qalot = item.Ec_qalot,
                    //                                       Ec_qanote = item.Ec_qanote,
                    //                                       qaModifier = item.qaModifier,
                    //                                       qaModifyDate = item.qaModifyDate,
                    //                                       UDF01 = item.UDF01,
                    //                                       UDF02 = item.UDF02,
                    //                                       UDF03 = item.UDF03,
                    //                                       UDF04 = item.UDF04,
                    //                                       UDF05 = item.UDF05,
                    //                                       UDF06 = item.UDF06,
                    //                                       UDF51 = item.UDF51,
                    //                                       UDF52 = item.UDF52,
                    //                                       UDF53 = item.UDF53,
                    //                                       UDF54 = item.UDF54,
                    //                                       UDF55 = item.UDF55,
                    //                                       UDF56 = item.UDF56,
                    //                                       IsDeleted = item.IsDeleted,
                    //                                       Remark = item.Remark,

                    //                                       Creator = item.Creator,
                    //                                       CreateDate = item.CreateDate,
                    //                                       Modifier = item.Modifier,
                    //                                       ModifyDate = item.ModifyDate,
                    //                                   }).ToList();

                    //DB.BulkUpdate(UpdateStockList);

                    #endregion 6.更新旧品库存
                }
                //if (oldIsSopUpdate != IsSopUpdate.SelectedValue)
                //{
                SaveItemSop();
                //}
                //if (oldisComfirm != IsManage.SelectedValue)
                //{
                //UpdateItemSub();
                if (this.Ec_distinction.SelectedValue != "4")
                {
                    #region 5.更新免检&要检

                    var CheckItem = from a in DB.Pp_Ec_Subs
                                    where a.Ec_no.Contains(Ec_no.Text)
                                    where a.Ec_newitem != "0"
                                    where a.IsDeleted == 0
                                    select a;
                    var UpdateIsCheck_NO = from a in CheckItem
                                           join b in DB.Pp_SapMaterials on a.Ec_newitem equals b.D_SAP_ZCA1D_Z002
                                           //where b.D_SAP_ZCA1D_Z019 != "X"
                                           select new
                                           {
                                               a.GUID,
                                               a.Ec_no,
                                               a.Ec_model,
                                               a.Ec_bomitem,
                                               a.Ec_bomsubitem,
                                               a.Ec_olditem,
                                               a.Ec_oldtext,
                                               a.Ec_oldqty,
                                               a.Ec_oldset,
                                               a.Ec_newitem,
                                               a.Ec_newtext,
                                               a.Ec_newqty,
                                               a.Ec_newset,
                                               a.Ec_bomno,
                                               a.Ec_procurement,
                                               a.Ec_location,
                                               a.IsCheck,
                                               a.IsManage,
                                               a.IsMmManage,
                                               a.IsPcbaManage,
                                               a.IsAssyManage,
                                               a.Ec_eol,
                                               a.Ec_change,
                                               a.Ec_local,
                                               a.Ec_note,
                                               a.Ec_process,
                                               a.Ec_bomdate,
                                               a.Ec_entrydate,
                                               a.Ec_pmcdate,
                                               a.Ec_pmclot,
                                               a.Ec_pmcmemo,
                                               a.Ec_pmcnote,
                                               a.Ec_bstock,
                                               a.pmcModifier,
                                               a.pmcModifyDate,
                                               a.Ec_p2ddate,
                                               a.Ec_p2dlot,
                                               a.Ec_p2dnote,
                                               a.p2dModifier,
                                               a.p2dModifyDate,
                                               a.Ec_mmdate,
                                               a.Ec_mmlot,
                                               a.Ec_mmlotno,
                                               a.Ec_mmnote,
                                               a.mmModifier,
                                               a.mmModifyDate,
                                               a.Ec_purdate,
                                               a.Ec_purorder,
                                               a.Ec_pursupplier,
                                               a.Ec_purnote,
                                               a.ppModifier,
                                               a.ppModifyDate,
                                               a.Ec_iqcdate,
                                               a.Ec_iqcorder,
                                               a.Ec_iqcnote,
                                               a.iqcModifier,
                                               a.iqcModifyDate,
                                               a.Ec_p1ddate,
                                               a.Ec_p1dline,
                                               a.Ec_p1dlot,
                                               a.Ec_p1dnote,
                                               a.p1dModifier,
                                               a.p1dModifyDate,
                                               a.Ec_qadate,
                                               a.Ec_qalot,
                                               a.Ec_qanote,
                                               a.qaModifier,
                                               a.qaModifyDate,
                                               a.UDF01,
                                               a.UDF02,
                                               a.UDF03,
                                               a.UDF04,
                                               a.UDF05,
                                               a.UDF06,
                                               a.UDF51,
                                               a.UDF52,
                                               a.UDF53,
                                               a.UDF54,
                                               a.UDF55,
                                               a.UDF56,
                                               a.IsDeleted,
                                               a.Remark,
                                               a.Creator,
                                               a.CreateDate,
                                               a.Modifier,
                                               a.ModifyDate,
                                           };

                    var IsCheckList_NO = UpdateIsCheck_NO.ToList().Distinct();
                    List<Pp_Ec_Sub> UpdateCheckList_NO = (from item in IsCheckList_NO
                                                          select new Pp_Ec_Sub
                                                          {
                                                              GUID = item.GUID,
                                                              Ec_no = item.Ec_no,
                                                              Ec_model = item.Ec_model,
                                                              Ec_bomitem = item.Ec_bomitem,
                                                              Ec_bomsubitem = item.Ec_bomsubitem,
                                                              Ec_olditem = item.Ec_olditem,
                                                              Ec_oldtext = item.Ec_oldtext,
                                                              Ec_oldqty = item.Ec_oldqty,
                                                              Ec_oldset = item.Ec_oldset,
                                                              Ec_newitem = item.Ec_newitem,
                                                              Ec_newtext = item.Ec_newtext,
                                                              Ec_newqty = item.Ec_newqty,
                                                              Ec_newset = item.Ec_newset,
                                                              Ec_bomno = item.Ec_bomno,
                                                              Ec_change = item.Ec_change,
                                                              Ec_local = item.Ec_local,
                                                              Ec_note = item.Ec_note,
                                                              Ec_process = item.Ec_process,
                                                              Ec_bomdate = item.Ec_bomdate,

                                                              Ec_procurement = item.Ec_procurement,
                                                              Ec_location = item.Ec_location,
                                                              IsCheck = item.IsCheck,
                                                              IsManage = item.IsManage,
                                                              IsMmManage = item.IsMmManage,
                                                              IsPcbaManage = item.IsPcbaManage,
                                                              IsAssyManage = item.IsAssyManage,
                                                              Ec_eol = item.Ec_eol,

                                                              Ec_entrydate = item.Ec_entrydate,
                                                              Ec_pmcdate = item.Ec_pmcdate,
                                                              Ec_pmclot = item.Ec_pmclot,
                                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                                              Ec_pmcnote = item.Ec_pmcnote,
                                                              Ec_bstock = item.Ec_bstock,
                                                              pmcModifier = item.pmcModifier,
                                                              pmcModifyDate = item.pmcModifyDate,
                                                              Ec_p2ddate = item.Ec_p2ddate,
                                                              Ec_p2dlot = item.Ec_p2dlot,
                                                              Ec_p2dnote = item.Ec_p2dnote,
                                                              p2dModifier = item.p2dModifier,
                                                              p2dModifyDate = item.p2dModifyDate,
                                                              Ec_mmdate = item.Ec_mmdate,
                                                              Ec_mmlot = item.Ec_mmlot,
                                                              Ec_mmlotno = item.Ec_mmlotno,
                                                              Ec_mmnote = item.Ec_mmnote,
                                                              mmModifier = item.mmModifier,
                                                              mmModifyDate = item.mmModifyDate,
                                                              Ec_purdate = item.Ec_purdate,
                                                              Ec_purorder = item.Ec_purorder,
                                                              Ec_pursupplier = item.Ec_pursupplier,
                                                              Ec_purnote = item.Ec_purnote,
                                                              ppModifier = item.ppModifier,
                                                              ppModifyDate = item.ppModifyDate,
                                                              Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                              Ec_iqcorder = "4300000000",
                                                              Ec_iqcnote = "免检",
                                                              iqcModifier = item.iqcModifier,
                                                              iqcModifyDate = item.iqcModifyDate,
                                                              Ec_p1ddate = item.Ec_p1ddate,
                                                              Ec_p1dline = item.Ec_p1dline,
                                                              Ec_p1dlot = item.Ec_p1dlot,
                                                              Ec_p1dnote = item.Ec_p1dnote,
                                                              p1dModifier = item.p1dModifier,
                                                              p1dModifyDate = item.p1dModifyDate,
                                                              Ec_qadate = item.Ec_qadate,
                                                              Ec_qalot = item.Ec_qalot,
                                                              Ec_qanote = item.Ec_qanote,
                                                              qaModifier = item.qaModifier,
                                                              qaModifyDate = item.qaModifyDate,
                                                              UDF01 = item.UDF01,
                                                              UDF02 = item.UDF02,
                                                              UDF03 = item.UDF03,
                                                              UDF04 = item.UDF04,
                                                              UDF05 = item.UDF05,
                                                              UDF06 = item.UDF06,
                                                              UDF51 = item.UDF51,
                                                              UDF52 = item.UDF52,
                                                              UDF53 = item.UDF53,
                                                              UDF54 = item.UDF54,
                                                              UDF55 = item.UDF55,
                                                              UDF56 = item.UDF56,
                                                              IsDeleted = item.IsDeleted,
                                                              Remark = item.Remark,
                                                              Creator = item.Creator,
                                                              CreateDate = item.CreateDate,
                                                              Modifier = GetIdentityName(),
                                                              ModifyDate = DateTime.Now,
                                                          }).ToList();

                    DB.BulkUpdate(UpdateCheckList_NO);
                    var UpdateIsCheck_Yes = from a in CheckItem
                                            join b in DB.Pp_SapMaterials on a.Ec_newitem equals b.D_SAP_ZCA1D_Z002
                                            where b.D_SAP_ZCA1D_Z019 == "X"
                                            select new
                                            {
                                                a.GUID,
                                                a.Ec_no,
                                                a.Ec_model,
                                                a.Ec_bomitem,
                                                a.Ec_bomsubitem,
                                                a.Ec_olditem,
                                                a.Ec_oldtext,
                                                a.Ec_oldqty,
                                                a.Ec_oldset,
                                                a.Ec_newitem,
                                                a.Ec_newtext,
                                                a.Ec_newqty,
                                                a.Ec_newset,
                                                a.Ec_bomno,
                                                a.Ec_procurement,
                                                a.Ec_location,
                                                a.IsCheck,
                                                a.IsManage,
                                                a.IsMmManage,
                                                a.IsPcbaManage,
                                                a.IsAssyManage,
                                                a.Ec_eol,
                                                a.Ec_change,
                                                a.Ec_local,
                                                a.Ec_note,
                                                a.Ec_process,
                                                a.Ec_bomdate,
                                                a.Ec_entrydate,
                                                a.Ec_pmcdate,
                                                a.Ec_pmclot,
                                                a.Ec_pmcmemo,
                                                a.Ec_pmcnote,
                                                a.Ec_bstock,
                                                a.pmcModifier,
                                                a.pmcModifyDate,
                                                a.Ec_p2ddate,
                                                a.Ec_p2dlot,
                                                a.Ec_p2dnote,
                                                a.p2dModifier,
                                                a.p2dModifyDate,
                                                a.Ec_mmdate,
                                                a.Ec_mmlot,
                                                a.Ec_mmlotno,
                                                a.Ec_mmnote,
                                                a.mmModifier,
                                                a.mmModifyDate,
                                                a.Ec_purdate,
                                                a.Ec_purorder,
                                                a.Ec_pursupplier,
                                                a.Ec_purnote,
                                                a.ppModifier,
                                                a.ppModifyDate,
                                                a.Ec_iqcdate,
                                                a.Ec_iqcorder,
                                                a.Ec_iqcnote,
                                                a.iqcModifier,
                                                a.iqcModifyDate,
                                                a.Ec_p1ddate,
                                                a.Ec_p1dline,
                                                a.Ec_p1dlot,
                                                a.Ec_p1dnote,
                                                a.p1dModifier,
                                                a.p1dModifyDate,
                                                a.Ec_qadate,
                                                a.Ec_qalot,
                                                a.Ec_qanote,
                                                a.qaModifier,
                                                a.qaModifyDate,
                                                a.UDF01,
                                                a.UDF02,
                                                a.UDF03,
                                                a.UDF04,
                                                a.UDF05,
                                                a.UDF06,
                                                a.UDF51,
                                                a.UDF52,
                                                a.UDF53,
                                                a.UDF54,
                                                a.UDF55,
                                                a.UDF56,
                                                a.IsDeleted,
                                                a.Remark,
                                                a.Creator,
                                                a.CreateDate,
                                                a.Modifier,
                                                a.ModifyDate,
                                            };

                    var IsCheckList_Yes = UpdateIsCheck_Yes.ToList().Distinct();
                    List<Pp_Ec_Sub> UpdateCheckList_Yes = (from item in IsCheckList_Yes
                                                           select new Pp_Ec_Sub
                                                           {
                                                               GUID = item.GUID,
                                                               Ec_no = item.Ec_no,
                                                               Ec_model = item.Ec_model,
                                                               Ec_bomitem = item.Ec_bomitem,
                                                               Ec_bomsubitem = item.Ec_bomsubitem,
                                                               Ec_olditem = item.Ec_olditem,
                                                               Ec_oldtext = item.Ec_oldtext,
                                                               Ec_oldqty = item.Ec_oldqty,
                                                               Ec_oldset = item.Ec_oldset,
                                                               Ec_newitem = item.Ec_newitem,
                                                               Ec_newtext = item.Ec_newtext,
                                                               Ec_newqty = item.Ec_newqty,
                                                               Ec_newset = item.Ec_newset,
                                                               Ec_bomno = item.Ec_bomno,
                                                               Ec_change = item.Ec_change,
                                                               Ec_local = item.Ec_local,
                                                               Ec_note = item.Ec_note,
                                                               Ec_process = item.Ec_process,
                                                               Ec_bomdate = item.Ec_bomdate,

                                                               Ec_procurement = item.Ec_procurement,
                                                               Ec_location = item.Ec_location,
                                                               IsCheck = item.IsCheck,
                                                               IsManage = item.IsManage,
                                                               IsMmManage = item.IsMmManage,
                                                               IsPcbaManage = item.IsPcbaManage,
                                                               IsAssyManage = item.IsAssyManage,
                                                               Ec_eol = item.Ec_eol,

                                                               Ec_entrydate = item.Ec_entrydate,
                                                               Ec_pmcdate = item.Ec_pmcdate,
                                                               Ec_pmclot = item.Ec_pmclot,
                                                               Ec_pmcmemo = item.Ec_pmcmemo,
                                                               Ec_pmcnote = item.Ec_pmcnote,
                                                               Ec_bstock = item.Ec_bstock,
                                                               pmcModifier = item.pmcModifier,
                                                               pmcModifyDate = item.pmcModifyDate,
                                                               Ec_p2ddate = item.Ec_p2ddate,
                                                               Ec_p2dlot = item.Ec_p2dlot,
                                                               Ec_p2dnote = item.Ec_p2dnote,
                                                               p2dModifier = item.p2dModifier,
                                                               p2dModifyDate = item.p2dModifyDate,
                                                               Ec_mmdate = item.Ec_mmdate,
                                                               Ec_mmlot = item.Ec_mmlot,
                                                               Ec_mmlotno = item.Ec_mmlotno,
                                                               Ec_mmnote = item.Ec_mmnote,
                                                               mmModifier = item.mmModifier,
                                                               mmModifyDate = item.mmModifyDate,
                                                               Ec_purdate = item.Ec_purdate,
                                                               Ec_purorder = item.Ec_purorder,
                                                               Ec_pursupplier = item.Ec_pursupplier,
                                                               Ec_purnote = item.Ec_purnote,
                                                               ppModifier = item.ppModifier,
                                                               ppModifyDate = item.ppModifyDate,
                                                               Ec_iqcdate = "",
                                                               Ec_iqcorder = "",
                                                               Ec_iqcnote = "",
                                                               iqcModifier = item.iqcModifier,
                                                               iqcModifyDate = item.iqcModifyDate,
                                                               Ec_p1ddate = item.Ec_p1ddate,
                                                               Ec_p1dline = item.Ec_p1dline,
                                                               Ec_p1dlot = item.Ec_p1dlot,
                                                               Ec_p1dnote = item.Ec_p1dnote,
                                                               p1dModifier = item.p1dModifier,
                                                               p1dModifyDate = item.p1dModifyDate,
                                                               Ec_qadate = item.Ec_qadate,
                                                               Ec_qalot = item.Ec_qalot,
                                                               Ec_qanote = item.Ec_qanote,
                                                               qaModifier = item.qaModifier,
                                                               qaModifyDate = item.qaModifyDate,
                                                               UDF01 = item.UDF01,
                                                               UDF02 = item.UDF02,
                                                               UDF03 = item.UDF03,
                                                               UDF04 = item.UDF04,
                                                               UDF05 = item.UDF05,
                                                               UDF06 = item.UDF06,
                                                               UDF51 = item.UDF51,
                                                               UDF52 = item.UDF52,
                                                               UDF53 = item.UDF53,
                                                               UDF54 = item.UDF54,
                                                               UDF55 = item.UDF55,
                                                               UDF56 = item.UDF56,
                                                               IsDeleted = item.IsDeleted,
                                                               Remark = item.Remark,
                                                               Creator = item.Creator,
                                                               CreateDate = item.CreateDate,
                                                               Modifier = GetIdentityName(),
                                                               ModifyDate = DateTime.Now,
                                                           }).ToList();

                    DB.BulkUpdate(UpdateCheckList_Yes);

                    #endregion 5.更新免检&要检

                    #region 7.新物料为空时更新旧物料管理

                    //if (this.IsManage.SelectedValue == "1")
                    //{
                    var P2DPur = from a in DB.Pp_Ec_Subs
                                 where a.Ec_no.Contains(Ec_no.Text)
                                 where a.Ec_newitem == "0"
                                 where a.Ec_procurement == "F"
                                 where a.Ec_location == "C003"
                                 where a.IsDeleted == 0
                                 select a;
                    var P2DPurList = P2DPur.ToList().Distinct();
                    List<Pp_Ec_Sub> UpdateP2DPurList = (from item in P2DPurList
                                                        select new Pp_Ec_Sub
                                                        {
                                                            GUID = item.GUID,
                                                            Ec_no = item.Ec_no,
                                                            Ec_model = item.Ec_model,
                                                            Ec_bomitem = item.Ec_bomitem,
                                                            Ec_bomsubitem = item.Ec_bomsubitem,
                                                            Ec_olditem = item.Ec_olditem,
                                                            Ec_oldtext = item.Ec_oldtext,
                                                            Ec_oldqty = item.Ec_oldqty,
                                                            Ec_oldset = item.Ec_oldset,
                                                            Ec_newitem = item.Ec_newitem,
                                                            Ec_newtext = item.Ec_newtext,
                                                            Ec_newqty = item.Ec_newqty,
                                                            Ec_newset = item.Ec_newset,
                                                            Ec_bomno = item.Ec_bomno,
                                                            Ec_change = item.Ec_change,
                                                            Ec_local = item.Ec_local,
                                                            Ec_note = item.Ec_note,
                                                            Ec_process = item.Ec_process,
                                                            Ec_bomdate = item.Ec_bomdate,

                                                            Ec_procurement = item.Ec_procurement,
                                                            Ec_location = item.Ec_location,
                                                            IsCheck = item.IsCheck,
                                                            IsManage = item.IsManage,
                                                            IsMmManage = item.IsMmManage,
                                                            IsPcbaManage = item.IsPcbaManage,
                                                            IsAssyManage = item.IsAssyManage,
                                                            Ec_eol = item.Ec_eol,

                                                            Ec_entrydate = item.Ec_entrydate,
                                                            Ec_pmcdate = item.Ec_pmcdate,
                                                            Ec_pmclot = item.Ec_pmclot,
                                                            Ec_pmcmemo = item.Ec_pmcmemo,
                                                            Ec_pmcnote = item.Ec_pmcnote,
                                                            Ec_bstock = item.Ec_bstock,
                                                            pmcModifier = item.pmcModifier,
                                                            pmcModifyDate = item.pmcModifyDate,
                                                            Ec_p2ddate = "",
                                                            Ec_p2dlot = "",
                                                            Ec_p2dnote = "",
                                                            p2dModifier = item.p2dModifier,
                                                            p2dModifyDate = item.p2dModifyDate,
                                                            Ec_mmdate = item.Ec_mmdate,
                                                            Ec_mmlot = item.Ec_mmlot,
                                                            Ec_mmlotno = item.Ec_mmlotno,
                                                            Ec_mmnote = item.Ec_mmnote,
                                                            mmModifier = item.mmModifier,
                                                            mmModifyDate = item.mmModifyDate,
                                                            Ec_purdate = item.Ec_purdate,
                                                            Ec_purorder = item.Ec_purorder,
                                                            Ec_pursupplier = item.Ec_pursupplier,
                                                            Ec_purnote = item.Ec_purnote,
                                                            ppModifier = item.ppModifier,
                                                            ppModifyDate = item.ppModifyDate,
                                                            Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                            Ec_iqcorder = "4300000000",
                                                            Ec_iqcnote = "免检",
                                                            iqcModifier = item.iqcModifier,
                                                            iqcModifyDate = item.iqcModifyDate,
                                                            Ec_p1ddate = item.Ec_p1ddate,
                                                            Ec_p1dline = item.Ec_p1dline,
                                                            Ec_p1dlot = item.Ec_p1dlot,
                                                            Ec_p1dnote = item.Ec_p1dnote,
                                                            p1dModifier = item.p1dModifier,
                                                            p1dModifyDate = item.p1dModifyDate,
                                                            Ec_qadate = item.Ec_qadate,
                                                            Ec_qalot = item.Ec_qalot,
                                                            Ec_qanote = item.Ec_qanote,
                                                            qaModifier = item.qaModifier,
                                                            qaModifyDate = item.qaModifyDate,
                                                            UDF01 = item.UDF01,
                                                            UDF02 = item.UDF02,
                                                            UDF03 = item.UDF03,
                                                            UDF04 = item.UDF04,
                                                            UDF05 = item.UDF05,
                                                            UDF06 = item.UDF06,
                                                            UDF51 = item.UDF51,
                                                            UDF52 = item.UDF52,
                                                            UDF53 = item.UDF53,
                                                            UDF54 = item.UDF54,
                                                            UDF55 = item.UDF55,
                                                            UDF56 = item.UDF56,
                                                            IsDeleted = item.IsDeleted,
                                                            Remark = item.Remark,
                                                            Creator = item.Creator,
                                                            CreateDate = item.CreateDate,
                                                            Modifier = GetIdentityName(),
                                                            ModifyDate = DateTime.Now,
                                                        }).ToList();

                    DB.BulkUpdate(UpdateP2DPurList);
                    var MMPur = from a in DB.Pp_Ec_Subs
                                where a.Ec_no.Contains(Ec_no.Text)
                                where a.Ec_newitem == "0"
                                where a.Ec_procurement == "F"
                                where a.Ec_location != "C003"
                                where a.IsDeleted == 0
                                select a;
                    var MMPurList = MMPur.ToList().Distinct();
                    List<Pp_Ec_Sub> UpdateMMPurList = (from item in MMPurList
                                                       select new Pp_Ec_Sub
                                                       {
                                                           GUID = item.GUID,
                                                           Ec_no = item.Ec_no,
                                                           Ec_model = item.Ec_model,
                                                           Ec_bomitem = item.Ec_bomitem,
                                                           Ec_bomsubitem = item.Ec_bomsubitem,
                                                           Ec_olditem = item.Ec_olditem,
                                                           Ec_oldtext = item.Ec_oldtext,
                                                           Ec_oldqty = item.Ec_oldqty,
                                                           Ec_oldset = item.Ec_oldset,
                                                           Ec_newitem = item.Ec_newitem,
                                                           Ec_newtext = item.Ec_newtext,
                                                           Ec_newqty = item.Ec_newqty,
                                                           Ec_newset = item.Ec_newset,
                                                           Ec_bomno = item.Ec_bomno,
                                                           Ec_change = item.Ec_change,
                                                           Ec_local = item.Ec_local,
                                                           Ec_note = item.Ec_note,
                                                           Ec_process = item.Ec_process,
                                                           Ec_bomdate = item.Ec_bomdate,

                                                           Ec_procurement = item.Ec_procurement,
                                                           Ec_location = item.Ec_location,
                                                           IsCheck = item.IsCheck,
                                                           IsManage = item.IsManage,
                                                           IsMmManage = item.IsMmManage,
                                                           IsPcbaManage = item.IsPcbaManage,
                                                           IsAssyManage = item.IsAssyManage,
                                                           Ec_eol = item.Ec_eol,

                                                           Ec_entrydate = item.Ec_entrydate,
                                                           Ec_pmcdate = item.Ec_pmcdate,
                                                           Ec_pmclot = item.Ec_pmclot,
                                                           Ec_pmcmemo = item.Ec_pmcmemo,
                                                           Ec_pmcnote = item.Ec_pmcnote,
                                                           Ec_bstock = item.Ec_bstock,
                                                           pmcModifier = item.pmcModifier,
                                                           pmcModifyDate = item.pmcModifyDate,
                                                           Ec_p2ddate = item.Ec_p2ddate,
                                                           Ec_p2dlot = item.Ec_p2dlot,
                                                           Ec_p2dnote = item.Ec_p2dnote,
                                                           p2dModifier = item.p2dModifier,
                                                           p2dModifyDate = item.p2dModifyDate,
                                                           Ec_mmdate = "",
                                                           Ec_mmlot = "",
                                                           Ec_mmlotno = "",
                                                           Ec_mmnote = "",
                                                           mmModifier = item.mmModifier,
                                                           mmModifyDate = item.mmModifyDate,
                                                           Ec_purdate = item.Ec_purdate,
                                                           Ec_purorder = item.Ec_purorder,
                                                           Ec_pursupplier = item.Ec_pursupplier,
                                                           Ec_purnote = item.Ec_purnote,
                                                           ppModifier = item.ppModifier,
                                                           ppModifyDate = item.ppModifyDate,
                                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                           Ec_iqcorder = "4300000000",
                                                           Ec_iqcnote = "免检",
                                                           iqcModifier = item.iqcModifier,
                                                           iqcModifyDate = item.iqcModifyDate,
                                                           Ec_p1ddate = item.Ec_p1ddate,
                                                           Ec_p1dline = item.Ec_p1dline,
                                                           Ec_p1dlot = item.Ec_p1dlot,
                                                           Ec_p1dnote = item.Ec_p1dnote,
                                                           p1dModifier = item.p1dModifier,
                                                           p1dModifyDate = item.p1dModifyDate,
                                                           Ec_qadate = item.Ec_qadate,
                                                           Ec_qalot = item.Ec_qalot,
                                                           Ec_qanote = item.Ec_qanote,
                                                           qaModifier = item.qaModifier,
                                                           qaModifyDate = item.qaModifyDate,
                                                           UDF01 = item.UDF01,
                                                           UDF02 = item.UDF02,
                                                           UDF03 = item.UDF03,
                                                           UDF04 = item.UDF04,
                                                           UDF05 = item.UDF05,
                                                           UDF06 = item.UDF06,
                                                           UDF51 = item.UDF51,
                                                           UDF52 = item.UDF52,
                                                           UDF53 = item.UDF53,
                                                           UDF54 = item.UDF54,
                                                           UDF55 = item.UDF55,
                                                           UDF56 = item.UDF56,
                                                           IsDeleted = item.IsDeleted,
                                                           Remark = item.Remark,
                                                           Creator = item.Creator,
                                                           CreateDate = item.CreateDate,
                                                           Modifier = GetIdentityName(),
                                                           ModifyDate = DateTime.Now,
                                                       }).ToList();

                    DB.BulkUpdate(UpdateMMPurList);

                    var P2DNoPur = from a in DB.Pp_Ec_Subs
                                   where a.Ec_no.Contains(Ec_no.Text)
                                   where a.Ec_newitem == "0"
                                   where a.Ec_procurement == "E"
                                   //where a.Ec_location == "C003"
                                   where a.IsDeleted == 0
                                   select a;
                    var P2DNoPurList = P2DNoPur.ToList().Distinct();
                    List<Pp_Ec_Sub> UpdateP2DNoPur = (from item in P2DNoPurList
                                                      select new Pp_Ec_Sub
                                                      {
                                                          GUID = item.GUID,
                                                          Ec_no = item.Ec_no,
                                                          Ec_model = item.Ec_model,
                                                          Ec_bomitem = item.Ec_bomitem,
                                                          Ec_bomsubitem = item.Ec_bomsubitem,
                                                          Ec_olditem = item.Ec_olditem,
                                                          Ec_oldtext = item.Ec_oldtext,
                                                          Ec_oldqty = item.Ec_oldqty,
                                                          Ec_oldset = item.Ec_oldset,
                                                          Ec_newitem = item.Ec_newitem,
                                                          Ec_newtext = item.Ec_newtext,
                                                          Ec_newqty = item.Ec_newqty,
                                                          Ec_newset = item.Ec_newset,
                                                          Ec_bomno = item.Ec_bomno,
                                                          Ec_change = item.Ec_change,
                                                          Ec_local = item.Ec_local,
                                                          Ec_note = item.Ec_note,
                                                          Ec_process = item.Ec_process,
                                                          Ec_bomdate = item.Ec_bomdate,

                                                          Ec_procurement = item.Ec_procurement,
                                                          Ec_location = item.Ec_location,
                                                          IsCheck = item.IsCheck,
                                                          IsManage = item.IsManage,
                                                          IsMmManage = item.IsMmManage,
                                                          IsPcbaManage = item.IsPcbaManage,
                                                          IsAssyManage = item.IsAssyManage,
                                                          Ec_eol = item.Ec_eol,

                                                          Ec_entrydate = item.Ec_entrydate,
                                                          Ec_pmcdate = item.Ec_pmcdate,
                                                          Ec_pmclot = item.Ec_pmclot,
                                                          Ec_pmcmemo = item.Ec_pmcmemo,
                                                          Ec_pmcnote = item.Ec_pmcnote,
                                                          Ec_bstock = item.Ec_bstock,
                                                          pmcModifier = item.pmcModifier,
                                                          pmcModifyDate = item.pmcModifyDate,
                                                          Ec_p2ddate = "",
                                                          Ec_p2dlot = "",
                                                          Ec_p2dnote = "",
                                                          p2dModifier = item.p2dModifier,
                                                          p2dModifyDate = item.p2dModifyDate,
                                                          Ec_mmdate = item.Ec_mmdate,
                                                          Ec_mmlot = item.Ec_mmlot,
                                                          Ec_mmlotno = item.Ec_mmlotno,
                                                          Ec_mmnote = item.Ec_mmnote,
                                                          mmModifier = item.mmModifier,
                                                          mmModifyDate = item.mmModifyDate,
                                                          Ec_purdate = item.Ec_purdate,
                                                          Ec_purorder = item.Ec_purorder,
                                                          Ec_pursupplier = item.Ec_pursupplier,
                                                          Ec_purnote = item.Ec_purnote,
                                                          ppModifier = item.ppModifier,
                                                          ppModifyDate = item.ppModifyDate,
                                                          Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                                                          Ec_iqcorder = "4300000000",
                                                          Ec_iqcnote = "免检",
                                                          iqcModifier = item.iqcModifier,
                                                          iqcModifyDate = item.iqcModifyDate,
                                                          Ec_p1ddate = item.Ec_p1ddate,
                                                          Ec_p1dline = item.Ec_p1dline,
                                                          Ec_p1dlot = item.Ec_p1dlot,
                                                          Ec_p1dnote = item.Ec_p1dnote,
                                                          p1dModifier = item.p1dModifier,
                                                          p1dModifyDate = item.p1dModifyDate,
                                                          Ec_qadate = item.Ec_qadate,
                                                          Ec_qalot = item.Ec_qalot,
                                                          Ec_qanote = item.Ec_qanote,
                                                          qaModifier = item.qaModifier,
                                                          qaModifyDate = item.qaModifyDate,
                                                          UDF01 = item.UDF01,
                                                          UDF02 = item.UDF02,
                                                          UDF03 = item.UDF03,
                                                          UDF04 = item.UDF04,
                                                          UDF05 = item.UDF05,
                                                          UDF06 = item.UDF06,
                                                          UDF51 = item.UDF51,
                                                          UDF52 = item.UDF52,
                                                          UDF53 = item.UDF53,
                                                          UDF54 = item.UDF54,
                                                          UDF55 = item.UDF55,
                                                          UDF56 = item.UDF56,
                                                          IsDeleted = item.IsDeleted,
                                                          Remark = item.Remark,
                                                          Creator = item.Creator,
                                                          CreateDate = item.CreateDate,
                                                          Modifier = GetIdentityName(),
                                                          ModifyDate = DateTime.Now,
                                                      }).ToList();

                    DB.BulkUpdate(UpdateP2DNoPur);
                    //}
                    //if (this.IsManage.SelectedValue == "0")
                    //{
                    //    var P2DPur = from a in DB.Pp_Ec_Subs
                    //                 where a.Ec_no.Contains(Ec_no.Text)
                    //                 where a.Ec_newitem == "0"
                    //                 where a.Ec_procurement == "F"
                    //                 where a.Ec_location == "C003"
                    //                 where a.IsDeleted == 0
                    //                 select a;
                    //    var P2DPurList = P2DPur.ToList().Distinct();
                    //    List<Pp_Ec_Sub> UpdateP2DPurList = (from item in P2DPurList
                    //                                        select new Pp_Ec_Sub
                    //                                        {
                    //                                            GUID = item.GUID,
                    //                                            Ec_no = item.Ec_no,
                    //                                            Ec_model = item.Ec_model,
                    //                                            Ec_bomitem = item.Ec_bomitem,
                    //                                            Ec_bomsubitem = item.Ec_bomsubitem,
                    //                                            Ec_olditem = item.Ec_olditem,
                    //                                            Ec_oldtext = item.Ec_oldtext,
                    //                                            Ec_oldqty = item.Ec_oldqty,
                    //                                            Ec_oldset = item.Ec_oldset,
                    //                                            Ec_newitem = item.Ec_newitem,
                    //                                            Ec_newtext = item.Ec_newtext,
                    //                                            Ec_newqty = item.Ec_newqty,
                    //                                            Ec_newset = item.Ec_newset,
                    //                                            Ec_bomno = item.Ec_bomno,
                    //                                            Ec_change = item.Ec_change,
                    //                                            Ec_local = item.Ec_local,
                    //                                            Ec_note = item.Ec_note,
                    //                                            Ec_process = item.Ec_process,
                    //                                            Ec_bomdate = item.Ec_bomdate,

                    //                                            Ec_procurement = item.Ec_procurement,
                    //                                            Ec_location = item.Ec_location,
                    //                                            IsCheck = item.IsCheck,
                    //                                            IsManage = IsManages,
                    //                                            Ec_eol = item.Ec_eol,

                    //                                            Ec_entrydate = item.Ec_entrydate,
                    //                                            Ec_pmcdate = item.Ec_pmcdate,
                    //                                            Ec_pmclot = item.Ec_pmclot,
                    //                                            Ec_pmcmemo = item.Ec_pmcmemo,
                    //                                            Ec_pmcnote = item.Ec_pmcnote,
                    //                                            Ec_bstock = item.Ec_bstock,
                    //                                            pmcModifier = item.pmcModifier,
                    //                                            pmcModifyDate = item.pmcModifyDate,
                    //                                            Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                            Ec_p2dlot = "与制二无关",
                    //                                            Ec_p2dnote = "与制二无关",
                    //                                            p2dModifier = GetIdentityName(),
                    //                                            p2dModifyDate = DateTime.Now,
                    //                                            Ec_mmdate = item.Ec_mmdate,
                    //                                            Ec_mmlot = item.Ec_mmlot,
                    //                                            Ec_mmlotno = item.Ec_mmlotno,
                    //                                            Ec_mmnote = item.Ec_mmnote,
                    //                                            mmModifier = item.mmModifier,
                    //                                            mmModifyDate = item.mmModifyDate,
                    //                                            Ec_purdate = item.Ec_purdate,
                    //                                            Ec_purorder = item.Ec_purorder,
                    //                                            Ec_pursupplier = item.Ec_pursupplier,
                    //                                            Ec_purnote = item.Ec_purnote,
                    //                                            ppModifier = item.ppModifier,
                    //                                            ppModifyDate = item.ppModifyDate,
                    //                                            Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                            Ec_iqcorder = "4300000000",
                    //                                            Ec_iqcnote = "免检",
                    //                                            iqcModifier = GetIdentityName(),
                    //                                            iqcModifyDate = DateTime.Now,
                    //                                            Ec_p1ddate = item.Ec_p1ddate,
                    //                                            Ec_p1dline = item.Ec_p1dline,
                    //                                            Ec_p1dlot = item.Ec_p1dlot,
                    //                                            Ec_p1dnote = item.Ec_p1dnote,
                    //                                            p1dModifier = item.p1dModifier,
                    //                                            p1dModifyDate = item.p1dModifyDate,
                    //                                            Ec_qadate = item.Ec_qadate,
                    //                                            Ec_qalot = item.Ec_qalot,
                    //                                            Ec_qanote = item.Ec_qanote,
                    //                                            qaModifier = item.qaModifier,
                    //                                            qaModifyDate = item.qaModifyDate,
                    //                                            UDF01 = item.UDF01,
                    //                                            UDF02 = item.UDF02,
                    //                                            UDF03 = item.UDF03,
                    //                                            UDF04 = item.UDF04,
                    //                                            UDF05 = item.UDF05,
                    //                                            UDF06 = item.UDF06,
                    //                                            UDF51 = item.UDF51,
                    //                                            UDF52 = item.UDF52,
                    //                                            UDF53 = item.UDF53,
                    //                                            UDF54 = item.UDF54,
                    //                                            UDF55 = item.UDF55,
                    //                                            UDF56 = item.UDF56,
                    //                                            IsDeleted = item.IsDeleted,
                    //                                            Remark = item.Remark,
                    //                                            Creator = item.Creator,
                    //                                            CreateDate = item.CreateDate,
                    //                                            Modifier = GetIdentityName(),
                    //                                            ModifyDate = DateTime.Now,
                    //                                        }).ToList();

                    //    DB.BulkUpdate(UpdateP2DPurList);
                    //    var MMPur = from a in DB.Pp_Ec_Subs
                    //                where a.Ec_no.Contains(Ec_no.Text)
                    //                where a.Ec_newitem == "0"
                    //                where a.Ec_procurement == "F"
                    //                where a.Ec_location != "C003"
                    //                where a.IsDeleted == 0
                    //                select a;
                    //    var MMPurList = MMPur.ToList().Distinct();
                    //    List<Pp_Ec_Sub> UpdateMMPurList = (from item in MMPurList
                    //                                       select new Pp_Ec_Sub
                    //                                       {
                    //                                           GUID = item.GUID,
                    //                                           Ec_no = item.Ec_no,
                    //                                           Ec_model = item.Ec_model,
                    //                                           Ec_bomitem = item.Ec_bomitem,
                    //                                           Ec_bomsubitem = item.Ec_bomsubitem,
                    //                                           Ec_olditem = item.Ec_olditem,
                    //                                           Ec_oldtext = item.Ec_oldtext,
                    //                                           Ec_oldqty = item.Ec_oldqty,
                    //                                           Ec_oldset = item.Ec_oldset,
                    //                                           Ec_newitem = item.Ec_newitem,
                    //                                           Ec_newtext = item.Ec_newtext,
                    //                                           Ec_newqty = item.Ec_newqty,
                    //                                           Ec_newset = item.Ec_newset,
                    //                                           Ec_bomno = item.Ec_bomno,
                    //                                           Ec_change = item.Ec_change,
                    //                                           Ec_local = item.Ec_local,
                    //                                           Ec_note = item.Ec_note,
                    //                                           Ec_process = item.Ec_process,
                    //                                           Ec_bomdate = item.Ec_bomdate,

                    //                                           Ec_procurement = item.Ec_procurement,
                    //                                           Ec_location = item.Ec_location,
                    //                                           IsCheck = item.IsCheck,
                    //                                           IsManage = 2,
                    //                                           Ec_eol = item.Ec_eol,

                    //                                           Ec_entrydate = item.Ec_entrydate,
                    //                                           Ec_pmcdate = item.Ec_pmcdate,
                    //                                           Ec_pmclot = item.Ec_pmclot,
                    //                                           Ec_pmcmemo = item.Ec_pmcmemo,
                    //                                           Ec_pmcnote = item.Ec_pmcnote,
                    //                                           Ec_bstock = item.Ec_bstock,
                    //                                           pmcModifier = item.pmcModifier,
                    //                                           pmcModifyDate = item.pmcModifyDate,
                    //                                           Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                           Ec_p2dlot = "与制二无关",
                    //                                           Ec_p2dnote = "与制二无关",
                    //                                           p2dModifier = GetIdentityName(),
                    //                                           p2dModifyDate = DateTime.Now,
                    //                                           Ec_mmdate = "",
                    //                                           Ec_mmlot = "",
                    //                                           Ec_mmlotno = "",
                    //                                           Ec_mmnote = "",
                    //                                           mmModifier = item.mmModifier,
                    //                                           mmModifyDate = item.mmModifyDate,
                    //                                           Ec_purdate = item.Ec_purdate,
                    //                                           Ec_purorder = item.Ec_purorder,
                    //                                           Ec_pursupplier = item.Ec_pursupplier,
                    //                                           Ec_purnote = item.Ec_purnote,
                    //                                           ppModifier = item.ppModifier,
                    //                                           ppModifyDate = item.ppModifyDate,
                    //                                           Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                           Ec_iqcorder = "4300000000",
                    //                                           Ec_iqcnote = "免检",
                    //                                           iqcModifier = GetIdentityName(),
                    //                                           iqcModifyDate = DateTime.Now,
                    //                                           Ec_p1ddate = item.Ec_p1ddate,
                    //                                           Ec_p1dline = item.Ec_p1dline,
                    //                                           Ec_p1dlot = item.Ec_p1dlot,
                    //                                           Ec_p1dnote = item.Ec_p1dnote,
                    //                                           p1dModifier = item.p1dModifier,
                    //                                           p1dModifyDate = item.p1dModifyDate,
                    //                                           Ec_qadate = item.Ec_qadate,
                    //                                           Ec_qalot = item.Ec_qalot,
                    //                                           Ec_qanote = item.Ec_qanote,
                    //                                           qaModifier = item.qaModifier,
                    //                                           qaModifyDate = item.qaModifyDate,
                    //                                           UDF01 = item.UDF01,
                    //                                           UDF02 = item.UDF02,
                    //                                           UDF03 = item.UDF03,
                    //                                           UDF04 = item.UDF04,
                    //                                           UDF05 = item.UDF05,
                    //                                           UDF06 = item.UDF06,
                    //                                           UDF51 = item.UDF51,
                    //                                           UDF52 = item.UDF52,
                    //                                           UDF53 = item.UDF53,
                    //                                           UDF54 = item.UDF54,
                    //                                           UDF55 = item.UDF55,
                    //                                           UDF56 = item.UDF56,
                    //                                           IsDeleted = item.IsDeleted,
                    //                                           Remark = item.Remark,
                    //                                           Creator = item.Creator,
                    //                                           CreateDate = item.CreateDate,
                    //                                           Modifier = GetIdentityName(),
                    //                                           ModifyDate = DateTime.Now,
                    //                                       }).ToList();

                    //    DB.BulkUpdate(UpdateMMPurList);

                    //    var P2DNoPur = from a in DB.Pp_Ec_Subs
                    //                   where a.Ec_no.Contains(Ec_no.Text)
                    //                   where a.Ec_newitem == "0"
                    //                   where a.Ec_procurement == "E"
                    //                   //where a.Ec_location == "C003"
                    //                   where a.IsDeleted == 0
                    //                   select a;
                    //    var P2DNoPurList = P2DNoPur.ToList().Distinct();
                    //    List<Pp_Ec_Sub> UpdateP2DNoPur = (from item in P2DNoPurList
                    //                                      select new Pp_Ec_Sub
                    //                                      {
                    //                                          GUID = item.GUID,
                    //                                          Ec_no = item.Ec_no,
                    //                                          Ec_model = item.Ec_model,
                    //                                          Ec_bomitem = item.Ec_bomitem,
                    //                                          Ec_bomsubitem = item.Ec_bomsubitem,
                    //                                          Ec_olditem = item.Ec_olditem,
                    //                                          Ec_oldtext = item.Ec_oldtext,
                    //                                          Ec_oldqty = item.Ec_oldqty,
                    //                                          Ec_oldset = item.Ec_oldset,
                    //                                          Ec_newitem = item.Ec_newitem,
                    //                                          Ec_newtext = item.Ec_newtext,
                    //                                          Ec_newqty = item.Ec_newqty,
                    //                                          Ec_newset = item.Ec_newset,
                    //                                          Ec_bomno = item.Ec_bomno,
                    //                                          Ec_change = item.Ec_change,
                    //                                          Ec_local = item.Ec_local,
                    //                                          Ec_note = item.Ec_note,
                    //                                          Ec_process = item.Ec_process,
                    //                                          Ec_bomdate = item.Ec_bomdate,

                    //                                          Ec_procurement = item.Ec_procurement,
                    //                                          Ec_location = item.Ec_location,
                    //                                          IsCheck = item.IsCheck,
                    //                                          IsManage = IsManages,
                    //                                          Ec_eol = item.Ec_eol,

                    //                                          Ec_entrydate = item.Ec_entrydate,
                    //                                          Ec_pmcdate = item.Ec_pmcdate,
                    //                                          Ec_pmclot = item.Ec_pmclot,
                    //                                          Ec_pmcmemo = item.Ec_pmcmemo,
                    //                                          Ec_pmcnote = item.Ec_pmcnote,
                    //                                          Ec_bstock = item.Ec_bstock,
                    //                                          pmcModifier = item.pmcModifier,
                    //                                          pmcModifyDate = item.pmcModifyDate,
                    //                                          Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                          Ec_p2dlot = "与制二无关",
                    //                                          Ec_p2dnote = "与制二无关",
                    //                                          p2dModifier = GetIdentityName(),
                    //                                          p2dModifyDate = DateTime.Now,
                    //                                          Ec_mmdate = item.Ec_mmdate,
                    //                                          Ec_mmlot = item.Ec_mmlot,
                    //                                          Ec_mmlotno = item.Ec_mmlotno,
                    //                                          Ec_mmnote = item.Ec_mmnote,
                    //                                          mmModifier = item.mmModifier,
                    //                                          mmModifyDate = item.mmModifyDate,
                    //                                          Ec_purdate = item.Ec_purdate,
                    //                                          Ec_purorder = item.Ec_purorder,
                    //                                          Ec_pursupplier = item.Ec_pursupplier,
                    //                                          Ec_purnote = item.Ec_purnote,
                    //                                          ppModifier = item.ppModifier,
                    //                                          ppModifyDate = item.ppModifyDate,
                    //                                          Ec_iqcdate = DateTime.Now.ToString("yyyyMMdd"),
                    //                                          Ec_iqcorder = "4300000000",
                    //                                          Ec_iqcnote = "免检",
                    //                                          iqcModifier = GetIdentityName(),
                    //                                          iqcModifyDate = DateTime.Now,
                    //                                          Ec_p1ddate = item.Ec_p1ddate,
                    //                                          Ec_p1dline = item.Ec_p1dline,
                    //                                          Ec_p1dlot = item.Ec_p1dlot,
                    //                                          Ec_p1dnote = item.Ec_p1dnote,
                    //                                          p1dModifier = item.p1dModifier,
                    //                                          p1dModifyDate = item.p1dModifyDate,
                    //                                          Ec_qadate = item.Ec_qadate,
                    //                                          Ec_qalot = item.Ec_qalot,
                    //                                          Ec_qanote = item.Ec_qanote,
                    //                                          qaModifier = item.qaModifier,
                    //                                          qaModifyDate = item.qaModifyDate,
                    //                                          UDF01 = item.UDF01,
                    //                                          UDF02 = item.UDF02,
                    //                                          UDF03 = item.UDF03,
                    //                                          UDF04 = item.UDF04,
                    //                                          UDF05 = item.UDF05,
                    //                                          UDF06 = item.UDF06,
                    //                                          UDF51 = item.UDF51,
                    //                                          UDF52 = item.UDF52,
                    //                                          UDF53 = item.UDF53,
                    //                                          UDF54 = item.UDF54,
                    //                                          UDF55 = item.UDF55,
                    //                                          UDF56 = item.UDF56,
                    //                                          IsDeleted = item.IsDeleted,
                    //                                          Remark = item.Remark,
                    //                                          Creator = item.Creator,
                    //                                          CreateDate = item.CreateDate,
                    //                                          Modifier = GetIdentityName(),
                    //                                          ModifyDate = DateTime.Now,
                    //                                      }).ToList();

                    //    DB.BulkUpdate(UpdateP2DNoPur);
                    //}

                    #endregion 7.新物料为空时更新旧物料管理
                }

                #region 6.更新旧品库存

                var ItemStock = from a in DB.Pp_Ec_Subs
                                where a.Ec_no.Contains(Ec_no.Text)
                                where a.Ec_olditem != "0"
                                select a;
                var UpdateItemStock = from a in ItemStock
                                      join b in DB.Pp_SapMaterials on a.Ec_olditem equals b.D_SAP_ZCA1D_Z002
                                      where b.D_SAP_ZCA1D_Z033 != 0
                                      select new
                                      {
                                          a.GUID,
                                          a.Ec_no,
                                          a.Ec_model,
                                          a.Ec_bomitem,
                                          a.Ec_bomsubitem,
                                          a.Ec_olditem,
                                          a.Ec_oldtext,
                                          a.Ec_oldqty,
                                          a.Ec_oldset,
                                          a.Ec_newitem,
                                          a.Ec_newtext,
                                          a.Ec_newqty,
                                          a.Ec_newset,
                                          a.Ec_bomno,
                                          a.Ec_change,
                                          a.Ec_local,
                                          a.Ec_note,
                                          a.Ec_process,
                                          a.Ec_procurement,
                                          a.Ec_location,
                                          a.IsCheck,
                                          a.IsManage,
                                          a.IsMmManage,
                                          a.IsPcbaManage,
                                          a.IsAssyManage,
                                          a.Ec_eol,
                                          a.Ec_bomdate,
                                          a.Ec_entrydate,
                                          a.Ec_pmcdate,
                                          a.Ec_pmclot,
                                          a.Ec_pmcmemo,
                                          a.Ec_pmcnote,
                                          Ec_bstock = b.D_SAP_ZCA1D_Z033,
                                          a.pmcModifier,
                                          a.pmcModifyDate,
                                          a.Ec_p2ddate,
                                          a.Ec_p2dlot,
                                          a.Ec_p2dnote,
                                          a.p2dModifier,
                                          a.p2dModifyDate,
                                          a.Ec_mmdate,
                                          a.Ec_mmlot,
                                          a.Ec_mmlotno,
                                          a.Ec_mmnote,
                                          a.mmModifier,
                                          a.mmModifyDate,
                                          a.Ec_purdate,
                                          a.Ec_purorder,
                                          a.Ec_pursupplier,
                                          a.Ec_purnote,
                                          a.ppModifier,
                                          a.ppModifyDate,
                                          a.Ec_iqcdate,
                                          a.Ec_iqcorder,
                                          a.Ec_iqcnote,
                                          a.iqcModifier,
                                          a.iqcModifyDate,
                                          a.Ec_p1ddate,
                                          a.Ec_p1dline,
                                          a.Ec_p1dlot,
                                          a.Ec_p1dnote,
                                          a.p1dModifier,
                                          a.p1dModifyDate,

                                          a.Ec_qadate,
                                          a.Ec_qalot,
                                          a.Ec_qanote,
                                          a.qaModifier,
                                          a.qaModifyDate,
                                          a.UDF01,
                                          a.UDF02,
                                          a.UDF03,
                                          a.UDF04,
                                          a.UDF05,
                                          a.UDF06,
                                          a.UDF51,
                                          a.UDF52,
                                          a.UDF53,
                                          a.UDF54,
                                          a.UDF55,
                                          a.UDF56,
                                          a.IsDeleted,
                                          a.Remark,
                                          a.Creator,
                                          a.CreateDate,
                                          a.Modifier,
                                          a.ModifyDate,
                                      };

                var ItemStockList = UpdateItemStock.ToList().Distinct();
                List<Pp_Ec_Sub> UpdateStockList = (from item in ItemStockList
                                                   select new Pp_Ec_Sub
                                                   {
                                                       GUID = item.GUID,
                                                       Ec_no = item.Ec_no,
                                                       Ec_model = item.Ec_model,
                                                       Ec_bomitem = item.Ec_bomitem,
                                                       Ec_bomsubitem = item.Ec_bomsubitem,
                                                       Ec_olditem = item.Ec_olditem,
                                                       Ec_oldtext = item.Ec_oldtext,
                                                       Ec_oldqty = item.Ec_oldqty,
                                                       Ec_oldset = item.Ec_oldset,
                                                       Ec_newitem = item.Ec_newitem,
                                                       Ec_newtext = item.Ec_newtext,
                                                       Ec_newqty = item.Ec_newqty,
                                                       Ec_newset = item.Ec_newset,
                                                       Ec_bomno = item.Ec_bomno,
                                                       Ec_change = item.Ec_change,
                                                       Ec_local = item.Ec_local,
                                                       Ec_note = item.Ec_note,
                                                       Ec_process = item.Ec_process,
                                                       Ec_bomdate = item.Ec_bomdate,

                                                       Ec_procurement = item.Ec_procurement,
                                                       Ec_location = item.Ec_location,
                                                       IsCheck = item.IsCheck,
                                                       IsManage = item.IsManage,
                                                       IsMmManage = item.IsMmManage,
                                                       IsPcbaManage = item.IsPcbaManage,
                                                       IsAssyManage = item.IsAssyManage,
                                                       Ec_eol = item.Ec_eol,

                                                       Ec_entrydate = item.Ec_entrydate,
                                                       Ec_pmcdate = item.Ec_pmcdate,
                                                       Ec_pmclot = item.Ec_pmclot,
                                                       Ec_pmcmemo = item.Ec_pmcmemo,
                                                       Ec_pmcnote = item.Ec_pmcnote,
                                                       Ec_bstock = item.Ec_bstock,
                                                       pmcModifier = item.pmcModifier,
                                                       pmcModifyDate = item.pmcModifyDate,
                                                       Ec_p2ddate = item.Ec_p2ddate,
                                                       Ec_p2dlot = item.Ec_p2dlot,
                                                       Ec_p2dnote = item.Ec_p2dnote,
                                                       p2dModifier = item.p2dModifier,
                                                       p2dModifyDate = item.p2dModifyDate,
                                                       Ec_mmdate = item.Ec_mmdate,
                                                       Ec_mmlot = item.Ec_mmlot,
                                                       Ec_mmlotno = item.Ec_mmlotno,
                                                       Ec_mmnote = item.Ec_mmnote,
                                                       mmModifier = item.mmModifier,
                                                       mmModifyDate = item.mmModifyDate,
                                                       Ec_purdate = item.Ec_purdate,
                                                       Ec_purorder = item.Ec_purorder,
                                                       Ec_pursupplier = item.Ec_pursupplier,
                                                       Ec_purnote = item.Ec_purnote,
                                                       ppModifier = item.ppModifier,
                                                       ppModifyDate = item.ppModifyDate,
                                                       Ec_iqcdate = item.Ec_iqcdate,
                                                       Ec_iqcorder = item.Ec_iqcorder,
                                                       Ec_iqcnote = item.Ec_iqcnote,
                                                       iqcModifier = item.iqcModifier,
                                                       iqcModifyDate = item.iqcModifyDate,
                                                       Ec_p1ddate = item.Ec_p1ddate,
                                                       Ec_p1dline = item.Ec_p1dline,
                                                       Ec_p1dlot = item.Ec_p1dlot,
                                                       Ec_p1dnote = item.Ec_p1dnote,
                                                       p1dModifier = item.p1dModifier,
                                                       p1dModifyDate = item.p1dModifyDate,
                                                       Ec_qadate = item.Ec_qadate,
                                                       Ec_qalot = item.Ec_qalot,
                                                       Ec_qanote = item.Ec_qanote,
                                                       qaModifier = item.qaModifier,
                                                       qaModifyDate = item.qaModifyDate,
                                                       UDF01 = item.UDF01,
                                                       UDF02 = item.UDF02,
                                                       UDF03 = item.UDF03,
                                                       UDF04 = item.UDF04,
                                                       UDF05 = item.UDF05,
                                                       UDF06 = item.UDF06,
                                                       UDF51 = item.UDF51,
                                                       UDF52 = item.UDF52,
                                                       UDF53 = item.UDF53,
                                                       UDF54 = item.UDF54,
                                                       UDF55 = item.UDF55,
                                                       UDF56 = item.UDF56,
                                                       IsDeleted = item.IsDeleted,
                                                       Remark = item.Remark,

                                                       Creator = item.Creator,
                                                       CreateDate = item.CreateDate,
                                                       Modifier = GetIdentityName(),
                                                       ModifyDate = DateTime.Now,
                                                   }).ToList();

                DB.BulkUpdate(UpdateStockList);

                #endregion 6.更新旧品库存

                //}
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

        //SOP确认
        private void SaveItemSop()//新增SOP确认
        {
            #region 1.新增SOP

            //if (this.IsSopUpdate.SelectedValue == "1")
            //{
            var q_NonPurchaseItem = from a in DB.Pp_Ec_Sops
                                        //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                                    where a.Ec_no.Contains(Ec_no.Text)
                                    //where b.Ec_no == strecn
                                    //where a.Prodate == sdate//投入日期
                                    select a;
            var resultNonPurchase = q_NonPurchaseItem.Distinct().ToList();
            List<Pp_Ec_Sop> NonPurchaseList = (from item in resultNonPurchase
                                               select new Pp_Ec_Sop
                                               {
                                                   GUID = item.GUID,
                                                   Ec_issuedate = Ec_issuedate.Text,
                                                   Ec_leader = Ec_leader.SelectedItem.Text,
                                                   ispengaModifysop = 1,
                                                   ispengpModifysop = 1,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,

                                                   Ec_entrydate = item.Ec_entrydate,

                                                   //    //组立
                                                   Ec_pengadate = item.Ec_pengadate,
                                                   Ec_penganote = item.Ec_penganote,
                                                   pengaModifier = item.pengaModifier,
                                                   pengaModifyDate = item.pengaModifyDate,
                                                   //    //PCBA
                                                   Ec_pengpdate = item.Ec_pengpdate,
                                                   Ec_pengpnote = item.Ec_pengpnote,
                                                   pengpModifier = item.pengpModifier,
                                                   pengpModifyDate = item.pengpModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = item.UDF02,
                                                   UDF03 = item.UDF03,
                                                   UDF04 = item.UDF04,
                                                   UDF05 = item.UDF05,
                                                   UDF06 = item.UDF06,
                                                   UDF51 = item.UDF51,
                                                   UDF52 = item.UDF52,
                                                   UDF53 = item.UDF53,
                                                   UDF54 = item.UDF54,
                                                   UDF55 = item.UDF55,
                                                   UDF56 = item.UDF56,
                                                   IsDeleted = 0,
                                                   Remark = item.Remark,
                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();
            DB.BulkUpdate(NonPurchaseList);
            DB.BulkSaveChanges();
            //}

            //if (this.IsSopUpdate.SelectedValue == "0")
            //{
            //    var q_NonPurchaseItem = from a in DB.Pp_Ec_Sops
            //                                //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
            //                            where a.Ec_no.Contains(Ec_no.Text)
            //                            //where b.Ec_no == strecn
            //                            //where a.Prodate == sdate//投入日期
            //                            select a;
            //    var resultNonPurchase = q_NonPurchaseItem.Distinct().ToList();
            //    List<Pp_Ec_Sop> NonPurchaseList = (from item in resultNonPurchase
            //                                       select new Pp_Ec_Sop
            //                                       {
            //                                           GUID = item.GUID,
            //                                           Ec_issuedate = Ec_issuedate.Text,
            //                                           Ec_leader = Ec_leader.SelectedItem.Text,
            //                                           ispengaModifysop = byte.Parse(IsSopUpdate.SelectedValue),
            //                                           ispengpModifysop = byte.Parse(IsSopUpdate.SelectedValue),
            //                                           Ec_no = item.Ec_no,
            //                                           Ec_model = item.Ec_model,

            //                                           Ec_entrydate = item.Ec_entrydate,

            //                                           //    //组立
            //                                           Ec_pengadate = item.Ec_pengadate,
            //                                           Ec_penganote = item.Ec_penganote,
            //                                           pengaModifier = item.pengaModifier,
            //                                           pengaModifyDate = item.pengaModifyDate,
            //                                           //    //PCBA
            //                                           Ec_pengpdate = item.Ec_pengpdate,
            //                                           Ec_pengpnote = item.Ec_pengpnote,
            //                                           pengpModifier = item.pengpModifier,
            //                                           pengpModifyDate = item.pengpModifyDate,

            //                                           UDF01 = item.UDF01,
            //                                           UDF02 = item.UDF02,
            //                                           UDF03 = item.UDF03,
            //                                           UDF04 = item.UDF04,
            //                                           UDF05 = item.UDF05,
            //                                           UDF06 = item.UDF06,
            //                                           UDF51 = item.UDF51,
            //                                           UDF52 = item.UDF52,
            //                                           UDF53 = item.UDF53,
            //                                           UDF54 = item.UDF54,
            //                                           UDF55 = item.UDF55,
            //                                           UDF56 = item.UDF56,
            //                                           IsDeleted = 0,
            //                                           Remark = item.Remark,
            //                                           Creator = item.Creator,
            //                                           CreateDate = item.CreateDate,
            //                                           Modifier = GetIdentityName(),
            //                                           ModifyDate = DateTime.Now,
            //                                       }).ToList();
            //    DB.BulkUpdate(NonPurchaseList);
            //    DB.BulkSaveChanges();
            //}

            #endregion 1.新增SOP
        }

        //SOP确认
        private void UpdateItemSub()//更新EC单身
        {
            #region 1.更新EC单身

            //批量删除单身
            //DeleteEcSubs();

            #region Pp_Ecs

            var q_NotEollist = from a in DB.Pp_Ec_Subs
                               where a.Ec_no.Contains(Ec_no.Text)
                               select a;

            #endregion Pp_Ecs

            //if (this.IsManage.SelectedValue == "1")
            //{
            //Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课需要管理！", "提示信息", MessageBoxIcon.Information);

            #region 1.非采购件

            //1.非采购件
            var New_NonPurchase = from a in q_NotEollist
                                  where a.Ec_procurement == "E"
                                  select a;

            var result_NonPurchase = New_NonPurchase.Distinct().ToList();
            List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
                                                   select new Pp_Ec_Sub
                                                   {
                                                       GUID = item.GUID,
                                                       Ec_no = item.Ec_no,
                                                       Ec_model = item.Ec_model,
                                                       Ec_bomitem = item.Ec_bomitem,
                                                       Ec_bomsubitem = item.Ec_bomsubitem,
                                                       Ec_olditem = item.Ec_olditem,
                                                       Ec_oldtext = item.Ec_oldtext,
                                                       Ec_oldqty = item.Ec_oldqty,
                                                       Ec_oldset = item.Ec_oldset,
                                                       Ec_newitem = item.Ec_newitem,
                                                       Ec_newtext = item.Ec_newtext,
                                                       Ec_newqty = item.Ec_newqty,
                                                       Ec_newset = item.Ec_newset,
                                                       Ec_bomno = item.Ec_bomno,
                                                       Ec_change = item.Ec_change,
                                                       Ec_local = item.Ec_local,
                                                       Ec_note = item.Ec_note,
                                                       Ec_process = item.Ec_process,
                                                       Ec_bomdate = item.Ec_bomdate,

                                                       Ec_procurement = item.Ec_procurement,
                                                       Ec_location = item.Ec_location,
                                                       IsCheck = item.IsCheck,
                                                       IsManage = 3,
                                                       Ec_eol = item.Ec_eol,

                                                       Ec_entrydate = item.Ec_entrydate,

                                                       Ec_pmcdate = item.Ec_pmcdate,
                                                       Ec_pmclot = item.Ec_pmclot,
                                                       Ec_pmcmemo = item.Ec_pmcmemo,
                                                       Ec_pmcnote = item.Ec_pmcnote,
                                                       Ec_bstock = 0,
                                                       pmcModifier = item.pmcModifier,
                                                       pmcModifyDate = item.pmcModifyDate,

                                                       //    //制二
                                                       Ec_p2ddate = item.Ec_p2ddate,
                                                       Ec_p2dlot = item.Ec_p2dlot,
                                                       Ec_p2dnote = item.Ec_p2dnote,
                                                       p2dModifier = item.p2dModifier,
                                                       p2dModifyDate = item.p2dModifyDate,

                                                       //    //部管
                                                       Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                       Ec_mmlot = "与部管无关",
                                                       Ec_mmlotno = "与部管无关",
                                                       Ec_mmnote = "与部管无关",
                                                       mmModifier = GetIdentityName(),
                                                       mmModifyDate = DateTime.Now,

                                                       //    //采购
                                                       Ec_purdate = item.Ec_purdate,
                                                       Ec_purorder = item.Ec_purorder,
                                                       Ec_pursupplier = item.Ec_pursupplier,
                                                       Ec_purnote = item.Ec_purnote,
                                                       ppModifier = item.ppModifier,
                                                       ppModifyDate = item.ppModifyDate,

                                                       //    //受检
                                                       Ec_iqcdate = item.Ec_iqcdate,
                                                       Ec_iqcorder = item.Ec_iqcorder,
                                                       Ec_iqcnote = item.Ec_iqcnote,
                                                       iqcModifier = item.iqcModifier,
                                                       iqcModifyDate = item.iqcModifyDate,

                                                       //    //制一
                                                       Ec_p1ddate = item.Ec_p1ddate,
                                                       Ec_p1dline = item.Ec_p1dline,
                                                       Ec_p1dlot = item.Ec_p1dlot,
                                                       Ec_p1dnote = item.Ec_p1dnote,
                                                       p1dModifier = item.p1dModifier,
                                                       p1dModifyDate = item.p1dModifyDate,

                                                       //    //品管
                                                       Ec_qadate = item.Ec_qadate,
                                                       Ec_qalot = item.Ec_qalot,
                                                       Ec_qanote = item.Ec_qanote,
                                                       qaModifier = item.qaModifier,
                                                       qaModifyDate = item.qaModifyDate,

                                                       UDF01 = item.UDF01,
                                                       UDF02 = "",
                                                       UDF03 = "",
                                                       UDF04 = "",
                                                       UDF05 = "",
                                                       UDF06 = "",
                                                       UDF51 = 0,
                                                       UDF52 = 0,
                                                       UDF53 = 0,
                                                       UDF54 = 0,
                                                       UDF55 = 0,
                                                       UDF56 = 0,
                                                       IsDeleted = 0,

                                                       Remark = item.Remark,
                                                       Creator = item.Creator,
                                                       CreateDate = item.CreateDate,
                                                       Modifier = GetIdentityName(),
                                                       ModifyDate = DateTime.Now,
                                                   }).ToList();
            DB.BulkUpdate(New_NonPurchaseList);
            DB.BulkSaveChanges();

            #endregion 1.非采购件

            #region 2.新物料为空

            var NonItem = from a in q_NotEollist
                          where a.Ec_no == "0"
                          select a;
            var result_NonItem = NonItem.Distinct().ToList();
            List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
                                               select new Pp_Ec_Sub
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,
                                                   Ec_bomitem = item.Ec_bomitem,
                                                   Ec_bomsubitem = item.Ec_bomsubitem,
                                                   Ec_olditem = item.Ec_olditem,
                                                   Ec_oldtext = item.Ec_oldtext,
                                                   Ec_oldqty = item.Ec_oldqty,
                                                   Ec_oldset = item.Ec_oldset,
                                                   Ec_newitem = item.Ec_newitem,
                                                   Ec_newtext = item.Ec_newtext,
                                                   Ec_newqty = item.Ec_newqty,
                                                   Ec_newset = item.Ec_newset,
                                                   Ec_bomno = item.Ec_bomno,
                                                   Ec_change = item.Ec_change,
                                                   Ec_local = item.Ec_local,
                                                   Ec_note = item.Ec_note,
                                                   Ec_process = item.Ec_process,
                                                   Ec_bomdate = item.Ec_bomdate,

                                                   Ec_procurement = item.Ec_procurement,
                                                   Ec_location = item.Ec_location,
                                                   IsCheck = item.IsCheck,
                                                   IsManage = 3,
                                                   Ec_eol = item.Ec_eol,

                                                   Ec_entrydate = item.Ec_entrydate,

                                                   Ec_pmcdate = item.Ec_pmcdate,
                                                   Ec_pmclot = item.Ec_pmclot,
                                                   Ec_pmcmemo = item.Ec_pmcmemo,
                                                   Ec_pmcnote = item.Ec_pmcnote,
                                                   Ec_bstock = 0,
                                                   pmcModifier = item.pmcModifier,
                                                   pmcModifyDate = item.pmcModifyDate,

                                                   //    //制二
                                                   Ec_p2ddate = item.Ec_p2ddate,
                                                   Ec_p2dlot = item.Ec_p2dlot,
                                                   Ec_p2dnote = item.Ec_p2dnote,
                                                   p2dModifier = item.p2dModifier,
                                                   p2dModifyDate = item.p2dModifyDate,

                                                   //    //部管
                                                   Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                   Ec_mmlot = "与部管无关",
                                                   Ec_mmlotno = "与部管无关",
                                                   Ec_mmnote = "与部管无关",
                                                   mmModifier = GetIdentityName(),
                                                   mmModifyDate = DateTime.Now,

                                                   //    //采购
                                                   Ec_purdate = item.Ec_purdate,
                                                   Ec_purorder = item.Ec_purorder,
                                                   Ec_pursupplier = item.Ec_pursupplier,
                                                   Ec_purnote = item.Ec_purnote,
                                                   ppModifier = item.ppModifier,
                                                   ppModifyDate = item.ppModifyDate,

                                                   //    //受检
                                                   Ec_iqcdate = item.Ec_iqcdate,
                                                   Ec_iqcorder = item.Ec_iqcorder,
                                                   Ec_iqcnote = item.Ec_iqcnote,
                                                   iqcModifier = item.iqcModifier,
                                                   iqcModifyDate = item.iqcModifyDate,

                                                   //    //制一
                                                   Ec_p1ddate = item.Ec_p1ddate,
                                                   Ec_p1dline = item.Ec_p1dline,
                                                   Ec_p1dlot = item.Ec_p1dlot,
                                                   Ec_p1dnote = item.Ec_p1dnote,
                                                   p1dModifier = item.p1dModifier,
                                                   p1dModifyDate = item.p1dModifyDate,

                                                   //    //品管
                                                   Ec_qadate = item.Ec_qadate,
                                                   Ec_qalot = item.Ec_qalot,
                                                   Ec_qanote = item.Ec_qanote,
                                                   qaModifier = item.qaModifier,
                                                   qaModifyDate = item.qaModifyDate,

                                                   UDF01 = item.UDF01,
                                                   UDF02 = "",
                                                   UDF03 = "",
                                                   UDF04 = "",
                                                   UDF05 = "",
                                                   UDF06 = "",
                                                   UDF51 = 0,
                                                   UDF52 = 0,
                                                   UDF53 = 0,
                                                   UDF54 = 0,
                                                   UDF55 = 0,
                                                   UDF56 = 0,
                                                   IsDeleted = 0,

                                                   Remark = item.Remark,
                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = GetIdentityName(),
                                                   ModifyDate = DateTime.Now,
                                               }).ToList();

            DB.BulkUpdate(New_NonItemList);
            //DB.BulkSaveChanges();

            #endregion 2.新物料为空

            #region 3.采购件非C003

            //1.采购件非C003

            var MMPurchase = from a in q_NotEollist

                             where a.Ec_procurement == "F"
                             where a.Ec_location != "C003"
                             select a;

            var result_MMPurchase = MMPurchase.Distinct().ToList();
            List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
                                                  select new Pp_Ec_Sub
                                                  {
                                                      GUID = item.GUID,
                                                      Ec_no = item.Ec_no,
                                                      Ec_model = item.Ec_model,
                                                      Ec_bomitem = item.Ec_bomitem,
                                                      Ec_bomsubitem = item.Ec_bomsubitem,
                                                      Ec_olditem = item.Ec_olditem,
                                                      Ec_oldtext = item.Ec_oldtext,
                                                      Ec_oldqty = item.Ec_oldqty,
                                                      Ec_oldset = item.Ec_oldset,
                                                      Ec_newitem = item.Ec_newitem,
                                                      Ec_newtext = item.Ec_newtext,
                                                      Ec_newqty = item.Ec_newqty,
                                                      Ec_newset = item.Ec_newset,
                                                      Ec_bomno = item.Ec_bomno,
                                                      Ec_change = item.Ec_change,
                                                      Ec_local = item.Ec_local,
                                                      Ec_note = item.Ec_note,
                                                      Ec_process = item.Ec_process,
                                                      Ec_bomdate = item.Ec_bomdate,

                                                      Ec_procurement = item.Ec_procurement,
                                                      Ec_location = item.Ec_location,
                                                      IsCheck = item.IsCheck,
                                                      IsManage = 1,
                                                      Ec_eol = item.Ec_eol,

                                                      Ec_entrydate = item.Ec_entrydate,

                                                      Ec_pmcdate = item.Ec_pmcdate,
                                                      Ec_pmclot = item.Ec_pmclot,
                                                      Ec_pmcmemo = item.Ec_pmcmemo,
                                                      Ec_pmcnote = item.Ec_pmcnote,
                                                      Ec_bstock = 0,
                                                      pmcModifier = item.pmcModifier,
                                                      pmcModifyDate = item.pmcModifyDate,

                                                      //    //制二
                                                      Ec_p2ddate = item.Ec_p2ddate,
                                                      Ec_p2dlot = item.Ec_p2dlot,
                                                      Ec_p2dnote = item.Ec_p2dnote,
                                                      p2dModifier = item.p2dModifier,
                                                      p2dModifyDate = item.p2dModifyDate,

                                                      //    //部管
                                                      Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
                                                      Ec_mmlot = "与部管无关",
                                                      Ec_mmlotno = "与部管无关",
                                                      Ec_mmnote = "与部管无关",
                                                      mmModifier = GetIdentityName(),
                                                      mmModifyDate = DateTime.Now,

                                                      //    //采购
                                                      Ec_purdate = item.Ec_purdate,
                                                      Ec_purorder = item.Ec_purorder,
                                                      Ec_pursupplier = item.Ec_pursupplier,
                                                      Ec_purnote = item.Ec_purnote,
                                                      ppModifier = item.ppModifier,
                                                      ppModifyDate = item.ppModifyDate,

                                                      //    //受检
                                                      Ec_iqcdate = item.Ec_iqcdate,
                                                      Ec_iqcorder = item.Ec_iqcorder,
                                                      Ec_iqcnote = item.Ec_iqcnote,
                                                      iqcModifier = item.iqcModifier,
                                                      iqcModifyDate = item.iqcModifyDate,

                                                      //    //制一
                                                      Ec_p1ddate = item.Ec_p1ddate,
                                                      Ec_p1dline = item.Ec_p1dline,
                                                      Ec_p1dlot = item.Ec_p1dlot,
                                                      Ec_p1dnote = item.Ec_p1dnote,
                                                      p1dModifier = item.p1dModifier,
                                                      p1dModifyDate = item.p1dModifyDate,

                                                      //    //品管
                                                      Ec_qadate = item.Ec_qadate,
                                                      Ec_qalot = item.Ec_qalot,
                                                      Ec_qanote = item.Ec_qanote,
                                                      qaModifier = item.qaModifier,
                                                      qaModifyDate = item.qaModifyDate,

                                                      UDF01 = item.UDF01,
                                                      UDF02 = "",
                                                      UDF03 = "",
                                                      UDF04 = "",
                                                      UDF05 = "",
                                                      UDF06 = "",
                                                      UDF51 = 0,
                                                      UDF52 = 0,
                                                      UDF53 = 0,
                                                      UDF54 = 0,
                                                      UDF55 = 0,
                                                      UDF56 = 0,
                                                      IsDeleted = 0,

                                                      Remark = item.Remark,
                                                      Creator = item.Creator,
                                                      CreateDate = item.CreateDate,
                                                      Modifier = GetIdentityName(),
                                                      ModifyDate = DateTime.Now,
                                                  }).ToList();
            DB.BulkUpdate(New_MMPurchaseList);
            //DB.BulkSaveChanges();

            #endregion 3.采购件非C003

            #region 4.采购件C003

            //1.采购件C003
            //1.采购件非C003

            var P2dPurchase = from a in q_NotEollist

                              where a.Ec_procurement == "F"
                              where a.Ec_location == "C003"
                              select a;

            var result_P2dPurchase = P2dPurchase.Distinct().ToList();
            List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
                                                          select new Pp_Ec_Sub
                                                          {
                                                              GUID = item.GUID,
                                                              Ec_no = item.Ec_no,
                                                              Ec_model = item.Ec_model,
                                                              Ec_bomitem = item.Ec_bomitem,
                                                              Ec_bomsubitem = item.Ec_bomsubitem,
                                                              Ec_olditem = item.Ec_olditem,
                                                              Ec_oldtext = item.Ec_oldtext,
                                                              Ec_oldqty = item.Ec_oldqty,
                                                              Ec_oldset = item.Ec_oldset,
                                                              Ec_newitem = item.Ec_newitem,
                                                              Ec_newtext = item.Ec_newtext,
                                                              Ec_newqty = item.Ec_newqty,
                                                              Ec_newset = item.Ec_newset,
                                                              Ec_bomno = item.Ec_bomno,
                                                              Ec_change = item.Ec_change,
                                                              Ec_local = item.Ec_local,
                                                              Ec_note = item.Ec_note,
                                                              Ec_process = item.Ec_process,
                                                              Ec_bomdate = item.Ec_bomdate,

                                                              Ec_procurement = item.Ec_procurement,
                                                              Ec_location = item.Ec_location,
                                                              IsCheck = item.IsCheck,
                                                              IsManage = 3,
                                                              Ec_eol = item.Ec_eol,

                                                              Ec_entrydate = item.Ec_entrydate,

                                                              Ec_pmcdate = item.Ec_pmcdate,
                                                              Ec_pmclot = item.Ec_pmclot,
                                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                                              Ec_pmcnote = item.Ec_pmcnote,
                                                              Ec_bstock = 0,
                                                              pmcModifier = item.pmcModifier,
                                                              pmcModifyDate = item.pmcModifyDate,

                                                              //    //制二
                                                              Ec_p2ddate = item.Ec_p2ddate,
                                                              Ec_p2dlot = item.Ec_p2dlot,
                                                              Ec_p2dnote = item.Ec_p2dnote,
                                                              p2dModifier = item.p2dModifier,
                                                              p2dModifyDate = item.p2dModifyDate,

                                                              //    //部管
                                                              Ec_mmdate = item.Ec_mmdate,
                                                              Ec_mmlot = item.Ec_mmlot,
                                                              Ec_mmlotno = item.Ec_mmlotno,
                                                              Ec_mmnote = item.Ec_mmnote,
                                                              mmModifier = item.mmModifier,
                                                              mmModifyDate = item.mmModifyDate,

                                                              //    //采购
                                                              Ec_purdate = item.Ec_purdate,
                                                              Ec_purorder = item.Ec_purorder,
                                                              Ec_pursupplier = item.Ec_pursupplier,
                                                              Ec_purnote = item.Ec_purnote,
                                                              ppModifier = item.ppModifier,
                                                              ppModifyDate = item.ppModifyDate,

                                                              //    //受检
                                                              Ec_iqcdate = item.Ec_iqcdate,
                                                              Ec_iqcorder = item.Ec_iqcorder,
                                                              Ec_iqcnote = item.Ec_iqcnote,
                                                              iqcModifier = item.iqcModifier,
                                                              iqcModifyDate = item.iqcModifyDate,

                                                              //    //制一
                                                              Ec_p1ddate = item.Ec_p1ddate,
                                                              Ec_p1dline = item.Ec_p1dline,
                                                              Ec_p1dlot = item.Ec_p1dlot,
                                                              Ec_p1dnote = item.Ec_p1dnote,
                                                              p1dModifier = item.p1dModifier,
                                                              p1dModifyDate = item.p1dModifyDate,

                                                              //    //品管
                                                              Ec_qadate = item.Ec_qadate,
                                                              Ec_qalot = item.Ec_qalot,
                                                              Ec_qanote = item.Ec_qanote,
                                                              qaModifier = item.qaModifier,
                                                              qaModifyDate = item.qaModifyDate,

                                                              UDF01 = item.UDF01,
                                                              UDF02 = "",
                                                              UDF03 = "",
                                                              UDF04 = "",
                                                              UDF05 = "",
                                                              UDF06 = "",
                                                              UDF51 = 0,
                                                              UDF52 = 0,
                                                              UDF53 = 0,
                                                              UDF54 = 0,
                                                              UDF55 = 0,
                                                              UDF56 = 0,
                                                              IsDeleted = 0,

                                                              Remark = item.Remark,
                                                              Creator = item.Creator,
                                                              CreateDate = item.CreateDate,
                                                              Modifier = GetIdentityName(),
                                                              ModifyDate = DateTime.Now,
                                                          }).ToList();
            DB.BulkUpdate(New_result_P2dPurchaseList);
            //DB.BulkSaveChanges();

            #endregion 4.采购件C003

            //}
            //if (this.IsManage.SelectedValue == "0")
            //{
            //    // Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课不需要管理！", "提示信息", MessageBoxIcon.Information);

            //    #region 1.非采购件

            //    //1.非采购件
            //    var New_NonPurchase = from a in q_NotEollist
            //                          where a.Ec_procurement == "E"
            //                          select a;

            //    var result_NonPurchase = New_NonPurchase.Distinct().ToList();
            //    List<Pp_Ec_Sub> New_NonPurchaseList = (from item in result_NonPurchase
            //                                           select new Pp_Ec_Sub
            //                                           {
            //                                               GUID = item.GUID,
            //                                               Ec_no = item.Ec_no,
            //                                               Ec_model = item.Ec_model,
            //                                               Ec_bomitem = item.Ec_bomitem,
            //                                               Ec_bomsubitem = item.Ec_bomsubitem,
            //                                               Ec_olditem = item.Ec_olditem,
            //                                               Ec_oldtext = item.Ec_oldtext,
            //                                               Ec_oldqty = item.Ec_oldqty,
            //                                               Ec_oldset = item.Ec_oldset,
            //                                               Ec_newitem = item.Ec_newitem,
            //                                               Ec_newtext = item.Ec_newtext,
            //                                               Ec_newqty = item.Ec_newqty,
            //                                               Ec_newset = item.Ec_newset,
            //                                               Ec_bomno = item.Ec_bomno,
            //                                               Ec_change = item.Ec_change,
            //                                               Ec_local = item.Ec_local,
            //                                               Ec_note = item.Ec_note,
            //                                               Ec_process = item.Ec_process,
            //                                               Ec_bomdate = item.Ec_bomdate,

            //                                               Ec_procurement = item.Ec_procurement,
            //                                               Ec_location = item.Ec_location,
            //                                               IsCheck = item.IsCheck,
            //                                               IsManage = 3,
            //                                               Ec_eol = item.Ec_eol,

            //                                               Ec_entrydate = item.Ec_entrydate,

            //                                               Ec_pmcdate = item.Ec_pmcdate,
            //                                               Ec_pmclot = item.Ec_pmclot,
            //                                               Ec_pmcmemo = item.Ec_pmcmemo,
            //                                               Ec_pmcnote = item.Ec_pmcnote,
            //                                               Ec_bstock = 0,
            //                                               pmcModifier = item.pmcModifier,
            //                                               pmcModifyDate = item.pmcModifyDate,

            //                                               //    //制二
            //                                               Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
            //                                               Ec_p2dlot = "与制二无关",
            //                                               Ec_p2dnote = "与制二无关",
            //                                               p2dModifier = GetIdentityName(),
            //                                               p2dModifyDate = DateTime.Now,

            //                                               //    //部管
            //                                               Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
            //                                               Ec_mmlot = "与部管无关",
            //                                               Ec_mmlotno = "与部管无关",
            //                                               Ec_mmnote = "与部管无关",
            //                                               mmModifier = GetIdentityName(),
            //                                               mmModifyDate = DateTime.Now,

            //                                               //    //采购
            //                                               Ec_purdate = item.Ec_purdate,
            //                                               Ec_purorder = item.Ec_purorder,
            //                                               Ec_pursupplier = item.Ec_pursupplier,
            //                                               Ec_purnote = item.Ec_purnote,
            //                                               ppModifier = item.ppModifier,
            //                                               ppModifyDate = item.ppModifyDate,

            //                                               //    //受检
            //                                               Ec_iqcdate = item.Ec_iqcdate,
            //                                               Ec_iqcorder = item.Ec_iqcorder,
            //                                               Ec_iqcnote = item.Ec_iqcnote,
            //                                               iqcModifier = item.iqcModifier,
            //                                               iqcModifyDate = item.iqcModifyDate,

            //                                               //    //制一
            //                                               Ec_p1ddate = item.Ec_p1ddate,
            //                                               Ec_p1dline = item.Ec_p1dline,
            //                                               Ec_p1dlot = item.Ec_p1dlot,
            //                                               Ec_p1dnote = item.Ec_p1dnote,
            //                                               p1dModifier = item.p1dModifier,
            //                                               p1dModifyDate = item.p1dModifyDate,

            //                                               //    //品管
            //                                               Ec_qadate = item.Ec_qadate,
            //                                               Ec_qalot = item.Ec_qalot,
            //                                               Ec_qanote = item.Ec_qanote,
            //                                               qaModifier = item.qaModifier,
            //                                               qaModifyDate = item.qaModifyDate,

            //                                               UDF01 = item.UDF01,
            //                                               UDF02 = "",
            //                                               UDF03 = "",
            //                                               UDF04 = "",
            //                                               UDF05 = "",
            //                                               UDF06 = "",
            //                                               UDF51 = 0,
            //                                               UDF52 = 0,
            //                                               UDF53 = 0,
            //                                               UDF54 = 0,
            //                                               UDF55 = 0,
            //                                               UDF56 = 0,
            //                                               IsDeleted = 0,

            //                                               Remark = item.Remark,
            //                                               Creator = item.Creator,
            //                                               CreateDate = item.CreateDate,
            //                                               Modifier = GetIdentityName(),
            //                                               ModifyDate = DateTime.Now,
            //                                           }).ToList();
            //    DB.BulkUpdate(New_NonPurchaseList);
            //    //DB.BulkSaveChanges();

            //    #endregion 1.非采购件

            //    #region 2.新物料为空

            //    var NonItem = from a in q_NotEollist
            //                  where a.Ec_newitem == "0"
            //                  select a;
            //    var result_NonItem = NonItem.Distinct().ToList();
            //    List<Pp_Ec_Sub> New_NonItemList = (from item in result_NonItem
            //                                       select new Pp_Ec_Sub
            //                                       {
            //                                           GUID = item.GUID,
            //                                           Ec_no = item.Ec_no,
            //                                           Ec_model = item.Ec_model,
            //                                           Ec_bomitem = item.Ec_bomitem,
            //                                           Ec_bomsubitem = item.Ec_bomsubitem,
            //                                           Ec_olditem = item.Ec_olditem,
            //                                           Ec_oldtext = item.Ec_oldtext,
            //                                           Ec_oldqty = item.Ec_oldqty,
            //                                           Ec_oldset = item.Ec_oldset,
            //                                           Ec_newitem = item.Ec_newitem,
            //                                           Ec_newtext = item.Ec_newtext,
            //                                           Ec_newqty = item.Ec_newqty,
            //                                           Ec_newset = item.Ec_newset,
            //                                           Ec_bomno = item.Ec_bomno,
            //                                           Ec_change = item.Ec_change,
            //                                           Ec_local = item.Ec_local,
            //                                           Ec_note = item.Ec_note,
            //                                           Ec_process = item.Ec_process,
            //                                           Ec_bomdate = item.Ec_bomdate,

            //                                           Ec_procurement = item.Ec_procurement,
            //                                           Ec_location = item.Ec_location,
            //                                           IsCheck = item.IsCheck,
            //                                           IsManage = IsManages,
            //                                           Ec_eol = item.Ec_eol,

            //                                           Ec_entrydate = item.Ec_entrydate,

            //                                           Ec_pmcdate = item.Ec_pmcdate,
            //                                           Ec_pmclot = item.Ec_pmclot,
            //                                           Ec_pmcmemo = item.Ec_pmcmemo,
            //                                           Ec_pmcnote = item.Ec_pmcnote,
            //                                           Ec_bstock = 0,
            //                                           pmcModifier = item.pmcModifier,
            //                                           pmcModifyDate = item.pmcModifyDate,

            //                                           //    //制二
            //                                           Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
            //                                           Ec_p2dlot = "与制二无关",
            //                                           Ec_p2dnote = "与制二无关",
            //                                           p2dModifier = GetIdentityName(),
            //                                           p2dModifyDate = DateTime.Now,

            //                                           //    //部管
            //                                           Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
            //                                           Ec_mmlot = "与部管无关",
            //                                           Ec_mmlotno = "与部管无关",
            //                                           Ec_mmnote = "与部管无关",
            //                                           mmModifier = GetIdentityName(),
            //                                           mmModifyDate = DateTime.Now,

            //                                           //    //采购
            //                                           Ec_purdate = item.Ec_purdate,
            //                                           Ec_purorder = item.Ec_purorder,
            //                                           Ec_pursupplier = item.Ec_pursupplier,
            //                                           Ec_purnote = item.Ec_purnote,
            //                                           ppModifier = item.ppModifier,
            //                                           ppModifyDate = item.ppModifyDate,

            //                                           //    //受检
            //                                           Ec_iqcdate = item.Ec_iqcdate,
            //                                           Ec_iqcorder = item.Ec_iqcorder,
            //                                           Ec_iqcnote = item.Ec_iqcnote,
            //                                           iqcModifier = item.iqcModifier,
            //                                           iqcModifyDate = item.iqcModifyDate,

            //                                           //    //制一
            //                                           Ec_p1ddate = item.Ec_p1ddate,
            //                                           Ec_p1dline = item.Ec_p1dline,
            //                                           Ec_p1dlot = item.Ec_p1dlot,
            //                                           Ec_p1dnote = item.Ec_p1dnote,
            //                                           p1dModifier = item.p1dModifier,
            //                                           p1dModifyDate = item.p1dModifyDate,

            //                                           //    //品管
            //                                           Ec_qadate = item.Ec_qadate,
            //                                           Ec_qalot = item.Ec_qalot,
            //                                           Ec_qanote = item.Ec_qanote,
            //                                           qaModifier = item.qaModifier,
            //                                           qaModifyDate = item.qaModifyDate,

            //                                           UDF01 = item.UDF01,
            //                                           UDF02 = "",
            //                                           UDF03 = "",
            //                                           UDF04 = "",
            //                                           UDF05 = "",
            //                                           UDF06 = "",
            //                                           UDF51 = 0,
            //                                           UDF52 = 0,
            //                                           UDF53 = 0,
            //                                           UDF54 = 0,
            //                                           UDF55 = 0,
            //                                           UDF56 = 0,
            //                                           IsDeleted = 0,

            //                                           Remark = item.Remark,
            //                                           Creator = item.Creator,
            //                                           CreateDate = item.CreateDate,
            //                                           Modifier = GetIdentityName(),
            //                                           ModifyDate = DateTime.Now,
            //                                       }).ToList();
            //    DB.BulkUpdate(New_NonItemList);
            //    //DB.BulkSaveChanges();

            //    #endregion 2.新物料为空

            //    #region 3.采购件非C003

            //    //1.采购件非C003

            //    var MMPurchase = from a in q_NotEollist

            //                     where a.Ec_procurement == "F"
            //                     where a.Ec_location != "C003"
            //                     select a;

            //    var result_MMPurchase = MMPurchase.Distinct().ToList();
            //    List<Pp_Ec_Sub> New_MMPurchaseList = (from item in result_MMPurchase
            //                                          select new Pp_Ec_Sub
            //                                          {
            //                                              GUID = item.GUID,
            //                                              Ec_no = item.Ec_no,
            //                                              Ec_model = item.Ec_model,
            //                                              Ec_bomitem = item.Ec_bomitem,
            //                                              Ec_bomsubitem = item.Ec_bomsubitem,
            //                                              Ec_olditem = item.Ec_olditem,
            //                                              Ec_oldtext = item.Ec_oldtext,
            //                                              Ec_oldqty = item.Ec_oldqty,
            //                                              Ec_oldset = item.Ec_oldset,
            //                                              Ec_newitem = item.Ec_newitem,
            //                                              Ec_newtext = item.Ec_newtext,
            //                                              Ec_newqty = item.Ec_newqty,
            //                                              Ec_newset = item.Ec_newset,
            //                                              Ec_bomno = item.Ec_bomno,
            //                                              Ec_change = item.Ec_change,
            //                                              Ec_local = item.Ec_local,
            //                                              Ec_note = item.Ec_note,
            //                                              Ec_process = item.Ec_process,
            //                                              Ec_bomdate = item.Ec_bomdate,

            //                                              Ec_procurement = item.Ec_procurement,
            //                                              Ec_location = item.Ec_location,
            //                                              IsCheck = item.IsCheck,
            //                                              IsManage = 2,
            //                                              Ec_eol = item.Ec_eol,

            //                                              Ec_entrydate = item.Ec_entrydate,

            //                                              Ec_pmcdate = item.Ec_pmcdate,
            //                                              Ec_pmclot = item.Ec_pmclot,
            //                                              Ec_pmcmemo = item.Ec_pmcmemo,
            //                                              Ec_pmcnote = item.Ec_pmcnote,
            //                                              Ec_bstock = 0,
            //                                              pmcModifier = item.pmcModifier,
            //                                              pmcModifyDate = item.pmcModifyDate,

            //                                              //    //制二
            //                                              Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
            //                                              Ec_p2dlot = "与制二无关",
            //                                              Ec_p2dnote = "与制二无关",
            //                                              p2dModifier = GetIdentityName(),
            //                                              p2dModifyDate = DateTime.Now,

            //                                              //    //部管
            //                                              Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
            //                                              Ec_mmlot = "与部管无关",
            //                                              Ec_mmlotno = "与部管无关",
            //                                              Ec_mmnote = "与部管无关",
            //                                              mmModifier = GetIdentityName(),
            //                                              mmModifyDate = DateTime.Now,

            //                                              //    //采购
            //                                              Ec_purdate = item.Ec_purdate,
            //                                              Ec_purorder = item.Ec_purorder,
            //                                              Ec_pursupplier = item.Ec_pursupplier,
            //                                              Ec_purnote = item.Ec_purnote,
            //                                              ppModifier = item.ppModifier,
            //                                              ppModifyDate = item.ppModifyDate,

            //                                              //    //受检
            //                                              Ec_iqcdate = item.Ec_iqcdate,
            //                                              Ec_iqcorder = item.Ec_iqcorder,
            //                                              Ec_iqcnote = item.Ec_iqcnote,
            //                                              iqcModifier = item.iqcModifier,
            //                                              iqcModifyDate = item.iqcModifyDate,

            //                                              //    //制一
            //                                              Ec_p1ddate = item.Ec_p1ddate,
            //                                              Ec_p1dline = item.Ec_p1dline,
            //                                              Ec_p1dlot = item.Ec_p1dlot,
            //                                              Ec_p1dnote = item.Ec_p1dnote,
            //                                              p1dModifier = item.p1dModifier,
            //                                              p1dModifyDate = item.p1dModifyDate,

            //                                              //    //品管
            //                                              Ec_qadate = item.Ec_qadate,
            //                                              Ec_qalot = item.Ec_qalot,
            //                                              Ec_qanote = item.Ec_qanote,
            //                                              qaModifier = item.qaModifier,
            //                                              qaModifyDate = item.qaModifyDate,

            //                                              UDF01 = item.UDF01,
            //                                              UDF02 = "",
            //                                              UDF03 = "",
            //                                              UDF04 = "",
            //                                              UDF05 = "",
            //                                              UDF06 = "",
            //                                              UDF51 = 0,
            //                                              UDF52 = 0,
            //                                              UDF53 = 0,
            //                                              UDF54 = 0,
            //                                              UDF55 = 0,
            //                                              UDF56 = 0,
            //                                              IsDeleted = 0,

            //                                              Remark = item.Remark,
            //                                              Creator = item.Creator,
            //                                              CreateDate = item.CreateDate,
            //                                              Modifier = GetIdentityName(),
            //                                              ModifyDate = DateTime.Now,
            //                                          }).ToList();

            //    DB.BulkUpdate(New_MMPurchaseList);
            //    //DB.BulkSaveChanges();

            //    #endregion 3.采购件非C003

            //    #region 4.采购件C003

            //    //1.采购件C003

            //    var P2dPurchase = from a in q_NotEollist

            //                      where a.Ec_procurement == "F"
            //                      where a.Ec_location == "C003"
            //                      select a;

            //    var result_P2dPurchase = P2dPurchase.Distinct().ToList();
            //    List<Pp_Ec_Sub> New_result_P2dPurchaseList = (from item in result_P2dPurchase
            //                                                  select new Pp_Ec_Sub
            //                                                  {
            //                                                      GUID = item.GUID,
            //                                                      Ec_no = item.Ec_no,
            //                                                      Ec_model = item.Ec_model,
            //                                                      Ec_bomitem = item.Ec_bomitem,
            //                                                      Ec_bomsubitem = item.Ec_bomsubitem,
            //                                                      Ec_olditem = item.Ec_olditem,
            //                                                      Ec_oldtext = item.Ec_oldtext,
            //                                                      Ec_oldqty = item.Ec_oldqty,
            //                                                      Ec_oldset = item.Ec_oldset,
            //                                                      Ec_newitem = item.Ec_newitem,
            //                                                      Ec_newtext = item.Ec_newtext,
            //                                                      Ec_newqty = item.Ec_newqty,
            //                                                      Ec_newset = item.Ec_newset,
            //                                                      Ec_bomno = item.Ec_bomno,
            //                                                      Ec_change = item.Ec_change,
            //                                                      Ec_local = item.Ec_local,
            //                                                      Ec_note = item.Ec_note,
            //                                                      Ec_process = item.Ec_process,
            //                                                      Ec_bomdate = item.Ec_bomdate,

            //                                                      Ec_procurement = item.Ec_procurement,
            //                                                      Ec_location = item.Ec_location,
            //                                                      IsCheck = item.IsCheck,
            //                                                      IsManage = IsManages,
            //                                                      Ec_eol = item.Ec_eol,

            //                                                      Ec_entrydate = item.Ec_entrydate,

            //                                                      Ec_pmcdate = item.Ec_pmcdate,
            //                                                      Ec_pmclot = item.Ec_pmclot,
            //                                                      Ec_pmcmemo = item.Ec_pmcmemo,
            //                                                      Ec_pmcnote = item.Ec_pmcnote,
            //                                                      Ec_bstock = 0,
            //                                                      pmcModifier = item.pmcModifier,
            //                                                      pmcModifyDate = item.pmcModifyDate,

            //                                                      //    //制二
            //                                                      Ec_p2ddate = DateTime.Now.ToString("yyyyMMdd"),
            //                                                      Ec_p2dlot = "与制二无关",
            //                                                      Ec_p2dnote = "与制二无关",
            //                                                      p2dModifier = GetIdentityName(),
            //                                                      p2dModifyDate = DateTime.Now,

            //                                                      //    //部管
            //                                                      Ec_mmdate = DateTime.Now.ToString("yyyyMMdd"),
            //                                                      Ec_mmlot = "与部管无关",
            //                                                      Ec_mmlotno = "与部管无关",
            //                                                      Ec_mmnote = "与部管无关",
            //                                                      mmModifier = GetIdentityName(),
            //                                                      mmModifyDate = DateTime.Now,

            //                                                      //    //采购
            //                                                      Ec_purdate = item.Ec_purdate,
            //                                                      Ec_purorder = item.Ec_purorder,
            //                                                      Ec_pursupplier = item.Ec_pursupplier,
            //                                                      Ec_purnote = item.Ec_purnote,
            //                                                      ppModifier = item.ppModifier,
            //                                                      ppModifyDate = item.ppModifyDate,

            //                                                      //    //受检
            //                                                      Ec_iqcdate = item.Ec_iqcdate,
            //                                                      Ec_iqcorder = item.Ec_iqcorder,
            //                                                      Ec_iqcnote = item.Ec_iqcnote,
            //                                                      iqcModifier = item.iqcModifier,
            //                                                      iqcModifyDate = item.iqcModifyDate,

            //                                                      //    //制一
            //                                                      Ec_p1ddate = item.Ec_p1ddate,
            //                                                      Ec_p1dline = item.Ec_p1dline,
            //                                                      Ec_p1dlot = item.Ec_p1dlot,
            //                                                      Ec_p1dnote = item.Ec_p1dnote,
            //                                                      p1dModifier = item.p1dModifier,
            //                                                      p1dModifyDate = item.p1dModifyDate,

            //                                                      //    //品管
            //                                                      Ec_qadate = item.Ec_qadate,
            //                                                      Ec_qalot = item.Ec_qalot,
            //                                                      Ec_qanote = item.Ec_qanote,
            //                                                      qaModifier = item.qaModifier,
            //                                                      qaModifyDate = item.qaModifyDate,

            //                                                      UDF01 = item.UDF01,
            //                                                      UDF02 = "",
            //                                                      UDF03 = "",
            //                                                      UDF04 = "",
            //                                                      UDF05 = "",
            //                                                      UDF06 = "",
            //                                                      UDF51 = 0,
            //                                                      UDF52 = 0,
            //                                                      UDF53 = 0,
            //                                                      UDF54 = 0,
            //                                                      UDF55 = 0,
            //                                                      UDF56 = 0,
            //                                                      IsDeleted = 0,

            //                                                      Remark = item.Remark,
            //                                                      Creator = item.Creator,
            //                                                      CreateDate = item.CreateDate,
            //                                                      Modifier = GetIdentityName(),
            //                                                      ModifyDate = DateTime.Now,
            //                                                  }).ToList();
            //    DB.BulkUpdate(New_result_P2dPurchaseList);
            //    //DB.BulkSaveChanges();

            //    #endregion 4.采购件C003
            //}

            #endregion 1.更新EC单身
        }

        private void BindDdlItemlist()//物料信息
        {
            #region 停产机种不导入

            var q_NotEollist = from a in DB.Pp_SapEcnSubs
                               where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                                                                  where d.D_SAP_ZCA1D_Z034 == ""
                                                                  select d.D_SAP_ZCA1D_Z002)
                                                               .Contains(a.D_SAP_ZPABD_S002)
                               where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                                                                  select d.D_SAP_DEST_Z001)
                                                                .Contains(a.D_SAP_ZPABD_S002)
                               where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                               select a;

            var q = from a in q_NotEollist
                    join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                    join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                    //where b.D_SAP_ZCA1D_Z010 == "E"
                    select new
                    {
                        Ec_no = a.D_SAP_ZPABD_S001,
                        Ec_model = c.D_SAP_DEST_Z002,
                        Ec_bomitem = a.D_SAP_ZPABD_S002,
                        Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                        Ec_olditem = a.D_SAP_ZPABD_S004,
                        Ec_oldtext = a.D_SAP_ZPABD_S005,
                        Ec_oldqty = a.D_SAP_ZPABD_S006,
                        Ec_oldset = a.D_SAP_ZPABD_S007,
                        Ec_newitem = a.D_SAP_ZPABD_S008,
                        Ec_newtext = a.D_SAP_ZPABD_S009,
                        Ec_newqty = a.D_SAP_ZPABD_S010,
                        Ec_newset = a.D_SAP_ZPABD_S011,
                        Ec_bomno = a.D_SAP_ZPABD_S012,
                        Ec_change = a.D_SAP_ZPABD_S013,
                        Ec_local = a.D_SAP_ZPABD_S014,
                        Ec_note = a.D_SAP_ZPABD_S015,
                        Ec_process = a.D_SAP_ZPABD_S016,
                        Ec_procurement = b.D_SAP_ZCA1D_Z010,
                        Ec_location = b.D_SAP_ZCA1D_Z030,
                        IsCheck = b.D_SAP_ZCA1D_Z019,
                    };
            var NonItem = from a in q_NotEollist
                          join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
                          join c in DB.Pp_SapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                          where a.D_SAP_ZPABD_S008 == "0"
                          //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                          //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                          //                                   where d.D_SAP_ZCA1D_Z034 == ""
                          //                                   select d.D_SAP_ZCA1D_Z002)
                          //                                .Contains(a.D_SAP_ZPABD_S002)
                          //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                          //                                   select d.D_SAP_DEST_Z001)
                          //                                 .Contains(a.D_SAP_ZPABD_S002)
                          //where a.D_SAP_ZPABD_S008 != "0" && (from d in DB.Pp_SapMaterials
                          //                                    where d.D_SAP_ZCA1D_Z010 == "E"
                          //                                    select d.D_SAP_ZCA1D_Z002)
                          //                                .Contains(a.D_SAP_ZPABD_S008)
                          where a.D_SAP_ZPABD_S001.Contains(Ec_no.Text)
                          //where b.Ec_no == strecn
                          //where a.Prodate == sdate//投入日期
                          select new
                          {
                              Ec_no = a.D_SAP_ZPABD_S001,
                              Ec_model = c.D_SAP_DEST_Z002,
                              Ec_bomitem = a.D_SAP_ZPABD_S002,
                              Ec_bomsubitem = a.D_SAP_ZPABD_S003,
                              Ec_olditem = a.D_SAP_ZPABD_S004,
                              Ec_oldtext = a.D_SAP_ZPABD_S005,
                              Ec_oldqty = a.D_SAP_ZPABD_S006,
                              Ec_oldset = a.D_SAP_ZPABD_S007,
                              Ec_newitem = a.D_SAP_ZPABD_S008,
                              Ec_newtext = a.D_SAP_ZPABD_S009,
                              Ec_newqty = a.D_SAP_ZPABD_S010,
                              Ec_newset = a.D_SAP_ZPABD_S011,
                              Ec_bomno = a.D_SAP_ZPABD_S012,
                              Ec_change = a.D_SAP_ZPABD_S013,
                              Ec_local = a.D_SAP_ZPABD_S014,
                              Ec_note = a.D_SAP_ZPABD_S015,
                              Ec_process = a.D_SAP_ZPABD_S016,
                              Ec_procurement = "E",
                              Ec_location = "C004",
                              IsCheck = "",
                          };

            var q_All = q.Union(NonItem);

            var q_item = from a in q_All
                         select new

                         {
                             a.Ec_bomitem
                         };
            var qs = q_item.Select(E =>
                new
                {
                    E.Ec_bomitem,
                }).Distinct();

            #endregion 停产机种不导入

            // 绑定到下拉列表（启用模拟树功能）

            DDL_Item.DataTextField = "Ec_bomitem";
            DDL_Item.DataValueField = "Ec_bomitem";
            DDL_Item.DataSource = qs;
            DDL_Item.DataBind();

            // 选中根节点
            this.DDL_Item.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        private void BindGrid()//管理确认
        {
            #region 停产机种不导入

            var q = from a in DB.Pp_Ec_Subs
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                                   where d.D_SAP_ZCA1D_Z034 == ""
                        //                                   select d.D_SAP_ZCA1D_Z002)
                        //                                .Contains(a.D_SAP_ZPABD_S002)
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapModelDests
                        //                                   select d.D_SAP_DEST_Z001)
                        //                                 .Contains(a.D_SAP_ZPABD_S002)
                    where a.Ec_no.Contains(Ec_no.Text)
                    select a;

            //var q_All = q.Union(NonItem);

            if (DDL_Item.SelectedIndex != -1 && DDL_Item.SelectedIndex != 0)
            {
                q = q.Where(u => u.Ec_bomitem.Contains(this.DDL_Item.SelectedItem.Text));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q);
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
                Grid1.DataBind();
            }

            #endregion 停产机种不导入
        }

        private void DeleteEcSubs()
        {
            // 获取要更新的订单
            var DelSubs = DB.Pp_Ec_Subs
                .Where(it => it.Ec_no.Contains(Ec_no.Text))
                .ToList();

            // 更新数量
            //foreach (var order in subs)
            //{
            //    order.IsDeleted = 1;
            //}
            DB.Pp_Ec_Subs.RemoveRange(DelSubs); // 批量删除
            // 保存更改
            DB.SaveChanges();

            DelInsNetOperateNotes();
        }

        //protected void IsSopUpdate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ShowNotify("列表三选中项的值：" + rblAutoPostBack.SelectedValue);

        //    if (IsSopUpdate.SelectedValue == "1")
        //    {
        //        Alert.ShowInTop("设变No:" + Ec_no.Text + "，SOP需要更新！", "提示信息", MessageBoxIcon.Information);
        //    }
        //    else

        //    {
        //        Alert.ShowInTop("设变No:" + Ec_no.Text + "，SOP不需要更新！", "提示信息", MessageBoxIcon.Information);
        //    }
        //}

        //protected void IsManage_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //ShowNotify("列表三选中项的值：" + rblAutoPostBack.SelectedValue);

        //    if (IsManage.SelectedValue == "1")
        //    {
        //        Alert.ShowInTop("设变No:" + Ec_no.Text + "，将设定成制二课需要管理！", "提示信息", MessageBoxIcon.Information);
        //    }
        //    else

        //    {
        //        Alert.ShowInTop("设变No:" + Ec_no.Text + "，将设定成制二课不需要管理！", "提示信息", MessageBoxIcon.Information);
        //    }
        //}

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ec_documents.FileName))
                {
                    //long fileSize = Ec_documents.PostedFile.ContentLength;//> 5242880;

                    //设变文件大小
                    if (Ec_documents.PostedFile.ContentLength > iFileSizeLimit)
                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_UploadFile, MessageBoxIcon.Error);
                        return;
                    }
                }
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
                if (Ec_leader.SelectedIndex == -1 || Ec_leader.SelectedIndex == -1)
                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Reselect, MessageBoxIcon.Information);
                    return;
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
                //if (this.IsManage.SelectedValue == "1")
                //{
                //    Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课需要管理！", "提示信息", MessageBoxIcon.Information);
                //}
                //if (this.IsManage.SelectedValue == "0")
                //{
                //    Alert.ShowInTop("设变:<" + Ec_no.Text + ">制二课不需要管理！", "提示信息", MessageBoxIcon.Information);
                //}
                SaveItem();
                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                SaveItemSub();
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
            string strDist = "";
            if (oldEc_distinction != Ec_distinction.SelectedValue)
            {
                strDist = "管理区分从" + oldEc_distinction + "改成" + Ec_distinction.SelectedValue;
            }
            else
            {
                strDist = "管理区分" + oldEc_distinction;
            }

            //修改日志
            string Newtext = Ec_issuedate.Text + "," + Ec_no.Text + "," + Ec_leader.SelectedItem.Text + "," + txtPbookdoc + "," + txtPpbookdoc + "," + "," + txtPjpbookdoc + "," + txtPdoc + ",管理区分:" + Ec_distinction.SelectedItem.Text + "," + strDist + ",物料管理：1,SOP确认：1"; ;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit技术* " + Newtext + " *Edit技术 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);
        }

        private void DelInsNetOperateNotes()
        {
            string strDist = "";
            if (oldEc_distinction != Ec_distinction.SelectedValue)
            {
                strDist = "管理区分从" + oldEc_distinction + "改成" + Ec_distinction.SelectedValue;
            }
            else
            {
                strDist = "管理区分" + oldEc_distinction;
            }

            //修改日志
            string Newtext = Ec_no.Text + ",管理区分:" + Ec_distinction.SelectedItem.Text + "," + strDist + ",";
            string OperateType = "删除";//操作标记
            string OperateNotes = "Del技术* " + Newtext + " *Del技术 的记录已删除";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变删除", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}