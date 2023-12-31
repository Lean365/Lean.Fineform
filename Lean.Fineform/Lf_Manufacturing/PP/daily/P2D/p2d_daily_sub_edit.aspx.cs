﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
//using EntityFramework.Extensions;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Text;
using System.IO;

using System.Text.RegularExpressions;
namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P2D
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

        #endregion

        #region Page_Load
        public static string mysql, strProorder, strProorderqty, strProlinename, strProdate, strProdirect, strProindirect, strProlot, strPromodel, strProhbn, strPropbctype, strProst, strProshort, strProrate, strProstdcapacity, strTotaltag, strProstime, strProetime, strProrealqty, strPropcbserial, strProlinestopmin, strProstopcou, strProstopmemo, strProbadcou, strProbadmemo, strProlinemin, strProrealtime, strProworkst, strProstdiff, strProqtydiff, strProratio, strUdf001, strUdf002, strUdf003, strUdf004, strUdf005, strUdf006, strisDelete, strRemark, strModifier, strModifyTime;
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

            Rate();
            MemoText.Text = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.生产工数为1时，参与OPH计划台数统计。</div><div>2.同一时段生产多个工单时，计划台数系统自动处理，作业工数计算方法：<strong>多个工单具体的实绩工数+停线时间</strong></div><div>3.实绩工数的计算公式：<strong>作业工数-停线时间</strong></div><div>4.实绩工数不能为负数</div><div>4.生产实绩不能超过工单数量</div>");
            //MemoText.Text = "填写说明" + Environment.NewLine + "1.作业工数为1时，参与OPH计划台数统计。" + Environment.NewLine +"2.同一时段多个工单生产时，";
        }



        private void BindGrid()
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

        #endregion

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


            // 生产实绩
            if (rowDict.ContainsKey("Prorealqty"))
            {
                rowData["Prorealqty"] = rowDict["Prorealqty"];
                item.Prorealqty = int.Parse(rowData["Prorealqty"].ToString());
                strProrealqty = rowData["Prorealqty"].ToString();

            }
            // 直接
            if (rowDict.ContainsKey("Prodirect"))
            {
                rowData["Prodirect"] = rowDict["Prodirect"];
                item.Prodirect = int.Parse(rowData["Prodirect"].ToString());
                strProdirect = rowData["Prodirect"].ToString();
            }
            // 间接
            if (rowDict.ContainsKey("Proindirect"))
            {
                rowData["Proindirect"] = rowDict["Proindirect"];
                item.Proindirect = int.Parse(rowData["Proindirect"].ToString());
                strProindirect = rowData["Proindirect"].ToString();
            }
            // 工数
            if (rowDict.ContainsKey("Prolinemin"))
            {
                rowData["Prolinemin"] = rowDict["Prolinemin"];
                item.Prolinemin = int.Parse( rowData["Prolinemin"].ToString());
                strProlinemin = rowData["Prolinemin"].ToString();
            }
            // 实绩工数
            if (rowDict.ContainsKey("Prorealtime"))
            {
                rowData["Prorealtime"] = rowDict["Prorealtime"];
                item.Prorealtime = int.Parse(rowData["Prorealtime"].ToString());
                strProrealtime = rowData["Prorealtime"].ToString();
            }
            // 实绩工数
            if (rowDict.ContainsKey("Propcbserial"))
            {
                rowData["Propcbserial"] = rowDict["Propcbserial"];
                item.Propcbserial = rowData["Propcbserial"].ToString();
                strPropcbserial = rowData["Propcbserial"].ToString();
            }
            item.Proworkst = 0;
                item.Prostdiff = 0;
                item.Proratio = 0;

            item.ModifyTime = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            


            //更新不良数据中的实绩生产数量，按日期，工单，班组
            UpdatingHelper.DefectRealqty_Update(item.Proorder, item.Prodate, item.Prolinename,userid);

            //更新不良集计数据中的实绩生产数量,按工单
            UpdatingHelper.DefectTotalRealqty_Update(item.Proorder, userid);

            //判断不良是否录入
            UpdatingHelper.CheckDefectData(item.Proorder, item.Prodate, item.Prolinename);

            //更新无不良台数
            UpdatingHelper.noDefectQty_Update(item.Proorder, userid);
            //更新订单已生产数量
            UpdatingHelper.UpdateOrderRealQty(item.Proorder, userid);

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
                        a.Propbctype,
                        a.Propcbserial,
                        a.Prorealtime,
                        a.Proworkst,
                        a.Prostdiff,
                        a.Proqtydiff,
                        a.Proratio,
                        a.Udf001,
                        a.Udf002,
                        a.Udf003,
                        a.Udf004,
                        a.Udf005,
                        a.Udf006,
                        a.isDelete,
                        a.Remark,
                        a.Creator,
                        a.CreateTime,
                        a.Modifier,
                        a.ModifyTime,
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
                if (!CheckPower("CoreOutputDelete"))
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
            CheckPowerWithLinkButtonField("CoreOutputDelete", Grid1, "deleteField");
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
            string Newtext = item.ID + "," + item.GUID + "," + item.Proetime + "," + item.Prostime + "," + item.Prorealqty + "," + item.Prodirect + "," + item.Proindirect + "," + item.Prorealtime + "," + item.Propcbserial ;
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


        #endregion




        #region DDLBindData





        #endregion



    }

}
