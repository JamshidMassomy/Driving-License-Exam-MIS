

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cpanel_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        plus.Security.Authorization.Authorize(plus.Security.Util.GetClientIP(), true);
        if (!IsPostBack)
        {
            plus.Security.Principal.Identity user = plus.Security.Principal.Identity.Current;
            if (user.record.ID == 1)
            {
                //this else nothing
            }

            prepare(user);
            LoadSideBar(user);
        }
    }

    void prepare(plus.Security.Principal.Identity user)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder("");
        Int16 ApplicationID = 0;
        string category = "";
        IDictionary _screens = user.GetScreens();
        foreach (DictionaryEntry screen in _screens)
        {
            IDictionary screenObj = (Dictionary<string, object>)screen.Value;
            if (screenObj["ParentID"] != DBNull.Value) continue;
            

            if (!Int16.Equals(ApplicationID, Convert.ToInt16(screenObj["ApplicationID"])))
            {
                if (ApplicationID != 0) sb.Append("</div>");
                // Applicatio div
                sb.AppendFormat("<h2>{0}</h2><h2 style='font: 0.9em/22px calibri; color:#666;'>{1}</h2><div class='div-application'>", screenObj["Application"], screenObj["APPDescription"]);

            } else if (!string.Equals(category, screenObj["Category"]))
                sb.AppendFormat("<div class='div-application' style='border:none; min-height:1px'></div>");
            
            string icon = screenObj["icon"].ToString();
            if (!icon.Contains("http")) icon = "../skin/cpanel/" + icon + ".png";
            sb.AppendFormat("<div class='div-screen'><a href='../page/init#path:=::{0}'><img src='{1}'><label><bdi>{2}</bdi></label><span><bdi>{3}</bdi></span></a></div>",
                  plus.Security.Cryptography.SimpleEncryption.EncryptPath((string)screenObj["FullPath"]), icon, screenObj["Name"], screenObj["Description"]);
            
            category = screenObj["Category"] as string;
            ApplicationID = Convert.ToInt16(screenObj["ApplicationID"]);
        }

        cpanel.InnerHtml = sb.ToString();
    }

    void LoadSideBar(plus.Security.Principal.Identity user) {
        
        string ustr = "";
        ustr = "<img class='UserLogo' src='../skin/logo/Transport1.png'>";
        ustr += "<span class='UserName'>" + " " + user.record.get("UserName").ToString().Replace('.', ' ') + "</span>";
        ustr += "<span class='Desc'>(Traffic archive)</span>";
        MenuUserDiv.InnerHtml = ustr;

        string val = Convert.ToString(plus.Data.DAL.valueOf("default", "select count(1) from [reg].[Notification] where StatusID = 1 AND ReceiverUserID = @ReceiverUserID", "ReceiverUserID", user.record.ID));
        notification.InnerHtml = val;
        notificationCount.InnerHtml = val;

        string str = "";
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "select ID, Title, Notification, fromuser from reg.vNotification where StatusID = 1 AND ReceiverUserID = @ReceiverUserID", "ReceiverUserID", user.record.ID).Rows)
        {
            string s = row["Notification"].ToString();
            if(s.Length > 25)
            {
                s = s.Substring(0, 25);
            }
            str = str + "<div class='notification' data-id='"+row["ID"]+ "' onclick='showNotification(this)' >" +
                    "<img class='icon' src='../skin/icon/notification1.png' />" +
                    "<div class='info' >" +
                        "<span class='Sender'><bdi>" + row["fromuser"] + "</bdi></span>" +
                        "<span class='Title'><bdi>" +  row["Title"] + " </bdi></span>" +
                    "</div>" +
                    "<span class='Description'><bdi>" + s + " ...</bdi></span>" +
                "</div>";
        }

        
        notificationList.InnerHtml = str; 
    }
}


﻿


