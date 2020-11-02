using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;

namespace DocuitWeb.Data
{
    public class UserService
    {
        private AppSettings _appSettings;
        private HttpClient _httpClient;
        private string _resource = "/user";
        
        public UserService(AppSettings appSettings, HttpClient httpClient)
        {
            _appSettings = appSettings;
            _httpClient = httpClient;
        }

        public async Task<User> FetchAsync(User user)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            User return_user;

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetById");

            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

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

        public async Task<User> PutAsync(User user)
        {
            user.CompanyId = _appSettings.CompanyId;
            
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                return await Task.FromResult(user);
            }
            catch
            {
                return null;
            }
        }
    }
}
