using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace LMS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // Log.Logger = new LoggerConfiguration()
           //.MinimumLevel.Debug()
           //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)//对其他日志进行重写,除此之外,目前框架只有微软自带的日志组件
           //.WriteTo.Console()
           ////.ReadFrom.Configuration(new ConfigurationBuilder()
           ////.AddJsonFile("SerilogConfigs.json")
           ////.Build())

           // //.WriteTo.Email(new EmailConnectionInfo()
           // //{
           // //    EmailSubject = "系统警告,请速速查看!",//邮件标题
           // //     FromEmail = "791457931@qq.com",//发件人邮箱
           // //     MailServer = "smtp.qq.com",//smtp服务器地址
           // //     NetworkCredentials = new NetworkCredential(userName: "791457931@qq.com", password: "C#EqualTo2C++LJC"),//个参数分别是发件人邮箱与客户端授权码
           // //     Port = 587,//端口号
           // //     ToEmail = "412148697@qq.com"//收件人

           // // })
           ////.WriteTo.MSSqlServer(@"Server=...", sinkOptions: new SinkOptions { TableName = "Logs" }, columnOptions: columnOptions)
           //.CreateLogger();

            //Log.Information("info");
            //Log.Error("err");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
