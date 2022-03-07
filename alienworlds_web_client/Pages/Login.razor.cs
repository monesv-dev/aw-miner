using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using alienworlds_web_client;
using alienworlds_web_client.Shared;
using alienworlds_web_client.Services;
using System.Text.Json.Nodes;
using alienworlds_web_client.Model;

namespace alienworlds_web_client.Pages
{
    public partial class Login
    {

#pragma warning disable CS8618
        [Inject]
        private ICookieService CookieService { get; set; }
        [Inject]
        private IHttpService httpService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private ISignalrService _SignalrService { get; set; }
#pragma warning restore CS8618
        private LoginViewModel model = new LoginViewModel();
        private string? status = null;
        public async Task SubmitLogin()
        {
            JsonObject? result = await httpService.Post<JsonObject>("/auth/login", new { username= model.username, password= model.password });
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
                UserViewModel _user = new UserViewModel() {
                    username = Convert.ToString(result["username"]),
                    token = Convert.ToString(result["token"])
                };
#pragma warning disable CS8604 // Possible null reference argument.
                await CookieService.SetCookie(key:"user_cookie", value: _user, _date: DateTime.Parse(Convert.ToString(result["expire"])));
                await _SignalrService.Reconnect();
                NavigationManager.NavigateTo("/dashboard",false);
            }
            else
            {
                status = Convert.ToString(result["status"]);
            }
        }
        public async Task Test_Get()
        {
            UserViewModel? _user =await CookieService.GetCookie<UserViewModel>("user_cookie");
            if (_user != null)
            {

            }
        }
        public async Task Test_Remove()
        {
            await CookieService.RemoveCookie("user_cookie");
        }
    }
}