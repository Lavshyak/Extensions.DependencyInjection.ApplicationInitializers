using Microsoft.Extensions.DependencyInjection;

namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

public static class ServiceProviderExtensions
{
    private static readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);

    public static async Task ExecuteApplicationInitializersAsync(this IServiceProvider services)
    {
        var mark = services.GetRequiredService<ApplicationInitializationStartedMark>();

        await Semaphore.WaitAsync();
        try
        {
            if (mark.InitializationStarted)
                throw new InvalidOperationException("Probably reinitialization");
            
            mark.MarkInitializationStarted();

            using var initializersScope = services.CreateScope();

            var initializers = initializersScope.ServiceProvider.GetServices<IApplicationInitializer>();
            foreach (var initializer in initializers)
            {
                using var initializerScope = initializersScope.ServiceProvider.CreateScope();
                await initializer.ExecuteAsync();
            }
        }
        finally
        {
            Semaphore.Release();
        }
    }

    public static async Task InjectServicesAndInvokeInNewScopeAsync<T1>(this IServiceProvider services,
        Func<T1, Task> func) where T1 : notnull
    {
        await using var scope = services.CreateAsyncScope();

        var t1 = scope.ServiceProvider.GetRequiredService<T1>();

        await func(t1);
    }

    public static async Task InjectServicesAndInvokeInNewScopeAsync<T1, T2>(this IServiceProvider services,
        Func<T1, T2, Task> func) where T1 : notnull where T2 : notnull
    {
        await using var scope = services.CreateAsyncScope();

        var t1 = scope.ServiceProvider.GetRequiredService<T1>();
        var t2 = scope.ServiceProvider.GetRequiredService<T2>();

        await func(t1, t2);
    }

    public static async Task InjectServicesAndInvokeInNewScopeAsync<T1, T2, T3>(this IServiceProvider services,
        Func<T1, T2, T3, Task> func) where T1 : notnull where T2 : notnull where T3 : notnull
    {
        await using var scope = services.CreateAsyncScope();

        var t1 = scope.ServiceProvider.GetRequiredService<T1>();
        var t2 = scope.ServiceProvider.GetRequiredService<T2>();
        var t3 = scope.ServiceProvider.GetRequiredService<T3>();
        await func(t1, t2, t3);
    }
}