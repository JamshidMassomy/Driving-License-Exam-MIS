using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Exam_Result : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _Result = "";
            string _Result_Info = "";
            foreach (System.Data.DataRow row in plus.Data.DAL.GetTable("Default", "SELECT * FROM xm.vResult WHERE ApplicantCode = '" + Session["OTP"] + "' ").Rows)
            {
                
                _Result = _Result + @"
                    <table class='table table-bordered'>
                    <thead>
                    <tr class='bg-blue'>
                        <th>OTP</th>
                        <th>آسم</th>
                        <th>ولد</th>
                        <th>نمبر تذکره</th>
                        <th>نمره</th>
                        <th>نتیجه</th>
                    </tr>
                    </thead>
                    <tbody>
                    <tr> 
                    <td>" + row["ApplicantCode"] + "</td>" +
                     "<td> " + row["FirstName"] + " </td>" +
                     "<td> " + row["FatherName"] + " </td>" +
                     "<td> " + row["NID"] + " </td>" +
                     "<td> " + row["Score"] + " </td>" +
                     "<td> " + row["Result"] + " </td>" +
                     "</tr>" +
                     "</tbody>" +
                  "</table> ";
                _Result_Info = _Result_Info + @"
                        <div class='card card-body border-top-1 border-top-pink'>
                            <div class='text-center'>
                            <h6 class='font-weight-bold mb-0'>" + row["Result"] + "</h6>" +
                            "<p class='mb-3'>" + row["Score"] + "</p><div></div>";
                
                Result_Info.InnerHtml = _Result_Info;
                Result_Table.InnerHtml = _Result;

            }
        }
    }
    protected void End(Object o, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/security/index/");
    }

    
}