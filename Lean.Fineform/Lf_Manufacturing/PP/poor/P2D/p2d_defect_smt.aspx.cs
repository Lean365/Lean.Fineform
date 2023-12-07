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
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using System.Data.Entity.Validation;

using System.ComponentModel;

namespace Lean.Fineform.Lf_Manufacturing.PP.poor.P2D
{

    public partial class p2d_defect_smt : PageBase
    {

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DDefectNew";
            }
        }

        #endregion

        #region Page_Load
        public static DataTable DefDatatable;
        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;
        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty, Probadnote, Proorder, Probadreason, Pronobadqty, Proorderqty, Promodel, Promodelqty, Probadqty, Probadtotal, Probadamount, Prongdept, Udf001;

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
            //IQueryable<Pp_Defect_P2d> q = DB.Pp_P2d_Defects;
            //DefDatatable = CopyToDataTable(q);

            //获取SQL数据表
            DefDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_Defect_P2d");

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
            BindDDLDept();
            BindDDLline();

            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.无不良台数不超过当天生产的实绩。</div><div>2.同LOT不同订单的集计系统自动处理。</div><div>3.不良集计是按选择的日期，批次对应工单的完成情况计算出来的。</div><div>4.OPH中没有不良的批次自动追加到不良集计中。</div>");
        }





        private void BindGrid()
        {
            IQueryable<Pp_P2d_Defect> q = DB.Pp_P2d_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            q = q.Where(u => u.isDelete == 0 && u.Prolinename.Contains(prolinename.SelectedItem.Text) && u.Proorder.Contains(proorder.SelectedItem.Text) && u.Prodate.Contains(ddate));


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
            q = SortAndPage<Pp_P2d_Defect>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));


            
        }

        #endregion

        #region DDLBindData
        //查询LOT
        private void BindDDLproOrder()
        {

            var q=from p in DB.Pp_Orders
                  where string.IsNullOrEmpty( p.Porderlot)
                  select new
                  {
                      Prolot = p.Porderno + "," + p.Porderqty,

                  };

                var qs = q.Select(E => new { E.Prolot }).ToList().Distinct();
                //var list = (from c in DB.ProSapPorders
                //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
                //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
                //3.2.将数据绑定到下拉框
                proorder.DataSource = qs;
                proorder.DataTextField = "Prolot";
                proorder.DataValueField = "Prolot";
                proorder.DataBind();
                this.proorder.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));


        }



        //查询班组
        private void BindDDLline()

        {

            if (DefDate.SelectedDate.HasValue)
            {
                Prodate = DefDate.SelectedDate.Value.ToString("yyyyMMdd");

                //查询LINQ去重复
                //var q = from a in DB.Pp_P1d_Outputs
                //            //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
                //            //where b.Proecnno == strecn
                //        where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P2d_Defects
                //                                               where d.isDelete == 0
                //                                               where d.Prodate == Prodate
                //                                               where d.Prolinename == a.Prolinename
                //                                               select d.Prolot)
                //                    .Contains(a.Prolot)//投入日期
                //        select new
                //        {

                //            a.Prolinename

                //        };
                var q = from a in DB.Pp_Lines
                        where a.lineclass.CompareTo("P") == 0
                        select new
                        {
                            Prolinename=a.linename
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
                string Contectext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Promodel + "," + current.Pronobadqty + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);


                //删除记录
                //DB.Pp_P2d_Defects.Where(l => l.ID == del_ID).Delete();

                current.isDelete = 1;
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
        public static string lclass, nclass, ncode, ConnStr;

        protected void DefDate_TextChanged(object sender, EventArgs e)
        {
            BindDDLline();
        }




        #endregion





        protected void prolinename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prolinename.SelectedIndex != 0 && prolinename.SelectedIndex != -1)
            {

                prorealqty.Text = "0";
                pronobadqty.Text = "0";
                promodel.Text = "";
                //proorder.Text = "";
                proorderqty.Text = "0";
                BindDDLproOrder();
            }
        }

        protected void proorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proorder.SelectedIndex != -1 && proorder.SelectedIndex != 0 && prolinename.SelectedIndex != -1 && prolinename.SelectedIndex != 0)
            {
                this.prolot.Text = proorder.SelectedItem.Text;
                string sorder = proorder.SelectedItem.Text.Substring(0, proorder.SelectedItem.Text.IndexOf(","));

                var q = from a in DB.Pp_Orders
                        where a.Porderno.CompareTo(sorder) == 0
                        select a;
                var qs = q.ToList();
                if (qs.Any())
                {
                    //this.proorder.Text = sorder;
                    this.proorderqty.Text = proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1);

                    string prohbn = qs[0].Porderhbn.ToString();

                    var q_model = from a in DB.Pp_Manhours
                                  where a.Proitem.CompareTo(prohbn) == 0
                                  select a;
                    var q_mname = q_model.ToList();
                    if (q_mname.Any())
                    {
                        this.promodel.Text = q_mname[0].Promodel.ToString();

                    }
                }
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
                Pp_P2d_Defect item = DB.Pp_P2d_Defects
                .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Contectext + " *Del 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);


                item.isDelete = 1;
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
                            Udf001 = tb.Rows[tb.Rows.Count - 1]["Udf001"].ToString();
                            //不良原因
                            Probadreason = tb.Rows[tb.Rows.Count - 1]["Probadreason"].ToString();
                            //不良件数
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();
                            //不良件数
                            Prongdept = tb.Rows[tb.Rows.Count - 1]["Prongdept"].ToString();
                        }
                        Pp_P2d_Defect item = new Pp_P2d_Defect();

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
                        item.Udf001 = Udf001;
                        //不良原因
                        item.Probadreason = Probadreason;
                        item.isDelete = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P2d_Defects.Add(item);
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
                //    Pp_Defect_P2d item = new Pp_Defect_P2d();

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
                //    Pp_Defect_P2dcode cclass = DB.Pp_Defect_P2dcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    Pp_Defect_P2dcode ccode = DB.Pp_Defect_P2dcodes
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
                            Udf001 = tb.Rows[0]["Udf001"].ToString();
                            //不良原因
                            Probadreason = tb.Rows[0]["Probadreason"].ToString();
                            //不良件数
                            Probadqty = tb.Rows[0]["Probadqty"].ToString();
                            //不良件数
                            Prongdept = tb.Rows[0]["Prongdept"].ToString();



                        }
                        Pp_P2d_Defect item = new Pp_P2d_Defect();

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

                        item.Udf001 = Udf001;
                        //不良原因
                        item.Probadreason = Probadreason;
                        item.isDelete = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P2d_Defects.Add(item);
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
                //    Pp_Defect_P2d item = new Pp_Defect_P2d();

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
                //    Pp_Defect_P2dcode cclass = DB.Pp_Defect_P2dcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    Pp_Defect_P2dcode ccode = DB.Pp_Defect_P2dcodes
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
                //    DB.Pp_P2d_Defects.Add(item);
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
            rowData["Prolot"] = prolot.Text;
            //机种

            //rowData["Prongclass"] = promodelqty.Text;
            rowData["Promodel"] = promodel.Text;
            //订单
            rowData["Proorder"] = proorder.SelectedItem.Text;
            //班组
            rowData["Prolinename"] = prolinename.SelectedItem.Text;
            //生产日期
            rowData["Prodate"] = DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            //订单台数
            rowData["Proorderqty"] = proorderqty.Text;
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
            rowData["Udf001"] = "";
            //不良原因
            rowData["Probadreason"] = "";


            rowData["isDelete"] = 0;

            rowData["GUID"] = Guid.NewGuid();
            //rowData["Udf001"] = "";
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


            Pp_P2d_Defect item = DB.Pp_P2d_Defects

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
            if (rowDict.ContainsKey("Udf001"))
            {

                rowData["Udf001"] = rowDict["Udf001"];

                item.Udf001 = rowData["Udf001"].ToString();

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

            Pp_P2d_Defect edititem = DB.Pp_P2d_Defects

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
            UpdateDeftctDataRow("Prongdept", rowDict, rowData);

            // 种类
            //UpdateDeftctDataRow("Proclassmatter", rowDict, rowData);

            // 类别
            //UpdateDeftctDataRow("Prongmatter", rowDict, rowData);

            // 数量
            UpdateDeftctDataRow("Probadqty", rowDict, rowData);

            // 总数
            //UpdateDeftctDataRow("Probadtotal", rowDict, rowData);

            // 对策
            UpdateDeftctDataRow("Probadnote", rowDict, rowData);

            UpdateDeftctDataRow("Udf001", rowDict, rowData);
            // 对策
            UpdateDeftctDataRow("Probadreason", rowDict, rowData);

        }
        //根据字段获取信息
        private void UpdateDeftctDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
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
            //        (from p in DB.Pp_P1d_Outputsubs
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
            //DB.Pp_Defect_P2dcounts
            //    //.Where(s => s.Prodate.Substring(0, 6) == pdate)
            //       .Where(s=>s.Proorder== strPorder)
            //       .ToList()
            //       .ForEach(x => { x.Pronobadqty = nobadqty; x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();

            //DB.Pp_P2d_Defects
            //  .Where(s => s.Proorder == strPorder)
            //  .Where(s => s.Prodate.Substring(0, 6) == pdate)


            //  .ToList()
            //  .ForEach(x => { x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();
            string order = proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1);

            string lot = proorder.SelectedItem.Text.Substring(0, proorder.SelectedItem.Text.IndexOf(","));


            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(order, GetIdentityName());



            //更新不具合件数
            //UpdatingHelper.UpdatebadTotal(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1));
            //Common.UpdateDefectQty();
            //更新不具合合计
            UpdatingHelper.UpdatebadAmount(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1), GetIdentityName());
            //更新无不良台数
            //Common.UpdatenobadAmount(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), int.Parse(pronobadqty.Text));

            //按订单更新生产数量，不良数量
            //Common.UpdateDefectCount(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), this.DefDate.SelectedDate.Value.ToString("yyyyMM"));
            //按LOT更新班组，日期
            //Common.UpdateDefectCountLot(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1), this.DefDate.SelectedDate.Value.ToString("yyyyMM"));
            //更新不良率
            //Common.UpdateBadrate(prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1));
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        //合计表格
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
