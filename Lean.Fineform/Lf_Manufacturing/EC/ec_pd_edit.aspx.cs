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
    public partial class ec_pd_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcPDEdit";
            }
        }

        #endregion

        #region Page_Load
        public static Decimal strinv;
        public static string strMailto, strID, strEc_no, strEc_newitem, strdist, strpurchaser;
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

            //strEc_olditem = strgroup[4].ToString().Trim();
            strEc_newitem = strgroup[5].ToString().Trim();
            strdist = strgroup[6].ToString().Trim();


            BindData();
            BindDataBala();

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
            BindDataStockQty();
            BindDataPurchaser();
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
                        
                        //where a.Ec_purdate == ""
                        select new
                        {
                            b.Ec_purdate,
                            b.Ec_pursupplier,
                            b.Ec_purorder,
                            b.Ec_purnote,
                            b.Ec_pmcmemo,
                            a.Ec_no,
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
                            b.Ec_pmclot,
                            
                            //c.D_SAP_ZCA1D_Z033,

                        };
                //bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.Ec_purdate,
                        E.Ec_pursupplier,
                        E.Ec_purorder,
                        E.Ec_purnote,
                        E.Ec_pmcmemo,
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
                        E.Ec_pmclot,
                       
                        //E.D_SAP_ZCA1D_Z033,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    string ss = qs[0].Ec_purdate;

                    if (!string.IsNullOrEmpty(ss))
                    {

                        Ec_purdate.SelectedDate = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Ec_purdate.SelectedDate = DateTime.Now;
                    }


                    Ec_pursupplier.Text = qs[0].Ec_pursupplier;

                    Ec_purorder.Text = qs[0].Ec_purorder;


                    if (string.IsNullOrEmpty(qs[0].Ec_purnote))
                    {
                        Ec_purnote.Text = "--";
                    }
                    else
                    {
                        Ec_purnote.Text = qs[0].Ec_pmcmemo;//处理方式
                    }
                   

                    Ec_no.Text = qs[0].Ec_no;//设变号码
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期
                    Ec_detailstent.Text = qs[0].Ec_details;//设变内容
                    Ec_leader.Text = qs[0].Ec_leader;//担当

                    Ec_model.Text = qs[0].Ec_model;//设变机种
                    Ec_bomitem.Text = qs[0].Ec_bomitem;//成品


                    Ec_olditem.Text = qs[0].Ec_olditem;//旧物料
                    Ec_newitem.Text = qs[0].Ec_newitem;//新物料

                    Ec_bstock.Text = qs[0].Ec_bstock.ToString();//旧品在库
                    ProInv.Text = strinv.ToString();//旧品在库
                    Ec_pmclot.Text = qs[0].Ec_pmclot;//预定LoT
                    
                    Ec_pur.Text = strpurchaser;

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



            //destid = mydt.Rows[0][55].ToString();//实施区分
            //distid = mydt.Rows[0][54].ToString();//管理区分
            //issuedate= mydt.Rows[0][28].ToString();
        }
        private void BindDataStockQty()
        {
            try
            {

                var q = from a in DB.Pp_SapMaterials
                            //join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                            //where a.D_SAP_ZCA1D_Z002 == strEc_olditem
                        select new
                        {
                            a.D_SAP_ZCA1D_Z033,

                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.D_SAP_ZCA1D_Z033,

                        //E.D_SAP_ZCA1D_Z033,
                    }).Distinct().ToList();
                    strinv = qs[0].D_SAP_ZCA1D_Z033;
                }
                else

                {
                    strinv = 0;
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
        private void BindDataPurchaser()
        {
            try
            {

                var q = from a in DB.Pp_SapMaterials
                            //join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.D_SAP_ZCA1D_Z002 == strEc_newitem
                        select new
                        {
                            a.D_SAP_ZCA1D_Z009,

                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.D_SAP_ZCA1D_Z009,

                        //E.D_SAP_ZCA1D_Z033,
                    }).Distinct().ToList();
                    strpurchaser = qs[0].D_SAP_ZCA1D_Z009;
                }
                else

                {
                    strpurchaser = "ZZZ";
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
        private void BindDataBala()
        {
            try
            {
                var q = from a in DB.Pp_EcBalances
                            //join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        //where a.Ec_model == strEc_model
                        //where a.Ec_item == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        where a.Ec_newitem == strEc_newitem

                        select new
                        {
                            a.Ec_olditem,
                            a.Ec_oldqty,
                            a.Ec_poqty,
                            a.Ec_balanceqty,
                            a.Ec_newitem,
                            a.Ec_no,
                            a.Ec_issuedate,


                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(a => new
                    {
                        a.Ec_olditem,
                        a.Ec_oldqty,
                        a.Ec_poqty,
                        a.Ec_balanceqty,
                        a.Ec_newitem,
                        a.Ec_no,
                        a.Ec_issuedate,
                        //E.D_SAP_ZCA1D_Z033,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    Ec_poqty.Text = qs[0].Ec_poqty.ToString();
                    Ec_balanceqty.Text = qs[0].Ec_balanceqty.ToString();

                }
                else
                {
                    Ec_poqty.Text = "0";
                    Ec_balanceqty.Text = "0";
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

            //查询设变从表并循环添加
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

                                              Ec_purdate = Ec_purdate.SelectedDate.Value.ToString("yyyyMMdd"),
                                              Ec_purorder = Ec_purorder.Text.ToUpper(),
                                              Ec_pursupplier = Ec_pursupplier.Text.ToUpper(),
                                              Ec_purnote = Ec_purnote.Text.ToUpper(),
                                              ppModifier = GetIdentityName(),
                                              ppModifyTime = DateTime.Now,

                                              Ec_iqcdate = item.Ec_iqcdate,
                                              Ec_iqcorder = item.Ec_iqcorder,
                                              Ec_iqcnote = item.Ec_iqcnote,
                                              iqcModifier = item.iqcModifier,
                                              iqcModifyTime = item.iqcModifyTime,
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
        }
        private void SaveBala()//修改设变平衡表
        {            //查询设变从表并循环添加
            var q = (from a in DB.Pp_EcBalances
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
                    //where a.Ec_olditem.Contains(strEc_olditem)
                    where a.Ec_newitem.Contains(strEc_newitem)
                    //where b.Ec_no == strecn
                    //where a.Prodate == sdate//投入日期
                    select a).ToList();

            List<Pp_EcBalance> List = (from a in q
                                        select new Pp_EcBalance
                                        {
                                            GUID = a.GUID,
                                            Ec_no = a.Ec_no, //设变号码

                                            Ec_balancedate = DateTime.Now.ToString("yyyyMMdd"),  //item.Ec_model = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();

                                            Ec_olditem = a.Ec_olditem,//技术旧品号

                                            Ec_poqty = Decimal.Parse(Ec_poqty.Text),
                                            Ec_balanceqty = Decimal.Parse(Ec_balanceqty.Text),
                                            Ec_newitem = a.Ec_newitem,//技术新品号
                                            Ec_issuedate = a.Ec_issuedate,
                                            Ec_status = a.Ec_status,
                                            Ec_model = a.Ec_model,

                                            Ec_oldqty = a.Ec_oldqty,//在库

                                            Ec_precess = Ec_purnote.Text.ToUpper(),
                                            Ec_note = Ec_purnote.Text.ToUpper(),
                                            //item.Ec_issuedate = "";
                                            isEnd = 0,
                                            isDelete = 0,
                                            Remark = "",
                                            Creator = GetIdentityName(),
                                            CreateTime = DateTime.Now,
                                        }).ToList();



            DB.BulkUpdate(List);

            DB.BulkSaveChanges();

        }
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
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
                //if(issuedate!="")
                //{
                //    Alert.ShowInTop("设变已实施，不能修改！" + Ec_no.Text);
                //    return;
                //}
                //H101485
                if (!String.IsNullOrEmpty(Ec_pursupplier.Text.ToString().ToUpper().Trim()))
                {
                    //验证字符长度
                    if (!ValidatorTools.IsStringLength(Ec_pursupplier.Text.ToString().ToUpper().Trim(), 7, 7))
                    {
                        Ec_pursupplier.Text = "";
                        Ec_pursupplier.Focus();
                        Alert.ShowInTop("请输入7位以H开头的供应商代码。" + Ec_pursupplier.Text.ToString().ToUpper().Trim());
                        return;
                    }

                    //验证输入字串
                    if (!ValidatorTools.isVEN(Ec_pursupplier.Text.ToString().ToUpper().Trim()))
                    {
                        Ec_pursupplier.Text = "";
                        Ec_pursupplier.Focus();
                        Alert.ShowInTop("请输入7位以H开头的供应商代码。" + Ec_pursupplier.Text.ToString().ToUpper().Trim());
                        return;
                    }
                }
                //4300741139
                if (!String.IsNullOrEmpty(Ec_purorder.Text.ToString().ToUpper().Trim()))
                {
                    //验证字符长度
                    if (!ValidatorTools.IsStringLength(Ec_purorder.Text.ToString().ToUpper().Trim(), 10, 10))
                    {
                        Ec_purorder.Text = "";
                        Ec_purorder.Focus();
                        Alert.ShowInTop("请10位以43开头的采购PO。" + Ec_purorder.Text.ToString().ToUpper().Trim());
                        return;
                    }

                    //验证输入字串
                    if (!ValidatorTools.isPO(Ec_purorder.Text.ToString().ToUpper().Trim()))
                    {
                        Ec_purorder.Text = "";
                        Ec_purorder.Focus();
                        Alert.ShowInTop("请10位以43开头的采购PO。" + Ec_purorder.Text.ToString().ToUpper().Trim());
                        return;
                    }
                }




                SaveItem();



                SaveBala();

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

        #endregion
        private void Mailto()
        {
            var q = from a in DB.Adm_Users
                    where a.Dept.ID == 24 || a.Dept.ID == 13
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
            string mailTitle = "设变发行：" + strEc_no + " 物料：" + strEc_newitem;
            string mailBody = "Dear All,\r\n" + "\r\n" + "此设变采购部门已处理。\r\n" + "请贵部门担当者及时处理为盼。\r\n" + "此邮件不需回复！\r\n" + "よろしくお願いいたします。\r\n" + "\r\n" + "\r\n" + "「" + GetIdentityName() + "\r\n" + DateTime.Now.ToString() + "」\r\n" + "このメッセージはWebSiteから自動で送信されている。\r\n\n";  //发送邮件的正文
            MailHelper.SendEmail(strMailto, mailTitle, mailBody);
            strMailto = "";
        }
        #region NetOperateNotes        

        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();
            //新增日志
            string Newtext = Ec_issuedate.Text + "," + Ec_no.Text + "," + Ec_poqty.Text + "," + Ec_balanceqty.Text + "," + Ec_purdate.Text + "," + Ec_pursupplier.Text + "," + Ec_purorder.Text + "," + Ec_purnote.Text + "," + Ec_bomitem.Text + "," + Ec_olditem.Text + "," + Ec_newitem.Text + Ec_bomitem.Text + "," + Ec_olditem.Text + "," + Ec_newitem.Text;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit采购* " + Newtext + " *Edit采购 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);

        }
        #endregion


    }
}