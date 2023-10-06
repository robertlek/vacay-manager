using VM.Storage.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace VM.Areas.Public.Controllers;

[Area("Public")]
public class VacationController : Controller
{
    private readonly Context _context;

    public VacationController(Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var departments = _context.Departments;
        return View(departments);
    }
}
