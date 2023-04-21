using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using OfficeOpenXml.Style;


namespace idn.Skycic.Inventory.Utils
{
    public class ExcelImport
    {
        public static DataSet ImportExcelXLS(HttpPostedFileWrapper file, bool hasHeaders)
        {
            string fileName = Path.GetTempFileName();
            file.SaveAs(fileName);

            return ImportExcelXLS(fileName, hasHeaders);
        }
        public static DataSet ImportExcelXLS(string FileName, bool hasHeaders)
        {

            string HDR = hasHeaders ? "Yes" : "No";


            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=1\"";
            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=1\"";
            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                foreach (DataRow row in dt.Rows)
                {
                    string sheet = row["TABLE_NAME"].ToString();

                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable(sheet);
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);
                }
            }
            return output;
        }



        struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
                    case "system.datetime":
                        return DateTime.Parse(input);
                    case "system.decimal":
                        return decimal.Parse(input);
                    case "system.boolean":
                        return bool.Parse(input);
                    default:
                        return input;
                }
            }
        }
        public static DataSet ImportExcelXML(HttpPostedFile file, bool hasHeaders, bool autoDetectColumnType)
        {
            return ImportExcelXML(file.InputStream, hasHeaders, autoDetectColumnType);
        }
        public static DataSet ImportExcelXML(Stream inputFileStream, bool hasHeaders, bool autoDetectColumnType)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(inputFileStream));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            DataSet ds = new DataSet();

            foreach (XmlNode node in doc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                DataTable dt = new DataTable(node.Attributes["ss:Name"].Value);
                ds.Tables.Add(dt);
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {
                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {
                        foreach (XmlNode data in rows[0].SelectNodes("ss:Cell/ss:Data", nsmgr))
                        {
                            columns.Add(new ColumnType(typeof(string)));//default to text
                            dt.Columns.Add(data.InnerText, typeof(string));
                        }
                        startIndex++;
                    }
                    if (autoDetectColumnType && rows.Count > 0)
                    {
                        XmlNodeList cells = rows[startIndex].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), autoDetectType.type);
                                columns.Add(autoDetectType);
                            }
                            else
                            {
                                dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                                columns[actualCellIndex] = autoDetectType;
                            }

                            actualCellIndex++;
                        }
                    }
                    for (int i = startIndex; i < rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        XmlNodeList cells = rows[i].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            XmlNode data = cell.SelectSingleNode("ss:Data", nsmgr);

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                for (int j = dt.Columns.Count; j < actualCellIndex; j++)
                                {
                                    dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                    columns.Add(getDefaultType());
                                }
                                ColumnType autoDetectType = getType(cell.SelectSingleNode("ss:Data", nsmgr));
                                dt.Columns.Add("Column" + actualCellIndex.ToString(), typeof(string));
                                columns.Add(autoDetectType);
                            }
                            if (data != null)
                                row[actualCellIndex] = data.InnerText;

                            actualCellIndex++;
                        }

                        dt.Rows.Add(row);
                    }
                }
            }
            return ds;

            //<?xml version="1.0"?>
            //<?mso-application progid="Excel.Sheet"?>
            //<Workbook>
            // <Worksheet ss:Name="Sheet1">
            //  <Table>
            //   <Row>
            //    <Cell><Data ss:Type="String">Item Number</Data></Cell>
            //    <Cell><Data ss:Type="String">Description</Data></Cell>
            //    <Cell ss:StyleID="s21"><Data ss:Type="String">Item Barcode</Data></Cell>
            //   </Row>
            // </Worksheet>
            //</Workbook>
        }

        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }

        private static ColumnType getType(XmlNode data)
        {
            string type = null;
            if (data.Attributes["ss:Type"] == null || data.Attributes["ss:Type"].Value == null)
                type = "";
            else
                type = data.Attributes["ss:Type"].Value;

            switch (type)
            {
                case "DateTime":
                    return new ColumnType(typeof(DateTime));
                case "Boolean":
                    return new ColumnType(typeof(Boolean));
                case "Number":
                    return new ColumnType(typeof(Decimal));
                case "":
                    decimal test2;
                    if (data == null || String.IsNullOrEmpty(data.InnerText) || decimal.TryParse(data.InnerText, out test2))
                    {
                        return new ColumnType(typeof(Decimal));
                    }
                    else
                    {
                        return new ColumnType(typeof(String));
                    }
                default://"String"
                    return new ColumnType(typeof(String));
            }
        }
    }
    public class ExcelExport
    {
        static int i = 1;
        public static void ExportToExcel(DataSet oDs, string strFileName)
        {
            try
            {
                FileInfo newFile = new FileInfo(strFileName);

                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    //Do the export stuff here..
                    int i = 0;
                    foreach (DataTable odt in oDs.Tables)
                    {


                        i++;
                        string sheetname = null == odt.TableName || odt.TableName.Equals(string.Empty) ? "Sheet" + i.ToString() : odt.TableName;
                        AddSheetsToWorkBookFromDataTable(xlPackage, odt, sheetname);

                    }
                    xlPackage.Save();
                    i = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // i = 0;
        }

        public static void ExportToExcelDS(DataSet oDs, string strFileName)
        {
            ExportToExcel(oDs, strFileName);
        }

        public static void ApplyFormattingToARangeByDataType(ExcelRange oRange, DataColumn oDC)
        {

            if (IsDate(oDC))
            {
                oRange.Style.Numberformat.Format = @"dd/mm/yyyy hh:mm:ss AM/PM";
            }
            else if (IsInteger(oDC))
            {
                //Do Nothing
            }
            else if (IsNumeric(oDC))
            {
                oRange.Style.Numberformat.Format = @"#.##";
            }
            oRange.AutoFitColumns();
        }

        public static void AddSheetsToWorkBookFromDataTable(ExcelPackage oPack, DataTable oDT, string SheetName)
        {
            try
            {
                ExcelWorksheet oWs = oPack.Workbook.Worksheets.Add(null == oDT.TableName || oDT.TableName.Equals(string.Empty) ? "Sheet" + i.ToString() : oDT.TableName);
                oWs.Cells.Style.Font.Name = "Calibiri";
                oWs.Cells.Style.Font.Size = 10;

                int ColCnt = oDT.Columns.Count, RowCnt = oDT.Rows.Count;

                //Export each row..
                oWs.Cells["A1"].LoadFromDataTable(oDT, true);

                //Format the header
                using (ExcelRange oRange = oWs.Cells["A1:" + GetColumnAlphabetFromNumber(ColCnt) + "1"])
                {
                    oRange.Style.Font.Bold = true;
                    oRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    oRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(229, 229, 229));
                }
                int CurrentColCount = 1;
                foreach (DataColumn oDC in oDT.Columns)
                {
                    using (ExcelRange oRange = oWs.Cells[GetColumnAlphabetFromNumber(CurrentColCount) + "1:" + GetColumnAlphabetFromNumber(CurrentColCount) + RowCnt.ToString()])
                    {
                        ApplyFormattingToARangeByDataType(oRange, oDC);
                    }
                    CurrentColCount++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsInteger(DataColumn col)
        {
            if (col == null)
                return false;

            var numericTypes = new[] { typeof(Byte),
                                                typeof(Int16), typeof(Int32), typeof(Int64), typeof(SByte),
                                                 typeof(UInt16), typeof(UInt32), typeof(UInt64)};
            return numericTypes.Contains(col.DataType);
        }

        public static bool IsNumeric(DataColumn col)
        {
            if (col == null)
                return false;
            var numericTypes = new[] {  typeof(Decimal), typeof(Double),
                                                typeof(Single)};
            return numericTypes.Contains(col.DataType);
        }


        public static bool IsDate(DataColumn col)
        {
            if (col == null)
                return false;
            var numericTypes = new[] { typeof(DateTime), typeof(TimeSpan) };
            return numericTypes.Contains(col.DataType);
        }


        public static string GetColumnAlphabetFromNumber(int iColCount)
        {
            string strColAlpha = string.Empty;

            try
            {
                int iloop = iColCount, icount1 = 0, icount2 = 0;
                Char chr = ' ';

                while (iloop > 676)
                {
                    iloop -= 676;
                    icount1++;
                }

                if (icount1 != 0)
                {
                    chr = (Char)(64 + icount1);
                    strColAlpha = chr.ToString();
                }
                while (iloop > 26)
                {
                    iloop -= 26;
                    icount2++;
                }
                if (icount2 != 0)
                {
                    chr = (Char)(64 + icount2);
                    strColAlpha = strColAlpha + chr.ToString();
                }
                chr = (Char)(64 + iloop);
                strColAlpha = strColAlpha + chr.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strColAlpha;
        }

        public static void ExportToExcel(SqlDataReader oReader, string strFileName)
        {
            throw new NotImplementedException();
        }


        public static void ExportToExcel(DataTable oDT, string strFileName)
        {
            try
            {
                FileInfo newFile = new FileInfo(strFileName);

                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    //Do the export stuff here..
                    string sheetname = null == oDT.TableName || oDT.TableName.Equals(string.Empty) ? "Sheet1" : oDT.TableName;
                    AddSheetsToWorkBookFromDataTable(xlPackage, oDT, sheetname);
                    xlPackage.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public static void ExportToExcel(DataTable oDT, Dictionary<string, string> dicColumnNames, string strFileName, string tableName = "")
        {
            var dt = ConstructDataTable(oDT, dicColumnNames);

            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName;
            ExportToExcel(dt, strFileName);
        }
        public static void ExportToExcelFromList<T>(List<T> oDT, Dictionary<string, string> dicColumnNames, string strFileName, string tableName = "")
        {
            var dt = ConstructDataTable(oDT, dicColumnNames);

            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName;
            ExportToExcel(dt, strFileName);
        }

        public static void ExportToExcelFromDataTable<T>(List<List<T>> oDT, List<Dictionary<string, string>> dicColumnNames, string strFileName, string tableName = "")
        {
            var count = 1;
            if (oDT != null && oDT.Count > 0)
            {
                count = oDT.Count;
            }
            var dataSet = new DataSet();
            DataTable[] dt = new DataTable[count];
            for (var i = 0; i < count; i++)
            {
                dt[i] = ConstructDataTable(oDT[i], dicColumnNames[i]);
                dataSet.Tables.Add(dt[i]);
            }


            //if (!string.IsNullOrEmpty(tableName))
            //dt.TableName = tableName;
            ExportToExcel(dataSet, strFileName);
        }

        private static DataTable ConstructDataTable(DataTable dt, Dictionary<string, string> dicColumnNames)
        {


            DataTable dtRet = new DataTable();

            foreach (var col in dicColumnNames.Keys)
            {
                dtRet.Columns.Add(dicColumnNames[col], dt.Columns[col].DataType);
            }

            foreach (DataRow dr in dt.Rows)
            {
                DataRow nDr = dtRet.NewRow();

                foreach (var col in dicColumnNames.Keys)
                {
                    nDr[dicColumnNames[col]] = dr[col];
                }

                dtRet.Rows.Add(nDr);
            }


            return dtRet;



        }

        public static DataTable ConstructDataTable<T>(List<T> dt, Dictionary<string, string> dicColumnNames)
        {


            var dtRet = new DataTable();

            foreach (var col in dicColumnNames.Keys)
            {
                dtRet.Columns.Add(dicColumnNames[col].Trim(), typeof(string));
            }
            var dtrow = dtRet.NewRow();
            foreach (DataColumn col in dtRet.Columns)
            {
                foreach (var dicColumn in dicColumnNames.Where(dicColumn => dicColumn.Value.Trim().Equals(col.ColumnName)))
                {
                    dtrow[col.ColumnName] = dicColumn.Key.Trim();
                    break;
                }

                //foreach (var dicColumn in dicColumnNames)
                //{
                //    if (dicColumn.Value.Trim().Equals(col.ColumnName))
                //    {
                //        dtrow[col.ColumnName] = dicColumn.Key.Trim();
                //        break;
                //    }
                //}

            }
            dtRet.Rows.Add(dtrow);
            foreach (T dr in dt)
            {
                DataRow nDr = dtRet.NewRow();

                foreach (var col in dicColumnNames.Keys)
                {
                    foreach (var prop in dr.GetType().GetProperties())
                    {
                        //nDr[dicColumnNames[col]] = dr.GetType().GetProperties().GetValue(dr, null);
                        if (prop.Name.Equals(col, StringComparison.InvariantCultureIgnoreCase))
                        {
                            nDr[dicColumnNames[col]] = prop.GetValue(dr, null);
                        }
                    }
                }

                dtRet.Rows.Add(nDr);
            }
            return dtRet;
        }

        public static DataTable ReturnDataTable<T>(List<T> dt, Dictionary<string, string> dicColumnNames)
        {
            return ConstructDataTable(dt, dicColumnNames);
        }

        #region["2019-09-20"]
        // Sửa cho Qinvoice. File excel có phân biệt dữ liệu Master và Detail
        public static void ExportToExcelFromList_Qinvoice<T>(List<T> oDT, Dictionary<string, string> dicColumnNames, string strFileName, string tableName, string columnNameDetail)
        {
            var dt = ConstructDataTable(oDT, dicColumnNames);

            if (!string.IsNullOrEmpty(tableName))
                dt.TableName = tableName;
            ExportToExcel_Qinvoice(dt, strFileName, columnNameDetail);
        }

        public static void ExportToExcel_Qinvoice(DataTable oDT, string strFileName, string columnNameDetail)
        {
            try
            {
                FileInfo newFile = new FileInfo(strFileName);

                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    //Do the export stuff here..
                    string sheetname = null == oDT.TableName || oDT.TableName.Equals(string.Empty) ? "Sheet1" : oDT.TableName;
                    AddSheetsToWorkBookFromDataTable_Qinvoice(xlPackage, oDT, sheetname, columnNameDetail);
                    xlPackage.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Tô màu background phân biệt dữ liệu Master và Detail
        /// </summary>
        /// <param name="oPack"></param>
        /// <param name="oDT"></param>
        /// <param name="SheetName"></param>
        /// <param name="columnNameDetail">Tên coumn đầu tiên của phần dữ liệu Detail</param>
        public static void AddSheetsToWorkBookFromDataTable_Qinvoice(ExcelPackage oPack, DataTable oDT, string SheetName, string columnNameDetail) 
        {
            try
            {
                ExcelWorksheet oWs = oPack.Workbook.Worksheets.Add(null == oDT.TableName || oDT.TableName.Equals(string.Empty) ? "Sheet" + i.ToString() : oDT.TableName);
                oWs.Cells.Style.Font.Name = "Calibiri";
                oWs.Cells.Style.Font.Size = 10;
                oWs.Row(1).Height = oWs.Row(2).Height = 25;

                int ColCnt = oDT.Columns.Count, RowCnt = oDT.Rows.Count;
                var iindexColumnDetail = oDT.Columns[columnNameDetail].Ordinal + 1;
                //Export each row..
                oWs.Cells["A1"].LoadFromDataTable(oDT, true);
                //Format the header Master
                var getColumnAlphabetFromNumberMaster = GetColumnAlphabetFromNumber(iindexColumnDetail - 1);
                var cellsMaster = "A1:" + getColumnAlphabetFromNumberMaster + "2";

                using (ExcelRange oRange = oWs.Cells[cellsMaster])
                {
                    oRange.Style.Font.Bold = true;
                    oRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    oRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(252, 228, 214));
                    oRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    oRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    oRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Top.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Bottom.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Left.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Right.Color.SetColor(Color.FromArgb(212, 212, 212));
                }
                //Format the header Detail
                var getColumnAlphabetFromNumberDetail_First = GetColumnAlphabetFromNumber(iindexColumnDetail);
                var getColumnAlphabetFromNumberDetail_Last = GetColumnAlphabetFromNumber(ColCnt);
                var cellsDetail = getColumnAlphabetFromNumberDetail_First + "1:" + getColumnAlphabetFromNumberDetail_Last + "2";
                using (ExcelRange oRange = oWs.Cells[cellsDetail])
                {
                    oRange.Style.Font.Bold = true;
                    oRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    oRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(198, 224, 180));
                    oRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    oRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    oRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Top.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Bottom.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Left.Color.SetColor(Color.FromArgb(212, 212, 212));
                    oRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    oRange.Style.Border.Right.Color.SetColor(Color.FromArgb(212, 212, 212));
                }
                int CurrentColCount = 1;
                foreach (DataColumn oDC in oDT.Columns)
                {
                    using (ExcelRange oRange = oWs.Cells[GetColumnAlphabetFromNumber(CurrentColCount) + "1:" + GetColumnAlphabetFromNumber(CurrentColCount) + RowCnt.ToString()])
                    {
                        ApplyFormattingToARangeByDataType(oRange, oDC);
                    }
                    CurrentColCount++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region ["2019-12-09"]
        public static void ExportToExcelMultiSheet(DataSet ds, string strFileName)
        {
            try
            {
                FileInfo newFile = new FileInfo(strFileName);

                using (ExcelPackage xlPackage = new ExcelPackage(newFile))
                {
                    for (var i = 0; i < ds.Tables.Count; i++)
                    {
                        var oDT = ds.Tables[i];
                        //Do the export stuff here..
                        string sheetname = null == oDT.TableName || oDT.TableName.Equals(string.Empty) ? "Sheet1" : oDT.TableName;
                        AddSheetsToWorkBookFromDataTable(xlPackage, oDT, sheetname);
                    }

                    xlPackage.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}