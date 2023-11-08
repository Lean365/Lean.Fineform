using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Lean.Fineform.Lf_Office.EM
{
    /// <summary>
    /// event_del 的摘要说明
    /// </summary>
    public class event_del : IHttpHandler
    {

        public static SqlConnection AppConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ToString());
        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request["id"];
            //SqlConnection con = new SqlConnection(@"Password=tac26901333;Persist Security Info=True;User ID=sa;Initial Catalog=DocumentManagement;Data Source=.");

            string dfsdf = "DELETE FROM [dbo].[Em_Event]  where ID=@id";
            SqlParameter ids = new SqlParameter("@id", value: id);
            SqlDataAdapter eventDAT = new SqlDataAdapter(dfsdf, AppConn);

            DataSet ds = new DataSet();
            eventDAT.SelectCommand.Parameters.Add(ids);
            eventDAT.Fill(ds);
            AppConn.Close();
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