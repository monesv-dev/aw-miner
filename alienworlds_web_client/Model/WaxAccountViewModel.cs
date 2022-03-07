namespace alienworlds_web_client.Model
{
    public class WaxAccountViewModel
    {
        public int wax_account_id { get; set; }
        public string username { get; set; } = "";
        public string wax_username { get; set; } = "";
        public string wax_password { get; set; } = "";
        public string wax_token { get; set; } = "";
        public DateTime w_create_date { get; set; } = DateTime.Now;
    }
    public class ADDWaxAccountViewModel
    {
        public string wax_username { get; set; } = "";
        public string wax_password { get; set; } = "";
        public string wax_token { get; set; } = "";
    }
}
