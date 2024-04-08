using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.SOP
{
    public partial class pe_pcba_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSopEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string strEc_no, strEc_model;

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

            //获取通过窗体传递的值
            string strtransmit = GetQueryValue("Ec_no");
            //转字符串组
            string[] strgroup = strtransmit.Split(',');

            strEc_no = strgroup[0].ToString().Trim();
            strEc_model = strgroup[1].ToString().Trim();

            BindGrid();
            BindData();

            //InitNewItem();
            //InitOldItem();
            //InitModel();
        }

        #endregion Page_Load

        #region Events

        private void BindGrid()
        {
            try
            {
                var q = from a in DB.Pp_Ec_Sops
                            //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where a.Ec_model == strEc_model
                        //where a.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_qadate==""
                        select new
                        {
                            a.ispengpModifysop,
                            a.Ec_pengadate,

                            a.Ec_no,
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_model,
                            a.pengaModifier,
                        };
                bool sss = q.Any();
                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(a => new
                    {
                        a.ispengpModifysop,
                        a.Ec_pengadate,

                        a.Ec_no,
                        a.Ec_issuedate,
                        a.Ec_leader,
                        a.Ec_model,
                        a.pengaModifier,
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
                var q = from a in DB.Pp_Ec_Sops
                            //join b in DB.Pp_Ec_Subs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on a.Ec_olditem equals c.D_SAP_ZCA1D_Z002
                        where a.Ec_no == strEc_no
                        where a.Ec_model == strEc_model
                        //where a.Ec_bomitem == strEc_bomitem
                        //where a.Ec_olditem == strEc_olditem
                        //where a.Ec_newitem == strEc_newitem
                        //where a.Ec_p1ddate==""
                        //where a.Ec_newitem != "0"
                        select new
                        {
                            a.ispengpModifysop,
                            a.Ec_pengpdate,
                            a.Ec_no,
                            a.Ec_issuedate,
                            a.Ec_leader,
                            a.Ec_model,
                            a.pengpModifier,
                        };

                if (q.Any())
                { // 切勿使用 source.Count() > 0
                    var qs = q.Select(E => new
                    {
                        E.ispengpModifysop,
                        E.Ec_pengpdate,
                        E.Ec_no,
                        E.Ec_issuedate,
                        E.Ec_leader,
                        E.Ec_model,
                        E.pengpModifier,
                    }).Distinct().ToList();

                    //var qs = q.Distinct().ToList();

                    string ss = qs[0].Ec_pengpdate;

                    if (!string.IsNullOrEmpty(ss))
                    {
                        //字串转日期
                        Ec_pengdate.SelectedDate = DateTime.ParseExact(ss, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        Ec_pengdate.SelectedDate = DateTime.Now;
                    }
                    if (qs[0].ispengpModifysop == 1)
                    {
                        isengModifysop.SelectedValue = "1";
                    }
                    else

                    {
                        isengModifysop.SelectedValue = "0";
                    }

                    // Ec_mmlotno.Text = qs[0].Ec_p2dlotsn;

                    //Ec_mmlotno.Text = qs[0].Ec_mmlotno;
                    //Ec_mmnote.Text = qs[0].Ec_mmnote;
                    // Ec_pengnote.Text = qs[0].Ec_pengpnote;

                    // Ec_p2dlotsn.Text = qs[0].Ec_p2dlotsn;

                    //Ec_p2dnote.Text = qs[0].Ec_p2dnote;

                    Ec_no.Text = qs[0].Ec_no;//设变号码
                    Ec_issuedate.Text = qs[0].Ec_issuedate;//发行日期
                    //Ec_detailstent.Text = qs[0].Ec_details;//设变内容
                    Ec_leader.Text = qs[0].Ec_leader;//担当

                    if (!string.IsNullOrEmpty(qs[0].pengpModifier))
                    {
                        var q_name = (from a in DB.Adm_Users
                                      where a.Name.Contains(qs[0].pengpModifier)
                                      select a).ToList();
                        if (q_name.Any())
                        {
                            pengpModifier.Text = q_name[0].ChineseName;//担当
                        }
                    }
                    else
                    {
                        string sname = GetIdentityName();
                        var q_name = (from a in DB.Adm_Users
                                      where a.Name.Contains(sname)
                                      select a).ToList();
                        if (q_name.Any())
                        {
                            pengpModifier.Text = q_name[0].ChineseName;//担当
                        }
                    }

                    Ec_model.Text = qs[0].Ec_model;//设变机种

                    //Ec_pmclot.Text = qs[0].Ec_qalot.Trim();//生管批次
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
            try
            {
                byte isSop;
                if (isengModifysop.SelectedValue == "1")
                {
                    isSop = 1;
                }
                else

                {
                    isSop = 0;
                }

                var q = (from a in DB.Pp_Ec_Sops
                             //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                             //join b in DB.Pp_Ec_Subs on a.D_SAP_ZPABD_Z001 equals b.Ec_no
                             //join c in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals c.D_SAP_DEST_Z001
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                             //                                   select d.D_SAP_ZCA1D_Z002)
                             //                                .Contains(a.D_SAP_ZPABD_S002)
                             //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.ProSapModelDests
                             //                                   select d.D_SAP_DEST_Z001)
                             //                                 .Contains(a.D_SAP_ZPABD_S002)
                         where a.Ec_no == (strEc_no)
                         where a.Ec_model == (strEc_model)
                         //where a.Ec_olditem.Contains(strEc_olditem)
                         //where a.Ec_newitem.Contains(strEc_newitem)
                         //where b.Ec_no == strecn
                         //where a.Prodate == sdate//投入日期
                         select a).ToList();
                List<Pp_Ec_Sop> UpdateList = (from item in q
                                              select new Pp_Ec_Sop
                                              {
                                                  GUID = item.GUID,
                                                  Ec_no = item.Ec_no,
                                                  Ec_model = item.Ec_model,
                                                  Ec_issuedate = item.Ec_issuedate,
                                                  Ec_leader = item.Ec_leader,
                                                  Ec_entrydate = item.Ec_entrydate,

                                                  ispengaModifysop = item.ispengaModifysop,
                                                  Ec_pengadate = item.Ec_pengadate,
                                                  Ec_penganote = item.Ec_penganote,
                                                  pengaModifier = item.pengaModifier,
                                                  pengaModifyDate = item.pengaModifyDate,

                                                  Ec_pengpdate = Ec_pengdate.SelectedDate.Value.ToString("yyyyMMdd"),
                                                  ispengpModifysop = isSop,
                                                  Ec_pengpnote = "-",
                                                  pengpModifier = GetIdentityName(),
                                                  pengpModifyDate = DateTime.Now,

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
                                                  Modifier = GetIdentityName(),
                                                  ModifyDate = DateTime.Now,
                                              }).ToList();
                DB.BulkUpdate(UpdateList);
                DB.BulkSaveChanges();
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

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveItem();
            InsNetOperateNotes();
        }

        #endregion Events

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();

            //新增日志
            string Newtext = Ec_issuedate.Text + "," + Ec_no.Text + "," + Ec_pengdate.Text + "," + isengModifysop.SelectedValue;
            string OperateType = "修改";//操作标记
            string OperateNotes = "Edit制技* " + Newtext + " *Edit制技 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变修改", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}