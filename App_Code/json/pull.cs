using System;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using plus.Security;
using plus.Web;
using System.IO;
/// <summary>
/// Summary description for pull
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class pull : plus.Web.Service.Json.pull//System.Web.Services.WebService 
{

    public pull () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string ListIcons()
    {
        StringBuilder sb = new StringBuilder();
        System.Web.Script.Serialization.JavaScriptSerializer sr = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<object> list = new List<object>();
        DirectoryInfo df = new DirectoryInfo(Server.MapPath("/skin/cpanel/"));
        foreach (FileInfo fi in df.GetFiles())
        {
            list.Add(fi.Name);
        }

        return sr.Serialize(list);
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string GenerateScreen(object _query)
    {
        string IP = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
        if (IP == "::1") IP = "127.0.0.1";
        JsonOutput json = new JsonOutput();

        bool isAuthorized = plus.Security.Authorization.Authorize(plus.Security.Util.GetClientIP(), false);
        if (!isAuthorized) return json.Write(plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.SECURITY_PATH) + "unauthorized/").Serialize();

        json.WriteStartArray();

        try
        {

            Dictionary<string, object> query = (Dictionary<string, object>)_query;

            IDictionary dict = new Dictionary<string, object>();

            dict.Add("dbName", plus.Util.Helper.EmptyToNull(query["dbName"]));
            dict.Add("ObjectName", plus.Util.Helper.EmptyToNull(query["ObjectName"]));
            dict.Add("Prefix", plus.Util.Helper.EmptyToNull(query["Prefix"]));
            dict.Add("IsSubForm", plus.Util.Helper.EmptyToNull(query["IsSubForm"]));
            dict.Add("ParentObject", plus.Util.Helper.EmptyToNull(query["ParentObject"]));
            dict.Add("ElPerRow", plus.Util.Helper.EmptyToNull(query["ElPerRow"]));
            dict.Add("Lang", plus.Util.Helper.EmptyToNull(query["Lang"]));

            System.Data.IDataReader reader = plus.Data.DAL.Read("Default", @"exec [reg].[spGenerateScreen] @dbName, @ObjectName, @Prefix, @IsSubForm, @ParentObject, @ElPerRow, @Lang ", dict);
            bool fetchColumns = (bool)plus.Util.Helper.IfNothing(query.ContainsKey("fetchColumns") ? query["fetchColumns"] : false, false);
            //json.WriteTableResult(table, reader, fetchColumns);
            //json.WriteQueryResult(String.Concat(pathArray[1], ".", pathArray[2]), reader, fetchColumns, true);

            json.WriteQuickResult(String.Concat("Tconfsys.reg", ".", "Screen"), reader, fetchColumns, true);

            if (!reader.IsClosed)
                reader.Close();

        }
        catch (Exception ex)
        {
            json.WriteError(ex.Message /*+ ex.StackTrace*/);
            json.WriteEndArray();
            return json.Serialize();
        }
        json.WriteEndArray();

        return json.Serialize();
    }
    
}
