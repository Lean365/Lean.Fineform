using System;
using System.Collections.Generic;

//using EntityFramework.Extensions;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

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

        public static string mysql, strProordertype, strProorder, strProorderqty, strProlinename, strProdate, strProdirect, strProindirect,
                            strProlot, strPromodel, strProhbn, strPropcbatype, strPropcbaside,
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
        public static int ParentID, rowID;
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

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_new.aspx", "新增P1D生产日报");
            //btnP2dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p2d_new.aspx", "新增P2D生产日报");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            //ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
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

        private void BindDdlPcbaType()
        {
            //查询LINQ去重复
            var q = from a in DB.Mm_Materials
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.MatDescription.Contains("pcb")

                    select new
                    {
                        a.MatDescription,
                    };

            var qs = q.Select(E => new { E.MatDescription }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlPropcbatype.DataSource = qs;
            ddlPropcbatype.DataTextField = "MatDescription";
            ddlPropcbatype.DataValueField = "MatDescription";
            ddlPropcbatype.DataBind();
        }

        private void BindDdlProStated()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.DictType.Contains("app_pro_status")

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

        private void UpdateDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs

                .Where(u => u.ID == rowID).FirstOrDefault();

            Pp_P2d_Output main_item = DB.Pp_P2d_Outputs

                 .Where(u => u.ID == ParentID).FirstOrDefault();

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

        //// 根据行ID来获取行数据
        private DataRow FindRowByID(int rowID)
        {
            //只能查询非空表，否则不能获取ID，请使用ConvertHelper.GetDataTable()函数

            //IQueryable<Pp_P2d_OutputSub> q = DB.Pp_P2d_OutputSubs;
            //DataTable OptDatatable =ConvertHelper.IEnumerableConvertToDataTable(q);
            var q = from a in DB.Pp_P2d_OutputSubs
                    where a.Parent == ParentID
                    select new
                    {
                        a.ID,
                        a.GUID,
                        a.Prostime,
                        a.Proetime,
                        a.Prorealqty,
                        a.Prolinestopmin,
                        a.Prostopcou,
                        a.Prostopmemo,
                        a.Probadcou,
                        a.Probadmemo,
                        a.Prolinemin,
                        a.Protime,
                        a.Propcbaside,
                        a.Prolosstime,
                        a.Promaketime,
                        a.Prohandoffnum,
                        a.Prohandofftime,
                        a.Propcbastated,
                        a.Prodowntime,
                        a.Propcbatype,
                        a.Propcbserial,
                        a.Prorealtime,
                        a.Proworkst,
                        a.Prostdiff,
                        a.Proqtydiff,
                        a.Proratio,
                        a.UDF01,
                        a.UDF02,
                        a.UDF03,
                        a.UDF04,
                        a.UDF05,
                        a.UDF06,
                        a.isDeleted,
                        a.Remark,
                        a.Creator,
                        a.CreateDate,
                        a.Modifier,
                        a.ModifyDate,
                        Parent_ID = a.Parent,
                    };

            DataTable OptDatatable = ConvertHelper.LinqConvertToDataTable(q);
            foreach (DataRow row in OptDatatable.Rows)
            {
                if (Convert.ToInt32(row["Id"]) == rowID)
                {
                    return row;
                }
            }
            return null;
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
            // 修改的现有数据
            Dictionary<int, Dictionary<string, object>> modifiedDict = Grid1.GetModifiedDict();

            var ss = modifiedDict.Values.ToArray();

            foreach (int rowIndex in modifiedDict.Keys)
            {
                // //将泛型转换成object[]

                //object [] obqty= modifiedDict[rowIndex].Values.ToArray();
                // //获取object[]中的值
                // string strQty = obqty[0].ToString();
                //获取行ID
                rowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                //获取OPHID
                //Ophguid = Grid1.DataKeys[rowIndex][1].ToString();
                // //判断订单是否完工
                // OrderFinish();
                // if (ophqty + decimal.Parse(strQty) > orderqty)
                // {
                //     Alert alert = new Alert();
                //     alert.Message = "生产实绩已经大于订单数量,请确认！订单数量：[" + orderqty + "],已经生产：[" + ophqty + "],本次输入：[" + strQty + "]";
                //     alert.IconFont = IconFont.Warning;
                //     alert.Target = Target.Parent;
                //     Alert.ShowInTop();

                //     return;
                // }

                try
                {
                    DataRow row = FindRowByID(rowID);

                    UpdateDataRow(modifiedDict[rowIndex], row);

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

                afterInsNetOperateNotes();
            }

            BindGrid();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void beforeInsNetOperateNotes()
        {
            //修改前日志
            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs
            .Where(u => u.ID == rowID).FirstOrDefault();
            string Newtext = item.ID + "," + item.GUID + "," + item.Proetime + "," + item.Prostime + "," + item.Prorealqty + "," + item.Prodirect + "," + item.Proindirect + "," + item.Prorealtime + "," + item.Propcbserial;
            string OperateType = "修改";//操作标记
            string OperateNotes = "beEdit生产OPH_SUB*" + Newtext + " *beEdit生产OPH_SUB 的记录将被修改";

            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩修改", OperateNotes);
        }

        private void afterInsNetOperateNotes()
        {
            //修改后日志

            Pp_P2d_OutputSub item = DB.Pp_P2d_OutputSubs
                        .Where(u => u.ID == rowID).FirstOrDefault();

            string Newtext = item.ID + "," + item.GUID + "," + item.Proetime + "," + item.Prostime + "," + strProrealqty + "," + strProdirect + "," + strProdirect + "," + strProrealtime + "," + strPropcbserial;
            string OperateType = "修改";//操作标记
            string OperateNotes = "afEdit生产OPH_SUB* " + Newtext + " *afEdit生产OPH_SUB 的记录已被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩修改", OperateNotes);
        }

        #endregion Events
    }
}