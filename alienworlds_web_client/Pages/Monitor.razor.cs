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
using System.Text.Json.Nodes;
using alienworlds_web_client.Services;
using alienworlds_web_client.Model;
using System.Text.Json;

namespace alienworlds_web_client.Pages
{
    public partial class Monitor
    {
        [Inject]
        private IHttpService httpService { get; set; }
        private List<WaxMiner>? _wax_miner = new List<WaxMiner>();
        [Inject]
        private ISignalrService _SignalrService { get; set; }
        protected override async Task OnInitializedAsync()
        {
            load_data();
            _SignalrService.e_add_wax_account_monitor_i += add_wax_account_monitor_m_handler;
            _SignalrService.e_remove_wax_account_monitor_i += remove_wax_account_monitor_m_handler;
            _SignalrService.e_update_wax_account_monitor += update_wax_account_monitor_handler;
        }
        private async Task load_data()
        {           
            JsonObject? result = await httpService.Get<JsonObject>("/aw/load_wax_monitor");
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
                _wax_miner = JsonSerializer.Deserialize<List<WaxMiner>>(Convert.ToString(result["data"]));
                StateHasChanged();
            }
        }

        private async void add_wax_account_monitor_m_handler(object sender, WaxMonitorViewModel args)
        {
            load_data();
            StateHasChanged();
        }
        private async void remove_wax_account_monitor_m_handler(object sender, WaxMonitorViewModel args)
        {
            load_data();
            StateHasChanged();
        }
        private async void update_wax_account_monitor_handler(object sender, WaxMiner args)
        {
            for(int i = 0; i < _wax_miner.Count; i++)
            {
                if (_wax_miner[i].wax_account_id == args.wax_account_id)
                    _wax_miner[i] = args;
            }
            StateHasChanged();
        }
    }
}