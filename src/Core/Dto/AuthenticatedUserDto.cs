using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Core.Dto
{
    public class AuthenticatedUserDto
    {
        public string AccessToken { get; set; }

        public string Username { get; set; }
    }
}
