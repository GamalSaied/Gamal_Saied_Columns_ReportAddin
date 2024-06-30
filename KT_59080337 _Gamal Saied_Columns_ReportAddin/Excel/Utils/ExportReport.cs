using KT_59080337__Gamal_Saied_Columns_ReportAddin.Revit.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT_59080337__Gamal_Saied_Columns_ReportAddin.Excel.Utils
{
    public class ExportReport
    {
        public static void ExportToExcel(List<CloumnsData> columnDataList, string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Columns Report");

                // Header row
                worksheet.Cells[1, 1].Value = "Family";
                worksheet.Cells[1, 2].Value = "Type";
                worksheet.Cells[1, 3].Value = "ID";
                worksheet.Cells[1, 4].Value = "Easting (m)";
                worksheet.Cells[1, 5].Value = "Northing (m)";
                worksheet.Cells[1, 6].Value = "Base Level";
                worksheet.Cells[1, 7].Value = "Base Offset (m)";
                worksheet.Cells[1, 8].Value = "Top Level";
                worksheet.Cells[1, 9].Value = "Top Offset (m)";
                worksheet.Cells[1, 10].Value = "Height (m)";
                worksheet.Cells[1, 11].Value = "Volume (m³)";

                // Data rows
                for (int i = 0; i < columnDataList.Count; i++)
                {
                    var columnData = columnDataList[i];
                    worksheet.Cells[i + 2, 1].Value = columnData.Family;
                    worksheet.Cells[i + 2, 2].Value = columnData.Type;
                    worksheet.Cells[i + 2, 3].Value = columnData.ID;
                    worksheet.Cells[i + 2, 4].Value = columnData.Easting;
                    worksheet.Cells[i + 2, 5].Value = columnData.Northing;
                    worksheet.Cells[i + 2, 6].Value = columnData.BaseLevel;
                    worksheet.Cells[i + 2, 7].Value = columnData.BaseOffset;
                    worksheet.Cells[i + 2, 8].Value = columnData.TopLevel;
                    worksheet.Cells[i + 2, 9].Value = columnData.TopOffset;
                    worksheet.Cells[i + 2, 10].Value = columnData.Height;
                    worksheet.Cells[i + 2, 11].Value = columnData.Volume;
                }

                // Save the package to the file path
                FileInfo fileInfo = new FileInfo(filePath);
                package.SaveAs(fileInfo);
            }
        }
    }
}
