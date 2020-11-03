using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DocuitWeb.Models
{
    public class MyHttp
    {
        public IHttpClientFactory _factoryhttp;
        public string Token = "";

        public MyHttp(IHttpClientFactory factoryhttp)
        {
            _factoryhttp=factoryhttp;
        }

        public HttpClient GetClient()
        {
            HttpClient httpClient = _factoryhttp.CreateClient("DocuItService");
            if (Token != "")
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);
            }
            return httpClient;
        }
    }
}
