using System;

namespace aw_class
{
    public class user_info 
    {
        //for online client
        public int wax_account_id { get; set; } = 0;
        public string username { get; set; } = "";
        public string wax_username { get; set; } = "";
        public string wax_password { get; set; } = "";
        //for token only
        public string accountName { get; set; } = "";
        public string token { get; set; } = "";
        public string[] publicKeys { get; set; } = null;
        public user_state state { get; set; } = user_state.INITIALIZING;
        public DateTime state_end { get; set; } = DateTime.Now;
        //status cooldown
        public int INITIALIZING { get; set; } = 0;
        public int MINE_SUCCESS { get; set; } = 0;
        public int MINE_TOO_SOON { get; set; } = 0;
        public int CPU_USAGE_LIMITED { get; set; } = 0;
        public int NOTHING_TO_MINE { get; set; } = 0;
        public int ERROR { get; set; } = 0;
    }
    public class users
    {
        public string error { get; set; } = null;
        public string message { get; set; } = null;
        public string accountName { get; set; } = "";
        public string[] publicKeys { get; set; } = new string[0];
        public bool verified { get; set; } = true;
    }
    public enum miner
    {
        NONE,
        ALIENWORLDS
        //alienworlds
    }
    public enum user_state
    {
        //initializing
        NONE,
        INITIALIZING,        
        READY,
        LOADINFO,
        AUTHORIZATION_REQUIRED,
        SUBMIT_TRANSECTION,
        PUSH_TRANSECTION,
        NET_USAGE_EXCEEDED,//net_usage_exceeded
        CPU_USAGE_LIMITED,
        NOTHING_TO_MINE,
        MINE_TOO_SOON,
        MINE_SUCCESS,
        BAG_EMPTY,
        HTTP_ERROR,
        ERROR
    }
    public class wax_query
    {
        public string code { get; set; } = "";
        public int index_position { get; set; } = 1;
        public bool json { get; set; } = true;
        public string key_type { get; set; } = "";
        public int limit { get; set; } = 1;
        public string lower_bound { get; set; } = "";
        public bool reverse { get; set; } = false;
        public string scope { get; set; } = "";
        public bool show_payer { get; set; } = false;
        public string table { get; set; } = "";
        public string upper_bound { get; set; } = "";
    }
    public class wax_miner
    {
        public bool more { get; set; } = false;
        public string next_key { get; set; } = "";
        public wax_miner_data[] rows { get; set; } = new wax_miner_data[0];
    }
    public class wax_miner_data
    {
        public string current_land { get; set; } = "";
        public DateTime last_mine { get; set; }
        private string _last_mine_tx;
        public string last_mine_tx { get { return _last_mine_tx; } set { _last_mine_tx = value.Substring(0, 16); } }
        public string miner { get; set; } = "";
    }
    public class wax_sign
    {
        public byte[] serializedTransaction { get; set; } = new byte[0];
        public string[] signatures { get; set; } = new string[0];

        public string error { get; set; } = null;
        public string message { get; set; } = null;
    }///{"error":"AuthenticationError","message":"Session Token is invalid or missing."}
    public class mine_work
    {
        public string account { get; set; } = "";
        public string rand_str { get; set; } = "";//nonce
        public byte[] rand_arr { get; set; }
        public string hex_digest { get; set; } = "";
    }

    public class chain_info
    {
        public int block_cpu_limit { get; set; } = 0;
        public int block_net_limit { get; set; } = 0;
        public string chain_id { get; set; } = "";
        public string fork_db_head_block_id { get; set; } = "";
        public int fork_db_head_block_num { get; set; } = 0;
        public string head_block_id { get; set; } = "";
        public int head_block_num { get; set; } = 0;
        public string head_block_producer { get; set; } = "";
        public DateTime head_block_time { get; set; } = DateTime.Now;
        public string last_irreversible_block_id { get; set; } = "";
        public int last_irreversible_block_num { get; set; } = 0;
        public string server_full_version_string { get; set; } = "";
        public string server_version { get; set; } = "";
        public string server_version_string { get; set; } = "";
        public int virtual_block_cpu_limit { get; set; } = 0;
        public int virtual_block_net_limit { get; set; } = 0;
    }

    public class block
    {
        public DateTime timestamp { get; set; } = DateTime.Now;
        public string producer { get; set; } = "";
        public int confirmed { get; set; } = 0;
        public string previous { get; set; } = "";
        public string transaction_mroot { get; set; } = "";
        public string action_mroot { get; set; } = "";
        public int schedule_version { get; set; } = 0;
        public string producer_signature { get; set; } = "";
        public string id { get; set; } = "";
        public int block_num { get; set; } = 0;
        public uint ref_block_prefix { get; set; } = 0;
    }

    /*Platform Guard*/
    public class platform_guard_data
    {
        public string account_name { get; set; } = "";
        public c_actions[] actions { get; set; } = new c_actions[] { new c_actions() };
    }
    public class c_actions
    {
        public string account { get; set; } = "m.federation";
        public string name { get; set; } = "mine";
        public c_authorization[] authorization { get; set; } = new c_authorization[] { new c_authorization() };
        public c_data data { get; set; } = new c_data { };
    }
    public class c_authorization
    {
        public string actor { get; set; } = "";
        public string permission { get; set; } = "active";
    }
    public class c_data
    {
        public string miner { get; set; } = "";
        public string nonce { get; set; } = "";
    }
    /*Platform Guard*/
    /*Platform Guard Result*/
    public class platform_guard
    {
        public string uniqid { get; set; } = "";
        public string account { get; set; } = "";
        public bool enabled { get; set; } = true;
        public string contract_account { get; set; } = "yeomenwarder";
        public string contract_action { get; set; } = "warder";
        public string contract_permission { get; set; } = "guard";
        public int buyram_bytes { get; set; } = 0;
        public bool account_tag_exists { get; set; } = true;
        public c_stats stats { get; set; } = new c_stats { };
    }
    public class c_stats
    {
        public string error { get; set; } = null;
        public int? errorCode { get; set; } = null;
        public string message { get; set; } = "";
        public bool cosign { get; set; } = true;
        public bool buyram { get; set; } = true;
        public int cosign_remaining_txs { get; set; } = 0;
    }
    /*Platform Guard Result*/
    public class transac_data
    {
        //online miner
        public int wax_account_id { get; set; } = 0;
        //end_online miner
        public string uniqid { get; set; } = "";
        public string warder { get; set; } = "";
        public string provider { get; set; } = "";
        public string accountname { get; set; } = "";
        public int cosign_remaining_txs { get; set; } = 0;
        public byte[] nonce { get; set; } = new byte[0];     
        public int ref_block_num { get; set; } = 0;
        public uint ref_block_prefix { get; set; } = 0;
        public DateTime timestamp { get; set; } = new DateTime();
    }
    public class transection_data
    {
        public string description { get; set; } = "jwt is insecure";
        public int[] serializedTransaction { get; set; } = new int[0];
        public bool freeBandwidth { get; set; } = true;
        public string website { get; set; } = "play.alienworlds.io";
    }
    public class packet_trx_data
    {
        public string[] signatures { get; set; } = new string[0];
        public int compression { get; set; } = 0;
        public string packed_context_free_data { get; set; } = "";
        public string packed_trx { get; set; } = "";  
    }
    public class tx_sign
    {
        //online miner
        public int wax_account_id { get; set; } = 0;
        //end_online miner
        public string accountname { get; set; } = "";
        public byte[] serializedTransaction { get; set; } = new byte[0];
    }

}

