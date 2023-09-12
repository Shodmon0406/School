using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class BaseController : Controller
    {
        [NonAction]
        protected List<string> ModelStateErors() => ModelState.SelectMany(e => e.Value.Errors.Select(er => er.ErrorMessage)).ToList();
    }
}
