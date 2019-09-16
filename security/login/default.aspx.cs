using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class security_login_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void uxLogin_Click(object sender, EventArgs e)
    {
        plus.Security.Principal.Identity user = plus.Security.Authentication.Authenticate(UserName.Text, Password.Text, SecurityCode.Text);
        //var usr = user;
        //user.record.ID = 1202;
        FailureText.Visible = !user.IsAuthenticated;
        FailureText.Text = "";

        if (!user.IsAuthenticated)
        {
            FailureText.Text = user.record.errorMessage;
            return;
        }
        if (user.IsAuthenticated) Response.Redirect("~/cpanel/");
        
    }
}