using VM.Models;
using VM.Storage.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VM.Environment;

namespace VM.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
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
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        Department department = _context.Departments.First(department => department.Id == id);
        return View(department);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmation(Department department)
    {
        _context.Departments.Remove(department);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Department> departments = _context.Departments.OrderBy(department => department.Name);
        return View(departments);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        Department department = _context.Departments.First(department => department.Id == id);
        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Department department)
    {
        if (ModelState.IsValid)
        {
            _context.Update(department);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(department);
    }
}
