using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;

namespace DocuitWeb.Services

{
    public class QuestionnaireTableService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/user";

        public QuestionnaireTableService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<QuestionnaireTable> FetchAsync(User user)
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
    }
}
