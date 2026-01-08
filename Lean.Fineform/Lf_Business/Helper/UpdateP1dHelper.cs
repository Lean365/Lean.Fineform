using FineUIPro;
using LeanFine.Lf_Business.Models.PP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;

namespace LeanFine.Lf_Business.Helper
{
    public class UpdateP1dHelper : PageBase
    {
        public static Decimal ophQty, orderQty;

        /// <summary>
        /// 制一课更新不良表（Pp_P1d_Defects）中实际生产数量，条件按日期，订单，班组
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="strPline"></param>
        public static void Pp_P1d_Defect_Update_For_Realqty(string strPorder, string strPdate, string strPline, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P1d_OutputSubs
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

            DB.Pp_P1d_Defects
              .Where(s => s.Proorder == strPorder)
              .Where(s => s.Prodate == strPdate)
              .Where(s => s.Prolinename == strPline)
              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }


        /// <summary>
        /// 制一课更新订单不良集计表（Pp_Defect_P1d_Orders），条件按订单,生产实绩（Prorealqty）
        /// </summary>
        /// <param name="strPorder"></param>
        public static void Pp_P1d_Defect_Orders_Update_For_Realqty(string strPorder, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P1d_OutputSubs
                     where p.IsDeleted == 0
                     where p.Proorder == strPorder
                     //where p.Remark.Contains(strLineType)
                     group p by p.Proorder into g
                     select new
                     {
                         TotalQty = g.Sum(p => p.Prorealqty)
                     }).ToList();
            if (q.Any())
            {
                realQty = q[0].TotalQty;
            }

            DB.Pp_Defect_P1d_Orders
              .Where(s => s.Proorder == strPorder)
              //.Where(s => s.Prodept == (strProdept))
              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 制一课更新批次不良集计表（Pp_Defect_P1d_Lots），条件按订单,生产实绩（Prorealqty）
        /// </summary>
        /// <param name="strProlot"></param>
        public static void Pp_P1d_Defect_Lots_Update_For_Realqty(string strProlot, string uid)
        {
            int realQty = 0;
            // 查询主表
            var mainQty = (from p in DB.Pp_P1d_OutputSubs
                           where p.IsDeleted == 0
                           where p.Prolot == strProlot
                           select p.Prorealqty).DefaultIfEmpty(0).Sum();

            // 查询其他三个表
            var eppDateQty = (from e in DB.Pp_P1d_Epp_Date_OutputSubs
                              where e.IsDeleted == 0
                              where e.Prolot == strProlot
                              select e.Prorealqty).DefaultIfEmpty(0).Sum();

            var eppQty = (from epp in DB.Pp_P1d_Epp_OutputSubs
                          where epp.IsDeleted == 0
                          where epp.Prolot == strProlot
                          select epp.Prorealqty).DefaultIfEmpty(0).Sum();

            var modifyQty = (from m in DB.Pp_P1d_Modify_OutputSubs
                             where m.IsDeleted == 0
                             where m.Prolot == strProlot
                             select m.Prorealqty).DefaultIfEmpty(0).Sum();

            // 合并所有数量
            realQty = (int)(mainQty + eppDateQty + eppQty + modifyQty);
            //更新生产实绩
            //var q =
            //        (from p in DB.Pp_P1d_OutputSubs
            //         where p.IsDeleted == 0
            //         where p.Prolot == strProlot
            //         //where p.Remark.Contains(strLineType)
            //         group p by p.Prolot into g
            //         select new
            //         {
            //             TotalQty = g.Sum(p => p.Prorealqty)
            //         }).ToList();
            //if (q.Any())
            //{
            //    realQty = q[0].TotalQty;
            //}

            DB.Pp_Defect_P1d_Lots
              .Where(s => s.Prolot == strProlot)
              //.Where(s => s.Prodept == (strProdept))
              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }



