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
    public class DossierElementService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/dossierelement";

        public DossierElementService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<DossierElement>> FetchGetAllAsync(Dossier dossier)
        {
            List<DossierElement> dossierElements = new List<DossierElement>();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpClient httpClient = _myHttp.GetClient();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/getall");

            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                dossierElements = JsonConvert.DeserializeObject<List<DossierElement>>(responseBody);
                return await Task.FromResult(dossierElements);
            }
            catch
            {
                return null;
            }
        }

        //public async Task<Dossier> PutAsync(Dossier dossier)
        //{
        //    dossier.CompanyId = _appSettings.CompanyId;
        //    HttpClient httpClient = new HttpClient();
        //    HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

        //    httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(dossier), Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
        //    try
        //    {
        //        response.EnsureSuccessStatusCode();
        //        return await Task.FromResult(dossier);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}


