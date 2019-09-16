using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class security_user_register_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            plus.Data.Common.populateDLLs(uxGenderID, uxMaritalStatusID);
            //plus.Util.Helper.error(plus.Util.Helper.right("0704447137", 4), "");
        }
    }

    protected void Register(object sender, EventArgs e)
    {
        if ( !string.Equals(uxPasswordRepeat.Text, uxPassword.Text )) {
            plus.Util.Helper.error("رمز عبور و تکرار رمز عبور یکی نیست. لطفاْ دقت نمایید.", "");
            return;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");

        System.Collections.IDictionary param = new Dictionary<string, object>();

        System.Data.Common.DbDataReader reader = plus.Data.DAL.Read ("Default", 
                "SELECT Substring(PARAMETER_NAME,2, 100) PARAMETER_NAME, DATA_TYPE, PARAMETER_MODE FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_SCHEMA = 'perm' AND SPECIFIC_NAME = 'spRegisterUser' ORDER BY ORDINAL_POSITION");
        
        while (reader.Read()) {
            string name = reader["PARAMETER_NAME"].ToString();
            if (sb.Length > 1) sb.Append(", ");
            sb.Append("@" + name);
            object value = plus.Data.Common.FindAndGetValue(dvAsanWazifa_prf_Profile, "ux" + name);
            param.Add(name, value);
        }

        reader.Close();

        plus.Security.Principal.UserRecord record = plus.Security.Authentication.RegisterUser(param);
        if (string.IsNullOrEmpty(record.errorMessage))
        {
            Server.Transfer("~/Security/User/activation");
            //plus.Util.Helper.success("User successfully created. Please check your email for activation link.", "");
            //return;
        }
        
    }
}