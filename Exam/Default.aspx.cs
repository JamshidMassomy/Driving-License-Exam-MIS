using System;
using System.Text;
using System.Web.UI.WebControls;

public partial class EMS_Default : System.Web.UI.Page
{
    private string ApplicantCode { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            Session["SessionStart"] = DateTime.Now.AddMinutes(30);
            ViewState["CategoryID"] = 1;
            if (!Convert.ToBoolean(Session["IsAuthenticate"]) || Session["SessionStart"]==null)
            {
                Response.Redirect("~/security/index/");
            }
            else
            {
  
                GetUserInfo();
            }
        }
    }
    protected void GetUserInfo()
    {
        string _html = "";
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "EXEC xm.spGetUserInfo @OTP", "OTP", Session["OTP"]).Rows)
        {
            ApplicantCode = Convert.ToString(row["OTP"]);
            _html = _html + "" +
                    "<a href='#' class='breadcrumb-item'><i class='icon-user mr-4'></i>"+row["FirstName"]+"</a>" +
                    "<a href='#' class='breadcrumb-item'><i class='icon-user mr-4'></i>"+row["FatherName"]+"</a>" +
                    "<a href='#' class='breadcrumb-item'><i class='icon-user mr-4'></i>"+row["LastName"]+"</a>" +
                    "<a href='#' class='breadcrumb-item'><i class='icon-barcode2 mr-4'></i>"+row["OTP"]+"</a>" +
                    "<a href='#' class='breadcrumb-item'><i class='icon-credit-card2 mr-4'></i>"+row["NID"]+"</a>";
        }
        User_Bio.InnerHtml = _html;
    }
    protected void GenerateQuestion()
    {
        string active = "active";
        string QCounter =
            @"<li class='page-item'><a href= '#' class='page-link page-link-white legitRipple'>1</a></li>";
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat(@"
        <li class='page-item'>
        <a href='#' class='page-link page-link-white legitRipple'>
             <i class='icon-arrow-left15'></i>
         </a>
        </li>
     {0}
     <li class='page-item'><a href = '#' class='page-link page-link-white legitRipple'>
             <i class='icon-circle2'></i>
         </a>

     </li>
	 
", QCounter);


          
       // Q_counter.InnerHtml = sb.ToString();
        string _temp = ""; //String.Empty
        //int CheckCounter = 0;
        DateTime expiry = (DateTime)base.Context.Session["SessionStart"];
        if (expiry < DateTime.Now)
        {
            Session.Abandon();
            Response.Redirect("~/security/index/");
        }
        var _Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "EXEC xm.spGetQuestions @CategoryID", "CategoryID", ViewState["CategoryID"] = (int)(ViewState["CategoryID"])+1).Rows)
        {
            var Options = plus.Data.DAL.FindArrayList(plus.Data.DAL.GetSettings("Default"),
                "SELECT * FROM xm.Choice where QuestionID = "+row["ID"]+" ");
            Question.InnerText =Convert.ToString(row["Question"]);
            QImg.Src = "../Exam/XMphoto/"+row["FileName"] +"";
            _1thBox.Text = Convert.ToString(Options[0][2]);
            _2thBox.Text = Convert.ToString(Options[1][2]);
            _3thBox.Text = Convert.ToString(Options[2][2]);
            _4thBox.Text= Convert.ToString(Options[3][2]);

            HQID.Value = Convert.ToString(row["ID"]);

            ViewState["Choice1"] = Options[0][0];
            ViewState["Choice2"] = Options[1][0];
            ViewState["Choice3"] = Options[2][0];
            ViewState["Choice4"] = Options[3][0];
        }
        if ((int)ViewState["CategoryID"] >= 7 )
        {
            Session["IsAuthenticate"] = false;
            Response.Redirect("Result.aspx");
            //Session.Abandon();
        }
    }
    protected void Next(object o, EventArgs e)
    {
        var _Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        for (int b = 0; b < 4; b++)
        {
            if (_Boxes[b].Checked)
            {
                b++;
                if (ViewState["Choice" + b + ""] == null)
                {
                    Response.Write("NULL passed");
                    //Next(o, e);
                }
                else { HCHID.Value = ViewState["Choice" + b + ""].ToString(); }
            }
        }
        plus.Data.DAL.valueOf("Default", "EXEC xm.spSaveApplicantAnswer '" + Session["OTP"] + "' ,'" + HQID.Value + "','" + HCHID.Value + "','" + DateTime.Now + "' ");
        var _2Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        foreach (var check in _2Boxes)
        {
            if (check.Checked)
            {
                check.Checked = false;
            }
        }
        GenerateQuestion();
    }
    protected void Info_Skip(object obj, EventArgs eva)
    {
        xm.Visible = true;
        info.Visible = false;
        GenerateQuestion();
    }
    protected void Timer_tick(object _o, EventArgs _e)
    {
        
    }
}


﻿


     