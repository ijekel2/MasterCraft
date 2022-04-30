using Microsoft.AspNetCore.Identity;

namespace MasterCraft.Domain.Entities
{
    public class ExtendedIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
