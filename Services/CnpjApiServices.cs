using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using QuickOrder.Models;


namespace QuickOrder.Services
{
    public class CnpjApiServices
    {
        //API PARA PESQUISAR CNPJ
        private const string BaseUrl = "https://receitaws.com.br/v1/cnpj/";

        public async Task<Client?> GetClientByCnpjAsync(string cnpj)
        {
            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ConsultaCNPJ");

                    HttpResponseMessage response = await client.GetAsync($"{BaseUrl}{cnpj}");

                    if (!response.IsSuccessStatusCode)
                        return null;

                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);

                    var clientObj = new Client(
                        cnpj:cnpj,
                        name: data["nome"]?.ToString()
                    );

                    return clientObj;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
