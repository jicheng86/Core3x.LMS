using Autofac;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LMS.Web.Models.autofac
{
    public class AutomaticInjectionModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            //var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToArray();
            //var perRequestType = typeof(IScoped);
            //builder.RegisterAssemblyTypes(assemblys)
            //    .Where(t => perRequestType.IsAssignableFrom(t) && t != perRequestType)
            //    .PropertiesAutowired()
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();

            //var perDependencyType = typeof(ITransient);
            //builder.RegisterAssemblyTypes(assemblys)
            //    .Where(t => perDependencyType.IsAssignableFrom(t) && t != perDependencyType)
            //    .PropertiesAutowired()
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency();

            //var singleInstanceType = typeof(ISingleton);
            //builder.RegisterAssemblyTypes(assemblys)
            //    .Where(t => singleInstanceType.IsAssignableFrom(t) && t != singleInstanceType)
            //    .PropertiesAutowired()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();


            //程序集范围注入
            Assembly service = Assembly.Load("Exercise.Services");
            Assembly Iservice = Assembly.Load("Exercise.IServices");
            builder.RegisterAssemblyTypes(service, Iservice)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            //单个注册
            // builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired();

            //在控制器中使用属性依赖注入，其中注入属性必须标注为public
            var types = typeof(Startup).Assembly.GetExportedTypes()
                .Where(type => typeof(Microsoft.AspNetCore.Mvc.ControllerBase)
                .IsAssignableFrom(type))
                .ToArray();
            builder.RegisterTypes(types).PropertiesAutowired();
        }

    }
}
