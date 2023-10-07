using VM.Environment;
using VM.Storage.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace VM.Areas.Public.Controllers;

[Area("Public")]
[Authorize(Roles = Roles.Admin + "," + Roles.Employee)]
public class VacationController : Controller
{
    private readonly Context _context;

    public VacationController(Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var departments = _context.Departments.OrderBy(department => department.Name);
        return View(departments);
    }
}
