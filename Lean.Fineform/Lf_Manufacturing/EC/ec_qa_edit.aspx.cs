using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_qa_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcQAEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string strMailto, strID, strEc_no, strEc_model, strEc_bomitem, strEc_olditem, strEc_newitem, strdist;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            //获取通过窗体传递的值
            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');

            strID = strgroup[0].ToString().Trim();
            strEc_no = strgroup[1].ToString().Trim();
            strEc_model = strgroup[2].ToString().Trim();
            strEc_bomitem = strgroup[3].ToString().Trim();
            strEc_olditem = strgroup[4].ToString().Trim();
            strEc_newitem = strgroup[5].ToString().Trim();
            strdist = strgroup[6].ToString().Trim();
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindData();
            BindGrid();
            //InitNewItem();
            //InitOldItem();
            //InitModel();
            Ec_qadate.Focus();
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

        private void BindGrid()
        {
            try
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                        join c in DB.Pp_SapMaterials on b.Ec_bomitem equals c.D_SAP_ZCA1D_Z002
                        //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where b.Ec_model == strEc_model
                        where b.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_qadate==""
                        select new
                        {
                            b.Ec_qadate,
                            b.Ec_qalot,

                            b.Ec_qanote,
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

                            b.Ec_p1dlot,

                            b.Ec_p1dnote,
                            //c.D_SAP_ZCA1D_Z033,

                            b.Ec_bomno,
                            b.Ec_bomsubitem,

                            b.Ec_oldset,

                            b.Ec_newset,

                            c.D_SAP_ZCA1D_Z005,
                        };

                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_model,
                        E.Ec_bomitem,
                        E.Ec_oldset,
                        E.Ec_newset,
                        E.D_SAP_ZCA1D_Z005,
                        E.Ec_olditem,
                        E.Ec_newitem,
                    }).Distinct();

                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    // 排列和数据库分页
                    // 2.获取当前分页数据
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
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

        private void BindData()
        {
            try
            {
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where b.Ec_model == strEc_model
                        where b.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_qadate==""

                        select new
                        {
                            b.Ec_qadate,
                            b.Ec_qalot,

                            b.Ec_qanote,
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

                            b.Ec_p1dlot,
                            b.Ec_mmlot,
                            b.Ec_p2dlot,

                            b.Ec_p1dnote,
                            //c.D_SAP_ZCA1D_Z033,
                        };

                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.Ec_qadate,
                        E.Ec_qalot,

                        E.Ec_qanote,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_status,
                        E.Ec_details,
                        E.Ec_leader,
                        E.Remark,
                        E.Ec_model,
                        //E.Ec_bomitem,
                        E.Ec_olditem,
                        E.Ec_newitem,
                        E.Ec_bstock,
                        E.Ec_pmclot,

                        E.Ec_p1dlot,
                        E.Ec_mmlot,
                        E.Ec_p2dlot,
                        E.Ec_p1dnote,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    string ss = qs[0].Ec_qadate;

                    if (!string.IsNullOrEmpty(ss))
                    {
                        //字串转日期
                        Ec_qadate.SelectedDate = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Ec_qadate.SelectedDate = DateTime.Now;
                    }
                    if (string.IsNullOrEmpty(qs[0].Ec_qalot))
                    {
                        Ec_qalot.Text = qs[0].Ec_p1dlot;
                    }
                    else
                    {
                        Ec_qalot.Text = qs[0].Ec_qalot;
                    }
                    if (string.IsNullOrEmpty(qs[0].Ec_qanote))
                    {
                        Ec_qalotsn.Text = "--";
                    }
                    else

                    {
                        Ec_qalotsn.Text = qs[0].Ec_qanote;
                    }
                    Ec_pmclot.Text = "||" + qs[0].Ec_pmclot.Trim();//生管批次

                    if (string.IsNullOrEmpty(qs[0].Ec_p1dlot))
                    {
                        Ec_qalot.Text = qs[0].Ec_pmclot.Trim();
                    }

                    Ec_p1dlot.Text = qs[0].Ec_p1dlot;//旧物料
                    Ec_mmlot.Text = qs[0].Ec_mmlot;//旧物料
                    Ec_p2dlot.Text = qs[0].Ec_p2dlot;//旧物料
                    //Ec_p1dlotsn.Text = qs[0].Ec_p1dlotsn;//新物料
                    Ec_p1dnote.Text = qs[0].Ec_p1dnote;//旧物料

                    Ec_no.Text = qs[0].Ec_no;//设变号码

                    Ec_detailstent.Text = qs[0].Ec_details;//设变内容
                    Ec_leader.Text = qs[0].Ec_leader;//担当
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

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        //字段赋值，保存
        private void SaveItem()//新增生产日报
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
                     where a.Ec_no == (strEc_no)
                     where a.Ec_model == (strEc_model)
                     where a.Ec_bomitem.Contains(strEc_bomitem)
                     //where a.Ec_olditem.Contains(strEc_olditem)
                     //where a.Ec_newitem.Contains(strEc_newitem)
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
                                             pmcModifyDate = item.pmcModifyDate,

                                             Ec_p2ddate = item.Ec_p2ddate,
                                             Ec_p2dlot = item.Ec_p2dlot,
                                             Ec_p2dnote = item.Ec_p2dnote,
                                             p2dModifier = item.p2dModifier,
                                             p2dModifyDate = item.p2dModifyDate,

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

                                             Ec_qadate = Ec_qadate.SelectedDate.Value.ToString("yyyyMMdd"),
                                             Ec_qalot = Ec_qalot.Text.ToUpper(),
                                             Ec_qanote = Ec_qalotsn.Text.ToUpper(),
                                             qaModifier = GetIdentityName(),
                                             qaModifyDate = DateTime.Now,

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
                                             isDeleted = item.isDeleted,
                                             Remark = item.Remark,
                                             Creator = item.Creator,
                                             CreateDate = item.CreateDate,
                                             Modifier = item.Modifier,
                                             ModifyDate = item.ModifyDate,
                                         }).ToList();
            DB.BulkUpdate(UpdateList);
            DB.BulkSaveChanges();
        }

        private void SaveItemSop()//修改SOP确认
        {
            #region 1.新增SOP

            //1.非采购件
            var q_NonPurchaseItem = from a in DB.Pp_EcSops
                                    where a.Ec_no == (strEc_no)
                                    where a.Ec_model == (strEc_model)
                                    //where b.Ec_no == strecn
                                    //where a.Prodate == sdate//投入日期
                                    select a;
            var resultNonPurchase = q_NonPurchaseItem.Distinct().ToList();
            List<Pp_Ec_Sop> NonPurchaseList = (from item in resultNonPurchase
                                               select new Pp_Ec_Sop
                                               {
                                                   GUID = item.GUID,
                                                   Ec_no = item.Ec_no,
                                                   Ec_model = item.Ec_model,

                                                   Ec_issuedate = item.Ec_issuedate,
                                                   Ec_leader = item.Ec_leader,
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
                                                   isDeleted = item.isDeleted,
                                                   Remark = item.Remark,
                                                   Creator = item.Creator,
                                                   CreateDate = item.CreateDate,
                                                   Modifier = item.Modifier,
                                                   ModifyDate = item.ModifyDate,
                                               }).ToList();
            DB.BulkUpdate(NonPurchaseList);
            DB.BulkSaveChanges();

            #endregion 1.新增SOP
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                SaveItem();
                SaveItemSop();
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

        #endregion Events

        private void Mailto()
        {
            var q = from a in DB.Adm_Users
                    where a.Dept.ID == 22
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
            string mailTitle = "设变发行：" + strEc_no + "机种：" + strEc_model;
            string mailBody = "Dear All,\r\n" + "\r\n" + "此设变已实施完成。\r\n" + "谢谢各部门配合。\r\n" + "\r\n" + "よろしくお願いいたします。\r\n" + "\r\n" + "\r\n" + "「" + GetIdentityName() + "\r\n" + DateTime.Now.ToString() + "」\r\n" + "このメッセージはWebSiteから自動で送信されている。\r\n\n";  //发送邮件的正文
            MailHelper.SendEmail(strMailto, mailTitle, mailBody);
            strMailto = "";
        }

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();

            //新增日志
            string Newtext = Ec_no.Text + "," + Ec_qadate.Text + "," + Ec_pmclot.Text + "," + Ec_qalot.Text + "," + Ec_qalotsn.Text;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit品管* " + Newtext + " *Edit品管 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}