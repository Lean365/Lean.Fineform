using Fine.Lf_Business.Models.QM;
using FineUIPro;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace Fine.Lf_Manufacturing.QM.fqc
{

    public partial class fqc_edit : PageBase
    {
        public static DataTable CheckD;
        //是否添加到末尾 false=末尾,true=开头
        private bool AppendToEnd = false;
        public string issueNo;
        public static string mysql, userid;
        public decimal qaQTY, mcQTY;
        public static int rowID, delrowID, editrowID, totalSum;
        public static string dqmSpecialNotes, dqmSamplinglevel, dqmCheckmethod, dqmSamplingQty, dqmJudgment, dqmJudgmentlevel, dqmRejectqty, dqmCheckNotes, dqmAcceptqty, dqmOrderqty, dqmCheckout, dqmProqty;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcEdit";
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
            userid = GetIdentityName();
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            mysql = "select * from [dbo].[Qm_Outgoing];";
            CheckD =ConvertHelper.GetDataTable(mysql);
            // 新增数据初始值
            JObject defaultObj = new JObject();
            defaultObj.Add("qmAcceptqty", "0");
            defaultObj.Add("qmRejectqty", "0");
            defaultObj.Add("qmSamplingQty", "0");
            //defaultObj.Add("ddlddlqmJudgmentlevel", "5");
            //defaultObj.Add("htmlqmCheckNotes", qmLotserial.Text);
            //defaultObj.Add("AtSchool", false);
            //defaultObj.Add("Major", "化学系");
            //defaultObj.Add("Delete", String.Format("<a href=\"javascript:;\" onclick=\"{0}\"><img src=\"{1}\"/></a>", deleteScript, IconHelper.GetResolvedIconUrl(Icon.Delete)));

            // 在第一行新增一条数据
            btnNew.OnClientClick = Grid1.GetAddNewRecordReference(defaultObj, AppendToEnd);

            // 重置表格
            btnReset.OnClientClick = Grid1.GetRejectChangesReference();
            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            //mocToDropDownList();
            BindDDLStdClass();
            BindDDLInsClass();
            BindDDLLevel();
            BindDDLcLevel();
            BindData();
            BindStock();
            BindGrid();
        }


        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Qm_Outgoing current = DB.Qm_Outgoings.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            qmInspector.Text = current.qmInspector;//检查员
            qmLine.Text = current.qmLine;//班别
            qmOrder.Text = current.qmOrder;//订单
            qmModels.Text = current.qmModels;//机种
            qmMaterial.Text = current.qmMaterial;//物料
            qmCheckdate.Text = current.qmCheckdate;//检查日期
            qmProlot.Text = current.qmProlot;//批号

            qmLotserial.Text = current.qmLotserial;//批号说明

            qmOrderqty.Text = current.qmOrderqty.ToString();

            //Qacheckguid.Text = current.Qacheckguid;//GUID
            BindTextData();
            BindCheckNum();


            //修改前日志
            string BeforeModi = current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmCheckmethod + "," + current.qmSamplingQty + "," + current.qmJudgment + "," + current.qmJudgmentlevel + "," + current.qmRejectqty + "," + current.qmCheckNotes;
            string OperateType = "修改";
            string OperateNotes = "beEdit* " + BeforeModi + " *beEdit 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品检验修改", OperateNotes);
        }
        private void BindCheckNum()
        {
            var qCty = from a in DB.Qm_Outgoings
                           //join b in DB.Pp_Manhours on a.Prohbn equals b.Proitem
                           //where b.Ec_no == strecn
                       where a.qmOrder.Contains(this.qmOrder.Text)
                       group a by a.qmOrder into g
                       //where a.Prolinename.Contains(this.qmLine.SelectedItem.Text)

                       select new
                       {

                           Cnum = g.Count()
                       };
            var qcs = qCty.ToList();

            if (qcs.Any())
            {
                qmCheckout.Text = (int.Parse(qcs[0].Cnum.ToString()) + 1).ToString();
            }
            else
            {
                qmCheckout.Text = "1";
            }
        }
        private void BindGrid()
        {

            IQueryable<Qm_Outgoing> q = DB.Qm_Outgoings; //.Include(u => u.Dept);

            // 在用户名称中搜索

            string mocorder = qmOrder.Text;
            string mocline = qmLine.Text;
            q = q.Where(u => u.isDelete == 0 && u.qmOrder.Contains(mocorder));
            q = q.Where(u => u.qmLine.Contains(mocline));

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
            q = SortAndPage<Qm_Outgoing>(q, Grid1);
            try
            {
                Grid1.DataSource = q;
                Grid1.DataBind();
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
            ConvertHelper.LinqConvertToDataTable(q);
            // 当前页的合计
            OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));




        }
        private void BindStock()
        {
            var q =
                   from p in DB.Qm_Outgoings
                   where p.qmOrder == this.qmOrder.Text
                   where p.isDelete == 0
                   group p by p.qmOrder into g
                   select new
                   {
                       g.Key,
                       TotalQty = g.Sum(p => p.qmAcceptqty)
                   };
            var qs = q.Distinct().ToList();

            if (qs.Any())
            {
                StockNum.Text = qs[0].TotalQty.ToString();
                decimal ss = decimal.Parse(this.qmOrderqty.Text) - decimal.Parse(qs[0].TotalQty.ToString());
                SurplusNum.Text = ss.ToString();
            }
            else
            {
                StockNum.Text = "0";
                SurplusNum.Text = this.qmOrderqty.Text;
            }
        }
        private void BindTextData()
        {
            int id = GetQueryIntValue("id");
            Qm_Outgoing current = DB.Qm_Outgoings.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            var q = from a in DB.Pp_Manhours
                        //join b in DB.Pp_Manhours on a.Prohbn equals b.Proitem
                        //where b.Ec_no == strecn
                        //where a.Proorder.Contains(this.qmOrder.SelectedItem.Text)
                    where a.Proitem.Contains(current.qmMaterial)

                    select new
                    {
                        a.Protext,
                        a.Prodesc,


                    };

            var qs = q.Select(E => new
            {
                E.Protext,
                E.Prodesc,
            }).ToList();


            if (qs.Any())
            {
                LaText.Text = qs[0].Protext.ToString();//物料TXT

                LaOrgin.Text = qs[0].Prodesc.ToString();//仕向
            }

        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //获取ID号
            int del_ID = Convert.ToInt32(Grid1.DataKeys[e.RowIndex][0]);

            //int del_ID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreFqcDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Outgoing current = DB.Qm_Outgoings.Find(del_ID);
                //删除日志
                string DelTxt = current.ID + "," + current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmCheckmethod;
                string DelOperateType = "修改";
                string DelOperateNotes = "beDel* " + DelTxt + " *beDel 的记录可能将被修改";
                OperateLogHelper.InsNetOperateNotes(userid,DelOperateType, "质量管理", "产品检验删除", DelOperateNotes);
                //删除记录
                //DB.proDefects.Where(l => l.ID == del_ID).Delete();

                current.isDelete = 1;
                current.Modifier = GetIdentityName();
                current.ModifyTime = DateTime.Now;
                DB.SaveChanges();
                //重新绑定
                BindGrid();

            }
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreFqcEdit", Grid1, "deleteField");
            // 设置LinkButtonField的点击客户端事件


        }
        #endregion

        #region Events
        //判断修改内容||判断重复
        private void CheckData()
        {

            //int id = GetQueryIntValue("id");
            //Qm_Outgoing current = DB.Qm_Outgoings.Find(id);



            ////int id = GetQueryIntValue("id");
            ////proLinestop current = DB.proLinestops.Find(id);
            //////decimal cQcpd005 = current.Qcpd005;
            ////string checkdata1 = current.Prostoptext;


            ////if (this.Prostoptext.Text == checkdata1)//decimal.Parse(this.LF001.Text) == cLF001 && this.Qcpd005.Text == cQcpd004)
            ////{
            ////    Alert alert = new Alert();
            ////    alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            ////    alert.IconUrl = "~/Lf_Resources/images/success.png";
            ////    alert.Target = Target.Top;
            ////    Alert.ShowInTop();
            ////    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            ////}

            ////string InputData = Qcpd003.Text.Trim();


            ////proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            ////if (redata != null)
            ////{
            ////    Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            ////    return;
            ////}
            ////string InputData = qmLine.Text.Trim()+ qmCheckdate.Text.Trim() + qmOrder.Text.Trim() + qmModels.Text.Trim() + qmMaterial.Text.Trim() + qmProlot.Text.Trim() + qmLotserial.Text.Trim();


            ////Qm_Outgoing Redata = DB.Qm_Outgoings.Where(u => u.qmLine == InputData).FirstOrDefault();

            ////if (Redata != null)
            ////{
            ////    Alert.ShowInTop("分析对策数据,判定说明说明方式< " + InputData + ">已经存在！修改即可");
            ////    return;
            ////}
            ////判断抽样数量
            //if (numqmProqty.Text == "0")
            //{
            //    Alert.ShowInTop("送样数量不能为0", "错误提示", MessageBoxIcon.Error);
            //    //Alert alert = new Alert();
            //    //alert.Message = "当判定为[拒收]时,不良级别不能是[合格].";
            //    //alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    //alert.Target = Target.Top;
            //    //Alert.ShowInTop();
            //    return;
            //}
            ////判断送样数量
            //if (numqmSamplingQty.Text == "0")
            //{
            //    Alert.ShowInTop("抽样数量不能为0", "错误提示", MessageBoxIcon.Error);
            //    //Alert alert = new Alert();
            //    //alert.Message = "当判定为[拒收]时,不良级别不能是[合格].";
            //    //alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    //alert.Target = Target.Top;
            //    //Alert.ShowInTop();
            //    return;
            //}
            ////判断合格
            //if (ddlqmJudgment.SelectedItem.Text != "允收")
            //{
            //    if (ddlqmJudgmentlevel.SelectedItem.Text != "合格")
            //    {
            //        //工单数量
            //        string MA002str;

            //        MA002str = "SELECT MC005 FROM [dbo].[proOrders]  WHERE MC006='" + this.qmOrder.Text + "'";
            //        SqlDataAdapter MA002DA = new SqlDataAdapter(MA002str, AppConn);
            //        DataSet MA002DS = new DataSet();
            //        MA002DA.Fill(MA002DS);
            //        mcQTY = decimal.Parse(MA002DS.Tables[0].Rows[0][0].ToString());

            //        //QA检验入库数量
            //        string MA003str;
            //        MA003str = "SELECT SUM(qmAcceptqty) AS qaQTY FROM [dbo].[Qm_Outgoings]  WHERE qmOrder='" + this.qmOrder.Text + "'";
            //        SqlDataAdapter MA003DA = new SqlDataAdapter(MA003str, AppConn);
            //        DataSet MA003DS = new DataSet();
            //        MA003DA.Fill(MA003DS);

            //        if (MA003DS.Tables[0].Rows[0][0].ToString().Length != 0)
            //        {
            //            qaQTY = decimal.Parse(MA003DS.Tables[0].Rows[0][0].ToString());
            //        }
            //        else
            //        {
            //            qaQTY = 0;
            //        }
            //    }
            //    else
            //    {
            //        Alert.ShowInTop("当判定为[拒收]时,不良级别不能是[合格]！", "错误提示", MessageBoxIcon.Error);
            //        return;
            //    }
            //}


            //if (qaQTY > mcQTY)
            //{
            //    //入库超出日志
            //    string Newtext = this.qmOrder.Text + "," + qaQTY + "," + mcQTY;
            //    string OperateType = this.Qacheckguid.Text;
            //    string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            //    OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品验收超出", OperateNotes);

            //    Alert.ShowInTop("工单 " + this.qmLine.Text + " 已经超检验入库！", "错误提示", MessageBoxIcon.Error);
            //    return;
            //}
            //if (this.ddlqmJudgment.SelectedIndex != 1 || this.ddlqmJudgment.SelectedIndex != 0)
            //{
            //    if (decimal.Parse(this.numqmAcceptqty.Text) == 0)
            //    {
            //        SaveNotice();
            //        SaveAction();

            //    }
            //}
            EditFqcDataRow();
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        //判断下拉列表选项
        private void CheckDDLData()
        {
            //if (qmInspector.SelectedItem.Text == "请选择")// && qmLine.SelectedIndex> 1 && qmOrder.SelectedIndex > 1 && ddlqmCheckmethod.SelectedIndex > 1 && ddlqmJudgment.SelectedIndex > 1 && ddlqmJudgmentlevel.SelectedIndex > 1)
            //{
            //    Alert alert = new Alert();
            //    alert.Message = "下拉列表请选择正确的项目.";
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    return;
            //}
            //if (qmLine.SelectedItem.Text == "请选择")
            //{
            //    Alert alert = new Alert();
            //    alert.Message = "下拉列表请选择正确的项目.";
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    return;
            //}
            //if (qmOrder.SelectedItem.Text == "请选择")
            //{
            //    Alert alert = new Alert();
            //    alert.Message = "下拉列表请选择正确的项目.";
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    return;
            //}
            //if (ddlqmCheckmethod.SelectedItem.Text == "请选择")
            //{
            //    Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
            //    return;
            //}
            //if (ddlqmJudgment.SelectedItem.Text == "请选择")
            //{
            //    Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
            //    return;
            //}
            //if (ddlqmJudgmentlevel.SelectedItem.Text == "请选择")
            //{
            //    Alert.ShowInTop("下拉列表请选择正确的项目！", "错误提示", MessageBoxIcon.Error);
            //    return;
            //}
            CheckData();
        }
        //发行NO，Uint64格式
        public void BindissueNoData()
        {
            string sdate = qmCheckdate.Text;

            var q = from a in DB.Qm_Unqualifieds
                    where a.qmCheckdate.Contains(sdate)
                    group a by a.qmCheckdate into p
                    select new
                    {
                        issueNo = p.Max(x => x.qmIssueno),
                    };
            var qs = q.Distinct().ToList();

            if (qs.Any())

            {
                issueNo = (UInt64.Parse(qs[0].issueNo) + 1).ToString();

            }
            else
            {
                issueNo = qmCheckdate.Text + "001";
            }
        }
        //新增不良联络及分析对策数据
        private void SaveNotice()
        {
            BindissueNoData();
            string prolot = this.qmProlot.Text;
            string proorder = this.qmOrder.Text;
            string prodate = this.qmCheckdate.Text;
            string proline = this.qmLine.Text;

            var reult = (from a in DB.Qm_Unqualifieds
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select a).ToList();
            if (!reult.Any())
            {
                var q = (from a in DB.Qm_Outgoings
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select a).ToList();
                if (q.Any())
                {
                    List<Qm_Unqualified> AddUnqualifiedList = (from a in q
                                                               select new Qm_Unqualified
                                                               {
                                                                   qmInspector = a.qmInspector, //检查人员
                                                                   qmLine = a.qmLine,//班别
                                                                   qmOrder = a.qmOrder,//生产工单
                                                                   qmModels = a.qmModels,//机种
                                                                   qmMaterial = a.qmMaterial,//物料
                                                                   qmRegion = LaOrgin.Text,//仕向
                                                                   qmCheckdate = a.qmCheckdate,//查检日期
                                                                   qmProlot = a.qmProlot,//生产批号
                                                                   qmLotserial = a.qmLotserial,//批号说明
                                                                   qmRejectqty = a.qmRejectqty,//验退数
                                                                   qmJudgmentlevel = a.qmJudgmentlevel,//不良级别
                                                                   qmCheckNotes = a.qmCheckNotes,//判定说明

                                                                   qmIssueno = issueNo,//发行NO
                                                                   GUID = Guid.NewGuid(),// Qacheckguid.Text;//GUID
                                                                                         //item.htmlRemark = htmlRemark.Text;
                                                                   Creator = GetIdentityName(),
                                                                   CreateTime = DateTime.Now,


                                                               }).ToList();
                    DB.BulkInsert(AddUnqualifiedList);
                    DB.BulkSaveChanges();
                }
            }
            else

            {
                var q = (from a in DB.Qm_Outgoings
                         join b in DB.Qm_Unqualifieds on new
                         {
                             qmProlot = a.qmProlot,
                             qmOrder = a.qmOrder,
                             qmCheckdate = a.qmCheckdate,
                             qmLine = a.qmLine
                         }
                         equals
                         new
                         {
                             qmProlot = b.qmProlot,
                             qmOrder = b.qmOrder,
                             qmCheckdate = b.qmCheckdate,
                             qmLine = b.qmLine
                         }

                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select new
                         {
                             b.GUID,
                             b.qmInspector,
                             b.qmLine,
                             b.qmOrder,
                             b.qmModels,
                             b.qmMaterial,
                             b.qmRegion,
                             b.qmCheckdate,
                             b.qmProlot,
                             b.qmLotserial,
                             a.qmRejectqty,
                             b.qmJudgmentlevel,
                             a.qmCheckNotes,
                             b.qmIssueno,
                             b.UDF01,
                             b.UDF02,
                             b.UDF03,
                             b.UDF04,
                             b.UDF05,
                             b.UDF06,
                             b.UDF51,
                             b.UDF52,
                             b.UDF53,
                             b.UDF54,
                             b.UDF55,
                             b.UDF56,

                             b.isDelete,
                             b.Remark,
                             b.Creator,
                             b.CreateTime,
                             b.Modifier,
                             b.ModifyTime,
                         }).ToList();
                if (q.Any())
                {
                    List<Qm_Unqualified> UpdateUnqualifiedList = (from a in q
                                                                  select new Qm_Unqualified
                                                                  {
                                                                      GUID = a.GUID,
                                                                      qmInspector = a.qmInspector,
                                                                      qmLine = a.qmLine,
                                                                      qmOrder = a.qmOrder,
                                                                      qmModels = a.qmModels,
                                                                      qmMaterial = a.qmMaterial,
                                                                      qmRegion = a.qmRegion,
                                                                      qmCheckdate = a.qmCheckdate,
                                                                      qmProlot = a.qmProlot,
                                                                      qmLotserial = a.qmLotserial,
                                                                      qmRejectqty = a.qmRejectqty,
                                                                      qmJudgmentlevel = a.qmJudgmentlevel,
                                                                      qmCheckNotes = a.qmCheckNotes,
                                                                      qmIssueno = a.qmIssueno,
                                                                      UDF01 = a.UDF01,
                                                                      UDF02 = a.UDF02,
                                                                      UDF03 = a.UDF03,
                                                                      UDF04 = a.UDF04,
                                                                      UDF05 = a.UDF05,
                                                                      UDF06 = a.UDF06,
                                                                      UDF51 = a.UDF51,
                                                                      UDF52 = a.UDF52,
                                                                      UDF53 = a.UDF53,
                                                                      UDF54 = a.UDF54,
                                                                      UDF55 = a.UDF55,
                                                                      UDF56 = a.UDF56,

                                                                      isDelete = a.isDelete,
                                                                      Remark = a.Remark,
                                                                      Creator = a.Creator,
                                                                      CreateTime = a.CreateTime,
                                                                      Modifier = GetIdentityName(),
                                                                      ModifyTime = DateTime.Now,


                                                                  }).ToList();
                    DB.BulkUpdate(UpdateUnqualifiedList);
                    DB.BulkSaveChanges();
                }
            }


            //Qm_Unqualified item = new Qm_Unqualified();
            //item.qmInspector = qmInspector.SelectedItem.Text; //检查人员
            //item.qmLine = qmLine.SelectedItem.Text;//班别
            //item.qmOrder = qmOrder.SelectedItem.Text;//生产工单
            //item.qmModels = qmModels.Text;//机种
            //item.qmMaterial = qmMaterial.Text;//物料
            //item.qmRegion = LaOrgin.Text;//仕向
            //item.qmCheckdate = qmCheckdate.SelectedDate.Value.ToString("yyyyMMdd");//查检日期
            //item.qmProlot = qmProlot.Text;//生产批号
            //item.qmLotserial = qmLotserial.Text;//批号说明
            //item.qmRejectqty = decimal.Parse(numqmRejectqty.Text);//验退数
            //item.qmJudgmentlevel = ddlqmJudgmentlevel.SelectedItem.Text;//不良级别
            //item.qmCheckNotes = htmlqmCheckNotes.Text;//判定说明

            //item.qmIssueno = issueNo;//发行NO
            //item.GUID = Guid.NewGuid();// Qacheckguid.Text;//GUID
            ////item.htmlRemark = htmlRemark.Text;
            //item.Creator = GetIdentityName();
            //item.CreateTime = DateTime.Now;

            //DB.Qm_Unqualifieds.Add(item);
            //DB.SaveChanges();

            //新增日志
            string Newtext = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + ddlqmCheckmethod.Text + "," + numqmSamplingQty.Text + "," + ddlqmJudgment.Text + "," + ddlqmJudgmentlevel.Text + "," + numqmRejectqty.Text + "," + htmlqmCheckNotes.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不合格通知新增", OperateNotes);
        }
        private void SaveAction()
        {
            BindissueNoData();
            string prolot = this.qmProlot.Text;
            string proorder = this.qmOrder.Text;
            string prodate = this.qmCheckdate.Text;
            string proline = this.qmLine.Text;

            var reult = (from a in DB.Qm_Improvements
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select a).ToList();
            if (!reult.Any())
            {
                var q = (from a in DB.Qm_Outgoings
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select a).ToList();
                if (q.Any())
                {
                    List<Qm_Improvement> AddImproveList = (from a in q
                                                           select new Qm_Improvement
                                                           {
                                                               GUID = Guid.NewGuid(),// Qacheckguid.Text,//GUID
                                                               qmInspector = a.qmInspector,//检查人员
                                                               qmLine = a.qmLine,//班别
                                                               qmOrder = a.qmOrder,//生产工单
                                                               qmModels = a.qmModels,//机种
                                                               qmMaterial = a.qmMaterial,//物料
                                                               qmRegion = LaOrgin.Text,//仕向
                                                               qmCheckdate = a.qmCheckdate,//查检日期
                                                               qmProlot = a.qmProlot,//生产批号
                                                               qmLotserial = a.qmLotserial,//批号说明
                                                               qmRejectqty = a.qmRejectqty,//验退数
                                                               qmJudgmentlevel = a.qmJudgmentlevel,//不良级别
                                                               qmCheckNotes = a.qmCheckNotes,//不良内容
                                                                                             //原因分析及对策实施
                                                               qmPersonnel = "Wait for Reply",//对应人员
                                                               qmDate = a.qmCheckdate,//对应日期
                                                               qmDirectreason = "Wait for Reply",//直接发生原因
                                                               qmIndirectreason = "Wait for Reply",//间接流出原因
                                                               qmDisposal = "Wait for Reply",//处置
                                                               qmDirectsolutions = "Wait for Reply",//直接对策
                                                               qmIndirectsolutions = "Wait for Reply",//间接对策
                                                                                                      //对策确认
                                                               qmVerify = "Wait for Reply",//检证人员
                                                               qmCarryoutdate = a.qmCheckdate,//实施日期

                                                               qmCarryoutverify = false,//实施检证

                                                               qmSolutionsverify = "Wait for Reply",//对策实施检证
                                                               qmNotes = "Wait for Reply",//特记事项
                                                                                          //BindissueNoData(),
                                                               qmIssueno = issueNo,//发行NO
                                                                                   //Remark =a. htmlRemark.Text,
                                                               Creator = GetIdentityName(),
                                                               CreateTime = DateTime.Now,


                                                           }).ToList();
                    DB.BulkInsert(AddImproveList);
                    DB.BulkSaveChanges();
                }
            }
            else

            {
                var q = (from a in DB.Qm_Outgoings
                         join b in DB.Qm_Improvements on new
                         {
                             qmProlot = a.qmProlot,
                             qmOrder = a.qmOrder,
                             qmCheckdate = a.qmCheckdate,
                             qmLine = a.qmLine
                         }
                         equals
                         new
                         {
                             qmProlot = b.qmProlot,
                             qmOrder = b.qmOrder,
                             qmCheckdate = b.qmCheckdate,
                             qmLine = b.qmLine
                         }

                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDelete == 0
                         select new
                         {
                             b.GUID,
                             b.qmInspector,
                             b.qmLine,
                             b.qmOrder,
                             b.qmModels,
                             b.qmMaterial,
                             b.qmRegion,
                             b.qmCheckdate,
                             b.qmProlot,
                             b.qmLotserial,
                             a.qmRejectqty,
                             a.qmJudgmentlevel,
                             a.qmCheckNotes,
                             b.qmPersonnel,
                             b.qmDate,
                             b.qmDirectreason,
                             b.qmIndirectreason,
                             b.qmDisposal,
                             b.qmDirectsolutions,
                             b.qmIndirectsolutions,
                             b.qmVerify,
                             b.qmCarryoutdate,
                             b.qmCarryoutverify,
                             b.qmSolutionsverify,
                             b.qmNotes,
                             b.qmIssueno,
                             b.UDF01,
                             b.UDF02,
                             b.UDF03,
                             b.UDF04,
                             b.UDF05,
                             b.UDF06,
                             b.UDF51,
                             b.UDF52,
                             b.UDF53,
                             b.UDF54,
                             b.UDF55,
                             b.UDF56,

                             b.isDelete,
                             b.Remark,
                             b.Creator,
                             b.CreateTime,
                             b.Modifier,
                             b.ModifyTime,
                         }).ToList();
                if (q.Any())
                {
                    List<Qm_Improvement> UpdateImproveList = (from a in q
                                                              select new Qm_Improvement
                                                              {
                                                                  GUID = a.GUID,
                                                                  qmInspector = a.qmInspector,
                                                                  qmLine = a.qmLine,
                                                                  qmOrder = a.qmOrder,
                                                                  qmModels = a.qmModels,
                                                                  qmMaterial = a.qmMaterial,
                                                                  qmRegion = a.qmRegion,
                                                                  qmCheckdate = a.qmCheckdate,
                                                                  qmProlot = a.qmProlot,
                                                                  qmLotserial = a.qmLotserial,
                                                                  qmRejectqty = a.qmRejectqty,
                                                                  qmJudgmentlevel = a.qmJudgmentlevel,
                                                                  qmCheckNotes = a.qmCheckNotes,
                                                                  qmPersonnel = a.qmPersonnel,
                                                                  qmDate = a.qmDate,
                                                                  qmDirectreason = a.qmDirectreason,
                                                                  qmIndirectreason = a.qmIndirectreason,
                                                                  qmDisposal = a.qmDisposal,
                                                                  qmDirectsolutions = a.qmDirectsolutions,
                                                                  qmIndirectsolutions = a.qmIndirectsolutions,
                                                                  qmVerify = a.qmVerify,
                                                                  qmCarryoutdate = a.qmCarryoutdate,
                                                                  qmCarryoutverify = a.qmCarryoutverify,
                                                                  qmSolutionsverify = a.qmSolutionsverify,
                                                                  qmNotes = a.qmNotes,
                                                                  qmIssueno = a.qmIssueno,
                                                                  UDF01 = a.UDF01,
                                                                  UDF02 = a.UDF02,
                                                                  UDF03 = a.UDF03,
                                                                  UDF04 = a.UDF04,
                                                                  UDF05 = a.UDF05,
                                                                  UDF06 = a.UDF06,
                                                                  UDF51 = a.UDF51,
                                                                  UDF52 = a.UDF52,
                                                                  UDF53 = a.UDF53,
                                                                  UDF54 = a.UDF54,
                                                                  UDF55 = a.UDF55,
                                                                  UDF56 = a.UDF56,

                                                                  isDelete = a.isDelete,
                                                                  Remark = a.Remark,
                                                                  Creator = a.Creator,
                                                                  CreateTime = a.CreateTime,
                                                                  Modifier = GetIdentityName(),
                                                                  ModifyTime = DateTime.Now,


                                                              }).ToList();
                    DB.BulkUpdate(UpdateImproveList);
                    DB.BulkSaveChanges();
                }
            }
            //Qm_Improvement item = new Qm_Improvement();
            //item.GUID = Guid.NewGuid();// Qacheckguid.Text;//GUID
            //item.qmInspector = qmInspector.SelectedItem.Text;//检查人员
            //item.qmLine = qmLine.SelectedItem.Text;//班别
            //item.qmOrder = qmOrder.SelectedItem.Text;//生产工单
            //item.qmModels = qmModels.Text;//机种
            //item.qmMaterial = qmMaterial.Text;//物料
            //item.qmRegion = LaOrgin.Text;//仕向
            //item.qmCheckdate = qmCheckdate.SelectedDate.Value.ToString("yyyyMMdd");//查检日期
            //item.qmProlot = qmProlot.Text;//生产批号
            //item.qmLotserial = qmLotserial.Text;//批号说明
            //item.qmRejectqty = decimal.Parse(numqmRejectqty.Text);//验退数
            //item.qmJudgmentlevel = ddlqmJudgmentlevel.SelectedItem.Text;//不良级别
            //item.qmCheckNotes = htmlqmCheckNotes.Text;//不良内容
            ////原因分析及对策实施
            //item.qmPersonnel = "Wait for Reply";//对应人员
            //item.qmDate = qmCheckdate.SelectedDate.Value.ToString("yyyyMMdd");//对应日期
            //item.qmDirectreason = "Wait for Reply";//直接发生原因
            //item.qmIndirectreason = "Wait for Reply";//间接流出原因
            //item.qmDisposal  = "Wait for Reply";//处置
            //item.qmDirectsolutions = "Wait for Reply";//直接对策
            //item.qmIndirectsolutions = "Wait for Reply";//间接对策
            ////对策确认
            //item.qmVerify = "Wait for Reply";//检证人员
            //item.qmCarryoutdate = qmCheckdate.SelectedDate.Value.ToString("yyyyMMdd");//实施日期

            //item.qmCarryoutverify = false;//实施检证

            //item.qmSolutionsverify = "Wait for Reply";//对策实施检证
            //item.qmNotes = "Wait for Reply";//特记事项
            ////BindissueNoData();
            //item.qmIssueno = issueNo;//发行NO
            ////item.Remark = htmlRemark.Text;
            //item.Creator = GetIdentityName();
            //item.CreateTime = DateTime.Now;

            //DB.Qm_Improvements.Add(item);
            //DB.SaveChanges();
            //新增日志
            string Newtext = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + ddlqmCheckmethod.Text + "," + numqmSamplingQty.Text + "," + ddlqmJudgment.Text + "," + ddlqmJudgmentlevel.Text + "," + numqmRejectqty.Text + "," + htmlqmCheckNotes.Text;
            string OperateType = "新增";
            string OperateNotes = "New* " + Newtext + " New* 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "分析对策新增", OperateNotes);
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            CheckDDLData();
            SaveNotice();
            SaveAction();
        }


        //判断操作类型

        private void EditFqcDataRow()
        {
            // 删除现有数据
            List<int> deletedRows = Grid1.GetDeletedList();
            foreach (int rowIndex in deletedRows)
            {
                delrowID = Convert.ToInt32(Grid1.DataKeys[rowIndex][0]);
                Qm_Outgoing current = DB.Qm_Outgoings
                    .Where(u => u.ID == delrowID).FirstOrDefault();
                //删除日志
                string DelTxt = current.ID + "," + current.qmInspector + "," + current.qmLine + "," + current.qmOrder + "," + current.qmModels + "," + current.qmMaterial + "," + current.qmCheckdate + "," + current.qmProlot + "," + current.qmLotserial + "," + current.qmCheckmethod;
                string DelOperateType = current.ID.ToString();
                string DelOperateNotes = "beEdit* " + DelTxt + " *beEdit 的记录可能将被修改";
                OperateLogHelper.InsNetOperateNotes(userid,DelOperateType, "质量管理", "产品检验", DelOperateNotes);
                current.isDelete = 1;
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

                UpdateFqcDataRow(modifiedDict[rowIndex], row);
            }


            // 新增数据
            List<Dictionary<string, object>> newAddedList = Grid1.GetNewAddedList();

            //mysql = "select * from [dbo].[proDefects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            if (AppendToEnd)
            {
                if (newAddedList.Count != 0)
                {
                    for (int i = 0; i < newAddedList.Count; i++)
                    {
                        DataRow rowData = CreateNewData(CheckD, newAddedList[i]);

                        CheckD.Rows.Add(rowData);

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


                            dqmSamplinglevel = tb.Rows[0]["qmSamplinglevel"].ToString();
                            dqmCheckmethod = tb.Rows[0]["qmCheckmethod"].ToString();
                            //班组
                            dqmProqty = tb.Rows[0]["qmProqty"].ToString();
                            dqmSamplingQty = tb.Rows[0]["qmSamplingQty"].ToString();
                            dqmJudgment = tb.Rows[0]["qmJudgment"].ToString();
                            dqmAcceptqty = tb.Rows[0]["qmAcceptqty"].ToString();
                            dqmJudgmentlevel = tb.Rows[0]["qmJudgmentlevel"].ToString();
                            dqmRejectqty = tb.Rows[0]["qmRejectqty"].ToString();
                            //Probadtotal = tb.Rows[tb.Rows.Count - 1]["Probadtotal"].ToString();
                            dqmCheckNotes = tb.Rows[0]["qmCheckNotes"].ToString();
                            dqmSpecialNotes = tb.Rows[0]["qmSpecialNotes"].ToString();

                        }
                        Qm_Outgoing item = new Qm_Outgoing();
                        //检查员
                        item.qmInspector = this.qmInspector.Text;
                        //班别
                        item.qmLine = this.qmLine.Text;
                        //生产工单
                        item.qmOrder = this.qmOrder.Text;
                        //机种
                        item.qmModels = this.qmModels.Text;
                        //物料
                        item.qmMaterial = this.qmMaterial.Text;
                        //查检日期
                        item.qmCheckdate = this.qmCheckdate.Text;
                        //生产批号
                        item.qmProlot = this.qmProlot.Text;
                        //批号说明
                        item.qmLotserial = this.qmLotserial.Text;
                        //检验方式
                        item.qmCheckmethod = dqmCheckmethod;
                        //抽样数
                        item.qmSamplingQty = decimal.Parse(dqmSamplingQty);
                        //判定
                        item.qmJudgment = dqmJudgment;
                        //不良级别
                        item.qmJudgmentlevel = dqmJudgmentlevel;
                        //验退数
                        item.qmRejectqty = decimal.Parse(dqmRejectqty);
                        //判定说明
                        item.qmCheckNotes = dqmCheckNotes;
                        //验收数
                        item.qmAcceptqty = decimal.Parse(dqmAcceptqty);
                        //生产订单数量
                        item.qmOrderqty = decimal.Parse(qmOrderqty.Text);
                        //检验次数
                        item.qmCheckout = int.Parse(qmCheckout.Text);
                        //生产数
                        item.qmProqty = decimal.Parse(dqmProqty);
                        //检验水准
                        item.qmSamplinglevel = dqmSamplinglevel;
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

                        item.isDelete = 0;
                        item.qmSpecialNotes = dqmSpecialNotes;
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Qm_Outgoings.Add(item);
                        DB.SaveChanges();



                        //新建日志
                        string NewText = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + ddlqmCheckmethod.Text + "," + numqmSamplingQty.Text + "," + ddlqmJudgment.Text + "," + ddlqmJudgmentlevel.Text + "," + numqmRejectqty.Text + "," + htmlqmCheckNotes.Text;
                        string NewOperateType = "新增";
                        string NewOperateNotes = "New*产品检验 " + NewText + " New*产品检验 的记录已经新增";
                        OperateLogHelper.InsNetOperateNotes(userid,NewOperateType, "质量管理", "产品检验新增", NewOperateNotes);
                    }
                }
                //else
                //{
                //    proDefect item = new proDefect();

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
                //    proDefectcode cclass = DB.proDefectcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    proDefectcode ccode = DB.proDefectcodes
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
                //    item.CreateUser = GetIdentityName();
                //    DB.proDefects.Add(item);
                //    DB.SaveChanges();




                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                //    string NewOperateType = item.Defectguid;
                //    string NewOperateNotes = "beNew* " + NewText + " *beNew 的记录已经将新增";
                //    NetCountHelper.NetLogRecord(userid, NewOperateType, "不具合管理", "不具合新增", NewOperateNotes);
                //}
            }
            else
            {
                if (newAddedList.Count != 0)
                {
                    for (int i = newAddedList.Count - 1; i >= 0; i--)
                    {

                        //遍历枚举类型Sample的各个枚举名称 


                        DataRow rowData = CreateNewData(CheckD, newAddedList[i]);

                        CheckD.Rows.InsertAt(rowData, 0);
                        //Defectb.Rows.InsertAt(rowData, 0);

                        //table.Rows.Add(rowData);
                        //DataRow rowData = CreateNewData(table, newAddedList[i]);

                        //Get the DataTable of a DataRow
                        DataTable tb = rowData.Table.NewRow().Table;


                        foreach (DataColumn col in tb.Columns)
                        {
                            //最后一条记录
                            string Laststr = tb.Rows[tb.Rows.Count - 1]["ID"].ToString();
                            //第一条记录
                            string Firststr = tb.Rows[0]["ID"].ToString();

                            dqmSamplinglevel = tb.Rows[0]["qmSamplinglevel"].ToString();
                            dqmCheckmethod = tb.Rows[0]["qmCheckmethod"].ToString();
                            //班组
                            dqmProqty = tb.Rows[0]["qmProqty"].ToString();
                            dqmSamplingQty = tb.Rows[0]["qmSamplingQty"].ToString();
                            dqmJudgment = tb.Rows[0]["qmJudgment"].ToString();
                            dqmAcceptqty = tb.Rows[0]["qmAcceptqty"].ToString();
                            dqmJudgmentlevel = tb.Rows[0]["qmJudgmentlevel"].ToString();
                            dqmRejectqty = tb.Rows[0]["qmRejectqty"].ToString();
                            //Probadtotal = tb.Rows[tb.Rows.Count - 1]["Probadtotal"].ToString();
                            dqmCheckNotes = tb.Rows[0]["qmCheckNotes"].ToString();
                            dqmSpecialNotes = tb.Rows[0]["qmSpecialNotes"].ToString();

                        }
                        Qm_Outgoing item = new Qm_Outgoing();
                        //检查员
                        item.qmInspector = this.qmInspector.Text;
                        //班别
                        item.qmLine = this.qmLine.Text;
                        //生产工单
                        item.qmOrder = this.qmOrder.Text;
                        //机种
                        item.qmModels = this.qmModels.Text;
                        //物料
                        item.qmMaterial = this.qmMaterial.Text;
                        //查检日期
                        item.qmCheckdate = this.qmCheckdate.Text;
                        //生产批号
                        item.qmProlot = this.qmProlot.Text;
                        //批号说明
                        item.qmLotserial = this.qmLotserial.Text;
                        //检验方式
                        item.qmCheckmethod = dqmCheckmethod;
                        //抽样数
                        item.qmSamplingQty = decimal.Parse(dqmSamplingQty);
                        //判定
                        item.qmJudgment = dqmJudgment;
                        //不良级别
                        item.qmJudgmentlevel = dqmJudgmentlevel;
                        //验退数
                        item.qmRejectqty = decimal.Parse(dqmRejectqty);
                        //判定说明
                        item.qmCheckNotes = dqmCheckNotes;
                        //验收数
                        item.qmAcceptqty = decimal.Parse(dqmAcceptqty);
                        //生产订单数量
                        item.qmOrderqty = decimal.Parse(qmOrderqty.Text);
                        //检验次数
                        item.qmCheckout = int.Parse(qmCheckout.Text);
                        //生产数
                        item.qmProqty = decimal.Parse(dqmProqty);
                        //检验水准
                        item.qmSamplinglevel = dqmSamplinglevel;
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

                        item.isDelete = 0;
                        item.qmSpecialNotes = dqmSpecialNotes;
                        item.GUID = Guid.NewGuid();

                        item.CreateTime = DateTime.Now;
                        item.Creator = GetIdentityName();
                        DB.Qm_Outgoings.Add(item);
                        DB.SaveChanges();



                        //新建日志
                        string NewText = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + ddlqmCheckmethod.Text + "," + numqmSamplingQty.Text + "," + ddlqmJudgment.Text + "," + ddlqmJudgmentlevel.Text + "," + numqmRejectqty.Text + "," + htmlqmCheckNotes.Text;
                        string NewOperateType = "新增";
                        string NewOperateNotes = "New产品检验* " + NewText + " *New产品检验 的记录已经新增";
                        OperateLogHelper.InsNetOperateNotes(userid,NewOperateType, "质量管理", "产品检验新增", NewOperateNotes);
                    }
                }
                //else
                //{
                //    proDefect item = new proDefect();

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
                //    proDefectcode cclass = DB.proDefectcodes
                //           .Where(u => u.cn_classmatter == "OK").FirstOrDefault();

                //    item.Prongclass = cclass.ngclass;

                //    item.Proclassmatter = "OK";

                //    //代码
                //    proDefectcode ccode = DB.proDefectcodes
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
                //    item.CreateUser = GetIdentityName();
                //    DB.proDefects.Add(item);
                //    DB.SaveChanges();




                //    //新建日志
                //    string NewText = item.Defectguid + "," + item.Prolinename + "," + item.Prolot + "," + item.Prodate + "," + item.Prorealqty + "," + item.Proclassmatter + "," + item.Prongmatter + "," + item.Probadqty + "," + item.Probadtotal + "," + item.Probadnote;
                //    string NewOperateType = item.Defectguid;
                //    string NewOperateNotes = "beNew * " + NewText + " *beNew 的记录已经将新增";
                //    NetCountHelper.NetLogRecord(userid, NewOperateType, "不具合管理", "不具合新增", NewOperateNotes);

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
            rowData["Id"] = GetNextRowID();
            //添加相关信息
            rowData["qmInspector"] = this.qmInspector.Text;
            rowData["qmLine"] = this.qmLine.Text;
            rowData["qmOrder"] = this.qmOrder.Text;
            rowData["qmModels"] = this.qmModels.Text;
            rowData["qmMaterial"] = this.qmMaterial.Text;
            rowData["qmCheckdate"] = this.qmCheckdate.Text;
            rowData["qmProlot"] = this.qmProlot.Text;
            rowData["qmLotserial"] = this.qmLotserial.Text;
            //rowData["qmCheckmethod"] = "";
            //rowData["qmSamplingQty"] = 0;
            //rowData["qmJudgment"] = "";
            //rowData["qmJudgmentlevel"] = "";
            //rowData["qmRejectqty"] =0;
            //rowData["qmCheckNotes"] = "";
            //rowData["qmAcceptqty"] = 0;
            rowData["qmOrderqty"] = this.qmOrderqty.Text;
            rowData["qmCheckout"] = this.qmCheckout.Text;
            //rowData["qmProqty"] = 0;
            //rowData["qmSamplinglevel"] = "";
            //rowData["Remark"] = "";
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

            rowData["GUID"] = Guid.NewGuid().ToString();

            rowData["isDelete"] = 0;
            rowData["CreateTime"] = DateTime.Now;
            rowData["CreateUser"] = GetIdentityName();
            //item.CreateTime = DateTime.Now;
            //item.CreateUser = GetIdentityName();
            //table.Rows.Add(rowData);
            InsertFqcDataRow(newAddedData, rowData);

            return rowData;
        }
        //修改内容提交
        private static void UpdateFqcDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {


            Qm_Outgoing item = DB.Qm_Outgoings

                .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改前日志
            string BeforeModi = item.ID + "," + item.qmInspector + "," + item.qmLine + "," + item.qmOrder + "," + item.qmModels + "," + item.qmMaterial + "," + item.qmCheckdate + "," + item.qmProlot + "," + item.qmLotserial + "," + item.qmCheckmethod + "," + item.qmSamplingQty + "," + item.qmJudgment + "," + item.qmJudgmentlevel + "," + item.qmRejectqty + "," + item.qmCheckNotes + "," + item.qmAcceptqty + "," + item.qmOrderqty + "," + item.qmCheckout + "," + item.qmProqty + "," + item.qmSamplinglevel;

            string OperateType = item.ID.ToString();
            string OperateNotes = "beEdit产品检验* " + BeforeModi + " *beEdit产品检验 的记录可能将被修改";
            OperateLogHelper.InsNetOperateNotes(userid, OperateType, "质量管理", "产品检验修改", OperateNotes);
            // 检验水准
            if (rowDict.ContainsKey("qmSamplinglevel"))
            {
                rowData["qmSamplinglevel"] = rowDict["qmSamplinglevel"];
                item.qmSamplinglevel = rowData["qmSamplinglevel"].ToString();
                //RealQty = rowData["Proclassmatter"].ToString();
                //
                //OrderFinish();
            }
            // 不良种类
            if (rowDict.ContainsKey("qmCheckmethod"))
            {
                rowData["qmCheckmethod"] = rowDict["qmCheckmethod"];
                item.qmCheckmethod = rowData["qmCheckmethod"].ToString();
                //RealQty = rowData["Proclassmatter"].ToString();
                //
                //OrderFinish();
            }
            // 不良代码
            if (rowDict.ContainsKey("qmProqty"))
            {
                rowData["qmProqty"] = rowDict["qmProqty"];
                item.qmProqty = decimal.Parse(rowData["qmProqty"].ToString());
                //StopCheck = rowData["Prongmatter"].ToString();
            }
            // 不良数量
            if (rowDict.ContainsKey("qmSamplingQty"))
            {

                rowData["qmSamplingQty"] = rowDict["qmSamplingQty"];
                item.qmSamplingQty = decimal.Parse(rowData["qmSamplingQty"].ToString());


                //StopMinute = rowData["Probadqty"].ToString();
            }
            // 不良总数
            if (rowDict.ContainsKey("qmJudgment"))
            {


                rowData["qmJudgment"] = rowDict["qmJudgment"];
                item.qmJudgment = rowData["qmJudgment"].ToString();
                //StopText = rowData["Probadtotal"].ToString();
            }
            // 改善对策
            if (rowDict.ContainsKey("qmAcceptqty"))
            {

                rowData["qmAcceptqty"] = rowDict["qmAcceptqty"];

                item.qmAcceptqty = decimal.Parse(rowData["qmAcceptqty"].ToString());

                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("qmJudgmentlevel"))
            {

                rowData["qmJudgmentlevel"] = rowDict["qmJudgmentlevel"];

                item.qmJudgmentlevel = rowData["qmJudgmentlevel"].ToString();

                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("qmRejectqty"))
            {

                rowData["qmRejectqty"] = rowDict["qmRejectqty"];

                item.qmRejectqty = decimal.Parse(rowData["qmRejectqty"].ToString());

                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("qmCheckNotes"))
            {

                rowData["qmCheckNotes"] = rowDict["qmCheckNotes"];

                item.qmCheckNotes = rowData["qmCheckNotes"].ToString();

                //ResonText = rowData["Probadnote"].ToString();
            }
            if (rowDict.ContainsKey("qmSpecialNotes"))
            {

                rowData["qmSpecialNotes"] = rowDict["qmSpecialNotes"];

                item.qmSpecialNotes = rowData["qmSpecialNotes"].ToString();

                //ResonText = rowData["Probadnote"].ToString();
            }
            item.ModifyTime = DateTime.Now;
            item.Modifier = userid;

            DB.SaveChanges();

            Qm_Outgoing edititem = DB.Qm_Outgoings

                     .Where(u => u.ID == editrowID).FirstOrDefault();

            //修改后日志
            string AfterModi = edititem.ID + "," + edititem.ID + "," + edititem.qmInspector + "," + edititem.qmLine + "," + edititem.qmOrder + "," + edititem.qmModels + "," + edititem.qmMaterial + "," + edititem.qmCheckdate + "," + edititem.qmProlot + "," + edititem.qmLotserial + "," + edititem.qmCheckmethod + "," + edititem.qmSamplingQty + "," + edititem.qmJudgment + "," + edititem.qmJudgmentlevel + "," + item.qmRejectqty + "," + edititem.qmCheckNotes + "," + edititem.qmAcceptqty + "," + edititem.qmOrderqty + "," + edititem.qmCheckout + "," + edititem.qmProqty + "," + edititem.qmSamplinglevel;
            string AfterOperateType = "修改";
            string AfterOperateNotes = "afEdit产品检验* " + AfterModi + " *afEdit产品检验 的记录已经将被修改";
            OperateLogHelper.InsNetOperateNotes(userid,AfterOperateType, "质量管理", "产品检验修改", AfterOperateNotes);
        }
        //获取新增行内容
        private void InsertFqcDataRow(Dictionary<string, object> rowDict, DataRow rowData)
        {
            // 区分
            BindFqcDataRow("qmSamplinglevel", rowDict, rowData);
            // 区分
            BindFqcDataRow("qmCheckmethod", rowDict, rowData);
            // 区分
            BindFqcDataRow("qmProqty", rowDict, rowData);
            // 区分
            BindFqcDataRow("qmSamplingQty", rowDict, rowData);

            // 种类
            BindFqcDataRow("qmJudgment", rowDict, rowData);

            // 类别
            BindFqcDataRow("qmAcceptqty", rowDict, rowData);

            // 数量
            BindFqcDataRow("qmJudgmentlevel", rowDict, rowData);

            // 总数
            BindFqcDataRow("qmRejectqty", rowDict, rowData);

            // 对策
            BindFqcDataRow("qmCheckNotes", rowDict, rowData);
            // 对策
            BindFqcDataRow("qmSpecialNotes", rowDict, rowData);

        }
        //根据字段获取信息
        private void BindFqcDataRow(string columnName, Dictionary<string, object> rowDict, DataRow rowData)
        {
            if (rowDict.ContainsKey(columnName))
            {
                rowData[columnName] = rowDict[columnName];
            }
        }
        // 根据行ID来获取行数据
        private DataRow FindRowByID(int rowID)
        {

            //mysql = "select* from[dbo].[proDefects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            foreach (DataRow row in CheckD.Rows)
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
            //mysql = "select* from[dbo].[proDefects];";
            //DataTable table = GetDataTable.Getdt(mysql);

            DataRow found = FindRowByID(rowID);
            if (found != null)
            {
                CheckD.Rows.Remove(found);
            }
        }

        // 模拟数据库的自增长列
        private int GetNextRowID()
        {
            int maxID = 0;
            //mysql = "select * from [dbo].[proDefects];";
            //DataTable table = GetDataTable.Getdt(mysql);
            foreach (DataRow row in CheckD.Rows)
            {
                int currentRowID = Convert.ToInt32(row["Id"]);
                if (currentRowID > maxID)
                {
                    maxID = currentRowID;
                }
            }
            return maxID + 1;
        }



        //合计表格
        private void OutputSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["qmProqty"]);
                rTotal += Convert.ToDecimal(row["qmAcceptqty"]);
                ratio += Convert.ToDecimal(row["qmRejectqty"]);
            }


            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("qmProqty", pTotal.ToString("F0"));
            summary.Add("qmAcceptqty", rTotal.ToString("F0"));
            summary.Add("qmRejectqty", ratio.ToString("F0"));

            Grid1.SummaryData = summary;

        }
        #endregion
        #region DDLBindData

        private void BindDDLcLevel()
        {
            //查询LINQ去重复
            var q = from a in DB.Qm_CheckAQLs
                        //join b in DB.Ec_Subs on a.Porderhbn equals b.Ec_bomitem
                        //where b.Ec_no == strecn
                        //where a.lineclass == "M"
                    select new
                    {
                        a.cLevel,

                    };

            var qs = q.Select(E => new { E.cLevel }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlqmSamplinglevel.DataSource = qs;
            ddlqmSamplinglevel.DataTextField = "cLevel";
            ddlqmSamplinglevel.DataValueField = "cLevel";
            ddlqmSamplinglevel.DataBind();


        }

        private void BindDDLStdClass()
        {
            IQueryable<Qm_CheckType> q = DB.Qm_CheckTypes;
            q = q.Where(u => u.Checktype.Contains("B"));

            // 绑定到下拉列表（启用模拟树功能）

            ddlqmJudgment.DataTextField = "Checkcntext";
            ddlqmJudgment.DataValueField = "Checkcntext";
            ddlqmJudgment.DataSource = q;
            ddlqmJudgment.DataBind();


        }

        private void BindDDLLevel()
        {
            //查询LINQ去重复
            var q = from a in DB.Qm_CheckTypes
                    where a.Checktype.Contains("C")

                    //join b in DB.Ec_Subs on a.Porderhbn equals b.Ec_bomitem
                    //where b.Ec_no == strecn
                    //where a.lineclass == "M"
                    select new
                    {
                        a.Checktype,
                        a.Checkcntext

                    };

            var qs = q.Select(E => new { E.Checktype, E.Checkcntext }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            ddlqmJudgmentlevel.DataSource = qs;
            ddlqmJudgmentlevel.DataTextField = "Checkcntext";
            ddlqmJudgmentlevel.DataValueField = "Checkcntext";
            ddlqmJudgmentlevel.DataBind();


        }

        private void BindDDLInsClass()
        {
            IQueryable<Qm_CheckType> q = DB.Qm_CheckTypes;
            q = q.Where(u => u.Checktype.Contains("A"));

            // 绑定到下拉列表（启用模拟树功能）

            ddlqmCheckmethod.DataTextField = "Checkcntext";
            ddlqmCheckmethod.DataValueField = "Checkcntext";
            ddlqmCheckmethod.DataSource = q;
            ddlqmCheckmethod.DataBind();




        }



        #endregion



    }
}
