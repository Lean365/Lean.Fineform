using System;
using System.Web;
using System.Web.Security;
using System.Threading;
using System.Globalization;
using FineUIPro;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Fine
{
    public partial class _default : PageBase
    {
        #region Page_Load
        private string CultureLang = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //NetHelper.IsCanConnection("fs6", 8000, 5);

                //lb1.Text = Resources.Resource.LoginTitle;//后台代码中使用
                CultureLang = Session["PreferredCulture"].ToString().ToLower().Trim();
                switch (CultureLang)
                {
                    case "zh-cn":
                        this.DDLCulture.Items.FindByText("简体中文").Selected = true;
                        Session["PreferredCulture"] = "zh-cn";
                        break;
                    case "en-us":
                        this.DDLCulture.Items.FindByText("English").Selected = true;
                        Session["PreferredCulture"] = "en-us";
                        break;
                    case "ja-jp":
                        this.DDLCulture.Items.FindByText("日本語").Selected = true;
                        Session["PreferredCulture"] = "ja-jp";
                        break;
                    default:
                        this.DDLCulture.Items.FindByText("简体中文").Selected = true;
                        Session["PreferredCulture"] = "zh-cn";
                        break;
                }
                LoadData();
            }
        }

        private void LoadData()
        {
             tbxUserName.Text="admin";
            tbxPassword.Text= "admin";
            // 如果用户已经登录，则重定向到管理首页
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
            string productName = String.Format(GetAssemblyInfo.AssemblyProduct);
            string versionName= String.Format(" v{0}", GetProductVersion());
            Window1.Title = String.Format(global::Resources.GlobalResource.sys_SignIn);
            //tbText.Text = String.Format(productName + versionName);
            //Window1.Title = String.Format("LeanManufacturing v{0}", GetProductVersion());
            //tbxUserName.Text = "admin";
            //tbxPassword.Text = "admin";
        }

        #endregion

        #region Events

        /// <summary>
        /// 语言选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DDLCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = DDLCulture.SelectedItem.Value;
            Session["PreferredCulture"] = value;
            //重定向页面
            Response.Redirect(Request.Url.PathAndQuery);
        }
        /// <summary>
        /// 登录确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string userName = tbxUserName.Text.Trim().ToLower();
            string password = tbxPassword.Text.Trim();

            Adm_User user = DB.Adm_Users.Where(u => u.Name == userName).FirstOrDefault();
            if (SsoHelper.AmIOnline(userName, (Hashtable)Application["Online"]))
            {
                //Response.Write("<script language='javascript'>");
                //Response.Write("alert('您登录的ID已经在线了！您不能重复登录！');");
                //Response.Write("</script>");
                //PageContext.RegisterStartupScript(Window1.GetHideReference() +
                //            Confirm.GetShowReference("您所使用的登录ID已经在线了！您不能重复登录！", String.Empty, MessageBoxIcon.Question));
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Login_Repeatedly, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                return;
            }

            if (user != null)
            {
                if (PasswordUtil.ComparePasswords(user.Password, password))
                {
                    if (!user.Enabled)
                    {
                        Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_User_Invalid);
                    }
                    else
                    {
                        //GetIdentityName() = userName;
                        //Common.LoginName = user.ChineseName;
                        //记录登录用户sessionid
                        SetOnlineInfo(userName);
                        // 登录成功


                        //OperateNotes.Info(String.Format("登录成功：用户“{0}”", user.Name));
                        //登录日志写入
                        string strLevel = "Info";                        
                        string strUserID = userName;
                        string strUserName= user.ChineseName;
                        string strLogger = typeof(_default).ToString();
                        string strInfo=(String.Format("用户 - {0} - 登录成功", tbxUserName.Text));
                        SaveItem(strLevel, strLogger, strUserID, strUserName, strInfo);
                        LoginSuccess(user);

                        return;
                    }
                }
                else
                {
                    //登录日志写入
                    string strLevel = "Error";
                    string strUserID = userName;
                    string strUserName = user.ChineseName;
                    string strLogger = typeof(_default).ToString();
                    string strInfo = (String.Format("用户 - {0} - 用户名或密码错误", tbxUserName.Text));
                    SaveItem(strLevel, strLogger, strUserID, strUserName, strInfo);


                    //OperateNotes.Warn(String.Format("登录失败：用户“{0}”密码错误", userName));
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Login_Error);
                    return;
                }

            }
            else
            {
                //登录日志写入

                string strLevel = "Error";
                string strUserID = userName;
                string strUserName = user.ChineseName;
                string strLogger = typeof(_default).ToString();
                string strInfo = (String.Format("用户 - {0} - 用户名或密码错误", tbxUserName.Text));
                SaveItem(strLevel, strLogger, strUserID, strUserName, strInfo);
                //OperateNotes.Warn(String.Format("登录失败：用户“{0}”不存在", userName));
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Login_Error);
                return;
            }

        }

        private void SaveItem(string Level,string Logger, string uid,string uname,string message)//登录成功日志
        {

            Adm_Log item = new Adm_Log();
            item.Date = DateTime.Now;
            //item.Prolineclass = prolinename.SelectedValue.ToString();
            item.Thread = "110";
            item.Level = Level;
            item.Logger = Logger;
            item.UserID = uid;

            item.UserName= uname;
            // 添加所有用户


            item.Message = message;
            item.Exception = "";
            DB.Adm_Logs.Add(item);
            DB.SaveChanges();


        }
        /// <summary>
        /// //获取当前项目存储的登录用户sessionid与用户id
        /// </summary>
        /// <param name="context"></param>
        /// <param name="username"></param>
        private void SetOnlineInfo(string username)
        {
            Hashtable hOnline = (Hashtable)Application["Online"];//读取全局变量
            if (hOnline != null)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                string strKey = "";
                while (idE.MoveNext())
                {
                    if (idE.Value != null && idE.Value.ToString().Equals(username))//如果当前用户已经登录，
                    {
                        //already login            
                        strKey = idE.Key.ToString();
                        hOnline[strKey] = "LeanBenchXX";//将当前用户已经在全局变量中的值设置为XX
                        break;
                    }
                }
            }
            else
            {
                hOnline = new Hashtable();
            }

            hOnline[Session.SessionID] = username;//初始化当前用户的
            Application.Lock();
            Application["Online"] = hOnline;
            Application.UnLock();
        }

        /// <summary>
        /// 注册在线用户
        /// </summary>
        /// <param name="user"></param>
        private void LoginSuccess(Adm_User user)
        {
            RegisterOnlineUser(user);

            // 用户所属的角色字符串，以逗号分隔
            string roleIDs = String.Empty;
            if (user.Roles != null)
            {
                roleIDs = String.Join(",", user.Roles.Select(r => r.ID).ToArray());
            }

            bool isPersistent = false;
            DateTime expiration = DateTime.Now.AddMinutes(120);
            CreateFormsAuthenticationTicket(user.Name, roleIDs, isPersistent, expiration);

            // 重定向到登陆后首页
            Response.Redirect(FormsAuthentication.DefaultUrl);
        }


        #endregion


    }
}
