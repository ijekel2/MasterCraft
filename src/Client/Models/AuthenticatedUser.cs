using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Models
{
    public class AuthenticatedUser
    {
        public string AccessToken { get; set; }

        public string UserName { get; set; }
    }
}
