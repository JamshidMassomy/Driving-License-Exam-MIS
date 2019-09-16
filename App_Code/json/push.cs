using System;
using System.Web.Script.Services;
using System.Web.Services;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using plus;
using plus.Web;

/// <summary>
/// Summary description for push
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class push : plus.Web.Service.Json.push // System.Web.Services.WebService
{

    public push()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public string Apply(object id, string Category)
    {

        bool isAuthorized = plus.Security.Authorization.Authorize(plus.Security.Util.GetClientIP(), false);
        if (!isAuthorized) return new plus.Web.Response(null, ResponseType.FAILED_AUTHORIZATION, plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.SECURITY_PATH) + "unauthorized/").Serialize();

        plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;

        IDictionary values = new Dictionary<string, object>();
        values.Add("ProfileID", user.record.get("EmployeeID"));
        values.Add("RecordID", id);
        values.Add("Category", Category);
        values.Add("token", "token");

        Response r = plus.Web.Service.Transaction.SaveNormal(System.Data.CommandType.StoredProcedure, values, "RegistryServices.dbo.spApply");
        return r.Serialize();

        //object retVal = plus.Data.DAL.valueOf("RegistryServices", @"EXEC dbo.spApply @ProfileID, @RecordID, @Category", "ProfileID", user.record.get("EmployeeID"), "Category", Category, "RecordID", id);

        //return new Response(retVal, ResponseType.SUCCESS, "").Serialize();
    }

    // [WebMethod(EnableSession = true)]
    // [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    // public string saveRights(object query)
    // {
        // Request _request = new Request();
        // plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;


        // if (!user.IsAuthenticated) return user.GetResponseObject().Serialize();
        // IDictionary _query = (IDictionary)query;
        // Response _response = new Response(null, ResponseType.NONE, "Couldn't save record. Permission denied!");
        // if (!(bool)plus.Data.DAL.valueOf((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB),
                    // "EXEC perm.spHasSaveRightOn 'Tconfsys', 'pol', 'RoleRight', @UserID;",
                    // "UserID", user.record.ID))
        // {
            // return _response.Serialize();
        // }

        // StringBuilder sb = new StringBuilder("SET XACT_ABORT ON; BEGIN TRANSACTION; ");
        // IDictionary param = new Dictionary<string, object>();
        // param.Add("CreatedBy", user.record.ID);
        // param.Add("ModifiedBy", user.record.ID);
        // param.Add("OpenTran", true);
        // param.Add("RoleID", _query["RoleID"]);
        // int index = 0;
        // foreach (IDictionary changes in (Object[])_query["values"])
        // {
            // index++;

            // sb.AppendFormat("EXEC perm.spSaveRoleRight @RoleID, @ScreenID_{0}, @Insert_{0}, @Del_{0}, @CreatedBy, @ModifiedBy, @OpenTran; ", index);
            // param.Add(string.Format("ScreenID_{0}", index), changes["screenid"].ToString());
            // param.Add(string.Format("Insert_{0}", index), String.Join(",", (Object[])changes["insert"]));
            // param.Add(string.Format("Del_{0}", index), String.Join(",", (Object[])changes["del"]));
        // }

        // sb.Append(" IF @@ERROR > 0 BEGIN  ROLLBACK TRANSACTION; RAISERROR(N'مشکل در سیستم رخ داد', 6, 1); END; COMMIT TRANSACTION; ");


        // try
        // {
            // plus.Data.DAL.Execute((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB)
                // , sb.ToString(), param);
            // _response = new Response("1", ResponseType.SUCCESS, "Successfully saved");
        // }
        // catch (Exception ex)
        // {
            // _response = plus.Web.Service.Transaction.HandlException(ex);
        // }

        // return _response.Serialize();
    // }
	
	
	
	// [WebMethod(EnableSession = true)]
	// [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	// public string saveAccesses(object query)
	// {
		// Request _request = new Request();
		// plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;


		// if (!user.IsAuthenticated) return user.GetResponseObject().Serialize();
		// IDictionary _query = (IDictionary)query;
		// Response _response = new Response(null, ResponseType.NONE, "Couldn't save record. Permission denied!");
		// if (!(bool)plus.Data.DAL.valueOf((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB),
					// "EXEC perm.spHasSaveRightOn 'Tconfsys', 'perm', 'RoleAccessibility', @UserID;",
					// "UserID", user.record.ID))
		// {
			// return _response.Serialize();
		// }

		// StringBuilder sb = new StringBuilder("SET XACT_ABORT ON; BEGIN TRANSACTION; ");
		// IDictionary param = new Dictionary<string, object>();
		// param.Add("CreatedBy", user.record.ID);
		// param.Add("ModifiedBy", user.record.ID);
		// param.Add("OpenTran", true);
		// param.Add("RoleID", _query["RoleID"]);
		// int index = 0;
		// foreach (IDictionary changes in (Object[])_query["values"])
		// {
			// index++;

			// sb.AppendFormat("EXEC perm.spSaveRoleAccess @RoleID, @ScreenID_{0}, @Insert_{0}, @Del_{0}, @CreatedBy, @ModifiedBy, @OpenTran; ", index);
			// param.Add(string.Format("ScreenID_{0}", index), changes["screenid"].ToString());
			// param.Add(string.Format("Insert_{0}", index), String.Join(",", (Object[])changes["insert"]));
			// param.Add(string.Format("Del_{0}", index), String.Join(",", (Object[])changes["del"]));
		// }

		// sb.Append(" IF @@ERROR > 0 BEGIN  ROLLBACK TRANSACTION; RAISERROR(N'مشکل در سیستم رخ داد', 6, 1); END; COMMIT TRANSACTION; ");


		// try
		// {
			// plus.Data.DAL.Execute((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB)
				// , sb.ToString(), param);
			// _response = new Response("1", ResponseType.SUCCESS, "Successfully saved");
		// }
		// catch (Exception ex)
		// {
			// _response = plus.Web.Service.Transaction.HandlException(ex);
		// }

		// return _response.Serialize();
	// }

	// [WebMethod(EnableSession = true)]
	// [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
	// public string saveSEAccesses(object query)
	// {
		// Request _request = new Request();
		// plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;


		// if (!user.IsAuthenticated) return user.GetResponseObject().Serialize();
		// IDictionary _query = (IDictionary)query;
		// Response _response = new Response(null, ResponseType.NONE, "Couldn't save record. Permission denied!");
		// if (!(bool)plus.Data.DAL.valueOf((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB),
					// "EXEC perm.spHasSaveRightOn 'Tconfsys', 'perm', 'RoleAccessibility', @UserID;",
					// "UserID", user.record.ID))
		// {
			// return _response.Serialize();
		// }

		// StringBuilder sb = new StringBuilder("SET XACT_ABORT ON; BEGIN TRANSACTION; ");
		// IDictionary param = new Dictionary<string, object>();
		// param.Add("CreatedBy", user.record.ID);
		// param.Add("ModifiedBy", user.record.ID);
		// param.Add("OpenTran", true);
		// param.Add("RoleID", _query["RoleID"]);
		// int index = 0;
		// foreach (IDictionary changes in (Object[])_query["values"])
		// {
			// index++;

			// sb.AppendFormat("EXEC perm.spSaveExternalObjectAccess @RoleID, @ScreenID_{0},@ExternalID_{0}, @Insert_{0}, @Del_{0}, @CreatedBy, @ModifiedBy, @OpenTran; ", index);
			// param.Add(string.Format("ScreenID_{0}", index), changes["screenid"].ToString());
			// param.Add(string.Format("ExternalID_{0}", index), changes["externalid"].ToString());
			// param.Add(string.Format("Insert_{0}", index), String.Join(",", (Object[])changes["insert"]));
			// param.Add(string.Format("Del_{0}", index), String.Join(",", (Object[])changes["del"]));
		// }

		// sb.Append(" IF @@ERROR > 0 BEGIN  ROLLBACK TRANSACTION; RAISERROR(N'مشکل در سیستم رخ داد', 6, 1); END; COMMIT TRANSACTION; ");


		// try
		// {
			// plus.Data.DAL.Execute((string)plus._System.Configuration.Manager.Get(plus._System.Configuration.ConfigurationElement.Tconfsys_DB)
				// , sb.ToString(), param);
			// _response = new Response("1", ResponseType.SUCCESS, "Successfully saved");
		// }
		// catch (Exception ex)
		// {
			// _response = plus.Web.Service.Transaction.HandlException(ex);
		// }

		// return _response.Serialize();
	// }

}
