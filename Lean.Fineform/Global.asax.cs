using System;
using System.Web;
using System.Collections;
using System.Data.Entity;
using FineUIPro;
namespace Lean.Fineform
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new LeanDatabaseInitializer());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }
        protected virtual void Application_EndRequest()
        {
            var context = HttpContext.Current.Items["__LeanWebformContext"] as LeanContext;


            if (context != null)
            {
                context.Database.CommandTimeout = 3600000;
                context.Dispose();
            }
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。
            //Hashtable hOnline = (Hashtable)Application["Online"];
            //if (hOnline != null && hOnline[Session.SessionID] != null)
            //{
            //    hOnline.Remove(Session.SessionID);
            //    Application.Lock();
            //    Application["Online"] = hOnline;
            //    Application.UnLock();
            //}
            Hashtable hOnline = (Hashtable)Application["Online"];
            if (hOnline != null)
            {
                if (hOnline[Session.SessionID] != null)
                {
                    hOnline.Remove(Session.SessionID);
                    Application.Lock();
                    Application["Online"] = hOnline;
                    Application.UnLock();
                }
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //记录登录用户sessionid

        }
    }
}