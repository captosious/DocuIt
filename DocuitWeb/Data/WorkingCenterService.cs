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
    public class WorkingCenterService
    {
        private AppSettings _appSettings;
        //private readonly HttpClient _httpClient;
        private MyHttp _myHttp;
        private string _resource = "/workingcenter";

        public WorkingCenterService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<WorkingCenter>> FetchGetAllAsync()
        {
            WorkingCenter obj = new WorkingCenter();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpClient httpClient = _myHttp.GetClient();

            obj.CompanyId = _appSettings.CompanyId;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetAll");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
                return await Task.FromResult(JsonConvert.DeserializeObject<List<WorkingCenter>>(responseBody));
            }
            catch
            {
                
                return null;
            }
        }

        public async Task<WorkingCenter> PutAsync(WorkingCenter obj)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpClient httpClient = _myHttp.GetClient();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                return await Task.FromResult(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}
