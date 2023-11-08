using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Validation;

namespace Lean.Fineform.Lf_Manufacturing.PP.poor
{

    public partial class p1d_defect_edit : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DDefectEdit";
            }
        }

        #endregion

        #region Page_Load
        public static DataTable DefDatatable;
        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;
        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum, Oldnobadqty;
        public static string Prolot, Prolinename, Prodate, Prorealqty, Probadnote, Proorder, Probadreason, Pronobadqty, Proorderqty, Promodel, Promodelqty, Probadqty, Probadtotal, Probadamount, Prongdept, Probadset;
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
            //IQueryable<Pp_Defect_P1d> q = DB.Pp_Defect_P1ds;
            //DefDatatable = CopyToDataTable(q);

            //获取SQL数据表
            DefDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_Defect_P1d");

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




            BindData();
            BindGrid();
            BindDDLDept();
            //UpdateQty();
            //DefDate.SelectedDate = DateTime.Now.AddDays(-1);
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.无不良台数不超过当天生产的实绩。</div><div>2.同LOT不同订单的集计系统自动处理。</div><div>3.不良集计是按选择的日期，批次对应工单的完成情况计算出来的。</div><div>4.OPH中没有不良的批次自动追加到不良集计中。</div>");
        }
        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Pp_Defect_P1d current = DB.Pp_Defect_P1ds.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }



            DefDate.Text = current.Prodate;
            // 选中当前节点的父节点
            prolinename.Text = current.Prolinename;

            prolot.Text = current.Prolot;

            prorealqty.Text = current.Prorealqty.ToString();
            promodel.Text = current.Promodel.ToString();
            //promodelqty.Text = current.Promodelqty.ToString();
            proorderqty.Text = current.Proorderqty.ToString();

            proorder.Text = current.Proorder.ToString();

            if (!String.IsNullOrEmpty(current.Pronobadqty.ToString()))
            {
                pronobadqty.Text = current.Pronobadqty.ToString();
            }
            else
            {
                pronobadqty.Text = current.Prorealqty.ToString();
            }
            //更新前无不良台数
            Oldnobadqty = int.Parse(pronobadqty.Text);

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
            IQueryable<Pp_Defect_P1d> q = DB.Pp_Defect_P1ds; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.DefDate.Text;
            q = q.Where(u => u.isDelete == 0 && u.Prolinename.Contains(prolinename.Text) && u.Prolot.Contains(prolot.Text) && u.Prodate.Contains(ddate));

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
            q = SortAndPage<Pp_Defect_P1d>(q, Grid1);

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
            OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));

            BindDDLDept();
        }





        #endregion

        #region Events
        #region BindDataToDropDownList


        //  不良种类
        private void BindDDLDept()
        {

            //查询LINQ去重复
            var q = from a in DB.Pp_Reasons
                    where a.Reasontype == "D"
                    //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
                    //where b.Proecnno == strecn
                    //where a.Prolineclass == "M"
                    select new
                    {
                        a.GUID,
                        a.Reasoncntext

                    };

            var qs = q.Select(E => new { E.GUID, E.Reasoncntext }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProngdept.DataSource = qs;
            ddlProngdept.DataTextField = "Reasoncntext";
            ddlProngdept.DataValueField = "Reasoncntext";
            ddlProngdept.DataBind();



        }


        #endregion

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
                Pp_Defect_P1d current = DB.Pp_Defect_P1ds.Find(del_ID);
                //删除日志
                string Contectext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Promodel + "," + current.Pronobadqty + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);


                //删除记录
                //DB.Pp_Defect_P1ds.Where(l => l.ID == del_ID).Delete();

                current.isDelete = 1;
                current.Modifier = GetIdentityName();
                current.ModifyTime = DateTime.Now;
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

        //判断操作类型

        private void EditDefectDataRow()
        {
            // 删除现有数据
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                delrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Pp_Defect_P1d item = DB.Pp_Defect_P1ds
                .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良*" + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);


                item.isDelete = 1;
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
                        Pp_Defect_P1d item = new Pp_Defect_P1d();

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
                        item.isDelete = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_Defect_P1ds.Add(item);
                        DB.SaveChanges();



                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                        string OperateType = "新增";
                        string OperateNotes = "New生产不良*" + Contectext + "*New生产不良 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合新增", OperateNotes);
                    }
                }
                //else
                //{
                //    Pp_Defect_P1d item = new Pp_Defect_P1d();

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
                //    Pp_Defect_P1dcode cclass = DB.Pp_Defect_P1dcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    Pp_Defect_P1dcode ccode = DB.Pp_Defect_P1dcodes
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

                //    item.CreateTime = DateTime.Now;
                //    item.Creator = GetIdentityName();
                //    DB.Pp_Defect_P1ds.Add(item);
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
                        Pp_Defect_P1d item = new Pp_Defect_P1d();

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
                        item.isDelete = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_Defect_P1ds.Add(item);
                        DB.SaveChanges();



                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
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

                //    item.CreateTime = DateTime.Now;
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

            //批次
            rowData["Prolot"] = prolot.Text;
            //机种

            //rowData["Prongclass"] = promodelqty.Text;
            rowData["Promodel"] = promodel.Text;
            //订单
            rowData["Proorder"] = proorder.Text;
            //班组
            rowData["Prolinename"] = prolinename.Text;
            //生产日期
            rowData["Prodate"] = DefDate.Text;
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


            rowData["isDelete"] = 0;

            rowData["GUID"] = Guid.NewGuid();
            rowData["Udf001"] = "";
            rowData["Udf002"] = "";
            rowData["Udf003"] = "";
            rowData["Udf004"] = 0;
            rowData["Udf005"] = 0;
            rowData["Udf006"] = 0;
            rowData["CreateTime"] = DateTime.Now;
            rowData["Creator"] = GetIdentityName();
            //item.CreateTime = DateTime.Now;
            //item.Creator = GetIdentityName();
            //table.Rows.Add(rowData);
            InsertDefectDataRow(newAddedData, rowData);

            return rowData;
        }
        //修改内容提交
        private static void UpdateDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            Pp_Defect_P1d item = DB.Pp_Defect_P1ds

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

            // 不良个所
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
            item.ModifyTime = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Pp_Defect_P1d edititem = DB.Pp_Defect_P1ds

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterContectext = edititem.ID + "," + edititem.Prolinename + "," + edititem.Prolot + "," + edititem.Prodate + "," + edititem.Prorealqty + "," + edititem.Pronobadqty + "," + edititem.Promodel + "," + edititem.Probadqty + "," + edititem.Probadtotal + "," + edititem.Probadnote;
            string AfterOperateType = "删除";
            string AfterOperateNotes = "afEdit生产不良* " + AfterContectext + " *afEdit生产不良 的记录已经被修改";
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

            // 对策
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
        #endregion


        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                //string inputUserName = tbxName.Text.Trim();

                //User user = DB.Users.Where(u => u.Name == inputUserName).FirstOrDefault();

                //if (user != null)
                //{
                //    Alert.ShowInTop("用户 " + inputUserName + " 已经存在！");
                //    return;
                //}

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
            int nobadqty = 0;
            string strPorder = "";
            string pdate = "";
            string strPlot = "";
            nobadqty = int.Parse(pronobadqty.Text);
            strPorder = proorder.Text;
            pdate = this.DefDate.Text.Substring(0, 6);

            strPlot = prolot.Text;


            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(strPorder, userid);

            //更新不具合合计
            UpdatingHelper.UpdatebadAmount(this.DefDate.Text, prolinename.Text, strPorder,userid);
            
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());

        }

        private void UpdateNobadqty()
        {
            //判断无不良台数是否变化
            if (Oldnobadqty != int.Parse(pronobadqty.Text))
            {
                string ddate = this.DefDate.Text;
                var Pp_Def = DB.Pp_Defect_P1ds.Where(u => u.isDelete == 0 && u.Prolinename.Contains(prolinename.Text) && u.Prolot.Contains(prolot.Text) && u.Prodate.Contains(ddate));
                foreach (var Pp_Defect_P1ds in Pp_Def)
                {
                    Pp_Defect_P1ds.Pronobadqty = int.Parse(this.pronobadqty.Text);

                    Pp_Defect_P1ds.Modifier = GetIdentityName();
                    Pp_Defect_P1ds.ModifyTime = DateTime.Now;
                }
                DB.SaveChanges();
            }



        }

        /// <summary>
        /// 合计表格
        /// </summary>
        /// <param name="source"></param>

        private void OutputSummaryData(DataTable source)
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