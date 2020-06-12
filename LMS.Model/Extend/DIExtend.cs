
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;


namespace LMS.Model.Extend
{
    /// <summary>
    ///  Microsoft.Extensions.DependencyInjection 扩展
    /// </summary>
    public static class DIExtend
    {
        /// <summary>
        /// Add Scoped from InterfaceAssembly and ImplementAssembly to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// AddScoped 方法重载
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly">接口程序集</param>
        /// <param name="implementAssembly">服务实现程序集</param>
        public static void AddScoped(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface && !t.Name.ToLower().Contains("base"));
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x));
                if (type != null && (type.Name.ToLower().Contains("service") || type.Name.ToLower().Contains("repository")))
                {
                    services.AddScoped(item, type);
                }
            }
        }

        /// <summary>
        /// Add AddSingleton from InterfaceAssembly and ImplementAssembly to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        ///  AddSingleton 方法重载
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly">接口程序集</param>
        /// <param name="implementAssembly">服务实现程序集</param>
        public static void AddSingleton(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface && !t.Name.ToLower().Contains("base"));
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x));
                if (type != null && (type.Name.ToLower().Contains("service") || type.Name.ToLower().Contains("repository")))
                {
                    services.AddSingleton(item, type);
                }
            }
        }

        /// <summary>
        /// Add AddTransient from InterfaceAssembly and ImplementAssembly to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// AddTransient 方法重载
        /// </summary>
        /// <param name="services"></param>
        /// <param name="interfaceAssembly">接口程序集</param>
        /// <param name="implementAssembly">服务实现程序集</param>
        public static void AddTransient(this IServiceCollection services, Assembly interfaceAssembly, Assembly implementAssembly)
        {
            var interfaces = interfaceAssembly.GetTypes().Where(t => t.IsInterface && !t.Name.ToLower().Contains("base"));
            var implements = implementAssembly.GetTypes();
            foreach (var item in interfaces)
            {
                var type = implements.FirstOrDefault(x => item.IsAssignableFrom(x));
                if (type != null && (type.Name.ToLower().Contains("service") || type.Name.ToLower().Contains("repository")))
                {
                    services.AddTransient(item, type);
                }
            }
        }
    }
}
