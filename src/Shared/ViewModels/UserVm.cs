using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class UserVm
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Username { get; set; }

        [MaxLength(256)]
        public string ProfileImageUrl { get; set; }
    }
}
