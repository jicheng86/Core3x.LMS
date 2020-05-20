using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace LMS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            // .MinimumLevel.Debug()
            // .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
            // .ReadFrom.Configuration(
            //    new ConfigurationBuilder()
            //    .AddJsonFile("SerilogConfigs.json")
            //    .Build()
            //    )
            //ICredentialsByHost
           

            //.WriteTo.MSSqlServer(@"Server=...", sinkOptions: new SinkOptions { TableName = "Logs" }, columnOptions: columnOptions)
            //.CreateLogger();
            //���������Ϳ�ʼ��¼��־
            Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .WriteTo.Console()
            // .WriteTo.File(formatter: new CompactJsonFormatter(), path: "Serilogs/logs/log.txt", restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: false, buffered: true)
            .WriteTo.File(path: "Serilogs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: false, buffered: true)
           .AuditTo.File("Serilogs/audit.txt",restrictedToMinimumLevel: LogEventLevel.Error)
           //.WriteTo.MSSqlServer(connectionString: @"Data Source=.;Database=DB_LMS_Core3.x;Integrated Security=SSPI;Persist Security Info=False;", sinkOptions: new SinkOptions { TableName = "Serilogs" }, restrictedToMinimumLevel: LogEventLevel.Debug)
           .WriteTo.Email(
              fromEmail: "791457931@qq.com",
              toEmail: "412148697.qq.com",
              mailServer: "smtp.qq.com",
              mailSubject: "ϵͳ�д�����д����־����鿴��",
              restrictedToMinimumLevel: LogEventLevel.Debug,
              networkCredential: new NetworkCredential(userName: "791457931@qq.com", password: "C#EqualTo2C++LJC"))
           // .WriteTo.Seq("http://localhost:5000")
           .CreateLogger();

            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()//ʹ��Serilog ��־���
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
