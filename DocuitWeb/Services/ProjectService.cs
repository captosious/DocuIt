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
    public class ProjectService
    {
        private AppSettings _appSettings;
        private MyHttp _myHttp;
        private string _resource = "/project";

        public ProjectService(AppSettings appSettings, MyHttp myHttp)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
        }

        public async Task<IEnumerable<Project>> FetchGetAllAsync()
        {
            List<Project> projects = new List<Project>();
            Project project = new Project();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            project.CompanyId = _appSettings.CompanyId;

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/getall");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                projects = JsonConvert.DeserializeObject<List<Project>>(responseBody);
                return await Task.FromResult(projects);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Project> PutAsync(Project project)
        {
            project.CompanyId = _appSettings.CompanyId;
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                return await Task.FromResult(project);
            }
            catch
            {
                return null;
            }
        }

        // Project Security

        public async Task<List<ProjectUserSecurity>> FetchProjectUsers(Project project)
        {
            IEnumerable<ProjectUserSecurity> projects = new List<ProjectUserSecurity>();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/projectsecurity/GetProjectUsers");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                projects = JsonConvert.DeserializeObject<IEnumerable<ProjectUserSecurity>>(responseBody);
                return await Task.FromResult((List<ProjectUserSecurity>)projects);
            }
            catch
            {
                return null;
            }
        }


        // Service for Project Security (not in a separate file)

        public async Task<List<ProjectUserSecurity>> FetchProjectUserSecurity(Project project)
        {
            IEnumerable<ProjectUserSecurity> projects = new List<ProjectUserSecurity>();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/projectsecurity/GetProjectUserSecurity");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                projects = JsonConvert.DeserializeObject<IEnumerable<ProjectUserSecurity>>(responseBody);
                return await Task.FromResult((List<ProjectUserSecurity>)projects);
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> SaveProjectUserSecurity(IEnumerable<ProjectUserSecurity> projectUserSecurities)
        {
            List<ProjectSecurity> projectSecurities = new List<ProjectSecurity>();
            ProjectSecurity projectSecurity;
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            //Prepare ProjectSecurity Object from ProjectUserSecurity
            foreach (ProjectUserSecurity user in projectUserSecurities)
            {
                projectSecurity = new ProjectSecurity();
                projectSecurity.CompanyId = user.CompanyId;
                projectSecurity.ProjectId = user.ProjectId;
                projectSecurity.UserId = user.UserId;
                
            }


            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/projectsecurity");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(httpClient.BaseAddress, httpRequestMessage.Content);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                projects = JsonConvert.DeserializeObject<IEnumerable<ProjectUserSecurity>>(responseBody);
                return 0; 
            }
            catch
            {
                return -1;
            }
        }

    }
}
