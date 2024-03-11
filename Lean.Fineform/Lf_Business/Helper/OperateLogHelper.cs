using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections;


namespace Fine
{
    public class OperateLogHelper : PageBase
    {
        

        #region Insert Login log
        //public static void InsertLoginLog(string UserID,string UserName,string Thread,string Level,)
        //{
        //    Adm_Log item = new Adm_Log();

        //}

        #endregion
        #region NetOperateNotes

        /// <summary>
        /// 写入操作日志
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="ip"></param>
        /// <param name="mo"></param>
        /// <param name="po"></param>
        /// <param name="lo"></param>
        /// <param name="lt"></param>
        public static void InsNetOperateNotes(string OperateUserName,string OperateType, string OperateModules, string OperatePowers, string OperateNotes)
        {
            try
            {
                Adm_OperateLog item = new Adm_OperateLog();
               
                item.OperateUserId = OperateUserName;
                //查询用户所在部门
                var q = (from a in DB.Adm_Depts
                         join b in DB.Adm_Users on a.ID equals b.Dept.ID
                         where b.Name == OperateUserName
                         select new
                         {
                             b.ChineseName,
                             a.Name,
                         }).ToList();
                if (q.Any())
                {
                    item.OperateUserName = q[0].ChineseName;
                    item.OperateUserDept = q[0].Name;
                }

                else
                {
                    item.OperateUserName = "-";
                    item.OperateUserDept = "-";
                }

                item.HostName = NetHelper.LocalHostName;
                item.HostIpAddress = NetHelper.GetIP4Address();

                item.HostBrowser = NetHelper.GetBrowserInfo();
                item.OperateModules = OperateModules;
                item.OperatePowers = OperatePowers;
                item.OperateType = OperateType;
                item.OperateNotes = OperateNotes;
                item.OperateTime = DateTime.Now;
                item.GUID = Guid.NewGuid();
                DB.Adm_OperateLogs.Add(item);
                DB.SaveChanges();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("空参数传递(err:null):" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("使用无效的类:" + Message);
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                //var errorMessages = ex.EntityValidationErrors
                //        .SelectMany(x => x.ValidationErrors)
                //        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                //var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                //var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                //throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

                //判断字段赋值
                var msg = string.Empty;
                var errors = (from u in ex.EntityValidationErrors select u.ValidationErrors).ToList();
                foreach (var erroritem in errors)
                    msg += erroritem.FirstOrDefault().ErrorMessage;
                Alert.ShowInTop("实体验证失败,赋值有异常:" + msg);
            }
        }
        #endregion
    }
}