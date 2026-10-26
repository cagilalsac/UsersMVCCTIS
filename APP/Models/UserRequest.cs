using APP.Domain;
using CORE.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Models
{
    public class UserRequest : Record
    {
        [Required(ErrorMessage = "{0} is required!")] 
        [StringLength(30, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} must be minimum {2} maximum {1} characters!")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public Genders Gender { get; set; }

        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        public double Score { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? StatusId { get; set; }

        [DisplayName("Roles")]
        [Required(ErrorMessage = "{0} is required!")]
        public List<int> RoleIds { get; set; } = new();
    }
}
