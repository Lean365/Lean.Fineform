﻿using System;

//using EntityFramework.Extensions;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_daily : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string SmtParm, AiParm, HandParm, RepairParm;

        public static int SmtSwitchNum,
SmtSwitchTotalTime,
AitSwitchNum,
AiStopTime,
HandSopTime,
HandPerson,
HandSopTotalTime,
HandSwitchNum,
HandSwitchTime,
HandSwitchTotalTime,
RepairSopTime,
RepairPerson,
RepairSopTotalTime,
RepairSwitchNum,
RepairSwitchTime,
RepairSwitchTotalTime;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //更新P2D实时总量
            //UpdatingHelper.UpdateP2DRealTotals();
            //BindDdlGUID();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreP2DOutputNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(-1);//DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(-1);//DateTime.Today; //DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            btnP1dNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P2D/p2d_daily_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";
            BindDdlLine();
            BindGrid();
        }

        private void BindGrid()
        {
            var LineType = (from a in DB.Pp_P2d_OutputSubs
                                //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                                //where b.Proecnno == strecn
                                //where b.Proecnbomitem == stritem
                                //where a.DictType.Contains("line_type_p")
                            select new
                            {
                                DictLabel = a.Prolinename,
                                DictValue = a.Prolinename,
                            }).Distinct().ToList();
            IQueryable<Pp_P2d_OutputSub> q = DB.Pp_P2d_OutputSubs; //.Include(u => u.Dept);

            //string sdate = this.DpStartDate.SelectedDate.Value.ToString("yyyyMM");

            //q.Where(u => u.Prodate.Contains(sdate));

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.ID.ToString().Contains(searchText) || u.Proorder.ToString().Contains(searchText) || u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            }
            if (this.DdlLine.SelectedIndex != -1 && this.DdlLine.SelectedIndex != 0)
            {
                q = q.Where(u => u.Prolinename.Contains(this.DdlLine.SelectedText));
            }
            //查询包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.DictValue)).AsQueryable().OrderByDescending(u => u.Prodate);

            // q = q.Where(u => u.Promodel != "0");
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
            Grid1.RecordCount = q_include.Count();

            // 排列和数据库分页
            q = SortAndPage<Pp_P2d_OutputSub>(q_include, Grid1);

            Grid1.DataSource = q_include;
            Grid1.DataBind();
            //ttbSearchMessage.Text = "";
        }

        public void BindDdlLine()
        {
            var LineType = (from a in DB.Pp_P2d_OutputSubs
                                //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                                //where b.Proecnno == strecn
                                //where b.Proecnbomitem == stritem
                                //where a.DictType.Contains("line_type_p")
                            select new
                            {
                                DictLabel = a.Prolinename,
                                DictValue = a.Prolinename,
                            }).Distinct().ToList();
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_P2d_OutputSubs
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                    where a.Prodate.CompareTo(sdate) >= 0
                    where a.Prodate.CompareTo(edate) <= 0
                    select new
                    {
                        a.Prolinename
                    };

            //包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.DictValue));

            var qs = q_include.Select(E => new { E.Prolinename, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DdlLine.DataSource = qs;
            DdlLine.DataTextField = "Prolinename";
            DdlLine.DataValueField = "Prolinename";
            DdlLine.DataBind();

            this.DdlLine.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        #endregion Page_Load

        #region Changed

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlLine.SelectedIndex != -1 && DdlLine.SelectedIndex != 0)
            {
                BindGrid();
            }
        }

        #endregion Changed

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreP2DOutputDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
            CheckPowerWithLinkButtonField("CoreP2DOutputSubEdit", Grid1, "subeditField");
            CheckPowerWithLinkButtonField("CoreP2DOutputEdit", Grid1, "editField");
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
        }

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreOphDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);
        //    InsNetOperateNotes();
        //    // 执行数据库操作
        //    //DB.Adm_Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Adm_Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.Pp_P2d_Outputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.Pp_P2d_Outputs.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string tracestr;
            object[] keys = Grid1.DataKeys[e.RowIndex];

            if (keys[0] == null)
            {
                tracestr = ",";
            }
            else
            {
                tracestr = keys[0].ToString() + ",";
            }

            if (keys[1] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[1].ToString() + ",";
            }
            if (keys[2] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[2].ToString() + ",";
            }
            if (keys[3] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[3].ToString() + ",";
            }
            if (keys[4] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[4].ToString() + ",";
            }
            if (keys[5] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[5].ToString() + ",";
            }
            if (keys[6] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[6].ToString() + ",";
            }
            if (keys[7] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[7].ToString() + ",";
            }

            //每月10号
            //string Date10 = DateTime.Now.ToString("yyyyMM10");
            //string nowDate = DateTime.Now.ToString("yyyyMMdd");
            //将日期字符串转换为日期对象
            //第一天
            //DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime Date10 = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10));
            DateTime nowDate = Convert.ToDateTime((DateTime.Now));
            DateTime editDate = Convert.ToDateTime(DateTime.ParseExact(keys[2].ToString(), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));

            //上个月
            DateTime lastDate = DateTime.Now.AddMonths(-1);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(Date10, nowDate);
            //int compEdit= DateTime.Compare(nowDate, editDate);

            int compeditYear = editDate.Year - lastDate.Year;

            int compeditMonth = editDate.Month - lastDate.Month;

            if (compNum == -1)
            {
                if (compeditYear < 0)
                {
                    Alert.ShowInTop("旧数据不能再修改！");
                    return;
                }
                if (compeditYear == 0)
                {
                    if (compeditMonth == 0)
                    {
                        Alert.ShowInTop("每月10号，停止修改上月数据！");
                        return;
                    }
                    if (compeditMonth < 0)
                    {
                        Alert.ShowInTop("旧数据不能再修改！");
                        return;
                    }
                }
            }
            if (compNum == 0)
            {
                if (compeditYear < 0)
                {
                    Alert.ShowInTop("旧数据不能再修改！");
                    return;
                }
                if (compeditYear == 0)
                {
                    if (compeditMonth == 0)
                    {
                        Alert.ShowInTop("每月10号，停止修改上月数据！");
                        return;
                    }
                    if (compeditMonth < 0)
                    {
                        Alert.ShowInTop("旧数据不能再修改！");
                        return;
                    }
                }
            }

            if (e.CommandName == "PrintOph")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Report/daily_report.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "EditOph")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P2D/p2d_daily_edit.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "EditOphsub")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P2D/p2d_daily_sub_edit.aspx?Transtr=" + tracestr + "&type=1") + Window1.GetMaximizeReference());
            }
            int del_ID = GetSelectedDataKeyID(Grid1);

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
                Pp_P2d_Output current = DB.Pp_P2d_Outputs.Find(del_ID);
                string Contectext = current.ID.ToString() + "," + current.GUID.ToString();
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del生产* " + Contectext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除标记", OperateNotes);

                DB.Pp_P2d_OutputSubs.Where(l => l.Parent == del_ID).DeleteFromQuery();
                DB.Pp_P2d_Outputs.Where(l => l.ID == del_ID).DeleteFromQuery();

                //更新订单已生产数量
                //UpdatingHelper.DelUpdateOrderRealQty(current.Proorder, GetIdentityName());

                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), "修改", "生产管理", "生产订单修改", OperateNotes);
            }
            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindDdlLine();
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindDdlLine();
                BindGrid();
            }
        }

        #endregion Events

        #region Export

        protected void BtnList_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P2d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_P2d_Output_Details";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            var q =
                from p in DB.Pp_P2d_OutputSubs
                    //join b in DB.Pp_P2d_Outputs on p.GUID equals b.GUID
                where p.IsDeleted == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { p.Proorder, p.Proshort, p.Prost, p.Propcbatype, p.Prohandoffnum, p.Prohandofftime, p.Protime, p.Promaketime, p.Prodowntime, p.Prolosstime, p.Proorderqty, p.Propcbaside, p.Prodirect, p.Propcbastated, p.Prorealqty, p.Promodel, p.Prohbn, p.Prolot, p.Prodate, p.Prostime, p.Proetime, p.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin, p.UDF51, p.UDF52, p.UDF53, p.UDF54, p.Prorealtotal }
                into g
                select new
                {
                    g.Key.Proorder,
                    g.Key.Prodate,
                    g.Key.Proorderqty,
                    g.Key.Prostime,
                    g.Key.Proetime,
                    g.Key.Prodirect,
                    g.Key.Prolinename,
                    g.Key.Promodel,
                    g.Key.Prohbn,
                    g.Key.Prolot,
                    g.Key.Propcbatype,
                    g.Key.Propcbaside,
                    g.Key.Propcbastated,
                    g.Key.Prorealqty,
                    g.Key.Prorealtotal,
                    g.Key.Prorealtime,
                    g.Key.Prostopcou,
                    g.Key.Prostopmemo,
                    g.Key.Probadcou,
                    g.Key.Probadmemo,
                    g.Key.Prohandoffnum,
                    g.Key.Prohandofftime,
                    g.Key.Protime,
                    g.Key.Promaketime,
                    g.Key.Prodowntime,
                    g.Key.Prolinemin,
                    g.Key.Prolinestopmin,
                    g.Key.Proshort,
                    g.Key.Prolosstime,
                    g.Key.Prost,
                    g.Key.UDF51,
                    g.Key.UDF52,
                    g.Key.UDF53,
                    g.Key.UDF54,
                };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Proorder.ToString().Contains(searchText) || u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            }
            if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
            }
            //检查统计按订单+批次

            var insq = from p in DB.Pp_P2d_OutputSubs
                       join a in DB.Pp_P2d_Inspection_Defects on new
                       { p.Proorder, p.Prodate, p.Promodel, p.Propcbatype } equals new { a.Proorder, Prodate = a.Proinspdate, a.Promodel, Propcbatype = a.Propcbtype } into Inspection
                       from aa in Inspection.DefaultIfEmpty()

                       where p.IsDeleted == 0

                       select new
                       {
                           p.Prodate,
                           p.Proorder,
                           p.Propcbatype,
                           p.Promodel,
                           Probadqty = aa == null ? 0 : aa.Probadqty,
                       };
            var manq = from p in DB.Pp_P2d_OutputSubs
                       join a in DB.Pp_P2d_Manufacturing_Defects on new
                       { p.Proorder, p.Prodate, p.Promodel, p.Propcbatype } equals new { a.Proorder, a.Prodate, a.Promodel, Propcbatype = a.Propcbtype } into Manufacturing
                       from aa in Manufacturing.DefaultIfEmpty()
                       where p.IsDeleted == 0
                       select new
                       {
                           p.Prodate,
                           p.Proorder,
                           p.Propcbatype,
                           p.Promodel,
                           Probadqty = aa == null ? 0 : aa.Probadqty,
                       };
            var q_all = (from cust in insq
                         select cust)
                         .Union
                         (from emp in manq
                          select emp);

            var q_total = from p in q_all
                          group p by new { p.Proorder, p.Promodel, p.Propcbatype }
                         into g
                          select new
                          {
                              g.Key.Proorder,
                              g.Key.Promodel,
                              g.Key.Propcbatype,
                              //Prorealqty = g.Sum(p => p.Prorealqty),
                              Probadqty = g.Sum(p => p.Probadqty),
                          };
            var qorder = q.OrderBy(p => p.Prodate).ThenBy(s => s.Prolinename).ToList();
            var qs = from g in qorder
                     join bq in q_total on g.Promodel equals bq.Promodel into TotalQty
                     from aa in TotalQty.DefaultIfEmpty()
                     orderby g.Prodate
                     select new
                     {
                         生产日期 = g.Prodate,
                         单位 = "制二课",
                         班组 = g.Prolinename,
                         机种 = g.Promodel,
                         物料 = g.Prohbn,
                         批次 = g.Prolot,
                         工单 = g.Proorder,
                         板别 = g.Propcbatype != "" ? g.Propcbatype : "无区分",
                         生产面 = g.Propcbaside != "" ? g.Propcbaside : "无区分",
                         Lot数量 = g.UDF54,
                         生产实绩 = g.Prorealqty,
                         累计生产 = g == null ? 0 : g.Prorealtotal,
                         //不良件数 = aa == null ? 0 : aa.Probadqty,
                         完成状况 = g.Propcbastated,
                         总工数 = g.Protime,
                         切换次数 = g.Prohandoffnum,
                         切换时间 = g.Prohandofftime,
                         切停机时间 = g.Prodowntime,
                         停线原因 = g.Prostopcou,
                         原因说明 = g.Prostopmemo,
                         未达成 = g.Probadcou,
                         未达成原因 = g.Probadmemo,
                         修工数 = g.Prolosstime,
                         投入工数 = g.Promaketime,
                         不良台数 = g.UDF51,
                         修正仕损 = g.UDF52,
                         手插仕损 = g.UDF53,
                     };
            if (qs.Any())
            {
                //ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs.AsQueryable().Distinct()), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnDaily_Click(object sender, EventArgs e)
        {
            BindSwitchTimes();
            if (!String.IsNullOrEmpty(SmtParm))
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFineExport"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                if (DpStartDate.SelectedDate.Value != DpEndDate.SelectedDate.Value)
                {
                    Alert.ShowInTop("日报只能查询一天的数据！");
                    return;
                }

                //DataTable Exp = new DataTable();
                //在库明细查询SQL
                string Xlsbomitem, ExportFileName;

                // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P2d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
                Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd") + "_P2d_Output_Details";
                //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
                ExportFileName = Xlsbomitem + ".xlsx";

                var q =
                    from p in DB.Pp_P2d_OutputSubs
                        //join b in DB.Pp_P2d_Outputs on p.GUID equals b.GUID
                    where p.IsDeleted == 0
                    //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                    group p by new { p.Proorder, p.Proshort, p.Prost, p.Propcbatype, p.Prohandoffnum, p.Prohandofftime, p.Protime, p.Promaketime, p.Prodowntime, p.Prolosstime, p.Proorderqty, p.Propcbaside, p.Prodirect, p.Propcbastated, p.Prorealqty, p.Promodel, p.Prohbn, p.Prolot, p.Prodate, p.Prostime, p.Proetime, p.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin, p.UDF51, p.UDF52, p.UDF53, p.UDF54, p.Prorealtotal }
                    into g
                    select new
                    {
                        g.Key.Proorder,
                        g.Key.Prodate,
                        g.Key.Proorderqty,
                        g.Key.Prostime,
                        g.Key.Proetime,
                        g.Key.Prodirect,
                        g.Key.Prolinename,
                        g.Key.Promodel,
                        g.Key.Prohbn,
                        g.Key.Prolot,
                        g.Key.Propcbatype,
                        g.Key.Propcbaside,
                        g.Key.Propcbastated,
                        g.Key.Prorealqty,
                        g.Key.Prorealtotal,
                        g.Key.Prorealtime,
                        g.Key.Prostopcou,
                        g.Key.Prostopmemo,
                        g.Key.Probadcou,
                        g.Key.Probadmemo,
                        g.Key.Prohandoffnum,
                        g.Key.Prohandofftime,
                        g.Key.Protime,
                        g.Key.Promaketime,
                        g.Key.Prodowntime,
                        g.Key.Prolinemin,
                        g.Key.Prolinestopmin,
                        g.Key.Proshort,
                        g.Key.Prolosstime,
                        g.Key.Prost,
                        g.Key.UDF51,
                        g.Key.UDF52,
                        g.Key.UDF53,
                        g.Key.UDF54,
                    };

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Proorder.ToString().Contains(searchText) || u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
                if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
                }
                //检查统计按订单+批次

                var insq = from p in DB.Pp_P2d_OutputSubs
                           join a in DB.Pp_P2d_Inspection_Defects on new
                           { p.Proorder, p.Prodate, p.Promodel, p.Propcbatype } equals new { a.Proorder, Prodate = a.Proinspdate, a.Promodel, Propcbatype = a.Propcbtype } into Inspection
                           from aa in Inspection.DefaultIfEmpty()

                           where p.IsDeleted == 0

                           select new
                           {
                               p.Prodate,
                               p.Proorder,
                               p.Propcbatype,
                               p.Promodel,
                               Probadqty = aa == null ? 0 : aa.Probadqty,
                           };
                var manq = from p in DB.Pp_P2d_OutputSubs
                           join a in DB.Pp_P2d_Manufacturing_Defects on new
                           { p.Proorder, p.Prodate, p.Promodel, p.Propcbatype } equals new { a.Proorder, a.Prodate, a.Promodel, Propcbatype = a.Propcbtype } into Manufacturing
                           from aa in Manufacturing.DefaultIfEmpty()
                           where p.IsDeleted == 0
                           select new
                           {
                               p.Prodate,
                               p.Proorder,
                               p.Propcbatype,
                               p.Promodel,
                               Probadqty = aa == null ? 0 : aa.Probadqty,
                           };
                var q_all = (from cust in insq
                             select cust)
                             .Union
                             (from emp in manq
                              select emp);

                var q_total = from p in q_all
                              group p by new { p.Proorder, p.Promodel, p.Propcbatype }
                             into g
                              select new
                              {
                                  g.Key.Proorder,
                                  g.Key.Promodel,
                                  g.Key.Propcbatype,
                                  //Prorealqty = g.Sum(p => p.Prorealqty),
                                  Probadqty = g.Sum(p => p.Probadqty),
                              };
                var qorder = q.OrderBy(p => p.Prodate).ThenBy(s => s.Prolinename).ToList();
                var qs = from g in qorder
                         join bq in q_total on g.Promodel equals bq.Promodel into TotalQty
                         from aa in TotalQty.DefaultIfEmpty()
                         select new
                         {
                             生产日期 = g.Prodate,
                             单位 = "制二课",
                             班组 = g.Prolinename.Substring(1, g.Prolinename.Length - 1),
                             机种 = g.Promodel,
                             物料 = g.Prohbn,
                             板别 = g.Propcbatype != "" ? g.Propcbatype : "",
                             生产面 = g.Propcbaside != "" ? g.Propcbaside : "",
                             批次 = g.Prolot,
                             工单 = g.Proorder,

                             Lot数量 = g.UDF54,
                             生产实绩 = g.Prorealqty,
                             累计生产 = g == null ? 0 : g.Prorealtotal,
                             //不良件数 = aa == null ? 0 : aa.Probadqty,
                             完成状况 = g.Propcbastated,
                             总工数 = g.Protime,
                             当日切换次数 = g.Prohandoffnum,
                             当日切换时间 = g.Prohandofftime,
                             当日切停机时间 = g.Prodowntime,
                             停线原因 = g.Prostopcou,
                             原因说明 = g.Prostopmemo,
                             未达成 = g.Probadcou,
                             未达成原因 = g.Probadmemo,
                             修工数 = g.Prolosstime,
                             投入工数 = g.Promaketime,
                             不良台数 = g.UDF51,
                             修正仕损 = g.UDF52,
                             手插仕损 = g.UDF53,
                         };
                if (qs.Any())
                {
                    //ConvertHelper.LinqConvertToDataTable(qs);

                    Grid1.AllowPaging = false;
                    ExportHelper.SheetP2dDaily_EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs.AsQueryable().Distinct()), sdate, ExportFileName, DpStartDate.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd"), DpStartDate.SelectedDate.Value.ToString("yyyy-MM-dd"), System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DpStartDate.SelectedDate.Value.DayOfWeek), SmtParm, AiParm, HandParm, RepairParm);
                    Grid1.AllowPaging = true;
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_NoSwitchdata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        public void BindSwitchTimes()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from p in DB.Pp_P2d_Switch_Notes
                    where p.IsDeleted == 0
                    select p;
            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) == 0);
            }
            var qs = q.ToList();
            if (qs.Any())
            {
                SmtSwitchNum = qs[0].ProSmtSwitchNum;
                SmtSwitchTotalTime = qs[0].ProSmtSwitchTotalTime;
                AitSwitchNum = qs[0].ProAitSwitchNum;
                AiStopTime = qs[0].ProAiStopTime;
                HandSopTime = qs[0].ProHandSopTime;
                HandPerson = qs[0].ProHandPerson;
                HandSopTotalTime = qs[0].ProHandSopTotalTime;
                HandSwitchNum = qs[0].ProHandSwitchNum;
                HandSwitchTime = qs[0].ProHandSwitchTime;
                HandSwitchTotalTime = qs[0].ProHandSwitchTotalTime;
                RepairSopTime = qs[0].ProRepairSopTime;
                RepairPerson = qs[0].ProRepairPerson;
                RepairSopTotalTime = qs[0].ProRepairSopTotalTime;
                RepairSwitchNum = qs[0].ProRepairSwitchNum;
                RepairSwitchTime = qs[0].ProRepairSwitchTime;
                RepairSwitchTotalTime = qs[0].ProRepairSwitchTotalTime;

                SmtParm = "SMT切换" + SmtSwitchNum + "次,总切换时间" + SmtSwitchTotalTime + "分钟。";
                AiParm = "自插切换" + AitSwitchNum + "次,总切换时间" + AiStopTime + "分钟。";
                HandParm = "手插读取工程表6分钟，合计：" + HandPerson + "人*" + HandSopTime + "分=" + HandSopTotalTime + "分钟," + HandPerson + "人切换机种" + HandSwitchNum + "次" + HandSwitchTime + "分钟，合计：" + HandPerson + "人*" + HandSwitchTime + "分=" + HandSwitchTotalTime + "分钟。";
                RepairParm = "修正读取工程表6分钟，合计：" + RepairPerson + "人*" + RepairSopTime + "分=" + RepairSopTotalTime + "分钟," + RepairPerson + "人切换机种" + RepairSwitchNum + "次" + RepairSwitchTime + "分钟，合计：" + RepairPerson + "人*" + RepairSwitchTime + "分=" + RepairSwitchTotalTime + "分钟。";
            }
            //else

            //{
            //    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_NoSwitchdata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            //    return;
            //}
        }

        #endregion Export
    }
}