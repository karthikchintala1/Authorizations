using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationsTest.Areas.Home
{
    [Route("home")]
    //[Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "SuperUser")]
        [Route("time")]
        public IActionResult Time() => Content(new TimeOnly().ToLongTimeString());
    }
}
