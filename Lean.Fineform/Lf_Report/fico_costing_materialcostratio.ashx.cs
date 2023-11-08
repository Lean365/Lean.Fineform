using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lean.Fineform.Lf_Report
{
    /// <summary>
    /// Fico_costing_materialcostratio 的摘要说明
    /// </summary>
    public class Fico_costing_materialcostratio : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
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