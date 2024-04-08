using System;
using FineUIPro;

namespace LeanFine.Lf_Office.OA
{
    public partial class warning : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //TextArea TextArea1 = Window1.FindControl("TextArea1") as TextArea;
                //TextArea1.Text = "1.此联系人搜索数据库中包含的所有信息都是保密的。"
                //                    + "\r\n\r\n2.请严格遵守相关制度。<就业规则、个人信息管理规程及其他相关规则>，同意不得擅自向第三方泄漏！"
                //                    + "\r\n\r\n3.この連絡先検索データベースに収録されている内容は、すべて社外秘（ティアックグループ外秘）となります。"
                //                     + "\r\n\r\n4.本連絡先検索にて取得した情報の取扱には細心の注意を払い、就業規"
                //                      + "\r\n\r\n則・個人情報管理規程その他関連規則を遵守し、会社に無断で第三者に。"
                //                      + "\r\n\r\n漏洩しないことに同意いただける場合に限り、本連絡先検索を利用する"
                //                       + "\r\n\r\nことができます。"

                //                    ;
            }
        }

        protected void Btn_Agree_Click(object sender, EventArgs e)
        {
            string strAgree = global::Resources.GlobalResource.sys_Iagree;
            PageContext.RegisterStartupScript("alertAndRedirect('" + strAgree + "', './contact_query.aspx');");
        }
    }
}