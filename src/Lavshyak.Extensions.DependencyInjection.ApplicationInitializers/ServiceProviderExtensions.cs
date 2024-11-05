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

            using var scope = services.CreateScope();

            var initializers = scope.ServiceProvider.GetServices<IApplicationInitializer>();
            foreach (var initializer in initializers)
            {
                await initializer.ExecuteAsync();
            }
        }
        finally
        {
            Semaphore.Release();
        }
    }
}