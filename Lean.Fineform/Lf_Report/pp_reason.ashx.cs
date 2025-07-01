using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// pp_reason 的摘要说明
    /// </summary>
    public class pp_reason : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();
        private JavaScriptSerializer jsS = new JavaScriptSerializer();
        private List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";

            //查询在特定日期的全部未达成原因Probadcou，Probadmemo
            var q_all =
                (from p in DBCharts.Pp_P1d_OutputSubs
                     //join b in DB.proOutputs on p.OPHID equals b.OPHID
                 where p.Prodate.Substring(0, 6).CompareTo(atedate) == 0
                 where !string.IsNullOrEmpty(p.Probadcou)
                 where !string.IsNullOrEmpty(p.Probadmemo)
                 where !string.IsNullOrEmpty(p.Probadcou)
                 where !string.IsNullOrEmpty(p.Probadmemo)
                 where p.IsDeleted == 0
                 group p by new { Probadcou = p.Probadcou.Substring(0, 6), Probadmemo = p.Probadmemo.Substring(0, 6) }
                            into g
                 select new
                 {
                     g.Key.Probadcou,
                     g.Key.Probadmemo,
                     value = g.Count(),
                 }).ToList();

            int i = 0;
            var q_Rows = from a in q_all.AsEnumerable()
                         select new
                         {
                             id = i++,
                             a.Probadcou,
                             pid = (from o in DBCharts.Pp_P1d_OutputSubs
                                    where o.Probadcou.Substring(0, 6) != a.Probadcou
                                    select o).Distinct().Count() + 1,
                             a.Probadmemo,
                             a.value,
                         };

            var q_PRows = q_Rows.Select(E => new { E.Probadcou, E.pid }).ToList().Distinct().OrderBy(u => u.pid);

            //q_Rows = q_Rows.OrderByDescending(u => u.RowNum);

            if (q_PRows.Any())
            {
                DataSet q_couds = new DataSet();
                DataTable q_coudt = ConvertHelper.LinqConvertToDataTable(q_PRows.AsQueryable());
                q_couds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_PRows.AsQueryable()));
                //用来传回去的内容
                //List<object> lists = new List<object>();                       //创建object类型的泛型
                foreach (DataRow dr in q_coudt.Rows)
                {
                    var obj = new { id = dr["pid"], name = dr["Probadcou"] };  //key，value
                    lists.Add(obj);
                }
                //var jsS = new JavaScriptSerializer();                           //创建json对象
                //context.Response.Write(jsS.Serialize(lists));                   //返回数据
            }
            var q_memo = (from p in q_Rows
                          select p).ToList().Take(10);

            if (q_memo.Any())
            {
                DataSet q_memods = new DataSet();
                DataTable q_memodt = ConvertHelper.LinqConvertToDataTable(q_memo.AsQueryable());
                q_memods.Tables.Add(ConvertHelper.LinqConvertToDataTable(q_memo.AsQueryable()));
                //用来传回去的内容
                //List<object> lists = new List<object>();                       //创建object类型的泛型
                foreach (DataRow dr in q_memodt.Rows)
                {
                    var obj = new { id = dr["id"], name = dr["Probadmemo"], pid = dr["pid"], Pname = dr["Probadcou"], value = dr["value"] };  //key，value
                    lists.Add(obj);
                }
                //var jsS = new JavaScriptSerializer();                           //创建json对象
                //context.Response.Write(jsS.Serialize(lists));              //返回数据
            }
            context.Response.Write(jsS.Serialize(lists));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}