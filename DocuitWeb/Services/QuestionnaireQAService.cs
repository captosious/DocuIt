using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using DocuitWeb.Services;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq;
using System.Collections.Generic;

namespace DocuitWeb.Services

{
    public class QuestionnaireQAService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/questionnaire";
        public QuestionnaireParameters parameters = new QuestionnaireParameters();

        public QuestionnaireQAService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
            //parameters.CompanyId = 0;
            //parameters.QuestionnaireTypeId = "";
        }

        public async Task<IEnumerable<QuestionnaireQA>> FetchAsyncQuestionnaireQuestions()
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            IEnumerable<QuestionnaireQA> questionnaire;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                questionnaire = JsonConvert.DeserializeObject<List<QuestionnaireQA>>(responseBody);
                return await Task.FromResult(questionnaire);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<QuestionnaireReportAnswers>> FetchAsyncQuestionnaireAnswers()
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            IEnumerable<QuestionnaireReportAnswers> questionnaire;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                questionnaire = JsonConvert.DeserializeObject<List<QuestionnaireReportAnswers>>(responseBody);
                return await Task.FromResult(questionnaire);
            }
            catch
            {
                return null;
            }
        }

        public class QuestionnaireParameters
        {
            public int CompanyId { get; set; }
            public int ProjectId { get; set; }
            public int DossierId { get; set; }
            public int QuestionnaireReportId { get; set; }
            public string QuestionnaireTypeId { get; set; }
        }
    }
}
