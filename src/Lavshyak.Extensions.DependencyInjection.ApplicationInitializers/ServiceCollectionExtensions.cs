using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

public static class ServiceCollectionExtensions
{
    /*public static void RegisterApplicationInitializedMark(this IServiceCollection services)
    {
        services.TryAddSingleton<ApplicationInitializationStartedMark>();
    }

    public static void RegisterApplicationInitializerCore<TApplicationInitializer>(this IServiceCollection services)
        where TApplicationInitializer : class, IApplicationInitializer
    {
        services.AddScoped<IApplicationInitializer, TApplicationInitializer>();
    }*/

    public static void RegisterApplicationInitializer<TApplicationInitializer>(this IServiceCollection services)
        where TApplicationInitializer : class, IApplicationInitializer
    {
        services.AddScoped<IApplicationInitializer, TApplicationInitializer>();
        services.TryAddSingleton<ApplicationInitializationStartedMark>();
    }
}