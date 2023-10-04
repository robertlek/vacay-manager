using VM.Models;
using VM.Storage.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace VM.Controllers;

public class DepartmentController : Controller
{
    private readonly Context _context;

    public DepartmentController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Add()
    {
        Department department = new();
        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Department department)
    {
        if (ModelState.IsValid)
        {
            _context.Add(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(department);
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Department> departments = _context.Departments;
        return View(departments);
    }
}
