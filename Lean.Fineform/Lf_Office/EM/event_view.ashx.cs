using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace Lean.Fineform.Lf_Office.EM
{
    /// <summary>
    /// event_view 的摘要说明
    /// </summary>
    public class event_view : IHttpHandler
    {

        public static SqlConnection AppConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ToString());
        public void ProcessRequest(HttpContext context)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            Dictionary<string, string> drow = new Dictionary<string, string>();
            List<Dictionary<string, object>> gas = new List<Dictionary<string, object>>();
            string start = System.Web.HttpUtility.UrlDecode(context.Request["start"], Encoding.UTF8);

            string end = System.Web.HttpUtility.UrlDecode(context.Request["end"], Encoding.UTF8);
            //SqlConnection con = new SqlConnection(@"Password=tac26901333;Persist Security Info=True;User ID=sa;Initial Catalog=DocumentManagement;Data Source=.");



            string eventStr = "SELECT [ID],[atuser],[attypename],[attitle],[atcontent],[atallday],[atsdate]" +
                 ",[atedate],[aturl],[atclassname],[ateditable],[atsource]" +
                 ",[atcolor],[atbgcolor],[atbdcolor],[attextcolor],[Remark]" +
                 ",[GUID],[CreateUser],[CreateTime],[ModiUser],[ModiTime]" +
                 " FROM[dbo].[Em_Event] where @start<atsdate and atsdate< @end";


            SqlParameter starts = new SqlParameter("@start", value: "1900-01-01");

            SqlParameter ends = new SqlParameter("@end", value: end);
            SqlDataAdapter eventDAT = new SqlDataAdapter(eventStr, AppConn);
            eventDAT.SelectCommand.Parameters.Add(starts);
            eventDAT.SelectCommand.Parameters.Add(ends);
            DataSet eventDS = new DataSet();
            eventDAT.Fill(eventDS);
            AppConn.Close();
            for (int i = 0; i < eventDS.Tables[0].Rows.Count; i++)
            {
                Dictionary<string, object> drow2 = new Dictionary<string, object>();
                //事件ID删除用
                drow2.Add("id", eventDS.Tables[0].Rows[i]["ID"].ToString());
                //事件内容
                drow2.Add("title", eventDS.Tables[0].Rows[i]["attitle"].ToString());
                drow2.Add("description", eventDS.Tables[0].Rows[i]["atcontent"].ToString());
                //事件类别
                drow2.Add("attype", eventDS.Tables[0].Rows[i]["attypename"].ToString());
                //时间
                drow2.Add("start", eventDS.Tables[0].Rows[i]["atsdate"].ToString());
                drow2.Add("end", eventDS.Tables[0].Rows[i]["atedate"].ToString());
                //操作
                drow2.Add("editable", eventDS.Tables[0].Rows[i]["ateditable"].ToString());
                drow2.Add("allDay", eventDS.Tables[0].Rows[i]["atallday"]);
                //事件排序
                drow2.Add("eventOrder", eventDS.Tables[0].Rows[i]["atsdate"]);

                //事件颜色区分
                //drow2.Add("color", eventDS.Tables[0].Rows[i]["atcolor"].ToString());//DarkGreen 
                drow2.Add("backgroundColor", eventDS.Tables[0].Rows[i]["atcolor"].ToString());//DarkGreen
                drow2.Add("borderColor", eventDS.Tables[0].Rows[i]["atcolor"].ToString());//White 

                drow2.Add("textColor", eventDS.Tables[0].Rows[i]["attextcolor"].ToString());//GhostWhite  
                //drow2.Add("eventColor", eventDS.Tables[0].Rows[i]["atcolor"].ToString());
                //drow2.Add("eventTextColor", eventDS.Tables[0].Rows[i]["attextcolor"].ToString());

                gas.Add(drow2);

            }
            context.Response.Write(jss.Serialize(gas));


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