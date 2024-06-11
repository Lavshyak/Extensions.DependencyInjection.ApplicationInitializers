namespace LAVSHYAK.Extensions.DependencyInjection.ApplicationInitializers;

public interface IApplicationInitializer
{
    public Task ExecuteAsync();
}