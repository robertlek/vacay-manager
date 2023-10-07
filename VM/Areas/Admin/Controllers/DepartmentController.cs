using VM.Models;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VM.Storage.Repository.IRepository;

namespace VM.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class DepartmentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public DepartmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
            _unitOfWork.Department.Add(department);
            _unitOfWork.Save();

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

        var department = _unitOfWork.Department.Get(department => department.Id == id);
        return View(department);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmation(Department department)
    {
        _unitOfWork.Department.Remove(department);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<Department> departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name);
        return View(departments);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        if (id == 0 || id == null)
        {
            return NotFound();
        }

        var department = _unitOfWork.Department.Get(department => department.Id == id);
        return View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Department department)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Department.Update(department);
            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        return View(department);
    }
}
