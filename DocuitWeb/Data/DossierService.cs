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
        private MyHttp _myHttp;

        public DossierService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<Dossier>> FetchGetAllAsync(Project project)
        {
            List<Dossier> dossiers = new List<Dossier>();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpClient httpClient = _myHttp.GetClient();
            
            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/getall");

            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                dossiers = JsonConvert.DeserializeObject<List<Dossier>>(responseBody);
                httpClient.Dispose();
                return await Task.FromResult(dossiers);
            }
            catch
            {
                httpClient.Dispose();
                return null;
            }
        }

        public async Task<Dossier> PutAsync(Dossier dossier)
        {
            dossier.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                return await Task.FromResult(dossier);
            }
            catch
            {
                httpClient.Dispose();
                return null;
            }
        }
    }
}

