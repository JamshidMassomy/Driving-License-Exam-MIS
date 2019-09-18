using System;
using System.Activities.Statements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Spire.Pdf.General.Render.Font.OpenTypeFile;

public partial class EMS_Default : System.Web.UI.Page
{
    private string ApplicantCode { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["CategoryID"] = 1;
            if (!Convert.ToBoolean(Session["IsAuthenticate"]))
            {
                Response.Redirect("~/security/index/");
            }
            else
            {
                GetUserInfo();
                GenerateQuestion(sender,e);
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
    protected void GenerateQuestion(object o, EventArgs e)
    {

        
        int CheckCounter = 0;
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
        if ((int)ViewState["CategoryID"] > 7 || (int)ViewState["CategoryID"] == 7)
        {
            Exit(o,e);
        }

    }

    protected void Save(object o, EventArgs e)
    {
        //int i = 1;
        if (_1thBox.Checked)
        {
            Response.Write("first");
            HCHID.Value =  ViewState["Choice1"].ToString();
            Response.Write(HCHID.Value);
        }
        if (_2thBox.Checked)
        {
            Response.Write("second");
            HCHID.Value = ViewState["Choice2"].ToString();
            Response.Write(HCHID.Value);
            //HCHID.Value = ViewState["Choice1"].ToString();
        }
        if (_3thBox.Checked)
        {
            Response.Write("third");
            HCHID.Value = ViewState["Choice3"].ToString();
            Response.Write(HCHID.Value);
            //HCHID.Value = ViewState["Choice1"].ToString();
        }
        if (_4thBox.Checked)
        {
            Response.Write("last");
            //HCHID.Value = ViewState["Choice1"].ToString();
        }

        //var _Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        //for (int b = 0; b <= 4; b++)
        //{
        //    if (_Boxes[b].Checked)
        //    {
        //        //Response.Write("selcted ITem is:Options" + Options[b][0]);
        //        HCHID.Value = ViewState["Choice1"].ToString();
        //    }
        //}
        ///to save and return the reuslt from HCHID.Value and HQID.Value
        Response.Write("Question ID :"+Convert.ToString(HQID.Value)+"and Check Box valu is :"+ Convert.ToString(HCHID.Value));
        
        Btn_Confirm.Attributes.Add("onclick", "this.disabled=true;");
        Response.Write("dbl clicked");
        //Btn_Confirm.


        //Response.Write(row["ID"] + "_____" + Options[0][0] + "______" + Options[1][0] + "_____" + Options[2][0] + "___________" + Options[3][0]);

        //Response.Write("Question ID: _"+QuestionID+"Choic ID is :"+ChoiceID+"Applicant OTP is :"+Session["OTP"] );
        //Btn_Confirm.Attributes.Add("OnClick","return false");
    }
    protected void Next(object o, EventArgs e)
    {
        var _Boxes = new CheckBox[] { _1thBox, _2thBox, _3thBox, _4thBox };
        foreach (var check in _Boxes)
        {
            if (check.Checked)
            {
                check.Checked = false;
            }
        }

        //for (int n = 0; n <= _Boxes.Length; n++)
        //{
        //    _Boxes[n].Checked = false;
        //}
        GenerateQuestion(o,e);

        //Response.Redirect("Next.aspx");
    }

    protected void Exit(object o, EventArgs args)
    {
        Response.Redirect("Result.aspx");
        Session["IsAuthenticate"] = false;
        //Response.Write("Greter then 7");
        ViewState["CategoryID"] = 0;
        Btn_Exit.Visible = true;
        //HtmlGenericControl html = new HtmlGenericControl();
        //html.InnerHtml = @"ختم 
        // <i class='icon - backward2 mr - 3 icon - 2x'></i>
        //    ";

        ////LinkBtn.Controls.Add(html);
        //‌Btn_Next.Controls.Add(html);
    }
}


﻿


