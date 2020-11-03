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
    public class SecurityService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/security";

        public SecurityService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<Security>> FetchAsync()
        {
            IEnumerable<Security> securities = new List<Security>();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(securities), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                securities = JsonConvert.DeserializeObject<IEnumerable<Security>>(responseBody);
                return await Task.FromResult(securities);
            }
            catch
            {
                return null;
            }
        }
    }
}
