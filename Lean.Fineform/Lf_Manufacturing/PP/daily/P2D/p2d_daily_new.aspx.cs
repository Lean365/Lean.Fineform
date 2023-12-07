using FineUIPro;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;

namespace Lean.Fineform.Lf_Manufacturing.PP.daily.P2D
{

    public partial class p2d_daily_new : PageBase
    {
        public static string lclass, OPHID, ConnStr, nclass, ncode;
        public static int rowID, delrowID, editrowID, totalSum;

        public static string userid, badSum;
        public static string Prolot, Prolinename, Prodate, Prorealqty,strPline, strmaxDate, strminDate, Probadqty, Probadtotal, Probadcou;
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

        #endregion

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
            //上月第一天
            DateTime last = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(-1);
            //本月第一天时间 
            DateTime dt = DateTime.Today; 
            DateTime dt_First = dt.AddDays(-(dt.Day) + 1);


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
            BindDDLorder();

            BindDDLline();






        }







        #endregion
        #region BindGrid
        #endregion
        #region BindDDLData

        //DDL查询LOT
        private void BindDDLorder()
        {
            //UpdatingHelper.UpdatePorderQty();

            //查询LINQ去重复
            var q = from a in DB.Pp_Orders
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                        //where b.Proecnbomitem == stritem
                    where a.Porderqty - a.Porderreal > 0
                    orderby a.Porderno
                    select new
                    {
                        //b.Proecnmodel,
                        a.Porderno

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
        }





        //DDL查询班组
        private void BindDDLline()
        {

            //查询LINQ去重复
            var q = from a in DB.Pp_Lines
                        //join b in DB.Pp_EcnSubs on a.Porderhbn equals b.Proecnbomitem
                        //where b.Proecnno == strecn
                    where a.lineclass == "P"

                    select new
                    {
                        a.linecode,
                        a.linename,

                    };

            var qs = q.Select(E => new { E.linecode, E.linename }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            prolinename.DataSource = qs;
            prolinename.DataTextField = "linename";
            prolinename.DataValueField = "linename";
            prolinename.DataBind();


        }
        //DDL查询不良各类

        #endregion
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

        //    //    Pp_P2d_Output current = DB.Pp_P2d_Outputs.Find(del_ID);
        //    //    //删除日志
        //    //    string Newtext = current.ID + "," + current.Prolinename + "," + current.Prolot + "," + current.Prodate + "," + current.Prorealqty + "," + current.Prongclass + "," + current.Prongcode + "," + current.Probadqty + "," + current.Probadtotal + "," + current.Probadcou;
        //    //   string OperateType = "删除";//操作标记
        //    //    string OperateNotes = "Del生产* " + Newtext + " *Del 的记录已删除";
        //    //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报删除", OperateNotes);
        //    //    //删除记录
        //    //    //DB.Pp_Ecns.Where(l => l.ID == del_ID).Delete();
        //    //    current.isDelete = 1;
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


            Pp_P2d_Output item = new Pp_P2d_Output();

            //ophID();
            //item.OPHID = int.Parse(OPHID) + 1 + 1000;
            item.GUID = Guid.NewGuid();
            //item.Prolineclass = prolinename.SelectedValue.ToString();

            item.Prolinename = prolinename.SelectedItem.Text;


            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Prodirect = int.Parse(prodirect.Text);
            item.Proindirect = int.Parse(proindirect.Text);
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
            item.Promodel = promodel.Text;
            item.Prost = Decimal.Parse(prost.Text);
            //item.Prosubst = Decimal.Parse(prosubst.Text);
            item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);

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

            item.isDelete = 0;
            item.Remark = remark.Text;
            item.CreateTime = DateTime.Now;
            item.Creator = GetIdentityName();
            DB.Pp_P2d_Outputs.Add(item);
            DB.SaveChanges();
            //读取ID值
            OPHID = item.GUID.ToString();
            ParentID = item.ID;
            //新增日志

            string Contectext = ParentID + "," + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
            string OperateType = "新增";//操作标记
            string OperateNotes = "New生产OPH* " + Contectext + " *New生产OPH 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "生产日报新增", OperateNotes);


            SaveSubItem();





        }

