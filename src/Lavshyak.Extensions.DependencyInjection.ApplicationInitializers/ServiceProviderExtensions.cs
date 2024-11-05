using Microsoft.Extensions.DependencyInjection;

namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

public static class ServiceProviderExtensions
{
    public static async Task ExecuteApplicationInitializersAsync(this IServiceProvider services)
    {
        var mark = services.GetRequiredService<ApplicationInitializedMark>();

        if (mark.Disposed)
            throw new InvalidOperationException("Probably reinitialization");

        await mark.Semaphore.WaitAsync();
        try
        {
            if (mark.Initialized)
                throw new InvalidOperationException("Probably reinitialization");

            using var scope = services.CreateScope();

            var initializers = scope.ServiceProvider.GetServices<IApplicationInitializer>();
            foreach (var initializer in initializers)
            {
                await initializer.ExecuteAsync();
            }

            mark.MarkInitialized();
        }
        finally
        {
            mark.Semaphore.Release();
            mark.Dispose();
        }
    }
}