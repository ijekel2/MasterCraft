using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Server.Controllers
{
    [Route("/api/[controller]")]
    public class ApiBaseController : Controller
    {
        private ISender cMediator = null!;

        protected ISender Mediator => cMediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
