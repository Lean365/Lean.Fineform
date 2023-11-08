using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading;
using FineUIPro;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Collections;
namespace Lean.Fineform.Lf_Office.EM
{
    /// <summary>
    /// event_edit 的摘要说明
    /// </summary>
    public class event_edit :PageBase, IHttpHandler
    {

        public static SqlConnection AppConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ToString());
        public override void ProcessRequest(HttpContext context)
        {
            string attypename = System.Web.HttpUtility.UrlDecode(context.Request["attype"], Encoding.UTF8);//事件类别
            string attitle = System.Web.HttpUtility.UrlDecode(context.Request["title"], Encoding.UTF8);//标题
            string atcontent = System.Web.HttpUtility.UrlDecode(context.Request["description"], Encoding.UTF8);//内容

            string atsdate = System.Web.HttpUtility.UrlDecode(context.Request["start"], Encoding.UTF8);//开始时间
            string atedate = System.Web.HttpUtility.UrlDecode(context.Request["end"], Encoding.UTF8);//结束时间

            string id = context.Request["id"];
            //SqlConnection con = new SqlConnection(@"Password=tac26901333;Persist Security Info=True;User ID=sa;Initial Catalog=DocumentManagement;Data Source=.");

            string eventStr = "UPDATE [dbo].[Em_Event] SET   attitle = @attitle, atsdate = @atsdate, atedate =@atedate, atcontent =@atcontent, ModiUser = @ModiUser, ModiTime = @ModiTime where ID=@id ";

            SqlParameter ids = new SqlParameter("@id", value: id);

            SqlParameter attitles = new SqlParameter("@attitle", value: attitle);
            SqlParameter atcontents = new SqlParameter("@atcontent", value: atcontent);
            SqlParameter atsdates = new SqlParameter("@atsdate", value: atsdate);
            SqlParameter atedates = new SqlParameter("@atedate", value: atedate);
            SqlParameter ModiUsers = new SqlParameter("@ModiUser", value: GetIdentityName());
            SqlParameter ModiTimes = new SqlParameter("@ModiTime", value: DateTime.Now);
            SqlDataAdapter eventDAT = new SqlDataAdapter(eventStr, AppConn);
            eventDAT.SelectCommand.Parameters.Add(ids);

            eventDAT.SelectCommand.Parameters.Add(attitles);
            eventDAT.SelectCommand.Parameters.Add(atcontents);
            eventDAT.SelectCommand.Parameters.Add(atsdates);
            eventDAT.SelectCommand.Parameters.Add(atedates);

            eventDAT.SelectCommand.Parameters.Add(ModiUsers);
            eventDAT.SelectCommand.Parameters.Add(ModiTimes);
            DataSet ds = new DataSet();
            eventDAT.Fill(ds);
            AppConn.Close();

            //string id = ds.Tables[0].Rows[0]["id"].ToString();
            //context.Response.Write(id);
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