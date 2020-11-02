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
    public class DossierService
    {
        private AppSettings _appSettings;
        private string _resource = "/dossier";
        private HttpClient _httpClient;

        public DossierService(AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Dossier>> FetchGetAllAsync(Project project)
        {
            List<Dossier> dossiers = new List<Dossier>();
            
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            
            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/getall");

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                dossiers = JsonConvert.DeserializeObject<List<Dossier>>(responseBody);
                return await Task.FromResult(dossiers);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Dossier> PutAsync(Dossier dossier)
        {
            dossier.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                return await Task.FromResult(dossier);
            }
            catch
            {
                return null;
            }
        }
    }
}

