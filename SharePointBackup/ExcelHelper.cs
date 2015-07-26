namespace SharePointBackup
{
    using System;
    using System.Data;
    using ClosedXML.Excel;

    class ExcelHelper
    {
        internal static void SaveList(DataTable dataTable)
        {
            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add(dataTable.TableName);
                worksheet.Cell(1, 1).InsertTable(dataTable.AsEnumerable());
                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(dataTable.TableName + ".xlsx");
            }
            catch (Exception exception)
            {
                throw new Exception("SaveList failed:" + exception.Message, exception);
            }
        }
    }
}