        //新增新增生产日报单身
        /// <summary>
        /// 新增单身数据
        /// </summary>
        private void SaveSubItem()
        {
            try
            {
                var res = (from p in DB.Pp_Lines
                               //where p.Proitem.Contains(prohbn.Text)
                               where p.lineclass.Contains("P")
                           orderby p.linename
                           //where p.Age > 30 && p.Department == "研发部"
                           select p).ToList();

                int icount = res.Count();
                //判断查询是否为空
                if (res.Any())
                {
                    //遍历
                    for (int i = 0; i < icount; i++)
                    {
                        Pp_P2d_OutputSub item = new Pp_P2d_OutputSub();

                        // 添加父ID
                        //Pp_P2d_Output ID = Attach<Pp_P2d_Output>(Convert.ToInt32(ParentID));
                        item.Parent = ParentID;
                        item.Prolinename = res[i].linename.ToString();
                        item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                        item.Prodirect = int.Parse(prodirect.Text);
                        item.Proindirect = int.Parse(proindirect.Text);
                        item.Prolot = prolot.Text;
                        item.Prohbn = prohbn.Text;//prohbn.Text;
                        item.Proorderqty = Decimal.Parse(prolotqty.Text);
                        item.Promodel = promodel.Text;
                        item.Prost = Decimal.Parse(prost.Text);
                        //item.Prosubst = Decimal.Parse(prosubst.Text);
                        item.Prostdcapacity = Decimal.Parse(prostdcapacity.Text);
                        item.Totaltag = true;
                        item.Proorder = proorder.SelectedItem.Text;
                        item.GUID = Guid.Parse(OPHID);
                        item.Prostime = "0"; //res[i].Prostime.ToString();
                        item.Proetime = "0";// res[i].Proetime.ToString();
                        //item.Udf001 = prodate.SelectedDate.Value.ToString("yyyyMMdd");
                        //item.Udf002 = this.prolinename.SelectedItem.Text;
                        item.Remark = remark.Text;
                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        item.isDelete = 0;
                        DB.Pp_P2d_OutputSubs.Add(item);
                        DB.SaveChanges();

                        //新增日志

                        string Newtext = ParentID + "," + res[i].linename + "~" + res[i].linename + "," + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + prohbn.Text + "," + prostdcapacity.Text;
                        string OperateType = "新增";

                        string OperateNotes = "New生产OPH_SUB* " + Newtext + " *New生产OPH_SUB 的记录已新增";
                        OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH实绩新增", OperateNotes);
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
        private void SaveDefect()
        {

            Pp_DefectTotal item = new Pp_DefectTotal();

            item.Prolot = prolot.Text;
            item.Prolinename = prolinename.SelectedItem.Text;
            item.Prodate = prodate.SelectedDate.Value.ToString("yyyyMMdd");
            item.Proorder = proorder.SelectedItem.Text;
            item.Proorderqty = Convert.ToInt32(decimal.Parse(prolotqty.Text));
            item.Prorealqty = 0;
            item.Pronobadqty = 0;
            item.Probadtotal = 0;
            item.Prodirectrate = 0;
            item.Probadrate = 0;
            item.isDelete = 0;
            item.Remark = "";
            item.Promodel = promodel.Text;
            item.GUID = Guid.NewGuid();
            item.Creator = GetIdentityName();
            item.CreateTime = DateTime.Now;
            DB.Pp_DefectTotals.Add(item);
            DB.SaveChanges();

            //新增日志
            string Contectext = prolot.Text + prolinename.SelectedItem.Text + "," + prodate.SelectedDate.Value.ToString("yyyyMMdd") + "," + prolot.Text + "," + proorder.SelectedItem.Text + "," + prolotqty.Text;
            string OperateType = "新增";
            string OperateNotes = "New生产订单不良* " + Contectext + " New生产订单不良* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "不良管理", "工单不良集计新增", OperateNotes);
        }

        /// <summary>
        /// 更新订单不良合计表
        /// </summary>
        private void SaveDefectUpdate()
        {

            //判断重复
            string strorder = proorder.SelectedItem.Text.Trim();
            string strlot = prolot.Text.Trim();

            var line = (from p in DB.Pp_P2d_OutputSubs
                            //join b in DB.Pp_P2d_Outputs on p.OPHID equals b.OPHID
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
                        (from p in DB.Pp_P2d_OutputSubs
                             //join b in DB.proOutputs on p.OPHID equals b.OPHID
                         where p.Proorder == (strorder)
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
            DB.Pp_DefectTotals
               .Where(s => s.Proorder == strorder)

               .ToList()
               .ForEach(x => { x.Prolinename = strPline; x.Prodate = sedate; x.Modifier =GetIdentityName(); x.ModifyTime = DateTime.Now; });
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
            string input = proorder.SelectedItem.Text.Trim();


            Pp_DefectTotal current = DB.Pp_DefectTotals.Where(u => u.Proorder == input).FirstOrDefault();

            if (current != null)
            {
                SaveDefectUpdate();
            }
            else
            {
                SaveDefect();
            }



            //表格数据已重新绑定

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());


        }




        #endregion
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
        #endregion
        protected void proorder_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.proorder.SelectedIndex != -1 && this.proorder.SelectedIndex != 0)
            {
                try
                {
                    var q = from a in DB.Pp_Orders
                            join b in DB.Pp_Manhours on a.Porderhbn equals b.Proitem
                            //join c in DB.Pp_SapMaterials on a.Proecnoldhbn equals c.D_SAP_ZCA1D_Z002
                            where a.Porderno == proorder.SelectedItem.Text
                            where b.Prowctext.Contains(prolinename.SelectedItem.Text.Substring(0,1))
                            //where a.Proecnmodel == strProecnmodel
                            //where a.Proecnbomitem == strProecnbomitem
                            //where a.Proecnoldhbn == strProecnoldhbn
                            //where a.Proecnnewhbn == strProecnnewhbn
                            orderby b.Propset descending
                            select new
                            {
                                a.Porderno,
                                a.Porderhbn,
                                a.Porderlot,
                                a.Porderqty,
                                a.Porderdate,
                                a.Porderserial,
                                b.Prowcname,
                                b.Promodel,
                                b.Protext,
                                b.Prowctext,
                                b.Proshort,
                                b.Propset,
                                b.Prorate,
                                b.Prost,
                                b.Proset,
                                b.Prodesc,
                                a.Porderreal
                                //c.D_SAP_ZCA1D_Z033,

                            };
                    
                    if (q.Any())
                    {

                        // 切勿使用 source.Count() > 0
                        var qs = q.Where(s=>s.Prowctext.Contains(prolinename.SelectedItem.Text.Substring(0, 1))).Distinct().Take(1).ToList();
                        if (qs[0].Prost != 0)
                        {
                            prohbn.Text = qs[0].Porderhbn;
                            promodel.Text = qs[0].Promodel;
                            prolot.Text = qs[0].Porderlot;
                            prost.Text = qs[0].Prost.ToString();
                            proshort.Text = qs[0].Proshort.ToString();
                            prorate.Text = qs[0].Proshort.ToString();
                            prolotqty.Text = qs[0].Porderqty.ToString();
                            //prorealqty.Text = (decimal.Parse(DSstr1.Tables[0].Rows[0][3].ToString()) - decimal.Parse(DSstr1.Tables[0].Rows[0][16].ToString())).ToString();
                            prosn.Text = qs[0].Porderserial;
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
