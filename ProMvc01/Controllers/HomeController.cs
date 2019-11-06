using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetStandard_Helper;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProMvc01
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDateTimeData _dateTimeData;

        public HomeController(ILogger<HomeController> logger, IDateTimeData dateTimeData)
        {
            _logger = logger;
            _dateTimeData = dateTimeData;

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            _logger.LogError("我是错误显示"+ _dateTimeData);
            _logger.LogDebug("我是调试信息");
            _logger.LogInformation("我是提示信息");
            return View();
        }
    }


}
