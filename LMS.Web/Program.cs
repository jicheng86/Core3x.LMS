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
             Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//��������־������д,����֮��,Ŀǰ���ֻ��΢���Դ�����־���
            .ReadFrom.Configuration(new ConfigurationBuilder()
            .AddJsonFile("SerilogConfigs.json")
            .Build())
             .WriteTo.Email(new EmailConnectionInfo()
             {
                 EmailSubject = "ϵͳ����,�����ٲ鿴!",//�ʼ�����
                 FromEmail = "791457931@qq.com",//����������
                 MailServer = "smtp.qq.com",//smtp��������ַ
                 NetworkCredentials = new NetworkCredential(userName: "791457931@qq.com", password: "C#EqualTo2C++LJC"),//�������ֱ��Ƿ�����������ͻ�����Ȩ��
                 Port = 587,//�˿ں�
                 ToEmail = "412148697@qq.com"//�ռ���

             })
            //.WriteTo.MSSqlServer(@"Server=...", sinkOptions: new SinkOptions { TableName = "Logs" }, columnOptions: columnOptions)
            .CreateLogger();

            Log.Information("info");
            Log.Error("err");
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
