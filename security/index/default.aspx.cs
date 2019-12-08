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
        //for test purpose
        plus.Security.Principal.Identity user = plus.Security.Authentication.Authenticate("jamshid.massomy", "password4321", "8989");
        if (user.IsAuthenticated) Response.Redirect("~/cpanel/");
       
    }

    protected void admin_click(object sender, EventArgs e)
    {
        Response.Redirect("~/security/login/");
    }
    protected void Demo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Demo/");
    }
    protected void Validate_OTP(object o, EventArgs e)
    {
        bool isAuthenticate = false;
        isAuthenticate = Convert.ToBoolean(plus.Data.DAL.valueOf("Default", "EXEC xm.spValidateApplicant @OTP", "OTP", Code.Text));
        if (isAuthenticate)
        {
            var _otp = Code.Text;
            Session["OTP"] = _otp;
            Session["IsAuthenticate"] = true;
            Response.Redirect("~/Exam/intro/");
        }
        else
        {
            noty_layout__topRight.Visible = true;
            noti_message.InnerHtml = "Invalid Code";
        }
        
    }



}