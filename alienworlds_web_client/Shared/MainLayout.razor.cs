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

namespace alienworlds_web_client.Shared
{
    public partial class MainLayout 
    {
        [Inject]
        private ISignalrService _SignalrService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await _SignalrService.InitSignalR();
        }

    }

}