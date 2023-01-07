using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerJobs.DataAccess.CurrencyValue {

    public interface IWhatsAppApi {
        Task<bool> SendMessageToWhatsApp (string message, string number);
    }

    public class WhatsAppApi : IWhatsAppApi {

        // please read this documentation project

        private string _instance = "";
        private string _token = "";

        public async Task<bool> SendMessageToWhatsApp (string number, string message) {
            using(HttpClient web = new HttpClient()) {
                await web.GetStringAsync(new Uri(
                    $"https://api.ultramsg.com/{_instance}/messages/chat" +
                    $"?token={_token}" +
                    $"&to={number}" +
                    $"&body={message}" +
                    $"&priority=10"));
            }
            return true;
        }
    }
}
