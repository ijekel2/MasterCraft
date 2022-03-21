using Microsoft.AspNetCore.Mvc;

namespace MasterCraft.Server.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class ApiBaseController : Controller
    {
    }
}
