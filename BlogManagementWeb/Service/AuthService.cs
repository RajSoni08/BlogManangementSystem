using BlogManagementWeb.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace BlogManagementWeb.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        //private readonly string _apiBaseUrl;
        private const string BaseUrl = "https://localhost:7094";

        public AuthService(HttpClient httpClient)
        {
            _httpClient = new HttpClient();
            //_apiBaseUrl = apiBaseUrl;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO model)
        {
            string json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("api/Home/login", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            LoginResponseDTO createdBlogPost = JsonConvert.DeserializeObject<LoginResponseDTO>(responseContent);
            return createdBlogPost;
        }
    }

    
}
