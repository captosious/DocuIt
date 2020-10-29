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
        private string _resource = "/user";

        public UserService(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task<User> FetchAsync(User user)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            user.CompanyId = _appSettings.CompanyId;
            user.Username = _appSettings.Username;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                user = JsonConvert.DeserializeObject<User>(responseBody);
                return await Task.FromResult(user);
            }
            catch
            {
                return null;
            }
        }

        public async Task<User> PutAsync(User user)
        {
            user.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
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
