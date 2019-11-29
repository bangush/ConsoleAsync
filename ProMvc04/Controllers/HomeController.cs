using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProMvc04.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProMvc04.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //_logger.LogError("我是错误显示");
            //_logger.LogDebug("我是调试信息");
            //_logger.LogInformation("我是提示信息");


            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "127.0.0.1"//rabbitmq ip
            };

            //创建连接
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();

            //事件基本消费者
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            string aaa = "";
            //接收到消息事件
            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body);
                aaa = message;
                Console.WriteLine($"收到消息： {message}");
                //确认该消息已被消费
                channel.BasicAck(ea.DeliveryTag, false);
            };

            //启动消费者 设置为手动应答消息
            channel.BasicConsume("queuename_zkb", false, consumer);
            Console.WriteLine("消费者已启动");
            //Console.ReadKey();
            channel.Dispose();
            connection.Close();














            return View();

        }

        public IActionResult About()
        {
            _logger.LogError("我是About显示");
            _logger.LogDebug("我是About信息");
            _logger.LogInformation("我是About信息");
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
