﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_p2d_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcP2DEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string strMailto, strID, strEc_no, strPronewqty, strProoldqty, strEc_model, strEc_bomitem, strEc_olditem, strEc_newitem, strdist;

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

            //BindDdlLine();
            BindData();
            BindGrid();
            //InitNewItem();
            //InitOldItem();
            //InitModel();

            Ec_p2ddate.Focus();
        }

        #endregion Page_Load

        #region Events

        private void BindGrid()
        {
            try
            {
                var nqty = (from a in DB.Pp_SapMaterials
                            where a.D_SAP_ZCA1D_Z002 == strEc_newitem
                            select a).ToList();

                if (nqty.Any())
                {
                    strPronewqty = nqty[0].D_SAP_ZCA1D_Z033.ToString();
                }
                else
                {
                    strPronewqty = "0";
                }
                var oqty = (from a in DB.Pp_SapMaterials
                            where a.D_SAP_ZCA1D_Z002 == strEc_olditem
                            select a).ToList();

                if (oqty.Any())
                {
                    strProoldqty = oqty[0].D_SAP_ZCA1D_Z033.ToString();
                }
                else
                {
                    strProoldqty = "0";
                }
                var q = from a in DB.Pp_Ecs
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        join c in DB.Pp_SapMaterials on b.Ec_bomsubitem equals c.D_SAP_ZCA1D_Z002
                        //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where b.Ec_model == strEc_model
                        //where a.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_qadate==""
                        select new
                        {
                            b.Ec_mmdate,
                            b.Ec_mmlot,
                            b.Ec_mmlotno,
                            b.Ec_mmnote,
                            b.Ec_pmclot,

                            a.Ec_no,
                            a.Ec_issuedate,
                            a.Ec_status,
                            a.Ec_details,
                            a.Ec_leader,
                            a.Remark,
                            b.Ec_model,
                            b.Ec_bomno,
                            b.Ec_bomsubitem,
                            b.Ec_olditem,
                            b.Ec_oldset,
                            b.Ec_newitem,
                            b.Ec_newset,
                            Ec_bstock = c.D_SAP_ZCA1D_Z033,
                            Protext = c.D_SAP_ZCA1D_Z005,
                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        Newstock = strPronewqty,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_model,
                        E.Ec_bomsubitem,
                        E.Ec_olditem,
                        E.Ec_newitem,
                        E.Ec_oldset,
                        E.Ec_newset,
                        E.Ec_bomno,
                        Oldstock = strProoldqty,
                        E.Protext,
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
                        join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                        //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where b.Ec_model == strEc_model
                        where b.Ec_newitem != "0"
                        //where a.Ec_bomitem == strEc_bomitem
                        // where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_p2ddate==""
                        select new
                        {
                            b.Ec_p2ddate,
                            b.Ec_p2dlot,

                            b.Ec_p2dnote,
                            b.Ec_pmclot,

                            a.Ec_no,
                            a.Ec_issuedate,
                            a.Ec_status,
                            a.Ec_details,
                            a.Ec_leader,
                            a.Remark,
                            b.Ec_model,
                            //a.Ec_bomitem,
                            b.Ec_olditem,
                            b.Ec_newitem,
                            b.Ec_bstock,
                            //c.D_SAP_ZCA1D_Z033,
                        };

                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.Ec_p2ddate,
                        E.Ec_p2dlot,

                        E.Ec_p2dnote,
                        E.Ec_pmclot,

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
                        //E.D_SAP_ZCA1D_Z033,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    string ss = qs[0].Ec_p2ddate;

                    if (!string.IsNullOrEmpty(ss))
                    {
                        Ec_p2ddate.SelectedDate = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Ec_p2ddate.SelectedDate = DateTime.Now;
                    }
                    //string pline = "电子仓"; //班组
                    //Ec_p2dline.text = pline;
                    if (string.IsNullOrEmpty(qs[0].Ec_p2dlot))
                    {
                        if (!string.IsNullOrEmpty(qs[0].Ec_pmclot))
                        {
                            Ec_p2dlot.Text = qs[0].Ec_pmclot.Replace("-", "");
                        }
                        else
                        {
                            Ec_p2dlot.Text = qs[0].Ec_model.Replace("-", "") + DateTime.Now.ToString("yyM");
                        }
                    }
                    else
                    {
                        Ec_p2dlot.Text = qs[0].Ec_p2dlot;
                    }

                    //if (!string.IsNullOrEmpty(qs[0].Ec_p2dlotsn))
                    //{
                    //    Ec_p2dlotsn.Text = qs[0].Ec_p2dlotsn;
                    //}

                    Ec_no.Text = qs[0].Ec_no;//设变号码
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期
                    Ec_detailstent.Text = qs[0].Ec_details;//设变内容
                    Ec_leader.Text = qs[0].Ec_leader;//担当

                    Ec_model.Text = qs[0].Ec_model;//设变机种
                                                   //Ec_bomitem.Text = qs[0].Ec_bomitem;//成品

                    Ec_pmclot.Text = qs[0].Ec_pmclot.ToString();//生管批次
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

        protected void PageManager1_CustomEvent(object sender, CustomEventArgs e)
        {
            if (e.EventArgument == "Confirm_OK")
            {
                Alert.ShowInTop("此机种将设为与制二无关状态！");
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
                     where a.Ec_no.Contains(strEc_no)
                     where a.Ec_model.Contains(strEc_model)
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
                                              Ec_bomdate = item.Ec_bomdate,
                                              Ec_entrydate = item.Ec_entrydate,
                                              Ec_pmcdate = item.Ec_pmcdate,
                                              Ec_pmclot = item.Ec_pmclot,
                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                              Ec_pmcnote = item.Ec_pmcnote,
                                              Ec_bstock = item.Ec_bstock,
                                              pmcModifier = item.pmcModifier,
                                              pmcModifyDate = item.pmcModifyDate,

                                              Ec_p2ddate = Ec_p2ddate.SelectedDate.Value.ToString("yyyyMMdd"),
                                              Ec_p2dlot = Ec_p2dlot.Text.ToUpper(),
                                              Ec_p2dnote = Ec_p2dlotsn.Text.ToUpper(),
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
        }

        private void Irrelevant()
        {
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
                     where a.Ec_no.Contains(strEc_no)
                     where a.Ec_model.Contains(strEc_model)
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
                                              Ec_bomdate = item.Ec_bomdate,
                                              Ec_entrydate = item.Ec_entrydate,

                                              IsManage = item.IsManage,
                                              IsMmManage = item.IsManage,
                                              IsPcbaManage = 0,
                                              IsAssyManage = item.IsAssyManage,
                                              Ec_pmcdate = item.Ec_pmcdate,
                                              Ec_pmclot = item.Ec_pmclot,
                                              Ec_pmcmemo = item.Ec_pmcmemo,
                                              Ec_pmcnote = item.Ec_pmcnote,
                                              Ec_bstock = item.Ec_bstock,
                                              pmcModifier = item.pmcModifier,
                                              pmcModifyDate = item.pmcModifyDate,

                                              Ec_p2ddate = Ec_p2ddate.SelectedDate.Value.ToString("yyyyMMdd"),
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

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
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
                //    Ec_ user = DB.Pp_Ec_Subs.Where(u => u.Ec_issuedate + u.Ec_no + u.Ec_model == ecndate + ecnno + ecnmodel).FirstOrDefault();

                //    if (user != null)
                //    {
                //        Alert.ShowInTop("设变 " + ecndate + ecnno + ecnmodel + " 已经存在！");
                //        return;
                //    }
                //}
                if (Ec_p2dlot.Text == Ec_model.Text.Replace("-", ""))
                {
                    //Ec_pmclot.Text = "";
                    Ec_p2dlot.Focus();
                    Alert.ShowInTop("生产批次与机种同名，请重新输入。");
                    return;
                }
                else
                {
                    if (!ValidatorTools.IsInteger(Ec_model.Text.Substring(0, 1)))
                    {
                        if (!String.IsNullOrEmpty(Ec_p2dlot.Text.ToString().ToUpper().Trim()))
                        {
                            string lot = Ec_p2dlot.Text.ToString().ToUpper().Trim();
                            string[] fields = lot.Split(',');

                            int lot_count = fields.Length; // 数组个数
                            String lot_field = "";

                            for (int i = 0; i < lot_count; i++)  // ---------- 字段循环
                            {
                                //显示字段名称
                                lot_field = fields[i];

                                //验证字符长度
                                if (!ValidatorTools.IsStringLength(lot_field, 3, 20))
                                {
                                    Ec_p2dlot.Text = "";
                                    Ec_p2dlot.Focus();
                                    Alert.ShowInTop("请输入3到20位以字母开头。" + lot_field);
                                    return;
                                }

                                //验证输入字串
                                if (!ValidatorTools.isLOT(lot_field))
                                {
                                    Ec_p2dlot.Text = "";
                                    Ec_p2dlot.Focus();
                                    Alert.ShowInTop("生产批次格式为字母+数字，请重新输入。" + lot_field);
                                    return;
                                }
                            }  // End_For
                        }
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
            PageContext.RegisterStartupScript(Confirm.GetShowReference("警告！点击确定此机种将设为与制二课无关状态。",
                        String.Empty,
                        MessageBoxIcon.Question,
                        PageManager1.GetCustomEventReference(false, "Confirm_OK"), // 第一个参数 false 用来指定当前不是AJAX请求
                        PageManager1.GetCustomEventReference("Confirm_Cancel")));
        }

        #endregion Events

        #region NetOperateNotes

        //发送邮件通知
        private void Mailto()
        {
            var q = from a in DB.Adm_Users
                    where a.Dept.ID == 18
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
            string mailBody = "Dear All,\r\n" + "\r\n" + "此设变制二课已处理。\r\n" + "请贵部门担当者及时处理为盼。\r\n" + "\r\n" + "よろしくお願いいたします。\r\n" + "\r\n" + "\r\n" + "「" + GetIdentityName() + "\r\n" + DateTime.Now.ToString() + "」\r\n" + "このメッセージはWebSiteから自動で送信されている。\r\n\n";  //发送邮件的正文
            MailHelper.SendEmail(strMailto, mailTitle, mailBody);
            strMailto = "";
        }

        private void InsNetOperateNotes()
        {
            //Mailto();
            //新增日志
            string Newtext = Ec_issuedate.Text + Ec_no.Text + Ec_p2ddate.Text + Ec_pmclot.Text + Ec_p2dlot.Text + Ec_p2dlotsn.Text + "," + strEc_newitem + "," + strEc_olditem;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit制二* " + Newtext + " *Edit制二 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}