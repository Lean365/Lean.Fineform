using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using LeanFine.Lf_Business.Models.PP;

namespace LeanFine
{
    public class UpdatingHelper : PageBase
    {
        public static Decimal ophQty, orderQty;

        /// <summary>
        /// 更新不良表（pp_defect）中实际生产数量，条件按日期，订单，班组
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="strPline"></param>
        public static void DefectRealqty_Update(string strPorder, string strPdate, string strPline, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P1d_OutputSubs
                     where p.isDeleted == 0
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
        /// 更新订单不良集计表（Pp_Defect_Totals），条件按订单,生产实绩（Prorealqty）
        /// </summary>
        /// <param name="strPorder"></param>
        public static void DefectTotalRealqty_Update(string strPorder, string uid)
        {
            int realQty = 0;

            //更新生产实绩
            var q =
                    (from p in DB.Pp_P1d_OutputSubs
                     where p.isDeleted == 0
                     where p.Proorder == strPorder
                     group p by p.Prolot into g
                     select new
                     {
                         TotalQty = g.Sum(p => p.Prorealqty)
                     }).ToList();
            if (q.Any())
            {
                realQty = q[0].TotalQty;
            }

            DB.Pp_Defect_Totals
              .Where(s => s.Proorder == strPorder)

              .ToList()
              .ForEach(x => { x.Prorealqty = realQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 更新无不良台数，条件生产订单，（Pronobadqty）
        /// </summary>
        /// <param name="strPorder"></param>
        public static void noDefectQty_Update(string strPorder, string uid)
        {
            int noQty = 0;
            int okQty = 0;
            var noqs =
                from p in DB.Pp_P1d_Defects
                where p.isDeleted == 0
                where p.Proorder == strPorder
                group p by new
                {
                    p.Prolinename,
                    p.Prodate,
                    p.Proorder,
                    p.Pronobadqty,
                }
                     into g
                select new
                {
                    g.Key.Prodate,
                    g.Key.Prolinename,
                    g.Key.Proorder,
                    g.Key.Pronobadqty,
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
                             TotalQty = g.Sum(p => p.Pronobadqty)
                         }).ToList();

            if (noqty.Any())
            {
                noQty = noqty[0].TotalQty;
            }
            //统计无不良台数（无不良录入时）
            var ids = new HashSet<string>(DB.Pp_P1d_Defects.Select(x => x.Prodate + x.Proorder + x.Prolinename));
            var results = DB.Pp_P1d_OutputSubs.Where(x => !ids.Contains(x.Prodate + x.Proorder + x.Prolinename) && x.Proorder == strPorder && x.isDeleted == 0);

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

            DB.Pp_Defect_Totals
                  .Where(s => s.Proorder == strPorder)
                  //.Where(s => s.Prodate == strPdate)

                  .ToList()
                  .ForEach(x => { x.Pronobadqty = noQty + okQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 判断订单是否已经录入不良集计，（Proorder判断）
        /// </summary>
        /// <param name="strPorder"></param>
        /// <param name="strPdate"></param>
        /// <param name="Pline"></param>
        public static void CheckDefectData(string strPorder, string strPdate, string Pline)
        {
            int noQty = 0;

            var noqs =
                (from p in DB.Pp_P1d_Defects
                 where p.isDeleted == 0
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
                     TotalQty = g.Sum(p => p.Pronobadqty)
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
        ///
        /// </summary>
        /// <param name="strPorder"></param>
        public static void okOrder_Update(string strPorder)
        {
            int okQty = 0;
            //string strPorder, string strPdate, string strPline
            // assumes that the ID property is an int - change the generic type if it's not
            var ids = new HashSet<string>(DB.Pp_P1d_Defects.Select(x => x.Prodate + x.Proorder + x.Prolinename));
            var results = DB.Pp_P1d_OutputSubs.Where(x => !ids.Contains(x.Prodate + x.Proorder + x.Prolinename) && x.Proorder == strPorder && x.isDeleted == 0);

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
        /// 更新不良集计(Probadtotal)
        /// </summary>
        /// <param name="strPorder"></param>
        public static void badQtyTotal(string strPorder, string uid)
        {
            int BadTotalQty = 0;
            var q = (from a in DB.Pp_P1d_Defects
                     where a.Proorder == strPorder
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
            DB.Pp_Defect_Totals
                  .Where(s => s.Proorder == strPorder)
                  //.Where(s => s.Prodate == strPdate)

                  .ToList()
                  .ForEach(x => { x.Probadtotal = BadTotalQty; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
            DB.SaveChanges();
        }

        /// <summary>
        /// 更新不具合合计，pp_defects=>Probadamount，Pp_Defect_Totals=>Probadtotal
        /// </summary>
        /// <param name="pdate"></param>
        /// <param name="pline"></param>
        /// <param name="porder"></param>
        public static void UpdatebadAmount(string pdate, string pline, string porder, string uid)
        {
            //求和
            var q =
                (from p in DB.Pp_P1d_Defects
                 .Where(s => s.isDeleted == 0)
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
                 .Where(s => s.isDeleted == 0)
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
                DB.Pp_Defect_Totals
                   .Where(s => s.Proorder == porder)
                   .ToList()
                   .ForEach(x => { x.Probadtotal = ccs; x.Modifier = uid; x.ModifyDate = DateTime.Now; });
                DB.SaveChanges();
            }
        }

        //判断工单是否完成
        /// <summary>
        /// 判断工单是否完成
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

        //更新订单已生产数量
        /// <summary>
        /// 更新订单已生产数量
        /// </summary>
        /// <param name="strGuid"></param>
        public static void UpdateOrderRealQty(string OrderNo, string uid)//(Guid strGuid)
        {
            try
            {
                var q_count = (from a in DB.Pp_P1d_OutputSubs
                               where a.isDeleted == 0
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
    }
}