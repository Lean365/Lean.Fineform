using FineUIPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
namespace LeanFine.Lf_Business.Helper
{
    public class UpdatingP2dHelper : PageBase
    {
        /// <summary>
        /// 制一课更新不良表（pp_defect）中实际生产数量，条件按日期，订单，班组
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="strPline"></param>
        public static void P2d_Defect_Realqty_Update(string strPorder, string strPdate, string strPline, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P2d_OutputSubs
                     where p.IsDeleted == 0
                     where p.Proorder == strPorder
                     where p.Prodate == strPdate
                     where p.Prolinename == strPline
                     group p by p.Prolot into g
                     select new
                     {
                         TotalQty = g.Sum(p => p.Prorealqty)
                     }).ToList();
            if (q.Any())
            {
                realQty = q[0].TotalQty;
            }

            DB.Pp_P2d_Defects
              .Where(s => s.Proorder == strPorder)
              .Where(s => s.Prodate == strPdate)
              .Where(s => s.Prolinename == strPline)
              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }
        /// <summary>
        /// 制二课更新订单不良集计表（Pp_Defect_P2d_Orders），条件按订单,生产实绩（Prorealqty）
        /// </summary>
        /// <param name="strPorder"></param>
        public static void Pp_Defect_P2d_Orders_Realqty_Update(string strPorder, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P2d_OutputSubs
                     where p.IsDeleted == 0
                     where p.Proorder == strPorder
                     //where p.Remark.Contains(strLineType)
                     group p by p.Prolot into g
                     select new
                     {
                         TotalQty = g.Sum(p => p.Prorealqty)
                     }).ToList();
            if (q.Any())
            {
                realQty = q[0].TotalQty;
            }

