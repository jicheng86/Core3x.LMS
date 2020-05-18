using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using NSwag;
using LMS.WebApi.Model;
using LMS.WebApi.Controllers;
using LMS.Service;
using LMS.IService;

namespace LMS.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            // Register the Swagger services  --NSwag
            services.AddSwaggerDocument(options =>
            {
                options.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "APISwagger";
                    document.Info.Description = "ASP.NET Core Web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "JiChengLee",
                        Email = "791457931@qq.com",
                        Url = "https://www.cnblogs.com/jicheng/"
                    };
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "JiChengLee",
                        Url = "https://www.cnblogs.com/jicheng/"
                    };
                };
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//ʹ��AutoMapper

            var jwtSettings = Configuration.GetSection("jwtOptions");
            services.Configure<JwtOptions>(jwtSettings);
            JwtOptions jwtOptions = jwtSettings.Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //������������Ƿ�Ӧ�洢��Microsoft.AspNetCore.Authentication�С��ɹ���Ȩ���AuthenticationProperties��
                options.SaveToken = true;
                //��ȡ������Ԫ���ݵ�ַ��Ȩ���Ƿ���ҪHTTPS��Ĭ��ֵΪtrue����Ӧ��ֻ�ڿ��������н��á�
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //��ȡ������һ������ֵ���Կ�����������֤�ڼ��Ƿ񽫶԰䷢�߽�����֤��
                    ValidateIssuer = true,
                    //��ȡ������һ��System.String������ʾ��ʹ�õ���Ч�����߼����ҵķ����ߡ�
                    ValidIssuer = jwtOptions.Issuer,
                    //��ȡ������һ������ֵ���Կ����Ƿ���������֤�ڼ���֤���ڡ�
                    ValidateAudience = true,
                    //��ȡ������һ���ַ��������ַ�����ʾ�����ڼ�����Ч���ڷ������ƵĹ��ڡ�
                    ValidAudience = jwtOptions.Audience,
                    //��ȡ������һ������ֵ���ò���ֵ�����Ƿ���֤microsoft.identitymodel.token�����ö�securityToken����ǩ����SecurityKey��
                    ValidateIssuerSigningKey = true,
                    //��ȡ������Ҫʹ�õ�Microsoft.IdentityModel.Tokens.SecurityKey����ǩ����֤��
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                     {
                         if (context.Response.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     }
                };
            });

            #region ��ͬ��controllerע�벻ͬ��ʵ��
            services.AddSingleton<ILogger, ILogger<JwtAuthenticationController>>();
            services.AddSingleton<ILogger, ILogger<WeatherForecastController>>();
            #endregion
            services.AddSingleton<IJwtAuthenticateService, JwtAuthenticationService>();

        }


        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseRouting();

            app.UseAuthentication();//������֤�м��
            app.UseAuthorization();//������Ȩ�м��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
