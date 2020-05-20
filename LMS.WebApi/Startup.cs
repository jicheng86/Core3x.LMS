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
using Serilog.Core;
using Serilog;
using Serilog.Events;
using LMS.Repository;
using Microsoft.EntityFrameworkCore;

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
        /// 构造函数
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
            services.AddDbContext<EFDbContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Default"));
            });
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//使用AutoMapper

           // services.Configure<JwtOptions>(Configuration.GetSection("jwtOptions")); 
             JwtOptions jwtOptions = Configuration.GetSection("jwtOptions").Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                //定义承载令牌是否应存储在Microsoft.AspNetCore.Authentication中。成功授权后的AuthenticationProperties。
                options.SaveToken = true;
                //获取或设置元数据地址或权限是否需要HTTPS。默认值为true。这应该只在开发环境中禁用。
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //获取或设置一个布尔值，以控制在令牌验证期间是否将对颁发者进行验证。
                    ValidateIssuer = true,
                    //获取或设置一个System.String，它表示将使用的有效发行者检查代币的发行者。
                    ValidIssuer = jwtOptions.Issuer,
                    //获取或设置一个布尔值，以控制是否将在令牌验证期间验证受众。
                    ValidateAudience = true,
                    //获取或设置一个字符串，该字符串表示将用于检查的有效受众反对令牌的观众。
                    ValidAudience = jwtOptions.Audience,
                    //获取或设置一个布尔值，该布尔值控制是否验证microsoft.identitymodel.token。调用对securityToken进行签名的SecurityKey。
                    ValidateIssuerSigningKey = true,
                    //获取或设置要使用的Microsoft.IdentityModel.Tokens.SecurityKey用于签名验证。
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
            // Add this line; you'll need `using Serilog;` up the top, too
           // app.UseSerilogRequestLogging();
            app.UseSerilogRequestLogging(options =>
            {
                // Customize the message template
                options.MessageTemplate = "Handled {RequestPath}";

                // Emit debug-level events instead of the defaults
                options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

                // Attach additional properties to the request completion event
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
                };
            });
            app.UseRouting();

            app.UseAuthentication();//启用验证中间件
            app.UseAuthorization();//启用授权中间件

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
