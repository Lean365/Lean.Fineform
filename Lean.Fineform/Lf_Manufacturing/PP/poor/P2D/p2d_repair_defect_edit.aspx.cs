using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor.P2D
{
    public partial class p2d_repair_defect_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DManuDefectEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable DefDatatable;

        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;

        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum, Oldnobadqty;
        public static string Prodate, Promodel, Proorder, Prolot, Proorderqty, Propcbtype, Prorealqty, Prolinename, Propcbcardno, Probadnote, Propcbcheckout, Probadreason, Probadqty, Probadtotal, Probadresponsibility, Probadprop, Probadserial, Probadrepairman;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
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
            defaultObj.Add("Propcbtype", "ANA");
            defaultObj.Add("Prolinename", "修正");
            defaultObj.Add("Probadqty", "0");
            defaultObj.Add("Probadrepairman", "黄儒钦");
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

            BindData();
            BindGrid();

            //UpdateQty();
            //lblProdate.SelectedDate = DateTime.Now.AddDays(-1);
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.选择项中没有的选项请联系电脑课添加</div>");
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Pp_P2d_Manufacturing_Defect current = DB.Pp_P2d_Manufacturing_Defects.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            lblProdate.Text = current.Prodate;
            // 选中当前节点的父节点
            lblProlinename.Text = current.Prolinename;

            lblProlot.Text = current.Prolot;

            lblProrealqty.Text = current.Prorealqty.ToString();
            lblPromodel.Text = current.Promodel.ToString();
            //promodelqty.Text = current.Promodelqty.ToString();
            lblProorderqty.Text = current.Proorderqty.ToString();

            lblProorder.Text = current.Proorder.ToString();

            //if (!String.IsNullOrEmpty(current.Pronobadqty.ToString()))
            //{
            //    pronobadqty.Text = current.Pronobadqty.ToString();
            //}
            //else
            //{
            //    pronobadqty.Text = current.Prorealqty.ToString();
            //}
            //更新前无不良台数
            //Oldnobadqty = int.Parse(pronobadqty.Text);

            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);

            //修改前日志
            string BeforeEdit = current.Prodate + "," + current.Prolinename + "," + current.Prolot + "," + current.Prorealqty + "," + current.Promodel + "," + current.Probadqty;
            string BeforeOperateType = "修改";
            string BeforeOperateNotes = "Edit生产不良* " + BeforeEdit + " *Edit生产不良 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, BeforeOperateType, "系统管理", "不具合修改", BeforeOperateNotes);
        }

        private void BindGrid()
        {
            IQueryable<Pp_P2d_Manufacturing_Defect> q = DB.Pp_P2d_Manufacturing_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.lblProdate.Text;
            q = q.Where(u => u.isDeleted == 0 && u.Prolinename.Contains(lblProlinename.Text) && u.Prolot.Contains(lblProlot.Text) && u.Prodate.Contains(ddate));

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

            //var q = DB.proDefects.Select(E => new { E.ID, E.Prongbdel, E.Prolot, E.Prolinename, E.Prodate, E.Prorealqty, E.Proclassmatter, E.Prongmatter, E.Probadqty, E.Probadtotal, E.Probadnote }).Distinct();
            //if (this.prolot.SelectedIndex != -1 && this.prolot.SelectedIndex != 0&& this.prolinename.SelectedIndex != -1 && this.prolinename.SelectedIndex != 0)
            //{
            //    q = q.Where(u => u.Prongbdel == false && u.Prolinename.Contains(prolinename.SelectedItem.Text) && u.Prolot.Contains(prolot.SelectedItem.Text));

            //    Grid1.DataSource = q;
            //}
            //else
            //{
            //    Grid1.DataSource = "";
            //}

            //Grid1.DataBind();
            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));

            //BindDdlLine();
            BindDdlPcbType();
            BindDdlCheckout();
            BindDdlBadprop();
            BindDdlBadresponsibility();
            BindDdlddlBadrepairmanp();
        }

        #endregion Page_Load

        #region BindDdl Dropdown ListData

        //A:PCB个所,B:责任单位,C:不良性质,E:板别,D:组立个所,F:PCBA,G:修理员,H:検査員,J:VC线别,K:检查状况,L:检出工程,M:目视别,P:未达成,S:停线
        //查询LOT

        //查询班组
        //private void BindDdlLine()

        //{
        //    Prodate = lblProdate.Text.ToString();

        //    //查询LINQ去重复
        //    var q = from a in DB.Pp_P2d_OutputSubs
        //            where a.Proorder.Contains(proorder.Text)
        //            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
        //            //where b.Proecnno == strecn
        //            //where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P2d_Defects
        //            //                                       where d.isDeleted == 0
        //            //                                       where d.Prodate == Prodate
        //            //                                       where d.Prolinename == a.Prolinename
        //            //                                       select d.Prolot)
        //            //            .Contains(a.Prolot)//投入日期
        //            select new
        //            {
        //                a.Prolinename
        //            };
        //    //var q = from a in DB.Pp_Lines
        //    //        where a.lineclass.CompareTo("P") == 0
        //    //        select new
        //    //        {
        //    //            Prolinename=a.linename
        //    //        };

        //    var qs = q.Select(E => new { E.Prolinename }).ToList().Distinct();
        //    //var list = (from c in DB.ProSapPorders
        //    //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
        //    //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
        //    //3.2.将数据绑定到下拉框
        //    ddlProlinename.DataSource = qs;
        //    ddlProlinename.DataTextField = "Prolinename";
        //    ddlProlinename.DataValueField = "Prolinename";
        //    ddlProlinename.DataBind();
        //    this.ddlProlinename.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        //}

        //板别
        private void BindDdlPcbType()
        {
            //查询LINQ去重复
            var q = from a in DB.Pp_P2d_OutputSubs
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.Proorder.Contains(lblProorder.Text)
                    select new
                    {
                        DictLabel = a.Propcbatype,
                        DictValue = a.Propcbatype
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPropcbtype.DataSource = qs;
            ddlPropcbtype.DataTextField = "DictLabel";
            ddlPropcbtype.DataValueField = "DictValue";
            ddlPropcbtype.DataBind();
            // 选中根节点
            this.ddlPropcbtype.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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
            ddlProbadprop.DataSource = qs;
            ddlProbadprop.DataTextField = "DictLabel";
            ddlProbadprop.DataValueField = "DictValue";
            ddlProbadprop.DataBind();
            // 选中根节点
            this.ddlProbadprop.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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
        private void BindDdlddlBadrepairmanp()
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
            ddlProbadrepairman.DataSource = qs;
            ddlProbadrepairman.DataTextField = "DictLabel";
            ddlProbadrepairman.DataValueField = "DictValue";
            ddlProbadrepairman.DataBind();
            // 选中根节点
            this.ddlProbadrepairman.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion BindDdl Dropdown ListData

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //获取ID号
            int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

            //int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP2DManuDefectDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P2d_Manufacturing_Defect current = DB.Pp_P2d_Manufacturing_Defects.Find(del_ID);
                //删除日志
                string Contectext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Promodel + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                //删除记录
                //DB.Pp_P2d_Defects.Where(l => l.ID == del_ID).Delete();

                current.isDeleted = 1;
                DB.SaveChanges();
                //重新绑定
                BindGrid();
            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreP2DManuDefectDelete", Grid1, "deleteField");
            // 设置LinkButtonField的点击客户端事件
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
                string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良*" + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                item.isDeleted = 1;
                DB.SaveChanges();

                //重新绑定
                BindGrid();
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

            //mysql = "select * from [dbo].[proDefects];";
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
                            Probadnote = tb.Rows[tb.Rows.Count - 1]["Probadnote"].ToString();//不良症状
                            //Propcbchecktype = tb.Rows[tb.Rows.Count - 1]["Propcbchecktype"].ToString();//检出工程
                            Propcbcheckout = tb.Rows[tb.Rows.Count - 1]["Propcbcheckout"].ToString();//检出工程
                            Probadreason = tb.Rows[tb.Rows.Count - 1]["Probadreason"].ToString();//不良原因
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();//不良数量
                            Probadresponsibility = tb.Rows[tb.Rows.Count - 1]["Probadresponsibility"].ToString();//责任归属
                            Probadprop = tb.Rows[tb.Rows.Count - 1]["Probadprop"].ToString();//不良性质
                            //Probadtotal = tb.Rows[tb.Rows.Count - 1]["Probadtotal"].ToString();//不良台数
                            Probadrepairman = tb.Rows[tb.Rows.Count - 1]["Probadrepairman"].ToString();//修理
                        }
                        Pp_P2d_Manufacturing_Defect item = new Pp_P2d_Manufacturing_Defect();

                        item.Prodate = lblProdate.Text;
                        item.Promodel = lblPromodel.Text;
                        item.Proorder = lblProorder.Text;
                        item.Prolot = lblProlot.Text;
                        item.Proorderqty = (int)decimal.Parse(lblProorderqty.Text);
                        item.Propcbtype = Propcbtype;
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Prolinename = Prolinename;
                        item.Propcbcardno = Propcbcardno;
                        item.Probadnote = Probadnote;
                        item.Propcbcheckout = Propcbcheckout;
                        item.Probadreason = Probadreason;
                        item.Probadqty = int.Parse(Probadqty);
                        item.Probadtotal = 0;
                        item.Probadresponsibility = Probadresponsibility;
                        item.Probadprop = Probadprop;
                        item.Probadserial = Probadserial;
                        item.Probadrepairman = Probadrepairman;
                        item.isDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Manufacturing_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良*" + Contectext + "*New生产不良 的记录已经将新增";
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

                //    item.Prongdept = Prongdept;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.SelectedItem.Text;

                //    item.Prodate = prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prongdept = "OK";
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
                //    item.Probadnote = "OK";
                //    item.Probadreason = "OK";
                //    item.Prongbdel = false;
                //    item.Remark = "";
                //    item.Defectguid = Guid.NewGuid().ToString();

                //    item.CreateDate = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_P2d_Defects.Add(item);
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
                            //tb.Rows[tb.Rows.Count - 1]["ID"].ToString();
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
                            Probadnote = tb.Rows[0]["Probadnote"].ToString();//不良症状
                            //Propcbchecktype = tb.Rows[0]["Propcbchecktype"].ToString();//检出工程
                            Propcbcheckout = tb.Rows[0]["Propcbcheckout"].ToString();//检出工程
                            Probadreason = tb.Rows[0]["Probadreason"].ToString();//不良原因
                            Probadqty = tb.Rows[0]["Probadqty"].ToString();//不良数量
                            Probadresponsibility = tb.Rows[0]["Probadresponsibility"].ToString();//责任归属
                            Probadprop = tb.Rows[0]["Probadprop"].ToString();//不良性质
                            //Probadtotal = tb.Rows[0]["Probadtotal"].ToString();//不良台数
                            Probadrepairman = tb.Rows[0]["Probadrepairman"].ToString();//修理
                        }
                        Pp_P2d_Manufacturing_Defect item = new Pp_P2d_Manufacturing_Defect();

                        item.Prodate = lblProdate.Text;
                        item.Promodel = lblPromodel.Text;
                        item.Proorder = lblProorder.Text;
                        item.Prolot = lblProlot.Text;
                        item.Proorderqty = (int)decimal.Parse(lblProorderqty.Text);
                        item.Propcbtype = Propcbtype;
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Prolinename = Prolinename;
                        item.Propcbcardno = Propcbcardno;
                        item.Probadnote = Probadnote;
                        item.Propcbcheckout = Propcbcheckout;
                        item.Probadreason = Probadreason;
                        item.Probadqty = int.Parse(Probadqty);
                        item.Probadtotal = 0;
                        item.Probadresponsibility = Probadresponsibility;
                        item.Probadprop = Probadprop;
                        item.Probadserial = Probadserial;
                        item.Probadrepairman = Probadrepairman;
                        item.isDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Manufacturing_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良* " + Contectext + " *New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }
                //else
                //{
                //    proDefect item = new proDefect();

                //    item.Prolot = this.prolot.Text;
                //    //班组

                //    proLine cline = DB.proLines
                //        .Where(u => u.linename == this.prolinename.Text).FirstOrDefault();

                //    item.Prongdept = Prongdept;
                //    item.Prolineclass = cline.lineclass;
                //    item.Prolinename = this.prolinename.Text;
                //    item.Prodate = this.prodate.Text;
                //    item.Prorealqty = decimal.Parse(this.prorealqty.Text);
                //    item.Prongdept = "OK";
                //    //种类
                //    proDefectcode cclass = DB.proDefectcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    proDefectcode ccode = DB.proDefectcodes
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
                //    item.CreateUser = GetIdentityName();
                //    DB.proDefects.Add(item);
                //    DB.SaveChanges();

                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                //    string NewOperateType = item.Defectguid;
                //    string NewOperateNotes = "beNew * " + NewText + " *beNew 的记录已经将新增";
                //    NetCountHelper.InsNetOperateNotes(userid, NewOperateType, "不具合管理", "不具合新增", NewOperateNotes);

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
            rowData["Prodate"] = lblProdate.Text;
            rowData["Promodel"] = lblPromodel.Text;
            rowData["Proorder"] = lblProorder.Text;
            rowData["Prolot"] = lblProlot.Text;
            rowData["Proorderqty"] = (int)decimal.Parse(lblProorderqty.Text);
            rowData["Prorealqty"] = int.Parse(lblProrealqty.Text);
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
            rowData["isDeleted"] = 0;
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
            string BeforeContectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
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
                    Alert.ShowInTop("实绩不能为空！");
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
            if (rowDict.ContainsKey("Probadnote"))
            {
                rowData["Probadnote"] = rowDict["Probadnote"];
                if (string.IsNullOrEmpty(rowData["Probadnote"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("不良症状不能为空！");
                    return;
                }
                else
                {
                    item.Probadnote = rowData["Probadnote"].ToString();
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
            if (rowDict.ContainsKey("Probadreason"))
            {
                rowData["Probadreason"] = rowDict["Probadreason"];
                if (string.IsNullOrEmpty(rowData["Probadreason"].ToString()))
                {
                    //item.Propcbcardno = "";
                    Alert.ShowInTop("原因不能为空！");
                    return;
                }
                else
                {
                    item.Probadreason = rowData["Probadreason"].ToString();
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
            if (rowDict.ContainsKey("Probadprop"))
            {
                rowData["Probadprop"] = rowDict["Probadprop"];
                if (string.IsNullOrEmpty(rowData["Probadprop"].ToString()))
                {
                    Alert.ShowInTop("性质不能为空！");
                    return;
                }
                else
                {
                    item.Probadprop = rowData["Probadprop"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }

            //修理
            if (rowDict.ContainsKey("Probadrepairman"))
            {
                rowData["Probadrepairman"] = rowDict["Probadrepairman"];
                if (string.IsNullOrEmpty(rowData["Probadrepairman"].ToString()))
                {
                    Alert.ShowInTop("修理不能为空！");
                    return;
                }
                else
                {
                    item.Probadrepairman = rowData["Probadrepairman"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            item.ModifyDate = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Pp_P2d_Manufacturing_Defect edititem = DB.Pp_P2d_Manufacturing_Defects

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterContectext = edititem.ID + "," + edititem.Prolinename + "," + edititem.Prolot + "," + edititem.Prodate + "," + edititem.Prorealqty + "," + edititem.Promodel + "," + edititem.Probadqty + "," + edititem.Probadtotal + "," + edititem.Probadnote;
            string AfterOperateType = "删除";
            string AfterOperateNotes = "afEdit生产不良* " + AfterContectext + " *afEdit生产不良 的记录已经被修改";
            OperateLogHelper.InsNetOperateNotes(userid, AfterOperateType, "不具合管理", "不具合修改", AfterOperateNotes);
        }

        //获取新增行内容
        private void InsertDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            UpdateDataRow("Propcbtype", rowDict, rowData);
            UpdateDataRow("Prorealqty", rowDict, rowData);
            UpdateDataRow("Prolinename", rowDict, rowData);
            UpdateDataRow("Propcbcardno", rowDict, rowData);
            UpdateDataRow("Probadnote", rowDict, rowData);
            UpdateDataRow("Propcbcheckout", rowDict, rowData);
            UpdateDataRow("Probadreason", rowDict, rowData);
            UpdateDataRow("Probadqty", rowDict, rowData);

            UpdateDataRow("Probadresponsibility", rowDict, rowData);
            UpdateDataRow("Probadprop", rowDict, rowData);
            UpdateDataRow("Probadserial", rowDict, rowData);
            UpdateDataRow("Probadrepairman", rowDict, rowData);
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
            //mysql = "select* from[dbo].[proDefects];";
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
            //mysql = "select* from[dbo].[proDefects];";
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
            //mysql = "select * from [dbo].[proDefects];";
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
                if (ddlProbadprop.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择不良性质！", MessageBoxIcon.Information);
                    return;
                }
                if (ddlProbadrepairman.SelectedIndex == 0 || ddlPropcbcheckout.SelectedIndex == -1)
                {
                    Alert.ShowInTop("请选择修理人员！", MessageBoxIcon.Information);
                    return;
                }
                //保存不具合数据
                EditDefectDataRow();
                UpdateNobadqty();
                //Alert.ShowInTop("数据保存成功！（表格数据已重新绑定）");
                //更新不具合件数

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

            //更新无不良台数
            //int nobadqty = 0;
            string strPorder = "";
            string pdate = "";
            string strPlot = "";
            //nobadqty = int.Parse(pronobadqty.Text);
            strPorder = lblProorder.Text;
            pdate = this.lblProdate.Text.Substring(0, 6);

            strPlot = lblProlot.Text;

            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(strPorder, GetIdentityName());

            //UpdatingHelper.okOrder_Update(strPorder);

            //更新不具合件数
            //UpdatingHelper.UpdatebadTotal(this.lblProdate.Text, prolinename.Text, strPorder);
            //Common.UpdateDefectQty();
            //更新不具合合计
            UpdatingHelper.UpdatebadAmount(this.lblProdate.Text, lblProlinename.Text, strPorder, GetIdentityName());
            //更新无不良台数
            //Common.UpdatenobadAmount(this.lblProdate.Text, prolinename.Text, strPorder, nobadqty);

            //按订单更新生产数量，不良数量
            //Common.UpdateDefectCount(strPorder, this.lblProdate.Text);
            //按LOT更新班组，日期
            //Common.UpdateDefectCountLot(strPlot, pdate);
            //更新不良率
            //Common.UpdateBadrate(strPorder);
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void UpdateNobadqty()
        {
            //判断无不良台数是否变化
            //if (Oldnobadqty != int.Parse(pronobadqty.Text))
            //{
            //    string ddate = this.lblProdate.Text;
            //    var Pp_Def = DB.Pp_P2d_Defects.Where(u => u.isDeleted == 0 && u.Prolinename.Contains(prolinename.Text) && u.Prolot.Contains(prolot.Text) && u.Prodate.Contains(ddate));
            //    foreach (var Pp_P2d_Defects in Pp_Def)
            //    {
            //        Pp_P2d_Defects.Pronobadqty = int.Parse(this.pronobadqty.Text);

            //        Pp_P2d_Defects.Modifier = GetIdentityName();
            //        Pp_P2d_Defects.ModifyDate = DateTime.Now;
            //    }
            //    DB.SaveChanges();
            //}
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