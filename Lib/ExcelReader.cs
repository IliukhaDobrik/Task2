using System;
using System.IO;

namespace Lib
{
    public static class ExcelReader
    {
        public static void ReadFromExcel(string path)
        {
            
            
            using (var writer = new StreamWriter("test.txt"))
            {
                for (int row = 0; row <= totalRows; row++)
                {
                    for (int col = 0; col < totalRColumns; col++)
                    {
                        string cellValue = Convert.ToString(((Range)worksheet.Cells[row, col]).Value2);
                        writer.Write(cellValue + "\t");
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}