using CORE.Domain;
using System.ComponentModel;

namespace APP.Models
{
    public class StatusResponse : Record
    {
        public string Title { get; set; }



        // Way 1:
        [DisplayName("Users")]
        public string UserNamesR { get; set; }

        [DisplayName("User Count")]
        public int UserCountR { get; set; }

        // Way 2:
        [DisplayName("Users")]
        public List<UserResponse> UsersR { get; set; }
    }
}