        /// <summary>
        /// 制一课更新批次不良集计表（Pp_Defect_P1d_Lots），条件按订单,生产实绩（Prorealqty）
        /// </summary>
        /// <param name="strProlot"></param>
        public static void Pp_P1d_Defect_Lots_Update_For_TotalOrderqty(string strProlot, string uid)
        {

            var q = (from item in DB.Pp_P1d_OutputSubs
                     where item.IsDeleted == 0
                     where item.Proordertype.Contains("ZDTA")
                     where item.Prolot == strProlot
                     select new { item.Proorder, item.Proorderqty, item.Prolot })
               .Union(from item in DB.Pp_P1d_Epp_Date_OutputSubs
                      where item.IsDeleted == 0
                      where item.Prolot == strProlot
                      select new { item.Proorder, item.Proorderqty, item.Prolot })
               .Union(from item in DB.Pp_P1d_Epp_OutputSubs
                      where item.IsDeleted == 0
                      where item.Prolot == strProlot
                      select new { item.Proorder, item.Proorderqty, item.Prolot })
               .Union(from item in DB.Pp_P1d_Modify_OutputSubs
                      where item.IsDeleted == 0
                      where item.Prolot == strProlot
                      select new { item.Proorder, item.Proorderqty, item.Prolot })
                               .Distinct()
                   .GroupBy(x => x.Prolot)
                   .Select(g => new
                   {
                       Prolot = g.Key,
                       TotalOrderQty = g.Sum(x => x.Proorderqty),
                       DistinctRecordCount = g.Count()
                   })
                   .ToList();

            // 确保在查询中添加对 strProlot 的筛选
            //var q = (from item in DB.Pp_P1d_OutputSubs//Pp_P1d_Epp_Date_OutputSub，Pp_P1d_Epp_OutputSub，Pp_P1d_Modify_OutputSub
            //         where item.Prolot == strProlot  // 添加筛选条件
            //         select new { item.Proorder, item.Proorderqty, item.Prolot })
            //       .Distinct()
            //       .GroupBy(x => x.Prolot)
            //       .Select(g => new
            //       {
            //           Prolot = g.Key,
            //           TotalOrderQty = g.Sum(x => x.Proorderqty),
            //           DistinctRecordCount = g.Count()
            //       })
            //       .ToList();

            int TotalOrderQty = q.Any() ? (int)q[0].TotalOrderQty : 0;

            // 更新生产实绩
            DB.Pp_Defect_P1d_Lots
              .Where(s => s.Prolot == strProlot)
              .ToList()
              .ForEach(x =>
              {
                  x.Prolotqty = TotalOrderQty;
                  x.Proorderqty = TotalOrderQty;
                  x.Modifier = uid;
                  x.ModifyDate = DateTime.Now;
              });

            DB.SaveChanges();
        }
        /// <summary>
        /// 仅更新目标表中已存在的订单统计信息（不新增记录）
        /// </summary>
        /// <param name="strPorder">生产订单号</param>
        public static void Pp_P1d_Defect_Orders_Update_ExistingDefectOrderOnly(string strPorder, string uid)
        {
            try
            {
                // 检查目标表是否存在该订单
                var targetRecord = DB.Pp_Defect_P1d_Orders
                    .FirstOrDefault(d => d.Proorder == strPorder);

                if (targetRecord == null)
                {
                    Console.WriteLine($"警告：订单 {strPorder} 在目标表中不存在，已跳过");
                    return;
                }

                // 查询源数据
                var sourceData = DB.Pp_P1d_OutputSubs
                    .Where(p => p.Proorder == strPorder)
                    .ToList();

                if (!sourceData.Any())
                {
                    throw new Exception($"订单 {strPorder} 在源表中无数据");
                }

                // 计算新值
                targetRecord.Prodate = $"{sourceData.Min(p => p.Prodate):yyyyMMdd}~{sourceData.Max(p => p.Prodate):yyyyMMdd}";
                targetRecord.Prolinename = string.Join(",", sourceData.Select(p => p.Prolinename).Distinct().OrderBy(n => n));

                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"更新订单 {strPorder} 失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 仅更新目标表中已存在的订单统计信息（不新增记录）
        /// </summary>
        /// <param name="strProlot">生产订单号</param>
        public static void Pp_P1d_Defect_Lots_Update_ExistingDefectLotsOnly(string strProlot, string uid)
        {
            try
            {
                // 检查目标表是否存在该订单
                var targetRecord = DB.Pp_Defect_P1d_Lots
                    .FirstOrDefault(d => d.Prolot == strProlot);

                if (targetRecord == null)
                {
                    Console.WriteLine($"警告：订单 {strProlot} 在目标表中不存在，已跳过");
                    return;
                }

                // 查询源数据
                var sourceData = DB.Pp_P1d_OutputSubs
                    .Where(p => p.Prolot == strProlot)
                    .ToList();

                if (!sourceData.Any())
                {
                    throw new Exception($"订单 {strProlot} 在源表中无数据");
                }

                // 计算新值
                targetRecord.Prodate = $"{sourceData.Min(p => p.Prodate):yyyyMMdd}~{sourceData.Max(p => p.Prodate):yyyyMMdd}";
                targetRecord.Promodel = string.Join(",", sourceData.Select(p => p.Promodel).Distinct().OrderBy(n => n));
                targetRecord.Proitem = string.Join(",", sourceData.Select(p => p.Prohbn).Distinct().OrderBy(n => n));
                targetRecord.Proorder = string.Join(",", sourceData.Select(p => p.Proorder).Distinct().OrderBy(n => n));
                targetRecord.Prolinename = string.Join(",", sourceData.Select(p => p.Prolinename).Distinct().OrderBy(n => n));
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"更新订单 {strProlot} 失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 制一课更新无不良台数，条件生产订单，（Prodzeroefects）
        /// </summary>
        /// <param name="strPorder">生产订单号</param>
        /// <param name="uid">用户ID</param>
        public static void Pp_P1d_Defect_Orders_Update_For_NoBadQty(string strProStartdate, string strPorder, string uid)
        {
            int noQty = 0;
            int okQty = 0;
            var noqs =
                from p in DB.Pp_P1d_Defects
                where p.Prodate.Substring(0, 6).CompareTo(strProStartdate) <= 0
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
            var results = (from o in DB.Pp_P1d_OutputSubs
                           where o.Proorder == strPorder && o.IsDeleted == 0
                           where !(from d in DB.Pp_P1d_Defects
                                   where d.IsDeleted == 0
                                   select new { d.Prodate, d.Proorder, d.Prolinename })
                                  .Contains(new { o.Prodate, o.Proorder, o.Prolinename })
                           select o).ToList();

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

            DB.Pp_Defect_P1d_Orders
                  .Where(s => s.Proorder == strPorder)
                  .ToList()
                  .ForEach(x => { x.Prodzeroefects = noQty + okQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 制一课更新无不良台数，条件生产订单，（Prodzeroefects）
        /// </summary>
        /// <param name="strProlot">生产订单号</param>
        /// <param name="uid">用户ID</param>
        public static void Pp_P1d_Defect_Lots_Update_For_NoBadQty(string strProlot, string uid)
        {
            int noQty = 0;
            int okQty = 0;
            var noqs =
                from p in DB.Pp_P1d_Defects
                    //where p.Prodate.Substring(0, 6).CompareTo(strProStartdate) <= 0
                where p.IsDeleted == 0
                where p.Prolot == strProlot
                group p by new
                {
                    p.Prolot,
                    p.Prodzeroefects,
                }
                     into g
                select new
                {
                    g.Key.Prolot,

                    g.Key.Prodzeroefects,
                };

            //统计无不良台数（有不良录入时）
            var noqty = (from p in noqs
                         group p by new
                         {
                             p.Prolot,
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
            var results = (from o in DB.Pp_P1d_OutputSubs
                           where o.Prolot == strProlot && o.IsDeleted == 0
                           where !(from d in DB.Pp_P1d_Defects
                                   where d.IsDeleted == 0
                                   select new { d.Prodate, d.Proorder, d.Prolinename })
                                  .Contains(new { o.Prodate, o.Proorder, o.Prolinename })
                           select o).ToList();
            //var ids = new HashSet<string>(DB.Pp_P1d_Defects.Select(x => x.Prodate + x.Proorder + x.Prolinename));
            //var results = DB.Pp_P1d_OutputSubs.Where(x => !ids.Contains(x.Prodate + x.Proorder + x.Prolinename) && x.Prolot == strProlot && x.IsDeleted == 0);
            //var res = results.ToList();
            var okqty = (from a in results
                         group a by a.Prolot
                                into g

                         select new
                         {
                             TotalQty = g.Sum(p => p.Prorealqty)
                         }).ToList();
            if (okqty.Any())
            {
                okQty = okqty[0].TotalQty;
            }

            DB.Pp_Defect_P1d_Lots
                  .Where(s => s.Prolot == strProlot)
                  .ToList()
                  .ForEach(x => { x.Prodzeroefects = noQty + okQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 制一课判断订单是否已经录入不良集计，（Proorder判断）
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="Pline"></param>
        public static void Pp_P1d_Defect_For_Check(string strPorder, string strPdate, string Pline)
        {
            int noQty = 0;

            var noqs =
                (from p in DB.Pp_P1d_Defects
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
        ///  制一课更新订单状态，条件订单号
        /// </summary>
        /// <param name="strPorder"></param>
        public static void Pp_Order_Update_For_Status(string strPorder)
        {
            int okQty = 0;
            //string strPorder, string strPdate, string strPline
            // assumes that the ID property is an int - change the generic type if it's not
            var ids = new HashSet<string>(DB.Pp_P1d_Defects.Select(x => x.Prodate + x.Proorder + x.Prolinename));
            var results = DB.Pp_P1d_OutputSubs.Where(x => !ids.Contains(x.Prodate + x.Proorder + x.Prolinename) && x.Proorder == strPorder && x.IsDeleted == 0);

            var q = (from a in results
                     group a by a.Proorder
                                into g

                     select new
                     {
                         TotalQty = g.Sum(p => p.Prorealqty)
                     }).ToList();
            if (q.Any())
            {
                okQty = q[0].TotalQty;
            }
        }

        /// <summary>
        /// 制一课更新不良集计(Probadtotal)
        /// </summary>
        /// <param name="strPorder"></param>
        public static void Pp_P1d_Defect_Orders_Update_For_BadQtyTotal(string strPorder, string uid)
        {
            int BadTotalQty = 0;
            var q = (from a in DB.Pp_P1d_Defects
                     where a.Proorder == strPorder
                     //where a.Remark.Contains(strLineType)
                     group a by a.Proorder
                  into g
                     select new
                     {
                         TotalQty = g.Sum(p => p.Probadqty)
                     }).ToList();
            if (q.Any())

            {
                BadTotalQty = q[0].TotalQty;
            }
            DB.Pp_Defect_P1d_Orders
                  .Where(s => s.Proorder == strPorder)
                  //.Where(s => s.Prodate == strPdate)
                  //.Where(s => s.Prodept == (strProdept))
                  .ToList()
                  .ForEach(x => { x.Probadtotal = BadTotalQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 制一课更新不具合合计，pp_defects=>Probadamount，Pp_Defect_P1d_Orders=>Probadtotal
        /// </summary>
        /// <param name="pdate"></param>
        /// <param name="pline"></param>
        /// <param name="porder"></param>
        public static void Pp_P1d_Defect_Orders_Update_For_TotalBadQty(string pdate, string pline, string porder, string uid)
        {
            //求和
            var q =
                (from p in DB.Pp_P1d_Defects
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

                DB.Pp_P1d_Defects
                     .Where(s => s.Proorder == porder)
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
                DB.Pp_Defect_P1d_Orders
                   .Where(s => s.Proorder == porder)
                   //.Where(s => s.Prodept == (strProdept))
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// 制一课更新不具合合计，pp_defects=>Probadamount，Pp_Defect_P1d_Orders=>Probadtotal
        /// </summary>
        /// <param name="strProlot"></param>

        public static void Pp_P1d_Defect_Lots_Update_For_TotalBadQty(string strProlot, string uid)
        {
            //求和
            var qs =
                (from p in DB.Pp_P1d_Defects
                 .Where(s => s.IsDeleted == 0)
                .Where(s => s.Prorealqty != 0)
                .Where(s => s.Prolot == strProlot)

                 group p by new
                 {
                     p.Prolot,
                 }
                    into g
                 select new
                 {
                     g.Key.Prolot,

                     TotalBadqty = g.Sum(p => p.Probadqty),
                 }).ToList();
            if (qs.Any())
            {
                int ccs = qs[0].TotalBadqty;

                DB.Pp_Defect_P1d_Lots
                   .Where(s => s.Prolot == strProlot)
                   //.Where(s => s.Prodept == (strProdept))
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
            }
        }


        /// <summary>
        /// 制一课判断工单是否完成
        /// </summary>
        /// <param name="strGuid"></param>
        public static void OrderFinish(Guid strGuid)
        {
            Pp_P1d_Output current = DB.Pp_P1d_Outputs

                .Where(u => u.GUID == strGuid).FirstOrDefault();

            String OphOrder = current.Proorder;

            //String SS = "SELECT[Proorder], SUM([Prorealqty])[Prorealqty]  FROM[OneHerba].[dbo].[Pp_P1d_OutputSubs] SUB" +
            //           "   LEFT JOIN[dbo].[Pp_P1d_Outputs] OPH ON SUB.OPHID = OPH.OPHID WHERE [Proorder]='"+ OphOrder + "' GROUP BY[Proorder]" +
            //           "   UNION ALL" +
            //           "   (SELECT Porderno, Porderqty FROM[dbo].[proOrders]" +
            //           "   WHERE Porderno = (" +
            //           "   SELECT[Proorder] FROM[dbo].[Pp_P1d_Outputs] WHERE [Proorder]='" + OphOrder + "'));";

            //语句描述：连接查询表。
            var q =
            from sub in DB.Pp_P1d_OutputSubs
            join oph in DB.Pp_P1d_Outputs on sub.GUID equals oph.GUID

            where oph.Proorder == OphOrder
            select new
            {
                Proorder = oph.Proorder,
                Prorealqty = sub.Prorealqty
            };
            //语句描述：使用Group By和Sum得到每个CategoryID 的单价总计。
            var qs =
                    from p in q
                    group p by new { p.Proorder } into g
                    select new
                    {
                        Proorder = g.Key.Proorder,
                        Prorealqty = g.Sum(p => p.Prorealqty)
                    };
            //判断linq语句结果是否为空的方法
            if (qs.FirstOrDefault() != null)
            {
                //获取linq的值
                foreach (var v in qs)
                {
                    ophQty = v.Prorealqty;
                }
            }
            else
            {
                ophQty = 0;
            }
            //语句描述：连接查询表。
            var qo =
                    from sub in DB.Pp_Orders
                        //join oph in DB.Pp_P1d_Outputs on sub.OPHID equals oph.OPHID

                    where sub.Porderno == OphOrder
                    select new
                    {
                        Proorder = sub.Porderno,
                        Porderqty = sub.Porderqty
                    };

            //判断linq语句结果是否为空的方法
            if (qo.FirstOrDefault() != null)
            {
                //获取linq的值
                foreach (var v in qo)
                {
                    orderQty = v.Porderqty;
                }
            }
            else
            {
                orderQty = 0;
            }
        }


        /// <summary>
        /// 制一课更新订单已生产数量
        /// </summary>
        /// <param name="strGuid"></param>
        public static void UpdateOrderRealQty(string OrderNo, string uid)//(Guid strGuid)
        {
            try
            {
                var q_count = (from a in DB.Pp_P1d_OutputSubs
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

        //删除OPH时更新生产订单
        public static void DelUpdateOrderRealQty(string OrderNo, string uid)//(Guid strGuid)
        {
            try
            {
                DB.Pp_Orders
                    .Where(s => s.Porderno == OrderNo)
                    .ToList()
                    .ForEach(x => { x.Porderreal = 0; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
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
        /// 删除P1D工单的产物
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="proLine"></param>
        /// <param name="proDate"></param>
        /// <param name="proLot"></param>
        /// <param name="proItem"></param>
        /// <param name="uid"></param>
        public static void DelOutputAssySubs(int ID, string proLine, string proDate, string proLot, string proItem, string uid)
        {
            try
            {
                //求和
                var q =
                    (from p in DB.Pp_P1d_OutputSubs
                     .Where(s => s.Parent == ID)
                     select new
                     {
                         p.Parent
                     }).ToList();

                //判断查询是否为空

                if (!q.Any())
                {
                    //删除日志
                    string Contectext = ID + "," + proLine + "," + proDate + "," + proLot + "," + proItem;
                    string OperateType = "删除";//操作标记
                    string OperateNotes = "Del生产OPH* " + Contectext + " *Del生产OPH 的无单身记录已删除";
                    OperateLogHelper.InsNetOperateNotes(uid, OperateType, "生产管理", "生产日报删除", OperateNotes);

                    // DB.Pp_P1d_OutputSubs.Where(l => l.Parent == ID).DeleteFromQuery();
                    DB.Pp_P1d_Outputs.Where(l => l.ID == ID).DeleteFromQuery();
                }
                //求和
                //var qs =
                //    (from p in DB.Pp_Assyes
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

                //    DB.Pp_Assyes
                //         .Where(s => s.Proorder == porder)
                //       .ToList()
                //       .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                //    DB.SaveChanges();
                //    DB.Pp_Defect_P1d_Orders
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
        /// 更新随机卡
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="BoundNum"></param>
        /// <param name="uid"></param>

        public static void UpdateBoundNumForOrder(string OrderNo, string uid)
        {

            try
            {
                // 1. 查询不重复的 Prorandomcard
                var q = DB.Pp_P1d_Defects
                    .Where(d => d.Proorder == OrderNo)
                    .Where(d => d.Prorandomcard != null)
                    .Where(d => d.IsDeleted == 0)
                    .Select(d => d.Prorandomcard)
                    .Distinct()
                    .OrderBy(num => num) // 可选：按数字排序
                    .ToList();

                if (q.Any())
                {
                    // 2. 组合成 "1,2,3" 格式
                    string combinedProrandomcards = string.Join(",", q);

                    // 3. 更新到 Pp_Defect_P1d_Order 表
                    var orderToUpdate = DB.Pp_Defect_P1d_Orders
                        .FirstOrDefault(o => o.Proorder == OrderNo);

                    if (orderToUpdate != null)
                    {
                        orderToUpdate.Prorandomcard = combinedProrandomcards;
                        DB.SaveChanges();
                    }
                    //else
                    //{
                    //    // 如果没有找到记录，可以选择创建新记录
                    //    var newOrder = new Pp_Defect_P1d_Order
                    //    {
                    //        Proorder = OrderNo,
                    //        Prorandomcard = combinedProrandomcards
                    //        // 设置其他必要字段
                    //    };
                    //    DB.Pp_Defect_P1d_Orders.Add(newOrder);
                    //    DB.SaveChanges();
                    //}
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"更新 Prorandomcard 时出错: {ex.Message}");
                throw; // 或者根据需求处理
            }



        }


        /// <summary>
        /// 更新随机卡
        /// </summary>
        /// <param name="Lot"></param>
        /// <param name="BoundNum"></param>
        /// <param name="uid"></param>

        public static void UpdateBoundNumForLot(string Lot, string uid)
        {

            try
            {
                // 1. 查询不重复的 Prorandomcard
                var q = DB.Pp_P1d_Defects
                    .Where(d => d.Prolot == Lot)
                    .Where(d => d.Prorandomcard != null)
                    .Where(d => d.IsDeleted == 0)
                    .Select(d => d.Prorandomcard)
                    .Distinct()
                    .OrderBy(num => num) // 可选：按数字排序
                    .ToList();

                if (q.Any())
                {
                    // 2. 组合成 "1,2,3" 格式
                    string combinedProrandomcards = string.Join(",", q);

                    // 3. 更新到 Pp_Defect_P1d_Order 表
                    var orderToUpdate = DB.Pp_Defect_P1d_Orders
                        .FirstOrDefault(o => o.Prolot == Lot);

                    if (orderToUpdate != null)
                    {
                        orderToUpdate.Prorandomcard = combinedProrandomcards;
                        DB.SaveChanges();
                    }
                    //else
                    //{
                    //    // 如果没有找到记录，可以选择创建新记录
                    //    var newOrder = new Pp_Defect_P1d_Order
                    //    {
                    //        Proorder = OrderNo,
                    //        Prorandomcard = combinedProrandomcards
                    //        // 设置其他必要字段
                    //    };
                    //    DB.Pp_Defect_P1d_Orders.Add(newOrder);
                    //    DB.SaveChanges();
                    //}
                }
            }
            catch (Exception ex)
            {
                // 处理异常
                Console.WriteLine($"更新 Prorandomcard 时出错: {ex.Message}");
                throw; // 或者根据需求处理
            }



        }
    }
}
