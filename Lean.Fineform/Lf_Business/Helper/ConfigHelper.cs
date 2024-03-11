using System;
using System.Collections.Generic;
using System.Web;
using FineUIPro;
using System.Linq;

namespace Fine
{
    public class ConfigHelper
    {
        #region fields & constructor

        private static List<Adm_Config> _configs;

        private static List<String> changedKeys = new List<string>();

        public static List<Adm_Config> Configs
        {
            get
            {
                if (_configs == null)
                {
                    InitConfigs();
                }
                return _configs;
            }
        }

        public static void Reload()
        {
            _configs = null;
        }

        public static void InitConfigs()
        {
            try
            {
                _configs = PageBase.DB.Adm_Configs.ToList();
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);

            }
        }

        #endregion

        #region methods

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValue(string key)
        {

                return Configs.Where(c => c.ConfigKey == key).Select(c => c.ConfigValue).FirstOrDefault();

        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, string value)
        {
            Adm_Config config = Configs.Where(c => c.ConfigKey == key).FirstOrDefault();
            if (config != null)
            {
                if (config.ConfigValue != value)
                {
                    changedKeys.Add(key);
                    config.ConfigValue = value;
                }
            }
        }

        /// <summary>
        /// 保存所有更改的配置项
        /// </summary>
        public static void SaveAll()
        {
            var changedConfigs = PageBase.DB.Adm_Configs.Where(c => changedKeys.Contains(c.ConfigKey));
            foreach (var changed in changedConfigs)
            {
                changed.ConfigValue = GetValue(changed.ConfigKey);
            }

            PageBase.DB.SaveChanges();

            Reload();
        }

        #endregion

        #region properties

        /// <summary>
        /// 网站标题
        /// </summary>
        public static string Title
        {

            get
            {
                return GetValue("Title");
            }
            set
            {
                SetValue("Title", value);
            }
        }

        /// <summary>
        /// 列表每页显示的个数
        /// </summary>
        public static int PageSize
        {
            get
            {
                return Convert.ToInt32(GetValue("PageSize"));
            }
            set
            {
                SetValue("PageSize", value.ToString());
            }
        }

        /// <summary>
        /// 帮助下拉列表
        /// </summary>
        public static string HelpList
        {
            get
            {
                return GetValue("HelpList");
            }
            set
            {
                SetValue("HelpList", value);
            }
        }


        /// <summary>
        /// 菜单样式
        /// </summary>
        public static string MenuType
        {
            get
            {
                return GetValue("MenuType");
            }
            set
            {
                SetValue("MenuType", value);
            }
        }


        /// <summary>
        /// 网站主题
        /// </summary>
        public static string Theme
        {
            get
            {
                return GetValue("Theme");
            }
            set
            {
                SetValue("Theme", value);
            }
        }


        #endregion
    }
}
