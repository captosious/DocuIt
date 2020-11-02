using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;

namespace DocuitWeb.Data
{
    public class CompanyService
    {
        private AppSettings _appSettings;
        private string _resource = "/company";
        private HttpClient _httpClient;

        public CompanyService(AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        } 

        public async Task<Company> FetchAsync()
        {
            Company company = new Company();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            company.CompanyId = _appSettings.CompanyId;

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                company = JsonConvert.DeserializeObject<Company>(responseBody);
                return await Task.FromResult(company);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Company> PutAsync(Company company)
        {
            company.CompanyId = _appSettings.CompanyId;
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(company), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                return await Task.FromResult(company);
            }
            catch
            {
                return null;
            }
        }
    }
}
