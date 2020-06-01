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
            //hangfire��������Ҫ���ݿ�־û�
            //Hangfire.AspNetCore
            //Hangfire.SqlServer   sqlserver���� ��Сд����

            //hangfire������Ҫ��һ���־û����������ݡ� �ٷ��Ƽ�����sqlserver,����mg,mssql,pgsql,redis���Ǹ��˷�װ��
            //�����ַ�������� Allow User Variables=true
            services.AddHangfire(x => x.UseStorage(new SqlServerStorage(
                Configuration.GetConnectionString("DefaultConnection"),
                new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,                      //- ��ҵ������ѯ�����Ĭ��ֵΪ15�롣
                    PrepareSchemaIfNecessary = true,                        //- �������Ϊtrue���򴴽����ݿ��Ĭ����true��
                    UseRecommendedIsolationLevel = true,                    // ������뼶��Ĭ���Ƕ�ȡ���ύ��
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                    TransactionTimeout = TimeSpan.FromMinutes(1),           //- ���׳�ʱ��Ĭ��Ϊ1���ӡ�
                    JobExpirationCheckInterval = TimeSpan.FromHours(1),     //- ��ҵ���ڼ������������ڼ�¼����Ĭ��ֵΪ1Сʱ��
                    CountersAggregateInterval = TimeSpan.FromMinutes(5),    //- �ۺϼ������ļ����Ĭ��Ϊ5���ӡ�
                    DashboardJobListLimit = 50000,                          //- �Ǳ����ҵ�б����ơ�Ĭ��ֵΪ50000��
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
