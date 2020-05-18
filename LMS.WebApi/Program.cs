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
using Serilog.Sinks.MSSqlServer;

namespace LMS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
             .ReadFrom.Configuration(new ConfigurationBuilder()
             .AddJsonFile("SerilogConfigs.json")
             .Build())
             //ICredentialsByHost
             .WriteTo.Email(
                fromEmail: "791457931@qq.com",
                toEmail: "412148697.qq.com",
                mailServer: "smtp.qq.com",
                mailSubject: "ϵͳ�д�����д����־����鿴��",
                restrictedToMinimumLevel: LogEventLevel.Error,
                networkCredential: new NetworkCredential(userName: "791457931@qq.com", password: "C#EqualTo2C++LJC"))
            
             //.WriteTo.MSSqlServer(@"Server=...", sinkOptions: new SinkOptions { TableName = "Logs" }, columnOptions: columnOptions)
             .CreateLogger();

            Log.Information("info");
            Log.Error("err");
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
