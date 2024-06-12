Use like this:
```
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

It uses Microsoft's DI, so sulution guarantee that the order of execution is the same as the order of addition
