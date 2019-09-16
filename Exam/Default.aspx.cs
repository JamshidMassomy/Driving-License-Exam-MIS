using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf.General.Render.Font.OpenTypeFile;

public partial class EMS_Default : System.Web.UI.Page
{
    private int CategoryID { get; set; }
    //private object QuestionID { get; set; }
    //private object ChoiceID { get; set; }
    private string ApplicantCode { get; set; }

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
                GenerateQuestion(e);
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
    protected void GenerateQuestion( object Val)
    {
        CategoryID++;
        foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "EXEC xm.spGetQuestions @CategoryID", "CategoryID", CategoryID).Rows)
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
            HCHID.Value = Convert.ToString(Options[0][0]);
            //Options[0][2];


            ///OptionsID  [0][0] [1][0]/ [2][0] [3][0]
            //QID = row["ID"];
            //QuestionID = QID;
            // ChID = Options[1][2];
        }
       
    }

    protected void Save(object o, EventArgs e)
    {
        ///to save and return the reuslt from HCHID.Value and HQID.Value
        Response.Write("Question ID :"+Convert.ToString(HQID.Value)+"and Check Box valu is :"+ Convert.ToString(HCHID.Value));
         //Response.Write(row["ID"] + "_____" + Options[0][0] + "______" + Options[1][0] + "_____" + Options[2][0] + "___________" + Options[3][0]);
        if (_1thBox.Checked)
        {

        }
        if (_2thBox.Checked)
        {

        }
        if (_3thBox.Checked)
        {

        }
        if (_4thBox.Checked)
        {

        }
        //Response.Write("Question ID: _"+QuestionID+"Choic ID is :"+ChoiceID+"Applicant OTP is :"+Session["OTP"] );
    }
    protected void Next(object o, EventArgs e)
    {
       
        GenerateQuestion(o);
        //Response.Redirect("Next.aspx");
    }
   
}


﻿


