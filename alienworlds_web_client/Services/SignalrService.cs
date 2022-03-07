using alienworlds_web_client.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace alienworlds_web_client.Services
{
    public interface ISignalrService
    {
        EventHandler<string> testupdated { get; set; }

        EventHandler<WaxAccountViewModel>? e_add_wax_account { get; set; }
        EventHandler<WaxAccountViewModel>? e_update_wax_account { get; set; }
        EventHandler<WAX_ID>? e_remove_wax_account { get; set; }

        EventHandler<WaxMiner>? e_add_wax_account_miner { get; set; }
        EventHandler<WaxMiner>? e_update_wax_account_miner { get; set; }
        EventHandler<WAX_ID>? e_remove_wax_account_miner { get; set; }

        EventHandler<WaxMonitorViewModel>? e_add_wax_account_monitor_m { get; set; }
         EventHandler<WaxMonitorViewModel>? e_add_wax_account_monitor_i { get; set; }

        EventHandler<WaxMonitorViewModel>? e_remove_wax_account_monitor_m { get; set; }
        EventHandler<WaxMonitorViewModel>? e_remove_wax_account_monitor_i { get; set; }
        EventHandler<WaxMiner>? e_update_wax_account_monitor { get; set; }
        Task InitSignalR();
        Task Reconnect();
    }
    public class SignalrService: ISignalrService
    {

        private HubConnection _connection;
        private string url = "https://api.monesv.ml/socket";//http://localhost:5235
        public EventHandler<string> testupdated { get; set; }
        public EventHandler<WaxAccountViewModel>? e_add_wax_account { get; set; }
        public EventHandler<WaxAccountViewModel>? e_update_wax_account { get; set; }
        public EventHandler<WAX_ID>? e_remove_wax_account { get; set; }

        public EventHandler<WaxMiner>? e_add_wax_account_miner { get; set; }
        public EventHandler<WaxMiner>? e_update_wax_account_miner { get; set; }
        public EventHandler<WAX_ID>? e_remove_wax_account_miner { get; set; }

        public EventHandler<WaxMonitorViewModel>? e_add_wax_account_monitor_m { get; set; }
        public EventHandler<WaxMonitorViewModel>? e_add_wax_account_monitor_i { get; set; }

        public EventHandler<WaxMonitorViewModel>? e_remove_wax_account_monitor_m { get; set; }
        public EventHandler<WaxMonitorViewModel>? e_remove_wax_account_monitor_i { get; set; }

        public EventHandler<WaxMiner>? e_update_wax_account_monitor { get; set; }
        private ICookieService _cookieService;

        public SignalrService(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }
        public async Task Reconnect()
        {
            await _connection.StopAsync();
            await InitSignalR();
        }
        public async Task InitSignalR()
        {
            UserViewModel? _user = await _cookieService.GetCookie<UserViewModel?>("user_cookie");
            if(_user != null)
            {
                _connection = new HubConnectionBuilder().WithUrl(url, options => {
                    options.AccessTokenProvider = async () =>
                    {
                        return await Task.FromResult(_user.token);
                    };  
                }).Build();
            }
            else
            {
                _connection = new HubConnectionBuilder().WithUrl(url, options => {}).Build();
            }
                   
            RegisterActions();
            await _connection.StartAsync();
        }
        private void RegisterActions()
        {
            _connection.On<WaxAccountViewModel>("add_wax_account", m => {e_add_wax_account?.Invoke(this, m);});
            _connection.On<WaxAccountViewModel>("update_wax_account", m => { e_update_wax_account?.Invoke(this, m); });
            _connection.On<WAX_ID>("remove_wax_account", m => { e_remove_wax_account?.Invoke(this, m); });

            _connection.On<WaxMiner>("add_wax_account_miner", m => { e_add_wax_account_miner?.Invoke(this, m); });
            _connection.On<WaxMiner>("update_wax_account_miner", m => { e_update_wax_account_miner?.Invoke(this, m); });
            _connection.On<WAX_ID>("remove_wax_account_miner", m => { e_remove_wax_account_miner?.Invoke(this, m); });

            _connection.On<WaxMonitorViewModel>("add_wax_account_monitor_m", m => { e_add_wax_account_monitor_m?.Invoke(this, m); });
            _connection.On<WaxMonitorViewModel>("add_wax_account_monitor_i", m => { e_add_wax_account_monitor_i?.Invoke(this, m); });

            _connection.On<WaxMonitorViewModel>("remove_wax_account_monitor_m", m => { e_remove_wax_account_monitor_m?.Invoke(this, m); });
            _connection.On<WaxMonitorViewModel>("remove_wax_account_monitor_i", m => { e_remove_wax_account_monitor_i?.Invoke(this, m); });


            _connection.On<WaxMiner>("update_wax_account_monitor", m => { e_update_wax_account_monitor?.Invoke(this, m); });

        }
    }

}
