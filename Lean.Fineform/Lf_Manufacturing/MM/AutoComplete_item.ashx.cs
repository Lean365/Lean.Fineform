using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lc_MM
{
    /// <summary>
    /// AutoComplete_item 的摘要说明
    /// </summary>
    public class AutoComplete_item : IHttpHandler
    {
        private LeanFineContext DBCharts = new LeanFineContext();

        public void ProcessRequest(HttpContext context)
        {
            //System.Threading.Thread.Sleep(2000);

            //获取一同发送过来的参数
            //string command = context.Request["cmd"];
            context.Response.ContentType = "text/plain";
            List<string> list = (from a in DBCharts.Mm_Materials

                                 select new
                                 {
                                     a.MatItem
                                 }).Take(5).ToList().Select(u => u.MatItem).ToList<string>();

            string[] sArray = list.ToArray();

            String term = context.Request.QueryString["term"];
            if (!String.IsNullOrEmpty(term))
            {
                term = term.ToLower();

                JArray ja = new JArray();
                foreach (string lang in sArray)
                {
                    if (lang.ToLower().Contains(term))
                    {
                        ja.Add(lang);
                    }
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write(ja.ToString());
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