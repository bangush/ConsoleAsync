using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using NetStandard_Helper;

namespace ProMvc01
{
    public class Startup
    {

        public IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWelcomeServices, WelcomeServices>();
            services.AddSingleton<IDateTimeData, DateTimeData>();

            services.AddTransient<ITransientService, TransientService>();
            services.AddTransient<IScopedService, ScopedService>();
            services.AddTransient<ISingletonService, SingletonService>();



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient();

            //注册appsetting的值            
            services.Configure<AppSetting>(_configuration.GetSection("Zhu:Kai"));

            var conn = _configuration.GetSection("ConnectionStrings");
            string efconn = conn["kkk"];
            string conns = conn["zzz"];

            //OMSECData.Acc_OmsContext.ConnectionString = efconn;
            //services.AddDbContext<OMSECData.Acc_OmsContext>
            //(
            //    options => options.UseSqlServer(efconn)
            //);
            //上面和下面的是同样的为Context链接的数据的方式
            //OMSECData.Acc_OmsContext.ConnectionString = conns;



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IConfiguration iconfiguration,//找appsetting的值
                              //IWelcomeServices welcomeServices,//自己写个服务
                              //ILogger<Startup> logger,
                              ILoggerFactory loggerFactory
                              )
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //中间件  app.Usexxx(可以实力对象)

            loggerFactory.AddNLog();//添加NLog                                    
            env.ConfigureNLog("nlog.config");//引入Nlog配置文件

            //app.Use(next =>
            //{
            //    logger.LogInformation("app.Use()....................");
            //    return async httpContext =>
            //    {
            //        logger.LogInformation("...............async httpContext");//在输出窗口查看
            //        if (httpContext.Request.Path.StartsWithSegments("/first"))
            //        {
            //            logger.LogInformation("...............async /first");
            //            await httpContext.Response.WriteAsync("/first");
            //        }
            //        else
            //        {
            //            logger.LogInformation("...............async /httpContext");
            //            await next(httpContext);
            //        }
            //    };
            //});
            //app.UseWelcomePage(new WelcomePageOptions { Path = "/welcome" });
            //app.Run(async (context) =>
            //{
            //    //var hello = iconfiguration["zkb"];
            //    //var hello = welcomeServices.GetMessgse();
            //    await context.Response.WriteAsync("15");
            //});



            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
