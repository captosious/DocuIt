﻿using System;
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

        public async Task<IEnumerable<ProjectUserSecurity>> FetchProjectUserSecurity(int CompanyId, int ProjectId)
        {
            IEnumerable<ProjectUserSecurity> projects = new List<ProjectUserSecurity>();
            ProjectUserSecurity project = new ProjectUserSecurity();
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            project.CompanyId = CompanyId;
            project.ProjectId = ProjectId;
            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + "/projectsecurity/GetProjectUserSecurity");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(project), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
            try
            {
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                projects = JsonConvert.DeserializeObject<IEnumerable<ProjectUserSecurity>>(responseBody);
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
    }
}