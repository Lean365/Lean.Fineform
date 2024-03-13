using System.Web;

namespace LeanFine.Lf_Report
{
    /// <summary>
    /// fico_costing_materialcostratio 的摘要说明
    /// </summary>
    public class fico_costing_materialcostratio : IHttpHandler
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