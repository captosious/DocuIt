using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DocuitWeb.Services
{
    public class UserService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/user";
        
        public UserService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<User> FetchAsync(User user)
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            User return_user;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetById");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return_user = JsonConvert.DeserializeObject<User>(responseBody);
                return await Task.FromResult(return_user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> PutAsync(User user)
        {
            user.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                return await Task.FromResult(0);
            }
            catch
            {
                httpClient.Dispose();
                return 1;
            }
        }

        public async Task<User> PatchAsync(User user)
        {
            JsonPatchDocument jsonPatchDocument;// = JsonPatchDocument<User>();

            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                return await Task.FromResult(user);
            }
            catch
            {
                httpClient.Dispose();
                return null;
            }
        }

        public async Task<int> SetPhotoAsync(MultipartFormDataContent form)
        {
            //user.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/SetPhoto");
            //httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            httpRequestMessage.Content = form;
            HttpResponseMessage response = await httpClient.PostAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                httpClient.Dispose();
                return await Task.FromResult(0);
            }
            catch
            {
                httpClient.Dispose();
                return 1;
            }
        }
    }
}