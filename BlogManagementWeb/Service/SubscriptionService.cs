using BlogManagementWeb.Service.IService;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BlogManagementWeb.Service
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7094";

        public SubscriptionService(HttpClient httpClient, IConfiguration configuration/*, IBlogRepository blogRepository*/)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            // _apiBaseUrl = configuration.GetValue<string>("ServiceUrls:BlogManagementAPI");
            // _blogRepository = blogRepository;
        }
        public async Task<IEnumerable<Subscription>> GetUsersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/SubscriptionController/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<Subscription> blog = JsonConvert.DeserializeObject<IEnumerable<Subscription>>(content);
            return blog;
        }

        public async Task<Subscription> GetUserByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/SubscriptionController/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Subscription blogPost = JsonConvert.DeserializeObject<Subscription>(content);
            return blogPost;
        }

        public async Task<Subscription> CreateUserAsync(SubsrciptionDTO blog)
        {
            string json = JsonConvert.SerializeObject(blog);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/SubscriptionController/", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Subscription createdBlogPost = JsonConvert.DeserializeObject<Subscription>(responseContent);
            return createdBlogPost;

        }
        public async Task DeleteUserAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/SubscriptionController/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
