using alienworlds_web_client.Model;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace alienworlds_web_client.Services
{
    public interface IHttpService
    {
        Task<T?> Get<T>(string uri);
        Task<T?> Post<T>(string uri, object value);
    }
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;
        private NavigationManager _navigationManager;
        private ICookieService _cookieService;
        private IConfiguration _configuration;
        public HttpService(
    HttpClient httpClient,
    NavigationManager navigationManager,
    ICookieService cookieService,
    IConfiguration configuration)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _cookieService = cookieService;
            _configuration = configuration;
        }
        public async Task<T?> Get<T>(string uri)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uri);
            return await sendRequest<T>(request);
        }

        public async Task<T?> Post<T>(string uri, object value)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, uri);            
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
            return await sendRequest<T>(request);
        }
        private async Task<T?> sendRequest<T>(HttpRequestMessage request)
        {      
            UserViewModel? _user = await _cookieService.GetCookie<UserViewModel?>("user_cookie");
            if (_user != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _user.token);            
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _cookieService.RemoveCookie("user_cookie");
                _navigationManager.NavigateTo("/login", false);
            }
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
