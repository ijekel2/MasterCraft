using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class ApplicationUserVm
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(64)]
        public string Username { get; set; } = string.Empty;
    }
}
