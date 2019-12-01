using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetStandard_Helper;
using System.Net.Http;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProMvc01
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDateTimeData _dateTimeData;
        private IWelcomeServices _WelcomeServices;
        public IHttpClientFactory _myhttpclientfactory;
        public IOptions<AppSetting> _Setting;


        private readonly ITransientService _guidTransientService;
        private readonly IScopedService _guidScopedService;
        private readonly ISingletonService _guidSingletonService;

        public HomeController(ILogger<HomeController> logger,
                               IDateTimeData dateTimeData,
                               IWelcomeServices welcomeServices,
                               IHttpClientFactory myhttpclientfactory,
                               IOptions<AppSetting> Setting,

                               ITransientService guidTransientService,
                               IScopedService guidScopedService,
                               ISingletonService guidSingletonService

            )
        {
            _logger = logger;
            _dateTimeData = dateTimeData;
            _WelcomeServices = welcomeServices;
            _myhttpclientfactory = myhttpclientfactory;
            _Setting = Setting;

            _guidTransientService = guidTransientService;
            _guidScopedService = guidScopedService;
            _guidSingletonService = guidSingletonService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            //var xx = _Setting.Value.Bin;
            //_logger.LogError("我是错误显示"+ _dateTimeData);
            //_logger.LogDebug("我是调试信息"+ _WelcomeServices);
            //_logger.LogInformation("我是提示信息____"+ GetV());

            //ViewBag.TransientItem = _guidTransientService.id();
            //ViewBag.ScopedItem = _guidScopedService.id();

            //ViewBag.SingletonItem = _guidSingletonService.id();

            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",//用户名
                Password = "guest",//密码
                HostName = "127.0.0.1",//rabbitmq ip
                //VirtualHost = "/"
            };

            //创建连接
            var connection = factory.CreateConnection();
            //创建通道
            var channel = connection.CreateModel();
            //声明一个队列    （队列名字，是否持久化，是否自动删除队列，是否排外的，是否等待服务器返回）
            channel.QueueDeclare("queuename_zkb", false, false, false, null);           
            Guid guid = new Guid();           
            var sendBytes = Encoding.UTF8.GetBytes(guid.ToString());
            //发布消息         （交换器名称，路由键，是否为强制性，
            channel.BasicPublish("", "queuename_zkb", null, sendBytes);
            channel.Close();
            connection.Close();
            return View();
        }

        public async Task<string> GetV()
        {

            var http = _myhttpclientfactory.CreateClient();
            var x = await http.GetStringAsync("https://www.baidu.com/");
            return x;
        }


    }


}
