using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using DocuitWeb.ModelsParams;
using System.Collections.Generic;

namespace DocuitWeb.Services

{
    public class QuestionnaireService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/questionnaire";

        public QuestionnaireService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<Questionnaire> FetchAsync(QuestionnaireParams questionnaireParams)
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            IEnumerable<Questionnaire> questionnaire;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(questionnaireParams), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                questionnaire = JsonConvert.DeserializeObject<Questionnaire>(responseBody);
                return await Task.FromResult(questionnaire);
            }
            catch
            {
                return null;
            }
        }
    }
}
