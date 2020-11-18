using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DocuitWeb.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Components.Authorization;

namespace DocuitWeb.Data
{
    public class AccessService
    {
        private AppSettings _appSettings;
        private HttpClient _httpClient;
        private string _resource = "/auth";
        public Login MyLogin { get; set; }
        private MyHttp _myHttp;
        private AuthenticationStateProvider _AuthenStateProv;

        public AccessService(AppSettings appSettings, MyHttp myHttp, AuthenticationStateProvider AuthenStateProv)
        {
            _appSettings = appSettings;
            _myHttp = myHttp;
            _AuthenStateProv = AuthenStateProv;
            MyLogin = new Login();
        }

        public async Task LogInAsync(string Username, string Password)
        {
            HttpClient httpClient = _myHttp.GetClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(); 

            MyLogin.CompanyId = _appSettings.CompanyId;
            MyLogin.UserName = Username;
            MyLogin.Password = Password;
            
            httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/login");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(MyLogin), Encoding.UTF8, "application/json");
            httpResponseMessage.EnsureSuccessStatusCode();
            try
            {
                httpResponseMessage = httpClient.Send(httpRequestMessage);
                string responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
                MyLogin = JsonConvert.DeserializeObject<Login>(responseBody);
                if (MyLogin != null)
                {
                    _myHttp.Token = MyLogin.Token;
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MyLogin.Token);
                    ((CustomAuthenticationStateProvider)_AuthenStateProv).MarkUserAsAuthenticated(Username, MyLogin.SecurityId.ToString());
                }
                else
                {
                    // If somethig went wrong... better to leave the user as logged off.
                    LogOut();
                }
                httpClient.Dispose();
            }
            catch (Exception ex)
            {
                // If somethig went wrong... better to leave the user as logged off.
                LogOut();
            }
        }

        public void LogOut()
        {
            ((CustomAuthenticationStateProvider)_AuthenStateProv).MarkUserAsNotAuthenticated();
            MyLogin = null;
        }

        public async Task<IEnumerable<BuildingTypeProject>> FetchGetAllAsync()
        {
            BuildingType obj = new BuildingType();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            obj.CompanyId = _appSettings.CompanyId;

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource + "/GetAll");
            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
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
            
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            _httpClient.BaseAddress = new Uri(_appSettings.DocuItServiceServer + _resource);

            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress, httpRequestMessage.Content);
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
