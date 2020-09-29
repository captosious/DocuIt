using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using System.Collections.Generic;

namespace DocuitWeb.Data
{
    public class StatusService
    {
        private AppSettings _appSettings;

        public StatusService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<Status>> FetchGetAllAsync()
        {
            IEnumerable<Status> status_list = new List<Status>();

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/status");

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(status_list), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                status_list = JsonConvert.DeserializeObject<IEnumerable<Status>>(responseBody);
                return await Task.FromResult(status_list);
            }
            catch
            {
                return null;
            }
        }
    }
}
