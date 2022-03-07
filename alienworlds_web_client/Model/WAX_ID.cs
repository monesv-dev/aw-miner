using aw_class;
using System.ComponentModel.DataAnnotations;

namespace alienworlds_web_client.Model
{
    public class WAX_ID
    {
        [Required]
        public int wax_account_id { get; set; } = 0;
        public miner tag_miner { get; set; }
    }
}
