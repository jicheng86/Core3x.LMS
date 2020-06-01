using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hangfire;
using Hangfire.SqlServer;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LMS.HangFire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //hangfire的任务需要数据库持久化
            //Hangfire.AspNetCore
            //Hangfire.SqlServer   sqlserver引用 大小写敏感

            //hangfire必须需要绑定一个持久化的连接数据。 官方推荐的是sqlserver,还有mg,mssql,pgsql,redis都是个人封装的
            //连接字符串必须加 Allow User Variables=true
            services.AddHangfire(x => x.UseStorage(new SqlServerStorage(
                Configuration.GetConnectionString("DefaultConnection"),
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,                      //- 作业队列轮询间隔。默认值为15秒。
                    PrepareSchemaIfNecessary = true,                        //- 如果设置为true，则创建数据库表。默认是true。
                    UseRecommendedIsolationLevel = true,                    // 事务隔离级别。默认是读取已提交。
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                    TransactionTimeout = TimeSpan.FromMinutes(1),           //- 交易超时。默认为1分钟。
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),     //- 作业到期检查间隔（管理过期记录）。默认值为1小时。
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),    //- 聚合计数器的间隔。默认为5分钟。
                    DashboardJobListLimit = 50000,                          //- 仪表板作业列表限制。默认值为50000。
                }
                )));
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseHangfireServer();
            app.UseHangfireDashboard();
            backgroundJobClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
