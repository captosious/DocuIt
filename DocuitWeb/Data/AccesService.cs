using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ProtectedBrowserStorage;

namespace DocuitWeb.Data
{
    public class AccessService
    {
        private AppSettings _appSettings;
        private HttpClient _httpClient;
        private string _resource = "/auth";

        public AccessService(AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient=  httpClient;
        }

        public async Task<Login> LogIn(string Username, string Password)
        {
            Login login = new Login();
            Login login_response = new Login();
            IdentityUser user = new IdentityUser();

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            login.CompanyId = _appSettings.CompanyId;
            login.UserName = Username;
            login.Password = Password;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/login");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                login_response = JsonConvert.DeserializeObject<Login>(responseBody);
                if (login_response != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login_response.Token);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                return await Task.FromResult(login_response);
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> LogOut()
        {
            return null;
        }

        public async Task<IEnumerable<BuildingTypeProject>> FetchGetAllAsync()
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
