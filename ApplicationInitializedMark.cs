namespace LAVSHYAK.Extensions.DependencyInjection.ApplicationInitializers;

internal class ApplicationInitializedMark : IDisposable
{
    public readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1);
    public WriteableOnce<bool> Initialized { get; } = new WriteableOnce<bool>(false);

    public void Dispose()
    {
        Semaphore.Dispose();
    }
}