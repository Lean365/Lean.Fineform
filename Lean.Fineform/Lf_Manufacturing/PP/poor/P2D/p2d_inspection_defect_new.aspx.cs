using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor.P2D
{
    public partial class p2d_inspection_defect_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DInspDefectNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable DefDatatable;

        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;

        public static string userid;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string Proinspdate, Promodel, Propcbtype, Provisualtype, Provctype, Prosideadate, Prosidebdate, Prodshiftname, Procensor, Proorder, Prolot, Proorderqty, Prorealqty, Proispqty, Propcbchecktype, Prolinename, Proinsqtime, Proaoitime, Probadqty, Prohandle, Probadserial, Probadcontent, Probadtype;

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
            //IQueryable<pp_defect_P2d> q = DB.Pp_P2d_Inspection_Defects;
            //DefDatatable = CopyToDataTable(q);

            //获取SQL数据表
            DefDatatable = ConvertHelper.GetDataTable("SELECT * FROM Pp_P2d_Inspection_Defect");

            //DefDatatable = ConvertHelper.LinqConvertToDataTable(q);

            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 新增数据初始值
            JObject defaultObj = new JObject();
            defaultObj.Add("Proinsqtime", "0");
            defaultObj.Add("Proaoitime", "0");
            defaultObj.Add("Probadqty", "1");
            defaultObj.Add("Prolinename", "SMT");
            defaultObj.Add("Probadcontent", "-");
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

            BindDdlOrder();
            BindDdlVisualtype();
            BindDdlVctype();
            //BindDdlLine();
            BindDdlShiftname();
            BindDdlCensor();
            BindDdlPcbType();
            BindDdlChecktype();
            //BindDdlHandle();
            BindDdlBadtype();
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.选择项中没有的选项请联系电脑课添加</div>");
        }

        private void BindGrid()
        {
            IQueryable<Pp_P2d_Inspection_Defect> q = DB.Pp_P2d_Inspection_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string ddate = this.DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            //q = q.Where(u => u.isDeleted == 0 && u.Prolinename.Contains(prolinename.SelectedItem.Text) && u.Proorder.Contains(proorder.SelectedItem.Text) && u.Prodate.Contains(ddate));

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
            q = SortAndPage<Pp_P2d_Inspection_Defect>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();

            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
        }

        #endregion Page_Load

        #region BindDdl Dropdown ListData

        //查询LOT
        private void BindDdlOrder()
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
            proorder.DataSource = qs;
            proorder.DataTextField = "Porderno";
            proorder.DataValueField = "Porderno";
            proorder.DataBind();
            this.proorder.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        //A:PCB个所,B:责任单位,C:不良性质,E:板别,D:组立个所,F:PCBA,G:修理员,H:検査員,J:VC线别,K:检查状况,L:检出工程,M:目视别,P:未达成,S:停线
        //目视
        private void BindDdlVisualtype()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_m")
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
            ddlProvisualtype.DataSource = qs;
            ddlProvisualtype.DataTextField = "DictLabel";
            ddlProvisualtype.DataValueField = "DictValue";
            ddlProvisualtype.DataBind();
        }

        //VC
        private void BindDdlVctype()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_j")
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
            ddlProvctype.DataSource = qs;
            ddlProvctype.DataTextField = "DictLabel";
            ddlProvctype.DataValueField = "DictValue";
            ddlProvctype.DataBind();
        }

        //查询线别
        //private void BindDdlLine()

        //{
        //    if (DefDate.SelectedDate.HasValue)
        //    {
        //        Proinspdate = DefDate.SelectedDate.Value.ToString("yyyyMMdd");

        //        //查询LINQ去重复
        //        var q = from a in DB.Pp_P2d_OutputSubs
        //                where a.Proorder.Contains(proorder.SelectedItem.Text)
        //                //join b in DB.proEcnSubs on a.Porderhbn equals b.Proecnbomitem
        //                //where b.Proecnno == strecn
        //                //where a.Prodate.Contains(Prodate) && !(from d in DB.Pp_P2d_Inspection_Defects
        //                //                                       where d.isDeleted == 0
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

        //  查询班别
        private void BindDdlShiftname()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_n")
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
            ddlProdshiftname.DataSource = qs;
            ddlProdshiftname.DataTextField = "DictLabel";
            ddlProdshiftname.DataValueField = "DictValue";
            ddlProdshiftname.DataBind();
        }

        //查询检查员
        private void BindDdlCensor()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_g")
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
            ddlProcensor.DataSource = qs;
            ddlProcensor.DataTextField = "DictLabel";
            ddlProcensor.DataValueField = "DictValue";
            ddlProcensor.DataBind();
        }

        //板别
        private void BindDdlPcbType()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_f")
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
            ddlPropcbtype.DataSource = qs;
            ddlPropcbtype.DataTextField = "DictLabel";
            ddlPropcbtype.DataValueField = "DictValue";
            ddlPropcbtype.DataBind();
        }

        //检查状况
        private void BindDdlChecktype()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_k")
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
            ddlPropcbchecktype.DataSource = qs;
            ddlPropcbchecktype.DataTextField = "DictLabel";
            ddlPropcbchecktype.DataValueField = "DictValue";
            ddlPropcbchecktype.DataBind();
        }

        //手贴
        //private void BindDdlHandle()
        //{
        //    //查询LINQ去重复
        //    var q = from a in DB.Pp_Reasons
        //            where a.Reasontype == "E"
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
        //    ddlProhandle.DataSource = qs;
        //    ddlProhandle.DataTextField = "Reasoncntext";
        //    ddlProhandle.DataValueField = "Reasoncntext";
        //    ddlProhandle.DataBind();

        //}
        //个所
        private void BindDdlBadtype()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.DictType.Contains("reason_type_a")
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
            ddlProbadtype.DataSource = qs;
            ddlProbadtype.DataTextField = "DictLabel";
            ddlProbadtype.DataValueField = "DictValue";
            ddlProbadtype.DataBind();
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
                Pp_P2d_Inspection_Defect current = DB.Pp_P2d_Inspection_Defects.Find(del_ID);
                //删除日志
                string Contectext = current.ID + "," + current.Proinspdate + "," + current.Promodel + "," + current.Propcbtype + "," + current.Provisualtype + "," + current.Provctype + "," + current.Prosideadate + "," + current.Prosidebdate + "," + current.Prodshiftname + "," + current.Procensor + "," + current.Proorder + "," + current.Prolot + "," + current.Proorderqty + "," + current.Prorealqty + "," + current.Proispqty + "," + current.Propcbchecktype + "," + current.Prolinename + "," + current.Proinsqtime + "," + current.Proaoitime + "," + current.Probadqty + "," + current.Prohandle + "," + current.Probadserial + "," + current.Probadcontent + "," + current.Probadtype;
                string OperateType = "删除";
                string OperateNotes = "Del生产不良* " + Contectext + " *Del生产不良 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                //删除记录
                //DB.Pp_P2d_Inspection_Defects.Where(l => l.ID == del_ID).Delete();

                current.isDeleted = 1;
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
            //BindDdlLine();
        }

        #endregion Events

        protected void ddlProlinename_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProlinename.SelectedIndex != 0 && ddlProlinename.SelectedIndex != -1)
            {
                var q = from a in DB.Pp_P2d_OutputSubs
                        where a.Proorder.CompareTo(proorder.SelectedItem.Text) == 0
                        where a.Prolinename.CompareTo(ddlProlinename.SelectedItem.Text) == 0
                        select a;
                var qs = q.Take(1).ToList();
                if (qs.Any())
                {
                    numProrealqty.Text = qs[0].Prorealqty.ToString();
                    //numProrealqty.Text = "1000";
                }

                //BindDdlproOrder();
            }
        }

        protected void proorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (proorder.SelectedIndex != -1 && proorder.SelectedIndex != 0)
            {
                this.prolot.Text = proorder.SelectedItem.Text;
                string sorder = proorder.SelectedItem.Text;

                var q = from a in DB.Pp_Orders
                        where a.Porderno.CompareTo(sorder) == 0
                        select a;
                var qs = q.ToList();
                if (qs.Any())
                {
                    //this.proorder.Text = sorder;
                    this.proorderqty.Text = qs[0].Porderqty.ToString();// proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1);

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

                //BindDdlLine();
                BindDdlPcbType();
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
                Pp_P2d_Inspection_Defect item = DB.Pp_P2d_Inspection_Defects
                .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string Contectext = item.ID + "," + item.Proinspdate + "," + item.Promodel + "," + item.Propcbtype + "," + item.Provisualtype + "," + item.Provctype + "," + item.Prosideadate + "," + item.Prosidebdate + "," + item.Prodshiftname + "," + item.Procensor + "," + item.Proorder + "," + item.Prolot + "," + item.Proorderqty + "," + item.Prorealqty + "," + item.Proispqty + "," + item.Propcbchecktype + "," + item.Prolinename + "," + item.Proinsqtime + "," + item.Proaoitime + "," + item.Probadqty + "," + item.Prohandle + "," + item.Probadserial + "," + item.Probadcontent + "," + item.Probadtype;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Contectext + " *Del 的记录可能将被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不具合管理", "不具合删除", OperateNotes);

                item.isDeleted = 1;
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

            //mysql = "select * from [dbo].[Pp_P2d_Inspection_Defects];";
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
                            Proinspdate = tb.Rows[tb.Rows.Count - 1]["Proinspdate"].ToString();  //检查日期
                            Promodel = tb.Rows[tb.Rows.Count - 1]["Promodel"].ToString(); //机种
                            Propcbtype = tb.Rows[tb.Rows.Count - 1]["Propcbtype"].ToString();   //板别
                            Provisualtype = tb.Rows[tb.Rows.Count - 1]["Provisualtype"].ToString();    //目视
                            Provctype = tb.Rows[tb.Rows.Count - 1]["Provctype"].ToString();    //VC
                            Prosideadate = tb.Rows[tb.Rows.Count - 1]["Prosideadate"].ToString(); //A面
                            Prosidebdate = tb.Rows[tb.Rows.Count - 1]["Prosidebdate"].ToString(); //B面
                            Prodshiftname = tb.Rows[tb.Rows.Count - 1]["Prodshiftname"].ToString();    //班别
                            Procensor = tb.Rows[tb.Rows.Count - 1]["Procensor"].ToString();    //检查员
                            Proorder = tb.Rows[tb.Rows.Count - 1]["Proorder"].ToString(); //订单
                            Prolot = tb.Rows[tb.Rows.Count - 1]["Prolot"].ToString();   //批次
                            Proorderqty = tb.Rows[tb.Rows.Count - 1]["Proorderqty"].ToString();  //数量
                            Prorealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();   //生产
                            Proispqty = tb.Rows[tb.Rows.Count - 1]["Proispqty"].ToString();    //检查
                            Propcbchecktype = tb.Rows[tb.Rows.Count - 1]["Propcbchecktype"].ToString();  //检查状态
                            Prolinename = tb.Rows[tb.Rows.Count - 1]["Prolinename"].ToString();  //线别
                            Proinsqtime = tb.Rows[tb.Rows.Count - 1]["Proinsqtime"].ToString();  //检查工数
                            Proaoitime = tb.Rows[tb.Rows.Count - 1]["Proaoitime"].ToString();   //AOI工数
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();    //不良数量
                            Prohandle = tb.Rows[tb.Rows.Count - 1]["Prohandle"].ToString();    //手贴
                            Probadserial = tb.Rows[tb.Rows.Count - 1]["Probadserial"].ToString(); //流水
                            Probadcontent = tb.Rows[tb.Rows.Count - 1]["Probadcontent"].ToString();    //内容
                            Probadtype = tb.Rows[tb.Rows.Count - 1]["Probadtype"].ToString();//个所
                        }
                        Pp_P2d_Inspection_Defect item = new Pp_P2d_Inspection_Defect();

                        item.Proinspdate = Proinspdate;
                        item.Promodel = Promodel;
                        item.Propcbtype = Propcbtype;
                        item.Provisualtype = Provisualtype;
                        item.Provctype = Provctype;
                        item.Prosideadate = DateTime.Parse(Prosideadate);
                        item.Prosidebdate = DateTime.Parse(Prosidebdate);
                        item.Prodshiftname = Prodshiftname;
                        item.Procensor = Procensor;
                        item.Proorder = Proorder;
                        item.Prolot = Prolot;
                        item.Proorderqty = int.Parse(Proorderqty);
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Proispqty = int.Parse(Proispqty);
                        item.Propcbchecktype = Propcbchecktype;
                        item.Prolinename = Prolinename;
                        item.Proinsqtime = int.Parse(Proinsqtime);
                        item.Proaoitime = int.Parse(Proaoitime);
                        item.Probadqty = int.Parse(Probadqty);
                        item.Prohandle = Prohandle;
                        item.Probadserial = Probadserial;
                        item.Probadcontent = Probadcontent;
                        item.Probadtype = Probadtype;

                        item.isDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Inspection_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + Proinspdate + "," + Promodel + "," + Propcbtype + "," + Provisualtype + "," + Provctype + "," + Prosideadate + "," + Prosidebdate + "," + Prodshiftname + "," + Procensor + "," + Proorder + "," + Prolot + "," + Proorderqty + "," + Prorealqty + "," + Proispqty + "," + Propcbchecktype + "," + Prolinename + "," + Proinsqtime + "," + Proaoitime + "," + Probadqty + "," + Prohandle + "," + Probadserial + "," + Probadcontent + "," + Probadtype;
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
                //    DB.Pp_P2d_Inspection_Defects.Add(item);
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
                            Proinspdate = tb.Rows[tb.Rows.Count - 1]["Proinspdate"].ToString();  //检查日期
                            Promodel = tb.Rows[tb.Rows.Count - 1]["Promodel"].ToString(); //机种
                            Propcbtype = tb.Rows[tb.Rows.Count - 1]["Propcbtype"].ToString();   //板别
                            Provisualtype = tb.Rows[tb.Rows.Count - 1]["Provisualtype"].ToString();    //目视
                            Provctype = tb.Rows[tb.Rows.Count - 1]["Provctype"].ToString();    //VC
                            Prosideadate = tb.Rows[tb.Rows.Count - 1]["Prosideadate"].ToString(); //A面
                            Prosidebdate = tb.Rows[tb.Rows.Count - 1]["Prosidebdate"].ToString(); //B面
                            Prodshiftname = tb.Rows[tb.Rows.Count - 1]["Prodshiftname"].ToString();    //班别
                            Procensor = tb.Rows[tb.Rows.Count - 1]["Procensor"].ToString();    //检查员
                            Proorder = tb.Rows[tb.Rows.Count - 1]["Proorder"].ToString(); //订单
                            Prolot = tb.Rows[tb.Rows.Count - 1]["Prolot"].ToString();   //批次
                            Proorderqty = tb.Rows[tb.Rows.Count - 1]["Proorderqty"].ToString();  //数量
                            Prorealqty = tb.Rows[tb.Rows.Count - 1]["Prorealqty"].ToString();   //生产
                            Proispqty = tb.Rows[tb.Rows.Count - 1]["Proispqty"].ToString();    //检查
                            Propcbchecktype = tb.Rows[tb.Rows.Count - 1]["Propcbchecktype"].ToString();  //检查状态
                            Prolinename = tb.Rows[tb.Rows.Count - 1]["Prolinename"].ToString();  //线别
                            Proinsqtime = tb.Rows[tb.Rows.Count - 1]["Proinsqtime"].ToString();  //检查工数
                            Proaoitime = tb.Rows[tb.Rows.Count - 1]["Proaoitime"].ToString();   //AOI工数
                            Probadqty = tb.Rows[tb.Rows.Count - 1]["Probadqty"].ToString();    //不良数量
                            Prohandle = tb.Rows[tb.Rows.Count - 1]["Prohandle"].ToString();    //手贴
                            Probadserial = tb.Rows[tb.Rows.Count - 1]["Probadserial"].ToString(); //流水
                            Probadcontent = tb.Rows[tb.Rows.Count - 1]["Probadcontent"].ToString();    //内容
                            Probadtype = tb.Rows[tb.Rows.Count - 1]["Probadtype"].ToString();//个所
                        }
                        Pp_P2d_Inspection_Defect item = new Pp_P2d_Inspection_Defect();

                        item.Proinspdate = Proinspdate;
                        item.Promodel = Promodel;
                        item.Propcbtype = Propcbtype;
                        item.Provisualtype = Provisualtype;
                        item.Provctype = Provctype;
                        item.Prosideadate = DateTime.Parse(Prosideadate);
                        item.Prosidebdate = DateTime.Parse(Prosidebdate);
                        item.Prodshiftname = Prodshiftname;
                        item.Procensor = Procensor;
                        item.Proorder = Proorder;
                        item.Prolot = Prolot;
                        item.Proorderqty = int.Parse(Proorderqty);
                        item.Prorealqty = int.Parse(Prorealqty);
                        item.Proispqty = int.Parse(Proispqty);
                        item.Propcbchecktype = Propcbchecktype;
                        item.Prolinename = Prolinename;
                        item.Proinsqtime = int.Parse(Proinsqtime);
                        item.Proaoitime = int.Parse(Proaoitime);
                        item.Probadqty = int.Parse(Probadqty);
                        item.Prohandle = Prohandle;
                        item.Probadserial = Probadserial;
                        item.Probadcontent = Probadcontent;
                        item.Probadtype = Probadtype;

                        item.isDeleted = 0;
                        item.Remark = "";
                        item.GUID = Guid.NewGuid();

                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();

                        DB.Pp_P2d_Inspection_Defects.Add(item);
                        DB.SaveChanges();

                        //新建日志
                        string Contectext = item.GUID + "," + Proinspdate + "," + Promodel + "," + Propcbtype + "," + Provisualtype + "," + Provctype + "," + Prosideadate + "," + Prosidebdate + "," + Prodshiftname + "," + Procensor + "," + Proorder + "," + Prolot + "," + Proorderqty + "," + Prorealqty + "," + Proispqty + "," + Propcbchecktype + "," + Prolinename + "," + Proinsqtime + "," + Proaoitime + "," + Probadqty + "," + Prohandle + "," + Probadserial + "," + Probadcontent + "," + Probadtype;
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
                //    DB.Pp_P2d_Inspection_Defects.Add(item);
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
            //班别
            rowData["Prodshiftname"] = ddlProdshiftname.SelectedItem.Text;
            //生产日期
            rowData["Proinspdate"] = DefDate.SelectedDate.Value.ToString("yyyyMMdd");
            //订单台数
            rowData["Proorderqty"] = (int)decimal.Parse(proorderqty.Text.ToString());
            //机种台数
            //rowData["Promodelqty"] = proorder.Text;
            //生产台数
            rowData["Prorealqty"] = (int)decimal.Parse(proorderqty.Text);
            //无不良台数
            // rowData["Pronobadqty"] = 0;// (int)decimal.Parse(pronobadqty.Text);

            //板别
            rowData["Propcbtype"] = ddlPropcbtype.SelectedItem.Text;

            //目视
            rowData["Provisualtype"] = ddlProvisualtype.SelectedItem.Text;
            //VC
            rowData["Provctype"] = ddlProvctype.SelectedItem.Text;
            //A面
            if (dpProsideadate.SelectedDate == null)
            { rowData["Prosideadate"] = DateTime.Parse("1900-01-01"); }
            else
            {
                rowData["Prosideadate"] = dpProsideadate.SelectedDate;
            }

            //B面
            if (dpProsidebdate.SelectedDate == null)
            { rowData["Prosidebdate"] = DateTime.Parse("1900-01-01"); }
            else
            {
                rowData["Prosidebdate"] = dpProsidebdate.SelectedDate;
            }
            //班别
            rowData["Prodshiftname"] = ddlProdshiftname.SelectedItem.Text;
            //检查
            rowData["Procensor"] = ddlProcensor.SelectedItem.Text;
            //生产数
            rowData["Prorealqty"] = (int)decimal.Parse(numProrealqty.Text);
            //检查数
            rowData["Proispqty"] = (int)decimal.Parse(numProispqty.Text);

            //检查状态
            rowData["Propcbchecktype"] = ddlPropcbchecktype.SelectedItem.Text;

            //线别
            rowData["Prolinename"] = ddlProlinename.SelectedItem.Text;
            //检查工数
            rowData["Proinsqtime"] = 0;
            //AOI工数
            rowData["Proaoitime"] = 0;
            //不良台数
            rowData["Probadqty"] = 0;

            //手贴
            //    if (ddlProhandle.SelectedIndex!=0&& ddlProhandle.SelectedIndex !=-1)
            //    {
            //        rowData["Prohandle"] = ddlProhandle.SelectedItem.Text;
            //}
            rowData["Prohandle"] = txtProhandle.Text;
            //流水
            rowData["Probadserial"] = txtProbadserial.Text;

            //内容
            rowData["Probadcontent"] = txtProbadcontent.Text;

            //不良个所
            rowData["Probadtype"] = ddlProbadtype.SelectedItem.Text;

            rowData["isDeleted"] = 0;

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
            Pp_P2d_Inspection_Defect item = DB.Pp_P2d_Inspection_Defects

                .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改前日志
            string BeforeContectext = item.ID + "," + item.Proinspdate + "," + item.Promodel + "," + item.Propcbtype + "," + item.Provisualtype + "," + item.Provctype + "," + item.Prosideadate + "," + item.Prosidebdate + "," + item.Prodshiftname + "," + item.Procensor + "," + item.Proorder + "," + item.Prolot + "," + item.Proorderqty + "," + item.Prorealqty + "," + item.Proispqty + "," + item.Propcbchecktype + "," + item.Prolinename + "," + item.Proinsqtime + "," + item.Proaoitime + "," + item.Probadqty + "," + item.Prohandle + "," + item.Probadserial + "," + item.Probadcontent + "," + item.Probadtype;
            string BeforeOperateType = "修改";
            string BeforeOperateNotes = "beEdit生产不良* " + BeforeContectext + " *beEdit生产不良 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, BeforeOperateType, "不具合管理", "不具合修改", BeforeOperateNotes);

            //板别
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
            //目视
            if (rowDict.ContainsKey("Provisualtype"))
            {
                rowData["Provisualtype"] = rowDict["Provisualtype"];
                if (string.IsNullOrEmpty(rowData["Provisualtype"].ToString()))
                {
                    item.Provisualtype = "";
                    //Alert.ShowInTop("目视不能为空！");
                    //return;
                }
                else
                {
                    item.Provisualtype = rowData["Provisualtype"].ToString();
                    //RealQty = rowData["Proclassmatter"].ToString();
                    //
                    //OrderFinish();
                }
            }
            //VC
            if (rowDict.ContainsKey("Provctype"))
            {
                rowData["Provctype"] = rowDict["Provctype"];
                if (string.IsNullOrEmpty(rowData["Provctype"].ToString()))
                {
                    item.Provctype = "";
                    //Alert.ShowInTop("VC不能为空！");
                    //return;
                }
                else
                {
                    item.Provctype = rowData["Provctype"].ToString();
                    //StopCheck = rowData["Prongmatter"].ToString();
                }
            }
            //A
            if (rowDict.ContainsKey("Prosideadate"))
            {
                rowData["Prosideadate"] = rowDict["Prosideadate"];
                if (string.IsNullOrEmpty(rowData["Prosideadate"].ToString()))
                {
                    item.Prosideadate = null;// DateTime.Parse("9999/12/31");
                    //Alert.ShowInTop("板别不能为空！");
                    //return;
                }
                else
                {
                    item.Prosideadate = DateTime.Parse(rowData["Prosideadate"].ToString());

                    //StopMinute = rowData["Probadqty"].ToString();
                }
            }
            //B
            if (rowDict.ContainsKey("Prosidebdate"))
            {
                rowData["Prosidebdate"] = rowDict["Prosidebdate"];
                if (string.IsNullOrEmpty(rowData["Prosidebdate"].ToString()))
                {
                    item.Prosidebdate = null;
                    //Alert.ShowInTop("板别不能为空！");
                    //return;
                }
                else
                {
                    item.Prosidebdate = DateTime.Parse(rowData["Probadtotal"].ToString());
                    //StopText = rowData["Probadtotal"].ToString();
                }
            }
            //班别
            if (rowDict.ContainsKey("Prodshiftname"))
            {
                rowData["Prodshiftname"] = rowDict["Prodshiftname"];
                if (string.IsNullOrEmpty(rowData["Prodshiftname"].ToString()))
                {
                    Alert.ShowInTop("班别不能为空！");
                    return;
                }
                else
                {
                    item.Prodshiftname = rowData["Prodshiftname"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //检查人员
            if (rowDict.ContainsKey("Procensor"))
            {
                rowData["Procensor"] = rowDict["Procensor"];
                if (string.IsNullOrEmpty(rowData["Procensor"].ToString()))
                {
                    Alert.ShowInTop("板别不能为空！");
                    return;
                }
                else
                {
                    item.Procensor = rowData["Procensor"].ToString();

                    //ResonText = rowData["Probadnote"].ToString();
                }
            }
            //实绩
            if (rowDict.ContainsKey("Prorealqty"))
            {
                rowData["Prorealqty"] = rowDict["Prorealqty"];
                if (string.IsNullOrEmpty(rowData["Prorealqty"].ToString()))
                {
                    Alert.ShowInTop("生产实绩不能为空！");
                    return;
                }
                else
                {
                    item.Prorealqty = int.Parse(rowData["Prorealqty"].ToString());
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //检查数
            if (rowDict.ContainsKey("Proispqty"))
            {
                rowData["Proispqty"] = rowDict["Proispqty"];
                if (string.IsNullOrEmpty(rowData["Proispqty"].ToString()))
                {
                    Alert.ShowInTop("检查数不能为空！");
                    return;
                }
                else
                {
                    item.Proispqty = int.Parse(rowData["Proispqty"].ToString());
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //检查状况
            if (rowDict.ContainsKey("Propcbchecktype"))
            {
                rowData["Propcbchecktype"] = rowDict["Propcbchecktype"];
                if (string.IsNullOrEmpty(rowData["Propcbchecktype"].ToString()))
                {
                    Alert.ShowInTop("检查状况不能为空！");
                    return;
                }
                else
                {
                    item.Propcbchecktype = rowData["Propcbchecktype"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //检查工数
            if (rowDict.ContainsKey("Proinsqtime"))
            {
                rowData["Proinsqtime"] = rowDict["Proinsqtime"];
                if (string.IsNullOrEmpty(rowData["Proinsqtime"].ToString()))
                {
                    item.Proinsqtime = 0;
                    //Alert.ShowInTop("检查工数不能为空！");
                    //return;
                }
                else
                {
                    item.Proinsqtime = int.Parse(rowData["Proinsqtime"].ToString());
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //AOI工数
            if (rowDict.ContainsKey("Proaoitime"))
            {
                rowData["Proaoitime"] = rowDict["Proaoitime"];
                if (string.IsNullOrEmpty(rowData["Proaoitime"].ToString()))
                {
                    item.Proaoitime = 0;
                    //Alert.ShowInTop("AOI工数不能为空！");
                    //return;
                }
                else
                {
                    item.Proaoitime = int.Parse(rowData["Proaoitime"].ToString());
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //不良数量
            if (rowDict.ContainsKey("Probadqty"))
            {
                rowData["Probadqty"] = rowDict["Probadqty"];
                if (string.IsNullOrEmpty(rowData["Probadqty"].ToString()))
                {
                    Alert.ShowInTop("不良数量不能为空！");
                    return;
                }
                else
                {
                    item.Probadqty = int.Parse(rowData["Probadqty"].ToString());
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //手贴
            if (rowDict.ContainsKey("Prohandle"))
            {
                rowData["Prohandle"] = rowDict["Prohandle"];
                if (string.IsNullOrEmpty(rowData["Prohandle"].ToString()))
                {
                    item.Prohandle = "";
                    //Alert.ShowInTop("AOI工数不能为空！");
                    //return;
                }
                else
                {
                    item.Prohandle = rowData["Prohandle"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //流水
            if (rowDict.ContainsKey("Probadserial"))
            {
                rowData["Probadserial"] = rowDict["Probadserial"];
                if (string.IsNullOrEmpty(rowData["Probadserial"].ToString()))
                {
                    item.Probadserial = "";
                    //Alert.ShowInTop("AOI工数不能为空！");
                    //return;
                }
                else
                {
                    item.Probadserial = rowData["Probadserial"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //内容
            if (rowDict.ContainsKey("Probadcontent"))
            {
                rowData["Probadcontent"] = rowDict["Probadcontent"];
                if (string.IsNullOrEmpty(rowData["Probadcontent"].ToString()))
                {
                    //item.Prohandle = "";
                    Alert.ShowInTop("内容不能为空！");
                    return;
                }
                else
                {
                    item.Probadcontent = rowData["Probadcontent"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            //个所
            if (rowDict.ContainsKey("Probadtype"))
            {
                rowData["Probadtype"] = rowDict["Probadtype"];
                if (string.IsNullOrEmpty(rowData["Probadtype"].ToString()))
                {
                    //item.Prohandle = "";
                    Alert.ShowInTop("个所不能为空！");
                    return;
                }
                else
                {
                    item.Probadtype = rowData["Probadtype"].ToString();
                }
                //ResonText = rowData["Probadnote"].ToString();
            }
            item.ModifyDate = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Pp_P2d_Inspection_Defect edititem = DB.Pp_P2d_Inspection_Defects

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterContectext = edititem.ID + "," + edititem.Proinspdate + "," + edititem.Promodel + "," + edititem.Propcbtype + "," + edititem.Provisualtype + "," + edititem.Provctype + "," + edititem.Prosideadate + "," + edititem.Prosidebdate + "," + edititem.Prodshiftname + "," + edititem.Procensor + "," + edititem.Proorder + "," + edititem.Prolot + "," + edititem.Proorderqty + "," + edititem.Prorealqty + "," + edititem.Proispqty + "," + edititem.Propcbchecktype + "," + edititem.Prolinename + "," + edititem.Proinsqtime + "," + edititem.Proaoitime + "," + edititem.Probadqty + "," + edititem.Prohandle + "," + edititem.Probadserial + "," + edititem.Probadcontent + "," + edititem.Probadtype;

            string AfterOperateType = "修改";
            string AfterOperateNotes = "afEdit生产不良* " + AfterContectext + " *afEdit生产不良 的记录已经将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, AfterOperateType, "不具合管理", "不具合修改", AfterOperateNotes);
        }

        //获取新增行内容
        private void InsertDefectDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            // 区分
            UpdateDataRow("Prongdept", rowDict, rowData);

            // 种类
            //UpdateDataRow("Proclassmatter", rowDict, rowData);

            // 类别
            //UpdateDataRow("Prongmatter", rowDict, rowData);

            // 数量
            UpdateDataRow("Probadqty", rowDict, rowData);

            // 总数
            //UpdateDataRow("Probadtotal", rowDict, rowData);

            // 对策
            UpdateDataRow("Probadnote", rowDict, rowData);

            UpdateDataRow("UDF01", rowDict, rowData);
            // 对策
            UpdateDataRow("Probadreason", rowDict, rowData);
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
            //mysql = "select* from[dbo].[Pp_P2d_Inspection_Defects];";
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
            //mysql = "select* from[dbo].[Pp_P2d_Inspection_Defects];";
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
            //mysql = "select * from [dbo].[Pp_P2d_Inspection_Defects];";
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
                //if (Decimal.Parse(pronobadqty.Text) > Decimal.Parse(prorealqty.Text))
                //{
                //    Alert.ShowInTop("无不良台数不能大于生产台数");
                //    return;

                //}

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
            //       .ForEach(x => { x.Pronobadqty = nobadqty; x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();

            //DB.Pp_P2d_Inspection_Defects
            //  .Where(s => s.Proorder == strPorder)
            //  .Where(s => s.Prodate.Substring(0, 6) == pdate)

            //  .ToList()
            //  .ForEach(x => { x.Prorealqty = rQty; x.ModiUser = GetIdentityName(); x.ModiTime = DateTime.Now; });
            //DB.SaveChanges();
            string order = proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1);

            //string lot = proorder.SelectedItem.Text.Substring(0, proorder.SelectedItem.Text.IndexOf(","));

            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(order, GetIdentityName());

            //更新不具合件数
            //UpdatingHelper.UpdatebadTotal(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, prolot.SelectedItem.Text.Substring(prolot.SelectedItem.Text.IndexOf(",") + 1, prolot.SelectedItem.Text.Length - prolot.SelectedItem.Text.IndexOf(",") - 1));
            //Common.UpdateDefectQty();
            //更新不具合合计
            //UpdatingHelper.UpdatebadAmount(this.DefDate.SelectedDate.Value.ToString("yyyyMMdd"), prolinename.SelectedItem.Text, proorder.SelectedItem.Text.Substring(proorder.SelectedItem.Text.IndexOf(",") + 1, proorder.SelectedItem.Text.Length - proorder.SelectedItem.Text.IndexOf(",") - 1), GetIdentityName());
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