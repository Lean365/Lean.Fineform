using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.QM.fqc
{
    public partial class fqc_action_new : PageBase
    {
        //日志配置文件调用
        //private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string issueNo;

        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFqcActionNew";
            }
        }

        #endregion ViewPower

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
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            BindData();
        }

        //发行NO
        public void BindissueNoData()
        {
            int id = GetQueryIntValue("ID");
            Qm_Outgoing current = DB.Qm_Outgoings.Find(id);
            string sdate = current.qmCheckdate;

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
                issueNo = current.qmCheckdate + "001";
            }
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("ID");
            //Qm_Outgoing current = DB.Qm_Outgoings.Find(id);

            var q = (from a in DB.Qm_Outgoings
                     where a.ID == id
                     join b in DB.Pp_Manhours on a.qmMaterial equals b.Proitem

                     select new
                     {
                         a.qmInspector,
                         a.qmLine,
                         a.qmOrder,
                         a.qmModels,
                         a.qmMaterial,
                         a.qmCheckdate,
                         a.qmProlot,
                         a.qmLotserial,
                         a.qmSamplinglevel,
                         a.qmCheckmethod,
                         a.qmSamplingQty,
                         a.qmJudgment,
                         a.qmJudgmentlevel,
                         a.qmRejectqty,
                         a.qmCheckNotes,
                         a.qmSpecialNotes,
                         a.qmAcceptqty,
                         a.qmOrderqty,
                         a.qmCheckout,
                         a.qmProqty,
                         qmRegion = b.Prodesc,
                         qmPersonnel = "",
                         qmDirectreason = "",
                         qmIndirectreason = "",
                         qmDisposal = "",
                         qmDirectsolutions = "",
                         qmDate = "",
                         qmIndirectsolutions = "",
                         qmVerify = "",
                         qmCarryoutdate = "",
                         qmCarryoutverify = false,
                         Remark = "",
                     }).ToList();

            //Qm_Improvement current = DB.Qm_Improvements.Find(id);

            //if (current == null)
            //{
            //    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
            //    Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
            //    return;
            //}

            if (q.Any())
            {
                qmInspector.Text = q[0].qmInspector;
                //item.Prolineclass = prolinename.SelectedValue.ToString();
                qmLine.Text = q[0].qmLine;
                qmOrder.Text = q[0].qmOrder;
                qmModels.Text = q[0].qmModels;
                qmMaterial.Text = q[0].qmMaterial;
                qmRegion.Text = q[0].qmRegion;
                qmCheckdate.Text = q[0].qmCheckdate;
                qmProlot.Text = q[0].qmProlot;
                qmLotserial.Text = q[0].qmLotserial;
                qmRejectqty.Text = q[0].qmRejectqty.ToString();
                qmJudgmentlevel.Text = q[0].qmJudgmentlevel;
                qmCheckNotes.Text = q[0].qmCheckNotes.ToString();
                if (q[0].qmPersonnel.ToString() == "Wait for Reply")
                {
                    qmPersonnel.Text = "";
                    qmDate.SelectedDate = DateTime.ParseExact(q[0].qmDate.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    qmPersonnel.Text = q[0].qmPersonnel.ToString();
                    qmDate.Text = DateTime.Now.ToString("yyyyMMdd"); ;
                }
                if (q[0].qmDirectreason.ToString() == "Wait for Reply")
                {
                    qmDirectreason.Text = "";
                }
                else
                {
                    qmDirectreason.Text = q[0].qmDirectreason.ToString();
                }

                if (q[0].qmIndirectreason.ToString() == "Wait for Reply")
                {
                    qmIndirectreason.Text = "";
                }
                else
                {
                    qmIndirectreason.Text = q[0].qmIndirectreason.ToString();
                }

                if (q[0].qmDisposal.ToString() == "Wait for Reply")
                {
                    qmDisposal.Text = "";
                }
                else
                {
                    qmDisposal.Text = q[0].qmDisposal.ToString();
                }

                if (q[0].qmDirectsolutions.ToString() == "Wait for Reply")

                {
                    qmDirectsolutions.Text = "";
                }
                else
                {
                    qmDirectsolutions.Text = q[0].qmDirectsolutions.ToString();
                }

                if (q[0].qmDirectreason.ToString() == "Wait for Reply")
                {
                    qmIndirectsolutions.Text = "";
                }
                else
                {
                    qmIndirectsolutions.Text = q[0].qmIndirectsolutions.ToString();
                }

                if (q[0].qmVerify.ToString() == "Wait for Reply")
                {
                    qmVerify.Text = "";
                    qmCarryoutdate.SelectedDate = DateTime.ParseExact(q[0].qmCarryoutdate.ToString(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    qmVerify.Text = q[0].qmPersonnel.ToString();
                    qmCarryoutdate.Text = DateTime.Now.ToString("yyyyMMdd"); ;
                }

                if (q[0].qmCarryoutverify == true)
                {
                    qmCarryoutverify.Checked = true;
                }
                else
                {
                    qmCarryoutverify.Checked = false;
                }
                if (q[0].qmDirectreason.ToString() == "Wait for Reply")
                {
                    qmIndirectsolutions.Text = "";
                }
                else
                {
                    qmCarryoutverify.Text = q[0].qmCarryoutverify.ToString();
                }

                if (q[0].qmDirectreason.ToString() == "Wait for Reply")
                {
                    qmIndirectsolutions.Text = "";
                }
                else
                {
                    qmCarryoutverify.Text = q[0].qmCarryoutverify.ToString();
                }

                if (q[0].qmDirectreason.ToString() == "Wait for Reply")
                {
                    qmIndirectsolutions.Text = "";
                }
                else
                {
                    qmIndirectsolutions.Text = q[0].qmIndirectsolutions.ToString();
                }
                qmCarryoutverify.Text = q[0].qmCarryoutverify.ToString();
                Remark.Text = q[0].Remark;

                //修改前日志
                string BeforeModi = q[0].qmInspector + "," + q[0].qmLine + "," + q[0].qmOrder + "," + q[0].qmModels + "," + q[0].qmMaterial + "," + q[0].qmRegion + "," + q[0].qmCheckdate + "," + q[0].qmProlot + "," + q[0].qmLotserial + "," + q[0].qmRejectqty + "," + q[0].qmJudgmentlevel + "," + q[0].qmCheckNotes + "," + q[0].qmPersonnel + "," + q[0].qmDate;
                string OperateType = "新增";
                string OperateNotes = "beNew* " + BeforeModi + " *beNew 的记录可能将被新增";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良对策修改", OperateNotes);
            }

            // 添加所有用户

            //Editor1.setContent("")
            // 初始化用户所属角色
            //InitUserRole(current);

            // 初始化用户所属部门
            //InitNoticeDept(current);

            // 初始化用户所属职称
            //InitUserTitle(current);
        }

        private void SaveAction()
        {
            BindissueNoData();
            int id = GetQueryIntValue("ID");
            Qm_Outgoing current = DB.Qm_Outgoings.Find(id);

            string prolot = current.qmProlot;
            string proorder = current.qmOrder;
            string prodate = current.qmCheckdate;
            string proline = current.qmLine;

            var reult = (from a in DB.Qm_Improvements
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDeleted == 0
                         select a).ToList();
            if (!reult.Any())
            {
                var q = (from a in DB.Qm_Outgoings
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDeleted == 0
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
                                                               qmRegion = qmRegion.Text,//仕向
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
                                                               CreateDate = DateTime.Now,
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
                         where a.isDeleted == 0
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

                             b.isDeleted,
                             b.Remark,
                             b.Creator,
                             b.CreateDate,
                             b.Modifier,
                             b.ModifyDate,
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

                                                                  isDeleted = a.isDeleted,
                                                                  Remark = a.Remark,
                                                                  Creator = a.Creator,
                                                                  CreateDate = a.CreateDate,
                                                                  Modifier = GetIdentityName(),
                                                                  ModifyDate = DateTime.Now,
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
            //item.CreateDate = DateTime.Now;

            //DB.Qm_Improvements.Add(item);
            //DB.SaveChanges();
            //新增日志
            string Newtext = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + qmJudgmentlevel.Text + "," + qmRejectqty.Text + "," + qmCheckNotes.Text;
            string OperateType = "新增";
            string OperateNotes = "New分析对策* " + Newtext + "*分析对策 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "分析对策新增", OperateNotes);
        }

        private void SaveNotice()
        {
            BindissueNoData();
            int id = GetQueryIntValue("ID");
            Qm_Outgoing current = DB.Qm_Outgoings.Find(id);

            string prolot = current.qmProlot;
            string proorder = current.qmOrder;
            string prodate = current.qmCheckdate;
            string proline = current.qmLine;

            var reult = (from a in DB.Qm_Unqualifieds
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDeleted == 0
                         select a).ToList();
            if (!reult.Any())
            {
                var q = (from a in DB.Qm_Outgoings
                         where a.qmProlot == prolot
                         where a.qmOrder == proorder
                         where a.qmCheckdate == prodate
                         where a.qmLine == proline
                         where a.qmRejectqty > 0
                         where a.isDeleted == 0
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
                                                                   qmRegion = qmRegion.Text,//仕向
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
                                                                   CreateDate = DateTime.Now,
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
                         where a.isDeleted == 0
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

                             b.isDeleted,
                             b.Remark,
                             b.Creator,
                             b.CreateDate,
                             b.Modifier,
                             b.ModifyDate,
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

                                                                      isDeleted = a.isDeleted,
                                                                      Remark = a.Remark,
                                                                      Creator = a.Creator,
                                                                      CreateDate = a.CreateDate,
                                                                      Modifier = GetIdentityName(),
                                                                      ModifyDate = DateTime.Now,
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
            //item.CreateDate = DateTime.Now;

            //DB.Qm_Unqualifieds.Add(item);
            //DB.SaveChanges();

            //新增日志
            string Newtext = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + qmJudgmentlevel.Text + "," + qmRejectqty.Text + "," + qmCheckNotes.Text;
            string OperateType = "新增";
            string OperateNotes = "New不合格通知*" + Newtext + "*New不合格通知 的记录已新增";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不合格通知新增", OperateNotes);
        }

        #endregion Page_Load

        //判断修改内容||判断重复
        private void CheckData()
        {
            //Guid id = Guid.Parse(GetQueryValue("GUID"));
            //Qm_Improvement current = DB.Qm_Improvements.Find(id);
            //string modi001 = current.qmPersonnel;
            //string modi002 = current.qmDate;
            //string modi003 = current.qmDirectreason;
            //string modi004 = current.qmDisposal;
            //string modi005 = current.qmCarryoutdate;

            //if (this.qmPersonnel.Text == modi001)
            //{
            //    if (this.qmDate.Text == modi002)
            //    {
            //        if (this.qmDirectreason.Text == modi003)
            //        {
            //            if (this.qmDisposal.Text == modi004)
            //            {
            //                if (this.qmCarryoutdate.Text == modi005)
            //                {
            //                    Alert.ShowInTop("global::Resources.GlobalResource.sys_Msg_Noedit！", "修改提示", MessageBoxIcon.Warning);
            //                    //Alert alert = new Alert();
            //                    //alert.Message = global::Resources.GlobalResource.sys_Msg_Noedit;
            //                    //alert.IconUrl = "~/Lf_Resources/images/success.png";
            //                    //alert.Target = Target.Top;
            //                    //Alert.ShowInTop();
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
            //    alert.IconUrl = "~/Lf_Resources/images/success.png";
            //    alert.Target = Target.Top;
            //    Alert.ShowInTop();
            //    //PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            //}

            //string InputData = Qcpd003.Text.Trim();

            //proMovingpricedata redata = DB.proMovingpricedatas.Where(u => u.Qcpd003 == InputData).FirstOrDefault();

            //if (redata != null)
            //{
            //    Alert.ShowInTop("基本信息,物料< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
            //string InputData = qmCheckNotes.Text.Trim();

            //Qm_Improvement Redata = DB.Qm_Improvements.Where(u => u.qmCheckNotes == InputData).FirstOrDefault();

            //if (Redata != null)
            //{
            //    Alert.ShowInTop("分析对策数据,判定说明说明方式< " + InputData + ">已经存在！修改即可");
            //    return;
            //}
            SaveAction();
            SaveNotice();
        }

        #region Events

        private void SaveItem()//新增质量控制数据
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Qm_Improvement item = DB.Qm_Improvements

                .Where(u => u.GUID == id).FirstOrDefault();

            item.qmInspector = qmInspector.Text;
            item.qmLine = qmLine.Text;
            item.qmOrder = qmOrder.Text;
            item.qmModels = qmModels.Text;
            item.qmMaterial = qmMaterial.Text;
            item.qmRegion = qmRegion.Text;
            item.qmCheckdate = qmCheckdate.Text;
            item.qmProlot = qmProlot.Text;
            item.qmLotserial = qmLotserial.Text;
            item.qmRejectqty = decimal.Parse(qmRejectqty.Text);
            item.qmJudgmentlevel = qmJudgmentlevel.Text;
            item.qmCheckNotes = qmCheckNotes.Text;
            item.qmPersonnel = qmPersonnel.Text;
            item.qmDate = qmDate.SelectedDate.Value.ToString("yyyyMMdd");
            item.qmDirectreason = qmDirectreason.Text;
            item.qmIndirectreason = qmIndirectreason.Text;
            item.qmDisposal = qmDisposal.Text;
            item.qmDirectsolutions = qmDirectsolutions.Text;
            item.qmIndirectsolutions = qmIndirectsolutions.Text;
            item.qmVerify = qmVerify.Text;

            item.qmCarryoutdate = qmCarryoutdate.SelectedDate.Value.ToString("yyyyMMdd");

            if (this.qmCarryoutverify.Checked == true)
            {
                item.qmCarryoutverify = true;
            }
            else
            {
                item.qmCarryoutverify = false;
            }
            item.qmSolutionsverify = qmSolutionsverify.Text;
            item.qmNotes = qmNotes.Text;

            item.Remark = Remark.Text;
            item.ModifyDate = DateTime.Now;
            item.Modifier = GetIdentityName();
            //DB.Prolines.Add(item);
            DB.SaveChanges();

            //修改后日志
            string ModifiedText = qmInspector.Text + "," + qmLine.Text + "," + qmOrder.Text + "," + qmModels.Text + "," + qmMaterial.Text + "," + qmRegion.Text + "," + qmCheckdate.Text + "," + qmProlot.Text + "," + qmLotserial.Text + "," + qmRejectqty.Text + "," + qmJudgmentlevel.Text + "," + qmCheckNotes.Text + "," + qmPersonnel.Text + "," + qmDate.Text;
            string OperateType = "修改";
            string OperateNotes = "afEdit* " + ModifiedText + "*afEdit 的记录已修改";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "不良对策修改", OperateNotes);
        }

        public decimal qaQTY, mcQTY;

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            ////工单未入库数量
            //string MA002str;

            //MA002str = "SELECT MC009 FROM [dbo].[proOrders]  WHERE MC003='" + this.qmLine.Text + "'";
            //SqlDataAdapter MA002DA = new SqlDataAdapter(MA002str, AppConn);
            //DataSet MA002DS = new DataSet();
            //MA002DA.Fill(MA002DS);

            //mcQTY = decimal.Parse(MA002DS.Tables[0].Rows[0][0].ToString());
            ////QA检验入库数量
            //string MA003str;
            //MA003str = "SELECT SUM(qmDate) AS qaQTY FROM PlutoOA.dbo.Invlas  WHERE qmLine='" + this.qmLine.Text + "'";
            //SqlDataAdapter MA003DA = new SqlDataAdapter(MA003str, AppConn);
            //DataSet MA003DS = new DataSet();
            //MA003DA.Fill(MA003DS);

            //if (MA003DS.Tables[0].Rows[0][0].ToString().Length != 0)
            //{
            //    qaQTY = decimal.Parse(MA003DS.Tables[0].Rows[0][0].ToString());
            //}
            //else
            //{
            //    qaQTY = 0;
            //}

            //if (qaQTY > mcQTY)
            //{
            //    //登录日志写入
            //    logger.Error(String.Format("用户 - {0} - 验收数量大于工单数量", GetIdentityName() + this.qmLine.Text + qaQTY + qmDirectreason.Text));
            //    Alert.ShowInTop("工单 " + this.qmLine.Text + " 已经超检验入库！");
            //    return;
            //}
            CheckData();
            //SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events

        #region DDLBindData

        protected void qmDirectreason_TextChanged(object sender, EventArgs e)
        {
            qmDirectreason.Text = StringHelper.FilterSpecial(qmDirectreason.Text.ToString().Trim());//文本框输入的值

            qmDirectreason.Text = StringHelper.ToDBC(qmDirectreason.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDirectreason.Text.ToString().Trim());//大小写转换
            //qmIndirectreason.Text = StringHelper.CNYcurD(qmDirectreason.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmIndirectreason_TextChanged(object sender, EventArgs e)
        {
            qmIndirectreason.Text = StringHelper.FilterSpecial(qmIndirectreason.Text.ToString().Trim());//文本框输入的值
            qmIndirectreason.Text = StringHelper.ToDBC(qmIndirectreason.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmIndirectreason.Text.ToString().Trim());//大小写转换
            //qmIndirectreason.Text = StringHelper.CNYcurD(qmIndirectreason.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmDisposal_TextChanged(object sender, EventArgs e)
        {
            qmDisposal.Text = StringHelper.FilterSpecial(qmDisposal.Text.ToString().Trim());//文本框输入的值
            qmDisposal.Text = StringHelper.ToDBC(qmDisposal.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDisposal.Text.ToString().Trim());//大小写转换
            //qmDisposal.Text = StringHelper.CNYcurD(qmDisposal.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmDirectsolutions_TextChanged(object sender, EventArgs e)
        {
            qmDirectsolutions.Text = StringHelper.FilterSpecial(qmDirectsolutions.Text.ToString().Trim());//文本框输入的值
            qmDirectsolutions.Text = StringHelper.ToDBC(qmDirectsolutions.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmDirectsolutions.Text.ToString().Trim());//大小写转换
            //qmDirectsolutions.Text = StringHelper.CNYcurD(qmDirectsolutions.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmIndirectsolutions_TextChanged(object sender, EventArgs e)
        {
            qmIndirectsolutions.Text = StringHelper.FilterSpecial(qmIndirectsolutions.Text.ToString().Trim());//文本框输入的值
            qmIndirectsolutions.Text = StringHelper.ToDBC(qmIndirectsolutions.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmIndirectsolutions.Text.ToString().Trim());//大小写转换
            //qmIndirectsolutions.Text = StringHelper.CNYcurD(qmIndirectsolutions.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmSolutionsverify_TextChanged(object sender, EventArgs e)
        {
            qmSolutionsverify.Text = StringHelper.FilterSpecial(qmSolutionsverify.Text.ToString().Trim());//文本框输入的值
            qmSolutionsverify.Text = StringHelper.ToDBC(qmSolutionsverify.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmSolutionsverify.Text.ToString().Trim());//大小写转换
            //qmSolutionsverify.Text = StringHelper.CNYcurD(qmSolutionsverify.Text.ToString().Trim());//会计金额大写转换
        }

        protected void qmNotes_TextChanged(object sender, EventArgs e)
        {
            qmNotes.Text = StringHelper.FilterSpecial(qmNotes.Text.ToString().Trim());//文本框输入的值
            qmNotes.Text = StringHelper.ToDBC(qmNotes.Text.ToString().Trim());//全角半角转换
            StringHelper.strToUpper(qmNotes.Text.ToString().Trim());//大小写转换
            //qmNotes.Text = StringHelper.CNYcurD(qmNotes.Text.ToString().Trim());//会计金额大写转换
        }

        #endregion DDLBindData
    }
}