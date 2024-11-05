namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

public class ApplicationInitializedMark : IDisposable
{
    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
    public bool Initialized { get; private set; }

    public void MarkInitialized()
    {
        Initialized = true;
    }
    
    public void Dispose()
    {
        Semaphore.Dispose();
    }
}