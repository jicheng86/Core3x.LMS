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
using Serilog.Sinks.SystemConsole.Themes;

namespace LMS.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //程序启动就开始记录日志
            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithProperty("SourceContext", null) //加入属性SourceContext，也就运行时是调用Logger的具体类
                .Enrich.FromLogContext() //动态加入属性，主要是针对上面的自定义字段User和Class，当然也可以随时加入别的属性。
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File(formatter: new CompactJsonFormatter(), path: "Serilogs/JsonFormatterlog.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: true, buffered: false)
                .WriteTo.File(path: "Serilogs/log.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Debug, retainedFileCountLimit: 365, encoding: Encoding.UTF8, shared: true, buffered: false)
                .AuditTo.File(path: "Serilogs/audit.txt", restrictedToMinimumLevel: LogEventLevel.Error)
                .WriteTo.MSSqlServer(connectionString: @"Data Source=.;Database=DB_LMS_Core3.x;Integrated Security=SSPI;Persist Security Info=False;", sinkOptions: new SinkOptions { TableName = "Serilogs4Web", AutoCreateSqlTable = true }, restrictedToMinimumLevel: LogEventLevel.Warning)
               //.WriteTo.Email(
               //   fromEmail: "lijc@lx-car.com",
               //   toEmail: "791457931@qq.com",
               //   mailServer: "smtp.263.net",
               //   mailSubject: "系统有错误，已写入日志，请查看！",
               //   restrictedToMinimumLevel: LogEventLevel.Warning,
               //   networkCredential: new NetworkCredential(userName: "lijc@lx-car.com", password: "zxc123111"))
               //.WriteTo.Seq(serverUrl: "http://localhost:5341")
               .CreateLogger();

            try
            {
                Log.Information("Starting up Successful");
                CreateHostBuilder(args)
                    .Build()
                    .Run();
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
            .UseSerilog()//使用Serilog 日志组件
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
