using aw_class;

namespace alienworlds_web_client.Model
{
    public class ADDWaxMinerViewModel
    {
        public int wax_account_id { get; set; }
        public miner tag_miner { get; set; } = miner.NONE;
        public string? accountName { get; set; } = null;
        public string? publicKeys { get; set; } = null;
        public user_state state { get; set; } = user_state.NONE;
        public string? state_settings { get; set; } = null;
        public DateTime? state_end { get; set; } = null;
        public DateTime? last_state { get; set; } = null;
    }
    public class WaxMinerViewModel
    {
        public new int wax_account_id { get; set; }
        public string username { get; set; } = "";
        public string wax_username { get; set; } = "";
        public string wax_password { get; set; } = "";
        public string wax_token { get; set; } = "";
        public DateTime w_create_date { get; set; } = DateTime.Now;
        public WaxMiner? miner { get; set; }
    }
    public class WaxMiner
    {
        public int wax_account_id { get; set; }
        public miner tag_miner { get; set; } = miner.NONE;
        public string? accountName { get; set; } = null;
        public string? publicKeys { get; set; } = null;
        public user_state? state { get; set; } = null;
        public string? state_settings { get; set; } = null;
        public DateTime? state_end { get; set; } = null;
        public DateTime? last_state { get; set; } = null;
        public DateTime? m_create_date { get; set; } = null;
    }

    public class state_setting
    {
        public int miner { get; set; }
        public List<get_state_setting> settings { get; set; } = new List<get_state_setting> { };
    }
    public class get_state_setting
    {
        public string _key { get; set; }
        public int _value { get; set; }
    }

    public class publickey
    {
        public int miner { get; set; }
        public List<getpublickey>?key { get; set; } = new List<getpublickey> { };
    }
    public class getpublickey
    {
        public string pubkey { get; set; } = "";
    }
}
