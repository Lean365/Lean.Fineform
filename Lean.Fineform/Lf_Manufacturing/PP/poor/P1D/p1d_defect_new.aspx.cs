﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p1d_defect_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                //CoreP1DDefectNew
                return "CoreP1DDefectNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable DefDatatable;

        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;

        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty, Probadnote, Proorder, Probadreason, Pronobadqty, Proorderqty, Promodel, Promodelqty, Probadqty, Probadtotal, Probadamount, Prongdept, Probadset;

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
            //IQueryable<Pp_P1d_Defect> q = DB.Pp_P1d_Defects;
            //DefDatatable = CopyToDataTable(q);

            //获取SQL数据表
            DefDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_P1d_Defect");

            //DefDatatable = ConvertHelper.LinqConvertToDataTable(q);

            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 新增数据初始值
            JObject defaultObj = new JObject();
            defaultObj.Add("Prongdept", "组立");
            defaultObj.Add("Probadqty", "1");
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

            DefDate.SelectedDate = DateTime.Now.AddDays(-1);

            BindDdlLine();

            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.无不良台数不超过当天生产的实绩。</div><div>2.同LOT不同订单的集计系统自动处理。</div><div>3.不良集计是按选择的日期，批次对应工单的完成情况计算出来的。</div><div>4.OPH中没有不良的批次自动追加到不良集计中。</div>");
        }

        private void BindGrid()
        {
            IQueryable<Pp_P1d_Defect> q = DB.Pp_P1d_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            q = q.Where(u => u.IsDeleted == 0 && u.Prolinename.Contains(prolinename.SelectedItem.Text) && u.Prolot.Contains(prolot.SelectedItem.Text) && u.Prodate.Contains(ddate));

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
            q = SortAndPage<Pp_P1d_Defect>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));

            BindDdlDept();
        }

        #endregion Page_Load

        #region BindDdl Dropdown ListData

        //查询LOT
        private void BindDdlprolot()
        {
            //查询LINQ去重复

            if (this.DefDate.SelectedDate.Value.ToString("yyyyMMdd").Length == 8)
            {
                Prodate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
                string pline = this.prolinename.SelectedItem.Text;
                var q = from p in DB.Pp_P1d_Outputs
                        where p.Prodate.Contains(Prodate) && p.Prolinename.Contains(pline)
                            && !(from d in DB.Pp_P1d_Defects
                                 where d.IsDeleted == 0
                                 where d.Prodate == Prodate
                                 where d.Prolinename == p.Prolinename
                                 select d.Proorder)//20220815修改之前是d.Prolots
                                 .Contains(p.Proorder)//20220815修改之前是p.Prolots
                        select new
                        {
                            Prolot = p.Prolot + "," + p.Proorder,
                        };

                var qs = q.Select(E => new { E.Prolot }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                prolot.DataSource = qs;
                prolot.DataTextField = "Prolot";
                prolot.DataValueField = "Prolot";
                prolot.DataBind();
                this.prolot.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        //查询班组
        private void BindDdlLine()

        {
            if (DefDate.SelectedDate.HasValue)
            {
                Prodate = DefDate.SelectedDate.Value.ToString("yyyyMMdd");

                //查询LINQ去重复
                var q = from a in DB.Pp_P1d_Outputs
                            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
                            //where b.Proecnno == strecn
                        where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P1d_Defects
                                                               where d.IsDeleted == 0
                                                               where d.Prodate == Prodate
                                                               where d.Prolinename == a.Prolinename
                                                               select d.Prolot)
                                    .Contains(a.Prolot)//投入日期
                        select new
                        {
                            a.Prolinename
                        };

                var qs = q.Select(E => new { E.Prolinename }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                prolinename.DataSource = qs;
                prolinename.DataTextField = "Prolinename";
                prolinename.DataValueField = "Prolinename";
                prolinename.DataBind();
                this.prolinename.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
            }
        }

        //  不良种类
        private void BindDdlDept()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_d")
                    orderby a.DictLabel
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
            ddlProngdept.DataSource = qs;
            ddlProngdept.DataTextField = "DictLabel";
            ddlProngdept.DataValueField = "DictValue";
            ddlProngdept.DataBind();
        }

        #endregion BindDdl Dropdown ListData

        #region Events

        protected void prolinename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prolinename.SelectedIndex != 0 && prolinename.SelectedIndex != -1)
            {
                prorealqty.Text = "";
                pronobadqty.Text = "";
                promodel.Text = "";
                proorder.Text = "";
                proorderqty.Text = "";
                BindDdlprolot();
            }
        }

        protected void prolot_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prolot.SelectedIndex != -1 && prolot.SelectedIndex != 0 && prolinename.SelectedIndex != -1 && prolinename.SelectedIndex != 0)
            {
                string sdate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
                string sline = prolinename.SelectedItem.Text;
                string slot = prolot.SelectedItem.Text.Substring(0, prolot.SelectedItem.Text.IndexOf(","));
                string sorder = prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1);

                var q_output = from a in DB.Pp_P1d_OutputSubs
                                   //join b in DB.Pp_P1d_OutputSubs on a.ID equals b.Parent.ID

                               where a.IsDeleted == 0
                               where a.Prodate == sdate
                               where a.Prolinename == sline
                               where a.Prolot == slot
                               where a.Proorder == sorder
                               select new
                               {
                                   a.Promodel,
                                   a.Prodate,
                                   a.Prolinename,
                                   a.Prolot,
                                   a.Prorealqty
                               };
                var q = from p in q_output

                        group p by new
                        {
                            //order.Proorder,
                            p.Promodel,
                            p.Prodate,
                            p.Prolinename,
                            p.Prolot
                        }

                    into g
                        select new
                        {
                            Prodate = g.Key.Prodate,
                            //Proorder =g.Key.Proorder,
                            Promodel = g.Key.Promodel,
                            Prolot = g.Key.Prolot,
                            Prorealqty = g.Sum(p => p.Prorealqty),
                        };
                var qs = q.ToList();
                if (qs.Any())
                {
                    prorealqty.Text = qs[0].Prorealqty.ToString();
                    pronobadqty.Text = qs[0].Prorealqty.ToString();
                    promodel.Text = qs[0].Promodel.ToString();

                    proorder.Text = sorder;

                    var orderqty = (from a in DB.Pp_Orders
                                    where a.Porderno == proorder.Text
                                    select a).ToList();
                    if (orderqty.Any())
                    {
                        proorderqty.Text = orderqty[0].Porderqty.ToString();
                        promodelqty.Text = orderqty[0].Porderqty.ToString();
                    }
                    else
                    {
                        proorderqty.Text = "0";
                        promodelqty.Text = "0";
                    }
                    //机种总数量不再需要
                    //var modelqty = (from a in DB.Pp_P1d_OutputSubs

                    //                where a.Prolot == slot

                    //                group a by new
                    //                {
                    //                    a.Prolot,
                    //                }
                    //                into g
                    //                select new
                    //                {
                    //                    Prolot = g.Key.Prolot,
                    //                    Promodelqty = g.Sum(a => a.Prorealqty),
                    //                }).ToList();
                    //if (modelqty.Any())
                    //{
                    //    promodelqty.Text = modelqty[0].Promodelqty.ToString();
                    //}

                    //proorderqty.Text = modelqty[0].Porderqty.ToString();

                    BindGrid();
                }

                //ConnStr = "SELECT [Prolinename],[Prodate],[Prolot],sum([Prorealqty])[Prorealqty] " +
                //            " FROM [dbo].[Pp_P1d_OutputSubs] where Prodate='" + this.DefDate.SelectedDate.Value.ToString("yyyyMMdd") + "' and  [Prolot] = '" + prolot.SelectedItem.Text + "' and  [Prolinename]='" + prolinename.SelectedItem.Text + "'" +
                //            "  group by[Prolinename],[Prodate],[Prolot]";

                //SqlDataAdapter DAstr1 = new SqlDataAdapter(ConnStr, AppConn);
                //DataSet DSstr1 = new DataSet();
                //DAstr1.Fill(DSstr1);
                ////赋值给订单序列号字段[MC008]
                //if (JudgeHelper.IfExitData(DSstr1, 0))
                //{
                //    if (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) >= 0)
                //    {
                //        prorealqty.Text = DSstr1.Tables[0].Rows[0][3].ToString();

                //        BindGrid();
                //    }
                //    else
                //    {
                //        Alert alert = new Alert();
                //        alert.Message = "ST为0,不能参与计算,请先确认！";
                //        alert.IconFont = IconFont.Warning;
                //        alert.Target = Target.Top;
                //        Alert.ShowInTop();
                //        return;
                //    }
                //}
                //else
                //{
                //    Alert alert = new Alert();
                //    alert.Message = "数据不能为空，请检查机种，LOT数量，ST！源数据在生产订单中，请确认！";
                //    alert.IconFont = IconFont.Warning;
                //    alert.Target = Target.Top;
                //    Alert.ShowInTop();
                //    return;
                //}
            }
        }

        //判断操作类型

        private void EditDefectDataRow()
        {
            //判断修改记录
            //if (Grid1.GetModifiedData().Count == 0)
            //{
            //    labResult.Text = "";
            //    ShowNotify("表格数据没有变化！");
            //    return;
            //}
            // 删除现有数据
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                delrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Pp_P1d_Defect item = DB.Pp_P1d_Defects
                .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Contectext + " *Del 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                item.IsDeleted = 1;
                item.Modifier = GetIdentityName();
                item.ModifyDate = DateTime.Now;
                DB.SaveChanges();
                DeleteRowByID(delrowID);
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

            //mysql = "select * from [dbo].[Pp_P1d_Defects];";
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

                            //批次
                            Prolot = tb.Rows[tb.Rows.Count - 1]["Prolot"].ToString();
                            //机种
                            Promodel = tb.Rows[tb.Rows.Count - 1]["Promodel"].ToString();
                            //订单
                            Proorder = tb.Rows[tb.Rows.Count - 1]["Proorder"].ToString();
                            //班组
                            Prolinename = tb.Rows[tb.Rows.Count - 1]["Prolinename"].ToString();
                            //生产日期
                            Prodate = tb.Rows[tb.Rows.Count - 1]["Prodate"].ToString();
                            //订单台数
                            Proorderqty = tb.Rows[tb.Rows.Count - 1]["Proorderqty"].ToString();
                            //机种台数
                            //Promodelqty = tb.Rows[tb.Rows.Count - 1]["Promodelqty"].ToString();
                            //生产台数
                            Prorealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();
                            //无不良台数
                            Pronobadqty = tb.Rows[tb.Rows.Count - 1]["Pronobadqty"].ToString();
                            //生产实绩
                            Prorealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();
                            //不良症状
                            Probadnote = tb.Rows[tb.Rows.Count - 1]["Probadnote"].ToString();
                            //不良个所
                            Probadset = tb.Rows[tb.Rows.Count - 1]["Probadset"].ToString();
                            //不良原因
                            Probadreason = tb.Rows[tb.Rows.Count - 1]["Probadreason"].ToString();
                            //不良件数
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();
                            //不良件数
                            Prongdept = tb.Rows[tb.Rows.Count - 1]["Prongdept"].ToString();
                        }
                        Pp_P1d_Defect item = new Pp_P1d_Defect();

                        item.Prolot = Prolot;
                        //班组
                        item.Prolinename = Prolinename;
                        //机种
                        item.Promodel = Promodel;
                        //订单
                        item.Proorder = proorder.Text;
                        //班组
                        item.Prolinename = Prolinename;
                        //日期
                        item.Prodate = Prodate;
                        //订单台数
                        item.Proorderqty = (int)decimal.Parse(proorderqty.Text.ToString());
                        //机种台数

                        //item.Promodelqty = (int)decimal.Parse(promodelqty.Text.ToString());
                        //生产实绩
                        item.Prorealqty = int.Parse(Prorealqty);

                        //无不良台数
                        item.Pronobadqty = int.Parse(pronobadqty.Text);

                        //区分
                        item.Prongdept = Prongdept;

                        //不良数量
                        item.Probadqty = int.Parse(Probadqty);
                        //item.Probadtotal = int.Parse(Probadtotal);

                        //不良集计
                        item.Probadtotal = 0;
                        //不良合计
                        item.Probadamount = 0;
                        //不良症状
                        item.Probadnote = Probadnote;
                        //不良个所
                        item.Probadset = Probadset;
                        //不良原因
                        item.Probadreason = Probadreason;
                        item.IsDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P1d_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良* " + Contectext + "*New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }
                //else
                //{
                //    Pp_P1d_Defect item = new Pp_P1d_Defect();

                //    item.Prolot = this.prolot.Text;
                //    //班组

                //    proLine cline = DB.proLines
                //        .Where(u => u.linename == this.prolinename.SelectedItem.Text).FirstOrDefault();

                //    item.Prongdept = Prongdept;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.SelectedItem.Text;

                //    item.Prodate = prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prongdept = "OK";
                //    //种类
                //    Pp_P1d_Defectcode cclass = DB.Pp_P1d_Defectcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    Pp_P1d_Defectcode ccode = DB.Pp_P1d_Defectcodes
                //        .Where(u => u.cn_ngmatter == "OK").FirstOrDefault();

                //    item.Prongcode = ccode.ngcode;

                //    item.Prongmatter = "OK";
                //    item.Probadqty = 0;

                //    item.Probadtotal = 0;
                //    item.Probadnote = "OK";
                //    item.Probadreason = "OK";
                //    item.Prongbdel = false;
                //    item.Remark = "";
                //    item.Defectguid = Guid.NewGuid().ToString();

                //    item.CreateDate = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_P1d_Defects.Add(item);
                //    DB.SaveChanges();

                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
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
                            //最后一条记录
                            tb.Rows[tb.Rows.Count - 1]["ID"].ToString();
                            //第一条记录
                            tb.Rows[0]["ID"].ToString();
                            //批次
                            Prolot = tb.Rows[0]["Prolot"].ToString();
                            //机种
                            Promodel = tb.Rows[0]["Promodel"].ToString();
                            //订单
                            Proorder = tb.Rows[0]["Proorder"].ToString();
                            //班组
                            Prolinename = tb.Rows[0]["Prolinename"].ToString();
                            //生产日期
                            Prodate = tb.Rows[0]["Prodate"].ToString();
                            //订单台数
                            Proorderqty = tb.Rows[0]["Proorderqty"].ToString();
                            //机种台数
                            //Promodelqty = tb.Rows[0]["Promodelqty"].ToString();
                            //生产台数
                            Prorealqty = tb.Rows[0]["Prorealqty"].ToString();
                            //无不良台数
                            Pronobadqty = tb.Rows[0]["Pronobadqty"].ToString();
                            //生产实绩
                            Prorealqty = tb.Rows[0]["Prorealqty"].ToString();
                            //不良症状
                            Probadnote = tb.Rows[0]["Probadnote"].ToString();

                            //不良个所
                            Probadset = tb.Rows[0]["Probadset"].ToString();
                            //不良原因
                            Probadreason = tb.Rows[0]["Probadreason"].ToString();
                            //不良件数
                            Probadqty = tb.Rows[0]["Probadqty"].ToString();
                            //不良件数
                            Prongdept = tb.Rows[0]["Prongdept"].ToString();
                        }
                        Pp_P1d_Defect item = new Pp_P1d_Defect();

                        item.Prolot = Prolot;
                        //班组
                        item.Prolinename = Prolinename;
                        //机种
                        item.Promodel = Promodel;
                        //订单
                        item.Proorder = proorder.Text;
                        //班组
                        //item.Prolinename = Prolinename;
                        //日期
                        item.Prodate = Prodate;
                        //订单台数
                        item.Proorderqty = (int)decimal.Parse(proorderqty.Text.ToString());
                        //机种台数

                        //item.Promodelqty = (int)decimal.Parse(promodelqty.Text.ToString());
                        //生产实绩
                        item.Prorealqty = int.Parse(Prorealqty);

                        //无不良台数
                        item.Pronobadqty = int.Parse(pronobadqty.Text);

                        //区分
                        item.Prongdept = Prongdept;

                        //不良数量
                        item.Probadqty = int.Parse(Probadqty);
                        //item.Probadtotal = int.Parse(Probadtotal);

                        //不良集计
                        item.Probadtotal = 0;
                        //不良合计
                        item.Probadamount = 0;
                        //不良症状
                        item.Probadnote = Probadnote;

                        //不良个所

                        item.Probadset = Probadset;
                        //不良原因
                        item.Probadreason = Probadreason;
                        item.IsDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P1d_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良* " + Contectext + "*New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }

                //else
                //{
                //    Pp_P1d_Defect item = new Pp_P1d_Defect();

                //    item.Prolot = this.prolot.Text;
                //    //班组

                //    proLine cline = DB.proLines
                //        .Where(u => u.linename == this.prolinename.SelectedItem.Text).FirstOrDefault();

                //    item.Prongdept = Prongdept;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.SelectedItem.Text;
                //    item.Prodate = this.prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prongdept = "OK";
                //    //种类
                //    Pp_P1d_Defectcode cclass = DB.Pp_P1d_Defectcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    Pp_P1d_Defectcode ccode = DB.Pp_P1d_Defectcodes
                //        .Where(u => u.cn_ngmatter == "OK").FirstOrDefault();

                //    item.Prongcode = ccode.ngcode;

                //    item.Prongmatter = "OK";
                //    item.Probadqty = 0;

                //    item.Probadtotal = 0;
                //    item.Probadnote = "OK";
                //    item.Probadreason = "OK";
                //    item.Prongbdel = false;
                //    item.Remark = "";
                //    item.Defectguid = Guid.NewGuid().ToString();

                //    item.CreateDate = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_P1d_Defects.Add(item);
                //    DB.SaveChanges();

                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
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

            //批次
            rowData["Prolot"] = prolot.SelectedItem.Text.Substring(0, prolot.SelectedItem.Text.IndexOf(","));
            //机种

            //rowData["Prongclass"] = promodelqty.Text;
            rowData["Promodel"] = promodel.Text;
            //订单
            rowData["Proorder"] = proorder.Text;
            //班组
            rowData["Prolinename"] = prolinename.SelectedItem.Text;
            //生产日期
            rowData["Prodate"] = DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            //订单台数
            rowData["Proorderqty"] = proorder.Text;
            //机种台数
            //rowData["Promodelqty"] = proorder.Text;
            //生产台数
            rowData["Prorealqty"] = this.prorealqty.Text;
            //无不良台数
            rowData["Pronobadqty"] = pronobadqty.Text;
            //区分

            rowData["Prongdept"] = Prongdept;

            //不良台数
            rowData["Probadqty"] = 0;
            //不良集计
            rowData["Probadtotal"] = 0;

            //不良全计
            rowData["Probadamount"] = 0;

            //不良症状
            rowData["Probadnote"] = "";

            //不良个所
            rowData["Probadset"] = "";
            //不良原因
            rowData["Probadreason"] = "";

            rowData["IsDeleted"] = 0;

            rowData["GUID"] = Guid.NewGuid();
            rowData["UDF01"] = "";
            rowData["UDF02"] = "";
            rowData["UDF03"] = "";
            rowData["UDF04"] = "";
            rowData["UDF05"] = "";
            rowData["UDF06"] = "";
            rowData["Udf51"] = 0;
            rowData["Udf52"] = 0;
            rowData["Udf53"] = 0;
            rowData["Udf54"] = 0;
            rowData["Udf55"] = 0;
            rowData["Udf56"] = 0;
            rowData["CreateDate"] = DateTime.Now;
            rowData["CREATOR"] = GetIdentityName();
            //item.CreateDate = DateTime.Now;
            //item.Creator = GetIdentityName();
            //table.Rows.Add(rowData);
            InsertDefectDataRow(newAddedData, rowData);

            return rowData;
        }

        //修改内容提交
        private static void UpdateDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            Pp_P1d_Defect item = DB.Pp_P1d_Defects

                .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改前日志
            string BeforeContectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
            string BeforeOperateType = "修改";
            string BeforeOperateNotes = "beEdit生产不良* " + BeforeContectext + " *beEdit生产不良 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, BeforeOperateType, "不具合管理", "不具合修改", BeforeOperateNotes);

            // 不良区分
            if (rowDict.ContainsKey("Prongdept"))
            {
                rowData["Prongdept"] = rowDict["Prongdept"];
                item.Prongdept = rowData["Prongdept"].ToString();
                //RealQty = rowData["Proclassmatter"].ToString();
                //
                //OrderFinish();
            }
            // 不良种类
            //if (rowDict.ContainsKey("Proclassmatter"))
            //{
            //    rowData["Proclassmatter"] = rowDict["Proclassmatter"];
            //    item.Proclassmatter = rowData["Proclassmatter"].ToString();
            //    //RealQty = rowData["Proclassmatter"].ToString();
            //    //
            //    //OrderFinish();
            //}
            // 不良代码
            //if (rowDict.ContainsKey("Prongmatter"))
            //{
            //    rowData["Prongmatter"] = rowDict["Prongmatter"];
            //    item.Prongmatter = rowData["Prongmatter"].ToString();
            //    //StopCheck = rowData["Prongmatter"].ToString();
            //}
            // 不良数量
            if (rowDict.ContainsKey("Probadqty"))
            {
                rowData["Probadqty"] = rowDict["Probadqty"];
                item.Probadqty = int.Parse(rowData["Probadqty"].ToString());

                //StopMinute = rowData["Probadqty"].ToString();
            }
            // 不良总数
            if (rowDict.ContainsKey("Probadtotal"))
            {
                rowData["Probadtotal"] = rowDict["Probadtotal"];
                item.Probadtotal = int.Parse(rowData["Probadtotal"].ToString());
                //StopText = rowData["Probadtotal"].ToString();
            }
            // 改善对策
            if (rowDict.ContainsKey("Probadnote"))
            {
                rowData["Probadnote"] = rowDict["Probadnote"];
                if (rowData["Probadnote"].ToString() == "")
                {
                    Alert.ShowInTop("不良症状不能为空！");
                    return;
                }
                else
                {
                    item.Probadnote = rowData["Probadnote"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("Probadset"))
            {
                rowData["Probadset"] = rowDict["Probadset"];

                item.Probadset = rowData["Probadset"].ToString();

                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("Probadreason"))
            {
                rowData["Probadreason"] = rowDict["Probadreason"];
                if (rowData["Probadreason"].ToString() == "")
                {
                    Alert.ShowInTop("不良原因不能为空！");
                    return;
                }
                else
                {
                    item.Probadreason = rowData["Probadreason"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            item.ModifyDate = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Pp_P1d_Defect edititem = DB.Pp_P1d_Defects

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterContectext = edititem.ID + "," + edititem.Prolinename + "," + edititem.Prolot + "," + edititem.Prodate + "," + edititem.Prorealqty + "," + edititem.Pronobadqty + "," + edititem.Promodel + "," + edititem.Probadqty + "," + edititem.Probadtotal + "," + edititem.Probadnote;
            string AfterOperateType = "修改";
            string AfterOperateNotes = "afEdit生产不良* " + AfterContectext + " *afEdit生产不良 的记录已经将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, AfterOperateType, "不具合管理", "不具合修改", AfterOperateNotes);
        }

        //获取新增行内容
        private void InsertDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            // 区分
            BindDeftctDataRow("Prongdept", rowDict, rowData);

            // 种类
            //BindDeftctDataRow("Proclassmatter", rowDict, rowData);

            // 类别
            //BindDeftctDataRow("Prongmatter", rowDict, rowData);

            // 数量
            BindDeftctDataRow("Probadqty", rowDict, rowData);

            // 总数
            //BindDeftctDataRow("Probadtotal", rowDict, rowData);

            // 对策
            BindDeftctDataRow("Probadnote", rowDict, rowData);

            BindDeftctDataRow("Probadset", rowDict, rowData);
            // 对策
            BindDeftctDataRow("Probadreason", rowDict, rowData);
        }

        //根据字段获取信息
        private void BindDeftctDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }

        // 根据行ID来获取行数据
        private DataRow FindRowByID(int rowID)
        {
            //mysql = "select* from[dbo].[Pp_P1d_Defects];";
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
            //mysql = "select* from[dbo].[Pp_P1d_Defects];";
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
            //mysql = "select * from [dbo].[Pp_P1d_Defects];";
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //获取ID号
            int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

            //int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP1DDefectDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P1d_Defect current = DB.Pp_P1d_Defects.Find(del_ID);
                //删除日志
                string Contectext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Promodel + "," + current.Pronobadqty + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                //删除记录
                //DB.Pp_P1d_Defects.Where(l => l.ID == del_ID).Delete();

                current.IsDeleted = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();
                //重新绑定
                BindGrid();
            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreP1DDefectDelete", Grid1, "deleteField");
            // 设置LinkButtonField的点击客户端事件
        }

        public static string lclass, nclass, ncode, ConnStr;

        protected void DefDate_TextChanged(object sender, EventArgs e)
        {
            //绑定DDL
            BindDdlLine();
        }

        #endregion Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (Decimal.Parse(pronobadqty.Text) > Decimal.Parse(prorealqty.Text))
                {
                    Alert.ShowInTop("无不良台数不能大于生产台数");
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
            // nobadqty = int.Parse(pronobadqty.Text);
            // strPorder = proorder.Text;
            // pdate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            //pline = prolinename.SelectedItem.Text;
            ////更新生产实绩
            //int rQty = 0;
            //var q =
            //        (from p in DB.Pp_P1d_OutputSubs
            //         where p.Poutputsubdel==false
            //         where p.Udf001 == strPorder
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
            //DB.Pp_P1d_Defectcounts
            //    //.Where(s => s.Prodate.Substring(0, 6) == pdate)
            //       .Where(s=>s.Proorder== strPorder)
            //       .ToList()
            //       .ForEach(x => { x.Pronobadqty = nobadqty; x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();

            //DB.Pp_P1d_Defects
            //  .Where(s => s.Proorder == strPorder)
            //  .Where(s => s.Prodate.Substring(0, 6) == pdate)

            //  .ToList()
            //  .ForEach(x => { x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();

            string order = prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1);
            string lot = prolot.SelectedItem.Text.Substring(0, prolot.SelectedItem.Text.IndexOf(","));
            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(order, userid);
            //更新不具合合计
            UpdatingHelper.UpdatebadAmount(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), GetIdentityName());

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 合计表格
        /// </summary>
        /// <param name="source"></param>
        //
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