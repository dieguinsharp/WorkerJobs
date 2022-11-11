using WorkerJobs;
using WorkerJobs.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging( (context, builder) => builder.AddConsole())
    .ConfigureServices(services => {
        services.Configure<HostOptions>(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(10);
        });
        services.AddHostedService<CurrencyValueWorker>();
    })
    .Build();

await host.RunAsync();
