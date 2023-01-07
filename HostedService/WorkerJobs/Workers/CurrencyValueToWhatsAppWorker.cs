using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerJobs.DataAccess.CurrencyValue;

namespace WorkerJobs.Workers {
    internal class CurrencyValueToWhatsAppWorker:BackgroundService {

        // your complet number (+5531....)
        // to use the api, please use the https://user.ultramsg.com/ website to register
        private readonly string _number = "XXXXXXXXXX";

        private readonly ICurrencyValue _currencyValueService;
        private readonly IWhatsAppApi _whatsAppApi;

        private readonly ILogger<CurrencyValueWorker> _logger;

        public CurrencyValueToWhatsAppWorker (IServiceProvider serviceProvider, ILogger<CurrencyValueWorker> logger) {
            
            _logger = logger;

            using(var scope = serviceProvider.CreateScope()) {
                _currencyValueService = scope.ServiceProvider.GetService<ICurrencyValue>();
                _whatsAppApi = scope.ServiceProvider.GetService<IWhatsAppApi>();
            }

        }

        public override Task StartAsync (CancellationToken cancellationToken) {
            
            _logger.LogInformation("CurrencyValueToWhatsAppWorker started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);

        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {

            while (!stoppingToken.IsCancellationRequested)
            {

                var average = await _currencyValueService.GetDayAverageValue();

                await _whatsAppApi.SendMessageToWhatsApp(_number, $"Today average dolar value worked from Jobs: {average}");

                _logger.LogInformation($"CurrencyValueToWhatsAppWorker: Sended day average value to WhatsApp.");          

                await Task.Delay(60000, stoppingToken);
                
            }

        }

        public override async Task StopAsync (CancellationToken cancellationToken) {

            var stopWatch = Stopwatch.StartNew();
            _logger.LogInformation("CurrencyValueToWhatsAppWorker stopped at: {time}", DateTimeOffset.Now);
            await base.StopAsync(cancellationToken);
            _logger.LogInformation("CurrencyValueToWhatsAppWorker took {ms} ms to stop.", stopWatch.ElapsedMilliseconds);
    
        }
    }
}
