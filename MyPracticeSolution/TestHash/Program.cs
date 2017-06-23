using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestHash
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = TimeSpan.FromMinutes(24*60*200);


            Console.WriteLine("press any key to begin test:");
            Console.ReadLine();
            Test test = new Test();
            test.execTest();
            Console.ReadLine();
        }
    }

    

    public class Test
    {

        public void execTest()
        {
            //Console.WriteLine("方法Md5_1,通过MD5.Create()创建实例:");
            //test_1(() => Md5_1(testStr));
            //Console.WriteLine("方法Md5_3,通过MD5CryptoServiceProvider创建实例:");
            //test_1(() => Md5_3(testStr));
            //Console.WriteLine("方法Md5_2,单例,UTF8:");
            //test_1(() => Md5_2(testStr));
            Console.WriteLine("方法Md5_4,单例，ANSI(default):");
            test_1(() => Md5_4(testStr, Encoding.Default));
            Console.WriteLine("方法Md5_4,单例，UTF8:");
            test_1(() => Md5_4(testStr, Encoding.UTF8));
            Console.WriteLine("方法Md5_4,单例，ASCII:");
            test_1(() => Md5_4(testStr, Encoding.ASCII));
            Console.WriteLine("方法Md5_4,单例，BigEndianUnicode:");
            test_1(() => Md5_4(testStr, Encoding.BigEndianUnicode));
            Console.WriteLine("方法Md5_4,单例，Unicode:");
            test_1(() => Md5_4(testStr, Encoding.Unicode));
            Console.WriteLine("方法Md5_4,单例，UTF32:");
            test_1(() => Md5_4(testStr, Encoding.UTF32));
            Console.WriteLine("方法Md5_4,单例，UTF7:");
            test_1(() => Md5_4(testStr, Encoding.UTF7));
        }


        public string testStr = "http://app.jg.eastmoney.com/F9Stock/AssetDebt.do?securityCode=600000.SH&companyType=127000000606280264&yearList=2017,2016,2015,2014&reportTypeList=1,5,3,6,7&dateSearchType=1&listedType=0,1&reportTypeInScope=1&reportType=1&rotate=0&seperate=0&order=desc&cashType=1&exchangeValue=1&customSelect=1&CurrencySelect=0";

        #region 测试方法
        public delegate void TestDelegate();
        private void test_1(TestDelegate targetFun)
        {
            int times = 100000;
            Stopwatch myWatch = new Stopwatch();
            myWatch.Start();
            for (int i = 0; i < times; i++)
            {
                targetFun.Invoke();
            }
            myWatch.Stop();
            long myUseTime = myWatch.ElapsedMilliseconds;
            Console.WriteLine("执行次数"+ times + ",執行時間: " + myUseTime.ToString() + " ms");
        }
        #endregion


        #region MD5方法
        /// <summary>
        /// 通过MD5.Create()创建实例
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Md5_1(string str)
        {
            //将输入字符串转换成字节数组
            var buffer = Encoding.UTF8.GetBytes(str);
            //接着，创建Md5对象进行散列计算
            var data = MD5.Create().ComputeHash(buffer);
            //创建一个新的Stringbuilder收集字节
            var sb = new StringBuilder();
            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                //格式每一个十六进制字符串
                sb.Append(t.ToString("X2"));
            }
            //返回十六进制字符串
            return sb.ToString();
        }
        private MD5 hashObj = MD5.Create();
        /// <summary>
        /// 单例,UTF8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Md5_2(string str)
        {
            //将输入字符串转换成字节数组
            var buffer = Encoding.UTF8.GetBytes(str);
            //接着，创建Md5对象进行散列计算
            var data = hashObj.ComputeHash(buffer);
            //创建一个新的Stringbuilder收集字节
            var sb = new StringBuilder();
            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                //格式每一个十六进制字符串
                sb.Append(t.ToString("X2"));
            }
            //返回十六进制字符串
            return sb.ToString();
        }
        /// <summary>
        /// 通过MD5CryptoServiceProvider创建实例
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Md5_3(string str)
        {
            //将输入字符串转换成字节数组
            var buffer = Encoding.UTF8.GetBytes(str);
            //接着，创建Md5对象进行散列计算
            MD5 md5 = new MD5CryptoServiceProvider();
            var data = md5.ComputeHash(buffer);
            //创建一个新的Stringbuilder收集字节
            var sb = new StringBuilder();
            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                //格式每一个十六进制字符串
                sb.Append(t.ToString("X2"));
            }
            //返回十六进制字符串
            return sb.ToString();
        }
        /// <summary>
        /// 单例，ANSI(default)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Md5_4(string str, Encoding encoding)
        {
            //将输入字符串转换成字节数组
            var buffer = encoding.GetBytes(str);
            //接着，创建Md5对象进行散列计算
            var data = hashObj.ComputeHash(buffer);
            //创建一个新的Stringbuilder收集字节
            var sb = new StringBuilder();
            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                //格式每一个十六进制字符串
                sb.Append(t.ToString("X2"));
            }
            //返回十六进制字符串
            return sb.ToString();
        }
        #endregion
    }
}
