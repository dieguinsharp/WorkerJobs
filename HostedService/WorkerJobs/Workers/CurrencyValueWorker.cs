using System.Diagnostics;
using WorkerJobs.DataAccess.CurrencyValue;

namespace WorkerJobs.Workers {
    internal class CurrencyValueWorker:BackgroundService {

        private readonly string _currencyType = "USD-BRL";

        private readonly ICurrencyValue _currencyValueService;
        private readonly ILogger<CurrencyValueWorker> _logger;

        public CurrencyValueWorker (IServiceProvider serviceProvider, ILogger<CurrencyValueWorker> logger) {
            
            _logger = logger;

            using(var scope = serviceProvider.CreateScope()) {
                _currencyValueService = scope.ServiceProvider.GetService<ICurrencyValue>();
            }

        }

        public override Task StartAsync (CancellationToken cancellationToken) {
            
            _logger.LogInformation("CurencyValueWorker started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);

        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {

            while (!stoppingToken.IsCancellationRequested)
            {

                var dolarDto = await _currencyValueService.GetCurrencyValue(_currencyType);

                _logger.LogInformation($"Dolar value: R$ {dolarDto?.USD.Bid} - Date last dolar value: {dolarDto?.USD.Create_Date}.");

                var sended = await _currencyValueService.SaveCurrencyValue(dolarDto.USD);

                if(sended)
                    _logger.LogInformation($"CurencyValueWorker: Sended to database.");             

                await Task.Delay(1000, stoppingToken);
                
            }

        }

        public override async Task StopAsync (CancellationToken cancellationToken) {

            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation("CurencyValueWorker stopped at: {time}", DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
            _logger.LogInformation("CurencyValueWorker took {ms} ms to stop.", stopWatch.ElapsedMilliseconds);
    
        }
    }
}
