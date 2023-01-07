using WorkerJobs;
using WorkerJobs.DataAccess.CurrencyValue;
using WorkerJobs.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging( (context, builder) => builder.AddConsole())
    .ConfigureServices(services => {
        services.Configure<HostOptions>(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(10);
        });
        services.AddScoped<ICurrencyValue, CurrencyValue>();
        services.AddScoped<IWhatsAppApi, WhatsAppApi>();
        services.AddHostedService<CurrencyValueWorker>();
        services.AddHostedService<CurrencyValueToWhatsAppWorker>();
    })
    .Build();

await host.RunAsync();
