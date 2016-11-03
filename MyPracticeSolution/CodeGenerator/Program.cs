using MyPractice;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadTableDesc();
        }

        static void ReadTableDesc()
        {
            var excelPath = @"C:\Users\PC_liss\Desktop\test.xlsx";
            XSSFWorkbook wk;
            using (FileStream fs = new FileStream(excelPath, FileMode.Open, FileAccess.ReadWrite))
            {
                wk = new XSSFWorkbook(fs);
            }
            XSSFSheet hst = (XSSFSheet)wk.GetSheetAt(0);
            var data = new List<object>();
            var index = 0;
            while (true)
            {
                XSSFRow hr = (XSSFRow)hst.GetRow(index);
                if (hr == null)
                {
                    break;
                }
                data.Add(new
                {
                    description = hr.GetCell(0).StringCellValue,
                    name = hr.GetCell(1).StringCellValue,
                });
                index++;
            }
            var test = data.ToJsonString().DeserializeJsonString<List<TableDesc>>();
            {
                var headCode = TableDesc.GetCode(test, TableDesc.HeadCode);
                var bodyCode = TableDesc.GetCode(test, TableDesc.BodyCode);

                var headTemplate = TableDesc.GetCode(test, TableDesc.HeadTemplat);
                var bodyTemplate = TableDesc.GetCode(test, TableDesc.BodyTemplate);

            }
        }

    }
    internal class TableDesc
    {

        public string description { get; set; }
        public string name { get; set; }

        public static Func<TableDesc, string> HeadCode = c => $"{c.name} = \"{c.description }\",\r\n";
        public static Func<TableDesc, string> BodyCode = c => $"{c.name} = _row[\"{c.name}\"].{(IsNumberColumn(c.description) ? "FormatNumeric" : "FormatString")}(),//{c.description}\r\n";
        public static Func<TableDesc, string> HeadTemplat = c => $"<th class=\"text_center\"><%={c.name}%></th>\r\n";
        public static Func<TableDesc, string> BodyTemplate = c =>
        {
            var isNum = IsNumberColumn(c.description);
            return $"<td class=\"{(isNum ? "text_right" : "text_left")}\" {(isNum ? "exportdatatype='N|2|T'" : "")}><%={c.name}%></td>\r\n";
        };

        public static string GetCode(List<TableDesc> tableDesc, Func<TableDesc, string> func)
        {
            var sb = new StringBuilder();
            tableDesc.ForEach(o => sb.Append(func(o)));
            return sb.ToString().TrimEnd("\r\n".ToCharArray());
        }
        private static Regex numberTest = new Regex(@"(\(|（).*(%|元|股)(\)|）)");
        public static bool IsNumberColumn(string desc)
        {
            return numberTest.IsMatch(desc);
        }
    }
}
