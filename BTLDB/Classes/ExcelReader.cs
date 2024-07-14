using System;
using System.Globalization;
using BTLDB.Models;
using OfficeOpenXml;

namespace BTLDB.Classes
{
    public class ExcelReader
    {
        public List<SiPMArray> ReadExcel(string filePath)
        {
            var sipmArrays = new List<SiPMArray>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["datasheet"];
                if (worksheet == null)
                {
                    throw new Exception("Worksheet 'datasheet' not found.");
                }

                int startRow = 11; // Adjust based on the actual starting row of your data
                int rowCount = worksheet.Dimension.Rows;

                SiPMArray ?currentArray = null;
                string usedTrayNo = "";
                for (int row = startRow; row <= rowCount; row++)
                {
                    int arrayRowIndex = row;
                    string trayNo = worksheet.Cells[arrayRowIndex, 1].Text;

                    if (!string.IsNullOrWhiteSpace(trayNo) && !string.IsNullOrEmpty(trayNo))
                    {
                        usedTrayNo = trayNo;
                    }
                    
                    var cellData = worksheet.Cells[arrayRowIndex, 2].Text;
                    int positionNo = int.Parse(cellData);
                    double TECResistance = double.Parse(worksheet.Cells[arrayRowIndex, 10].Text, CultureInfo.InvariantCulture);
                    double MeanResistance = double.Parse(worksheet.Cells[arrayRowIndex, 11].Text, CultureInfo.InvariantCulture);
                    double StdDevResistance = double.Parse(worksheet.Cells[arrayRowIndex, 12].Text, CultureInfo.InvariantCulture);
                    double RTD = double.Parse(worksheet.Cells[arrayRowIndex, 13].Text, CultureInfo.InvariantCulture);
                    string sn = worksheet.Cells[arrayRowIndex, 3].Text;
                    currentArray = new SiPMArray
                    {
                        TrayNo = usedTrayNo,
                        PositionNo = positionNo,
                        SN = sn,
                        Channels = new List<Channel>(),
                        TECResistance = TECResistance,
                        MeanResistance = MeanResistance,
                        StdDevResistance = StdDevResistance,
                        RTD = RTD
                    };
                    sipmArrays.Add(currentArray);

                    for (int i = 0; i < 16; i++)
                    {
                        int ChNo = int.Parse(worksheet.Cells[row, 4].Text);
                        double Vop = double.Parse(worksheet.Cells[row, 5].Text, CultureInfo.InvariantCulture);
                        double Id1 = double.Parse(worksheet.Cells[row, 6].Text, CultureInfo.InvariantCulture);
                        double Id2 = double.Parse(worksheet.Cells[row, 7].Text, CultureInfo.InvariantCulture);
                        double Is = double.Parse(worksheet.Cells[row, 8].Text, CultureInfo.InvariantCulture);
                        double RqN = double.Parse(worksheet.Cells[row, 9].Text, CultureInfo.InvariantCulture);
                        currentArray?.Channels.Add(new Channel
                        {
                            ChNo = ChNo,
                            Vop = Vop,
                            Id1 = Id1,
                            Id2 = Id2,
                            Is = Is,
                            RqN = RqN
                        });
                        row++;
                    }
                    row--; //have to decrement by one for next round
                }
            }

            return sipmArrays;
        }
    }
}

