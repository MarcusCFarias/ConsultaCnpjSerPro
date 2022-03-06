using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ConsultaCnpjSerPro.Model;

namespace ConsultaCnpjSerPro
{
    public class ConsultaCNPJ
    {
        private readonly string consumerKey = "";
        private readonly string consumerSecret = "";
        private readonly string completeEndPointAuthorizationRequest = "https://gateway.apiserpro.serpro.gov.br/token";
        private readonly string completeEndPointDataRequest = "https://gateway.apiserpro.serpro.gov.br/consulta-cnpj-df/v2/empresa/{0}";
        public async Task<DataSerproApi> GetAPI(string cnpj)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            DataSerproApi dataSerproApi = new DataSerproApi();

            var authorizationToken = await this.GetAuthorizationToken();

            if (!string.IsNullOrEmpty(authorizationToken))
            {
                dataSerproApi = await this.GetResource(authorizationToken, cnpj);
            }

            return dataSerproApi;
        }
        private async Task<string> GetAuthorizationToken()
        {
            string bearerToken = string.Empty;
            string encodePair = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", consumerKey, consumerSecret)));

            var requestAuthorization = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(completeEndPointAuthorizationRequest),
                Content = new StringContent("grant_type=client_credentials")
            };

            requestAuthorization.Headers.Add("Authorization", string.Format("Basic {0}", encodePair));
            requestAuthorization.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpClient httpClient = new HttpClient();
            var tokenResult = await httpClient.SendAsync(requestAuthorization);

            if (tokenResult.StatusCode == HttpStatusCode.OK)
            {
                var bearerData = await tokenResult.Content.ReadAsStringAsync();
                bearerToken = JObject.Parse(bearerData)["access_token"].ToString();
            }

            return bearerToken;
        }
        private async Task<DataSerproApi> GetResource(string bearerToken, string cnpj)
        {
            var requestData = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(string.Format(completeEndPointDataRequest, cnpj)),
            };

            requestData.Headers.Add("Accept", "application/json");
            requestData.Headers.Add("Authorization", String.Format("Bearer {0}", bearerToken));

            HttpClient httpClient = new HttpClient();
            var results = await httpClient.SendAsync(requestData);

            var reader = new StreamReader(await results.Content.ReadAsStreamAsync());
            string responseString = reader.ReadToEnd();

            dynamic jsonDataSerproApi = JsonConvert.DeserializeObject<DataSerproApi>(responseString);

            return jsonDataSerproApi;
        }
    }
}
