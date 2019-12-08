using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

public partial class EMS_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            if (!Convert.ToBoolean(Session["IsAuthenticate"]))
            {
                Response.Redirect("~/security/index/");
            }
            else
            {
                ViewState["CategoryID"] = 0;
                Session["SessionStart"] = DateTime.Now.AddMinutes(5);
                GetUserInfo();
                GenerateQuestion();


            }
        }
    }
    protected void GetUserInfo()
    {
        string _html = "";
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "EXEC xm.spGetUserInfo @OTP", "OTP", Session["OTP"]).Rows)
        {
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
        //DateTime expiry = (DateTime)base.Context.Session["SessionStart"];
        //if (expiry < DateTime.Now)
        //{
        //    Session.Abandon();
        //    Response.Redirect("~/security/index/");
        //}
        var _Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "EXEC xm.spGetQuestions @CategoryID", "CategoryID", ViewState["CategoryID"] = (int)(ViewState["CategoryID"])+1).Rows)
        {
            
            var Options = plus.Data.DAL.FindArrayList(plus.Data.DAL.GetSettings("Default"),
                "SELECT * FROM xm.Choice where QuestionID = "+row["ID"]+" ");
            Question.InnerText =Convert.ToString(row["Question"]);
            QImg.Src = "../../Exam/photos/" + row["FileName"] +"";
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
        if ((int)ViewState["CategoryID"] >=7 )
        {
            Finish();
        }
    }
    protected void Next(object o, EventArgs e)
    {

        Qselect.Value = ViewState["CategoryID"].ToString();
        GenerateQuestion();
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
        
    }
    //protected void Info_Skip(object obj, EventArgs eva)
    //{
    //    xm.Visible = true;
    //    info.Visible = false;
    //    GenerateQuestion();
    //}
    protected void Finish()
    {
       // plus.Data.DAL.valueOf("Default", "EXEC xm.spSaveResult '" + Session["OTP"] + "','" + DateTime.Now + "' ");
        Response.Redirect("~/Exam/result");
        Session.Abandon();
        
    }
}


﻿


     