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

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_eng_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限,空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcENGNew";
            }
        }

        #endregion ViewPower

        public static string strMana, IsCheck, strMailto, strWhouse, strPur, strEol, strEcnno, strInv, bitem, sitem, oitem, oitemset, nitem, nitemset, fileName, txtPbookdoc, txtPpbookdoc, txtPjpbookdoc, txtPdoc;
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
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.管理区分：全仕向,部管课,内部管理,技术课</div><div>2.附件大小规定：单一文件不能超5Mb,如果同时上付多个附件,附件总大小不能超过20Mb,否则请逐个上传</div><div>3.请添加中文翻译内容</div><div>4.担当者不能为空</div><div>4.修改管理区分时,设变单身数据将重新导入,之前各部门填写的资料全部作废</div><div style=\"margin-bottom:10px;color:red;\">5.关于制二课管理与否：物料状态分为4种：0--不管理，1--共通(全部门都需填写)，2--部管无关，3--制二无关,4--组立无关</div>");
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDdlUserlist();
            BindData();
            BindDdlAdmindist();
            BindDdlItemlist();
            BindDdltype();
            //BindDdlmodel();
            //BindDdlohbn();
            //BindDdlnhbn();
            //InitOldItem();
            //InitNewItem();
            //InitModel();
            BindGrid();
        }

        #endregion Page_Load

        #region Events

        private void BindData()//SAP设变信息查询
        {
            strEcnno = Request.QueryString["ECNNO"];
            //string mysql = "SELECT D_SAP_ZPABD_Z001 AS ECNNO,D_SAP_ZPABD_Z005 AS ISSUEDATE,D_SAP_ZPABD_Z003 AS ECNTITLE,D_SAP_ZPABD_Z002 AS ECNMODEL ,D_SAP_ZPABD_Z027 AS ECNDETAIL,D_SAP_ZPABD_Z025 AS AMOUT,D_SAP_ZPABD_Z012 AS mReason,D_SAP_ZPABD_Z013 AS sReason,[D_SAP_ZPABD_Z004] AS Flag  FROM [dbo].[ProSapEngChanges] " +
            //                "LEFT JOIN  [dbo].[Ec_s] ON REPLACE(Ec_no,' ','')=D_SAP_ZPABD_Z001 " +
            //                " WHERE REPLACE(Ec_no,' ','') IS NULL  AND D_SAP_ZPABD_Z001='" + ItemMaster + "' " +
            //                " GROUP BY D_SAP_ZPABD_Z001,D_SAP_ZPABD_Z002,D_SAP_ZPABD_Z003,D_SAP_ZPABD_Z027,D_SAP_ZPABD_Z005,D_SAP_ZPABD_Z025,D_SAP_ZPABD_Z012,D_SAP_ZPABD_Z013,[D_SAP_ZPABD_Z004] ORDER BY D_SAP_ZPABD_Z005 DESC;";

            var q = from a in DB.Pp_SapEcns
                    where a.D_SAP_ZPABD_Z001 == strEcnno
                    select new
                    {
                        a.D_SAP_ZPABD_Z001,
                        a.D_SAP_ZPABD_Z005,
                        a.D_SAP_ZPABD_Z003,
                        a.D_SAP_ZPABD_Z002,
                        a.D_SAP_ZPABD_Z027,
                        a.D_SAP_ZPABD_Z025,
                        a.D_SAP_ZPABD_Z012,
                        a.D_SAP_ZPABD_Z013,
                        a.D_SAP_ZPABD_Z004,
                    };
            var qs = q.Select(E => new
            {
                E.D_SAP_ZPABD_Z001,
                E.D_SAP_ZPABD_Z005,
                E.D_SAP_ZPABD_Z003,
                E.D_SAP_ZPABD_Z002,
                E.D_SAP_ZPABD_Z027,
                E.D_SAP_ZPABD_Z025,
                E.D_SAP_ZPABD_Z012,
                E.D_SAP_ZPABD_Z013,
                E.D_SAP_ZPABD_Z004,
            }).Distinct().ToList();

            Ec_issuedate.Text = qs[0].D_SAP_ZPABD_Z005;//发行日期
            Ec_no.Text = qs[0].D_SAP_ZPABD_Z001;//设变号码
            Ec_title.Text = qs[0].D_SAP_ZPABD_Z003;//设变标题
            mReason.Text = qs[0].D_SAP_ZPABD_Z012;//设变原因
            sReason.Text = qs[0].D_SAP_ZPABD_Z013;//设变原因
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
            Ec_status.Text = qs[0].D_SAP_ZPABD_Z004;//状态

            //DataTable mydt = GetDataTable.Getdt(mysql);
            //Ec_issuedate.Text = mydt.Rows[0][1].ToString();//发行日期
            //Ec_no.Text = mydt.Rows[0][0].ToString();//设变号码
            //Ec_title.Text = mydt.Rows[0][2].ToString();//设变标题
            //mReason.Text = mydt.Rows[0][6].ToString();//设变原因
            //sReason.Text = mydt.Rows[0][7].ToString();//设变原因
            //Ec_model.Text = mydt.Rows[0][3].ToString();//设变机种
            //Ec_details.Text = mydt.Rows[0][4].ToString();//设变内容
            //Ec_detailstent.Text = mydt.Rows[0][4].ToString();//DTA设变内容
            //Ec_lossamount.Text = mydt.Rows[0][5].ToString();//仕损金额
            //Ec_status.Text = mydt.Rows[0][8].ToString();//状态
        }

        private void BindGrid()//管理确认
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
                              //join b in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals b.D_SAP_ZCA1D_Z002
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

            if (DDL_Item.SelectedIndex != -1 && DDL_Item.SelectedIndex != 0)
            {
                q_All = q_All.Where(u => u.Ec_bomitem.Contains(this.DDL_Item.SelectedItem.Text));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q_All);
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q_All);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
                Grid1.DataBind();
                Alert.ShowInTop("设变单身没有资料，请通知电脑课追加机种信息！");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }

            #endregion 停产机种不导入
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

        protected void ddlPbook_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ec_letterno.Text = "";
        }

        private void BindDdlAdmindist()
        {
            IQueryable<Adm_Dict> q = DB.Adm_Dicts;
            q = q.Where(u => u.DictType.Contains("app_ec_mgtype"));
            //q = q.OrderBy(u => u.Difftype);

            // 绑定到下拉列表（启用模拟树功能）

            Ec_distinction.DataTextField = "DictLabel";
            Ec_distinction.DataValueField = "DictValue";
            Ec_distinction.DataSource = q;
            Ec_distinction.DataBind();

            // 选中根节点
            Ec_distinction.SelectedValue = "0";
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
            var qs = q.Select(E => new { E.Docname, E.Docnumber }).ToList().Distinct();
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

        //字段赋值,保存
        private void SaveItem()//新增设变单头
        {
            //判断重复
            string inputecNo = Ec_no.Text.Trim();

            Pp_Ec ec = DB.Pp_Ecs.Where(u => u.Ec_no == inputecNo && u.IsDeleted == 0).FirstOrDefault();

            if (ec == null)
            {
                //查询设变从表并循环添加

                Pp_Ec item = new Pp_Ec();
                if (!string.IsNullOrEmpty(Ec_issuedate.Text))
                {
                    item.Ec_issuedate = Ec_issuedate.Text;
                    item.Remark = "";
                }
                else
                {
                    item.Ec_issuedate = DateTime.Now.ToString("yyyyMMdd");
                    item.Remark = "SAP中发行日期为空";
                }

                item.Ec_no = Ec_no.Text;
                //item.Ec_relyno = Ec_relyno.Text;
                //item.Ec_model = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();

                //item.Ec_tcjno = Ec_tcjno.Text;
                item.Ec_status = Ec_status.Text;//状态
                if (Ec_details.Text != "-")
                {
                    item.Ec_details = Ec_details.Text;//设变内容
                }
                else
                {
                    item.Ec_details = Ec_detailstent.Text;//设变内容
                }

                item.Ec_leader = Ec_leader.SelectedItem.Text;//DTA担当
                //金额
                item.Ec_lossamount = int.Parse(Ec_lossamount.Text);//金额
                item.Ec_distinction = int.Parse(Ec_distinction.SelectedValue);
                item.Ec_entrydate = DateTime.Now.ToString("yyyyMMdd");
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

                Pdoc();
                if (!string.IsNullOrEmpty(txtPdoc))
                {
                    item.Ec_documents = txtPdoc;
                }
                else
                {
                    item.Ec_documents = "";
                }
                //if (IsSopUpdate.SelectedValue == "1")
                //{
                //item.IsSopUpdate = 1;
                //}
                //else

                //{
                //    item.IsSopUpdate = 0;
                //}
                //if (IsManage.SelectedValue == "1")
                //{
                //item.IsManage = 1;
                //}
                //else

                //{
                //    item.IsManage = 0;
                //}
                item.Ec_title = Ec_title.Text;
                item.UDF06 = Ec_title.Text;
                item.IsDeleted = 0;

                item.GUID = Guid.NewGuid();
                item.CreateDate = DateTime.Now;
                item.Creator = GetIdentityName();
                DB.Pp_Ecs.Add(item);
                DB.SaveChanges();
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

                Ec_letterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

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
                Ec_eppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPpbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

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
                Ec_teppletterdoc.SaveAs(Server.MapPath("../../Lf_Documents/ecdocs/" + fileName));

                txtPjpbookdoc = "../../Lf_Documents/ecdocs/" + fileName;

                //// 清空表单字段（第一种方法）
                //tbxUseraName.Reset();
                //filePhoto.Reset();

                // 清空表单字段（第三种方法）
                SimpleForm1.Reset();
            }
        }

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

        private void SaveItemSub()//新增设变SUB表
        {
            try
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
                    List<Pp_Ec_Sub> New_NonItemList = (from a in result_NonItem
                                                       select new Pp_Ec_Sub
                                                       {
                                                           GUID = Guid.NewGuid(),
                                                           Ec_no = a.Ec_no,
                                                           Ec_model = a.Ec_model,
                                                           Ec_bomitem = a.Ec_bomitem,
                                                           Ec_bomsubitem = a.Ec_bomsubitem,
                                                           Ec_olditem = a.Ec_olditem,
                                                           Ec_oldtext = a.Ec_oldtext,
                                                           Ec_oldqty = a.Ec_oldqty,
                                                           Ec_oldset = a.Ec_oldset,
                                                           Ec_newitem = a.Ec_newitem,
                                                           Ec_newtext = a.Ec_newtext,
                                                           Ec_newqty = a.Ec_newqty,
                                                           Ec_newset = a.Ec_newset,
                                                           Ec_procurement = a.Ec_procurement,
                                                           Ec_location = a.Ec_location,
                                                           Ec_eol = a.Ec_eol,
                                                           IsCheck = a.IsCheck,
                                                           IsManage = 1,
                                                           IsMmManage = 0,
                                                           IsAssyManage = 1,
                                                           IsPcbaManage = 1,
                                                           Ec_bomno = a.Ec_bomno,
                                                           Ec_change = a.Ec_change,
                                                           Ec_local = a.Ec_local,
                                                           Ec_note = a.Ec_note,
                                                           Ec_process = a.Ec_process,
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
                }
                if (this.Ec_distinction.SelectedValue == "2")//部管
                {
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
                                                               IsPcbaManage = 1,
                                                               IsAssyManage = 0,

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
                    List<Pp_Ec_Sub> New_NonItemList = (from a in result_NonItem
                                                       select new Pp_Ec_Sub
                                                       {
                                                           GUID = Guid.NewGuid(),
                                                           Ec_no = a.Ec_no,
                                                           Ec_model = a.Ec_model,
                                                           Ec_bomitem = a.Ec_bomitem,
                                                           Ec_bomsubitem = a.Ec_bomsubitem,
                                                           Ec_olditem = a.Ec_olditem,
                                                           Ec_oldtext = a.Ec_oldtext,
                                                           Ec_oldqty = a.Ec_oldqty,
                                                           Ec_oldset = a.Ec_oldset,
                                                           Ec_newitem = a.Ec_newitem,
                                                           Ec_newtext = a.Ec_newtext,
                                                           Ec_newqty = a.Ec_newqty,
                                                           Ec_newset = a.Ec_newset,
                                                           Ec_procurement = a.Ec_procurement,
                                                           Ec_location = a.Ec_location,
                                                           Ec_eol = a.Ec_eol,
                                                           IsCheck = a.IsCheck,
                                                           IsManage = 0,
                                                           IsMmManage = 0,
                                                           IsPcbaManage = 1,
                                                           IsAssyManage = 0,
                                                           Ec_bomno = a.Ec_bomno,
                                                           Ec_change = a.Ec_change,
                                                           Ec_local = a.Ec_local,
                                                           Ec_note = a.Ec_note,
                                                           Ec_process = a.Ec_process,
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
                                                              IsManage = 0,
                                                              IsMmManage = 1,
                                                              IsPcbaManage = 0,
                                                              IsAssyManage = 0,
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
                }
                if (this.Ec_distinction.SelectedValue == "3")//内部
                {
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
                                                               IsPcbaManage = 1,
                                                               IsAssyManage = 0,

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
                    List<Pp_Ec_Sub> New_NonItemList = (from a in result_NonItem
                                                       select new Pp_Ec_Sub
                                                       {
                                                           GUID = Guid.NewGuid(),
                                                           Ec_no = a.Ec_no,
                                                           Ec_model = a.Ec_model,
                                                           Ec_bomitem = a.Ec_bomitem,
                                                           Ec_bomsubitem = a.Ec_bomsubitem,
                                                           Ec_olditem = a.Ec_olditem,
                                                           Ec_oldtext = a.Ec_oldtext,
                                                           Ec_oldqty = a.Ec_oldqty,
                                                           Ec_oldset = a.Ec_oldset,
                                                           Ec_newitem = a.Ec_newitem,
                                                           Ec_newtext = a.Ec_newtext,
                                                           Ec_newqty = a.Ec_newqty,
                                                           Ec_newset = a.Ec_newset,
                                                           Ec_procurement = a.Ec_procurement,
                                                           Ec_location = a.Ec_location,
                                                           Ec_eol = a.Ec_eol,
                                                           IsCheck = a.IsCheck,
                                                           IsManage = 0,
                                                           IsMmManage = 0,
                                                           IsPcbaManage = 1,
                                                           IsAssyManage = 0,
                                                           Ec_bomno = a.Ec_bomno,
                                                           Ec_change = a.Ec_change,
                                                           Ec_local = a.Ec_local,
                                                           Ec_note = a.Ec_note,
                                                           Ec_process = a.Ec_process,
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
                                                              IsPcbaManage = 1,
                                                              IsAssyManage = 0,
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
                                                                      IsPcbaManage = 1,
                                                                      IsAssyManage = 0,
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
                }
                if (this.Ec_distinction.SelectedValue == "4")//技术
                {
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
                                                               IsManage = 2,
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
                    List<Pp_Ec_Sub> New_NonItemList = (from a in result_NonItem
                                                       select new Pp_Ec_Sub
                                                       {
                                                           GUID = Guid.NewGuid(),
                                                           Ec_no = a.Ec_no,
                                                           Ec_model = a.Ec_model,
                                                           Ec_bomitem = a.Ec_bomitem,
                                                           Ec_bomsubitem = a.Ec_bomsubitem,
                                                           Ec_olditem = a.Ec_olditem,
                                                           Ec_oldtext = a.Ec_oldtext,
                                                           Ec_oldqty = a.Ec_oldqty,
                                                           Ec_oldset = a.Ec_oldset,
                                                           Ec_newitem = a.Ec_newitem,
                                                           Ec_newtext = a.Ec_newtext,
                                                           Ec_newqty = a.Ec_newqty,
                                                           Ec_newset = a.Ec_newset,
                                                           Ec_procurement = a.Ec_procurement,
                                                           Ec_location = a.Ec_location,
                                                           Ec_eol = a.Ec_eol,
                                                           IsCheck = a.IsCheck,
                                                           IsManage = 2,
                                                           IsMmManage = 0,
                                                           IsAssyManage = 0,
                                                           IsPcbaManage = 0,
                                                           Ec_bomno = a.Ec_bomno,
                                                           Ec_change = a.Ec_change,
                                                           Ec_local = a.Ec_local,
                                                           Ec_note = a.Ec_note,
                                                           Ec_process = a.Ec_process,
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
                                                              IsManage = 2,
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
                                                                      IsManage = 2,
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

                    DB.BulkUpdate(UpdateP2DPurList);
                    var MMPur = from a in DB.Pp_Ec_Subs
                                where a.Ec_no.Contains(Ec_no.Text)
                                where a.Ec_newitem == "0"
                                where a.Ec_procurement == "F"
                                where a.Ec_location != "C003"
                                where a.IsDeleted == 0
                                select a;
                    var MMPurList = MMPur.ToList().Distinct().Distinct();
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

                    DB.BulkUpdate(UpdateP2DNoPur);

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
                                                       Modifier = item.Modifier,
                                                       ModifyDate = item.ModifyDate,
                                                   }).ToList();

                DB.BulkUpdate(UpdateStockList);
                DB.BulkSaveChanges();

                #endregion 6.更新旧品库存
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

        private void SaveBalance()//新增平衡表
        {
            var q = from a in DB.Pp_Ec_Subs
                    where a.Ec_olditem != "0"
                    //where a.D_SAP_ZPABD_S002.CompareTo("20190701") > 0
                    //join b in DB.ProSapModelDests on a.D_SAP_ZPABD_S002 equals b.D_SAP_DEST_Z001
                    //where a.D_SAP_ZPABD_S002 != "" && (from d in DB.Pp_SapMaterials
                    //                                   select d.D_SAP_ZCA1D_Z002)
                    //                                .Contains(a.D_SAP_ZPABD_S002)
                    //where b.Ec_no == strecn
                    //where a.Prodate == sdate//投入日期
                    where a.Ec_no.Contains(strEcnno)
                    select new
                    {
                        a.Ec_no,
                        a.Ec_model,
                        //a.Ec_bomitem,
                        a.Ec_olditem,
                        a.Ec_newitem,
                        a.Ec_bstock,
                        a.Ec_entrydate,
                    };
            //去重复
            var qs = q.Distinct().ToList();

            List<Pp_Ec_Balance> List = (from a in qs
                                        select new Pp_Ec_Balance
                                        {
                                            Ec_no = a.Ec_no, //设变号码

                                            Ec_balancedate = DateTime.Now.ToString("yyyyMMdd"),  //item.Ec_model = i.ToString().Replace("-", "").Replace(" ", "").ToUpper();

                                            Ec_olditem = a.Ec_olditem,//技术旧品号

                                            Ec_poqty = 0,
                                            Ec_balanceqty = 0,
                                            Ec_newitem = a.Ec_newitem,//技术新品号
                                            Ec_issuedate = a.Ec_entrydate,
                                            Ec_status = "Fixed",
                                            Ec_model = a.Ec_model,

                                            Ec_oldqty = a.Ec_bstock,//在库

                                            Ec_precess = "",
                                            Ec_note = "",
                                            //item.Ec_issuedate = "";
                                            isEnd = 0,
                                            IsDeleted = 0,
                                            Remark = "",
                                            GUID = Guid.NewGuid(),
                                            Creator = GetIdentityName(),
                                            CreateDate = DateTime.Now,
                                        }).ToList();

            DB.BulkInsert(List);

            DB.BulkSaveChanges();
        }

        //SOP确认
        private void SaveItemSop()//新增SOP确认
        {
            #region 1.新增SOP

            //if (this.IsSopUpdate.SelectedValue == "1")
            //{
            var q_SopItem = from a in DB.Pp_Ec_Subs
                                //join d in DB.Pp_SapMaterials on a.D_SAP_ZPABD_S008 equals d.D_SAP_ZCA1D_Z002
                            where a.Ec_no.Contains(Ec_no.Text)
                            //where a.Ec_model.Contains()
                            //where b.Ec_no == strecn
                            //where a.Prodate == sdate//投入日期
                            select new
                            {
                                //a.Ec_issuedate,
                                // a.Ec_leader,
                                a.Ec_no,
                                a.Ec_model,
                            };
            var resultSop = q_SopItem.Distinct().ToList();
            List<Pp_Ec_Sop> NonPurchaseList = (from a in resultSop
                                               select new Pp_Ec_Sop
                                               {
                                                   GUID = Guid.NewGuid(),
                                                   Ec_issuedate = Ec_issuedate.Text,
                                                   Ec_leader = Ec_leader.SelectedItem.Text,
                                                   ispengaModifysop = 1,
                                                   ispengpModifysop = 1,
                                                   Ec_no = a.Ec_no,
                                                   Ec_model = a.Ec_model,

                                                   Ec_entrydate = DateTime.Now.ToString("yyyyMMdd"),

                                                   //    //组立
                                                   Ec_pengadate = "",
                                                   Ec_penganote = "",
                                                   pengaModifier = "",
                                                   //pengaModifyDate = "",
                                                   //    //PCBA
                                                   Ec_pengpdate = "",
                                                   Ec_pengpnote = "",
                                                   pengpModifier = "",
                                                   //pengpModifyDate = "",

                                                   UDF01 = "",
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
                                                   Remark = "",
                                                   Creator = GetIdentityName(),
                                                   CreateDate = DateTime.Now,
                                               }).Distinct().ToList();
            DB.BulkInsert(NonPurchaseList);
            DB.BulkSaveChanges();
            //}

            #endregion 1.新增SOP
        }

        //制二课管理确认

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

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
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

                if (Ec_leader.SelectedIndex == 0 || Ec_leader.SelectedIndex == -1)
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
                SaveItemSub();
                SaveBalance();
                SaveItemSop();
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

        protected void IsSopUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowNotify("列表三选中项的值：" + rblAutoPostBack.SelectedValue);

            //if (IsSopUpdate.SelectedValue == "1")
            //{
            //    Alert.ShowInTop("设变No:" + Ec_no.Text + "，SOP需要更新！", "提示信息", MessageBoxIcon.Information);
            //}
            //else

            //{
            //    Alert.ShowInTop("设变No:" + Ec_no.Text + "，SOP不需要更新！", "提示信息", MessageBoxIcon.Information);
            //}
        }

        protected void IsManage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ShowNotify("列表三选中项的值：" + rblAutoPostBack.SelectedValue);

            //if (IsManage.SelectedValue == "1")
            //{
            //    Alert.ShowInTop("设变No:" + Ec_no.Text + "，将设定成制二课需要管理！", "提示信息", MessageBoxIcon.Information);
            //}
            //else

            //{
            //    Alert.ShowInTop("设变No:" + Ec_no.Text + "，将设定成制二课不需要管理！", "提示信息", MessageBoxIcon.Information);
            //}
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
            //strMailto = "";
        }

        #region NetOperateNotes

        private void InsNetOperateNotes()
        {
            //发送邮件通知
            //Mailto();

            //新增日志
            string Newtext = Ec_issuedate.Text + "," + Ec_no.Text + "," + Ec_leader.SelectedItem.Text + "," + txtPbookdoc + "," + txtPpbookdoc + "," + "," + txtPjpbookdoc + ",管理区分" + Ec_distinction.SelectedItem.Text + "," + txtPdoc + ",物料管理：1,SOP确认：1";
            string OperateType = "新增";//操作标记
            string OperateNotes = "New技术* " + Newtext + " New*技术 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "设变管理", "设变新增", OperateNotes);
        }

        #endregion NetOperateNotes
    }
}