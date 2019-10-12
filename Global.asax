<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.BASE_URL, "http://localhost/Exam/");
        plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.SECURITY_PATH, "http://localhost/security/");
        plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.SYSTEM_LANGUAGE, plus.Util.Language.Dari);
		plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.AUTHENTICATION_MODE, plus._System.Configuration.AuthenticationMode.MOBILE);
		plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.CONFSYS_DB, "econfsys");
		plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.ACTIVITY_LOGGING, true);
		plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.SCREEN_TRANSLATION, false);
		plus._System.Configuration.Manager.Set(plus._System.Configuration.ConfigurationElement.CLAUSE_CACHE, false);
       // Code that runs on application startup
        plus._System.Configuration.Manager.init();
        plus.Security.Authentication.init();

    }

    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        // Security Check IP
        plus.Security.Authorization.AuthorizeContext();

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        plus.Security.Authentication.SignOut(Context);
    }
       
</script>
