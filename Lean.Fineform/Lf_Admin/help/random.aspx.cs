using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
////using EntityFramework.Extensions;

namespace Lean.Fineform.Lf_Admin.help

{
    public partial class random : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSysView";
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Numtrand.Text = "0";
            //this.Txtrand.Text = "";
            Txtstr();

        }
        public void Txtstr()
        {
            Txtrandom1.Text = "";
            Txtrandom2.Text = "";
            Txtrandom3.Text = "";
            Txtrandom4.Text = "";
            Txtrandom5.Text = "";
            //Txtrandom6.Text = "";

            for (int i = 1; i < 25; i++)
            {
                if (Numtrand.Text == "")
                {
                    Txtrandom1.Text = Txtrandom1.Text + "\r\n" + GetRandomString(4, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom2.Text = Txtrandom2.Text + "\r\n" + GetRandomString(6, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom3.Text = Txtrandom3.Text + "\r\n" + GetRandomString(8, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom4.Text = Txtrandom4.Text + "\r\n" + GetRandomString(10, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom5.Text = Txtrandom5.Text + "\r\n" + GetRandomString(12, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    //Txtrandom6.Text = Txtrandom6.Text + "\r\n" + GetRandomString(14, Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);

                    //this.Txtrandom1.Text.Replace(" ", "").Replace("\r\n\r\n", "\r\n").Replace("\r\n\r\n", "\r\n");
                }
                else
                {

                    Txtrandom1.Text = Txtrandom1.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom2.Text = Txtrandom2.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom3.Text = Txtrandom3.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom4.Text = Txtrandom4.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    Txtrandom5.Text = Txtrandom5.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);
                    //Txtrandom6.Text = Txtrandom6.Text + "\r\n" + GetRandomString(int.Parse(Numtrand.Text), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(1), Convert.ToBoolean(0), Txtrand.Text);

                }

            }

        }
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }



        protected void Window1_Close(object sender, EventArgs e)
        {

        }

        protected void Btn1_Click(object sender, EventArgs e)
        {
            if (Numtrand.Text == "")
            {

                Alert.ShowInTop("请确认输入的位数大于0",MessageBoxIcon.Error);
                //Alert.ShowInTop("您所使用的登录ID已经在线了！您不能重复登录！");
                return;
            }
            else
            {
                Txtstr();
            }
        }

        protected void Btn2_Click(object sender, EventArgs e)
        {
            if (this.Txtrand.Text == "")
            {

                Alert.ShowInTop("请确认输入的特定字符不为空", MessageBoxIcon.Error);
                //Alert.ShowInTop("您所使用的登录ID已经在线了！您不能重复登录！");
                return;
            }
            else
            {
                Txtstr();
            }
        }

        protected void Btn3_Click(object sender, EventArgs e)
        {
            if (this.Txtrand.Text == "")
            {
                Alert.ShowInTop("请确认输入的特定字符不为空", MessageBoxIcon.Error);
                //Alert.ShowInTop("您所使用的登录ID已经在线了！您不能重复登录！");
                return;
            }
            else
            {
                if (Numtrand.Text == "")
                {
                    //Alert alert = new Alert();
                    //alert.Message = "请确认输入的位数大于0";
                    //alert.IconUrl = "~/Lf_Resources/images/key_stop.png";
                    //alert.Target = Target.Top;
                    Alert.ShowInTop("请确认输入的位数大于0", MessageBoxIcon.Error);
                    //Alert.ShowInTop("您所使用的登录ID已经在线了！您不能重复登录！");
                    return;
                }
                else
                {
                    Txtstr();
                }

            }
        }

    }
}