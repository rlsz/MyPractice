using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GetMD5OrSHA1
{
    class Program
    {
        static void Main(string[] args)
        {
            //target A8529D7E703CF07D35F7E34EA8833C0C

            //var a = MD5Encrypt("网下申购日");//fcdfce6422bff963bc64c4b4077198fc
            //var b = Md5_1("网下申购日");//FCDFCE6422BFF963BC64C4B4077198FC

            //var c1 = Md5_2(Encoding.Default.GetBytes("网下申购日"));
            //var c2 = Md5_2(Encoding.Unicode.GetBytes("网下申购日"));
            //var c3 = Md5_2(Encoding.ASCII.GetBytes("网下申购日"));
            //var c4 = Md5_2(Encoding.BigEndianUnicode.GetBytes("网下申购日"));
            //var c5 = Md5_2(Encoding.UTF32.GetBytes("网下申购日"));
            //var c6 = Md5_2(Encoding.UTF7.GetBytes("网下申购日"));
            //var c7 = Md5_2(Encoding.UTF8.GetBytes("网下申购日"));//here it is,看来网上用的MD5全都是用UTF8编码格式的

            //var d = Md5_2(Encoding.UTF8.GetBytes("mytest"));

            char result = 'n';
            while (result == 'n')
            {
                result = EnterPath();
            }
        }

        public static string Md5_2(byte[] buffer)
        {
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

        public static string Md5_1(string str)
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
        public static string MD5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(strText));
            StringBuilder sc = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                sc.Append(result[i].ToString("x2"));
            }
            return sc.ToString();
        }
        static char EnterPath()
        {
            Console.WriteLine("please enter file path:");
            var path = Console.ReadLine();
            GetFile(path);
            GetMD5(path);
            GetSHA1(path);
            GetSHA256(path);
            Console.Write("quit?(y/n):");
            var key = Console.ReadKey();
            Console.WriteLine();
            return key.KeyChar;
        }
        static void GetFile(string s)
        {
            try
            {
                FileInfo fi = new FileInfo(s);
                Console.WriteLine("文件路径：{0}", s);
                Console.WriteLine("文件名称：{0}", fi.Name.ToString());
                Console.WriteLine("文件类型：{0}", fi.Extension.TrimStart('.'));
                Console.WriteLine("文件大小：{0} K", fi.Length / 1024);
                Console.WriteLine("文件创建时间：{0}", fi.CreationTime.ToString());
                Console.WriteLine("上次访问时间：{0}", fi.LastAccessTime.ToString());
                Console.WriteLine("上次写入时间：{0}", fi.LastWriteTime.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void GetMD5(string s)
        {
            try
            {
                FileStream file = new FileStream(s, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retval = md5.ComputeHash(file);
                file.Close();
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sc.Append(retval[i].ToString("x2"));
                }
                Console.WriteLine("文件MD5：{0}", sc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void GetSHA1(string s)
        {
            try
            {
                FileStream file = new FileStream(s, FileMode.Open);
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] retval = sha1.ComputeHash(file);
                file.Close();
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sc.Append(retval[i].ToString("x2"));
                }
                Console.WriteLine("文件SHA1：{0}", sc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void GetSHA256(string s)
        {
            try
            {
                FileStream file = new FileStream(s, FileMode.Open);
                SHA256 sha256 = new SHA256CryptoServiceProvider();
                byte[] retval = sha256.ComputeHash(file);
                file.Close();
                StringBuilder sc = new StringBuilder();
                for (int i = 0; i < retval.Length; i++)
                {
                    sc.Append(retval[i].ToString("x2"));
                }
                Console.WriteLine("文件SHA256：{0}", sc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