            DB.Pp_Defect_P2d_Orders
              .Where(s => s.Proorder == strPorder)
              //.Where(s => s.Prodept == (strProdept))
              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }
        /// <summary>
        /// 制二课更新无不良台数，条件生产订单，（Prodzeroefects）
        /// </summary>
        /// <param name="strPorder"></param>
        public static void Pp_Defect_P2d_Orders_NoBadqty_Update(string strPorder, string uid)
        {
            int noQty = 0;
            int okQty = 0;
            var noqs =
                from p in DB.Pp_P2d_Defects
                where p.IsDeleted == 0
                where p.Proorder == strPorder
                group p by new
                {
                    p.Prolinename,
                    p.Prodate,
                    p.Proorder,
                    p.Prodzeroefects,
                }
                     into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Prolinename,
                    g.Key.Proorder,
                    g.Key.Prodzeroefects,
                };

            //统计无不良台数（有不良录入时）
            var noqty = (from p in noqs
                         group p by new
                         {
                             p.Proorder,
                         }
                        into g
                         select new
                         {
                             TotalQty = g.Sum(p => p.Prodzeroefects)
                         }).ToList();

            if (noqty.Any())
            {
                noQty = noqty[0].TotalQty;
            }
            //统计无不良台数（无不良录入时）
            var ids = new HashSet<string>(DB.Pp_P2d_Defects.Select(x => x.Prodate + x.Proorder + x.Prolinename));
            var results = DB.Pp_P2d_OutputSubs.Where(x => !ids.Contains(x.Prodate + x.Proorder + x.Prolinename) && x.Proorder == strPorder && x.IsDeleted == 0);

            var okqty = (from a in results
                         group a by a.Proorder
                                into g

                         select new
                         {
                             TotalQty = g.Sum(p => p.Prorealqty)
                         }).ToList();
            if (okqty.Any())
            {
                okQty = okqty[0].TotalQty;
            }

            DB.Pp_Defect_P2d_Orders
                  .Where(s => s.Proorder == strPorder)
                  //.Where(s => s.Prodept == (strProdept))

                  .ToList()
                  .ForEach(x => { x.Prodzeroefects = noQty + okQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }
        /// <summary>
        /// 制二课更新订单已生产数量
        /// </summary>
        /// <param name="strGuid"></param>
        public static void P2UpdateOrderRealQty(string OrderNo, string uid)//(Guid strGuid)
        {
            try
            {
                var q_count = (from a in DB.Pp_P2d_OutputSubs
                               where a.IsDeleted == 0
                               where a.Proorder.Contains(OrderNo)
                               group a by new
                               {
                                   a.Proorder
                               }
                          into g
                               select new
                               {
                                   g.Key.Proorder,
                                   RealQty = g.Sum(p => p.Prorealqty),
                               }).ToList();
                if (q_count.Any())
                {
                    if (q_count[0].RealQty != 0)
                    {
                        var q2 = DB.Pp_Orders.First(c => c.Porderno == OrderNo);
                        q2.Porderreal = q_count[0].RealQty;
                        q2.Modifier = uid;
                        q2.ModifyDate = DateTime.Now;
                        if (String.IsNullOrEmpty(q2.Porderroute))
                        {
                            q2.Porderroute = DateTime.Now.Year.ToString("YYYYMMDD");
                        }
                        if (String.IsNullOrEmpty(q2.Porderserial))
                        {
                            string Y = DateTime.Now.Year.ToString(); //获取年份  // 2008
                            string M = DateTime.Now.Month.ToString(); //获取月份   // 9
                            if (M.CompareTo("10") == 0)
                            {
                                M = "X";
                            }
                            if (M.CompareTo("11") == 0)
                            {
                                M = "Y";
                            }
                            if (M.CompareTo("12") == 0)
                            {
                                M = "Z";
                            }
                            string sn = "00008448763" + Y + M + "0001~00008448763" + Y + M + q_count[0].RealQty.ToString().PadLeft(4, '0');
                            q2.Porderserial = sn;
                        }
                    }
                    else
                    {
                        var q2 = DB.Pp_Orders.First(c => c.Porderno == OrderNo);
                        q2.Porderreal = q_count[0].RealQty;
                        q2.Modifier = uid;
                        q2.ModifyDate = DateTime.Now;
                        if (String.IsNullOrEmpty(q2.Porderroute))
                        {
                            q2.Porderroute = DateTime.Now.Year.ToString("YYYYMMDD");
                        }
                        if (String.IsNullOrEmpty(q2.Porderserial))
                        {
                            string Y = DateTime.Now.Year.ToString(); //获取年份  // 2008
                            string M = DateTime.Now.Month.ToString(); //获取月份   // 9
                            if (M.CompareTo("10") == 0)
                            {
                                M = "X";
                            }
                            if (M.CompareTo("11") == 0)
                            {
                                M = "Y";
                            }
                            if (M.CompareTo("12") == 0)
                            {
                                M = "Z";
                            }
                            string sn = "00008448763" + Y + M + "0001~00008448763" + Y + M + q_count[0].RealQty.ToString().PadLeft(4, '0');
                            q2.Porderserial = sn;
                        }
                    }
                    DB.SaveChanges();
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
            //}
        }
        /// <summary>
        /// 更新损耗时间
        /// </summary>
        /// <param name="pline"></param>
        /// <param name="uid"></param>
        public static void P2UpdateLossTime(string pline, int id)
        {
            try
            {
                if (pline == "修正")
                {
                    DB.Pp_P2d_OutputSubs
                        .Where(s => s.Prolinename == pline)
                       .Where(s => s.ID == id)
                        .ToList()
                        .ForEach(x => { x.UDF52 = x.Prohandofftime * 21; x.Remark = "修正仕损工数"; });
                    DB.SaveChanges();
                }
                if (pline == "手插")
                {
                    DB.Pp_P2d_OutputSubs
                        .Where(s => s.Prolinename == pline)
                       .Where(s => s.ID == id)
                        .ToList()
                        .ForEach(x => { x.UDF53 = x.Prohandofftime * 16; x.Remark = "手插仕损工数"; });
                    DB.SaveChanges();
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
        /// 更新P2D工单的已生产总数
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="Prolinename"></param>
        /// <param name="uid"></param>
        /// <param name="Propcbatype"></param>
        /// <param name="Propcbaside"></param>
        public static void UpdateP2DRealTotal(string OrderNo, string ProDate, string uid)
        {
            try
            {
                //求和
                var q =
                    (from p in DB.Pp_P2d_OutputSubs
                     .Where(s => s.IsDeleted == 0)
                    .Where(s => s.Prorealqty != 0)
                    //.Where(s => s.Prodate == ProDate)
                    .Where(s => s.Proorder == OrderNo)
                         //.Where(s => s.Propcbatype == Propcbatype)
                         //.Where(s => s.Propcbaside == Propcbaside)
                         //.Where(s => s.Prolinename == Prolinename)
                     group p by new
                     {
                         p.Prolinename,
                         p.Proorder,
                         p.Propcbatype,
                         p.Propcbaside,
                     }
                        into g
                     select new
                     {
                         g.Key.Proorder,
                         g.Key.Prolinename,
                         g.Key.Propcbatype,
                         g.Key.Propcbaside,

                         Prorealtotal = g.Sum(p => p.Prorealqty),
                     }).ToList();

                //判断查询是否为空

                if (q.Any())
                {
                    //var qs=q.Where(s => s.Prodate == ProDate).ToList();
                    //for遍历
                    for (int i = 0; i < q.Count; i++)
                    {
                        int cc = q[i].Prorealtotal;
                        string Prolinename = q[i].Prolinename;
                        string Propcbatype = q[i].Propcbatype;
                        string Propcbaside = q[i].Propcbaside;

                        DB.Pp_P2d_OutputSubs
                             .Where(s => s.Prodate.CompareTo(ProDate) >= 0 && s.Proorder == OrderNo && s.Prolinename == Prolinename && s.Propcbatype == Propcbatype && s.Propcbaside == Propcbaside)

                           .ToList()
                           .ForEach(x => { x.Prorealtotal = cc; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                        DB.SaveChanges();
                    }
                }
                //求和
                //var qs =
                //    (from p in DB.Pp_P1d_Defects
                //     .Where(s => s.IsDeleted == 0)
                //    .Where(s => s.Prorealqty != 0)
                //    .Where(s => s.Proorder == porder)

                //     group p by new
                //     {
                //         p.Proorder,
                //     }
                //        into g
                //     select new
                //     {
                //         Proorder = g.Key.Proorder,

                //         Probadamount = g.Sum(p => p.Probadqty),
                //     }).ToList();
                //if (qs.Any())
                //{
                //    int ccs = qs[0].Probadamount;

                //    DB.Pp_P1d_Defects
                //         .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //    DB.Pp_Defect_P2d_Orders
                //       .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //}
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
        /// 更新P2D工单的已生产总数
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="Prolinename"></param>
        /// <param name="uid"></param>
        /// <param name="Propcbatype"></param>
        /// <param name="Propcbaside"></param>
        public static void UpdateP2DRealTotals()
        {
            try
            {
                //求和
                var q =
                    (from p in DB.Pp_P2d_OutputSubs
                     .Where(s => s.IsDeleted == 0)
                    .Where(s => s.Prorealqty != 0)
                         //.Where(s => s.Prodate == pdate)
                         //.Where(s => s.Proorder == OrderNo)
                         //.Where(s => s.Propcbatype == Propcbatype)
                         //.Where(s => s.Propcbaside == Propcbaside)
                         //.Where(s => s.Prolinename == Prolinename)
                     group p by new
                     {
                         p.Prolinename,
                         p.Proorder,
                         p.Propcbatype,
                         p.Propcbaside,
                     }
                        into g
                     select new
                     {
                         g.Key.Proorder,
                         g.Key.Prolinename,
                         g.Key.Propcbatype,
                         g.Key.Propcbaside,

                         Prorealtotal = g.Sum(p => p.Prorealqty),
                     }).ToList();

                //判断查询是否为空

                if (q.Any())
                {
                    //for遍历
                    for (int i = 0; i < q.Count; i++)
                    {
                        int cc = q[i].Prorealtotal;
                        string Proorder = q[i].Proorder;
                        string Prolinename = q[i].Prolinename;
                        string Propcbatype = q[i].Propcbatype;
                        string Propcbaside = q[i].Propcbaside;

                        DB.Pp_P2d_OutputSubs
                           .Where(s => s.Proorder == Proorder && s.Prolinename == Prolinename && s.Propcbatype == Propcbatype && s.Propcbaside == Propcbaside)

                           .ToList()
                           .ForEach(x => { x.Prorealtotal = cc; x.Modifier = "Admin"; x.ModifyDate = DateTime.Now; });
                        DB.SaveChanges();
                    }
                }
                //求和
                //var qs =
                //    (from p in DB.Pp_P1d_Defects
                //     .Where(s => s.IsDeleted == 0)
                //    .Where(s => s.Prorealqty != 0)
                //    .Where(s => s.Proorder == porder)

                //     group p by new
                //     {
                //         p.Proorder,
                //     }
                //        into g
                //     select new
                //     {
                //         Proorder = g.Key.Proorder,

                //         Probadamount = g.Sum(p => p.Probadqty),
                //     }).ToList();
                //if (qs.Any())
                //{
                //    int ccs = qs[0].Probadamount;

                //    DB.Pp_P1d_Defects
                //         .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //    DB.Pp_Defect_P2d_Orders
                //       .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //}
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
        /// 删除P2D工单的产物
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="proLine"></param>
        /// <param name="proDate"></param>
        /// <param name="proLot"></param>
        /// <param name="proItem"></param>
        /// <param name="uid"></param>
        public static void DelOutputPcbaSubs(int ID, string proLine, string proDate, string proLot, string proItem, string uid)
        {
            try
            {
                //求和
                var q =
                    (from p in DB.Pp_P2d_OutputSubs
                     .Where(s => s.Parent == ID)
                     select new
                     {
                         p.Parent
                     }).ToList();

                //判断查询是否为空

                if (!q.Any())
                {
                    //删除日志
                    //删除日志
                    string Contectext = ID + "," + proLine + "," + proDate + "," + proLot + "," + proItem;
                    string OperateType = "删除";//操作标记
                    string OperateNotes = "Del生产OPH* " + Contectext + " *Del生产OPH 的无单身记录已删除";
                    OperateLogHelper.InsNetOperateNotes(uid, OperateType, "生产管理", "生产日报删除", OperateNotes);

                    // DB.Pp_P1d_OutputSubs.Where(l => l.Parent == ID).DeleteFromQuery();
                    DB.Pp_P2d_Outputs.Where(l => l.ID == ID).DeleteFromQuery();
                }
                //求和
                //var qs =
                //    (from p in DB.Pp_P1d_Defects
                //     .Where(s => s.IsDeleted == 0)
                //    .Where(s => s.Prorealqty != 0)
                //    .Where(s => s.Proorder == porder)

                //     group p by new
                //     {
                //         p.Proorder,
                //     }
                //        into g
                //     select new
                //     {
                //         Proorder = g.Key.Proorder,

                //         Probadamount = g.Sum(p => p.Probadqty),
                //     }).ToList();
                //if (qs.Any())
                //{
                //    int ccs = qs[0].Probadamount;

                //    DB.Pp_P1d_Defects
                //         .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //    DB.Pp_Defect_P2d_Orders
                //       .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //}
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
        /// 制二课判断订单是否已经录入不良集计，（Proorder判断）
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="Pline"></param>
        public static void Pp_Defect_P2d_Orders_Update_For_Check(string strPorder, string strPdate, string Pline)
        {
            int noQty = 0;

            var noqs =
                (from p in DB.Pp_P2d_Defects
                 where p.IsDeleted == 0
                 where p.Proorder == strPorder
                 where p.Prodate == strPdate
                 where p.Prolinename == Pline
                 group p by new
                 {
                     p.Prolinename,
                     p.Prodate,
                     p.Proorder,
                 }
                     into g
                 select new
                 {
                     TotalQty = g.Sum(p => p.Prodzeroefects)
                 }).ToList();
            if (noqs.Any())
            {
                noQty = noqs[0].TotalQty;

                if (noQty > 0)
                {
                    Alert.ShowInTop("日期：" + strPdate + ",订单：" + strPorder + ",班组：" + Pline + ",已经输入了不良集计，需要重新输入无不良台数进行更新！");
                }
            }
        }


        /// <summary>
        /// 制一课更新不具合合计，pp_defects=>Probadamount，Pp_Defect_P2d_Orders=>Probadtotal
        /// </summary>
        /// <param name="pdate"></param>
        /// <param name="pline"></param>
        /// <param name="porder"></param>
        public static void Pp_Defect_P2d_BadTotal_Update(string pdate, string pline, string porder, string uid)
        {
            //求和
            var q =
                (from p in DB.Pp_P2d_Defects
                 .Where(s => s.IsDeleted == 0)
                .Where(s => s.Prorealqty != 0)
                .Where(s => s.Prodate == pdate)
                .Where(s => s.Proorder == porder)
                .Where(s => s.Prolinename == pline)

                 group p by new
                 {
                     p.Prodate,
                     p.Proorder,
                     p.Prolinename,
                 }
                    into g
                 select new
                 {
                     Prodate = g.Key.Prodate,
                     Proorder = g.Key.Proorder,
                     Prolinename = g.Key.Prolinename,

                     Probadamount = g.Sum(p => p.Probadqty),
                 }).ToList();

            //判断查询是否为空

            if (q.Any())
            {
                //for遍历
                for (int i = 0; i < q.Count; i++)
                {
                    int cc = q[i].Probadamount;

                    DB.Pp_P1d_Defects
                         .Where(s => s.Prolinename == pline && s.Proorder == porder && s.Prodate == pdate)

                       .ToList()
                       .ForEach(x => { x.Probadamount = cc; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                    DB.SaveChanges();
                }
            }
            //求和
            var qs =
                (from p in DB.Pp_P1d_Defects
                 .Where(s => s.IsDeleted == 0)
                .Where(s => s.Prorealqty != 0)
                .Where(s => s.Proorder == porder)

                 group p by new
                 {
                     p.Proorder,
                 }
                    into g
                 select new
                 {
                     Proorder = g.Key.Proorder,

                     Probadamount = g.Sum(p => p.Probadqty),
                 }).ToList();
            if (qs.Any())
            {
                int ccs = qs[0].Probadamount;

                DB.Pp_P2d_Defects
                     .Where(s => s.Proorder == porder)
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
                DB.Pp_Defect_P2d_Orders
                   .Where(s => s.Proorder == porder)
                   //.Where(s => s.Prodept == (strProdept))
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
            }
        }
    }
}
