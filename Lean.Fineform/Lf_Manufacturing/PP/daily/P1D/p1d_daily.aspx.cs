using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
//using EntityFramework.Extensions;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;

namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P1D
{
    public partial class p1d_daily : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DOutputView";
            }
        }

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {

            //BindDDLGUID();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreP1DOutputNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);
            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            btnP1dNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P1D/p1d_daily_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";
            BindDDLLine();
            BindGrid();
        }



        private void BindGrid()
        {
            var LineType = (from a in DB.Pp_Lines
                            where a.lineclass.Contains("M")
                            select new
                            {
                                a.linename
                            }).ToList();
            IQueryable<Pp_P1d_Output> q = DB.Pp_P1d_Outputs; //.Include(u => u.Dept);


            //string sdate = this.DPstart.SelectedDate.Value.ToString("yyyyMM");

            //q.Where(u => u.Prodate.Contains(sdate));


            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.ID.ToString().Contains(searchText) || u.Proorder.ToString().Contains(searchText) || u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            }

            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");


            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            }
            if (this.DDLline.SelectedIndex != -1 && this.DDLline.SelectedIndex != 0)
            {

                q = q.Where(u => u.Prolinename.Contains(this.DDLline.SelectedText));
            }
            //查询包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.linename)).AsQueryable();

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
            q = SortAndPage<Pp_P1d_Output>(q_include, Grid1);

            Grid1.DataSource = q_include;
            Grid1.DataBind();
            //ttbSearchMessage.Text = "";
        }
        public void BindDDLLine()
        {
            var LineType = (from a in DB.Pp_Lines
                            where a.lineclass.Contains("M")
                            select new
                            {
                                a.linename
                            }).ToList();
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_P1d_Outputs
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                    where a.Prodate.CompareTo(sdate) >= 0
                    where a.Prodate.CompareTo(edate) <= 0
                    select new
                    {
                        a.Prolinename

                    };


            //包含子集
            var q_include = q.AsEnumerable().Where(p => LineType.Any(g => p.Prolinename == g.linename));

            var qs = q_include.Select(E => new { E.Prolinename, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLline.DataSource = qs;
            DDLline.DataTextField = "Prolinename";
            DDLline.DataValueField = "Prolinename";
            DDLline.DataBind();

            this.DDLline.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));

        }

        #endregion

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            //BindDDLData();
            //DDLline.Items.Clear();
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            //BindDDLData();
            //DDLline.Items.Clear();
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
            CheckPowerWithLinkButtonField("CoreP1DOutputDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
            CheckPowerWithLinkButtonField("CoreP1DOutputSubEdit", Grid1, "subeditField");
            CheckPowerWithLinkButtonField("CoreP1DOutputEdit", Grid1, "editField");
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
        //    DB.Pp_P1d_Outputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.Pp_P1d_Outputs.Where(u => ids.Contains(u.ID)).Delete();





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
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P1D/p1d_daily_edit.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            }
            if (e.CommandName == "EditOphsub")
            {

                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/PP/daily/P1D/p1d_daily_sub_edit.aspx?Transtr=" + tracestr + "&type=1") + Window1.GetMaximizeReference());

            }
            int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreP1DOutputDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Pp_P1d_Output current = DB.Pp_P1d_Outputs.Find(del_ID);
                string Contectext = current.ID.ToString() + "," + current.GUID.ToString();
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del生产* " + Contectext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除标记", OperateNotes);
                
                DB.Pp_P1d_OutputSubs.Where(l => l.Parent == del_ID).DeleteFromQuery();
                DB.Pp_P1d_Outputs.Where(l => l.ID == del_ID).DeleteFromQuery();

                //更新订单已生产数量
                UpdatingHelper.DelUpdateOrderRealQty(current.Proorder, GetIdentityName());

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

        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindDDLLine();
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindDDLLine();
                BindGrid();
            }
        }

        #endregion

        protected void DDLline_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDLline.SelectedIndex != -1 && DDLline.SelectedIndex != 0)
            {

                BindGrid();
            }
        }


        protected void BtnList_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P1d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DPstart.SelectedDate.Value.ToString("yyyyMM") + "_DailyList";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            var q =
                from p in DB.Pp_P1d_OutputSubs
                join b in DB.Pp_P1d_Outputs on p.GUID equals b.GUID
                where p.isDelete == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { b.Prodirect, b.Prostdcapacity, p.Prorealqty, b.Promodel, b.Prohbn, b.Prolot, b.Prodate, p.Prostime, p.Proetime, b.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin }
                into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Prostime,
                    g.Key.Proetime,
                    g.Key.Prodirect,
                    g.Key.Prolinename,
                    g.Key.Promodel,
                    g.Key.Prohbn,
                    g.Key.Prolot,
                    g.Key.Prostdcapacity,
                    g.Key.Prorealqty,
                    g.Key.Prorealtime,
                    g.Key.Prostopcou,
                    g.Key.Prostopmemo,
                    g.Key.Probadcou,
                    g.Key.Probadmemo,
                    g.Key.Prolinemin,
                    g.Key.Prolinestopmin,



                };



            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            }
                string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");


                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
                if (DDLline.SelectedIndex != 0 && DDLline.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolinename.ToString().Contains(DDLline.SelectedItem.Text));
                }


            var qs = from g in q
                     select new
                     {
                         生产日期 = g.Prodate,
                         时间段 = g.Prostime + "-" + g.Proetime,
                         班组 = g.Prolinename,
                         机种 = g.Promodel,
                         物料 = g.Prohbn,
                         批次 = g.Prolot,
                         计划 = g.Prostdcapacity,
                         实绩 = g.Prorealqty,
                         生产工数 = g.Prorealtime,
                         停线原因 = g.Prostopcou,
                         原因说明 = g.Prostopmemo,
                         未达成 = g.Probadcou,
                         未达成原因 = g.Probadmemo,
                         损失工数 = g.Prolinestopmin,
                         投入工数 = g.Prodirect * 60,
                     };
            if (qs.Any())
            {
                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }

        }
    }
}
