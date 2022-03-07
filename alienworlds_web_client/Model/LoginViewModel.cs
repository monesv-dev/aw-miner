using System.ComponentModel.DataAnnotations;

namespace alienworlds_web_client.Model
{
    public class LoginViewModel
    {
        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
