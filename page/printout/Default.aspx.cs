using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data.SqlClient;
using System.Configuration;

public partial class page_printout_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if(Request.QueryString["excel"] != null){
			ExportExcel();
			return;
		}
        string url = Request.QueryString["url"];
        //Response.Write(url);

        string html = System.IO.File.ReadAllText(Server.MapPath("~" + url));

        string recordid = Request.QueryString["recordid"];

        string path = Request.QueryString["altPath"] ?? Request.QueryString["path"]; // <conn>.<schema>.<object>

        string type = Request.QueryString["type"] ?? "application";
		
        string[] pathArray = path.Contains('_') ? path.Split('_') : path.Split('.');

        System.Data.Common.DbDataReader reader = null;

		
		String TypeString = Request.QueryString["Type"];

		if (TypeString.Equals("timesheet"))
        {
            System.Collections.IDictionary param = new Dictionary<string, object>();
			
			var UserID = Request.QueryString["userID"];
            var OrgUnit = Request.QueryString["orgID"];
            var start = Request.QueryString["start"];
            var end = Request.QueryString["end"];
            var rank = Request.QueryString["rank"];
			
			param.Add("ID", null);
            param.Add("OrganizationUnitID", null);
            param.Add("StartDate", null);
            param.Add("EndDate", null);
            param.Add("Rank", null);
            
			if(UserID.Length != 0)
				param["ID"] = UserID;
            if(OrgUnit.Length != 0)
				param["OrganizationUnitID"] = OrgUnit;
			if(start.Length != 0)
				param["StartDate"] = start;
			if(end.Length != 0)
				param["EndDate"] = end;
			if(rank.Length != 0)
				param["Rank"] = rank;
            param.Add("CreatedBy", 1);
            param.Add("Offset", 0);
            param.Add("Limit", 0);

            int typeID = 0;
            string p = Request.QueryString["Path"].Split('.')[0];
            try
            {
                typeID = Convert.ToInt32(Request.QueryString["typeID"]);
            }
            catch (Exception) { }

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[p].ToString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "att.spGetMonthlyTimeSheet";
                foreach (var ent in param.Keys)
                {
                    command.Parameters.Add(new SqlParameter(ent.ToString(), param[ent]));
                }
                command.CommandTimeout = 200;
                reader = command.ExecuteReader();

            };

            //reader = plus.Data.DAL.Read("AMS", "att.spGetMonthlyTimeSheet",param);

            Dictionary<int, String> maps = new Dictionary<int, string>();

            maps.Add(2, "PR");
            maps.Add(3, "AB");
            maps.Add(4, "HD");
            maps.Add(5, "ALH");
            maps.Add(6, "AL");


            StringBuilder sb = new StringBuilder();
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>کود</th><th>نام مکمل</th><th>نام پدر</th><th>وظیفه</th><th>بست</th><th>اداره</th>");

            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (i > 6)
                {
                    sb.Append("<th>" + reader.GetName(i).Split('_')[2] + "</th>");
                }
            }

            sb.Append("</tr></thead>");
            sb.Append("<tbody>");

            while (reader.Read())
            {
                sb.Append("<tr>");
                for(int i = 1; i < reader.FieldCount; i++)
                {
                    if (i < 7 || (typeID == 0 || typeID == 1))
                        sb.Append("<td>" + reader[i].ToString() + "</td>");
                    else
                    {
                        string s = reader[i].ToString();
                        if ( maps[typeID].Equals(s))
                        {
                            sb.Append("<td>" + reader[i].ToString() + "</td>");
                        }
                        else
                        {
                            sb.Append("<td>&nbsp;</td>");
                        }
                    }
                }
                sb.Append("</tr>");
            }
            sb.Append("<tbody>");

            html = html.Replace("$body", sb.ToString());
			Response.Write(html);
            conn.Close();

            return;
        }

        else if (TypeString.Equals("attreport"))
        {

            int typeID = 0;
            try
            {
                typeID = Convert.ToInt32(Request.QueryString["typeID"]);
            }
            catch (Exception) { }

            System.Collections.IDictionary param = new Dictionary<string, object>();
            var UserID = Request.QueryString["userID"];
            var OrgUnit = Request.QueryString["orgID"];
            var start = Request.QueryString["start"];
            var end = Request.QueryString["end"];
            var rank = Request.QueryString["rank"];

            param.Add("UserID", null);
            param.Add("OrganizationUnit", null);
            param.Add("StartDate", null);
            param.Add("EndDate", null);
            param.Add("RankID", null);
            param.Add("AttID", null);
            param.Add("HRCode", null);
            param.Add("CreatedBy", null);
            param.Add("Type", null);
            if (UserID.Length != 0)
                param["UserID"] = UserID;
            if (OrgUnit.Length != 0)
                param["OrganizationUnit"] = OrgUnit;
            if (start.Length != 0)
                param["StartDate"] = start;
            if (end.Length != 0)
                param["EndDate"] = end;
            if (rank.Length != 0)
                param["RankID"] = rank;
            if (typeID != 0)
                param["Type"] = typeID;

            reader = plus.Data.DAL.Read("AMS", "att.spGenerateTimeSheet", param);



            StringBuilder sb = new StringBuilder();
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th> کود کادری </th><th> نام مکمل </th><th> نام پدر </th><th> وظیفه </th><th> بست </th><th> ریاست </th><th> شیفت </th><th> تاریخ </th><th> وقت دخول </th><th> وقت خروج </th><th> حاضری </th>");
            sb.Append("</tr>");
            sb.Append("</thead>");

      


            sb.Append("<tbody>");
            while (reader.Read())
            {
                sb.Append("<tr>");
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    if(!reader.GetName(i).Equals("AttType"))
                        sb.Append("<td>" + reader[i].ToString() + "</td>"); 
                }
                sb.Append("</tr>");
            }

            sb.Append("</tbody>");
            html = html.Replace("$body", sb.ToString());
			Response.Write(html);
			return;
        }

		
        if (type.Equals("idcard"))
        {
            //string keyCol = Request.QueryString["PID"] ?? "ID";
            System.Data.DataSet ds = plus.Data.DAL.GetDataSet("hcm", "select FullName, FatherName, HRCode, Position, IssueDate, PhotoPath from rec.vCardPrint where ID=" + recordid);

            string imgPath = ds.Tables[0].Rows[0]["PhotoPath"].ToString();
            string xName = ds.Tables[0].Rows[0]["FullName"].ToString();
            string xNameEng = (ds.Tables[0].Rows[0]["FullName"].ToString())+"s";
            string xSO = ds.Tables[0].Rows[0]["Fathername"].ToString();
            string xDuty = ds.Tables[0].Rows[0]["Position"].ToString();
            string xCode = ds.Tables[0].Rows[0]["HRCode"].ToString();
            string xIssueDate = ds.Tables[0].Rows[0]["IssueDate"].ToString();

            //Labels
            string lblName = "اسم:";
            string lblSO = "ولد/بنت:";
            string lblDuty = "وظیفه:";
            string lblID = "شماره شناخت:";
            string lblIssueDate = "تاریخ صدور:";

            string lblNameEng = "Name:";
            string lblSOEng = "S/O:";
            string lblDutyEng = "Duty:";
            string lblIDEng = "ID Number:";
            string lblIssueDateEng = "IssueDate:";

            //Get the Data
            string Name = xName; //"نادر";
            string SO = xSO;//"روشن";
            string Duty = xDuty;//"انجنیر نرم افزار";
            string ID = xCode;//"SC000122";
            string IssueDate = xIssueDate;//"1396-12-2";

            string NameEng = xNameEng;//"Nadir";
            string SOEng = xSO;//"Roshan";
            string DutyEng = xDuty;//"Software Engineer";
            string IDEng = xCode; //"SC000122";
            string IssueDateEng = xIssueDate; //"2017-12-2";

            //imgPath = imgPath.Contains('\\') ? imgPath.Replace('\\', '/') : imgPath.Replace('\\','/');

            string photo = Server.MapPath("~/hcm/userPhoto" +imgPath);

            //Set X and Y axis for Labels and Data
            PointF lblNameLoc = new PointF(535f, 130f);
            PointF lblSOLoc = new PointF(535f, 190f);
            PointF lblDutyLoc = new PointF(535f, 250f);
            PointF lblIDLoc = new PointF(535f, 310f);
            PointF lblIssueDateLoc = new PointF(535f, 365f);

            PointF lblNameEngLoc = new PointF(265f, 130f);
            PointF lblSOEngLoc = new PointF(265f, 190f);
            PointF lblDutyEngLoc = new PointF(265f, 250f);
            PointF lblIDEngLoc = new PointF(265f, 310f);
            PointF lblIssueDateEngLoc = new PointF(265f, 365f);

            PointF nameLoc = new PointF(360f, 130f);
            PointF SOLoc = new PointF(360f, 190f);
            PointF DutyLoc = new PointF(360f, 250f);
            PointF IDLoc = new PointF(360f, 310f);
            PointF IssueDateLoc = new PointF(360f, 365f);
            PointF photoLoc = new PointF(600f, 140f);

            PointF nameEngLoc = new PointF(450f, 130f);
            PointF SOEngLoc = new PointF(450f, 190f);
            PointF DutyEngLoc = new PointF(450f, 250f);
            PointF IDEngLoc = new PointF(450f, 310f);
            PointF IssueDateEngLoc = new PointF(450f, 365f);
            PointF photoEngLoc = new PointF(20f, 140f);

            //Dari Text Direction
            StringFormat rtl = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            //Cards path
            string imageDariFilePath = Server.MapPath("~/page/templ/idcard/dari/Administration.jpg"); //@"~/hcm/page/idcard/dari/Administration.jpg"; //template path dari
            string imageEngFilePath = Server.MapPath("~/page/templ/idcard/dari/Administration.jpg");//@"~/hcm/page/idcard/eng/Administration.jpg"; //template path eng

            string imageDariOutput = Server.MapPath("~/page/templ/idcard/Generated/" + Name + ".jpg"); //@"~/hcm/page/idcard/Generated/" + Name + ".jpg"; //Path after generated dari
            string imageEngOutput = Server.MapPath("~/page/templ/idcard/Generated/" + NameEng + ".jpg"); //@"~/hcm/page/idcard/Generated/" + NameEng + ".jpg"; //Path after generated eng

            //Resize Image
            System.Drawing.Image imgResized = ScaleImage(System.Drawing.Image.FromFile(photo), 70, 80);

            //Convert Image to bitmap

            Bitmap daribitmap = (Bitmap)System.Drawing.Image.FromFile(imageDariFilePath);
            Bitmap engbitmap = (Bitmap)System.Drawing.Image.FromFile(imageEngFilePath);

            Bitmap bitmapPhoto = (Bitmap)imgResized;

            //Write the labels and data on image using graphics class

            using (Graphics gdari = Graphics.FromImage(daribitmap))
            using (Graphics geng = Graphics.FromImage(engbitmap))
            {
                using (Font arialFont = new Font("Times New Roman", 8))
                {
                    //Dari Card-----------------------------------------------------------------
                    //Draw labels
                    gdari.DrawString(lblName, arialFont, Brushes.Black, lblNameLoc, rtl);
                    gdari.DrawString(lblSO, arialFont, Brushes.Black, lblSOLoc, rtl);
                    gdari.DrawString(lblDuty, arialFont, Brushes.Black, lblDutyLoc, rtl);
                    gdari.DrawString(lblID, arialFont, Brushes.Black, lblIDLoc, rtl);
                    gdari.DrawString(lblIssueDate, arialFont, Brushes.Black, lblIssueDateLoc, rtl);

                    //Draw data
                    //gdari.CompositingMode = CompositingMode.SourceOver;
                    //bitmapPhoto.MakeTransparent();
                    gdari.DrawImage(bitmapPhoto, photoLoc);
                    gdari.DrawString(Name, arialFont, Brushes.Black, nameLoc, rtl);
                    gdari.DrawString(SO, arialFont, Brushes.Black, SOLoc, rtl);
                    gdari.DrawString(Duty, arialFont, Brushes.Black, DutyLoc, rtl);
                    gdari.DrawString(ID, arialFont, Brushes.Black, IDLoc, rtl);
                    gdari.DrawString(IssueDate, arialFont, Brushes.Black, IssueDateLoc, rtl);

                    // English Card--------------------------------------------------------------
                    //Draw labels
                    geng.DrawString(lblNameEng, arialFont, Brushes.Black, lblNameEngLoc);
                    geng.DrawString(lblSOEng, arialFont, Brushes.Black, lblSOEngLoc);
                    geng.DrawString(lblDutyEng, arialFont, Brushes.Black, lblDutyEngLoc);
                    geng.DrawString(lblIDEng, arialFont, Brushes.Black, lblIDEngLoc);
                    geng.DrawString(lblIssueDateEng, arialFont, Brushes.Black, lblIssueDateEngLoc);

                    //Draw data
                    //geng.CompositingMode = CompositingMode.SourceOver;
                    //bitmapPhoto.MakeTransparent();
                    geng.DrawImage(bitmapPhoto, photoEngLoc);
                    geng.DrawString(NameEng, arialFont, Brushes.Black, nameEngLoc);
                    geng.DrawString(SOEng, arialFont, Brushes.Black, SOEngLoc);
                    geng.DrawString(DutyEng, arialFont, Brushes.Black, DutyEngLoc);
                    geng.DrawString(IDEng, arialFont, Brushes.Black, IDEngLoc);
                    geng.DrawString(IssueDateEng, arialFont, Brushes.Black, IssueDateEngLoc);
                }
            }

            // Save the generated card
            using (MemoryStream memorydari = new MemoryStream())
            using (MemoryStream memoryeng = new MemoryStream())
            {
                using (FileStream fsdari = new FileStream(imageDariOutput, FileMode.Create, FileAccess.ReadWrite))
                using (FileStream fseng = new FileStream(imageEngOutput, FileMode.Create, FileAccess.ReadWrite))
                {
                    daribitmap.Save(memorydari, ImageFormat.Jpeg);
                    engbitmap.Save(memoryeng, ImageFormat.Jpeg);
                    byte[] bytesdari = memorydari.ToArray();
                    byte[] byteseng = memoryeng.ToArray();
                    fsdari.Write(bytesdari, 0, bytesdari.Length);
                    fseng.Write(byteseng, 0, byteseng.Length);
                }
            }
            //html = html.Replace("{imgPath}", Convert.ToString(imageDariOutput));

        }
        else
        {
            if (type.Equals("DailyUserReport"))
            {

                string reportdate = Request.QueryString["reportdate"];
                string reporttype = Request.QueryString["reporttype"];
                string shamsidate = Request.QueryString["shamsidate"];
                string issent = Request.QueryString["issent"];

                string rolename = Request.QueryString["rolename"];

                reader = plus.Data.DAL.Read(pathArray[0],
                            string.Format("SELECT * FROM {0}.{1} WHERE RoleID=@RoleID AND ReportDate = @ReportDate and ReportType=@ReportType and Issent=@Issent order by Total DESC", pathArray[1], pathArray[2])
                            , "RoleID", recordid, "ReportDate", reportdate, "ReportType", reporttype, "Issent", issent);

                String tbody = " <page size='A4' class='Print'>";
                tbody += "<table class='table' style='width:90%;'>";

                String thead = "";

                thead += "<thead>";
                thead += "<tr>";
                thead += "<th colspan='10' style='border:none; font-size:16px;'>گزارش روزانه کارمندان بخش $RoleName بابت تاریخ $rptdate </th>";
                thead += "</tr>";

                thead += "<tr>";
                thead += "<th> شماره</th>";
                thead += "<th> اسم کارمند</th>";
                thead += "<th> دیپارتمنت</th>";
                thead += "<th> تاریخ</th>";
                thead += "<th> نوع گزارش</th>";
                thead += "<th>حالت اسناد</th>";
                thead += "<th>تعداد</th>";
                thead += "</tr>";
                thead += "</thead>";

                thead = thead.Replace("$RoleName", rolename);
                thead = thead.Replace("$rptdate", shamsidate);

                tbody += thead;
                tbody += "<tbody>";

                int counter = 1;

                while (reader.Read())
                {
                    string trow = "<tr>";
                    trow += "<td>$NO</td>";
                    trow += "<td>$FullName</td>";
                    trow += "<td>$RoleName</td>";
                    trow += "<td>$ReportDate_Shamsi</td>";
                    trow += "<td>$RptTypeName</td>";
                    trow += "<td>$SENTName</td>";
                    trow += "<td>$Total</td>";
                    trow += "</tr>";

                    trow = trow.Replace("$NO", counter.ToString());

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);
                        trow = trow.Replace("$" + colName, Convert.ToString(reader[colName]));
                    }

                    tbody += trow;
                    counter++;
                }
                tbody += "</tbody></table> <br/>";
                tbody += "<div style='margin-right:20px; margin-left:20px;'>";

                System.Data.Common.DbDataReader summaryReader = plus.Data.DAL.Read(pathArray[0],
                    string.Format("exec [rpt].[spGetGrandTotalDailyUsersReport] {0}, '{1}', {2}, {3}", recordid, reportdate, reporttype, issent));

                String sthead = "";
                String summary = "<table class='table' style='width:90%;'>";
                String stbody = "<tbody>";


                sthead += "<thead>";
                sthead += "<tr>";
                sthead += "<th colspan='10' style='border:none; font-size:16px;'>مجموع عمومی:</th>";
                sthead += "</tr>";

                sthead += "</thead>";

                summary += sthead;

                while (summaryReader.Read())
                {

                    stbody += "<tr>";
                    stbody += "<th>@GrandTotal</th>";
                    stbody += "</tr>";


                    for (int i = 0; i < summaryReader.FieldCount; i++)
                    {
                        string colName = summaryReader.GetName(i);

                        stbody = stbody.Replace("@" + colName, Convert.ToString(summaryReader[colName]));
                    }

                }
                summary += stbody;
                summary += "</table>";

                tbody += summary;
                tbody += "</div>";
                tbody += "</page>";

                html = html.Replace("$body", tbody);
            }

            else if (!type.Equals("closedlist"))
            {

                string keyCol = Request.QueryString["keyCol"] ?? "ID";
                reader = plus.Data.DAL.Read(pathArray[0],
                    string.Format("SELECT * FROM {0}.{1} WHERE {2} = @RecordID", pathArray[1], pathArray[2], keyCol)
                    , "RecordID", recordid);



                if (type.Equals("application"))
                {

                    while (reader.Read())
                    {
                        // loop through columns


                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colName = reader.GetName(i); // firstName
                            if (!colName.Equals("BirthDate"))
                            {
                                html = html.Replace("{" + colName + "}", Convert.ToString(reader[colName]));
                            }
                            else
                            {
                                html = html.Replace("{" + colName + "}", Convert.ToDateTime(reader[colName]).ToString("dd MMM yyyy"));
                            }
                        }
                    }

                }

                else if (type.Equals("progresslist"))
                {

                    string subpath = "preport_rpt_vUserReport";
                    string[] subPathArray = subpath.Split('_');
                    string subKeyCol = "ID";
                    System.Data.Common.DbDataReader subReader = plus.Data.DAL.Read(subPathArray[0],
                        string.Format("SELECT * FROM {0}.{1} WHERE {2} = @RecordID", subPathArray[1], subPathArray[2], subKeyCol)
                        , "RecordID", recordid);

                    String tbody = " <page size='A4' class='Print'>";
                    tbody += "<table class='table' style='font-size:10px !important;'>";

                    String thead = "";

                    thead += "<thead>";
                    thead += "<tr>";
                    thead += "<th colspan='10' style='border:none; font-size:14px;'>گزارش نمبر $CODE از فعالیت و کارکرد مورخ $SHAMSI محترم $Employee  کارمند بخش $RoleName</th>";
                    thead += "</tr>";
                    thead += "<tr>";
                    thead += "<th> شماره</th>";
                    thead += "<th> کود متقاضی</th>";
                    thead += "<th> نام متقاضی</th>";
                    thead += "<th>نام پدر</th>";
                    thead += "<th>نام پدرکلان</th>";
                    thead += "<th>محل تولد</th>";
                    thead += "<th> تاریخ تولد</th>";
                    thead += "<th> تاریخ اجرأت کارمند</th>";
                    thead += "<th> زمان اجرأت کارمند</th>";
                    thead += "<th> مرحله ارسالی</th>";
                    thead += "</tr>";
                    thead += "</thead>";

                    while (subReader.Read())
                    {
                        for (int i = 0; i < subReader.FieldCount; i++)
                        {
                            string subColName = subReader.GetName(i);
                            thead = thead.Replace("$" + subColName, Convert.ToString(subReader[subColName]));
                        }
                    }

                    tbody += thead;
                    tbody += "<tbody>";


                    int counter = 1;

                    while (reader.Read())
                    {
                        string trow = "<tr>";
                        trow += "<td>$NO</td>";
                        trow += "<td>$Code</td>";
                        trow += "<td>$FullName</td>";
                        trow += "<td>$FatherNameLocal</td>";
                        trow += "<td>$GrandFatherNameLocal</td>";
                        trow += "<td>$BirthLocation</td>";
                        trow += "<td>$BirthDate</td>";
                        trow += "<td>$ProcessedDate</td>";
                        trow += "<td>$ProcessedTime</td>";
                        trow += "<td>$ProcessName</td>";
                        trow += "</tr>";

                        trow = trow.Replace("$NO", counter.ToString());

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colName = reader.GetName(i);
                            trow = trow.Replace("$" + colName, Convert.ToString(reader[colName]));
                        }

                        tbody += trow;
                        counter++;
                    }
                    tbody += "</tbody></table> <br/>";
                    tbody += "<div style='margin-right:10px; margin-left:10px;'>";

                    String sthead = "";
                    String summary = "<table class='table' style='font-size:16px; width:90%;'>";
                    String stbody = "<tbody>";

                    sthead += "<tr>";
                    sthead += "<th>امضأ و نام مکمل کارمند</th>";
                    sthead += "<th>امضأ و نام مکمل آمر بخش</th>";
                    sthead += "<th>امضأ و نام مکمل تسلیم شونده</th>";

                    sthead += "</tr>";
                    sthead += "</thead>";

                    summary += sthead;

                    stbody += "<tr style='height:100px;'>";
                    stbody += "<th></th>";
                    stbody += "<th></th>";
                    stbody += "<th></th>";
                    stbody += "</tr>";

                    summary += stbody;
                    summary += "</table>";

                    tbody += summary;
                    tbody += "</div>";
                    tbody += "</page>";
                    html = html.Replace("$body", tbody);
                }

                else if (type.Equals("postprogresslist"))
                {

                    string subpath = "preport_rpt_vUserReport";
                    string[] subPathArray = subpath.Split('_');
                    string subKeyCol = "ID";
                    System.Data.Common.DbDataReader subReader = plus.Data.DAL.Read(subPathArray[0],
                        string.Format("SELECT * FROM {0}.{1} WHERE {2} = @RecordID", subPathArray[1], subPathArray[2], subKeyCol)
                        , "RecordID", recordid);

                    String tbody = " <page size='A4' class='Print'>";
                    tbody += "<table class='table' style='font-size:10px !important;'>";

                    String thead = "";

                    thead += "<thead>";
                    thead += "<tr>";
                    thead += "<th colspan='10' style='border:none; font-size:14px;'>گزارش نمبر $CODE از فعالیت و کارکرد مورخ $SHAMSI محترم $Employee  کارمند بخش $RoleName</th>";
                    thead += "</tr>";
                    thead += "<tr>";
                    thead += "<th> شماره</th>";
                    thead += "<th> نمبر پاسپورت</th>";
                    thead += "<th> نام متقاضی</th>";
                    thead += "<th>نام پدر</th>";
                    thead += "<th>نام پدرکلان</th>";
                    thead += "<th>محل تولد</th>";
                    thead += "</tr>";
                    thead += "</thead>";

                    while (subReader.Read())
                    {
                        for (int i = 0; i < subReader.FieldCount; i++)
                        {
                            string subColName = subReader.GetName(i);
                            thead = thead.Replace("$" + subColName, Convert.ToString(subReader[subColName]));
                        }
                    }

                    tbody += thead;
                    tbody += "<tbody>";


                    int counter = 1;

                    while (reader.Read())
                    {
                        string trow = "<tr>";
                        trow += "<td>$NO</td>";
                        trow += "<td>$PassportNumber</td>";
                        trow += "<td>$FullName</td>";
                        trow += "<td>$FatherNameLocal</td>";
                        trow += "<td>$GrandFatherNameLocal</td>";
                        trow += "<td>$BirthLocation</td>";
                        trow += "</tr>";

                        trow = trow.Replace("$NO", counter.ToString());

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colName = reader.GetName(i);
                            trow = trow.Replace("$" + colName, Convert.ToString(reader[colName]));
                        }

                        tbody += trow;
                        counter++;
                    }
                    tbody += "</tbody></table> <br/>";
                    tbody += "<div style='margin-right:10px; margin-left:10px;'>";

                    String sthead = "";
                    String summary = "<table class='table' style='font-size:16px; width:90%;'>";
                    String stbody = "<tbody>";

                    sthead += "<tr>";
                    sthead += "<th>امضأ و نام مکمل کارمند</th>";
                    sthead += "<th>امضأ و نام مکمل آمر بخش</th>";
                    sthead += "<th>امضأ و نام مکمل تسلیم شونده</th>";

                    sthead += "</tr>";
                    sthead += "</thead>";

                    summary += sthead;

                    stbody += "<tr style='height:100px;'>";
                    stbody += "<th></th>";
                    stbody += "<th></th>";
                    stbody += "<th></th>";
                    stbody += "</tr>";

                    summary += stbody;
                    summary += "</table>";

                    tbody += summary;
                    tbody += "</div>";
                    tbody += "</page>";
                    html = html.Replace("$body", tbody);
                }

                else if (type.Equals("printedlist"))
                {

                    string subpath = "RegistryServices_stc_vStockOut";
                    string[] subPathArray = subpath.Split('_');
                    string subKeyCol = "ID";
                    System.Data.Common.DbDataReader subReader = plus.Data.DAL.Read(subPathArray[0],
                        string.Format("SELECT * FROM {0}.{1} WHERE {2} = @RecordID", subPathArray[1], subPathArray[2], subKeyCol)
                        , "RecordID", recordid);

                    String tbody = " <page size='A4' class='Print'>";
                    tbody += "<table class='table'>";

                    String thead = "";

                    thead += "<thead>";
                    thead += "<tr>";
                    thead += "<th colspan='10' style='border:none; font-size:16px;'> گزارش پاسپورت های چاپ شده از نمبر ($StartSerial) الی نمبر ($EndSerial) <br/> که به تاریخ $Date در جمع $FullName ثبت گردیده</th>";
                    thead += "</tr>";
                    thead += "<tr>";
                    thead += "<th> شماره</th>";
                    thead += "<th> نمبر پاسپورت</th>";
                    thead += "<th> نام متقاضی</th>";
                    thead += "<th> پاسپورت</th>";
                    thead += "<th>درخواست</th>";
                    thead += "<th> جریمه</th>";
                    thead += "<th> مدت</th>";
                    thead += "<th> پرداخت</th>";
                    thead += "<th>قیمت</th>";
                    thead += "<th> تاریخ صدور</th>";
                    thead += "<th> حالت</th>";
                    thead += "</tr>";
                    thead += "</thead>";

                    while (subReader.Read())
                    {
                        for (int i = 0; i < subReader.FieldCount; i++)
                        {
                            string subColName = subReader.GetName(i);
                            thead = thead.Replace("$" + subColName, Convert.ToString(subReader[subColName]));
                        }
                    }

                    tbody += thead;
                    tbody += "<tbody>";


                    int counter = 1;

                    while (reader.Read())
                    {
                        string trow = "<tr>";
                        trow += "<td>$NO</td>";
                        trow += "<td>$PassportNumber</td>";
                        trow += "<td>$FullName</td>";
                        trow += "<td>$PassportType</td>";
                        trow += "<td>$ApplicationType</td>";
                        trow += "<td>$FineName</td>";
                        trow += "<td>$DurationName</td>";
                        trow += "<td>$PaymentType</td>";
                        trow += "<td>$PaymentPrice</td>";
                        trow += "<td>$IssueDate_Shamsi</td>";
                        trow += "<td>$StatusName</td>";
                        trow += "</tr>";

                        trow = trow.Replace("$NO", counter.ToString());

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colName = reader.GetName(i);
                            trow = trow.Replace("$" + colName, Convert.ToString(reader[colName]));
                        }

                        tbody += trow;
                        counter++;
                    }
                    tbody += "</tbody></table> <br/>";
                    tbody += "<div style='margin-right:20px; margin-left:20px;'>";

                    System.Data.Common.DbDataReader summaryReader = plus.Data.DAL.Read(subPathArray[0],
                        string.Format("exec [rpt].[spGetPassportCountReportByStockOutId] {0}, {1}, {2}", 1, recordid, "Status")
                        , "RecordID", recordid);

                    String sthead = "";
                    String summary = "<table class='table' style='width:90%;'>";
                    String stbody = "<tbody>";


                    sthead += "<thead>";
                    sthead += "<tr>";
                    sthead += "<th colspan='10' style='border:none; font-size:16px;'>خلص گزارش پاسپورت های چاپ شده نظر به مقدار پرداخت</th>";
                    sthead += "</tr>";
                    sthead += "<tr>";
                    sthead += "<th>نوعیت پرداخت</th>";
                    sthead += "<th> مقدار پرداخت</th>";
                    sthead += "<th> باطل شده</th>";
                    sthead += "<th>تائید شده</th>";
                    sthead += "<th>مجموع چاپ شده</th>";
                    sthead += "</tr>";
                    sthead += "</thead>";

                    summary += sthead;

                    while (summaryReader.Read())
                    {

                        stbody += "<tr>";
                        stbody += "<th>@PaymentType</th>";
                        stbody += "<th>@PaymentPrice</th>";
                        stbody += "<th>@SPOILED</th>";
                        stbody += "<th>@PRINTED</th>";
                        stbody += "<th>@TOTAL</th>";
                        stbody += "</tr>";


                        for (int i = 0; i < summaryReader.FieldCount; i++)
                        {
                            string colName = summaryReader.GetName(i);

                            stbody = stbody.Replace("@" + colName, Convert.ToString(summaryReader[colName]));
                        }

                    }
                    summary += stbody;
                    summary += "</table>";

                    tbody += summary;
                    tbody += "</div>";
                    tbody += "</page>";

                    html = html.Replace("$body", tbody);
                }
            }




            else if (type.Equals("closedlist"))
            {

                string start = Request.QueryString["start"];
                string end = Request.QueryString["end"];

                string starts = Request.QueryString["starts"];
                string ends = Request.QueryString["ends"];

                string postofficename = Request.QueryString["postofficename"];

                reader = plus.Data.DAL.Read(pathArray[0],
                            string.Format("SELECT * FROM {0}.{1} WHERE ClosedDate BETWEEN '{2}' AND '{3}' AND PostOfficeID = @PostOffice", pathArray[1], pathArray[2], start, end)
                            , "PostOffice", recordid);

                String tbody = " <page size='A4' class='Print'>";
                tbody += "<table class='table'>";

                String thead = "";

                thead += "<thead>";
                thead += "<tr>";
                thead += "<th colspan='10' style='border:none; font-size:16px;'>گزارش درخواست های مربوط پسته خانه $PostOffice که از تاریخ $starts الی تاریخ $ends تکمیل و نهائی گردیده اند</th>";
                thead += "</tr>";

                thead += "<tr>";
                thead += "<th> شماره</th>";
                thead += "<th> نمبر پاسپورت</th>";
                thead += "<th> نام متقاضی</th>";
                thead += "<th> نام پدر</th>";
                thead += "<th> نام پدر کلان</th>";
                thead += "<th>محل تولد</th>";
                thead += "<th>پوسته خانه</th>";
                thead += "<th>نمبر تلفون</th>";

                thead += "<th>ملاحضات</th>";
                thead += "</tr>";
                thead += "</thead>";

                thead = thead.Replace("$PostOffice", postofficename);
                thead = thead.Replace("$starts", starts);
                thead = thead.Replace("$ends", ends);
                tbody += thead;
                tbody += "<tbody>";


                int counter = 1;

                while (reader.Read())
                {
                    string trow = "<tr>";
                    trow += "<td>$NO</td>";
                    trow += "<td>$PassportNumber</td>";
                    trow += "<td>$FullName</td>";
                    trow += "<td>$FatherNameLocal</td>";
                    trow += "<td>$GrandFatherNameLocal</td>";
                    trow += "<td>$LocationName</td>";
                    trow += "<td>$PostOffice</td>";
                    trow += "<td>$Mobile</td>";

                    trow += "<td style='width:150px;'></td>";
                    trow += "</tr>";

                    trow = trow.Replace("$NO", counter.ToString());

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string colName = reader.GetName(i);
                        trow = trow.Replace("$" + colName, Convert.ToString(reader[colName]));
                    }

                    tbody += trow;
                    counter++;
                }
                tbody += "</tbody>";
                tbody += "</page>";
                html = html.Replace("$body", tbody);
            }




            reader.Close();
            Response.Write(html);
        }

		

    }
	
	public void ExportExcel(){
		ExcelPackage pck = new ExcelPackage();
		var ws = pck.Workbook.Worksheets.Add("Sample1");
		ws.View.RightToLeft = true;
		System.Data.Common.DbDataReader reader = null;

		
		String TypeString = Request.QueryString["Type"];
        String path = Request.QueryString["Path"].Split('.')[0];

		if (TypeString.Equals("timesheet"))
        {
            System.Collections.IDictionary param = new Dictionary<string, object>();
            
			
			var UserID = Request.QueryString["userID"];
            var OrgUnit = Request.QueryString["orgID"];
            var start = Request.QueryString["start"];
            var end = Request.QueryString["end"];
            var rank = Request.QueryString["rank"];
			
			param.Add("ID", null);
            param.Add("OrganizationUnitID", null);
            param.Add("StartDate", null);
            param.Add("EndDate", null);
            param.Add("Rank", null);
            
			if(UserID.Length != 0)
				param["ID"] = UserID;
            if(OrgUnit.Length != 0)
				param["OrganizationUnitID"] = OrgUnit;
			if(start.Length != 0)
				param["StartDate"] = start;
			if(end.Length != 0)
				param["EndDate"] = end;
			if(rank.Length != 0)
				param["Rank"] = rank;
            param.Add("CreatedBy", 1);
            param.Add("Offset", 0);
            param.Add("Limit", 0);
            int typeID = 0;
            try
            {
                typeID = Convert.ToInt32(Request.QueryString["typeID"]);
            }
            catch (Exception) { }

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[path].ToString());
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "att.spGetMonthlyTimeSheet";
                foreach (var ent in param.Keys)
                {
                    command.Parameters.Add(new SqlParameter( ent.ToString(), param[ent]));
                }
                command.CommandTimeout = 200;
                reader = command.ExecuteReader();

            };
            

            //reader = plus.Data.DAL.Read("AMS", "att.spGetMonthlyTimeSheet",param);

            Dictionary<int, String> maps = new Dictionary<int, string>();

            maps.Add(2, "PR");
            maps.Add(3, "AB");
            maps.Add(4, "HD");
            maps.Add(5, "ALH");
            maps.Add(6, "AL");


            StringBuilder sb = new StringBuilder();
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th></th><th></th><th></th><th></th><th></th><th></th>");
			
			
			char a = 'A';
			ws.Cells[a+""+1].Value = "کود";a++;
			ws.Cells[a+""+1].Value = "نام مکمل";a++;
			ws.Cells[a+""+1].Value = "نام پدر";a++;
			ws.Cells[a+""+1].Value = "وظیفه";a++;
			ws.Cells[a+""+1].Value = "بست";a++;
			ws.Cells[a+""+1].Value = "اداره";a++;
			char b = ' ';
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (i > 6)
                {
					if(a > 'Z'){
						b = 'A';
						a = 'A';
					}
					
						String val = reader.GetName(i).Split('_')[2];
						ws.Cells[b+""+a+""+1].Value = val;
						a++;
					
                }
            }
			int g = 2;
			
            while (reader.Read())
            {
                a = 'A';
				b = ' ';
                for(int i = 1; i < reader.FieldCount; i++)
                {
                    if (i < 7 || (typeID == 0 || typeID == 1)){
						String val = reader[i].ToString();
						ws.Cells[b+""+a+""+g].Value = val;
					}
                    else
                    {
                        string s = reader[i].ToString();
                        if ( maps[typeID].Equals(s))
                        {
							String val = reader[i].ToString();
							ws.Cells[b+""+a+""+g].Value = val;
						
                        }
                        else
                        {
                            
                        }
                    }
					a++;
					if(a > 'Z'){
						a = 'A';
						b = 'A';
					}
                }
				g++;
            }

            conn.Close();
        }

        else if (TypeString.Equals("attreport"))
        {

            int typeID = 0;
            try
            {
                typeID = Convert.ToInt32(Request.QueryString["typeID"]);
            }
            catch (Exception) { }

            System.Collections.IDictionary param = new Dictionary<string, object>();
            var UserID = Request.QueryString["userID"];
            var OrgUnit = Request.QueryString["orgID"];
            var start = Request.QueryString["start"];
            var end = Request.QueryString["end"];
            var rank = Request.QueryString["rank"];

            param.Add("UserID", null);
            param.Add("OrganizationUnit", null);
            param.Add("StartDate", null);
            param.Add("EndDate", null);
            param.Add("RankID", null);
            param.Add("AttID", null);
            param.Add("HRCode", null);
            param.Add("CreatedBy", null);
            param.Add("Type", null);
            if (UserID.Length != 0)
                param["UserID"] = UserID;
            if (OrgUnit.Length != 0)
                param["OrganizationUnit"] = OrgUnit;
            if (start.Length != 0)
                param["StartDate"] = start;
            if (end.Length != 0)
                param["EndDate"] = end;
            if (rank.Length != 0)
                param["RankID"] = rank;
            if (typeID != 0)
                param["Type"] = typeID;

            reader = plus.Data.DAL.Read("AMS", "att.spGenerateTimeSheet", param);



            StringBuilder sb = new StringBuilder();
            
      
			char a = 'A';
			ws.Cells[a+""+1].Value = " کود کادری ";a++;
			ws.Cells[a+""+1].Value = "نام مکمل";a++;
			ws.Cells[a+""+1].Value = "نام پدر";a++;
			ws.Cells[a+""+1].Value = "وظیفه";a++;
			ws.Cells[a+""+1].Value = "بست";a++;
			ws.Cells[a+""+1].Value = "ریاست";a++;
			ws.Cells[a+""+1].Value = "شیفت";a++;
			ws.Cells[a+""+1].Value = "تاریخ";a++;
			ws.Cells[a+""+1].Value = "وقت دخول";a++;
			ws.Cells[a+""+1].Value = "وقت خروج";a++;
			ws.Cells[a+""+1].Value = "حاضری";a++;
			char b = ' ';


			int g = 2;
            while (reader.Read())
            {
				a = 'A';
				b = ' ';
                for (int i = 1; i < reader.FieldCount; i++)
                {
                    if(!reader.GetName(i).Equals("AttType"))
						ws.Cells[b+""+a+""+g].Value = reader[i].ToString();
                    a++;
					if(a > 'Z'){
						a = 'A';
						b = 'A';
					}	
                }
				g++;
            }

            
        }

		ws.Cells[ws.Dimension.Address].AutoFitColumns();
		
		pck.SaveAs(Response.OutputStream);
		Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
		Response.AddHeader("content-disposition", "attachment;  filename=Sample1.xlsx");
		
	}
    public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var newImage = new Bitmap(newWidth, newHeight);
        Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
        return newImage;
    }
}