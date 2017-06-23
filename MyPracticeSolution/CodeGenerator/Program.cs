using ForWebStudy.Models;
using MyPractice;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 测试

            for (int i = probability.Count - 1; i < 100; i++)
            {
                var list = getList(i);
                for(var j = 0; j < list.data.Count; j = j + 28)
                {
                    Console.WriteLine(string.Join(",", list.data.Skip(j).Take(28)));
                }
                Console.WriteLine("n:" + i + " ,sum:" + list.data.Sum() + " ,max:" + list.data.Max() + " ,denominator:" + list.denominator);
                Console.ReadLine();
            }
            {
                var list = getList(600);
                for (var j = 0; j < list.data.Count; j = j + 28)
                {
                    Console.WriteLine(string.Join(",", list.data.Skip(j).Take(28)));
                }
                Console.WriteLine("n:" + 600 + " ,sum:" + list.data.Sum() + " ,max:" + list.data.Max() + " ,denominator:" + list.denominator);
                Console.ReadLine();
            }

            #endregion
            return;
            #region MyRegion

            
            List<int> test = null;
            foreach (var item in test)
            {
                var asd = item;
            }

            return;

            var a = new BllChild();
            var b = new BllChild1();
            var c = BllChild.Instance;
            return;
            dynamic request = new ExpandoObject();
            request.SearchKey = "test";
            request.TypeID = "11111";
            request.OrderDict=new Dictionary<string, OrderOption>();
            request.OrderDict.Add("questionid", OrderOption.Desc);
            request.PageSize = 15;
            request.CurrentPage = 1;
            request.Status = StatusOption.Active;
            request.SearchType = SearchTypeOption.SplitPage;
            //request.Test = "test";

            //var www = request.GetType().GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);

            var aaa = ((Object)request).ToJsonString();
            var aaaa = request.ToJsonString();
            //var bbb = aaa.de
            //var ccc = bbb.SearchType;

            return;
            {
                //Object a = null;
                //var b = a as TableDesc;

                //var urlDetection = new Regex(@"^/{1}[^/\\]+.*$", RegexOptions.Compiled);
                //var a = "/account/login";
                //var b = urlDetection.IsMatch(a);
            }

            ReadTableDesc();
            #endregion
        }

        #region 知乎问题

        //种子数据
        public static List<Probability> probability = new List<Probability>();
        static Program()
        {
            probability.Add(new Probability
            {
                data = new List<int> { },   //占位置用
                denominator = -1
            });
            probability.Add(new Probability
            {
                data = new List<int> { 1 },
                denominator = 1
            });
            probability.Add(new Probability
            {
                data = new List<int> { 0, 1 },
                denominator = 1
            });
            probability.Add(new Probability
            {
                data = new List<int> { 0, 1, 1 },
                denominator = 2
            });
            probability.Add(new Probability
            {
                data = new List<int> { 0, 1, 1, 2 },
                denominator = 4
            });
            //probability.Add(new Probability
            //{
            //    data = new List<int> { 0, 1, 1, 2, 2 },
            //    denominator = 6
            //});
            //foreach (var item in probability)
            //{
            //    Console.WriteLine((double)item.data.Sum(c => c)/item.denominator);
            //}
            //Console.ReadLine();
        }

        public class Probability
        {
            public List<int> data { get; set; }
            public int denominator { get; set; }

            /// <summary>
            /// Reduction of a fraction提取最大公约数并排除该约数
            /// </summary>
            /// <param name="old"></param>
            /// <returns></returns>
            public void ROAF()
            {
                var min = data.Where(c => c > 1).Min();
                if (data.Exists(c => c % min > 0) || denominator % min > 0)
                {
                    throw new Exception("无法除尽");
                }
                else
                {
                    data = data.Select(c => c / min).ToList();
                    denominator = denominator / min;
                }
            }
        }

        public static Probability getList(int n)
        {
            if (n <= probability.Count - 1)
            {
                return probability[n];
            }
            var p_1 = getList(n - 1);
            var n_1 = p_1.data;
            var N = up_n(n);//probability从1开始

            var ppp = new Probability();
            ppp.denominator = p_1.denominator * N;

            var list = new List<int> { 0 };
            for (int i = 1; i < n; i++)
            {
                var a = up_n(i + 1);//ppp.data从0开始
                var b = down_n(i + 1);
                int P_i;
                if (i < n - 1)
                {
                    P_i = b * n_1[i - 1] + (N - a) * n_1[i];
                }
                else if (i == n - 1)
                {
                    P_i = b * n_1[i - 1];
                }
                else
                {
                    throw new Exception();
                }
                list.Add(P_i);
            }
            ppp.data = list;
            ppp.ROAF();
            probability.Add(ppp);
            return ppp;
        }
        /// <summary>
        /// 取n/2的下整数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int down_n(int n)
        {
            var k = (n % 2) == 0 ? n / 2 : (n - 1) / 2;
            return k;
        }
        /// <summary>
        /// 取n/2的上整数
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int up_n(int n)
        {
            var k = (n % 2) == 0 ? n / 2 : (n + 1) / 2;
            return k;
        }
        

        #endregion



        public class BllBase
        {
            protected BllBase() { }

            protected static BllBase instance = new BllBase();
            public static BllBase Instance
            {
                get
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    var a= MethodBase.GetCurrentMethod(); ;

                    if (instance == null)
                    {
                        instance = new BllBase();
                    }
                    return instance;
                }
            }
        }
        public class BllChild:BllBase
        {
            static BllChild()
            {
                instance = new BllChild();
            }

        }
        public class BllChild1 : BllBase
        {
            static BllChild1()
            {
                instance = new BllChild1();
            }

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
                    column0 = hr.GetCell(0).StringCellValue,
                    column1 = hr.GetCell(1).StringCellValue,
                });
                index++;
            }
            var test = data.ToJsonString().DeserializeJsonString<List<TableDesc>>();
            {
                //var headCode = TableDesc.GetCode(test, TableDesc.HeadCode);
                //var bodyCode = TableDesc.GetCode(test, TableDesc.BodyCode);

                //var headTemplate = TableDesc.GetCode(test, TableDesc.HeadTemplat);
                //var bodyTemplate = TableDesc.GetCode(test, TableDesc.BodyTemplate);
                var code = TableDesc.GetCode(test, TableDesc.ModelTemplate);

            }
        }

    }
    internal class TableDesc
    {

        public string column0 { get; set; }
        public string column1 { get; set; }

        public static Func<TableDesc, string> HeadCode = c => $"{c.column1} = \"{c.column0 }\",\r\n";
        public static Func<TableDesc, string> BodyCode = c => $"{c.column1} = _row[\"{c.column1}\"].{(IsNumberColumn(c.column0) ? "FormatNumeric" : "FormatString")}(),//{c.column0}\r\n";
        public static Func<TableDesc, string> HeadTemplat = c => $"<th class=\"text_center\"><%={c.column1}%></th>\r\n";
        public static Func<TableDesc, string> BodyTemplate = c =>
        {
            var isNum = IsNumberColumn(c.column0);
            return $"<td class=\"{(isNum ? "text_right" : "text_left")}\" {(isNum ? "exportdatatype='N|2|T'" : "")}><%={c.column1}%></td>\r\n";
        };
        public static Func<TableDesc, string> ModelTemplate = c =>
        {
            return $"/// <summary>\r\n/// {c.column1}\r\n/// </summary>\r\npublic string {c.column0} {{ get; set; }}\r\n";
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
