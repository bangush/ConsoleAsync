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
            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
