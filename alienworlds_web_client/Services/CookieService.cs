using Microsoft.JSInterop;
using System.Text.Json;

namespace alienworlds_web_client.Services
{
    public interface ICookieService
    {
        Task<T?> GetCookie<T>(string key);
        Task SetCookie<T>(string key, T value, DateTime _date);
        Task RemoveCookie(string key);
    }
    public class CookieService : ICookieService
    {
        private IJSRuntime _jsRuntime;
        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }
        public async Task<T?> GetCookie<T>(string key)
        {
            string json = await _jsRuntime.InvokeAsync<string>("cookie_function.get_cookie", key);
            if (json == null)
                return default;
            return JsonSerializer.Deserialize<T>(json);
        }
        public async Task SetCookie<T>(string key, T value,DateTime _date)
        {
            await _jsRuntime.InvokeVoidAsync("cookie_function.set_cookie", key, JsonSerializer.Serialize(value), _date);
        }
        public async Task RemoveCookie(string key)
        {
            await _jsRuntime.InvokeVoidAsync("cookie_function.remove_cookie", key);
        }
    }
}
