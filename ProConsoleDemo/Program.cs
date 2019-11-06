using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProConsoleDemo
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //}

        static async Task Main(string[] args)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://www.baidu.com/");

           
            //var str=  Helper.HtmlEncode("http://www.cnblogs.com/a file with spaces.html?a=1&b=博客园#abc");
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
