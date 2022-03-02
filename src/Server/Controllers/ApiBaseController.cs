using MasterCraft.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mediator = MasterCraft.Application.Common.Utilities.Mediator;

namespace MasterCraft.Server.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class ApiBaseController : Controller
    {
        private ISender cMediator = null!;
        private Mediator cMyMediator = null!;

        protected ISender Mediator => cMediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        protected Mediator MyMediator => cMyMediator ??= HttpContext.RequestServices.GetRequiredService<Mediator>();
    }
}
