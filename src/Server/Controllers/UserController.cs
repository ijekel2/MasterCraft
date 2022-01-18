using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    public class UserController : ApiBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
