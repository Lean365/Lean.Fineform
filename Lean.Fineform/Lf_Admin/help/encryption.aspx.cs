using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
//using EntityFramework.Extensions;

namespace Fine.Lf_Admin.help

{
    public partial class encryption : PageBase
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea1.Text))
            {
                TextArea2.Text = EncryptHelper.AES_Encrypt(TextArea1.Text, "PmsCyB6u42Y3KpcuPmsCyB6u42Y3KpcuPmsCyB6u42Y3", "PmsCyB6u42Y3Kpcu");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)

        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.AES_Decrypt(TextArea2.Text, "PmsCyB6u42Y3KpcuPmsCyB6u42Y3KpcuPmsCyB6u42Y3", "PmsCyB6u42Y3Kpcu");
            }
        }

        protected void Button18_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea1.Text))
            {
                TextArea2.Text = EncryptHelper.EncodeBase64(TextArea1.Text);
            }
        }

        protected void Button19_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.DecodeBase64(TextArea2.Text);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea1.Text))
            {
                TextArea2.Text = EncryptHelper.EncryptDES(TextArea1.Text, "12345678");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.DecryptDES(TextArea2.Text, "12345678");
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea1.Text))
            {
                TextArea2.Text = EncryptHelper.MD5Encrypt(TextArea1.Text);

            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.MD5Decrypt(TextArea2.Text);
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {

        }

        protected void Button9_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.GetSHA256HashFromString(TextArea2.Text);
            }
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.GetSHA384HashFromString(TextArea2.Text);
            }
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.GetSHA1HashFromString(TextArea2.Text);
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextArea2.Text))
            {
                TextArea1.Text = EncryptHelper.GetSHA512HashFromString(TextArea2.Text);
            }
        }
    }
}