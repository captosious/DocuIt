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
        private string _resource = "questionnaire";
        public QuestionnaireParameters parameters = new QuestionnaireParameters();

        public QuestionnaireQAService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
            //parameters.CompanyId = 0;
            //parameters.QuestionnaireTypeId = "";
        }

        public async Task<IEnumerable<Questionnaire>>FetchAsyncQuestionnaireQuestions()
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            IEnumerable<Questionnaire> questionnaire;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/" + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                questionnaire = JsonConvert.DeserializeObject<List<Questionnaire>>(responseBody);
                return await Task.FromResult(questionnaire);
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<QuestionnaireReportAnswers>> FetchAsyncQuestionnaireAnswers()
        {
            string resource_obj = "questionnairereportanswers";
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            IEnumerable<QuestionnaireReportAnswers> questionnaire;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + resource_obj);
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

        // Controller: QuestionnaireReportAnswers
        public async Task<int> SaveQuestionnaire(IEnumerable<QuestionnaireReportAnswers> questionnaireQAs)
        {
            string resource_obj = "questionnairereportanswers";
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpClient httpClient = _myHttp.GetClient();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/" + resource_obj);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(questionnaireQAs), Encoding.UTF8, "application/json");

            //HttpResponseMessage response = await httpClient.SendAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                //response.EnsureSuccessStatusCode();
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        // Local Classes 
        public class QuestionnaireParameters
        {
            public int CompanyId { get; set; }
            public int ProjectId { get; set; }
            public int DossierId { get; set; }
            public int QuestionnaireReportId { get; set; }
        }
    }
}
