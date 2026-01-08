using FineUIPro;
using LeanFine.Lf_Business.Helper;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using static LeanFine.QueryExtensions;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_daily_new : PageBase
    {
        public static string lclass, OPHID, ConnStr, nclass, ncode;
        public static int rowID, delrowID, editrowID, totalSum;

        public static string userid, badSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty, strPline, strmaxDate, strminDate, Probadqty, Probadtotal, Probadcou, strProModel;
        public static string mysql;

        public int Prorate, ParentID;

        // 搜索防抖变量
        private static string lastSearchText = "";
        private static DateTime lastSearchTime = DateTime.MinValue;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputNew";
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
            //prodirect.Text = "1";
            //proindirect.Text = "1";
            //prolinename.Text = "制二课";
            //上月第一天
            DateTime last = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1);
            //本月第一天时间
            DateTime dt = DateTime.Today;
            DateTime dt_First = dt.AddMonths(-1).AddDays(-(dt.Day) + 1);

            //本月最后一天时间
            DateTime dt2 = dt.AddMonths(1);
            DateTime dt_Last = dt2.AddDays(-(dt.Day));

            //每月10号
            //string Date10 = DateTime.Now.ToString("yyyyMM10");
            //string nowDate = DateTime.Now.ToString("yyyyMMdd");
            //将日期字符串转换为日期对象
            //第一天
            //DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime Date10 = Convert.ToDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10));
            DateTime nowDate = Convert.ToDateTime((DateTime.Now));

            //DateTime editDate = Convert.ToDateTime(DateTime.ParseExact(prodate.SelectedDate.Value.ToString("yyyyMMdd"), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture));

            //上个月
            DateTime lastDate = DateTime.Now.AddMonths(-1);
            //通过DateTIme.Compare()进行比较（）
            int compNum = DateTime.Compare(Date10, nowDate);
            //int compEdit= DateTime.Compare(nowDate, editDate);

            if (compNum == -1)
            {
                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = dt_First;
                this.prodate.MaxDate = dt_Last;
            }
            if (compNum == 1)
            {
                this.prodate.SelectedDate = DateTime.Now.AddDays(-1);
                this.prodate.MinDate = last;
                this.prodate.MaxDate = dt_Last;
            }
            userid = GetIdentityName();
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDdlorder();

            //BindDdlLine();
        }

        #endregion Page_Load

        #region BindDdlData

        //DDL查询订单 - 大数据优化版本
        private void BindDdlorder()
        {
            try
            {
                // 大数据优化策略：
                // 1. 初始加载50条最新订单，确保界面快速响应
                // 2. 使用 AsNoTracking() 不跟踪实体变化，大幅提高性能
                // 3. 只选择必要字段，减少数据传输
                // 4. 按订单号倒序排列，最新订单优先显示
                // 5. 通过EnableEdit实现真正的动态搜索功能
                // 6. 使用快速搜索策略，避免界面假死

                // 使用快速加载方法，确保界面响应性
                LoadRecentOrders();
            }
            catch (Exception ex)
            {
                // 记录错误日志并显示用户友好的错误信息
                Alert.ShowInTop("加载订单数据时发生错误，请稍后重试。", MessageBoxIcon.Error + ex.Message);
            }
        }

        /// <summary>
        /// 加载最近50条订单数据，确保界面快速响应
        /// </summary>
        private void LoadRecentOrders()
        {
            try
            {
                // 查询最近50条有效订单
                var orders = DB.Pp_Orders
                    .Where(o => o.IsDeleted == 0)
                    .Where(o => o.Porderqty > 0)
                    .OrderByDescending(o => o.Porderno)
                    .Select(o => new { o.Porderno })
                    .AsNoTracking() // 不跟踪实体变化，大幅提高性能
                    .Take(50)
                    .ToList();

                // 清空并重新绑定下拉列表
                proorder.Items.Clear();
                proorder.Items.Add(new FineUIPro.ListItem("", ""));

                foreach (var order in orders)
                {
                    proorder.Items.Add(new FineUIPro.ListItem(order.Porderno, order.Porderno));
                }

                proorder.EmptyText = "请选择订单或使用搜索框搜索更多订单";
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("加载订单数据时发生错误，请稍后重试。", MessageBoxIcon.Error + ex.Message);
                proorder.EmptyText = "加载失败，请刷新重试";
            }
        }



        //DDL查询不良各类

        #endregion BindDdlData

        #region Events

        private void OPHRate()//稼动率
        {
            Pp_Efficiency current = DB.Pp_Efficiencys

                .OrderByDescending(u => u.Proratedate).FirstOrDefault();

            Prorate = current.Prorate;
        }

        //判断修改内容||判断重复
        private void CheckRepeatItem(string strOrder, string strLine, string strDate)
        {
            if (prolotqty.Text == null || prolotqty.Text == "0.0")
            {
                Alert.ShowInTop("生产工单的数量不能为空！", MessageBoxIcon.Warning);
                return;
            }

            //int id = GetQueryIntValue("id");
            //proManhour current = DB.Pp_Manhours.Find(id);
            //Cmc001 = current.Prodate;
            //Cmc002 = current.Proitem;
            //Cmc004 = current.Proshort.ToString();
            //Cmc005 = current.Prost.ToString();
            //Cmc008 = current.Prodesc;

            //if (this.Prodate.SelectedDate.Value.ToString("yyyyMMdd") == Cmc001)
            //{
            //    if (this.Proitem.Text == Cmc002)
            //    {
            //        if (this.Proshort.Text == Cmc004)
            //        {
            //            if (this.Prost.Text == Cmc005)
            //            {
            //                if (this.Prodesc.Text == Cmc008)
            //                {
            //                    Alert alert = new Alert();
            //                    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    alert.IconFont = IconFont.Warning;
            //                    alert.Target = Target.Top;
            //                    Alert.ShowInTop();
            //                }
            //            }
            //        }
            //    }

            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            //int id = GetQueryIntValue("id");
            //proLinestop current = DB.proLinestops.Find(id);
            ////decimal cQcpd005 = current.Qcpd005;
            //string checkdata1 = current.Prostoptext;

            //if (this.Prostoptext.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //    alert.IconFont = IconFont.Warning;
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            Pp_P2d_Output redata = DB.Pp_P2d_Outputs.Where(u => u.Proorder.ToString() + u.Prodate + u.Prolinename == strOrder + strDate + strLine).FirstOrDefault();

            if (redata != null)
            {
                Alert.ShowInTop(strOrder + "||" + strLine + "||" + strDate + ",生产工单在同一天同一班组不能重复输入！", MessageBoxIcon.Warning);
                return;
            }
            //保存数据
            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        private void SaveItem()//新增生产日报单头
        {
            try
            {
                Pp_P2d_Output item = new Pp_P2d_Output();

                //ophID();
                //item.OPHID = int.Parse(OPHID) + 1 + 1000;
                item.GUID = Guid.NewGuid();
                //item.Prolineclass = prolinename.SelectedValue.ToString();
                item.Proordertype = pordertype.Text;
                item.Prolinename = "制二课";//prolinename.SelectedItem.Text;

                item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                item.Prodirect = 1;// int.Parse(prodirect.Text);
                item.Proindirect = 1;// int.Parse(proindirect.Text);
                item.Prolot = prolot.Text;
                item.Prohbn = prohbn.Text;//prohbn.Text;
                if (prosn.Text == "")
                {
                    item.Prosn = "DTA0000~9999";
                }
                else
                {
                    item.Prosn = prosn.Text;
                }

                item.Proorderqty = Decimal.Parse(prolotqty.Text);

                item.Promodel = "Pcba";// promodel.Text;

                item.Prost = 0;//Decimal.Parse(prost.Text);
                //item.Prosubst = Decimal.Parse(prosubst.Text);
                item.Prostdcapacity = 0; //Decimal.Parse(prostdcapacity.Text);

                item.Proorder = proorder.SelectedItem.Text;

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
                item.Remark = remark.Text;
                item.CreateDate = DateTime.Now;
                item.Creator = GetIdentityName();
                DB.Pp_P2d_Outputs.Add(item);
                DB.SaveChanges();
                //读取ID值
                OPHID = item.GUID.ToString();
                ParentID = item.ID;
                //新增日志

                string Contectext = ParentID + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text;
                string OperateType = "新增";//操作标记
                string OperateNotes = "New生产OPH* " + Contectext + " *New生产OPH 的记录已新增";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报新增", OperateNotes);

                SaveSubItem();
                //更新单头机种
                UpdateItem(strProModel);
                //删除无单身数据
                UpdatingP2dHelper.DelOutputPcbaSubs(ParentID, "制二课", prodate.SelectedDate.Value.ToString("yyyyMMdd"), prolot.Text, prohbn.Text, GetIdentityName());
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

        //新增新增生产日报单身
        /// <summary>
        /// 新增单身数据
        /// </summary>
        private void SaveSubItem()
        {
            try
            {
                var hbn = (from a in DB.Pp_Orders
                           where a.Porderno.Contains(proorder.SelectedItem.Text)
                           select a).ToList();

                if (hbn.Any())
                {
                    string itemhbn = hbn[0].Porderhbn.ToString();
                    string itemtype = hbn[0].Pordertype.ToString();
                    var q_model = (from p in DB.Pp_Manhours
                                   where p.Proitem.Contains(itemhbn)
                                   //where p.Prowctext.Contains("SMT")
                                   orderby p.Prowctext
                                   //where p.Age > 30 && p.Department == "研发部"
                                   select p).Take(1).ToList();
                    if (q_model.Any())
                    {
                        if (itemtype.Contains("ZDTA") || itemtype.Contains("ZDTB") || itemtype.Contains("ZDTC"))
                        {
                            //手插班，修正班
                            for (int j = 0; j < 1; j++)
                            {
                                string[] list = new string[] { "3手插", "4修正", };
                                foreach (var val in list)
                                {
                                    Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                    // 添加父ID
                                    //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                    item.Proordertype = pordertype.Text;
                                    item.Parent = ParentID;
                                    item.Prolinename = val;
                                    item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    item.Prodirect = 10;
                                    item.Proindirect = 2;
                                    item.Prolot = prolot.Text;
                                    item.Prohbn = prohbn.Text;//prohbn.Text;
                                    item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                    item.Promodel = q_model[0].Promodel.ToString();
                                    strProModel = q_model[0].Promodel.ToString();
                                    item.Prorate = Decimal.Parse(q_model[0].Prorate.ToString());
                                    item.Prost = Decimal.Parse(q_model[0].Prost.ToString());
                                    item.Proshort = Decimal.Parse(q_model[0].Proshort.ToString());
                                    item.Propcbatype = "";
                                    item.Propcbaside = "";
                                    //item.Prosubst = Decimal.Parse(prosubst.Text);
                                    item.Prostdcapacity = 0;
                                    item.Totaltag = true;
                                    item.Proorder = proorder.SelectedItem.Text;
                                    item.GUID = Guid.Parse(OPHID);
                                    item.Prostime = val;
                                    item.Proetime = val;
                                    item.UDF01 = "";
                                    item.UDF02 = "";
                                    item.UDF03 = "";
                                    item.UDF04 = "";
                                    item.UDF05 = "";
                                    item.UDF06 = "";
                                    item.UDF51 = 0;
                                    item.UDF52 = 0;
                                    item.UDF53 = 0;
                                    item.UDF54 = 0;
                                    item.UDF55 = 0;
                                    item.UDF56 = 0;

                                    item.Remark = remark.Text;
                                    item.CreateDate = DateTime.Now;
                                    item.Creator = GetIdentityName();
                                    item.IsDeleted = 0;
                                    DB.Pp_P2d_OutputSubs.Add(item);
                                    DB.SaveChanges();

                                    //新增日志

                                    string Newtext = ParentID + "," + q_model[0].Prowctext + "~" + q_model[0].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text;
                                    string OperateType = "新增";

                                    string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);
                                }
                            }
                            //自插班
                            for (int j = 0; j < 1; j++)
                            {
                                string[] list = new string[] { "2自插", };
                                foreach (var val in list)
                                {
                                    Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                    // 添加父ID
                                    //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                    item.Proordertype = pordertype.Text;
                                    item.Parent = ParentID;
                                    item.Prolinename = val;
                                    item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    item.Prodirect = 10;
                                    item.Proindirect = 2;
                                    item.Prolot = prolot.Text;
                                    item.Prohbn = prohbn.Text;//prohbn.Text;
                                    item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                    item.Promodel = q_model[0].Promodel.ToString();
                                    strProModel = q_model[0].Promodel.ToString();
                                    item.Prorate = Decimal.Parse(q_model[0].Prorate.ToString());
                                    item.Prost = Decimal.Parse(q_model[0].Prost.ToString());
                                    item.Proshort = Decimal.Parse(q_model[0].Proshort.ToString());
                                    item.Propcbatype = "";
                                    item.Propcbaside = "";
                                    //item.Prosubst = Decimal.Parse(prosubst.Text);
                                    item.Prostdcapacity = 0;
                                    item.Totaltag = true;
                                    item.Proorder = proorder.SelectedItem.Text;
                                    item.GUID = Guid.Parse(OPHID);
                                    item.Prostime = "自插";
                                    item.Proetime = "自插";
                                    item.UDF01 = "";
                                    item.UDF02 = "";
                                    item.UDF03 = "";
                                    item.UDF04 = "";
                                    item.UDF05 = "";
                                    item.UDF06 = "";
                                    item.UDF51 = 0;
                                    item.UDF52 = 0;
                                    item.UDF53 = 0;
                                    item.UDF54 = 0;
                                    item.UDF55 = 0;
                                    item.UDF56 = 0;

                                    item.Remark = remark.Text;
                                    item.CreateDate = DateTime.Now;
                                    item.Creator = GetIdentityName();
                                    item.IsDeleted = 0;
                                    DB.Pp_P2d_OutputSubs.Add(item);
                                    DB.SaveChanges();

                                    //新增日志

                                    string Newtext = ParentID + "," + q_model[0].Prowctext + "~" + q_model[0].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text;
                                    string OperateType = "新增";

                                    string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);
                                }
                            }
                        }
                        if (itemtype.Contains("ZDTD") || itemtype.Contains("ZDTE") || itemtype.Contains("ZDTF"))
                        {
                            //SMT设1，2班
                            for (int j = 0; j < 1; j++)
                            {
                                string[] list = new string[] { "T", "B" };
                                foreach (var val in list)
                                {
                                    Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                                    // 添加父ID
                                    //Pp_P1d_Output ID = Attach<Pp_P1d_Output>(Convert.ToInt32(ParentID));
                                    item.Proordertype = pordertype.Text;
                                    item.Parent = ParentID;
                                    item.Prolinename = "1SMT";
                                    item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                                    item.Prodirect = 10;
                                    item.Proindirect = 2;
                                    item.Prolot = prolot.Text;
                                    item.Prohbn = prohbn.Text;//prohbn.Text;
                                    item.Proorderqty = Decimal.Parse(prolotqty.Text);
                                    item.Promodel = q_model[0].Promodel.ToString();
                                    strProModel = q_model[0].Promodel.ToString();
                                    item.Prorate = Decimal.Parse(q_model[0].Prorate.ToString());
                                    item.Prost = Decimal.Parse(q_model[0].Prost.ToString());
                                    item.Proshort = Decimal.Parse(q_model[0].Proshort.ToString());
                                    item.Propcbaside = val;
                                    //item.Prosubst = Decimal.Parse(prosubst.Text);
                                    item.Prostdcapacity = 0;
                                    item.Totaltag = true;
                                    item.Proorder = proorder.SelectedItem.Text;
                                    item.GUID = Guid.Parse(OPHID);
                                    item.Propcbastated = "";
                                    item.Protime = 0;
                                    item.Prohandoffnum = 0;
                                    item.Prohandofftime = 0;
                                    item.Prodowntime = 0;
                                    item.Prolosstime = 0;
                                    item.Promaketime = 0;
                                    item.Proworkst = 0;
                                    item.Prostdiff = 0;
                                    item.Proqtydiff = 0;
                                    item.Proratio = 0;
                                    item.Prostime = "SMT";
                                    item.Proetime = "SMT";
                                    item.UDF01 = "";
                                    item.UDF02 = "";
                                    item.UDF03 = "";
                                    item.UDF04 = "";
                                    item.UDF05 = "";
                                    item.UDF06 = "";
                                    item.UDF51 = 0;
                                    item.UDF52 = 0;
                                    item.UDF53 = 0;
                                    item.UDF54 = 0;
                                    item.UDF55 = 0;
                                    item.UDF56 = 0;

                                    item.Remark = remark.Text;
                                    item.CreateDate = DateTime.Now;
                                    item.Creator = GetIdentityName();
                                    item.IsDeleted = 0;
                                    DB.Pp_P2d_OutputSubs.Add(item);
                                    DB.SaveChanges();

                                    //新增日志

                                    string Newtext = ParentID + "," + q_model[0].Prowctext + "~" + q_model[0].Prowctext + "," + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text;
                                    string OperateType = "新增";

                                    string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                                    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);
                                }
                            }
                        }
                    }
                    else
                    {
                        //Alert.ShowInTop("没有机种或工时", String.Empty, ActiveWindow.GetHideReference());
                        //"请确认订单类别！\r\nZDTD，ZDTE，ZDTF为PCBA订单为类型。\r\n如果是SMT或自插，应该有相对应的Short数，请联系技术添加。<br/>\r\n如果是手插或修正，请联系电脑课修改订单类型。
                        string strMsg = "没有机种或工时";
                        Alert.ShowInTop(strMsg, MessageBoxIcon.Error);

                        return;
                    }
                    if (q_model.Count == 0)
                    {
                        string strMsg = "没有机种或工时";
                        //"请确认订单类别！\r\nZDTD，ZDTE，ZDTF为PCBA订单为类型。\r\n如果是SMT或自插，应该有相对应的Short数，请联系技术添加。<br/>\r\n如果是手插或修正，请联系电脑课修改订单类型。
                        //string strMsg = String.Format("<div style=\"margin-bottom:10px;color: #0000FF;\"><strong>填写说明：</strong></div><div>1.请确认订单类别。</div><div>2.ZDTD，ZDTE，ZDTF为<strong>PCBA订单为类型</strong>。</div><div>3.如果是SMT或自插，应该有相对应的Short数，<strong>请联系技术添加</strong>。</div><div>4.如果是手插或修正，<strong>请联系电脑课修改订单类型</strong>。</div>");
                        Alert.ShowInTop(strMsg, MessageBoxIcon.Error);
                        return;
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
        /// 更新单头机种
        /// </summary>
        /// <param name="UpdatestrModel"></param>
        private void UpdateItem(string UpdatestrModel)//更新生产日报单头，机种
        {
            //int id = GetQueryIntValue("id");
            Pp_P2d_Output item = DB.Pp_P2d_Outputs

                .Where(u => u.ID == ParentID).FirstOrDefault();

            item.Promodel = UpdatestrModel;

            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Proophs.Add(item);
            DB.SaveChanges();

            //SaveSubItem();

            //修改后日志
            string ModifiedText = "制二课," + prodate.Text + "," + prolot.Text + "," + prohbn.Text + "," + promodel.Text;
            string OperateType = "修改";
            string OperateNotes = "*afEdit生产OPH " + ModifiedText + "*afEdit生产OPH 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH修改", OperateNotes);
        }

        /// <summary>
        /// 新增订单不良合计表
        /// </summary>
        private void SaveOrderDefect()
        {
            Pp_Defect_P2d_Order item = new Pp_Defect_P2d_Order();

            item.Prolot = prolot.Text;
            item.Prolinename = "制二课";
            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proorder = proorder.SelectedItem.Text;
            item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
            item.Promodel = strProModel;
            item.Proitem = prohbn.Text;
            item.Prorealqty = 0;
            item.Prodzeroefects = 0;
            item.Probadtotal = 0;
            item.Prodirectrate = 0;
            item.Probadrate = 0;
            item.Prodept = "PCBA";
            item.IsDeleted = 0;
            item.UDF01 = prohbn.Text;
            item.Remark = "PCBA";
            //item.Promodel = promodel.Text;
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateDate = DateTime.Now;
            DB.Pp_Defect_P2d_Orders.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = prolot.Text + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.SelectedItem.Text + "," + prolotqty.Text;
            string OperateType = "新增";
            string OperateNotes = "New生产订单不良* " + Contectext + " New生产订单不良* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计新增", OperateNotes);
        }

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveOrderDefectUpdate()
        {
            //判断重复
            string strorder = proorder.SelectedItem.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P2d_OutputSubs
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
                                    (from p in DB.Pp_P2d_OutputSubs
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
                        (from p in DB.Pp_P2d_OutputSubs
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
            DB.Pp_Defect_P2d_Orders
               .Where(s => s.Proorder == strorder)

               .ToList()
               .ForEach(x => { x.Promodel = strProModel; x.Prolinename = strPline; x.Prodate = sedate; x.Modifier = GetIdentityName(); x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();

            //新增日志
            string Contectext = strorder + "," + strlot + "," + strPline + "," + sedate;
            string OperateType = "修改";
            string OperateNotes = "Edit生产不良*" + Contectext + " *Edit生产不良 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计修改", OperateNotes);
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
            Pp_Defect_P2d_Lot item = new Pp_Defect_P2d_Lot();

            item.Prolot = prolot.Text;
            item.Prolinename = "制二课";
            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proorder = proorder.SelectedItem.Text;
            item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
            item.Promodel = strProModel;
            item.Proitem = prohbn.Text;
            item.Prorealqty = 0;
            item.Prodzeroefects = 0;
            item.Probadtotal = 0;
            item.Prodirectrate = 0;
            item.Probadrate = 0;
            item.Prodept = "PCBA";
            item.IsDeleted = 0;
            item.UDF01 = prohbn.Text;
            item.Remark = "PCBA";
            //item.Promodel = promodel.Text;
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateDate = DateTime.Now;
            DB.Pp_Defect_P2d_Lots.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = prolot.Text + "制二课" + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.SelectedItem.Text + "," + prolotqty.Text;
            string OperateType = "新增";
            string OperateNotes = "New生产订单不良* " + Contectext + " New生产订单不良* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计新增", OperateNotes);
        }

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveLotDefectUpdate()
        {
            //判断重复
            string strorder = proorder.SelectedItem.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P2d_OutputSubs
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
                                    (from p in DB.Pp_P2d_OutputSubs
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
                        (from p in DB.Pp_P2d_OutputSubs
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
            DB.Pp_Defect_P2d_Lots
               .Where(s => s.Proorder == strorder)

               .ToList()
               .ForEach(x => { x.Promodel = strProModel; x.Prolinename = strPline; x.Prodate = sedate; x.Modifier = GetIdentityName(); x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();

            //新增日志
            string Contectext = strorder + "," + strlot + "," + strPline + "," + sedate;
            string OperateType = "修改";
            string OperateNotes = "Edit生产不良*" + Contectext + " *Edit生产不良 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计修改", OperateNotes);
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
                CheckRepeatItem(proorder.SelectedItem.Text, "制二课", prodate.SelectedDate.Value.ToString("yyyyMMdd"));
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
            string inputOrder = proorder.SelectedItem.Text.Trim();

            Pp_Defect_P2d_Order currentOrder = DB.Pp_Defect_P2d_Orders.Where(u => u.Proorder == inputOrder && u.Prodept.Contains("PCBA")).FirstOrDefault();

            if (currentOrder != null)
            {
                SaveOrderDefectUpdate();
            }
            else
            {
                SaveOrderDefect();
            }
            //判断重复
            string inputLot = proorder.SelectedItem.Text.Trim();

            Pp_Defect_P2d_Order currentLot = DB.Pp_Defect_P2d_Orders.Where(u => u.Prolot == inputLot && u.Prodept.Contains("PCBA")).FirstOrDefault();

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

        #region Changed

        protected void proorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 获取用户输入的文本
                string inputText = proorder.Text.Trim();

                // 防抖机制：避免频繁搜索
                if (!string.IsNullOrEmpty(inputText) && inputText.Length >= 1 && inputText.Length <= 6)
                {
                    // 检查是否是重复搜索
                    if (inputText == lastSearchText && (DateTime.Now - lastSearchTime).TotalSeconds < 1)
                    {
                        return; // 1秒内重复搜索，直接返回
                    }

                    // 更新搜索记录
                    lastSearchText = inputText;
                    lastSearchTime = DateTime.Now;

                    // 显示搜索提示
                    proorder.EmptyText = "正在搜索...";

                    // 使用快速搜索方法
                    var orderNumbers = SearchOrdersFast(inputText);

                    // 更新下拉列表
                    proorder.Items.Clear();
                    proorder.Items.Add(new FineUIPro.ListItem("", ""));

                    foreach (var orderNo in orderNumbers)
                    {
                        proorder.Items.Add(new FineUIPro.ListItem(orderNo, orderNo));
                    }

                    // 如果找到匹配项，自动选择第一个
                    if (orderNumbers.Count > 0)
                    {
                        proorder.SelectedIndex = 1; // 跳过空项
                        string selectedOrder = proorder.SelectedItem.Text;

                        // 使用缓存的查询结果，避免重复查询
                        LoadOrderDetails(selectedOrder);
                    }
                    else
                    {
                        proorder.EmptyText = "未找到匹配的订单";
                        Alert.ShowInTop("未找到匹配的订单", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (this.proorder.SelectedIndex != -1 && this.proorder.SelectedIndex != 0)
                {
                    // 用户选择了下拉列表中的项目
                    string selectedOrder = proorder.SelectedItem.Text;
                    LoadOrderDetails(selectedOrder);
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

        /// <summary>
        /// 快速搜索6位订单号
        /// </summary>
        private List<string> SearchOrdersFast(string searchText)
        {
            try
            {
                // 利用6位固定长度的特点进行快速搜索
                if (searchText.Length == 6)
                {
                    // 精确匹配6位订单号
                    var exactMatch = DB.Pp_Orders
                        .Where(o => o.IsDeleted == 0)
                        .Where(o => o.Porderqty > 0)
                        .Where(o => o.Porderno == searchText)
                        .Select(o => o.Porderno)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(exactMatch))
                    {
                        return new List<string> { exactMatch };
                    }
                }
                else
                {
                    // 前缀匹配
                    return DB.Pp_Orders
                        .Where(o => o.IsDeleted == 0)
                        .Where(o => o.Porderqty > 0)
                        .Where(o => o.Porderno.Length == 6)
                        .Where(o => o.Porderno.StartsWith(searchText))
                        .OrderByDescending(o => o.Porderno)
                        .Select(o => o.Porderno)
                        .AsNoTracking()
                        .Take(20)
                        .ToList();
                }

                return new List<string>();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("搜索出错: " + ex.Message, MessageBoxIcon.Error);
                return new List<string>();
            }
        }

        /// <summary>
        /// 加载订单详细信息（优化版本）
        /// </summary>
        private void LoadOrderDetails(string orderNo)
        {
            try
            {
                // 使用单次查询获取所有需要的信息
                var orderInfo = DB.Pp_Orders
                    .Where(o => o.Porderno == orderNo && o.IsDeleted == 0)
                    .Select(o => new
                    {
                        o.Pordertype,
                        o.Porderno,
                        o.Porderhbn,
                        o.Porderlot,
                        o.Porderqty,
                        o.Porderdate,
                        o.Porderserial,
                        o.Porderreal
                    })
                    .AsNoTracking()
                    .FirstOrDefault();

                if (orderInfo != null)
                {
                    // 查询机种信息
                    var modelInfo = DB.Pp_Manhours
                        .Where(m => m.Proitem == orderInfo.Porderhbn)
                        .Select(m => m.Promodel)
                        .AsNoTracking()
                        .FirstOrDefault();

                    if (!string.IsNullOrEmpty(modelInfo))
                    {
                        remark.Text = orderInfo.Pordertype + "," + orderInfo.Porderno + "," + modelInfo + "," + orderInfo.Porderhbn + "+" + (string.IsNullOrEmpty(orderInfo.Porderlot) ? orderInfo.Porderno + "||" + orderInfo.Porderqty.ToString() : orderInfo.Porderlot);
                        pordertype.Text = orderInfo.Pordertype;
                        prohbn.Text = orderInfo.Porderhbn;
                        prolot.Text = (string.IsNullOrEmpty(orderInfo.Porderlot) ? orderInfo.Porderno + "||" + orderInfo.Porderqty.ToString() : orderInfo.Porderlot);
                        prolotqty.Text = orderInfo.Porderqty.ToString();
                        prosn.Text = orderInfo.Porderserial;
                        promodel.Text = modelInfo;
                    }
                    else
                    {
                        Alert.ShowInTop("机种信息不存在！请再次确认！！！", MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    Alert.ShowInTop("订单信息不存在！请再次确认！！！", String.Empty, ActiveWindow.GetHideReference());
                    return;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("加载订单详情时出错: " + ex.Message, MessageBoxIcon.Error);
            }
        }

        protected void prodirect_TextChanged(object sender, EventArgs e)
        {
            // HourQty();
        }

        #endregion Changed

        #region 计算产能率

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

        #endregion 计算产能率


    }
}