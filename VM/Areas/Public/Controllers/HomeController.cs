using VM.Library;
using Microsoft.AspNetCore.Mvc;

namespace VM.Areas.Public.Controllers;

[Area("Public")]
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
