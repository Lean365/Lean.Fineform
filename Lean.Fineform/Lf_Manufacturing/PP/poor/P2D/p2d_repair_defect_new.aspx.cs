using FineUIPro;
using LeanFine.Lf_Business.Helper;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.PP.poor.P2D
{
    public partial class p2d_repair_defect_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DManuDefectNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable DefDatatable;

        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;

        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string Prodate, Promodel, Proorder, Prolot, Proorderqty, Propcbtype, Prorealqty, Prolinename, Propcbcardno, Prodefectsymptom, Propcbcheckout, Prodefectcause, Probadqty, Probadtotal, Probadresponsibility, Prodefectnature, Probadserial, Prorepairman;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
                //this.Defectguid.Text = Guid.NewGuid().ToString();
            }
        }

        private void LoadData()
        {
            userid = GetIdentityName();

            //linq查询空表没法显示字段名,只能查询非空表
            //IQueryable<pp_defect_P2d> q = DB.Pp_P2d_Defects;
            //DefDatatable = CopyToDataTable(q);

            //获取SQL数据表
            DefDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_P2d_Manufacturing_Defect");

            //DefDatatable = ConvertHelper.LinqConvertToDataTable(q);

            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 新增数据初始值
            JObject defaultObj = new JObject();
            defaultObj.Add("Propcbtype", "A");
            defaultObj.Add("Prolinename", "修正");
            defaultObj.Add("Probadqty", "0");
            defaultObj.Add("Prorepairman", "黄儒钦");
            //defaultObj.Add("Name", "用户名");
            //defaultObj.Add("Gender", "1");
            //defaultObj.Add("EntranceYear", "2015");
            //defaultObj.Add("EntranceDate", "2015-09-01");
            //defaultObj.Add("AtSchool", false);
            //defaultObj.Add("Major", "化学系");
            //defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

            // 在第一行新增一条数据
            btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

            // 重置表格
            btnReset.OnClientClick = Grid1.GetRejectChangesReference();

            // 删除选中行按钮
            //btnDel.OnClientClick = Grid1.GetNoSelectionAlertReference("请至少选择一项！");
            //this.prodate.SelectedDate = DateTime.Now;

            dpProdate.SelectedDate = DateTime.Now.AddDays(-1);

            BindDdlproOrder();
            //BindDdlLine();

            BindDdlCheckout();
            BindDdlBadprop();
            BindDdlBadresponsibility();
            BindDdlddlBadProrepairmanp();
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.选择项中没有的选项请联系电脑课添加</div>");
        }

        private void BindGrid()
        {
            IQueryable<Pp_P2d_Manufacturing_Defect> q = DB.Pp_P2d_Manufacturing_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.dpProdate.SelectedDate.Value.ToString("yyyyMMdd");
            //q = q.Where(u => u.IsDeleted == 0 && u.Prolinename.Contains(prolinename.SelectedItem.Text) && u.Proorder.Contains(proorder.SelectedItem.Text) && u.Prodate.Contains(ddate));

            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}

            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Pp_P2d_Manufacturing_Defect>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
        }

        #endregion Page_Load

        #region BindDdl Dropdown ListData

        //A:PCB个所,B:责任单位,C:不良性质,E:板别,D:组立个所,F:PCBA,G:修理员,H:検査員,J:VC线别,K:检查状况,L:检出工程,M:目视别,P:未达成,S:停线
        //查询LOT
        private void BindDdlproOrder()
        {
            //var a = from p in DB.Pp_P2d_Outputs
            //            //where string.IsNullOrEmpty( p.Porderlot)
            //        select p.Proorder;
            var q = from e in DB.Pp_Orders

                        //where a.Contains(e.Porderno)
                    select new
                    {
                        e.Porderno
                    };

            var qs = q.Select(E => new { E.Porderno }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProorder.DataSource = qs;
            ddlProorder.DataTextField = "Porderno";
            ddlProorder.DataValueField = "Porderno";
            ddlProorder.DataBind();
            this.ddlProorder.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //查询班组
        //private void BindDdlLine()

        //{
        //    if (dpProdate.SelectedDate.HasValue)
        //    {
        //        Prodate = dpProdate.SelectedDate.Value.ToString("yyyyMMdd");

        //        //查询LINQ去重复
        //        var q = from a in DB.Pp_P2d_OutputSubs
        //                where a.Proorder.Contains(proorder.SelectedItem.Text)
        //                //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
        //                //where b.Proecnno == strecn
        //                //where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P2d_Defects
        //                //                                       where d.IsDeleted == 0
        //                //                                       where d.Prodate == Prodate
        //                //                                       where d.Prolinename == a.Prolinename
        //                //                                       select d.Prolot)
        //                //            .Contains(a.Prolot)//投入日期
        //                select new
        //                {
        //                    a.Prolinename
        //                };
        //        //var q = from a in DB.Pp_Lines
        //        //        where a.lineclass.CompareTo("P") == 0
        //        //        select new
        //        //        {
        //        //            Prolinename=a.linename
        //        //        };

        //        var qs = q.Select(E => new { E.Prolinename }).ToList().Distinct();
        //        //var list = (from c in DB.ProSapPorders
        //        //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
        //        //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
        //        //3.2.将数据绑定到下拉框
        //        ddlProlinename.DataSource = qs;
        //        ddlProlinename.DataTextField = "Prolinename";
        //        ddlProlinename.DataValueField = "Prolinename";
        //        ddlProlinename.DataBind();
        //        this.ddlProlinename.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        //    }
        //}

        //  不良种类
        //private void BindDdlDept()
        //{
        //    //查询LINQ去重复
        //    var q = from a in DB.Pp_Reasons
        //            where a.Reasontype == "D"
        //            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
        //            //where b.Proecnno == strecn
        //            //where a.Prolineclass == "M"
        //            select new
        //            {
        //                a.GUID,
        //                a.Reasoncntext

        //            };

        //    var qs = q.Select(E => new { E.GUID, E.Reasoncntext }).ToList().Distinct();
        //    //var list = (from c in DB.ProSapPorders
        //    //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
        //    //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
        //    //3.2.将数据绑定到下拉框
        //    ddlProdefectcategory.DataSource = qs;
        //    ddlProdefectcategory.DataTextField = "Reasoncntext";
        //    ddlProdefectcategory.DataValueField = "Reasoncntext";
        //    ddlProdefectcategory.DataBind();

        //}
        //板别
        private void BindDdlPcbType()
        {
            if (ddlProorder.SelectedIndex != -1 && ddlProorder.SelectedIndex != 0)
            {
                //查询LINQ去重复
                var q = from a in DB.Pp_P2d_OutputSubs
                            //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                            //where b.Proecnno == strecn
                            //where b.Proecnbomitem == stritem
                        where a.Proorder.Contains(ddlProorder.SelectedText)
                        select new
                        {
                            DictLabel = a.Propcbatype,
                            DictValue = a.Propcbatype
                        };

                var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                if (qs.Any())
                {
                    //3.2.将数据绑定到下拉框
                    ddlPropcbtype.DataSource = qs;
                    ddlPropcbtype.DataTextField = "DictLabel";
                    ddlPropcbtype.DataValueField = "DictValue";
                    ddlPropcbtype.DataBind();
                    // 选中根节点
                    this.ddlPropcbtype.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
                }
                else
                {
                    //查询LINQ去重复
                    var qd = from a in DB.Adm_Dicts
                                 //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                                 //where b.Proecnno == strecn
                                 //where b.Proecnbomitem == stritem
                             where a.DictType.Contains("reason_type_f")
                             select new
                             {
                                 a.DictLabel,
                                 a.DictValue
                             };

                    var qds = qd.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
                    //3.2.将数据绑定到下拉框
                    ddlPropcbtype.DataSource = qds;
                    ddlPropcbtype.DataTextField = "DictLabel";
                    ddlPropcbtype.DataValueField = "DictValue";
                    ddlPropcbtype.DataBind();
                    // 选中根节点
                    this.ddlPropcbtype.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
                }
            }
            else
            {
                Alert.ShowInTop("请选择生产订单！", MessageBoxIcon.Information);
                return;
            }
        }

        //检出工程
        private void BindDdlCheckout()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_l")
                    select new
                    {
                        a.DictLabel,
                        a.DictValue
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPropcbcheckout.DataSource = qs;
            ddlPropcbcheckout.DataTextField = "DictLabel";
            ddlPropcbcheckout.DataValueField = "DictValue";
            ddlPropcbcheckout.DataBind();
            // 选中根节点
            this.ddlPropcbcheckout.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //不良性质
        private void BindDdlBadprop()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_c")
                    select new
                    {
                        a.DictLabel,
                        a.DictValue
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProdefectnature.DataSource = qs;
            ddlProdefectnature.DataTextField = "DictLabel";
            ddlProdefectnature.DataValueField = "DictValue";
            ddlProdefectnature.DataBind();
            // 选中根节点
            this.ddlProdefectnature.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //归属
        private void BindDdlBadresponsibility()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_b")
                    select new
                    {
                        a.DictLabel,
                        a.DictValue
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProbadresponsibility.DataSource = qs;
            ddlProbadresponsibility.DataTextField = "DictLabel";
            ddlProbadresponsibility.DataValueField = "DictValue";
            ddlProbadresponsibility.DataBind();
            // 选中根节点
            this.ddlProbadresponsibility.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //修理
        private void BindDdlddlBadProrepairmanp()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_h")
                    select new
                    {
                        a.DictLabel,
                        a.DictValue
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProrepairman.DataSource = qs;
            ddlProrepairman.DataTextField = "DictLabel";
            ddlProrepairman.DataValueField = "DictValue";
            ddlProrepairman.DataBind();
            // 选中根节点
            this.ddlProrepairman.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion BindDdl Dropdown ListData

        #region Events

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //获取ID号
            int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

            //int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreDefectDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P2d_Defect current = DB.Pp_P2d_Defects.Find(del_ID);
                //删除日志
                string Contectext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Promodel + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Prodefectsymptom;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                //删除记录
                //DB.Pp_P2d_Defects.Where(l => l.ID == del_ID).Delete();

                current.IsDeleted = 1;
                DB.SaveChanges();
                //重新绑定
                BindGrid();
            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreDefectDelete", Grid1, "deleteField");
            // 设置LinkButtonField的点击客户端事件
        }
        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        public static string lclass, nclass, ncode, ConnStr;

        protected void dpProdate_TextChanged(object sender, EventArgs e)
        {
            //BindDdlLine();
        }

        #endregion Events

        protected void ddlProorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProorder.SelectedIndex != -1 && ddlProorder.SelectedIndex != 0)
            {
                this.lblProlot.Text = ddlProorder.SelectedItem.Text;
                string sorder = ddlProorder.SelectedItem.Text;

                var q = from a in DB.Pp_P2d_OutputSubs
                        where a.Proorder.CompareTo(sorder) == 0
                        select a;
                var qs = q.ToList();
                if (qs.Any())
                {
                    //this.proorder.Text = sorder;
                    this.lblProorderqty.Text = qs[0].Proorderqty.ToString();// proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1);

                    string prohbn = qs[0].Prohbn.ToString();

                    var q_model = from a in DB.Pp_Manhours
                                  where a.Proitem.CompareTo(prohbn) == 0
                                  select a;
                    var q_mname = q_model.ToList();
                    if (q_mname.Any())
                    {
                        this.lblPromodel.Text = q_mname[0].Promodel.ToString();
                    }
                }
                else
                {
                    this.lblProorderqty.Text = "";
                    this.lblPromodel.Text = "";
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.ShowInTop("请先添加生产记录！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                //BindDdlDept();
                //BindDdlLine();
                BindDdlPcbType();
                BindDdlCheckout();
                BindDdlBadprop();
                BindDdlBadresponsibility();
                BindDdlddlBadProrepairmanp();
            }
        }

        //判断操作类型

        private void EditDefectDataRow()
        {
            // 删除现有数据
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                delrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Pp_P2d_Manufacturing_Defect item = DB.Pp_P2d_Manufacturing_Defects
                .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Propcbtype + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Prodefectsymptom;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Contectext + " *Del 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                item.IsDeleted = 1;
                DB.SaveChanges();

                //重新绑定
                BindGrid();

                //DeleteRowByID(delrowID);
            }

            // 修改的现有数据
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                editrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                DataRow row = FindRowByID(editrowID);

                UpdateDefectDataRow(modifiedDict[rowIndex], row);
            }

            // 新增数据
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

            //mysql = "select * from [dbo].[Pp_P2d_Defects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            if (AppendToEnd)
            {
                if (newAddedList.Count != 0)
                {
                    for (int i = 0; i < newAddedList.Count; i++)
                    {
                        DataRow rowData = CreateNewData(DefDatatable, newAddedList[i]);

                        DefDatatable.Rows.Add(rowData);

                        //table.Rows.Add(rowData);
                        //DataRow rowData = CreateNewData(table, newAddedList[i]);

                        //Get the DataTable of a DataRow
                        DataTable tb = rowData.Table.NewRow().Table;

                        foreach (DataColumn col in tb.Columns)
                        {
                            //最后一条记录
                            tb.Rows[tb.Rows.Count - 1]["ID"].ToString();
                            //第一条记录
                            tb.Rows[0]["ID"].ToString();
                            Prodate = tb.Rows[tb.Rows.Count - 1]["Prodate"].ToString();//生产日期
                            Promodel = tb.Rows[tb.Rows.Count - 1]["Promodel"].ToString();//机种
                            Proorder = tb.Rows[tb.Rows.Count - 1]["Proorder"].ToString();//订单
                            Prolot = tb.Rows[tb.Rows.Count - 1]["Prolot"].ToString();//批次
                            Proorderqty = tb.Rows[tb.Rows.Count - 1]["Proorderqty"].ToString();//订单数量
                            Propcbtype = tb.Rows[tb.Rows.Count - 1]["Propcbtype"].ToString();//板别
                            Prorealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();//生产实绩
                            Prolinename = tb.Rows[tb.Rows.Count - 1]["Prolinename"].ToString();//班组

                            Propcbcardno = tb.Rows[tb.Rows.Count - 1]["Propcbcardno"].ToString();//卡号
                            Prodefectsymptom = tb.Rows[tb.Rows.Count - 1]["Prodefectsymptom"].ToString();//不良症状
                            //Propcbchecktype = tb.Rows[tb.Rows.Count - 1]["Propcbchecktype"].ToString();//检出工程
                            Propcbcheckout = tb.Rows[tb.Rows.Count - 1]["Propcbcheckout"].ToString();//检出工程
                            Prodefectcause = tb.Rows[tb.Rows.Count - 1]["Prodefectcause"].ToString();//不良原因
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();//不良数量
                            Probadresponsibility = tb.Rows[tb.Rows.Count - 1]["Probadresponsibility"].ToString();//责任归属
                            Prodefectnature = tb.Rows[tb.Rows.Count - 1]["Prodefectnature"].ToString();//不良性质
                            //Probadtotal = tb.Rows[tb.Rows.Count - 1]["Probadtotal"].ToString();//不良台数
                            Prorepairman = tb.Rows[tb.Rows.Count - 1]["Prorepairman"].ToString();//修理
                        }
                        Pp_P2d_Manufacturing_Defect item = new Pp_P2d_Manufacturing_Defect();

                        item.Prodate = Prodate;
                        item.Promodel = Promodel;
                        item.Proorder = Proorder;
                        item.Prolot = Prolot;
                        item.Proorderqty = int.Parse(Proorderqty);
                        item.Propcbtype = Propcbtype;
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Prolinename = Prolinename;
                        item.Propcbcardno = Propcbcardno;
                        item.Prodefectsymptom = Prodefectsymptom;
                        item.Propcbcheckout = Propcbcheckout;
                        item.Prodefectcause = Prodefectcause;
                        item.Probadqty = int.Parse(Probadqty);
                        item.Probadtotal = 0;
                        item.Probadresponsibility = Probadresponsibility;
                        item.Prodefectnature = Prodefectnature;
                        item.Probadserial = Probadserial;
                        item.Prorepairman = Prorepairman;
                        item.IsDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Manufacturing_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Prodefectsymptom;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良* " + Contectext + "*New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }
                //else
                //{
                //    pp_defect_P2d item = new pp_defect_P2d();

                //    item.Prolot = this.prolot.Text;
                //    //班组

                //    proLine cline = DB.proLines
                //        .Where(u => u.linename == this.prolinename.SelectedItem.Text).FirstOrDefault();

                //    item.Prodefectcategory = Prodefectcategory;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.SelectedItem.Text;

                //    item.Prodate = prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prodefectcategory = "OK";
                //    //种类
                //    pp_defect_P2dcode cclass = DB.pp_defect_P2dcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    pp_defect_P2dcode ccode = DB.pp_defect_P2dcodes
                //        .Where(u => u.cn_ngmatter == "OK").FirstOrDefault();

                //    item.Prongcode = ccode.ngcode;

                //    item.Prongmatter = "OK";
                //    item.Probadqty = 0;

                //    item.Probadtotal = 0;
                //    item.Prodefectsymptom = "OK";
                //    item.Prodefectcause = "OK";
                //    item.Prongbdel = false;
                //    item.Remark = "";
                //    item.Defectguid = Guid.NewGuid().ToString();

                //    item.CreateDate = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_P2d_Defects.Add(item);
                //    DB.SaveChanges();

                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Prodefectsymptom;
                //    string NewOperateType = item.Defectguid;
                //    string OperateNotes = "New* " + NewText + " New* 的记录已经将新增";
                //    NetCountHelper.InsNetOperateNotes(userid, NewOperateType, "不具合管理", "不具合新增", OperateNotes);
                //}
            }
            else
            {
                if (newAddedList.Count != 0)
                {
                    for (int i = newAddedList.Count - 1; i >= 0; i--)
                    {
                        //遍历枚举类型Sample的各个枚举名称

                        DataRow rowData = CreateNewData(DefDatatable, newAddedList[i]);

                        DefDatatable.Rows.InsertAt(rowData, 0);
                        //DefDatatable.Rows.InsertAt(rowData, 0);

                        //table.Rows.Add(rowData);
                        //DataRow rowData = CreateNewData(table, newAddedList[i]);

                        //Get the DataTable of a DataRow
                        DataTable tb = rowData.Table.NewRow().Table;

                        foreach (DataColumn col in tb.Columns)
                        {
                            //第一条记录
                            tb.Rows[0]["ID"].ToString();
                            Prodate = tb.Rows[0]["Prodate"].ToString();//生产日期
                            Promodel = tb.Rows[0]["Promodel"].ToString();//机种
                            Proorder = tb.Rows[0]["Proorder"].ToString();//订单
                            Prolot = tb.Rows[0]["Prolot"].ToString();//批次
                            Proorderqty = tb.Rows[0]["Proorderqty"].ToString();//订单数量
                            Propcbtype = tb.Rows[0]["Propcbtype"].ToString();//板别
                            Prorealqty = tb.Rows[0]["Prorealqty"].ToString();//生产实绩
                            Prolinename = tb.Rows[0]["Prolinename"].ToString();//班组

                            Propcbcardno = tb.Rows[0]["Propcbcardno"].ToString();//卡号
                            Prodefectsymptom = tb.Rows[0]["Prodefectsymptom"].ToString();//不良症状
                            //Propcbchecktype = tb.Rows[0]["Propcbchecktype"].ToString();//检出工程
                            Propcbcheckout = tb.Rows[0]["Propcbcheckout"].ToString();//检出工程
                            Prodefectcause = tb.Rows[0]["Prodefectcause"].ToString();//不良原因
                            Probadqty = tb.Rows[0]["Probadqty"].ToString();//不良数量
                            Probadresponsibility = tb.Rows[0]["Probadresponsibility"].ToString();//责任归属
                            Prodefectnature = tb.Rows[0]["Prodefectnature"].ToString();//不良性质
                            //Probadtotal = tb.Rows[0]["Probadtotal"].ToString();//不良台数
                            Prorepairman = tb.Rows[0]["Prorepairman"].ToString();//修理
                        }
                        Pp_P2d_Manufacturing_Defect item = new Pp_P2d_Manufacturing_Defect();

                        item.Prodate = Prodate;
                        item.Promodel = Promodel;
                        item.Proorder = Proorder;
                        item.Prolot = Prolot;
                        item.Proorderqty = int.Parse(Proorderqty);
                        item.Propcbtype = Propcbtype;
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Prolinename = Prolinename;
                        item.Propcbcardno = Propcbcardno;
                        item.Prodefectsymptom = Prodefectsymptom;
                        item.Propcbcheckout = Propcbcheckout;
                        item.Prodefectcause = Prodefectcause;
                        item.Probadqty = int.Parse(Probadqty);
                        item.Probadtotal = 0;
                        item.Probadresponsibility = Probadresponsibility;
                        item.Prodefectnature = Prodefectnature;
                        item.Probadserial = Probadserial;
                        item.Prorepairman = Prorepairman;
                        item.IsDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Manufacturing_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Prodefectsymptom;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良* " + Contectext + "*New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }

                //else
                //{
                //    pp_defect_P2d item = new pp_defect_P2d();

                //    item.Prolot = this.prolot.Text;
                //    //班组

                //    proLine cline = DB.proLines
                //        .Where(u => u.linename == this.prolinename.SelectedItem.Text).FirstOrDefault();

                //    item.Prodefectcategory = Prodefectcategory;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.SelectedItem.Text;
                //    item.Prodate = this.prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prodefectcategory = "OK";
                //    //种类
                //    pp_defect_P2dcode cclass = DB.pp_defect_P2dcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    pp_defect_P2dcode ccode = DB.pp_defect_P2dcodes
                //        .Where(u => u.cn_ngmatter == "OK").FirstOrDefault();

                //    item.Prongcode = ccode.ngcode;

                //    item.Prongmatter = "OK";
                //    item.Probadqty = 0;

                //    item.Probadtotal = 0;
                //    item.Prodefectsymptom = "OK";
                //    item.Prodefectcause = "OK";
                //    item.Prongbdel = false;
                //    item.Remark = "";
                //    item.Defectguid = Guid.NewGuid().ToString();

                //    item.CreateDate = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_P2d_Defects.Add(item);
                //    DB.SaveChanges();

                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Prodefectsymptom;
                //    string NewOperateType = item.Defectguid;
                //    string OperateNotes = "New * " + NewText + " New* 的记录已经将新增";
                //    NetCountHelper.InsNetOperateNotes(userid, NewOperateType, "不具合管理", "不具合新增", OperateNotes);

                //}
            }

            //labResult.Text = String.Format("用户修改的数据：<pre>{0}</pre>", Grid1.GetModifiedData().ToString(Newtonsoft.Json.Formatting.Indented));

            BindGrid();

            //Alert.ShowInTop("数据保存成功！（表格数据已重新绑定）"+ Grid1.GetModifiedData().ToString(Newtonsoft.Json.Formatting.Indented));
        }

        //新增行内容补充
        private DataRow CreateNewData(DataTable table, Dictionary<string, object> newAddedData)
        {
            DataRow rowData = table.NewRow();

            // 设置行ID（模拟数据库的自增长列）
            rowData["ID"] = GetNextRowID();

            rowData["GUID"] = new Guid();
            rowData["Prodate"] = dpProdate.SelectedDate.Value.ToString("yyyyMMdd");
            rowData["Promodel"] = lblPromodel.Text;
            rowData["Proorder"] = ddlProorder.SelectedText;
            rowData["Prolot"] = lblProlot.Text;
            rowData["Proorderqty"] = (int)decimal.Parse(lblProorderqty.Text);
            rowData["Probadqty"] = 0;
            rowData["Prorealqty"] = 0;
            rowData["Probadtotal"] = 0;
            rowData["UDF01"] = "";
            rowData["UDF02"] = "";
            rowData["UDF03"] = "";
            rowData["UDF04"] = "";
            rowData["UDF05"] = "";
            rowData["UDF06"] = "";
            rowData["UDF51"] = 0;
            rowData["UDF52"] = 0;
            rowData["UDF53"] = 0;
            rowData["UDF54"] = 0;
            rowData["UDF55"] = 0;
            rowData["UDF56"] = 0;
            rowData["IsDeleted"] = 0;
            rowData["Creator"] = GetIdentityName();
            rowData["CreateDate"] = DateTime.Now;
            InsertDefectDataRow(newAddedData, rowData);
            return rowData;
        }

        //修改内容提交
        private static void UpdateDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            Pp_P2d_Manufacturing_Defect item = DB.Pp_P2d_Manufacturing_Defects

                .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改前日志
            string BeforeContectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Prodefectsymptom;
            string BeforeOperateType = "修改";
            string BeforeOperateNotes = "beEdit生产不良* " + BeforeContectext + " *beEdit生产不良 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, BeforeOperateType, "不具合管理", "不具合修改", BeforeOperateNotes);

            // 板别
            if (rowDict.ContainsKey("Propcbtype"))
            {
                rowData["Propcbtype"] = rowDict["Propcbtype"];
                if (string.IsNullOrEmpty(rowData["Propcbtype"].ToString()))
                {
                    Alert.ShowInTop("板别不能为空！");
                    return;
                }
                else
                {
                    item.Propcbtype = rowData["Propcbtype"].ToString();
                    //RealQty = rowData["Proclassmatter"].ToString();
                    //
                    //OrderFinish();
                }
            }
            // 实绩
            if (rowDict.ContainsKey("Prorealqty"))
            {
                rowData["Prorealqty"] = rowDict["Prorealqty"];
                if (string.IsNullOrEmpty(rowData["Prorealqty"].ToString()))
                {
                    Alert.ShowInTop("实绩不能为空！");
                    return;
                }
                else
                {
                    item.Prorealqty = int.Parse(rowData["Prorealqty"].ToString());
                    //RealQty = rowData["Proclassmatter"].ToString();
                    //
                    //OrderFinish();
                }
            }
            // 班别
            if (rowDict.ContainsKey("Prolinename"))
            {
                rowData["Prolinename"] = rowDict["Prolinename"];

                if (string.IsNullOrEmpty(rowData["Prolinename"].ToString()))
                {
                    Alert.ShowInTop("班别不能为空！");
                    return;
                }
                else
                {
                    item.Prolinename = rowData["Prolinename"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            // 卡号
            if (rowDict.ContainsKey("Propcbcardno"))
            {
                rowData["Propcbcardno"] = rowDict["Propcbcardno"];
                if (string.IsNullOrEmpty(rowData["Propcbcardno"].ToString()))
                {
                    item.Propcbcardno = "";
                    //Alert.ShowInTop("卡号不能为空！");
                    //return;
                }
                else
                {
                    item.Propcbcardno = rowData["Propcbcardno"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            // 不良症状
            if (rowDict.ContainsKey("Prodefectsymptom"))
            {
                rowData["Prodefectsymptom"] = rowDict["Prodefectsymptom"];
                if (string.IsNullOrEmpty(rowData["Prodefectsymptom"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("不良症状不能为空！");
                    return;
                }
                else
                {
                    item.Prodefectsymptom = rowData["Prodefectsymptom"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            // 检出工程
            if (rowDict.ContainsKey("Propcbcheckout"))
            {
                rowData["Propcbcheckout"] = rowDict["Propcbcheckout"];
                if (string.IsNullOrEmpty(rowData["Propcbcheckout"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("检出工程不能为空！");
                    return;
                }
                else
                {
                    item.Propcbcheckout = rowData["Propcbcheckout"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            // 原因
            if (rowDict.ContainsKey("Prodefectcause"))
            {
                rowData["Prodefectcause"] = rowDict["Prodefectcause"];
                if (string.IsNullOrEmpty(rowData["Prodefectcause"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("原因不能为空！");
                    return;
                }
                else
                {
                    item.Prodefectcause = rowData["Prodefectcause"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            // 不良数量
            if (rowDict.ContainsKey("Probadqty"))
            {
                rowData["Probadqty"] = rowDict["Probadqty"];

                if (string.IsNullOrEmpty(rowData["Probadqty"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("不良数量不能为空！");
                    return;
                }
                else
                {
                    item.Probadqty = int.Parse(rowData["Probadqty"].ToString());

                    //StopMinute = rowData["Probadqty"].ToString();
                }
            }
            // 归属
            if (rowDict.ContainsKey("Probadresponsibility"))
            {
                rowData["Probadresponsibility"] = rowDict["Probadresponsibility"];
                if (string.IsNullOrEmpty(rowData["Probadresponsibility"].ToString()))
                {
                    Alert.ShowInTop("责任单位归属不能为空！");
                    return;
                }
                else
                {
                    item.Probadresponsibility = rowData["Probadresponsibility"].ToString();
                    //StopText = rowData["Probadtotal"].ToString();
                }
            }
            // 性质
            if (rowDict.ContainsKey("Prodefectnature"))
            {
                rowData["Prodefectnature"] = rowDict["Prodefectnature"];
                if (string.IsNullOrEmpty(rowData["Prodefectnature"].ToString()))
                {
                    Alert.ShowInTop("性质不能为空！");
                    return;
                }
                else
                {
                    item.Prodefectnature = rowData["Prodefectnature"].ToString();
                }
                //ResonText = rowData["Prodefectsymptom"].ToString();
            }

            //修理
            if (rowDict.ContainsKey("Prorepairman"))
            {
                rowData["Prorepairman"] = rowDict["Prorepairman"];
                if (string.IsNullOrEmpty(rowData["Prorepairman"].ToString()))
                {
                    Alert.ShowInTop("修理不能为空！");
                    return;
                }
                else
                {
                    item.Prorepairman = rowData["Prorepairman"].ToString();
                }
                //ResonText = rowData["Prodefectsymptom"].ToString();
            }
            item.ModifyDate = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Pp_P2d_Manufacturing_Defect edititem = DB.Pp_P2d_Manufacturing_Defects

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterContectext = edititem.ID + "," + edititem.Prolinename + "," + edititem.Prolot + "," + edititem.Prodate + "," + edititem.Prorealqty + "," + edititem.Promodel + "," + edititem.Probadqty + "," + edititem.Probadtotal + "," + edititem.Prodefectsymptom;
            string AfterOperateType = "修改";
            string AfterOperateNotes = "afEdit生产不良* " + AfterContectext + " *afEdit生产不良 的记录已经将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, AfterOperateType, "不具合管理", "不具合修改", AfterOperateNotes);
        }

        //获取新增行内容
        private void InsertDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            UpdateDataRow("Propcbtype", rowDict, rowData);
            UpdateDataRow("Prorealqty", rowDict, rowData);
            UpdateDataRow("Prolinename", rowDict, rowData);
            UpdateDataRow("Propcbcardno", rowDict, rowData);
            UpdateDataRow("Prodefectsymptom", rowDict, rowData);
            UpdateDataRow("Propcbcheckout", rowDict, rowData);
            UpdateDataRow("Prodefectcause", rowDict, rowData);
            UpdateDataRow("Probadqty", rowDict, rowData);

            UpdateDataRow("Probadresponsibility", rowDict, rowData);
            UpdateDataRow("Prodefectnature", rowDict, rowData);
            UpdateDataRow("Probadserial", rowDict, rowData);
            UpdateDataRow("Prorepairman", rowDict, rowData);
        }

        //根据字段获取信息
        private void UpdateDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }

        // 根据行ID来获取行数据
        private DataRow FindRowByID(int rowID)
        {
            //mysql = "select* from[dbo].[Pp_P2d_Defects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            foreach (DataRow row in DefDatatable.Rows)
            {
                if (Convert.ToInt32(row["Id"]) == rowID)
                {
                    return row;
                }
            }
            return null;
        }

        // 根据行ID来删除行数据
        private void DeleteRowByID(int rowID)
        {
            //mysql = "select* from[dbo].[Pp_P2d_Defects];";
            //DataTable table = GetDataTable.Getdt(mysql);

            DataRow found = FindRowByID(rowID);
            if (found != null)
            {
                DefDatatable.Rows.Remove(found);
            }
        }

        // 模拟数据库的自增长列
        private int GetNextRowID()
        {
            int maxID = 0;
            //mysql = "select * from [dbo].[Pp_P2d_Defects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            foreach (DataRow row in DefDatatable.Rows)
            {
                int currentRowID = Convert.ToInt32(row["Id"]);
                if (currentRowID > maxID)
                {
                    maxID = currentRowID;
                }
            }
            return maxID + 1;
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlPropcbtype.SelectedIndex == 0 || ddlPropcbtype.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择板别！", MessageBoxIcon.Information);
                    return;
                }
                if (ddlPropcbcheckout.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择检出工程！", MessageBoxIcon.Information);
                    return;
                }
                if (ddlProbadresponsibility.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择责任归属！", MessageBoxIcon.Information);
                    return;
                }
                if (ddlProdefectnature.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择不良性质！", MessageBoxIcon.Information);
                    return;
                }
                if (ddlProrepairman.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择修理人员！", MessageBoxIcon.Information);
                    return;
                }

                //保存不具合数据
                EditDefectDataRow();
                //Alert.ShowInTop("数据保存成功！（表格数据已重新绑定）");
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

            ////更新无不良台数
            //int nobadqty = 0;
            //string strPorder = "";
            //string pdate = "";
            //string pline = "";
            // nobadqty = int.Parse(Prodzeroefects.Text);
            // strPorder = proorder.Text;
            // pdate = this.dpProdate.SelectedDate.Value.ToString("yyyyMMdd");
            //pline = prolinename.SelectedItem.Text;
            ////更新生产实绩
            //int rQty = 0;
            //var q =
            //        (from p in DB.Pp_P1d_Outputsubs
            //         where p.Poutputsubdel==false
            //         where p.UDF01 == strPorder
            //         where p.Prolinename== pline
            //         where p.Prodate== pdate
            //         group p by p.Prolot into g
            //         select new
            //         {
            //             TotalQty = g.Sum(p => p.Prorealqty)
            //         }).ToList();
            //if (q.Any())
            //{
            //    rQty = q[0].TotalQty;
            //}
            //DB.pp_defect_P2dcounts
            //    //.Where(s => s.Prodate.Substring(0, 6) == pdate)
            //       .Where(s=>s.Proorder== strPorder)
            //       .ToList()
            //       .ForEach(x => { x.Prodzeroefects = nobadqty; x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();

            //DB.Pp_P2d_Defects
            //  .Where(s => s.Proorder == strPorder)
            //  .Where(s => s.Prodate.Substring(0, 6) == pdate)

            //  .ToList()
            //  .ForEach(x => { x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();
            string order = ddlProorder.SelectedItem.Text.Substring(ddlProorder.SelectedItem.Text.IndexOf(",") + 1, ddlProorder.SelectedItem.Text.Length - ddlProorder.SelectedItem.Text.IndexOf(",") - 1);

            //string lot = proorder.SelectedItem.Text.Substring(0, proorder.SelectedItem.Text.IndexOf(","));

            //更新无不良台数
            UpdatingP2dHelper.Pp_Defect_P2d_Orders_NoBadqty_Update(order, GetIdentityName());

            //更新不具合件数
            //UpdatingHelper.UpdatebadTotal(this.dpProdate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1));
            //Common.UpdateDefectQty();
            //更新不具合合计
            //UpdatingHelper.UpdatebadAmount(this.dpProdate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1), GetIdentityName());
            //更新无不良台数
            //Common.UpdatenobadAmount(this.dpProdate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), int.Parse(Prodzeroefects.Text));

            //按订单更新生产数量，不良数量
            //Common.UpdateDefectCount(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), this.dpProdate.SelectedDate.Value.ToString("yyyyMM"));
            //按LOT更新班组，日期
            //Common.UpdateDefectCountLot(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), this.dpProdate.SelectedDate.Value.ToString("yyyyMM"));
            //更新不良率
            //Common.UpdateBadrate(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1));
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += 0;// Convert.ToDecimal(row["Prorealqty"]);
                rTotal += Convert.ToDecimal(row["Probadqty"]);
                ratio = 0;// rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prorealqty", pTotal.ToString("F2"));
            summary.Add("Probadqty", rTotal.ToString("F2"));
            summary.Add("Probadtotal", ratio.ToString("p0"));

            Grid1.SummaryData = summary;
        }
    }
}