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
        services.AddHostedService<CurrencyValueWorker>();
    })
    .Build();

await host.RunAsync();
