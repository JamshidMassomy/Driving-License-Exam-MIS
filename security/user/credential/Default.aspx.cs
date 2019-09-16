using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class security_user_credential_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;
        FailureText.Visible = false;
        FailureText.Text = "";
        if (!user.IsAuthenticated)
        {
            Response.Redirect("~/security/unauthorized");
        }
        if (!IsPostBack)
        {
            UserName.Text = user.record.UserName;
        }
    }
    protected void uxChangePassword_Click(object sender, ImageClickEventArgs e)
    {
        if (!string.Equals(uxNewPassword.Text, uxConfirmNewPassword.Text))
        {
            plus.Util.Helper.error("رمز عبور جدید و تکرار رمز عبور باید یکی باشد. لطفاٌ دقت نمایید.", "");
            return;
        }
        if (uxNewPassword.Text.Contains(" ") || uxNewPassword.Text.Trim().Length < 8)
        {
            FailureText.Text = "رمز عبور باید حد اقل <b>۸ حرف</b> بوده و دارای <b>فاصله نباشد</b>.";
            FailureText.Visible = true;
            //plus.Util.Helper.error("رمز عبور باید حد اقل ۸ حرف بوده و دارای فاصله نباشد.", "");
            return;
        }
        plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;
        if (user.IsAuthenticated)
        {
            try
            {
                bool isChanged = plus.Security.Authentication.ChangePassword(UserName.Text, Password.Text, uxNewPassword.Text);
                if (isChanged)
                {
                    Response.Redirect("PasswordChanged.aspx");
                }
            }
            catch (Exception ex)
            {
                FailureText.Text = ex.Message;
                FailureText.Visible = true;
            }
        }
    }
}