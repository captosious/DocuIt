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
    public class BuildingTypeProjectService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/buildingtypeproject";
        
        public BuildingTypeProjectService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<BuildingTypeProject>> FetchGetAllAsync()
        {
            BuildingType obj = new BuildingType();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            obj.CompanyId = _appSettings.CompanyId;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetAll");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return await Task.FromResult(JsonConvert.DeserializeObject<List<BuildingTypeProject>>(responseBody));
            }
            catch
            {
                return null;
            }
        }

        public async Task<BuildingTypeProject> PutAsync(BuildingTypeProject obj)
        {
            obj.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

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
