using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Core.CommandModels
{
    public class RegisterUserCommandModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), 
            ErrorMessageResourceName = nameof(Properties.Resources.PasswordsDoNotMatch), 
            ErrorMessageResourceType = typeof(Properties.Resources))]
        public string ConfirmPassword { get; set; }
    }
}
