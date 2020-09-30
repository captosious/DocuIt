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
        private string _resource = "/buildingtype";

        public BuildingTypeService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<BuildingType>> FetchGetAllAsync()
        {
            BuildingType obj = new BuildingType();
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            obj.CompanyId = _appSettings.CompanyId;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetAll");

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

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
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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
