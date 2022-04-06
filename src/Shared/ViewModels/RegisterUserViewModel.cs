using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Shared.ViewModels
{
    public class RegisterUserViewModel
    {
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
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        [Compare(nameof(Password), 
            ErrorMessageResourceName = nameof(Properties.Resources.PasswordsDoNotMatch), 
            ErrorMessageResourceType = typeof(Properties.Resources))]
        public string ConfirmPassword { get; set; }
    }
}
