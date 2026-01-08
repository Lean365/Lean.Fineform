using FineUIPro;
using LeanFine.Lf_Business.Helper;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using static LeanFine.QueryExtensions;

namespace LeanFine.Lf_Manufacturing.PP.daily
{
    public partial class p1d_rpr_daily_new : PageBase
    {
        public static string lclass, OPHID, ConnStr, nclass, ncode;
        public static int rowID, delrowID, editrowID, totalSum;

        public static string userid, badSum;
        public static string ProOrderType, Prolot, Prolinename, Prodate, Prorealqty, strPline, strmaxDate, strminDate, Probadqty, Probadtotal, Probadcou;
        public static string mysql;

        public int Prorate, ParentID;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DOutputNew";
            }
        }

        #endregion ViewPower

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckData();

                LoadData();
            }
        }

        private void CheckData()
        {
            string inputDate = DateTime.Now.ToString("yyyyMM");
            Pp_Efficiency rate = DB.Pp_Efficiencys.Where(u => u.Proratedate == inputDate).FirstOrDefault();

            if (rate == null)
            {
                //Alert alert = new Alert();
                //alert.Message = "请维护 " + inputDate + " 生产赁率！";
                //alert.IconUrl = "~/Lf_Resources/images/message/warring.png";
                //alert.Target = Target.Top;
                Alert.ShowInTop("请维护 " + inputDate + " 生产赁率！", MessageBoxIcon.Warning);

                //return;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
        }

        private void LoadData()
        {
            DateTime nowDt = DateTime.Now;
            //上月第一天
            DateTime lastFirst = nowDt.AddDays(1 - nowDt.Day).AddMonths(-1);
            //上月最后一天
            DateTime lastFinal = nowDt.AddDays(1 - nowDt.Day).AddMonths(-1);

            //本月第一天时间
            DateTime dt_First = new DateTime(nowDt.Year, nowDt.Month, 1);

            //本月最后一天时间
            DateTime dt_Final = nowDt.AddDays(1 - nowDt.Day).AddMonths(1).AddDays(-1);

            //每月10号
            //string Date10 = DateTime.Now.ToString("yyyyMM10");
            //string nowDate = DateTime.Now.ToString("yyyyMMdd");
            //将日期字符串转换为日期对象
            //第一天
            //DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime Date10 = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10));
            DateTime nowDate = Convert.ToDateTime((DateTime.Now));

            //DateTime editDate = Convert.ToDateTime(DateTime.ParseExact(prodate.SelectedDate.Value.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));

            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(Date10, nowDate);
            //int compEdit= DateTime.Compare(nowDate, editDate);

            if (compNum == -1)
            {
                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = dt_First;
                this.prodate.MaxDate = dt_Final;
            }
            if (compNum == 1)
            {
                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = lastFirst;
                this.prodate.MaxDate = dt_Final;
            }
            userid = GetIdentityName();
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDdlorder();

            BindDdlLine();
        }

        #endregion Page_Load

        #region BindDdlData

        //DDL查询LOT
        private void BindDdlorder()
        {
            //UpdatingHelper.UpdatePorderQty();

            //查询LINQ去重复
            var q = from a in DB.Pp_Manhours
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                        //where a.Porderqty - a.Porderreal > 0
                        //where a.Pordertype.Contains("ZDTA") || a.Pordertype.Contains("ZDTB") || a.Pordertype.Contains("ZDTC") || a.Pordertype.Contains("ZDTG")
                    orderby a.Proitem
                    select new
                    {
                        //b.Proecnmodel,
                        a.Proitem
                    };

            var qs = q.Select(E => new { E.Proitem }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            prohbn.DataSource = qs;
            prohbn.DataTextField = "Proitem";
            prohbn.DataValueField = "Proitem";
            prohbn.DataBind();
        }

        //DDL查询班组
        private void BindDdlLine()
        {
            //查询LINQ去重复
            var q = from a in DB.Adm_Dicts
                    where a.DictType.Contains("line_type_m")
                    select new
                    {
                        a.DictValue,
                        a.DictLabel
                    };

            var qs = q.Select(E => new { E.DictLabel, E.DictValue }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            prolinename.DataSource = qs;
            prolinename.DataTextField = "DictLabel";
            prolinename.DataValueField = "DictValue";
            prolinename.DataBind();
        }

        //DDL查询不良各类

        #endregion BindDdlData

        #region Events

        //protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        //{
        //    //object[] keys = Grid1.DataKeys[e.RowIndex];

        //    ////selID = Convert.ToInt32(keys[0].ToString());
        //    //string ss = keys[1].ToString();

        //    ////获取ID号
        //    //int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

        //    //if (e.CommandName == "Delete")
        //    //{
        //    //    // 在操作之前进行权限检查
        //    //    if (!CheckPower("CoreOutputDelete"))
        //    //    {
        //    //        CheckPowerFailWithAlert();
        //    //        return;
        //    //    }

        //    //    Pp_Output current = DB.Pp_P1d_Outputs.Find(del_ID);
        //    //    //删除日志
        //    //    string Newtext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Prongclass + "," + current.Prongcode + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadcou;
        //    //   string OperateType = "删除";//操作标记
        //    //    string OperateNotes = "Del生产* " + Newtext + " *Del 的记录已删除";
        //    //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除", OperateNotes);
        //    //    //删除记录
        //    //    //DB.Pp_Ecns.Where(l => l.ID == del_ID).Delete();
        //    //    current.IsDeleted = 1;
        //    //    DB.SaveChanges();
        //    //    //重新绑定

        //    //}
        //}

        //protected void Grid1_PreDataBound(object sender, EventArgs e)
        //{
        //    // 数据绑定之前，进行权限检查
        //    CheckPowerWithLinkButtonField("CoreOutputDelete", Grid1, "deleteField");
        //    // 设置LinkButtonField的点击客户端事件

        //}
        private void OPHRate()//稼动率
        {
            Pp_Efficiency current = DB.Pp_Efficiencys

                .OrderByDescending(u => u.Proratedate).FirstOrDefault();

            Prorate = current.Prorate;
        }

        private void SaveItem()//新增生产日报单头
        {
            Pp_P1d_Modify_Output item = new Pp_P1d_Modify_Output();

            //ophID();
            //item.OPHID = int.Parse(OPHID) + 1 + 1000;
            item.GUID = Guid.NewGuid();
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.Proordertype = ProOrderType;
            item.Prolinename = prolinename.SelectedItem.Text;

            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Prodirect = int.Parse(prodirect.Text);
            item.Proindirect = int.Parse(proindirect.Text);
            item.Prolot = prolot.Text;
            item.Prohbn = prohbn.SelectedItem.Text;//prohbn.Text;
            if (prosn.Text == "")
            {
                item.Prosn = "DTA0000~9999";
            }
            else
            {
                item.Prosn = prosn.Text;
            }

            item.Proorderqty = Decimal.Parse(prolotqty.Text);
            item.Promodel = promodel.Text;
            item.Prost = Decimal.Parse(prost.Text);
            //item.Prosubst = Decimal.Parse(prosubst.Text);
            item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);

            item.Proorder = proorder.Text;

            //item.Prostime = prostime.Text;
            //item.Proetime = proetime.Text;
            //item.Proplanqty = int.Parse(proplanqty.Text);
            //item.Prorealqty = int.Parse(prorealqty.Text);

            //item.Proplantotal = int.Parse(proplantotal.Text);
            //item.Prorealtotal = int.Parse(prorealtotal.Text);

            //item.Prolinestop = prolinestop.Checked;
            //item.Prolinestopmin = int.Parse(prolinestopmin.Text);

            //item.Probadcou = probadcou.Text;

            ////string sss = proetime.SelectedDate.Value.Subtract(prostime.SelectedDate.Value).Duration().TotalMinutes.ToString();//计算时间差（分钟）
            ////时间差的绝对值 ，测试你的代码运行了多长时间。
            //item.Prorealtime = int.Parse(proetime.SelectedDate.Value.Subtract(prostime.SelectedDate.Value).Duration().TotalMinutes.ToString()) - int.Parse(prolinestopmin.Text);
            //item.Proratio = int.Parse(proratio.Text);
            // 添加所有用户

            item.IsDeleted = 0;
            item.Promodifynotes = promodifynotes.Text;
            item.Remark = proorder.Text + "," + prolot.Text;
            item.CreateDate = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_P1d_Modify_Outputs.Add(item);
            DB.SaveChanges();
            //读取ID值
            OPHID = item.GUID.ToString();
            ParentID = item.ID;
            //新增日志

            string Contectext = ParentID + "," + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
            string OperateType = "新增";//操作标记
            string OperateNotes = "New改修OPH* " + Contectext + " *New改修OPH 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "改修管理", "改修OPH日报新增", OperateNotes);

            SaveSubItem();
            //删除无单身数据
            UpdateP1dHelper.DelOutputAssySubs(ParentID, prolinename.SelectedItem.Text, prodate.SelectedDate.Value.ToString("yyyyMMdd"), prolot.Text, prohbn.Text, GetIdentityName());
        }

        //新增新增生产日报单身
        /// <summary>
        /// 新增单身数据
        /// </summary>
        private void SaveSubItem()
        {
            try
            {
                var res = (from a in DB.Adm_Dicts
                               //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                               //where b.Proecnno == strecn
                               //where b.Proecnbomitem == stritem
                           where a.DictType.Contains("app_phase_time")
                           select new
                           {
                               a.DictLabel,
                               a.DictValue
                           }).ToList();

                int icount = res.Count();
                //判断查询是否为空
                if (res.Any())
                {
                    //遍历
                    for (int i = 0; i < icount; i++)
                    {
                        Pp_P1d_Modify_OutputSub item = new Pp_P1d_Modify_OutputSub();

                        // 添加父ID
                        //Pp_Output ID = Attach<Pp_Output>(Convert.ToInt32(ParentID));
                        item.Parent = ParentID;
                        item.Prolinename = prolinename.SelectedItem.Text;
                        item.Proordertype = ProOrderType;
                        item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                        item.Prodirect = int.Parse(prodirect.Text);
                        item.Proindirect = int.Parse(proindirect.Text);
                        item.Prolot = prolot.Text;
                        item.Prohbn = prohbn.SelectedItem.Text;//prohbn.Text;
                        item.Proorderqty = Decimal.Parse(prolotqty.Text);
                        item.Promodel = promodel.Text;
                        item.Prost = Decimal.Parse(prost.Text);
                        //item.Prosubst = Decimal.Parse(prosubst.Text);
                        item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                        item.Totaltag = true;
                        item.Proorder = proorder.Text;
                        item.GUID = Guid.Parse(OPHID);
                        //时间
                        item.Prostime = res[i].DictValue.Substring(0, 5).TrimEnd().ToString();
                        item.Proetime = res[i].DictValue.Substring(6, 5).TrimEnd().ToString();
                        //item.Udf001 = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                        //item.Udf002 = this.prolinename.SelectedItem.Text;
                        item.Promodifynotes = promodifynotes.Text;
                        item.Remark = proorder.Text + "," + prolot.Text;
                        item.CreateDate = DateTime.Now;
                        item.Creator = GetIdentityName();
                        item.IsDeleted = 0;
                        DB.Pp_P1d_Modify_OutputSubs.Add(item);
                        DB.SaveChanges();

                        //新增日志

                        string Newtext = ParentID + "," + res[i].DictValue + "," + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                        string OperateType = "新增";

                        string OperateNotes = "New改修OPH_SUB* " + Newtext + " *New改修OPH_SUB 的记录已新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "改修OPH实绩新增", OperateNotes);
                    }
                }
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

        /// <summary>
        /// 新增订单不良合计表
        /// </summary>
        private void SaveOrderDefect()
        {
            try
            {
                Pp_Defect_P1d_Order item = new Pp_Defect_P1d_Order();

                item.Prolot = prolot.Text;
                item.Prolinename = prolinename.SelectedItem.Text;
                item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                item.Proorder = proorder.Text;
                item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
                item.Prorealqty = int.Parse(prolotqty.Text);
                item.Proitem = prohbn.SelectedItem.Text;
                item.Prodept = "ASSYM";
                item.Prodzeroefects = 0;
                item.Probadtotal = 0;
                item.Prodirectrate = 0;
                item.Probadrate = 0;
                item.IsDeleted = 0;
                item.Remark = "ASSYM";

                item.Promodel = promodel.Text;
                item.GUID = Guid.NewGuid();
                item.Creator = GetIdentityName();
                item.CreateDate = DateTime.Now;
                DB.Pp_Defect_P1d_Orders.Add(item);
                DB.SaveChanges();

                //新增日志
                string Contectext = prolot.Text + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.Text + "," + prolotqty.Text;
                string OperateType = "新增";
                string OperateNotes = "New改修OPH* " + Contectext + " New改修OPH* 的记录已新增";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "改修管理", "改修OPH新增", OperateNotes);
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

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveOrderDefectUpdate()
        {
            //判断重复
            string strorder = proorder.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P1d_Modify_OutputSubs
                            //join b in DB.Pp_P1d_Outputs on p.OPHID equals b.OPHID
                        where p.Proorder == (strorder)

                        select new
                        {
                            p.Prolinename
                        }).Distinct().ToList();
            if (line.Any())
            {
                for (int i = 0; i < line.Count(); i++)
                {
                    strPline += line[i].Prolinename.ToString() + ",";
                }
            }
            var maxdate =
                                    (from p in DB.Pp_P1d_Modify_OutputSubs
                                         //join b in DB.proOutputs on p.OPHID equals b.OPHID
                                     where p.Proorder == (strorder)
                                     //.Where(s => s.Prolot.Contains(strPlot))
                                     // p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                                     group p by p.Proorder into g
                                     select new
                                     {
                                         g.Key,
                                         mdate = g.Max(p => p.Prodate)
                                     }).Distinct().ToList();
            if (maxdate.Any())
            {
                strmaxDate = maxdate[0].mdate;
            }

            var mindate =
                        (from p in DB.Pp_P1d_Modify_OutputSubs
                             //join b in DB.proOutputs on p.OPHID equals b.OPHID
                         where p.Proorder == (strorder)
                         //.Where(s => s.Prolot.Contains(strPlot))
                         //where p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                         group p by p.Proorder into g
                         select new
                         {
                             g.Key,
                             mdate = g.Min(p => p.Prodate)
                         }).Distinct().ToList();
            if (mindate.Any())
            {
                strminDate = mindate[0].mdate;
            }

            string sedate = strminDate + "~" + strmaxDate;
            DB.Pp_Defect_P1d_Orders
               .Where(s => s.Proorder == strorder)

               .ToList()
               .ForEach(x => { x.Prolinename = strPline; x.Prodate = sedate; x.Modifier = GetIdentityName(); x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();

            //新增日志
            string Contectext = strorder + "," + strlot + "," + strPline + "," + sedate;
            string OperateType = "修改";
            string OperateNotes = "Edit改修OPH*" + Contectext + " *Edit改修OPH 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "改修管理", "改修OPH修改", OperateNotes);
            strPline = "";
            strmaxDate = "";
            strminDate = "";
            sedate = "";
        }

        /// <summary>
        /// 新增订单不良合计表
        /// </summary>
        private void SaveLotDefect()
        {
            try
            {
                Pp_Defect_P1d_Order item = new Pp_Defect_P1d_Order();

                item.Prolot = prolot.Text;
                item.Prolinename = prolinename.SelectedItem.Text;
                item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                item.Proorder = proorder.Text;
                item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
                item.Prorealqty = int.Parse(prolotqty.Text);
                item.Proitem = prohbn.SelectedItem.Text;
                item.Prodept = "ASSYM";
                item.Prodzeroefects = 0;
                item.Probadtotal = 0;
                item.Prodirectrate = 0;
                item.Probadrate = 0;
                item.IsDeleted = 0;
                item.Remark = "ASSYM";

                item.Promodel = promodel.Text;
                item.GUID = Guid.NewGuid();
                item.Creator = GetIdentityName();
                item.CreateDate = DateTime.Now;
                DB.Pp_Defect_P1d_Orders.Add(item);
                DB.SaveChanges();

                //新增日志
                string Contectext = prolot.Text + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.Text + "," + prolotqty.Text;
                string OperateType = "新增";
                string OperateNotes = "New改修OPH* " + Contectext + " New改修OPH* 的记录已新增";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "改修管理", "改修OPH新增", OperateNotes);
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

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveLotDefectUpdate()
        {
            //判断重复
            string strorder = proorder.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P1d_Modify_OutputSubs
                            //join b in DB.Pp_P1d_Outputs on p.OPHID equals b.OPHID
                        where p.Prolot == (strlot)

                        select new
                        {
                            p.Prolinename
                        }).Distinct().ToList();
            if (line.Any())
            {
                for (int i = 0; i < line.Count(); i++)
                {
                    strPline += line[i].Prolinename.ToString() + ",";
                }
            }
            var maxdate =
                                    (from p in DB.Pp_P1d_Modify_OutputSubs
                                         //join b in DB.proOutputs on p.OPHID equals b.OPHID
                                     where p.Prolot == (strlot)
                                     //.Where(s => s.Prolot.Contains(strPlot))
                                     // p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                                     group p by p.Prolot into g
                                     select new
                                     {
                                         g.Key,
                                         mdate = g.Max(p => p.Prodate)
                                     }).Distinct().ToList();
            if (maxdate.Any())
            {
                strmaxDate = maxdate[0].mdate;
            }

            var mindate =
                        (from p in DB.Pp_P1d_Modify_OutputSubs
                             //join b in DB.proOutputs on p.OPHID equals b.OPHID
                         where p.Prolot == (strlot)
                         //.Where(s => s.Prolot.Contains(strPlot))
                         //where p.Prodate.Substring(0, 6).CompareTo(strDpdate) <= 0
                         group p by p.Prolot into g
                         select new
                         {
                             g.Key,
                             mdate = g.Min(p => p.Prodate)
                         }).Distinct().ToList();
            if (mindate.Any())
            {
                strminDate = mindate[0].mdate;
            }

            string sedate = strminDate + "~" + strmaxDate;
            DB.Pp_Defect_P1d_Orders
               .Where(s => s.Prolot == strlot)

               .ToList()
               .ForEach(x => { x.Prolinename = strPline; x.Prodate = sedate; x.Modifier = GetIdentityName(); x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();

            //新增日志
            string Contectext = strorder + "," + strlot + "," + strPline + "," + sedate;
            string OperateType = "修改";
            string OperateNotes = "Edit改修OPH*" + Contectext + " *Edit改修OPH 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "改修管理", "改修OPH修改", OperateNotes);
            strPline = "";
            strmaxDate = "";
            strminDate = "";
            sedate = "";
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            Distinctdata();
        }

        //判断OPH重复单头
        private void Distinctdata()
        {
            try
            {
                //保存OPH数据
                SaveItem();
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

            //判断重复
            string inputOrder = proorder.Text.Trim();

            Pp_Defect_P1d_Order currentOrder = DB.Pp_Defect_P1d_Orders.Where(u => u.Proorder == inputOrder).FirstOrDefault();

            if (currentOrder != null)
            {
                SaveOrderDefectUpdate();
            }
            else
            {
                SaveOrderDefect();
            }
            string inputLot = prolot.Text.Trim();

            Pp_Defect_P1d_Order currentLot = DB.Pp_Defect_P1d_Orders.Where(u => u.Prolot == inputLot).FirstOrDefault();

            if (currentLot != null)
            {
                SaveLotDefectUpdate();
            }
            else
            {
                SaveLotDefect();
            }
            //表格数据已重新绑定

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events

        #region Times

        //计算时间
        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        private string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            TimeSpan ts = DateTime1.Subtract(DateTime2).Duration();
            dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            return dateDiff;
        }

        #endregion Times

        protected void prohbn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.prohbn.SelectedIndex != -1 && this.prohbn.SelectedIndex != 0)
            {
                try
                {
                    var q = from a in DB.Pp_Manhours
                                //join b in DB.Pp_Manhours on a.Porderhbn equals b.Proitem
                                //join c in DB.Pp_SapMaterials on a.Proecnoldhbn equals c.D_SAP_ZCA1D_Z002
                            where a.Proitem == prohbn.SelectedItem.Text
                            //where a.Proecnmodel == strProecnmodel
                            //where a.Proecnbomitem == strProecnbomitem
                            //where a.Proecnoldhbn == strProecnoldhbn
                            //where a.Proecnnewhbn == strProecnnewhbn

                            orderby a.Prost descending

                            select new
                            {
                                //Pordertype = "ZDTG",
                                //Porderno = "G" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmssfff"),
                                a.Proitem,
                                //Porderlot = a.Promodel + DateTime.Now.ToString("yyyyMMdd"),
                                //Porderqty = 0,
                                //Porderdate = DateTime.Now.ToString("yyyyMMdd"),
                                //Porderserial = a.Promodel + DateTime.Now.ToString("yyyyMMdd") + "~" + this.prolotqty.Text,
                                a.Prowcname,
                                a.Promodel,
                                a.Protext,
                                a.Prowctext,
                                a.Proshort,
                                a.Propset,
                                a.Prorate,
                                a.Prost,
                                a.Proset,
                                a.Prodesc,
                                //a.Porderreal
                                //c.D_SAP_ZCA1D_Z033,
                            };

                    if (q.Any())
                    {
                        //var qt = q.ToList();
                        //ProOrderType = qt[0].Pordertype;
                        //prohbn.Text = qt[0].Porderhbn;
                        //promodel.Text = qt[0].Promodel;
                        //prolot.Text = qt[0].Porderlot;

                        //prolotqty.Text = qt[0].Porderqty.ToString();
                        ////prorealqty.Text = (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) - decimal.Parse(DSstr1.Tables[0].Rows[0][16].ToString())).ToString();
                        //prosn.Text = qs[0].Porderserial;

                        var maxst = from a in q
                                    group a by new { a.Proitem, a.Promodel }
                             into g
                                    select new
                                    {
                                        //最大ST
                                        Prost = g.Max(x => x.Prost),
                                        //物料
                                        g.Key.Proitem,
                                        //机种
                                        g.Key.Promodel,
                                    };

                        // 切勿使用 source.Count() > 0
                        var qs = maxst.Distinct().Take(1).ToList();
                        if (qs[0].Prost != 0)
                        {
                            ProOrderType = "ZDTG";
                            proorder.Text = "M" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmssfff");
                            prohbn.Text = qs[0].Proitem;
                            promodel.Text = qs[0].Promodel;
                            prolot.Text = "M" + qs[0].Promodel + DateTime.Now.ToString("yyyyMMdd");
                            prost.Text = qs[0].Prost.ToString();
                            //prolotqty.Text = qs[0].Porderqty.ToString();
                            //prorealqty.Text = (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) - decimal.Parse(DSstr1.Tables[0].Rows[0][16].ToString())).ToString();

                            HourQty();
                        }
                        else
                        {
                            // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                            Alert.ShowInTop("ST为0！", String.Empty, ActiveWindow.GetHideReference());
                            return;
                        }
                    }
                    else
                    {
                        // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                        Alert.ShowInTop("此物料没有ST，请通知技术部门录入！", String.Empty, ActiveWindow.GetHideReference());
                        return;
                    }
                }
                catch (ArgumentNullException Message)
                {
                    Alert.ShowInTop("异常1:" + Message);
                }
                catch (InvalidCastException Message)
                {
                    Alert.ShowInTop("异常2:" + Message);
                }
                catch (Exception Message)
                {
                    Alert.ShowInTop("异常3:" + Message);
                }
            }
        }

        protected void prodirect_TextChanged(object sender, EventArgs e)
        {
            HourQty();
        }

        protected void prolotqty_TextChanged(object sender, EventArgs e)
        {
            prosn.Text = this.promodel.Text + DateTime.Now.ToString("yyyyMMdd") + "~" + this.prolotqty.Text;
        }

        //计算标准产能
        /// <summary>
        /// 标准产能计算
        /// </summary>
        private void HourQty()
        {
            decimal stdiff = (decimal.Parse(prost.Text));
            if (stdiff != 0)
            {
                OPHRate();
                decimal rate = (decimal)Prorate / 100;
                prostdcapacity.Text = ((decimal.Parse(prodirect.Text) * 60) / (decimal.Parse(prost.Text)) * rate).ToString("0.0");
            }
        }
    }
}