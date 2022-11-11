using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;

namespace WorkerJobs.Workers {
    internal class CurrencyValueWorker:BackgroundService {

        private string _stringConnection = "Server=localhost;Database=WorkerJobs;Persist Security info=True;Trusted_Connection=True;Integrated Security=SSPI";
        private string _apiUrl = "https://economia.awesomeapi.com.br/json/all/";
        private static string _currencyType = "USD-BRL";
        private string _fullUrl { get { return _apiUrl + _currencyType; } }

        private readonly ILogger<CurrencyValueWorker> _logger;

        public CurrencyValueWorker (ILogger<CurrencyValueWorker> logger) {
            
            _logger = logger;    
        }

        public override Task StartAsync (CancellationToken cancellationToken) {
            
            _logger.LogInformation("CurencyValueWorker started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);

        }

        protected override async Task ExecuteAsync (CancellationToken stoppingToken) {

            while (!stoppingToken.IsCancellationRequested)
            {

                using(HttpClient web = new HttpClient()) {

                    var stringByWeb = await web.GetStringAsync(new Uri(_fullUrl));

                    var dolarDto = JsonConvert.DeserializeObject<WorkerJobs.Common.Workers.CurrencyValue.CurrencyValueDTO.Root>(stringByWeb);

                    _logger.LogInformation($"Dolar value: R$ {dolarDto?.USD.Bid} - Date last dolar value: {dolarDto?.USD.Create_Date}.");

                    using(var conn = new SqlConnection(_stringConnection)) {

                        conn.Open();

                        var query = $"INSERT INTO CurrencyValue" +
                            $"(Currency, Value, CurrentValueTimestamp, WorkedTimestamp) " +
                            $"VALUES " +
                            $"(@currency, @value, @timestamp1, @timestamp2);";

                        using(var cmd = new SqlCommand(query,conn)) {

                            cmd.Parameters.Add("@currency",SqlDbType.VarChar).Value = _currencyType;
                            cmd.Parameters.Add("@value",SqlDbType.Float).Value = dolarDto?.USD.Bid;
                            cmd.Parameters.Add("@timestamp1",SqlDbType.DateTime2).Value = dolarDto?.USD.Create_Date;
                            cmd.Parameters.Add("@timestamp2",SqlDbType.DateTime2).Value = DateTimeOffset.Now.ToString();

                            cmd.ExecuteNonQuery();

                            _logger.LogInformation($"CurencyValueWorker: Sended to database.");
                        }
                    }              
                }

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
