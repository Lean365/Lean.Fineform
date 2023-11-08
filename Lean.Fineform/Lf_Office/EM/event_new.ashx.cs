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
    /// event_new 的摘要说明
    /// </summary>
    public class event_new : PageBase,IHttpHandler
    {
        string id, attypename, atuser, attitle, atcontent, atsdate, atcolor, atedate, atbgcolor, atbdcolor, attextcolor, GUID, CreateUser, remark;
        DateTime CreateTime;
        public static SqlConnection AppConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ToString());
        public override void ProcessRequest(HttpContext context)
        {
            
            attypename = System.Web.HttpUtility.UrlDecode(context.Request["attype"], Encoding.UTF8);//事件类别
            attitle = System.Web.HttpUtility.UrlDecode(context.Request["title"], Encoding.UTF8);//标题
            atcontent = System.Web.HttpUtility.UrlDecode(context.Request["description"], Encoding.UTF8);//内容

            atsdate = System.Web.HttpUtility.UrlDecode(context.Request["start"], Encoding.UTF8);//开始时间
            atedate = System.Web.HttpUtility.UrlDecode(context.Request["end"], Encoding.UTF8);//结束时间

            if (attypename == "1")
            {
                atuser = GetIdentityName();
                atcolor = "#0066CC";
                atbgcolor = "#0066CC";
                atbdcolor = "#FFFFFF";
                attextcolor = "#FFFFE0";
                remark = "公开日程";
                GUID = Guid.NewGuid().ToString();
                CreateUser = GetIdentityName();
                CreateTime = DateTime.Now;
            }
            else
            {
                atuser = GetIdentityName();
                atcolor = "#00A600";
                atbgcolor = "#00A600";
                atbdcolor = "#FFFFFF";
                attextcolor = "#F5F5F5";
                remark = "个人日程";
                GUID = Guid.NewGuid().ToString();
                CreateUser =GetIdentityName();
                CreateTime = DateTime.Now;
            }
            AddEvent();
            context.Response.Write(id);
            //context.Response.Write("事件添加成功！");


        }
        #region 判断某个日期是否在某段日期范围内
        /// <summary> 
        /// 判断某个日期是否在某段日期范围内，返回布尔值
        /// </summary> 
        /// <param name="dt">要判断的日期</param> 
        /// <param name="dt1">开始日期</param> 
        /// <param name="dt2">结束日期</param> 
        /// <returns></returns>  
        private bool IsInDate(DateTime dt, DateTime dt1, DateTime dt2)
        {
            return dt.CompareTo(dt1) >= 0 && dt.CompareTo(dt2) <= 0;
        }
        #endregion

        public void AddEvent()
        {
            if (attitle != "")
            {
                if (atcontent != "")
                {
                    if (atsdate != "")
                    {
                        if (atedate != "")
                        {
                            //SqlConnection con = new SqlConnection(@"Password=tac26901333;Persist Security Info=True;User ID=sa;Initial Catalog=DocumentManagement;Data Source=.");
                            string eventStr = "INSERT INTO  [dbo].[Em_Event]" +
                "(atuser,attypename,attitle,atcontent,atallday, atsdate, atedate, aturl, atclassname, ateditable," +
                "atsource, atcolor, atbgcolor, atbdcolor, attextcolor, Remark, GUID, CreateUser, CreateTime)" +
                "VALUES(@atuser, @attypename, @attitle, @atcontent ,0,@atsdate ," +
                "@atedate ,'','',0,'',@atcolor, @atbgcolor, @atbdcolor, @attextcolor, @remark, @GUID, @CreateUser, @CreateTime );select SCOPE_IDENTITY() as id";


                            SqlParameter atusers = new SqlParameter("@atuser", value: atuser);
                            SqlParameter attypenames = new SqlParameter("@attypename", value: attypename);
                            SqlParameter attitles = new SqlParameter("@attitle", value: attitle);
                            SqlParameter atcontents = new SqlParameter("@atcontent", value: atcontent);
                            SqlParameter atsdates = new SqlParameter("@atsdate", value: atsdate);
                            SqlParameter atedates = new SqlParameter("@atedate", value: atedate);
                            SqlParameter atcolors = new SqlParameter("@atcolor", value: atcolor);
                            SqlParameter atbgcolors = new SqlParameter("@atbgcolor", value: atbgcolor);
                            SqlParameter atbdcolors = new SqlParameter("@atbdcolor", value: atbdcolor);
                            SqlParameter attextcolors = new SqlParameter("@attextcolor", value: attextcolor);
                            SqlParameter remarks = new SqlParameter("@remark", value: remark);
                            SqlParameter GUIDs = new SqlParameter("@GUID", value: GUID);
                            SqlParameter CreateUsers = new SqlParameter("@CreateUser", value: CreateUser);
                            SqlParameter CreateTimes = new SqlParameter("@CreateTime", value: CreateTime);


                            SqlDataAdapter eventDAT = new SqlDataAdapter(@eventStr, AppConn);
                            eventDAT.SelectCommand.Parameters.Add(atusers);
                            eventDAT.SelectCommand.Parameters.Add(attypenames);
                            eventDAT.SelectCommand.Parameters.Add(attitles);
                            eventDAT.SelectCommand.Parameters.Add(atcontents);
                            eventDAT.SelectCommand.Parameters.Add(atsdates);
                            eventDAT.SelectCommand.Parameters.Add(atedates);
                            eventDAT.SelectCommand.Parameters.Add(atcolors);
                            eventDAT.SelectCommand.Parameters.Add(atbgcolors);
                            eventDAT.SelectCommand.Parameters.Add(atbdcolors);
                            eventDAT.SelectCommand.Parameters.Add(attextcolors);
                            eventDAT.SelectCommand.Parameters.Add(remarks);
                            eventDAT.SelectCommand.Parameters.Add(GUIDs);
                            eventDAT.SelectCommand.Parameters.Add(CreateUsers);
                            eventDAT.SelectCommand.Parameters.Add(CreateTimes);


                            DataSet eventDS = new DataSet();
                            eventDAT.Fill(eventDS);
                            AppConn.Close();
                            id = eventDS.Tables[0].Rows[0]["id"].ToString();
                        }
                    }
                }
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