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
        private string _resource = "/auth";
        HttpClient _httpClient; 
        readonly ProtectedSessionStorage _protectedStorage;

        public AccessService(AppSettings appSettings, HttpClient httpClient, ProtectedSessionStorage protectedStorage)
        {
            _appSettings = appSettings;
            _httpClient=  httpClient;
            _protectedStorage = protectedStorage;
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
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _protectedStorage.GetAsync<string>("Token").ToString());
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
