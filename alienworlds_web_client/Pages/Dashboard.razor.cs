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
using System.Text.Json;
using alienworlds_web_client.Model;
using aw_class;

namespace alienworlds_web_client.Pages
{
    public partial class Dashboard
    {
        [Inject]
        private ICookieService CookieService { get; set; }
        [Inject]
        private IHttpService httpService { get; set; }
        [Parameter]
        public string select { get; set; }

        [Inject]
        private ISignalrService _SignalrService { get; set; }


        private ADDWaxAccountViewModel _add_wax_account = new ADDWaxAccountViewModel();
        private List<WaxAccountViewModel>? _wax_account = new List<WaxAccountViewModel>();
        private List<WaxMinerViewModel>? _wax_miner = new List<WaxMinerViewModel> ();
        private ADDWaxMinerViewModel _add_wax_miner = new ADDWaxMinerViewModel();

        private List<WaxMonitorViewModel>? _wax_monitor_master = new List<WaxMonitorViewModel> ();
        private ADDWaxMonitorViewModel _add_wax_monitor = new ADDWaxMonitorViewModel();


        private publickey pubkey = new publickey();
        private state_setting state_settings = new state_setting() {  };

        private string test = "";

        private async Task update_wax_account(WaxAccountViewModel a_model)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/update_wax_account", a_model);
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
                
            }
        }

        private async Task update_wax_miner(WaxMinerViewModel m_model)
        {
            Console.WriteLine(JsonSerializer.Serialize(m_model));
            JsonObject? result = await httpService.Post<JsonObject>("/aw/update_wax_account_miner", m_model.miner);
            Console.WriteLine(JsonSerializer.Serialize(result));
            if (result == null)
                return;        
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {

            }
        }

        protected override async Task OnInitializedAsync()
        {
            Dictionary<string, int> state_setting0 = JsonSerializer.Deserialize<Dictionary<string, int>>("{\"INITIALIZING\":1,\"MINE_SUCCESS\":5,\"MINE_TOO_SOON\":10,\"CPU_USAGE_LIMITED\":30,\"NOTHING_TO_MINE\":0}");
            state_setting state_setting1 = new state_setting();
            List<get_state_setting> state_setting2 = new List<get_state_setting>();
            foreach (var item in state_setting0)
            {state_setting2.Add(new get_state_setting { _key = item.Key, _value = Convert.ToInt32(item.Value) });}
            state_setting1.settings = state_setting2;
            state_settings = state_setting1;
            await load_data();

            _SignalrService.e_add_wax_account += add_wax_account_handler;
            _SignalrService.e_update_wax_account += update_wax_account_handler;
            _SignalrService.e_remove_wax_account += remove_wax_account_handler;
            _SignalrService.e_add_wax_account_miner += add_wax_account_miner_handler;
            _SignalrService.e_update_wax_account_miner += update_wax_account_miner_handler;
            _SignalrService.e_remove_wax_account_miner += remove_wax_account_miner_handler;
            _SignalrService.e_add_wax_account_monitor_m += add_wax_account_monitor_m_handler;
            _SignalrService.e_remove_wax_account_monitor_m += remove_wax_account_monitor_m_handler;
        }

        private async void add_wax_account_handler(object sender, WaxAccountViewModel args)
        {
            _wax_account.Add(args);
            StateHasChanged();
        }
        private async void update_wax_account_handler(object sender, WaxAccountViewModel args)
        {         
            int find =  _wax_account.FindIndex(x => x.wax_account_id == args.wax_account_id);
            if (find != -1)
            {
                _wax_account[find] = args;
                StateHasChanged();
            }
        }

        private async void remove_wax_account_handler(object sender, WAX_ID args)
        {
            var find = _wax_account.Find(x => x.wax_account_id == args.wax_account_id);
            if (find != null)
            {
                _wax_account.Remove(find);
                StateHasChanged();
            }
        }

        private async void add_wax_account_miner_handler(object sender, WaxMiner args)
        {
            var find = _wax_account.FirstOrDefault(x => x.wax_account_id == args.wax_account_id);
            if (find != null)
            {
                _wax_miner.Add(new WaxMinerViewModel {
                    username = find.username,
                    wax_account_id = find.wax_account_id,
                    wax_password = find.wax_password,
                    wax_token = find.wax_token,
                    wax_username=find.wax_username,
                    w_create_date = find.w_create_date,
                    miner= args                
                });
                StateHasChanged();
            }
        }
        private async void update_wax_account_miner_handler(object sender, WaxMiner args)
        {
            int find = _wax_miner.FindIndex(x => x.wax_account_id == args.wax_account_id && x.miner.tag_miner == args.tag_miner);
            if(find != -1)
            {
                _wax_miner[find].miner = args;
                StateHasChanged();
            }
        }

        private async void remove_wax_account_miner_handler(object sender, WAX_ID args)
        {
            var find = _wax_miner.FirstOrDefault(x => x.wax_account_id == args.wax_account_id && x.miner.tag_miner == args.tag_miner);
            if (find != null)
            {
                _wax_miner.Remove(find);
                StateHasChanged();
            }
        }
        private async void add_wax_account_monitor_m_handler(object sender, WaxMonitorViewModel args)
        {

                _wax_monitor_master.Add(args);
                StateHasChanged();

        }
        private async void remove_wax_account_monitor_m_handler(object sender, WaxMonitorViewModel args)
        {
            var find = _wax_monitor_master.FirstOrDefault(args);
            if (find != null)
            {
                _wax_monitor_master.Remove(find);
                StateHasChanged();
            }
        }

        private async Task load_data()
        {
            JsonObject? result = await httpService.Get<JsonObject>("/aw/load_wax_account");
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
#pragma warning disable CS8604
                _wax_account = JsonSerializer.Deserialize<List<WaxAccountViewModel>>(Convert.ToString(result["data"]["wax_account"]));
                _wax_miner = JsonSerializer.Deserialize<List<WaxMinerViewModel>>(Convert.ToString(result["data"]["wax_miner"]));
                _wax_monitor_master = JsonSerializer.Deserialize<List<WaxMonitorViewModel>>(Convert.ToString(result["data"]["wax_monitor_master"]));
                //Console.WriteLine(JsonSerializer.Serialize(_wax_miner));
#pragma warning restore CS8604
                //WaxMinerViewModel
            }
        }
        private async Task reload()
        {
            JsonObject? result = await httpService.Get<JsonObject>("/Aw/reload_miner");
        }
        private void viewpublickeys(int s_miner)
        {
            string[] pubkey0 = JsonSerializer.Deserialize<string[]>(_wax_miner[s_miner].miner.publicKeys);
            publickey pubkey1 = new publickey();
            pubkey1.miner = s_miner;
            List<getpublickey> pubkey2 = new List<getpublickey>();
            foreach (string item in pubkey0)
            {
                pubkey2.Add(new getpublickey { pubkey = item });
            }
            pubkey1.key = pubkey2;
            pubkey = pubkey1;

        }

        private void viewstatesteeings(int s_miner)
        {
            Dictionary<string,int> state_setting0 = JsonSerializer.Deserialize<Dictionary<string, int>>(_wax_miner[s_miner].miner.state_settings);
            state_setting state_setting1 = new state_setting();
            state_setting1.miner = s_miner;
            List<get_state_setting> state_setting2 = new List<get_state_setting>();
            foreach(var item in state_setting0)
            {
                state_setting2.Add(new get_state_setting {_key=item.Key,_value=Convert.ToInt32(item.Value) });
            }
            state_setting1.settings = state_setting2;
            state_settings = state_setting1;
            Console.WriteLine(JsonSerializer.Serialize(state_settings));
        }

        private void pre_update_public_key(List<getpublickey> _test)
        {
             _wax_miner[pubkey.miner].miner.publicKeys = JsonSerializer.Serialize(_test.Select(x => x.pubkey).ToArray());
        }

        private void pre_update_settings(List<get_state_setting> _test)
        {
            Dictionary<string, int> _result = new Dictionary<string, int>();
            foreach(var item in _test)
            {
                _result.Add(item._key,item._value);
            }
            _wax_miner[state_settings.miner].miner.state_settings = JsonSerializer.Serialize(_result);
        }

        private async void insert_wax_account(ADDWaxAccountViewModel _data)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/add_wax_account", _data);
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
            }
        }

        private async void remove_wax_account(int _wax_account_id)
        {           
            JsonObject? result = await httpService.Post<JsonObject>("/aw/remove_wax_account", new WAX_ID {wax_account_id=_wax_account_id,tag_miner=0});
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
                var find =_wax_account.Find(x=>x.wax_account_id == _wax_account_id);
                if (find != null)
                {
                   
                    _wax_account.Remove(find);
                    StateHasChanged();
                }
            }
        }

        private async void insert_wax_miner(ADDWaxMinerViewModel _data)
        {
            Dictionary<string, int> state_result = new Dictionary<string, int>();
            foreach (var item in state_settings.settings)
            {
                state_result.Add(item._key, item._value);
            }
            JsonObject? result = await httpService.Post<JsonObject>("/aw/add_wax_account_miner", new ADDWaxMinerViewModel {
                accountName = _data.accountName == null ? "": _data.accountName,
                last_state = DateTime.Now,
                state_end = DateTime.Now,
                publicKeys = _data.publicKeys ==null?"": _data.publicKeys,
                tag_miner  = _data.tag_miner,
                state = _data.state,
                state_settings = JsonSerializer.Serialize(state_result),
                wax_account_id = _data.wax_account_id
                
            });
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
            }
        }//remove_wax_account
        private async void remove_wax_miner(int _wax_account_id, miner tag_miner)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/remove_wax_account_miner", new WAX_ID { wax_account_id = _wax_account_id, tag_miner = tag_miner });
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
                var find = _wax_miner.FirstOrDefault(x => x.wax_account_id == _wax_account_id && x.miner.tag_miner == tag_miner);
                if (find != null)
                {
                    _wax_miner.Remove(find);
                    StateHasChanged();
                }
            }
        }
        private async Task add_wax_monitor(ADDWaxMonitorViewModel a_model)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/add_wax_account_monitor", a_model);
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
            }
        }
        private async Task update_wax_monitor(WaxMonitorViewModel a_model)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/update_wax_account_monitor", a_model);
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {              
            }
        }
        private async Task remove_wax_monitor(WaxMonitorViewModel a_model)
        {
            JsonObject? result = await httpService.Post<JsonObject>("/aw/remove_wax_account_monitor", a_model);
            if (result == null)
                return;
            else if (Convert.ToString(result["status"]) == "SUCCEED")
            {
            }
        }
    }
}