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
using alienworlds_web_client.Model;

namespace alienworlds_web_client.Shared
{
    public partial class NavMenu
    {
        [Inject]
        private ICookieService CookieService { get; set; }
        private bool login { get; set; }
        protected override async Task OnInitializedAsync()
        {
            UserViewModel? _user = await CookieService.GetCookie<UserViewModel?>("user_cookie");
            if (_user != null)
            {
                login = true;
            }
            else
            {
                login = false;
            }
        }
    }
}