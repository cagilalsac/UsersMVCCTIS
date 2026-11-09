using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Authentication.Models
{
    public class LoginRequest
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
