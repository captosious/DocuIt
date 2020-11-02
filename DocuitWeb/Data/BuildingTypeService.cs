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
        private readonly HttpClient _httpClient;
        private string _resource = "/buildingtype";

        public BuildingTypeService(AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BuildingType>> FetchGetAllAsync()
        {
            BuildingType obj = new BuildingType();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            obj.CompanyId = _appSettings.CompanyId;

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetAll");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return await Task.FromResult(JsonConvert.DeserializeObject<List<BuildingType>>(responseBody));
            }
            catch
            {
                return null;
            }
        }

        public async Task<BuildingType> PutAsync(BuildingType obj)
        {
            obj.CompanyId = _appSettings.CompanyId;
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress, httpRequestMessage.Content);
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
