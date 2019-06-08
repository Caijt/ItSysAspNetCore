using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ItSys.Common
{
    public class ExcelHelper
    {
        public static List<Dictionary<string, object>> ToList(Stream stream, int startRow = 2, string[] keys = null)
        {
            List<Dictionary<string, object>> l = new List<Dictionary<string, object>>();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                var sheet = package.Workbook.Worksheets[1];
                int rows = sheet.Dimension.Rows;
                int cols = sheet.Dimension.Columns;

                for (int row = startRow; row <= rows; row++)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int col = 1; col <= cols; col++)
                    {
                        string key;
                        if (keys != null && col <= keys.Length)
                        {
                            key = keys[col - 1];
                        }
                        else
                        {
                            key = Regex.Match(sheet.Cells[row, col].Address, "[A-Z]+").Value;
                        }
                        dict.Add(key, sheet.Cells[row, col].Value);
                    }
                    l.Add(dict);
                }
            }
            return l;
        }

        public static byte[] ListToExcel<T>(List<T> list, Dictionary<string, Func<T, object>> dict)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("test");
                //标题
                for (int col = 1; col <= dict.Count; col++)
                {
                    worksheet.Cells[1, col].Value = dict.Keys.ElementAt(col - 1);
                }
                //内容
                for (int row = 1; row <= list.Count; row++)
                {
                    T obj = list[row - 1];
                    for (int col = 1; col <= dict.Count; col++)
                    {
                        worksheet.Cells[row + 1, col].Value = dict.Values.ElementAt(col - 1).Invoke(obj);
                    }
                }
                return package.GetAsByteArray();
            }
        }
    }
}
