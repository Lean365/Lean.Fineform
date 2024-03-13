using System.Collections;

namespace LeanFine
{
    public class SsoHelper
    {
        /// <summary>
        ///
        /// Global文件的SessionEnd事件中增加此代码
        /// </summary>

        /**/

        /// <summary>
        /// 判断用户strUserID是否包含在Hashtable h中
        /// </summary>
        /// <param name="strUserID"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static bool AmIOnline(string strUserID, Hashtable h)
        {
            if (strUserID == null)
                return false;

            //继续判断是否该用户已经登陆
            if (h == null)
                return false;

            //判断哈希表中是否有该用户
            IDictionaryEnumerator e1 = h.GetEnumerator();
            bool flag = false;
            while (e1.MoveNext())
            {
                if (e1.Value.ToString().CompareTo(strUserID) == 0)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
    }
}