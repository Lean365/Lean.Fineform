using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;using System.Data.Entity.Validation;
using FineUIPro;

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;


namespace Lean.Fineform.Lf_Manufacturing.EC
{
    public partial class ec_qc_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcIQCEdit";
            }
        }

        #endregion

        #region Page_Load
        public static string strMailto, mysql, guid, issuedate, strID, strEc_no, strEc_newitem;
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

            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');
            //ID,Ec_no,Ec_model,Ec_bomitem,Ec_olditem,Ec_newitem
            strID = strgroup[0].ToString().Trim();
            strEc_no = strgroup[1].ToString().Trim();
            strEc_newitem = strgroup[2].ToString().Trim();

            BindData();

            //InitNewItem();
            //InitOldItem();
            //InitModel();
            Ec_iqcdate.Focus();
        }



        //#region InitOldItem

        //private void InitOldItem()
        //{
        //    // 打开编辑角色的窗口
        //    string selectJobTitleURL = String.Format("../plutoProinfo/itEm_select.aspx?ids=<script>{0}</script>", hfSelectedDhbn.GetValueReference());
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
        //    string selectJobTitleURL = String.Format("../plutoProinfo/itEm_select.aspx?ids=<script>{0}</script>", hfSelectedWhbn.GetValueReference());
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



        #endregion

        #region Events

        private void BindData()
        {
            try
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        //where a.Ec_model == strEc_model
                        //where a.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        where b.Ec_newitem == strEc_newitem

                        select new
                        {
                            b.Ec_iqcdate,
                            
                            b.Ec_iqcnote,
                            b.Ec_iqcorder,
                            b.Ec_pmcnote,
                            b.Ec_no,
                            a.Ec_issuedate,
                            a.Ec_status,
                            a.Ec_details,
                            a.Ec_leader,
                            a.Remark,
                            b.Ec_model,
                            b.Ec_bomitem,
                            b.Ec_olditem,
                            b.Ec_newitem,
                            b.Ec_bstock,
                            b.Ec_purorder
                            //c.D_SAP_ZCA1D_Z033,

                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.Ec_iqcdate,
                        
                        E.Ec_iqcnote,
                        E.Ec_iqcorder,
                        E.Ec_pmcnote,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_status,
                        E.Ec_details,
                        E.Ec_leader,
                        E.Remark,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_olditem,
                        E.Ec_newitem,
                        E.Ec_bstock,
                        E.Ec_purorder,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    string ss = qs[0].Ec_iqcdate;
                    if (!string.IsNullOrEmpty(ss))
                    {

                        Ec_iqcdate.SelectedDate = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Ec_iqcdate.SelectedDate = DateTime.Now;
                    }
                    //Ec_iqclot.Text = qs[0].Ec_iqclot;
                    if (string.IsNullOrEmpty(qs[0].Ec_iqcorder))
                    {
                        Ec_iqcorder.Text = "4300000000";
                    }
                    else
                    {
                        Ec_iqcorder.Text = qs[0].Ec_iqcorder;
                    }
                    //Ec_iqcnote.Text = qs[0].Ec_iqcnote;

                    Ec_no.Text = qs[0].Ec_no;//设变号码
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期
                    Ec_detailstent.Text = qs[0].Ec_details;//设变内容
                    Ec_leader.Text = qs[0].Ec_leader;//担当

                    Ec_model.Text = qs[0].Ec_model;//设变机种
                    Ec_bomitem.Text = qs[0].Ec_bomitem;//成品


                    Ec_olditem.Text = qs[0].Ec_olditem;//旧物料
                    Ec_newitem.Text = qs[0].Ec_newitem;//新物料

                    Ec_bstock.Text = qs[0].Ec_bstock.ToString();//旧品在库
                    Ec_pursupplier.Text = qs[0].Ec_purorder;//采购PO

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
        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                Alert.ShowInTop("此物料免检！");
                // 非AJAX回发
                Irrelevant();

                InsNetOperateNotes();
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else if (e.EventArgument == "Confirm_Cancel")
            {
                // AJAX回发
                Alert.ShowInTop("将返回编辑页面！");
            }
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
        {

            var q =( from a in DB.Pp_EcSubs
                        //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                        //join b in DB.Pp_EcSubs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                        //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                                   select d.D_SAP_ZCA1D_Z002)
                        //                                .Contains(a.D_SAP_ZPABD_S002)
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                        //                                   select d.D_SAP_DEST_Z001)
                        //                                 .Contains(a.D_SAP_ZPABD_S002)
                    where a.Ec_no.Contains(strEc_no)
                    //where a.Ec_model.Contains(strEc_model)
                    //where a.Ec_bomitem.Contains(strEc_bomitem)
                    //where a.Ec_olditem.Contains(strEc_olditem)
                    where a.Ec_newitem.Contains(strEc_newitem)
                    //where b.Ec_no == strecn
                    //where a.Prodate == sdate//投入日期
                    select a).ToList();
            List<Pp_EcSub> UpdateList = (from item in q
                                          select new Pp_EcSub
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
                                              isCheck = item.isCheck,
                                              isConfirm = item.isConfirm,
                                              Ec_bomdate = item.Ec_bomdate,
                                              Ec_entrydate = item.Ec_entrydate,
                                              Ec_pmcdate = item.Ec_pmcdate,
                                              Ec_pmclot = item.Ec_pmclot,
                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                              Ec_pmcnote = item.Ec_pmcnote,
                                              Ec_bstock = item.Ec_bstock,
                                              pmcModifier = item.pmcModifier,
                                              pmcModifyTime = item.pmcModifyTime,

                                              Ec_p2ddate = item.Ec_p2ddate,
                                              Ec_p2dlot = item.Ec_p2dlot,
                                              Ec_p2dnote = item.Ec_p2dnote,
                                              p2dModifier = item.p2dModifier,
                                              p2dModifyTime = item.p2dModifyTime,

                                              Ec_mmdate = item.Ec_mmdate,//投入日期
                                              Ec_mmlot = item.Ec_mmlot,
                                              Ec_mmlotno = item.Ec_mmlotno,
                                              Ec_mmnote = item.Ec_mmnote,
                                              mmModifier = item.mmModifier,
                                              mmModifyTime = item.mmModifyTime,

                                              Ec_purdate = item.Ec_purdate,
                                              Ec_purorder = item.Ec_purorder,
                                              Ec_pursupplier = item.Ec_pursupplier,
                                              Ec_purnote = item.Ec_purnote,
                                              ppModifier = item.ppModifier,
                                              ppModifyTime = item.ppModifyTime,

                                              Ec_iqcdate = Ec_iqcdate.SelectedDate.Value.ToString("yyyyMMdd"),
                                              Ec_iqcorder = Ec_iqcorder.Text.ToUpper(),
                                              Ec_iqcnote = "检查确认OK",
                                              iqcModifier =GetIdentityName(),
                                              iqcModifyTime = DateTime.Now,

                                              Ec_p1ddate = item.Ec_p1ddate,
                                              Ec_p1dline = item.Ec_p1dline,
                                              Ec_p1dlot = item.Ec_p1dlot,
                                              Ec_p1dnote = item.Ec_p1dnote,
                                              p1dModifier = item.p1dModifier,
                                              p1dModifyTime = item.p1dModifyTime,

                                              Ec_qadate =item.Ec_qadate,
                                              Ec_qalot = item.Ec_qalot,
                                              Ec_qanote =item.Ec_qanote,
                                              qaModifier = item.qaModifier,
                                              qaModifyTime = item.qaModifyTime,

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
                                              isDelete = item.isDelete,
                                              Remark = item.Remark,
                                              Creator = item.Creator,
                                              CreateTime = item.CreateTime,
                                              Modifier = item.Modifier,
                                              ModifyTime = item.ModifyTime,
                                          }).ToList();
            DB.BulkUpdate(UpdateList);
            DB.BulkSaveChanges();

        }
        private void Irrelevant()//新增生产日报
        {

            var q = (from a in DB.Pp_EcSubs
                        //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                        //join b in DB.Pp_EcSubs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                        //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                        //                                   select d.D_SAP_ZCA1D_Z002)
                        //                                .Contains(a.D_SAP_ZPABD_S002)
                        //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                        //                                   select d.D_SAP_DEST_Z001)
                        //                                 .Contains(a.D_SAP_ZPABD_S002)
                    where a.Ec_no.Contains(strEc_no)
                    //where a.Ec_model.Contains(strEc_model)
                    //where a.Ec_bomitem.Contains(strEc_bomitem)
                    //where a.Ec_olditem.Contains(strEc_olditem)
                    where a.Ec_newitem.Contains(strEc_newitem)
                    //where b.Ec_no == strecn
                    //where a.Prodate == sdate//投入日期
                    select a).ToList();
            List<Pp_EcSub> UpdateList = (from item in q
                                          select new Pp_EcSub
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
                                              Ec_entrydate = item.Ec_entrydate,
                                              Ec_pmcdate = item.Ec_pmcdate,
                                              Ec_pmclot = item.Ec_pmclot,
                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                              Ec_pmcnote = item.Ec_pmcnote,
                                              Ec_bstock = item.Ec_bstock,
                                              pmcModifier = item.pmcModifier,
                                              pmcModifyTime = item.pmcModifyTime,

                                              Ec_p2ddate = item.Ec_p2ddate,
                                              Ec_p2dlot = item.Ec_p2dlot,
                                              Ec_p2dnote = item.Ec_p2dnote,
                                              p2dModifier = item.p2dModifier,
                                              p2dModifyTime = item.p2dModifyTime,

                                              Ec_mmdate = item.Ec_mmdate,//投入日期
                                              Ec_mmlot = item.Ec_mmlot,
                                              Ec_mmlotno = item.Ec_mmlotno,
                                              Ec_mmnote = item.Ec_mmnote,
                                              mmModifier = item.mmModifier,
                                              mmModifyTime = item.mmModifyTime,

                                              Ec_purdate = item.Ec_purdate,
                                              Ec_purorder = item.Ec_purorder,
                                              Ec_pursupplier = item.Ec_pursupplier,
                                              Ec_purnote = item.Ec_purnote,
                                              ppModifier = item.ppModifier,
                                              ppModifyTime = item.ppModifyTime,

                                              Ec_iqcdate = Ec_iqcdate.SelectedDate.Value.ToString("yyyyMMdd"),
                                              Ec_iqcorder = "4300000000",
                                              Ec_iqcnote = "免检",
                                              iqcModifier = GetIdentityName(),
                                              iqcModifyTime = DateTime.Now,

                                              Ec_p1ddate = item.Ec_p1ddate,
                                              Ec_p1dline = item.Ec_p1dline,
                                              Ec_p1dlot = item.Ec_p1dlot,
                                              Ec_p1dnote = item.Ec_p1dnote,
                                              p1dModifier = item.p1dModifier,
                                              p1dModifyTime = item.p1dModifyTime,

                                              Ec_qadate = item.Ec_qadate,
                                              Ec_qalot = item.Ec_qalot,
                                              Ec_qanote = item.Ec_qanote,
                                              qaModifier = item.qaModifier,
                                              qaModifyTime = item.qaModifyTime,

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
                                              isDelete = item.isDelete,
                                              Remark = item.Remark,

                                              Creator = item.Creator,
                                              CreateTime = item.CreateTime,
                                              Modifier = item.Modifier,
                                              ModifyTime = item.ModifyTime,
                                          }).ToList();
            DB.BulkUpdate(UpdateList);
            DB.BulkSaveChanges();
            InsNetOperateNotes();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            //if (issuedate != "")
            //{
            //    Alert.ShowInTop("设变已实施，不能修改！" + Ec_no.Text);
            //    return;
            //}
            //string inputUserName = tbxName.Text.Trim();

            //User user = DB.Adm_Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            //if (user != null)
            //{
            //    Alert.ShowInTop("用户 " + inputUserName + " 已经存在！");
            //    return;
            //}
            //string str = this.Ec_model.Text.ToString().Replace("//", "/").ToUpper().Replace(" ", "").Replace("-", "");

            //string[] sArray = str.Split('/');


            //foreach (string i in sArray)
            //{
            //    string ecndate = Ec_issuedate.Text.Trim();
            //    string ecnno = Ec_no.Text.Trim();
            //    string ecnmodel = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();
            //    Ec_ user = DB.Pp_EcSubs.Where(u => u.Ec_issuedate + u.Ec_no + u.Ec_model == ecndate + ecnno + ecnmodel).FirstOrDefault();


            //    if (user != null)
            //    {
            //        Alert.ShowInTop("设变 " + ecndate + ecnno + ecnmodel + " 已经存在！");
            //        return;
            //    }
            //}
            try
            {
                //4300741139
                if (!String.IsNullOrEmpty(Ec_iqcorder.Text.ToString().ToUpper().Trim()))
                {
                    //验证字符长度
                    if (!ValidatorTools.IsStringLength(Ec_iqcorder.Text.ToString().ToUpper().Trim(), 10, 10))
                    {
                        Ec_iqcorder.Text = "";
                        Ec_iqcorder.Focus();
                        Alert.ShowInTop("请输入10位的以4300开头的采购PO。" + Ec_iqcorder.Text.ToString().ToUpper().Trim());
                        return;
                    }

                    //验证输入字串
                    if (!ValidatorTools.isPO(Ec_iqcorder.Text.ToString().ToUpper().Trim()))
                    {
                        Ec_iqcorder.Text = "";
                        Ec_iqcorder.Focus();
                        Alert.ShowInTop("请输入10位以4300开头的采购PO。" + Ec_iqcorder.Text.ToString().ToUpper().Trim());
                        return;
                    }
                }

                //4300741139
                if (!String.IsNullOrEmpty(Ec_iqcorder.Text.ToString().ToUpper().Trim()))
                {
                    //验证字符长度
                    if (!ValidatorTools.IsStringLength(Ec_iqcorder.Text.ToString().ToUpper().Trim(), 10, 10))
                    {
                        Ec_iqcorder.Text = "";
                        Ec_iqcorder.Focus();
                        Alert.ShowInTop("请10位以4300开头的采购PO。" + Ec_iqcorder.Text.ToString().ToUpper().Trim());
                        return;
                    }

                    //验证输入字串
                    if (!ValidatorTools.isPO(Ec_iqcorder.Text.ToString().ToUpper().Trim()))
                    {
                        Ec_iqcorder.Text = "";
                        Ec_iqcorder.Focus();
                        Alert.ShowInTop("请10位以4300开头的采购PO。" + Ec_iqcorder.Text.ToString().ToUpper().Trim());
                        return;
                    }
                }
                SaveItem();
                InsNetOperateNotes();
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
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        protected void btnIrrelevant_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！点击确定此物料免检状态。",
            String.Empty,
            MessageBoxIcon.Question,
            PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
            PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }
        #endregion
        private void Mailto()
        {
            var q = from a in DB.Adm_Users
                    where a.Dept.ID == 14 || a.Dept.ID == 19
                    where a.Email != "123@teac.com.cn"
                    select a;
            if (q.Any())
            {
                var qs = q.ToList();
                for (int i = 0; i < q.Count(); i++)
                {
                    strMailto += qs[i].Email.ToString() + ",";
                }
            }
            strMailto = strMailto.Remove(strMailto.LastIndexOf(","));
            string mailTitle = "设变发行：" + strEc_no + "物料：" + strEc_newitem;
            string mailBody = "Dear All,\r\n" + "\r\n" + "此设变受检部门已处理。\r\n" + "请贵部门担当者及时处理为盼。\r\n" + "\r\n" + "よろしくお願いいたします。\r\n" + "\r\n" + "\r\n" + "「" + GetIdentityName() + "\r\n" + DateTime.Now.ToString() + "」\r\n" + "このメッセージはWebSiteから自動で送信されている。\r\n\n";  //发送邮件的正文
            MailHelper.SendEmail(strMailto, mailTitle, mailBody);
            strMailto = "";
        }
        #region NetOperateNotes
        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();


            //新增日志
            string Newtext = Ec_issuedate.Text + "," + Ec_no.Text + "," + Ec_iqcdate.Text + "," + Ec_iqcorder.Text + "," + Ec_bomitem.Text + "," + Ec_olditem.Text + "," + Ec_newitem.Text;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit受检* " + Newtext + " *Edit受检 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);

        }
        #endregion


    }
}