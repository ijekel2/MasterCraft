using Microsoft.AspNetCore.Mvc;

namespace MasterCraft.Server.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ApiBaseController : Controller
    {
        protected string GetUrl()
        {
            return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";
        }

        public CreatedResult Created(int id)
        {
            return Created($"{GetUrl()}/{id}", null);
        }
    }
}
