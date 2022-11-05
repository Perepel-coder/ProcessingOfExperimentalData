using Services.Interfaces;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace Services
{
    public class InputData: IInputData
    {
        public InputData() { }
        public DataTable GetInputDataFromFile(Stream fileStream)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                var myTable = new List<object[]>();

                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;

                IWorkbook workbook = application.Workbooks.Open(fileStream, ExcelOpenType.Automatic);
                IWorksheet worksheet = workbook.Worksheets[0];

                return worksheet.ExportDataTable(worksheet.UsedRange, ExcelExportDataTableOptions.ColumnNames);
            }
        }
    }
}

