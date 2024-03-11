using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using FineUIPro;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Fine.Lf_Report
{
    /// <summary>
    /// Pp_tracking_lot 的摘要说明
    /// </summary>
    public class Pp_tracking_lot : IHttpHandler
    {
        FineContext DBCharts = new FineContext();
        JavaScriptSerializer jsS = new JavaScriptSerializer();
        List<object> lists = new List<object>();

        public void ProcessRequest(HttpContext context)
        {
            //获取通过窗体传递的值
            string TransString = System.Web.HttpUtility.UrlDecode(context.Request["TransDate"], Encoding.UTF8);//结束时间
            if (TransString != "未选择"&& TransString != "指定なし" && TransString != "Not selected")
            {
                //转字符串组
                string[] strgroup = TransString.Split(',');
                //ID,Proecnno,Proecnmodel,Proecnbomitem,Proecnoldhbn,Proecnnewhbn
                string tdate = strgroup[0].ToString().Trim();
                string tmodel = strgroup[1].ToString().Trim();
                string tlot = strgroup[2].ToString().Trim();
                string titem = strgroup[3].ToString().Trim();


                //获取一同发送过来的参数
                //string command = context.Request["cmd"];
                context.Response.ContentType = "text/plain";
                var q = from p in DBCharts.Pp_TrackingCounts
                        where p.isDelete == 0
                        where p.Pro_Date.Contains(tdate)
                        where p.Pro_Model.Contains(tmodel)
                        where p.Pro_Lot.Contains(tlot)
                        where p.Pro_Item.Contains(titem)
                        //where p.Pro_Process.Contains(tprocess)
                        select new
                        {
                            p.Pro_Date,
                            p.Pro_Model,
                            p.Pro_Lot,
                            p.Pro_Item,
                            Pro_Process = (p.Pro_Process.Length == 1 ? "0" + p.Pro_Process : p.Pro_Process),
                            p.Pro_ProcessQty,
                            p.Pro_MaxTime,
                            p.Pro_MinTime,
                            p.Pro_AvgTime,
                            p.Pro_StdDev,
                        };

                q = q.OrderBy(u => u.Pro_Process);
                DataSet ds = new DataSet();
                DataTable dt = ConvertHelper.LinqConvertToDataTable(q.AsQueryable());
                ds.Tables.Add(ConvertHelper.LinqConvertToDataTable(q.AsQueryable()));
                //用来传回去的内容
                //List<object> lists = new List<object>();                       //创建object类型的泛型
                foreach (DataRow dr in dt.Rows)
                {
                    var obj = new { name = dr["Pro_Process"], value1 = dr["Pro_MaxTime"], value2 = dr["Pro_MinTime"], value3 = dr["Pro_ProcessQty"], value4 = dr["Pro_StdDev"], value5 = dr["Pro_AvgTime"] };  //key，value
                    lists.Add(obj);
                }
                //var jsS = new JavaScriptSerializer();                           //创建json对象
                context.Response.Write(jsS.Serialize(lists));                   //返回数据
            }
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