using Microsoft.Extensions.DependencyInjection;

namespace LAVSHYAK.Extensions.DependencyInjection.ApplicationInitializers;

public static class ServiceProviderExtensions
{
    public static async Task ExecuteApplicationInitializersAsync(this IServiceProvider services)
    {
        var mark = services.GetRequiredService<ApplicationInitializedMark>();

        await mark.Semaphore.WaitAsync();

        if (mark.Initialized.Value)
            throw new InvalidOperationException();

        using var scope = services.CreateScope();

        var initializers = scope.ServiceProvider.GetServices<IApplicationInitializer>();
        foreach (var initializer in initializers)
        {
            await initializer.ExecuteAsync();
        }

        mark.Initialized.Value = true;

        mark.Semaphore.Release();
    }
}