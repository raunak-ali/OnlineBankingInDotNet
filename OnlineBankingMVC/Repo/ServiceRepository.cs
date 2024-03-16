using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OnlineBankingMVC.Repo
{
        public class ServiceRepository
    {
        HttpClient client;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public ServiceRepository(HttpClient httpClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            client = httpClient;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            client.BaseAddress = new Uri(_configuration["apiBaseURL"]);

            var token = _httpContextAccessor.HttpContext?.Session.GetString("JWTToken");
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        }
        //public HttpResponseMessage VerifyLogin(string url, object model)
        //{
        //    return client.PostAsJsonAsync(url, model).Result;
        //}
        public HttpResponseMessage GetResponse(string url)
        {
            return client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return client.DeleteAsync(url).Result;
        }


    }
}
