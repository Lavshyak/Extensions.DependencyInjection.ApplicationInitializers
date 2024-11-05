namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

internal class ApplicationInitializedMark : IDisposable
{
    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
    public bool Initialized { get; private set; }
    public bool Disposed { get; private set; }

    public void MarkInitialized()
    {
        Initialized = true;
    }

    public void Dispose()
    {
        Semaphore.Dispose();
        Disposed = true;
    }
}