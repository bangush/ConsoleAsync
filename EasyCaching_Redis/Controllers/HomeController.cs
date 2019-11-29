using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyCaching.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyCaching_Redis.Controllers
{
    public class HomeController : ControllerBase
    {


        private IEasyCachingProvider cachingProvider;
        private IEasyCachingProviderFactory easyCachingProviderFactory;
        public HomeController(IEasyCachingProviderFactory cachingProviderFactory)
        {
            this.easyCachingProviderFactory = cachingProviderFactory;
            this.cachingProvider = cachingProviderFactory.GetCachingProvider("RedisExample");
        }


        [HttpGet("Demo")]
        public IActionResult SetRedisItem()
        {
            this.cachingProvider.Set("zaranet use easycaching", "this is my value", TimeSpan.FromDays(100));
            return Ok();
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
