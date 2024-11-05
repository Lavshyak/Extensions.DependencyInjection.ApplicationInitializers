namespace Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;

internal class ApplicationInitializationStartedMark
{
    public bool InitializationStarted { get; private set; }

    public void MarkInitializationStarted()
    {
        InitializationStarted = true;
    }
}