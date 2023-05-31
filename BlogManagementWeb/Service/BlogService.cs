using BlogManagementWeb.Service.IService;
using Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace BlogManagementWeb.Service

{
    public class BlogService : IBlogService
    {
       // private readonly IBlogRepository _blogRepository;
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7094";

        public BlogService(HttpClient httpClient, IConfiguration configuration/*, IBlogRepository blogRepository*/)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            // _apiBaseUrl = configuration.GetValue<string>("ServiceUrls:BlogManagementAPI");
           // _blogRepository = blogRepository;
        }
        public async Task<IEnumerable<Models.Blog>> GetUsersAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/BlogController/");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<Models.Blog> blog = JsonConvert.DeserializeObject<IEnumerable<Models.Blog>>(content);
            return blog;
        }

        public async Task<Models.Blog> GetUserByIdAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"/api/BlogController/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            Models.Blog blogPost = JsonConvert.DeserializeObject<Models.Blog>(content);
            return blogPost;
        }

        public async Task<Models.Blog> CreateUserAsync(Models.Blog blog)
        {
            string json = JsonConvert.SerializeObject(blog);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/BlogController/", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Models.Blog createdBlogPost = JsonConvert.DeserializeObject<Models.Blog>(responseContent);
            return createdBlogPost;

        }

        public async Task<Models.Blog> UpdateUserAsync(int id, Models.Blog blog)
        {
            string json = JsonConvert.SerializeObject(blog);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"/api/BlogController/{id}", content);
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            Models.Blog updatedBlogPost = JsonConvert.DeserializeObject<Models.Blog>(responseContent);
            return updatedBlogPost;
        }

        public async Task DeleteUserAsync(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/BlogController/{id}");
            response.EnsureSuccessStatusCode();
        }
        //public IEnumerable<Blog> GetPendingBlogs()
        //{
        //    // Retrieve only pending blogs from the repository
        //    return _blogRepository.GetPendingBlogs();
        //}
    }
}
