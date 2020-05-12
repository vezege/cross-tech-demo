using CrossTech.Application.Interfaces.Gateways;
using CrossTech.Application.Interfaces.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrossTech.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Метод регистрации UseCases, реализующих интерфейсы, наследующие интерфейс IUseCase
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void RegisterUseCases(this IServiceCollection services, AssemblyName[] assemblies)
        {
            Type basicInterface = typeof(IUseCase);

            // ищем название сборки, содержащей интерфейсы UseCases и реализации этих интерфейсов
            AssemblyName applicationAN = assemblies.Where(x => x.Name.Equals("CrossTech.Application")).FirstOrDefault();
            if (applicationAN != null)
            {
                // загружаем сборку
                Assembly assembly = Assembly.Load(applicationAN);
                if (assembly != null)
                {
                    // ищем интерфейсы, наследующие IUseCase
                    IEnumerable<TypeInfo> useCaseInterfaces = assembly.DefinedTypes
                                                                .Where(t =>
                                                                {
                                                                    if (t.IsInterface && !t.IsGenericType && t != basicInterface.GetTypeInfo())
                                                                    {
                                                                        return basicInterface.IsAssignableFrom(t);
                                                                    };
                                                                    return false;
                                                                });
                    // ищем классы, реализаующие интерфейсы, наследующие IUseCase
                    IEnumerable<TypeInfo> useCases = assembly.DefinedTypes
                                                            .Where(t =>
                                                            {
                                                                if (t.IsClass && !t.IsGenericType && !t.IsAbstract)
                                                                {
                                                                    return t.GetInterfaces().Any(i =>
                                                                    {
                                                                        return useCaseInterfaces.Contains(i);
                                                                    });
                                                                }
                                                                return false;
                                                            });
                    // регистрируем интерфейс и реализующий его класс
                    foreach (TypeInfo useCase in useCases)
                    {
                        var useCaseInterface = useCase.ImplementedInterfaces
                                                    .Where(i => useCaseInterfaces.Contains(i))
                                                    .FirstOrDefault();
                        services.Add(new ServiceDescriptor(useCaseInterface, useCase, ServiceLifetime.Scoped));
                    }
                }
            }
        }

        /// <summary>
        /// Метод регистрации Gateways, реализующих интерфейсы, наследующие IGateway
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void RegisterGateways(this IServiceCollection services, AssemblyName[] assemblies)
        {
            Type basicInterface = typeof(IGateway<>).GetGenericTypeDefinition();

            // находим названия сборок, содержащие интерфейсы Gateways и реализации интерфейсов
            AssemblyName applicationAN = assemblies.Where(x => x.Name.Equals("CrossTech.Application")).FirstOrDefault();

            if (applicationAN != null)
            {
                // загружаем сборки
                Assembly applicationAssembly = Assembly.Load(applicationAN);
                Assembly persistanceAssembly = Assembly.Load("CrossTech.Persistence");
                if (applicationAssembly != null && persistanceAssembly != null)
                {
                    // находим интерфейсы, наследующие IBasicGateway
                    IEnumerable<TypeInfo> gatewayInterfaces = applicationAssembly.DefinedTypes
                                                                    .Where(t =>
                                                                    {
                                                                        if (t.IsInterface && !t.IsGenericType)
                                                                        {
                                                                            return t.GetInterfaces().Any(i =>
                                                                            {
                                                                                return i.IsGenericType && basicInterface.IsAssignableFrom(i.GetGenericTypeDefinition());
                                                                            });
                                                                        }
                                                                        return false;
                                                                    });
                    // находим классы, реализующие интерфейсы, наследующие IBasicGateway
                    IEnumerable<TypeInfo> gateways = persistanceAssembly.DefinedTypes
                                                            .Where(t =>
                                                            {
                                                                if (t.IsClass && !t.IsGenericType)
                                                                {
                                                                    return t.GetInterfaces().Any(i => gatewayInterfaces.Contains(i));
                                                                }
                                                                return false;
                                                            });
                    // регистрируем каждый интерфейс и соответствующий ему класс как singleton'ы, чтобы изменения сохранялись между сеансами
                    foreach (TypeInfo gateway in gateways)
                    {
                        var gatewayInterface = gateway.ImplementedInterfaces
                                                    .Where(i => gatewayInterfaces.Contains(i))
                                                    .FirstOrDefault();
                        services.Add(new ServiceDescriptor(gatewayInterface, gateway, ServiceLifetime.Singleton));
                    }
                }
            }
        }
    }
}
