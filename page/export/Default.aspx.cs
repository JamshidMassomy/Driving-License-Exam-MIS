using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using OfficeOpenXml.Style;

public partial class page_export_Default : System.Web.UI.Page
{
    public static StringBuilder Log = new StringBuilder();
    System.Drawing.Color HeaderColor = System.Drawing.Color.FromArgb(31, 78, 120);
    System.Drawing.Color SubColor = System.Drawing.Color.FromArgb(184, 204, 228);
    System.Drawing.Color TotalColor = System.Drawing.Color.FromArgb(15, 36, 62);

    protected void Page_Load(object sender, EventArgs eventArgs)
    {

        Log.Clear();
        string url = Request.QueryString["url"];
        string html = "";
        string recordid = Request.QueryString["recordid"];
        string path = Request.QueryString["altPath"] ?? Request.QueryString["path"];
        string commandType = Request.QueryString["CommandType"] ?? "table";
        string type = Request.QueryString["type"] ?? "excel";
        
        string[] pathArray = path.Contains('_') ? path.Split('_') : path.Split('.'); // [DB,SCHEMA,OBJECT]
        OfficeOpenXml.ExcelPackage _worksheet = null;
        if (Request.QueryString.AllKeys.Contains("Group"))
        {
            int group = Convert.ToInt32(Request.QueryString["Group"]);
            _worksheet = new ExcelPackage(new MemoryStream(File.ReadAllBytes(Server.MapPath(url))));
            ExcelWorksheet ws = _worksheet.Workbook.Worksheets[1];
            ws.CustomHeight = true;

            ExcelNamedRangeCollection names = _worksheet.Workbook.Names;
            List<Address> nList = new List<Address>();
            foreach (var name in names)
            {
                nList.Add(new Address { Name = name.Name, SRow = name.Start.Row, SColumn = name.Start.Column, ERow = name.End.Row, EColumn = name.End.Column });
            }
            nList.ForEach(f => _worksheet.Workbook.Names.Remove(f.Name));
        System.Data.DataSet ds = plus.Data.DAL.GetDataSet(pathArray[0],
            string.Format(
            commandType == "table" ?
                "SELECT * FROM [{0}].[{1}] WHERE ID=@ID" :
                    "EXEC [{0}].[{1}] @ID "
                , pathArray[1], pathArray[2]), // SELECT * FROM [SCHEMA].[OBJECT] WHERE ID=@ID"
            "ID", recordid);
            for (int i = 0; i < ds.Tables.Count; i += group)
            {
                List<System.Data.DataTable> ls = new List<System.Data.DataTable>();
                ExcelWorksheet w = _worksheet.Workbook.Worksheets.Add("T" + i, ws);
                w.PrinterSettings.VerticalCentered = true;
                w.PrinterSettings.HorizontalCentered = true;
                w.PrinterSettings.FitToHeight = 1;
                w.PrinterSettings.FitToWidth = 1;
                foreach (var name in nList)
                {
                    ExcelRangeBase rg = w.Cells[name.SRow, name.SColumn, name.ERow, name.EColumn];
                    w.Workbook.Names.Add(name.Name, rg);
                }
                for (int j = 0; j < group; j++)
                {
                    ls.Add(ds.Tables[i + j]);
                }
                Excel.Generate(w, ls);
                nList.ForEach(n => w.Workbook.Names.Remove(n.Name));
                w.CustomHeight = true;
                for (int r = 1; r <= 50; r++)
                {
                    var h = ws.Row(r).Height;
                    w.Row(r).Height = h;
                }
            }

        }
        else if (Request.QueryString.AllKeys.Contains("Custom"))
        {
            System.Data.DataSet ds = plus.Data.DAL.GetDataSet(pathArray[0],
                string.Format(
                commandType == "table" ?
                    "SELECT * FROM [{0}].[{1}] WHERE ID=@ID" :
                    "EXEC [{0}].[{1}] @ID "
                    , pathArray[1], pathArray[2]), // SELECT * FROM [SCHEMA].[OBJECT] WHERE ID=@ID"
                "ID", recordid);
            _worksheet = new ExcelPackage(new MemoryStream(File.ReadAllBytes(Server.MapPath(url))));
            var sheet = _worksheet.Workbook.Worksheets[1];
            Excel.Generate(sheet, ds.Tables[0]);
            int baseRow = sheet.Workbook.Names["TemplateStart"].Start.Row;
            ds.Tables.RemoveAt(0);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                baseRow += LoadProject(sheet, ds.Tables[i], baseRow, i)+ 1;
            }

            var footer = _worksheet.Workbook.Worksheets["Footer"];
            footer.Cells[footer.Dimension.Address].Copy(sheet.Cells[baseRow, 1]);
            //Response.Write(Log.ToString());
            //return;
        }
        else if (Request.QueryString.AllKeys.Contains("BudgetBreakDown"))
        {
            System.Data.DataSet ds = plus.Data.DAL.GetDataSet(pathArray[0],
                string.Format(
                commandType == "table" ?
                    "SELECT * FROM [{0}].[{1}] WHERE ID=@ID" :
                    "EXEC [{0}].[{1}] @ID "
                    , pathArray[1], pathArray[2]), // SELECT * FROM [SCHEMA].[OBJECT] WHERE ID=@ID"
                "ID", recordid);
            _worksheet = new ExcelPackage(new MemoryStream(File.ReadAllBytes(Server.MapPath(url))));
            var sheet = _worksheet.Workbook.Worksheets[1];
            Excel.Generate(sheet, ds.Tables[0]);
            List<DataRow> list = GetRowList(ds.Tables[2].Rows);
            List<DataRow> AllData = GetRowList(ds.Tables[3].Rows);
            List<DataRow> Programs = GetRowList(ds.Tables[1].Rows);

            int StartRow = sheet.Workbook.Names["StartData"].Start.Row;
            int ParentIndex = ds.Tables[2].Columns.IndexOf("ParentID");
            List<DataRow> TopLevel = list.Where(f => !f.Field<int?>("ParentID").HasValue).ToList();
            List<int> TotalRows = new List<int>();
            int ID = 0;
            int Count = 0;
            try { 
                TopLevel.ForEach(top =>
                {
                    ID = top.Field<int>("ID");
                    Count = list.Where(e => e.Field<int?>("ParentID") == top.Field<int>("ID")).Count();
                    DataRow L2 = list.Where(e => e.Field<int?>("ParentID") == top.Field<int>("ID")).Single();
                    List<DataRow> subs = list.Where(e => e.Field<int?>("ParentID") == L2.Field<int>("ID")).ToList();
                    int SumRow;
                    StartRow += LoadObjectCodeFull(sheet,StartRow, top, subs,AllData.Where(e => subs.Select(f => f.Field<int>("ID")).Contains(e.Field<int>("ParentID"))).ToList(), out SumRow);

                    TotalRows.Add(SumRow);
                });
                ExcelHelper helper = new ExcelHelper(null);

                helper.SetRange(sheet.Cells[StartRow , 1, StartRow , 15]).Merge(true).Height(10).BorderAround(ExcelBorderStyle.Medium);
                string formulaTemplate = "SUM(";
                TotalRows.ForEach(e =>
                {
                    formulaTemplate += "{CH}" + (e);
                    if (TotalRows.IndexOf(e) != TotalRows.Count - 1)
                    {
                        formulaTemplate += "+";
                    }
                });
                formulaTemplate += ")";
                string[] colsChar = { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };
                helper.SetRange(sheet.Cells[StartRow + 1, 1, StartRow + 1, 2]).Merge(true).Val("مجموع بودجه")
                    .FillSolid(TotalColor).RightCenter().FontSize(14).TextColor(System.Drawing.Color.White);
                for (int j = 1; j <= colsChar.Length; j++)
                {
                    helper.SetRange(sheet.Cells[StartRow+ 1 , 2 + j]).FillSolid(TotalColor).TextColor(System.Drawing.Color.White)
                        .Formula(formulaTemplate.Replace("{CH}", colsChar[j - 1])).Font("Times New Roman").FontSize(14)
                        .NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)").ShrinkToFit(true);

                }
            }
            catch(Exception ex)
            {
                Response.Write(ID+" "+Count);
                throw ex;
                return;
            }

        }
        else
        {

            System.Data.DataSet ds = plus.Data.DAL.GetDataSet(pathArray[0],
                string.Format(
                commandType == "table" ?
                    "SELECT * FROM [{0}].[{1}] WHERE ID=@ID" :
                    "EXEC [{0}].[{1}] @ID "
                    , pathArray[1], pathArray[2]), // SELECT * FROM [SCHEMA].[OBJECT] WHERE ID=@ID"
                "ID", recordid);
            _worksheet = Excel.Generate(Server.MapPath(url), ds);

        }
       
        /*BarCode*/
        /* System.Drawing.Image _barcode = plus.Transform.Barcode.GetImage("Asan Khedmat Generated Bar code", Zen.Barcode.BarcodeSymbology.Code39NC);
        var barCodeImage = _worksheet.Workbook.Worksheets[1].Drawings.AddPicture("BarCode", _barcode);
        barCodeImage.SetPosition(4, 4);
        barCodeImage.SetSize(150, 50); */
        /*BarCode*/
        // byte[] _bytes = _worksheet.GetAsByteArray();
        // System.IO.MemoryStream _Streamed = new System.IO.MemoryStream(_bytes);
        // Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
        // workbook.LoadFromStream(_Streamed);
        // System.IO.MemoryStream StreamOfExcel = new System.IO.MemoryStream();
        // var fileName = "excel" + DateTime.Now.Ticks;
        // workbook.SaveToStream(StreamOfExcel);
        // Response.ContentType = "application/excel";
        // Response.ContentEncoding = System.Text.Encoding.UTF8;
        // Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");
        // Response.BinaryWrite(StreamOfExcel.ToArray());
        // Response.Flush();
        // Response.End();
		
		Response.BinaryWrite(_worksheet.GetAsByteArray());
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("content-disposition", "attachment;  filename=File.xlsx");
        Response.Write(Log.ToString());
        // this gives you pdf fromat 
        //var fileName = "PDF" + DateTime.Now.Ticks;
        //workbook.SaveToStream(_streamForPDF, Spire.Xls.FileFormat.PDF);
        //Response.ContentType = "application/pdf";
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".pdf");
        //Response.BinaryWrite(_streamForPDF.ToArray());
        //Response.Flush();
        //Response.End();
        
    }

    private int LoadObjectCodeFull(ExcelWorksheet sheet,int startRow, DataRow top, List<DataRow> subs, List<DataRow> rows, out int sumRow)
    {
        int TotalRowsAdded = 1;
        ExcelHelper helper = new ExcelHelper(sheet.Cells[startRow, 1]);
        helper.Val(top.Field<string>("Code")).AlignCenter().Height(21).TextColor(System.Drawing.Color.White).FillSolid(HeaderColor);
        helper.SetRange(sheet.Cells[startRow, 2, startRow, 15]).Merge(true)
            .RightCenter().TextColor(System.Drawing.Color.White).FillSolid(HeaderColor).Val(top.Field<string>("Dari"));
        helper.SetRange(sheet.Cells[startRow,1,startRow,15]).AllBorderAround(ExcelBorderStyle.Medium).FontSize(16);
        int stRow = startRow + 1;
        List<int> Sums = new List<int>();
        subs.ForEach(s =>
        {
            int SumRow;
            TotalRowsAdded += LoadSubLevelObjectCode(sheet, startRow + TotalRowsAdded, s, rows.Where(e => e.Field<int?>("ParentID") == s.Field<int>("ID")).ToList(), out SumRow);
            Sums.Add(SumRow);
        });

        helper.SetRange(sheet.Cells[startRow + TotalRowsAdded, 1,startRow+TotalRowsAdded,2]).Merge(true).Height(19.5)
            .FillSolid(TotalColor).RightCenter().TextColor(System.Drawing.Color.White).Val("مجموع کود "+top.Field<string>("Code"))
            .Font("Times New Roman").FontSize(14);

        string formulaTemplate = "SUM(";
        Sums.ForEach(e =>
        {
            formulaTemplate += "{CH}" + (e);
            if (Sums.IndexOf(e) != Sums.Count - 1)
            {
                formulaTemplate += "+";
            }
        });
        formulaTemplate += ")";

        string[] colsChar = { "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

        for(int j=1;j<= colsChar.Length; j++)
        {
            helper.SetRange(sheet.Cells[startRow + TotalRowsAdded, 2 + j]).FillSolid(TotalColor).TextColor(System.Drawing.Color.White)
                .Formula(formulaTemplate.Replace("{CH}", colsChar[j - 1])).Font("Times New Roman").FontSize(14)
                .NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)").ShrinkToFit(true);
        }
        
        sumRow = startRow + TotalRowsAdded;
        TotalRowsAdded++;

        return TotalRowsAdded;
    }

    private int LoadSubLevelObjectCode(ExcelWorksheet sheet, int startRow, DataRow top, List<DataRow> list, out int sumRow)
    {
        int RowsAdded = 0;
        ExcelHelper helper = new ExcelHelper(sheet.Cells[startRow, 1]);
        helper.AlignCenter().FillSolid(SubColor).Val(top.Field<string>("Code")).Height(18);
        helper.SetRange(sheet.Cells[startRow, 2, startRow, 15]).RightCenter().Merge(true).FillSolid(SubColor).Val(top.Field<string>("Dari"));
        helper.SetRange(sheet.Cells[startRow, 1, startRow, 15]).AllBorderAround(ExcelBorderStyle.Thin).BottomBorder(ExcelBorderStyle.Medium).FontSize(12).Bold(true);

        RowsAdded++;
        String LastYear = "LastYearBudget";
        String CurYear = "CurrentYearBudget";
        list.ForEach(rw =>
        {
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 1]).Height(18).RightCenter().Val(rw.Field<string>("ObjectCode"));
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 2]).RightCenter().Val(rw.Field<string>("ObjectCodeName")).ShrinkToFit(true);
            decimal sumLastYear = 0.0M;
            decimal sumCurYear = 0.0M;
            for(int i = 1; i < 5; i++)
            {
                if(rw.Table.Columns.IndexOf(LastYear+i) > -1)
                {
                    var val = rw.Field<decimal?>(LastYear + i);
                    sumLastYear += val.HasValue ? val.Value : 0M;
                    helper.SetRange(sheet.Cells[startRow + RowsAdded, 3 + i]).NumberFormat("#,##0").Font("Times New Roman").Val(val.HasValue ? val / 1000 : 0);
                }
                
                if(rw.Table.Columns.IndexOf(CurYear+i) > -1)
                {
                    var val = rw.Field<decimal?>(CurYear + i);
                    sumCurYear += val.HasValue ? val.Value : 0M;
                    helper.SetRange(sheet.Cells[startRow + RowsAdded, 9 + i]).NumberFormat("#,##0").Font("Times New Roman").Val(val.HasValue ? val / 1000 : 0);
                }
            }
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 3]).NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)").Formula("SUM(D" + (startRow + RowsAdded) + ":H" + (startRow + RowsAdded) + ")");
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 9]).NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)").TextColor(System.Drawing.Color.White).FillSolid(HeaderColor).Formula("SUM(D" + (startRow + RowsAdded) + ":H" + (startRow + RowsAdded) + ")");
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 15]).NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)").TextColor(System.Drawing.Color.White).FillSolid(HeaderColor).Formula("SUM(J" + (startRow + RowsAdded) + ":N" + (startRow + RowsAdded) + ")");
            helper.SetRange(sheet.Cells[startRow + RowsAdded, 1, startRow + RowsAdded, 15]).AllBorderAround(ExcelBorderStyle.Thin).FontSize(12);


            RowsAdded++;

        });
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 1]) .Height(18).Val("مجموع").RightCenter();
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 2]).RightCenter().Val(top.Field<string>("Code") + " - " + top.Field<string>("Dari"));
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 3]) .Formula("SUM(C" + (startRow + 1) +  ":C" + (startRow + RowsAdded - 1)+")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 4]) .Formula("SUM(D" + (startRow + 1) +  ":D" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 5]) .Formula("SUM(E" + (startRow + 1) +  ":E" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 6]) .Formula("SUM(F" + (startRow + 1) +  ":F" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 7]) .Formula("SUM(G" + (startRow + 1) +  ":G" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 8]) .Formula("SUM(H" + (startRow + 1) +  ":H" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 9]) .Formula("SUM(I" + (startRow + 1) +  ":I" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 10]).Formula("SUM(J" + (startRow + 1) + ":J" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 11]).Formula("SUM(K" + (startRow + 1) + ":K" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 12]).Formula("SUM(L" + (startRow + 1) + ":L" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 13]).Formula("SUM(M" + (startRow + 1) + ":M" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 14]).Formula("SUM(N" + (startRow + 1) + ":N" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 15]).Formula("SUM(O" + (startRow + 1) + ":O" + (startRow + RowsAdded - 1) + ")");
        helper.SetRange(sheet.Cells[startRow + RowsAdded, 1, startRow + RowsAdded, 15]).TextColor(System.Drawing.Color.White)
            .FillSolid(HeaderColor).NumberFormat("_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)")
            .Font("Times New Roman").FontSize(12);

        sumRow = startRow + RowsAdded;
        RowsAdded++;
        return RowsAdded;
    }

    private List<DataRow> GetRowList(DataRowCollection rows)
    {
        List<DataRow> list = new List<DataRow>();
        foreach(DataRow dr in rows)
        {
            list.Add(dr);
        }
        return list;
    }

    public int LoadProject(ExcelWorksheet sheet, DataTable table, int baseRow, int index)
    {
        int r = 0;

        int M22Col = sheet.Workbook.Names["M22Total"].Start.Column;
        int M25Col = sheet.Workbook.Names["M25Total"].Start.Column;
        int TotalCol = sheet.Workbook.Names["Total"].Start.Column;

        decimal Total22 = 0;
        decimal Total25 = 0;
        decimal Total = 0;

        for ( r = 0; r < table.Rows.Count; r++)
        {
            decimal SumRow = 0;
            foreach(DataColumn col in table.Columns)
            {
                if (sheet.Workbook.Names.ContainsKey(col.ColumnName))
                {
                    int SColumn = sheet.Workbook.Names[col.ColumnName].Start.Column;
                    int EColumn = sheet.Workbook.Names[col.ColumnName].End.Column;

                    var cell= sheet.Cells[baseRow + r, SColumn, baseRow + r, EColumn];
                    if (SColumn != EColumn) cell.Merge = true;
                    //Log.Append("Writing " + (baseRow + r) + "-" + SColumn + "-" + EColumn+"="+table.Rows[r][col.ColumnName]+"-COLNAME="+col.ColumnName+"</br>");
                    cell.Style.ShrinkToFit = true;
                    cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 235));
                    cell.Value = table.Rows[r][col.ColumnName];


                    if(col.ColumnName == "M22Total")
                    {
                        int val = Convert.ToInt32(table.Rows[r][col.ColumnName]);
                        SumRow += val;
                        Total22 += val;
                    }
                    else if(col.ColumnName == "M25Total")
                    {
                        int val = Convert.ToInt32(table.Rows[r][col.ColumnName]);
                        SumRow += val;
                        Total25 += val;
                    }
                }
            }
            sheet.Cells[baseRow + r, TotalCol, baseRow + r, TotalCol].Value = SumRow;
        }
        sheet.Row(baseRow + r).Height = 15;

        Total = Total22 + Total25;
        var cells = sheet.Cells[baseRow + r, 1, baseRow + r, M22Col - 1];
        cells.Merge = true;
        cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(242, 242, 242));
        cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        cells.Style.Font.Bold = true;
        cells.Value = "مجموع پروژه";
        var cl22 = sheet.Cells[baseRow + r, M22Col, baseRow + r, M22Col];
        cl22.Value = Total22;
        cl22.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cl22.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(242, 242, 242));
        var cl25 = sheet.Cells[baseRow + r, M25Col, baseRow + r, M25Col];
        cl25.Value = Total25;
        cl25.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cl25.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(242, 242, 242));
        var clT = sheet.Cells[baseRow + r, TotalCol, baseRow + r, TotalCol];

        clT.Value = Total;
        clT.Style.Fill.PatternType = ExcelFillStyle.Solid;
        clT.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(242, 242, 242));

        var cel = sheet.Cells[baseRow, 1, baseRow + r - 1, 1];
        //cel.Merge = false;
        cel.Merge = true;
        cel.Value = index + 1;
        cel.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        cel.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
        cel.Style.Fill.PatternType = ExcelFillStyle.Solid;
        cel.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 235));

        var range = sheet.Cells[baseRow, 1, baseRow + r, sheet.Dimension.Columns];
        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        range.Style.Border.BorderAround(ExcelBorderStyle.Medium);

        return r;
    }


    public class Address
    {
        public string Name { get; set; }
        public int SRow { get; set; }
        public int SColumn { get; set; }
        public int ERow { get; set; }
        public int EColumn { get; set; }
    }

    public class ExcelHelper
    {
        ExcelRangeBase cells;
        public ExcelHelper(ExcelRangeBase rangeBase)
        {
            this.cells = rangeBase;
        }
        public ExcelHelper SetRange(ExcelRangeBase rangeBase)
        {
            this.cells = rangeBase;
            return this;
        }
        public ExcelHelper Merge(bool merge)
        {
            cells.Merge = merge;
            return this;
        }

        public ExcelHelper BottomBorder(ExcelBorderStyle style)
        {
            cells.Style.Border.Bottom.Style = style;
            return this;
        }
        public ExcelHelper Formula(string f)
        {
            cells.Formula = f;
            return this;
        }
        public ExcelHelper TextColor(System.Drawing.Color cl)
        {
            cells.Style.Font.Color.SetColor(cl);
            return this;
        }
        public ExcelHelper FontSize(float d)
        {
            cells.Style.Font.Size = d;
            return this;
        }
        public ExcelHelper Font(String name)
        {
            cells.Style.Font.Name = name;
            return this;
        }
        public ExcelHelper StyleName(string Name)
        {
            cells.StyleName = Name;
            return this;
        }
        public ExcelHelper NumberFormat(string format)
        {
            cells.Style.Numberformat.Format = format;
            return this;
        }
        public ExcelHelper AlignCenter()
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            return this;
        }
        public ExcelHelper Height(double h)
        {
            cells.Worksheet.Row(cells.Start.Row).Height = h;
            return this;
        }
        public ExcelHelper RightCenter()
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            return this;
        }
        public ExcelHelper LeftCenter()
        {
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            cells.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            return this;
        }
        public ExcelHelper FillSolid(int r,int g,int b)
        {
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(r, b, g));
            return this;
        }

        public ExcelHelper FillSolid(System.Drawing.Color cl)
        {
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(cl);
            return this;
        }
        public ExcelHelper Bold(bool bold)
        {
            cells.Style.Font.Bold = bold;
            return this;
        }

        public ExcelHelper ShrinkToFit(bool shrink)
        {
            cells.Style.WrapText = !shrink;
            cells.Style.ShrinkToFit = shrink;
            return this;
        }
        public ExcelHelper BorderAround(ExcelBorderStyle style)
        {
            cells.Style.Border.BorderAround(style);
            return this;
        }
        public ExcelHelper AllBorderAround(ExcelBorderStyle style)
        {
            cells.Style.Border.Bottom.Style = style;
            cells.Style.Border.Top.Style = style;
            cells.Style.Border.Left.Style = style;
            cells.Style.Border.Right.Style = style;
            return this;
        }
        public ExcelHelper Val(object val)
        {
            cells.Value = val;
            return this;
        }
    }
}