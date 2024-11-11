Use like this:
```
using Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;
// asp.net 8 app
await app.Services.InjectServicesAndInvokeInNewScopeAsync<MainDbContext>(async mainDbContext =>
    await mainDbContext.Database.MigrateAsync()
);
```
Or like this:
```
using Lavshyak.Extensions.DependencyInjection.ApplicationInitializers;
// asp.net 8 builder

var services = builder.Services;

services.RegisterApplicationInitializer<DataBaseMigrator>();
services.RegisterApplicationInitializer<IdentityRolesSynchronizator>();
services.RegisterApplicationInitializer<MinioConfigurator>();
services.RegisterApplicationInitializer<WebhookToMinioAddator>();

var app = builder.Build();
await app.Services.ExecuteApplicationInitializersAsync();
await app.RunAsync();
```

It uses Microsoft's DI, so solution guarantee that the order of execution is the same as the order of addition
