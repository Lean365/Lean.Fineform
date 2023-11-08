using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using FineUIPro;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Lean.Fineform.Lc_MM
{
    /// <summary>
    /// AutoComplete_item 的摘要说明
    /// </summary>
    public class AutoComplete_item : IHttpHandler
    {
        LeanContext DBCharts = new LeanContext();
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