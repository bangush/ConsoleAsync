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

namespace ProMvc01
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IWelcomeServices, WelcomeServices>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              IConfiguration iconfiguration,//找appsetting的值
                              IWelcomeServices welcomeServices,//自己写个服务
                              ILogger<Startup> logger,
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
