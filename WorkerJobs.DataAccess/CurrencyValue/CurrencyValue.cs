using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using WorkerJobs.Common.DTOs;

namespace WorkerJobs.DataAccess.CurrencyValue {

    public interface ICurrencyValue {
        Task<CurrencyValueDTO.Root> GetCurrencyValue (string currencyType);
        Task<bool> SaveCurrencyValue (CurrencyValueDTO currencyValueDTO);
    }

    public class CurrencyValue : ICurrencyValue {

        private string _stringConnection = "Server=localhost;Database=WorkerJobs;Persist Security info=True;Trusted_Connection=True;Integrated Security=SSPI";
        private string _apiUrl = "https://economia.awesomeapi.com.br/json/all/";

        public async Task<CurrencyValueDTO.Root> GetCurrencyValue (string currencyType) {
            using(HttpClient web = new HttpClient()) {

                var stringByWeb = await web.GetStringAsync(new Uri(_apiUrl + currencyType));
                var currencyValue = JsonConvert.DeserializeObject<CurrencyValueDTO.Root>(stringByWeb);

                if(currencyValue != null)
                    return currencyValue;

                return new CurrencyValueDTO.Root() { USD = new CurrencyValueDTO() { Bid = 0, Create_Date = DateTime.MinValue } };
            }
        }

        public async Task<bool> SaveCurrencyValue (CurrencyValueDTO currencyValueDTO) {
            using(var conn = new SqlConnection(_stringConnection)) {

                conn.Open();

                var query = $"INSERT INTO CurrencyValue" +
                    $"(Currency, Value, CurrentValueTimestamp, WorkedTimestamp) " +
                    $"VALUES " +
                    $"(@currency, @value, @timestamp1, @timestamp2);";

                using(var cmd = new SqlCommand(query,conn)) {

                    cmd.Parameters.Add("@currency", SqlDbType.VarChar).Value = currencyValueDTO.Code + "-" + currencyValueDTO.CodeIn;
                    cmd.Parameters.Add("@value", SqlDbType.Float).Value = currencyValueDTO.Bid;
                    cmd.Parameters.Add("@timestamp1", SqlDbType.DateTime2).Value = currencyValueDTO.Create_Date;
                    cmd.Parameters.Add("@timestamp2", SqlDbType.DateTime2).Value = DateTimeOffset.Now.ToString();

                    await cmd.ExecuteNonQueryAsync();
                }

                return true;
            } 
        }
    }
}
