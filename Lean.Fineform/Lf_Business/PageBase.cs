using FineUIPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LeanFine
{
    public class PageBase : System.Web.UI.Page
    {
        #region 只读静态变量

        // Session key
        private static readonly string SK_ONLINE_UPDATE_TIME = "OnlineUpdateTime";

        //private static readonly string SK_USER_ROLE_ID = "UserRoleId";

        private static readonly string CHECK_POWER_FAIL_PAGE_MESSAGE = "您无权访问此页面！";
        private static readonly string CHECK_POWER_FAIL_ACTION_MESSAGE = "您无权进行此操作！";

        #endregion 只读静态变量

        #region 上传文件类型判断

        protected static readonly List<string> VALID_FILE_TYPES = new List<string> { "jpg", "png", "pdf", "doc", "xls", "ppt", "docx", "pptx", "xlsx" };

        protected static bool ValidateFileType(string fileName)
        {
            string fileType = String.Empty;
            int lastDotIndex = fileName.LastIndexOf(".");
            if (lastDotIndex >= 0)
            {
                fileType = fileName.Substring(lastDotIndex + 1).ToLower();
            }

            if (VALID_FILE_TYPES.Contains(fileType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion 上传文件类型判断

        #region 判断文件大小

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected static long GetFileSize(string filePath)
        {
            long temp = 0;
            if (!String.IsNullOrEmpty(filePath))
            {
                //判断当前路径是否指向某个文件
                if (!File.Exists(filePath))
                {
                    string[] strs = Directory.GetFileSystemEntries(filePath);
                    foreach (string item in strs)
                    {
                        temp += GetFileSize(item);
                    }
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    return fileInfo.Length;
                }
            }
            return temp;
        }

        #endregion 判断文件大小

        #region 实体上下文

        public static LeanFineContext DB
        {
            get
            {
                // http://stackoverflow.com/questions/6334592/one-dbcontext-per-request-in-asp-net-mvc-without-ioc-container
                if (!HttpContext.Current.Items.Contains("__Lean365NetContext"))
                {
                    HttpContext.Current.Items["__Lean365NetContext"] = new LeanFineContext();
                }
                return HttpContext.Current.Items["__Lean365NetContext"] as LeanFineContext;
            }
        }

        #endregion 实体上下文

        #region 浏览权限

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public virtual string ViewPower
        {
            get
            {
                return String.Empty;
            }
        }

        #endregion 浏览权限

        #region 页面初始化

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // 此用户是否有访问此页面的权限
            if (!CheckPowerView())
            {
                CheckPowerFailWithPage();
                return;
            }

            // 设置主题
            if (PageManager.Instance != null)
            {
                var pm = PageManager.Instance;
                var themeValue = ConfigHelper.Theme;
                // 是否为内置主题
                if (IsSystemTheme(themeValue))
                {
                    pm.CustomTheme = String.Empty;
                    pm.Theme = (Theme)Enum.Parse(typeof(Theme), themeValue, true);
                }
                else
                {
                    pm.CustomTheme = themeValue;
                }
            }

            UpdateOnlineUser(User.Identity.Name);
            CheckOtherLogin();
            // 设置页面标题
            Page.Title = ConfigHelper.Title;

            // 禁用表单的自动完成功能
            Form.Attributes["autocomplete"] = "off";
        }

        private bool IsSystemTheme(string themeName)
        {
            themeName = themeName.ToLower();
            string[] themes = Enum.GetNames(typeof(Theme));
            foreach (string theme in themes)
            {
                if (theme.ToLower() == themeName)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion 页面初始化

        #region 请求参数

        /// <summary>
        /// 获取查询字符串中的参数值
        /// </summary>
        protected string GetQueryValue(string queryKey)
        {
            return Request.QueryString[queryKey];
        }

        /// <summary>
        /// 获取查询字符串中的参数值
        /// </summary>
        protected int GetQueryIntValue(string queryKey)
        {
            int queryIntValue = -1;
            try
            {
                queryIntValue = Convert.ToInt32(Request.QueryString[queryKey]);
            }
            catch (Exception)
            {
                // TODO
            }

            return queryIntValue;
        }

        #endregion 请求参数

        #region 表格相关

        //查询ID
        protected int GetSelectedDataKeyID(Grid grid)
        {
            int id = -1;
            int rowIndex = grid.SelectedRowIndex;
            if (rowIndex >= 0)
            {
                id = Convert.ToInt32(grid.DataKeys[rowIndex][0]);
            }
            return id;
        }

        //查询GUID
        protected string GetSelectedDataKeyGUID(Grid grid)
        {
            string guid = "";
            int rowIndex = grid.SelectedRowIndex;
            if (rowIndex >= 0)
            {
                guid = grid.DataKeys[rowIndex][0].ToString();
            }
            return guid;
        }

        protected string GetSelectedDataKey(Grid grid, int dataIndex)
        {
            string data = String.Empty;
            int rowIndex = grid.SelectedRowIndex;
            if (rowIndex >= 0)
            {
                data = grid.DataKeys[rowIndex][dataIndex].ToString();
            }
            return data;
        }

        /// <summary>
        /// 获取表格选中项DataKeys的第一个值，并转化为整型列表
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        protected List<int> GetSelectedDataKeyIDs(Grid grid)
        {
            List<int> ids = new List<int>();
            foreach (int rowIndex in grid.SelectedRowIndexArray)
            {
                ids.Add(Convert.ToInt32(grid.DataKeys[rowIndex][0]));
            }

            return ids;
        }

        protected List<string> GetSelectedDataKeyGUIDs(Grid grid)
        {
            List<string> ids = new List<string>();
            foreach (int rowIndex in grid.SelectedRowIndexArray)
            {
                ids.Add((grid.DataKeys[rowIndex][0].ToString()));
            }

            return ids;
        }

        #endregion 表格相关

        #region EF相关

        // 排序
        protected IQueryable<T> Sort<T>(IQueryable<T> q, FineUIPro.Grid grid)
        {
            return q.SortBy(grid.SortField + " " + grid.SortDirection);
        }

        // 排序后分页
        protected IQueryable<T> SortAndPage<T>(IQueryable<T> q, FineUIPro.Grid grid)
        {
            if (grid.PageIndex >= grid.PageCount && grid.PageCount >= 1)
            {
                grid.PageIndex = grid.PageCount - 1;
            }

            return Sort(q, grid).Skip(grid.PageIndex * grid.PageSize).Take(grid.PageSize);
        }

        // 附加实体到数据库上下文中（首先在Local中查找实体是否存在，不存在才Attach，否则会报错）
        // http://patrickdesjardins.com/blog/entity-framework-4-3-an-object-with-the-same-key-already-exists-in-the-objectstatemanager
        protected T Attach<T>(int keyID) where T : class, IKeyID, new()
        {
            T t = DB.Set<T>().Local.Where(x => x.ID == keyID).FirstOrDefault();
            if (t == null)
            {
                t = new T { ID = keyID };
                DB.Set<T>().Attach(t);
            }
            return t;
        }

        // 向现有实体集合中添加新项
        protected void AddEntities<T>(ICollection<T> existItems, int[] newItemIDs) where T : class, IKeyID, new()
        {
            foreach (int roleID in newItemIDs)
            {
                T t = Attach<T>(roleID);
                existItems.Add(t);
            }
        }

        // 替换现有实体集合中的所有项
        // http://stackoverflow.com/questions/2789113/entity-framework-update-entity-along-with-child-entities-add-update-as-necessar
        protected void ReplaceEntities<T>(ICollection<T> existEntities, int[] newEntityIDs) where T : class, IKeyID, new()
        {
            if (newEntityIDs.Length == 0)
            {
                existEntities.Clear();
            }
            else
            {
                int[] tobeAdded = newEntityIDs.Except(existEntities.Select(x => x.ID)).ToArray();
                int[] tobeRemoved = existEntities.Select(x => x.ID).Except(newEntityIDs).ToArray();

                AddEntities<T>(existEntities, tobeAdded);

                existEntities.Where(x => tobeRemoved.Contains(x.ID)).ToList().ForEach(e => existEntities.Remove(e));
                //foreach (int roleID in tobeRemoved)
                //{
                //    existEntities.Remove(existEntities.Single(r => r.ID == roleID));
                //}
            }
        }

        // http://patrickdesjardins.com/blog/validation-failed-for-one-or-more-entities-see-entityvalidationerrors-property-for-more-details-2
        // ((System.Data.Entity.Validation.DbEntityValidationException)$exception).EntityValidationErrors

        #endregion EF相关

        #region 在线用户相关

        protected void UpdateOnlineUser(string username)
        {
            DateTime now = DateTime.Now;
            object lastUpdateTime = Session[SK_ONLINE_UPDATE_TIME];
            if (lastUpdateTime == null || (Convert.ToDateTime(lastUpdateTime).Subtract(now).TotalMinutes > 5))
            {
                // 记录本次更新时间
                Session[SK_ONLINE_UPDATE_TIME] = now;

                Adm_Online online = DB.Adm_Onlines.Where(o => o.User.Name == username).FirstOrDefault();
                if (online != null)
                {
                    online.UpdateTime = now;
                    DB.SaveChanges();
                }
            }
        }

        protected void RegisterOnlineUser(Adm_User user)
        {
            Adm_Online online = DB.Adm_Onlines.Where(o => o.User.ID == user.ID).FirstOrDefault();

            // 如果不存在，就创建一条新的记录
            if (online == null)
            {
                online = new Adm_Online();
                DB.Adm_Onlines.Add(online);
            }
            DateTime now = DateTime.Now;
            online.User = user;
            online.IPAdddress = NetHelper.GetIP4Address();// Request.UserHostAddress;
            online.LoginTime = now;
            online.UpdateTime = now;
            online.LoginNum = online.LoginNum + 1;

            DB.SaveChanges();

            // 记录本次更新时间
            Session[SK_ONLINE_UPDATE_TIME] = now;
        }

        /// <summary>
        /// 在线人数
        /// </summary>
        /// <returns></returns>
        protected int GetOnlineCount()
        {
            DateTime lastM = DateTime.Now.AddMinutes(-15);
            return DB.Adm_Onlines.Where(o => o.UpdateTime > lastM).Count();
        }

        #endregion 在线用户相关

        #region 当前登录用户信息

        // http://blog.163.com/zjlovety@126/blog/static/224186242010070024282/
        // http://www.cnblogs.com/gaoshuai/articles/1863231.html
        /// <summary>
        /// 当前登录用户的角色列表
        /// </summary>
        /// <returns></returns>
        protected List<int> GetIdentityRoleIDs()
        {
            List<int> roleIDs = new List<int>();

            if (User.Identity.IsAuthenticated)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)User.Identity).Ticket;
                string userData = ticket.UserData;

                foreach (string roleID in userData.Split(','))
                {
                    if (!String.IsNullOrEmpty(roleID))
                    {
                        roleIDs.Add(Convert.ToInt32(roleID));
                    }
                }
            }

            return roleIDs;
        }

        /// <summary>
        /// 当前登录用户名
        /// </summary>
        /// <returns></returns>
        protected string GetIdentityName()
        {
            if (User.Identity.IsAuthenticated)
            {
                return User.Identity.Name;
            }
            return String.Empty;
        }

        /// <summary>
        /// 创建表单验证的票证并存储在客户端Cookie中
        /// </summary>
        /// <param name="userName">当前登录用户名</param>
        /// <param name="roleIDs">当前登录用户的角色ID列表</param>
        /// <param name="isPersistent">是否跨浏览器会话保存票证</param>
        /// <param name="expiration">过期时间</param>
        protected void CreateFormsAuthenticationTicket(string userName, string roleIDs, bool isPersistent, DateTime expiration)
        {
            // 创建Forms身份验证票据
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                userName,                       // 与票证关联的用户名
                DateTime.Now,                   // 票证发出时间
                expiration,                     // 票证过期时间
                isPersistent,                   // 如果票证将存储在持久性 Cookie 中（跨浏览器会话保存），则为 true；否则为 false。
                roleIDs                         // 存储在票证中的用户特定的数据
             );

            // 对Forms身份验证票据进行加密，然后保存到客户端Cookie中
            string hashTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashTicket);
            cookie.HttpOnly = true;
            // 1. 关闭浏览器即删除（Session Cookie）：DateTime.MinValue
            // 2. 指定时间后删除：大于 DateTime.Now 的某个值
            // 3. 删除Cookie：小于 DateTime.Now 的某个值
            if (isPersistent)
            {
                cookie.Expires = expiration;
            }
            else
            {
                cookie.Expires = DateTime.MinValue;
            }
            Response.Cookies.Add(cookie);
        }

        #endregion 当前登录用户信息

        #region 权限检查

        /// <summary>
        /// 检查当前用户是否拥有当前页面的浏览权限
        /// 页面需要先定义ViewPower属性，以确定页面与某个浏览权限的对应关系
        /// </summary>
        /// <returns></returns>
        protected bool CheckPowerView()
        {
            return CheckPower(ViewPower);
        }

        /// <summary>
        /// 检查当前用户是否拥有某个权限
        /// </summary>
        /// <param name="powerType"></param>
        /// <returns></returns>
        protected bool CheckPower(string powerName)
        {
            // 如果权限名为空，则放行
            if (String.IsNullOrEmpty(powerName))
            {
                return true;
            }

            // 当前登陆用户的权限列表
            List<string> rolePowerNames = GetRolePowerNames();
            if (rolePowerNames.Contains(powerName))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取当前登录用户拥有的全部权限列表
        /// </summary>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        protected List<string> GetRolePowerNames()
        {
            // 将用户拥有的权限列表保存在Session中，这样就避免每个请求多次查询数据库
            if (Session["UserPowerList"] == null)
            {
                List<string> rolePowerNames = new List<string>();

                // 超级管理员拥有所有权限
                if (GetIdentityName() == "admin")
                {
                    rolePowerNames = DB.Adm_Powers.Select(p => p.Name).ToList();
                }
                else
                {
                    List<int> roleIDs = GetIdentityRoleIDs();

                    foreach (var role in DB.Adm_Roles.Include(r => r.Powers).Where(r => roleIDs.Contains(r.ID)))
                    {
                        foreach (var power in role.Powers)
                        {
                            if (!rolePowerNames.Contains(power.Name))
                            {
                                rolePowerNames.Add(power.Name);
                            }
                        }
                    }
                }

                Session["UserPowerList"] = rolePowerNames;
            }
            return (List<string>)Session["UserPowerList"];
        }

        #endregion 权限检查

        #region 权限相关

        protected void CheckPowerFailWithPage()
        {
            Response.Write(CHECK_POWER_FAIL_PAGE_MESSAGE);
            Response.End();
        }

        protected void CheckPowerFailWithButton(FineUIPro.Button btn)
        {
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithLinkButtonField(FineUIPro.Grid grid, string columnID)
        {
            FineUIPro.LinkButtonField btn = grid.FindColumn(columnID) as FineUIPro.LinkButtonField;
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithWindowField(FineUIPro.Grid grid, string columnID)
        {
            FineUIPro.WindowField btn = grid.FindColumn(columnID) as FineUIPro.WindowField;
            btn.Enabled = false;
            btn.ToolTip = CHECK_POWER_FAIL_ACTION_MESSAGE;
        }

        protected void CheckPowerFailWithAlert()
        {
            PageContext.RegisterStartupScript(Alert.GetShowInTopReference(CHECK_POWER_FAIL_ACTION_MESSAGE));
        }

        protected void CheckPowerWithButton(string powerName, FineUIPro.Button btn)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithButton(btn);
            }
        }

        protected void CheckPowerWithLinkButtonField(string powerName, FineUIPro.Grid grid, string columnID)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithLinkButtonField(grid, columnID);
            }
        }

        protected void CheckPowerWithWindowField(string powerName, FineUIPro.Grid grid, string columnID)
        {
            if (!CheckPower(powerName))
            {
                CheckPowerFailWithWindowField(grid, columnID);
            }
        }

        /// <summary>
        /// 为删除Grid中选中项的按钮添加提示信息
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="grid"></param>
        protected void ResolveDeleteButtonForGrid(FineUIPro.Button btn, Grid grid)
        {
            ResolveDeleteButtonForGrid(btn, grid, "确定要删除选中的{0}项记录吗？");
        }

        protected void ResolveDeleteButtonForGrid(FineUIPro.Button btn, Grid grid, string confirmTemplate)
        {
            ResolveDeleteButtonForGrid(btn, grid, "请至少应该选择一项记录！", confirmTemplate);
        }

        protected void ResolveDeleteButtonForGrid(FineUIPro.Button btn, Grid grid, string noSelectionMessage, string confirmTemplate)
        {
            // 点击删除按钮时，至少选中一项
            btn.OnClientClick = grid.GetNoSelectionAlertInParentReference(noSelectionMessage);
            btn.ConfirmText = String.Format(confirmTemplate, "&nbsp;<span class=\"highlight\"><script>" + grid.GetSelectedCountReference() + "</script></span>&nbsp;");
            btn.ConfirmTarget = Target.Top;
        }

        #endregion 权限相关

        #region 产品版本

        public string GetProductVersion()
        {
            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            return String.Format("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
        }

        #endregion 产品版本

        #region 模拟树的下拉列表

        protected List<T> ResolveDDL<T>(List<T> mys) where T : Adm_ICustomTree, ICloneable, IKeyID, new()
        {
            return ResolveDDL<T>(mys, -1, true);
        }

        protected List<T> ResolveDDL<T>(List<T> mys, int currentId) where T : Adm_ICustomTree, ICloneable, IKeyID, new()
        {
            return ResolveDDL<T>(mys, currentId, true);
        }

        // 将一个树型结构放在一个下列列表中可供选择
        protected List<T> ResolveDDL<T>(List<T> source, int currentID, bool addRootNode) where T : Adm_ICustomTree, ICloneable, IKeyID, new()
        {
            List<T> result = new List<T>();

            if (addRootNode)
            {
                // 添加根节点
                T root = new T();
                root.Name = "--根节点--";
                root.ID = -1;
                root.TreeLevel = 0;
                root.Enabled = true;
                result.Add(root);
            }

            foreach (T item in source)
            {
                T newT = (T)item.Clone();
                result.Add(newT);

                // 所有节点的TreeLevel加一
                if (addRootNode)
                {
                    newT.TreeLevel++;
                }
            }

            // currentId==-1表示当前节点不存在
            if (currentID != -1)
            {
                // 本节点不可点击（也就是说当前节点不可能是当前节点的父节点）
                // 并且本节点的所有子节点也不可点击，你想如果当前节点跑到子节点的子节点，那么这些子节点就从树上消失了
                bool startChileNode = false;
                int startTreeLevel = 0;
                foreach (T my in result)
                {
                    if (my.ID == currentID)
                    {
                        startTreeLevel = my.TreeLevel;
                        my.Enabled = false;
                        startChileNode = true;
                    }
                    else
                    {
                        if (startChileNode)
                        {
                            if (my.TreeLevel > startTreeLevel)
                            {
                                my.Enabled = false;
                            }
                            else
                            {
                                startChileNode = false;
                            }
                        }
                    }
                }
            }

            return result;
        }

        #endregion 模拟树的下拉列表

        #region 日志记录

        protected void LogInfo(string message)
        {
            DB.Adm_Logs.Add(new Adm_Log
            {
                Level = "Info",
                Message = message,
                Date = DateTime.Now
            });
            DB.SaveChanges();
        }

        #endregion 日志记录

        /// <summary>
        /// 多语言选项
        /// </summary>
        protected override void InitializeCulture()
        {
            // 用Session来存储语言信息
            if (Session["PreferredCulture"] == null) { Session["PreferredCulture"] = Request.UserLanguages[0]; }
            string UserCulture = Session["PreferredCulture"].ToString();
            if (UserCulture != "")
            {
                //根据Session的值重新绑定语言代码
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(UserCulture);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(UserCulture);
            }
        }

        /// <summary>
        /// 检查用户重复登录SSO
        /// </summary>
        /// <param name="context"></param>
        protected void CheckOtherLogin()
        {
            Hashtable hOnline = (Hashtable)Application["Online"];//获取已经存储的application值
            if (hOnline != null)
            {
                IDictionaryEnumerator idE = hOnline.GetEnumerator();
                while (idE.MoveNext())
                {
                    if (idE.Key != null && idE.Key.ToString().Equals(Session.SessionID))
                    {
                        //already login
                        if (idE.Value != null && "CubeXX".Equals(idE.Value.ToString()))//说明在别处登录
                        {
                            hOnline.Remove(Session.SessionID);
                            Application.Lock();
                            Application["Online"] = hOnline;
                            Application.UnLock();
                            Response.Write("<script>alert('你的帐号已在别处登陆，你被强迫下线！');window.location.href='./default.aspx';</script>");//退出当前到登录页面
                            Response.End();
                        }
                    }
                }
            }
        }
    }
}