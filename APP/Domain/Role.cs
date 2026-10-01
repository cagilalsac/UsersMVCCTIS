using CORE.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Domain
{
    public class Role : Record
    {
        [Required, StringLength(25)]
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; } = new(); // navigation property
    }
}
