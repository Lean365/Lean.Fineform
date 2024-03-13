using FineUIPro;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.EC
{
    public partial class ec_qc : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEcIQCView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public String mysql, ecnno;
        public int selID;

        protected void Page_Load(object sender, EventArgs e)
        {
            //numBEbudgetmoney.Attributes.Add("Value", "0名");
            //numBEbudgetmoney.Attributes.Add("OnFocus", "if(this.value=='0') {this.value=''}");
            //numBEbudgetmoney.Attributes.Add("OnBlur", "if(this.value==''){this.value='0'}");
            if (!IsPostBack)
            {
                LoadData();

                //btnDel.OnClientClick = Grid2.GetNoSelectionAlertReference(" 请至少选择一项！") + GetDeleteScript(Grid2); // 删除选中行客户端脚本定义 .就这一句就可以了。（和官网的一至）
            }
        }

        private void LoadData()
        {
            //DateToDDL();

            // 权限检查
            //CheckPowerWithButton("CoreBudgetEdit", btnSave);
            //CheckPowerWithButton("CoreBusubjectDelete", btnDeleteSelected);

            //CheckPowerWithButton("CoreBudgetCheck", btnCheck);
            ////ResolveDeleteButtonForGrid(btnDeleteSelected, Grid2, "确定要删除选中的{0}项记录吗？");

            //CheckPowerWithButton("CoreBudgetunCheck", btnUncheck);
            //CheckPowerWithButton("CoreBudgetAdmit", btnAdmit);
            //btnAdmit.OnClientClick = Window1.GetShowReference("~/oneBudget/Expense/oneExpense_admit.aspx", "审核费用预算");
            //CheckPowerWithButton("CoreBudgetunAdmit", btnunAdmit);
            //btnunAdmit.OnClientClick = Window2.GetShowReference("~/oneBudget/Expense/oneExpense_admit.aspx", "弃审费用预算");
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;
            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            BindGrid1();

            // 默认选中第一个角色
            Grid2.SelectedRowIndex = 0;
            // 每页记录数
            Grid2.PageSize = ConfigHelper.PageSize;

            BindGrid2();
        }

        private void BindGrid1()
        {
            try
            {
                if (rbtnFirstAuto.Checked)
                {
                    //查询公式

                    //            var q = DB.Pp_Ecs.Select(E => new { ProvinceCode = E.Ec_iqcdate, E.Ec_newitem, E.Ec_no, E.Ec_olditem }).Distinct();

                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_newitem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
                            where !string.IsNullOrEmpty(b.Ec_purdate)// != "" || b.Ec_purdate != null
                            where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" || b.Ec_pmcdate != null
                            where string.IsNullOrEmpty(b.Ec_iqcdate)// == "" || b.Ec_iqcdate == null
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            where b.isCheck == "X"
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                b.isCheck,
                                a.Ec_distinction,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };

                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    //指定字段
                    //var ss=q.Select(E => new { Ec_no = E.Ec_no, Ec_issuedate= E.Ec_issuedate }).ToList().Distinct();

                    //不指定字段,去重复
                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}

                    Grid1.DataSource = qs;
                    Grid1.DataBind();
                    //var q = from n in db.NewsModel
                    //        join b in db.BigClassModel on n.BigClassID equals b.BigClassID
                    //        join s in db.SmallClassModel on n.SmallClassID equals s.SmallClassID
                    //        orderby n.AddTime descending
                    //        select new
                    //        {
                    //            n.NewsID,
                    //            n.BigClassID,
                    //            n.SmallClassID,
                    //            n.Title,
                    //            b.BigClassName,
                    //            s.SmallClassName,
                    //        };
                    //return q.ToList();
                    //q.Count()//这个就是记录总数

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "  SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] left join[OneHerba].[dbo].[ProSapMaterials] on D_SAP_ZCA1D_Z002 = Ec_newitem";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X or [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
                    ////if (GetIdentityName() != "admin")
                    ////{)
                    ////    q = q.Where(u => u.Name != "admin");
                    ////}

                    //// 过滤启用状态
                    ////if (rblEnableStatus.SelectedValue != "all")
                    ////{
                    ////    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    ////}

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    ////Grid1.RecordCount = q.Count();

                    //// 排列和数据库分页
                    ////q = SortAndPage<INVMB>(q, Grid1);

                    ////Grid1.DataSource = mydr;
                    ////Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
                }
                else if (rbtnSecondAuto.Checked)
                {
                    //查询公式

                    //            var q = DB.Pp_Ecs.Select(E => new { ProvinceCode = E.Ec_iqcdate, E.Ec_newitem, E.Ec_no, E.Ec_olditem }).Distinct();

                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_newitem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
                            where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" || b.Ec_iqcdate != null
                            where !string.IsNullOrEmpty(b.Ec_purdate)// != "" || b.Ec_purdate != null
                            where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" || b.Ec_pmcdate != null
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            where b.isCheck == "X"
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                b.isCheck,
                                a.Ec_distinction,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };

                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    //指定字段
                    //var ss=q.Select(E => new { Ec_no = E.Ec_no, Ec_issuedate= E.Ec_issuedate }).ToList().Distinct();

                    //不指定字段,去重复
                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}

                    Grid1.DataSource = qs;
                    Grid1.DataBind();
                    //var q = from n in db.NewsModel
                    //        join b in db.BigClassModel on n.BigClassID equals b.BigClassID
                    //        join s in db.SmallClassModel on n.SmallClassID equals s.SmallClassID
                    //        orderby n.AddTime descending
                    //        select new
                    //        {
                    //            n.NewsID,
                    //            n.BigClassID,
                    //            n.SmallClassID,
                    //            n.Title,
                    //            b.BigClassName,
                    //            s.SmallClassName,
                    //        };
                    //return q.ToList();
                    //q.Count()//这个就是记录总数

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "  SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] left join[OneHerba].[dbo].[ProSapMaterials] on D_SAP_ZCA1D_Z002 = Ec_newitem";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X or [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
                    ////if (GetIdentityName() != "admin")
                    ////{)
                    ////    q = q.Where(u => u.Name != "admin");
                    ////}

                    //// 过滤启用状态
                    ////if (rblEnableStatus.SelectedValue != "all")
                    ////{
                    ////    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    ////}

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    ////Grid1.RecordCount = q.Count();

                    //// 排列和数据库分页
                    ////q = SortAndPage<INVMB>(q, Grid1);

                    ////Grid1.DataSource = mydr;
                    ////Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
                }
                else if (rbtnThirdAuto.Checked)
                {
                    //查询公式

                    //            var q = DB.Pp_Ecs.Select(E => new { ProvinceCode = E.Ec_iqcdate, E.Ec_newitem, E.Ec_no, E.Ec_olditem }).Distinct();

                    var q = from a in DB.Pp_Ecs
                            join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                            //join c in DB.Pp_SapMaterials on b.Ec_newitem equals c.D_SAP_ZCA1D_Z002
                            where b.isDeleted == 0
                            //where a.Ec_distinction != 4
                            //where a.Remark.Contains("OK") == false
                            where b.isCheck == "X"
                            orderby b.Ec_entrydate descending
                            select new
                            {
                                a.Ec_no,
                                b.isCheck,
                                a.Ec_distinction,
                                b.Ec_model,
                                b.Ec_bomitem,
                                b.Ec_bomsubitem,
                                b.Ec_olditem,
                                b.Ec_newitem,
                                a.Ec_issuedate,
                            };

                    string searchText = ttbSearchEcnsub.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Ec_no.ToString().Contains(searchText) || u.Ec_model.ToString().Contains(searchText) || u.Ec_bomitem.ToString().Contains(searchText) || u.Ec_bomsubitem.ToString().Contains(searchText) || u.Ec_olditem.ToString().Contains(searchText) || u.Ec_newitem.ToString().Contains(searchText) || u.Ec_issuedate.ToString().Contains(searchText));
                    }

                    //指定字段
                    //var ss=q.Select(E => new { Ec_no = E.Ec_no, Ec_issuedate= E.Ec_issuedate }).ToList().Distinct();

                    //不指定字段,去重复
                    var qs = q.Select(E => new { E.Ec_no, E.Ec_issuedate }).ToList().Distinct();

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    //Grid1.RecordCount = GridHelper.GetTotalCount(q);

                    //if (Grid1.RecordCount != 0)
                    //{
                    //    // 排列和数据库分页
                    //    GridHelper.GetPagedDataTable(Grid1, q);
                    //}

                    Grid1.DataSource = qs;
                    Grid1.DataBind();
                    //var q = from n in db.NewsModel
                    //        join b in db.BigClassModel on n.BigClassID equals b.BigClassID
                    //        join s in db.SmallClassModel on n.SmallClassID equals s.SmallClassID
                    //        orderby n.AddTime descending
                    //        select new
                    //        {
                    //            n.NewsID,
                    //            n.BigClassID,
                    //            n.SmallClassID,
                    //            n.Title,
                    //            b.BigClassName,
                    //            s.SmallClassName,
                    //        };
                    //return q.ToList();
                    //q.Count()//这个就是记录总数

                    //IQueryable<INVMB> q = DBERP.INVMB; //.Include(u => u.Dept);
                    //mysql = "  SELECT DISTINCT ESUB.[Ec_no],[Ec_issuedate]   FROM [dbo].[Ec_Subs] ESUB LEFT JOIN [dbo].[Ec_s]ECN ON ECN.Ec_no=ESUB.[Ec_no] left join[OneHerba].[dbo].[ProSapMaterials] on D_SAP_ZCA1D_Z002 = Ec_newitem";
                    //// 在用户名称中搜索
                    //string searchText = ttbSearchEcn.Text.Trim();
                    //if (!String.IsNullOrEmpty(searchText))
                    //{
                    //    mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X or [Ec_issuedate] LIKE '%'+'" + searchText + "'+'%' OR ESUB.[Ec_no] LIKE '%'+'" + searchText + "'+'%' OR [Ec_model] LIKE  '%'+'" + searchText + "'+'%' OR [Ec_bomitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_bomsubitem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_olditem] LIKE '%'+'" + searchText + "'+'%' OR [Ec_newitem] LIKE '%'+'" + searchText + "'+'%' ";
                    //}
                    //mysql = mysql + "WHERE Ec_distinction<>3 and D_SAP_ZCA1D_Z019 = 'X ORDER BY [Ec_issuedate] DESC;";
                    //// 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    //// 2.获取当前分页数据
                    //DataTable table = GetPagedDataTable(Grid1.PageIndex, Grid1.PageSize);

                    //// 3.绑定到Grid
                    //Grid1.DataSource = table;
                    //Grid1.DataBind();
                    ////if (GetIdentityName() != "admin")
                    ////{)
                    ////    q = q.Where(u => u.Name != "admin");
                    ////}

                    //// 过滤启用状态
                    ////if (rblEnableStatus.SelectedValue != "all")
                    ////{
                    ////    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
                    ////}

                    //// 在查询添加之后，排序和分页之前获取总记录数
                    ////Grid1.RecordCount = q.Count();

                    //// 排列和数据库分页
                    ////q = SortAndPage<INVMB>(q, Grid1);

                    ////Grid1.DataSource = mydr;
                    ////Grid1.DataBind();

                    Grid1.SelectedRowIndex = 0;
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

        private void BindGrid2()
        {
            if (Grid1.SelectedRowIndex < 0)
            {
                Grid2.DataSource = "";

                Grid2.DataBind();
                return;
            }
            int classId = Convert.ToInt32(Grid1.DataKeys[Grid1.SelectedRowIndex][0]);

            int roleID = GetSelectedDataKeyID(Grid1);

            if (!string.IsNullOrEmpty(ecnno))
            {
                if (rbtnFirstAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              where a.Ec_no.Contains(ecnno)
                              where string.IsNullOrEmpty(b.Ec_iqcdate)// != "" || b.Ec_iqcdate != null
                              where !string.IsNullOrEmpty(b.Ec_purdate)// != "" || b.Ec_purdate != null
                              where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" || b.Ec_pmcdate != null'
                              where b.isCheck == "X"
                              where b.isDeleted == 0
                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  b.Ec_entrydate,
                                  Ec_iqcdate = b.Ec_iqcdate == null ? "" : b.Ec_iqcdate,
                                  b.Ec_newitem,
                                  a.Ec_no,
                                  b.Ec_olditem,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_iqcdate,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_entrydate,
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                else if (rbtnSecondAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              where a.Ec_no.Contains(ecnno)
                              where !string.IsNullOrEmpty(b.Ec_newitem)
                              where !string.IsNullOrEmpty(b.Ec_iqcdate)// != "" || b.Ec_iqcdate != null
                              where !string.IsNullOrEmpty(b.Ec_purdate)// != "" || b.Ec_purdate != null
                              where !string.IsNullOrEmpty(b.Ec_pmcdate)// != "" || b.Ec_pmcdate != null
                              where b.isCheck == "X"
                              where b.isDeleted == 0
                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  Ec_iqcdate = b.Ec_iqcdate == null ? "" : b.Ec_iqcdate,
                                  b.Ec_newitem,
                                  a.Ec_no,
                                  b.Ec_olditem,
                                  b.Ec_entrydate,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_iqcdate,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_entrydate,
                    }).Distinct();

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
                else if (rbtnThirdAuto.Checked)
                {
                    //查询LINQ去重复
                    var doc = from a in DB.Pp_Ecs
                              join b in DB.Pp_EcSubs on a.Ec_no equals b.Ec_no
                              where a.Ec_no.Contains(ecnno)
                              where b.isCheck == "X"
                              where b.isDeleted == 0

                              select new
                              {
                                  a.Ec_documents,
                                  a.Ec_letterno,
                                  a.Ec_letterdoc,
                                  a.Ec_eppletterno,
                                  a.Ec_eppletterdoc,
                                  a.Ec_teppletterno,
                                  a.Ec_teppletterdoc,
                                  b.Ec_iqcdate,
                                  b.Ec_newitem,
                                  a.Ec_no,
                                  b.Ec_olditem,
                                  b.Ec_entrydate,
                              };

                    //查询LINQ去重复
                    var q = doc.Select(E =>
                    new
                    {
                        E.Ec_documents,
                        E.Ec_letterno,
                        E.Ec_letterdoc,
                        E.Ec_eppletterno,
                        E.Ec_eppletterdoc,
                        E.Ec_teppletterno,
                        E.Ec_teppletterdoc,
                        E.Ec_iqcdate,
                        E.Ec_newitem,
                        E.Ec_no,
                        E.Ec_olditem,
                        E.Ec_entrydate,
                    }).Distinct();
                    //q = q.Where(u => u.Ec_no.Contains(ecnno) && u.Ec_newitem != "--" && u.Ec_newitem != "0" && u.Ec_iqcdate == "");

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid2.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid2.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid2, q);

                        Grid2.DataSource = table;
                        Grid2.DataBind();
                    }
                    else
                    {
                        Grid2.DataSource = "";
                        Grid2.DataBind();
                    }
                }
            }
            else
            {
                Grid2.DataSource = "";

                Grid2.DataBind();
            }
            //ecnno = "";
        }

        #endregion Page_Load

        #region Events

        protected void ttbSearchEcnsub_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.ShowTrigger1 = true;
            BindGrid1();
            BindGrid2();
        }

        protected void ttbSearchEcnsub_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchEcnsub.Text = String.Empty;
            ttbSearchEcnsub.ShowTrigger1 = false;
            BindGrid1();
            BindGrid2();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int pageindex1 = Grid1.PageIndex;
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid1.PageIndex > Grid1.PageCount - 1)
            {
                Grid1.PageIndex = Grid1.PageCount - 1;
            }

            BindGrid1();
            int pageindex2 = Grid1.PageIndex;
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            // 更改每页显示数目时，防止 PageIndex 越界
            if (Grid2.PageIndex > Grid2.PageCount - 1)
            {
                Grid2.PageIndex = Grid2.PageCount - 1;
            }

            BindGrid2();
        }

        #endregion Events

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
        }

        protected void Grid1_RowClick(object sender, GridRowClickEventArgs e)
        {
            object[] keys = Grid1.DataKeys[e.RowIndex];
            ecnno = keys[1].ToString();

            BindGrid2();
        }

        #endregion Grid1 Events

        #region Grid2 Events

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithLinkButtonField("CoreBudgetDelete", Grid2, "deleteField");

            // 设置LinkButtonField的点击客户端事件
            //LinkButtonField deleteField = Grid1.FindColumn("Delete") as LinkButtonField;
            //deleteField.OnClientClick = GetDeleteScript(Grid1);
            CheckPowerWithLinkButtonField("CoreEcIQCEdit", Grid2, "editField");
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] keys = Grid2.DataKeys[e.RowIndex];
            //ID,Ec_no,Ec_newitem
            //selID = Convert.ToInt32(keys[0].ToString());
            string ss = ',' + keys[1].ToString() + ',' + keys[2].ToString();
            PageContext.RegisterStartupScript(Window3.GetShowReference("~/Lf_Manufacturing/EC/ec_qc_edit.aspx?Ec_no=" + ss + "&type=1") + Window3.GetMaximizeReference());
            //选中ID
            int DelID = Convert.ToInt32(Grid2.DataKeys[e.RowIndex][0]);//GetSelectedDataKeyID(Grid2);

            BindGrid2();
        }

        protected void Grid2_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string checkedValue = String.Empty;
            if (rbtnFirstAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid1();
                BindGrid2();
            }
            ecnno = "";
        }

        #endregion Grid2 Events

        protected void Window3_Close(object sender, EventArgs e)
        {
            BindGrid1();
            BindGrid2();
        }
    }
}