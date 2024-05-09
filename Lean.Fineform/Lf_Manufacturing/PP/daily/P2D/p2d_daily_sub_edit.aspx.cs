using System;
using System.Collections.Generic;

//using EntityFramework.Extensions;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_daily_sub_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputSubEdit";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable DailyDatatable;

        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;

        public static int rowID, delrowID, editrowID, totalSum;

        public static string mysql, strGUID, strProordertype, strProorder, strProorderqty, strProlinename, strProdate, strProdirect, strProindirect,
                            strProlot, strPromodel, strProhbn, strPropcbatype, strPropcbaside, strProrealtotal,
                            strProst, strProshort, strProrate, strProstdcapacity, strTotaltag,
                            strProstime, strProetime, strProrealqty,
                            strPropcbserial, strProlinestopmin, strProstopcou, strProstopmemo, strProbadcou, strProbadmemo,
                            strProlinemin, strProrealtime,
                            strPropcbastated, strProtime, strProhandoffnum, strProhandofftime, strProdowntime,
                            strProlosstime, strPromaketime, strProworkst,
                            strProstdiff, strProqtydiff, strProratio,
                            strUDF01, strUDF02, strUDF03, strUDF04, strUDF05, strUDF06,
                            strUDF51, strUDF52, strUDF53, strUDF54, strUDF55, strUDF56,
                            strisDeleted, strRemark,
                            strCreator, strCreateDate, strModifier, strModifyDate;

        public static int prorate;
        public static int ParentID;
        public static decimal orderqty, ophqty;
        public static string userid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void Rate()//产线代码
        {
            Pp_Efficiency current = DB.Pp_Efficiencys

                .OrderByDescending(u => u.Proratedate).FirstOrDefault();

            prorate = current.Prorate;
        }

        private void LoadData()
        {
            userid = GetIdentityName();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            CheckPowerWithLinkButtonField("CoreP2DOutputDelete", Grid1, "deleteField");
            ////CheckPowerWithButton("CoreProophp1dNew", btnP1dNew);
            ////CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);
            //CheckPowerWithButton("CorePlutoExport2007", BtnExport);
            //CheckPowerWithButton("CorePlutoExport2003", Btn2003);
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            //ResolveDeleteButtonForGrid(btnDelete, Grid1);

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            //ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
            CreateRow();
            BindText();
            BindDdlPcbaType();
            BindDdlProStated();
            BindDdlProCause();
            Rate();
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.生产工数为1时，参与OPH计划台数统计。</div><div>2.同一时段生产多个工单时，计划台数系统自动处理，作业工数计算方法：<strong>多个工单具体的实绩工数+停线时间</strong></div><div>3.实绩工数的计算公式：<strong>作业工数-停线时间</strong></div><div>4.实绩工数不能为负数</div><div>4.生产实绩不能超过工单数量</div>");
            //MemoText.Text = "填写说明" + Environment.NewLine + "1.作业工数为1时，参与OPH计划台数统计。" + Environment.NewLine +"2.同一时段多个工单生产时，";
        }

        private void BindGrid()
        {
            try
            {
                //ParentID = GetQueryIntValue("ID");

                //获取通过窗体传递的值
                string strtransmit = GetQueryValue("Transtr");
                //转字符串组
                string[] strgroup = strtransmit.Split(',');

                ParentID = int.Parse(strgroup[0].ToString().Trim());
                strProlinename = strgroup[1].ToString().Trim();
                strProdate = strgroup[2].ToString().Trim();
                strProlot = strgroup[3].ToString().Trim();
                strPromodel = strgroup[4].ToString().Trim();
                strProst = strgroup[5].ToString().Trim();
                strProstdcapacity = strgroup[6].ToString().Trim();
                strProorder = strgroup[7].ToString().Trim();

                IQueryable<Pp_P2d_OutputSub> q = DB.Pp_P2d_OutputSubs; //.Include(u => u.Dept);
                q = q.Where(u => u.Parent.ToString() == ParentID.ToString());
                //q.OrderBy(u => "100"+u.Prostime);
                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = q.Count();

                // 排列和数据库分页
                q = SortAndPage<Pp_P2d_OutputSub>(q, Grid1);

                Grid1.DataSource = q;
                Grid1.DataBind();
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

        private void BindText()
        {
            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs
            .Where(u => u.Parent == ParentID).FirstOrDefault();
            guid.Text = item.GUID.ToString();
            parent.Text = item.Parent.ToString();
            proordertype.Text = item.Proordertype.ToString();
            prodate.Text = item.Prodate.ToString();
            promodel.Text = item.Promodel;
            prolot.Text = item.Prolot;
            proorder.Text = item.Proorder;
            prolinename.Text = item.Prolinename.ToString();
            proorderqty.Text = item.Proorderqty.ToString();
            strGUID = item.GUID.ToString();
            //strParent = item.Parent;
            strProordertype = item.Proordertype;
            strProorder = item.Proorder;
            strProorderqty = item.Proorderqty.ToString();
            strProlinename = item.Prolinename;
            strProdate = item.Prodate;
            strProdirect = item.Prodirect.ToString();
            strProindirect = item.Proindirect.ToString();
            strProlot = item.Prolot;
            strPromodel = item.Promodel;
            strProhbn = item.Prohbn;
            strPropcbatype = item.Propcbatype;
            strPropcbaside = item.Propcbaside;
            strProst = item.Prost.ToString();
            strProshort = item.Proshort.ToString();
            strProrate = item.Prorate.ToString();
            strProstdcapacity = item.Prostdcapacity.ToString();
            strTotaltag = item.Totaltag.ToString();
            strProstime = item.Prostime;
            strProetime = item.Proetime;
            strProrealqty = item.Prorealqty.ToString();
            strProrealtotal = item.Prorealtotal.ToString();
            strPropcbserial = item.Propcbserial;
            strProlinestopmin = item.Prolinestopmin.ToString();
            strProstopcou = item.Prostopcou;
            strProstopmemo = item.Prostopmemo;
            strProbadcou = item.Probadcou;
            strProbadmemo = item.Probadmemo;
            strProlinemin = item.Prolinemin.ToString();
            strProrealtime = item.Prorealtime.ToString();
            strPropcbastated = item.Propcbastated;
            strProtime = item.Protime.ToString();
            strProhandoffnum = item.Prohandoffnum.ToString();
            strProhandofftime = item.Prohandofftime.ToString();
            strProdowntime = item.Prodowntime.ToString();
            strProlosstime = item.Prolosstime.ToString();
            strPromaketime = item.Promaketime.ToString();
            strProworkst = item.Proworkst.ToString();
            strProstdiff = item.Prostdiff.ToString();
            strProqtydiff = item.Proqtydiff.ToString();
            strProratio = item.Proratio.ToString();
        }

        private void CreateRow()
        {            //获取SQL数据表
            DailyDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_P2d_OutputSub");
            // 新增数据初始值
            JObject defaultObj = new JObject();
            //defaultObj.Add("Prolinename", "SMT");
            //defaultObj.Add("Prodate", strProdate);
            //defaultObj.Add("Promodel", strPromodel);
            //defaultObj.Add("Prolot", strProlot);

            // 在第一行新增一条数据
            btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

            // 重置表格
            btnReset.OnClientClick = Grid1.GetRejectChangesReference();
        }

        private void BindDdlPcbaType()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.DictType.Contains("reason_type_f")
                    orderby a.DictLabel
                    select new
                    {
                        a.DictLabel,
                        a.DictValue,
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPropcbatype.DataSource = qs;
            ddlPropcbatype.DataTextField = "DictLabel";
            ddlPropcbatype.DataValueField = "DictValue";
            ddlPropcbatype.DataBind();
        }

        private void BindDdlProStated()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.DictType.Contains("app_pro_status")
                    orderby a.DictLabel
                    select new
                    {
                        a.DictLabel,
                        a.DictValue,
                    };

            var qs = q.Select(E => new { E.DictValue, E.DictLabel }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPropcbastated.DataSource = qs;
            ddlPropcbastated.DataTextField = "DictValue";
            ddlPropcbastated.DataValueField = "DictValue";
            ddlPropcbastated.DataBind();
        }

        private void BindDdlProCause()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.DictType.Contains("reason_type_p")
                    orderby a.DictLabel
                    select new
                    {
                        a.DictLabel,
                        a.DictValue,
                    };

            var qs = q.Select(E => new { E.DictValue, E.DictLabel }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlProbadcou.DataSource = qs;
            ddlProbadcou.DataTextField = "DictValue";
            ddlProbadcou.DataValueField = "DictValue";
            ddlProbadcou.DataBind();
        }

        #endregion Page_Load

        #region Events

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        //判断操作类型

        private void EditDataRow()
        {
            // 删除现有数据
            //List<int> deletedRows = Grid1.GetDeletedList();
            //foreach (int rowIndex in deletedRows)
            //{
            //    delrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
            //    Pp_P2d_Defect item = DB.Pp_P2d_Defects
            //    .Where(u => u.ID == delrowID).FirstOrDefault();
            //    //删除日志
            //    string Contectext = item.ID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Pronobadqty + "," + item.Promodel + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
            //    string OperateType = "删除";
            //    string OperateNotes = "Del* " + Contectext + " *Del 的记录可能将被删除";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

            //    item.isDeleted = 1;
            //    DB.SaveChanges();

            //    //重新绑定
            //    BindGrid();

            //    //DeleteRowByID(delrowID);
            //}

            // 修改的现有数据
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();
            foreach (int rowIndex in modifiedDict.Keys)
            {
                editrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                DataRow row = FindRowByID(editrowID);

                UpdateDataRow(modifiedDict[rowIndex], row);
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
                        DataRow rowData = CreateNewData(DailyDatatable, newAddedList[i]);

                        DailyDatatable.Rows.Add(rowData);

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

                            //班组
                            strProlinename = tb.Rows[tb.Rows.Count - 1]["Prolinename"].ToString();
                            //生产日期
                            strProdate = tb.Rows[tb.Rows.Count - 1]["Prodate"].ToString();
                            //机种
                            strPromodel = tb.Rows[tb.Rows.Count - 1]["Promodel"].ToString();
                            //批次
                            strProlot = tb.Rows[tb.Rows.Count - 1]["Prolot"].ToString();
                            //板别
                            strPropcbatype = tb.Rows[tb.Rows.Count - 1]["Propcbatype"].ToString();
                            //多面板
                            strPropcbaside = tb.Rows[tb.Rows.Count - 1]["Propcbaside"].ToString();
                            //机种台数
                            //Promodelqty = tb.Rows[tb.Rows.Count - 1]["Promodelqty"].ToString();
                            //生产台数
                            strProrealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();
                            //完成情况
                            strPropcbastated = tb.Rows[tb.Rows.Count - 1]["Propcbastated"].ToString();
                            //未达成原因
                            strProbadcou = tb.Rows[tb.Rows.Count - 1]["Probadcou"].ToString();
                            //序列号
                            strPropcbserial = tb.Rows[tb.Rows.Count - 1]["Propcbserial"].ToString();
                            //生产工数
                            strProtime = tb.Rows[tb.Rows.Count - 1]["Protime"].ToString();
                            //切换次数
                            strProhandoffnum = tb.Rows[tb.Rows.Count - 1]["Prohandoffnum"].ToString();
                            //切换时间
                            strProhandofftime = tb.Rows[tb.Rows.Count - 1]["Prohandofftime"].ToString();
                            //切停机时间
                            strProdowntime = tb.Rows[tb.Rows.Count - 1]["Prodowntime"].ToString();
                            //损失工数
                            strProlosstime = tb.Rows[tb.Rows.Count - 1]["Prolosstime"].ToString();
                            //投入工数
                            strPromaketime = tb.Rows[tb.Rows.Count - 1]["Promaketime"].ToString();
                        }
                        Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                        item.GUID = Guid.Parse(strGUID);
                        item.Parent = ParentID;
                        item.Proordertype = strProordertype;
                        item.Proorder = strProorder;
                        item.Proorderqty = decimal.Parse(strProorderqty);
                        item.Prolinename = strProlinename;
                        item.Prodate = strProdate;
                        item.Prodirect = int.Parse(strProdirect);
                        item.Proindirect = int.Parse(strProindirect);
                        item.Prolot = strProlot;
                        item.Promodel = strPromodel;
                        item.Prohbn = strProhbn;
                        item.Propcbatype = strPropcbatype;
                        item.Propcbaside = strPropcbaside;
                        item.Prost = decimal.Parse(strProst);
                        item.Proshort = decimal.Parse(strProshort);
                        item.Prorate = decimal.Parse(strProrate);
                        item.Prostdcapacity = decimal.Parse(strProstdcapacity);
                        item.Totaltag = false;
                        item.Prostime = strProstime;
                        item.Proetime = strProetime;
                        item.Prorealqty = int.Parse(strProrealqty);
                        item.Prorealtotal = int.Parse(strProrealtotal);
                        item.Propcbserial = strPropcbserial;
                        item.Prolinestopmin = int.Parse(strProlinestopmin);
                        item.Prostopcou = strProstopcou;
                        item.Prostopmemo = strProstopmemo;
                        item.Probadcou = strProbadcou;
                        item.Probadmemo = strProbadmemo;
                        item.Prolinemin = int.Parse(strProlinemin);
                        item.Prorealtime = int.Parse(strProrealtime);
                        item.Propcbastated = strPropcbastated;
                        item.Protime = int.Parse(strProtime);
                        item.Prohandoffnum = int.Parse(strProhandoffnum);
                        item.Prohandofftime = int.Parse(strProhandofftime);
                        item.Prodowntime = int.Parse(strProdowntime);
                        item.Prolosstime = int.Parse(strProlosstime);
                        item.Promaketime = int.Parse(strPromaketime);
                        item.Proworkst = decimal.Parse(strProworkst);
                        item.Prostdiff = decimal.Parse(strProstdiff);
                        item.Proqtydiff = int.Parse(strProqtydiff);
                        item.Proratio = int.Parse(strProratio);

                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P2d_OutputSubs.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Protime + "," + item.Promodel + "," + item.Prohandoffnum + "," + item.Prohandofftime + "," + item.Prodowntime;
                        string OperateType = "新增";
                        string OperateNotes = "New生产日报* " + Contectext + "*New生产日报 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报新增", OperateNotes);
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

                        DataRow rowData = CreateNewData(DailyDatatable, newAddedList[i]);

                        DailyDatatable.Rows.InsertAt(rowData, 0);
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

                            //班组
                            strProlinename = tb.Rows[0]["Prolinename"].ToString();
                            //生产日期
                            strProdate = tb.Rows[0]["Prodate"].ToString();
                            //机种
                            strPromodel = tb.Rows[0]["Promodel"].ToString();
                            //批次
                            strProlot = tb.Rows[0]["Prolot"].ToString();
                            //板别
                            strPropcbatype = tb.Rows[0]["Propcbatype"].ToString();
                            //多面板
                            strPropcbaside = tb.Rows[0]["Propcbaside"].ToString();
                            //机种台数
                            //Promodelqty = tb.Rows[0]["Promodelqty"].ToString();
                            //生产台数
                            strProrealqty = tb.Rows[0]["Prorealqty"].ToString();
                            //完成情况
                            strPropcbastated = tb.Rows[0]["Propcbastated"].ToString();
                            //未达成原因
                            strProbadcou = tb.Rows[0]["Probadcou"].ToString();
                            //序列号
                            strPropcbserial = tb.Rows[0]["Propcbserial"].ToString();
                            //生产工数
                            strProtime = tb.Rows[0]["Protime"].ToString();
                            //切换次数
                            strProhandoffnum = tb.Rows[0]["Prohandoffnum"].ToString();
                            //切换时间
                            strProhandofftime = tb.Rows[0]["Prohandofftime"].ToString();
                            //切停机时间
                            strProdowntime = tb.Rows[0]["Prodowntime"].ToString();
                            //损失工数
                            strProlosstime = tb.Rows[0]["Prolosstime"].ToString();
                            //投入工数
                            strPromaketime = tb.Rows[0]["Promaketime"].ToString();
                        }
                        Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                        item.GUID = Guid.Parse(strGUID);
                        item.Parent = ParentID;
                        item.Proordertype = strProordertype;
                        item.Proorder = strProorder;
                        item.Proorderqty = decimal.Parse(strProorderqty);
                        item.Prolinename = strProlinename;
                        item.Prodate = strProdate;
                        item.Prodirect = int.Parse(strProdirect);
                        item.Proindirect = int.Parse(strProindirect);
                        item.Prolot = strProlot;
                        item.Promodel = strPromodel;
                        item.Prohbn = strProhbn;
                        item.Propcbatype = strPropcbatype;
                        item.Propcbaside = strPropcbaside;
                        item.Prost = decimal.Parse(strProst);
                        item.Proshort = decimal.Parse(strProshort);
                        item.Prorate = decimal.Parse(strProrate);
                        item.Prostdcapacity = decimal.Parse(strProstdcapacity);
                        item.Totaltag = false;
                        item.Prostime = strProstime;
                        item.Proetime = strProetime;
                        item.Prorealqty = int.Parse(strProrealqty);
                        item.Prorealtotal = int.Parse(strProrealtotal);
                        item.Propcbserial = strPropcbserial;
                        item.Prolinestopmin = int.Parse(strProlinestopmin);
                        item.Prostopcou = strProstopcou;
                        item.Prostopmemo = strProstopmemo;
                        item.Probadcou = strProbadcou;
                        item.Probadmemo = strProbadmemo;
                        item.Prolinemin = int.Parse(strProlinemin);
                        item.Prorealtime = int.Parse(strProrealtime);
                        item.Propcbastated = strPropcbastated;
                        item.Protime = int.Parse(strProtime);
                        item.Prohandoffnum = int.Parse(strProhandoffnum);
                        item.Prohandofftime = int.Parse(strProhandofftime);
                        item.Prodowntime = int.Parse(strProdowntime);
                        item.Prolosstime = int.Parse(strProlosstime);
                        item.Promaketime = int.Parse(strPromaketime);
                        item.Proworkst = decimal.Parse(strProworkst);
                        item.Prostdiff = decimal.Parse(strProstdiff);
                        item.Proqtydiff = int.Parse(strProqtydiff);
                        item.Proratio = int.Parse(strProratio);
                        item.isDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Pp_P2d_OutputSubs.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Protime + "," + item.Promodel + "," + item.Prohandoffnum + "," + item.Prohandofftime + "," + item.Prodowntime;
                        string OperateType = "新增";
                        string OperateNotes = "New生产日报* " + Contectext + "*New生产日报 的记录已经将新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报新增", OperateNotes);
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
                //    item.Prodate = this.prodate.Text;
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

            rowData["GUID"] = strGUID.ToString();
            rowData["Parent"] = ParentID;

            rowData["Proordertype"] = strProordertype;

            rowData["Proorder"] = strProorder;

            rowData["Proorderqty"] = strProorderqty;

            rowData["Prolinename"] = strProlinename;

            rowData["Prodate"] = strProdate;

            rowData["Prodirect"] = strProdirect;

            rowData["Proindirect"] = strProindirect;
            rowData["Prolot"] = strProlot;
            rowData["Promodel"] = strPromodel;
            rowData["Prohbn"] = strProhbn;
            rowData["Propcbatype"] = strPropcbatype;
            rowData["Propcbaside"] = strPropcbaside;
            rowData["Prost"] = strProst;
            rowData["Proshort"] = strProshort;
            rowData["Prorate"] = strProrate;
            rowData["Prostdcapacity"] = strProstdcapacity;
            rowData["Totaltag"] = 1;
            rowData["Prostime"] = strProstime;
            rowData["Proetime"] = strProetime;
            rowData["Prorealqty"] = 0;
            rowData["Prorealtotal"] = 0;
            rowData["Propcbserial"] = strPropcbserial;
            rowData["Prolinestopmin"] = 0;
            rowData["Prostopcou"] = strProstopcou;
            rowData["Prostopmemo"] = strProstopmemo;
            rowData["Probadcou"] = strProbadcou;
            rowData["Probadmemo"] = strProbadmemo;
            rowData["Prolinemin"] = 0;
            rowData["Prorealtime"] = 0;
            rowData["Propcbastated"] = strPropcbastated;
            rowData["Protime"] = 0;
            rowData["Prohandoffnum"] = 0;
            rowData["Prohandofftime"] = 0;
            rowData["Prodowntime"] = 0;
            rowData["Prolosstime"] = 0;
            rowData["Promaketime"] = 0;
            rowData["Proworkst"] = 0;
            rowData["Prostdiff"] = 0;
            rowData["Proqtydiff"] = 0;
            rowData["Proratio"] = 0;

            //rowData["Udf001"] = "";
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
            rowData["CreateDate"] = DateTime.Now;
            rowData["Creator"] = GetIdentityName();
            //item.CreateDate = DateTime.Now;
            //item.Creator = GetIdentityName();
            //table.Rows.Add(rowData);
            InsertDataRow(newAddedData, rowData);

            return rowData;
        }

        //获取新增行内容
        private void InsertDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            // 板别
            UpdateDataRow("Propcbatype", rowDict, rowData);

            // 多面
            UpdateDataRow("Propcbaside", rowDict, rowData);

            // 类别
            //UpdateDataRow("Prongmatter", rowDict, rowData);

            // 数量
            UpdateDataRow("Prorealqty", rowDict, rowData);

            // 完成
            UpdateDataRow("Propcbastated", rowDict, rowData);

            // 未达成原因
            UpdateDataRow("Probadcou", rowDict, rowData);
            //序列号
            UpdateDataRow("Propcbserial", rowDict, rowData);
            // 工数
            UpdateDataRow("Protime", rowDict, rowData);
            // 切换次数
            UpdateDataRow("Prohandoffnum", rowDict, rowData);
            // 切换时间
            UpdateDataRow("Prohandofftime", rowDict, rowData);
            // 切停机时间
            UpdateDataRow("Prodowntime", rowDict, rowData);
            // 损失工数
            UpdateDataRow("Prolosstime", rowDict, rowData);
            // 投入工数
            UpdateDataRow("Promaketime", rowDict, rowData);
        }

        private void UpdateDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs

                .Where(u => u.ID == editrowID).FirstOrDefault();

            //Pp_P2d_Output main_item = DB.Pp_P2d_Outputs

            //     .Where(u => u.ID == ParentID).FirstOrDefault();

            beforeInsNetOperateNotes();
            // 板别
            if (rowDict.ContainsKey("Propcbatype"))
            {
                rowData["Propcbatype"] = rowDict["Propcbatype"];
                item.Propcbatype = (rowData["Propcbatype"].ToString().ToUpper());
                strProrealqty = rowData["Propcbatype"].ToString();
            }
            // 多面板
            if (rowDict.ContainsKey("Propcbaside"))
            {
                rowData["Propcbaside"] = rowDict["Propcbaside"];
                item.Propcbaside = (rowData["Propcbaside"].ToString().ToUpper());
                strProrealqty = rowData["Propcbaside"].ToString();
            }

            // 生产实绩
            if (rowDict.ContainsKey("Prorealqty"))
            {
                rowData["Prorealqty"] = rowDict["Prorealqty"];
                item.Prorealqty = int.Parse(rowData["Prorealqty"].ToString());
                strProrealqty = rowData["Prorealqty"].ToString();
            }
            // 直接
            item.Prodirect = 0;

            // 间接
            item.Proindirect = 0;

            // 工数

            item.Prolinemin = 0;

            // 实绩工数

            item.Prorealtime = 0;

            // 未达成原因
            if (rowDict.ContainsKey("Probadcou"))
            {
                rowData["Probadcou"] = rowDict["Probadcou"];
                item.Probadcou = rowData["Probadcou"].ToString().ToUpper();
                strProbadcou = rowData["Probadcou"].ToString();
            }

            // 未达成备注
            //if (rowDict.ContainsKey("Probadmemo"))
            //{
            //    rowData["Probadmemo"] = rowDict["Probadmemo"];
            //    item.Probadmemo = rowData["Probadmemo"].ToString().ToUpper();
            //    strProbadmemo = rowData["Probadmemo"].ToString();
            //}
            item.Probadmemo = "";
            // 序列号
            if (rowDict.ContainsKey("Propcbserial"))
            {
                rowData["Propcbserial"] = rowDict["Propcbserial"];
                item.Propcbserial = rowData["Propcbserial"].ToString().ToUpper();
                strPropcbserial = rowData["Propcbserial"].ToString();
            }
            // 生产工数
            if (rowDict.ContainsKey("Protime"))
            {
                rowData["Protime"] = rowDict["Protime"];
                item.Protime = int.Parse(rowData["Protime"].ToString());
                strProtime = rowData["Protime"].ToString();
            }
            // 切换次数
            if (rowDict.ContainsKey("Prohandoffnum"))
            {
                rowData["Prohandoffnum"] = rowDict["Prohandoffnum"];
                item.Prohandoffnum = int.Parse(rowData["Prohandoffnum"].ToString());
                strProhandoffnum = rowData["Prohandoffnum"].ToString();
            }
            // 切换时间
            if (rowDict.ContainsKey("Prohandofftime"))
            {
                rowData["Prohandofftime"] = rowDict["Prohandofftime"];
                item.Prohandofftime = int.Parse(rowData["Prohandofftime"].ToString());
                strProhandofftime = rowData["Prohandofftime"].ToString();
            }
            // 切停机时间
            if (rowDict.ContainsKey("Prodowntime"))
            {
                rowData["Prodowntime"] = rowDict["Prodowntime"];
                item.Prodowntime = int.Parse(rowData["Prodowntime"].ToString());
                strProdowntime = rowData["Prodowntime"].ToString();
            }
            // 损失工数
            if (rowDict.ContainsKey("Prolosstime"))
            {
                rowData["Prolosstime"] = rowDict["Prolosstime"];
                item.Prolosstime = int.Parse(rowData["Prolosstime"].ToString());
                strProlosstime = rowData["Prolosstime"].ToString();
            }
            //投入工数
            if (rowDict.ContainsKey("Promaketime"))
            {
                rowData["Promaketime"] = rowDict["Promaketime"];
                item.Promaketime = int.Parse(rowData["Promaketime"].ToString());
                strPromaketime = rowData["Promaketime"].ToString();
            }
            //完成情况
            if (rowDict.ContainsKey("Propcbastated"))
            {
                rowData["Propcbastated"] = rowDict["Propcbastated"];
                item.Propcbastated = (rowData["Propcbastated"].ToString()).ToUpper();
                strPropcbastated = rowData["Propcbastated"].ToString();
            }
            item.Proworkst = 0;
            item.Prostdiff = 0;
            item.Proratio = 0;

            item.ModifyDate = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();
            afterInsNetOperateNotes();
            //更新不良数据中的实绩生产数量，按日期，工单，班组
            UpdatingHelper.DefectRealqty_Update(item.Proorder, item.Prodate, item.Prolinename, userid);

            //更新不良集计数据中的实绩生产数量,按工单
            UpdatingHelper.DefectTotalRealqty_Update(item.Proorder, userid);

            //判断不良是否录入
            UpdatingHelper.CheckDefectData(item.Proorder, item.Prodate, item.Prolinename);

            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(item.Proorder, userid);
            //更新订单已生产数量
            //UpdatingHelper.UpdateOrderRealQty(item.Proorder, userid);
        }

        //根据字段获取信息
        private void UpdateDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }

        //// 根据行ID来获取行数据
        private DataRow FindRowByID(int rowID)
        {
            //只能查询非空表，否则不能获取ID，请使用ConvertHelper.GetDataTable()函数

            //IQueryable<Pp_P2d_OutputSub> q = DB.Pp_P2d_OutputSubs;
            //DataTable OptDatatable =ConvertHelper.IEnumerableConvertToDataTable(q);
            //var q = from a in DB.Pp_P2d_OutputSubs
            //        where a.Parent == ParentID
            //        select new
            //        {
            //            a.ID,
            //            a.GUID,
            //            a.Prostime,
            //            a.Proetime,
            //            a.Prorealqty,
            //            a.Prolinestopmin,
            //            a.Prostopcou,
            //            a.Prostopmemo,
            //            a.Probadcou,
            //            a.Probadmemo,
            //            a.Prolinemin,
            //            a.Protime,
            //            a.Propcbaside,
            //            a.Prolosstime,
            //            a.Promaketime,
            //            a.Prohandoffnum,
            //            a.Prohandofftime,
            //            a.Propcbastated,
            //            a.Prodowntime,
            //            a.Propcbatype,
            //            a.Propcbserial,
            //            a.Prorealtime,
            //            a.Proworkst,
            //            a.Prostdiff,
            //            a.Proqtydiff,
            //            a.Proratio,
            //            a.UDF01,
            //            a.UDF02,
            //            a.UDF03,
            //            a.UDF04,
            //            a.UDF05,
            //            a.UDF06,
            //            a.isDeleted,
            //            a.Remark,
            //            a.Creator,
            //            a.CreateDate,
            //            a.Modifier,
            //            a.ModifyDate,
            //            Parent_ID = a.Parent,
            //        };

            //DataTable OptDatatable = ConvertHelper.LinqConvertToDataTable(q);
            foreach (DataRow row in DailyDatatable.Rows)
            {
                if (Convert.ToInt32(row["Id"]) == rowID)
                {
                    return row;
                }
            }
            return null;
        }

        // 模拟数据库的自增长列
        private int GetNextRowID()
        {
            int maxID = 0;
            //mysql = "select * from [dbo].[Pp_P2d_Defects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            foreach (DataRow row in DailyDatatable.Rows)
            {
                int currentRowID = Convert.ToInt32(row["Id"]);
                if (currentRowID > maxID)
                {
                    maxID = currentRowID;
                }
            }
            return maxID + 1;
        }

        //表格行命令
        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //获取ID号
            int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP2DOutputDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P2d_OutputSub current = DB.Pp_P2d_OutputSubs.Find(del_ID);
                string Newtext = current.ID + "," + current.Prostime + "," + current.Proetime;
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del生产OPH*" + Newtext + "*Del生产OPH 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除", OperateNotes);
                //删除记录
                DB.Pp_P2d_OutputSubs.Where(l => l.ID == del_ID).DeleteFromQuery();
                //重新绑定
                BindGrid();
            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreP2DOutputDelete", Grid1, "deleteField");
            // 设置LinkButtonField的点击客户端事件
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                ////DataRow row = FindRowByID(rowID);
                EditDataRow();

                //UpdateDataRow(modifiedDict[rowIndex], row);

                //报表重建
                //Common.UpdateLineData();
                // Common.UpdateModelData();

                //更新不具合实绩

                // Common.UpdateDefectQty(Ophguid);
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
                foreach (var erroritem in errors)
                    msg += erroritem.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }

            //Alert.ShowInTop("数据保存成功！（表格数据已重新绑定）");

            BindGrid();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void beforeInsNetOperateNotes()
        {
            //修改前日志
            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs
            .Where(u => u.ID == editrowID).FirstOrDefault();
            string Newtext = item.ID + "," + item.GUID + "," + item.Proetime + "," + item.Prostime + "," + item.Prorealqty + "," + item.Prodirect + "," + item.Proindirect + "," + item.Prorealtime + "," + item.Propcbserial;
            string OperateType = "修改";//操作标记
            string OperateNotes = "beEdit生产OPH_SUB*" + Newtext + " *beEdit生产OPH_SUB 的记录将被修改";

            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩修改", OperateNotes);
        }

        private void afterInsNetOperateNotes()
        {
            //修改后日志

            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs
                        .Where(u => u.ID == editrowID).FirstOrDefault();

            string Newtext = item.ID + "," + item.GUID + "," + item.Proetime + "," + item.Prostime + "," + strProrealqty + "," + strProdirect + "," + strProdirect + "," + strProrealtime + "," + strPropcbserial;
            string OperateType = "修改";//操作标记
            string OperateNotes = "afEdit生产OPH_SUB* " + Newtext + " *afEdit生产OPH_SUB 的记录已被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩修改", OperateNotes);
        }

        #endregion Events
    }
}