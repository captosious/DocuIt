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
    public class BuildingTypeService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/buildingtype";

        public BuildingTypeService(AppSettings appSettings ,MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<BuildingType>> FetchGetAllAsync()
        {
            BuildingType obj = new BuildingType();
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
                httpClient.Dispose();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<BuildingType>>(responseBody));
            }
            catch
            {
                httpClient.Dispose();
                return null;
            }
        }

        public async Task<BuildingType> PutAsync(BuildingType obj)
        {
            obj.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = new HttpClient(); 
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient = _myHttp.GetClient();

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
