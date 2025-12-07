using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Dtos;
using Newtonsoft.Json;

namespace DysonDesktop.Services
{

    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient();
            // _client.BaseAddress = new Uri("https://api.seusite.com/");
        }

        public async Task<List<TransactionModel>> GetTransactionsAsync()
        {
            // Simulando chamada de API externa
            // Na real seria: var response = await _client.GetStringAsync("transactions");
            
            await Task.Delay(500); // Simula delay de rede

            return new List<TransactionModel>
            {
                new TransactionModel { HashId = "PrJTIfqX...", From = "SYSTEM_MINT", To = "MIIBCgKCAQE...", Amount = "6 Dtc", Date = "12/06/2025" },
                new TransactionModel { HashId = "OwMSQuJO...", From = "SYSTEM_MINT", To = "MIIBCgKCAQE...", Amount = "2 Dtc", Date = "12/06/2025" },
                new TransactionModel { HashId = "FpFsd6XL...", From = "SYSTEM_MINT", To = "MIIBCgKCAQE...", Amount = "5 Dtc", Date = "12/06/2025" },
                new TransactionModel { HashId = "K6mUaDa6...", From = "SYSTEM_MINT", To = "MIIBCgKCAQE...", Amount = "4 Dtc", Date = "12/06/2025" }
            };
        }
    }
}