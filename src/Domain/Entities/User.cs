using Microsoft.AspNetCore.Identity;

namespace MasterCraft.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }

    }
}
