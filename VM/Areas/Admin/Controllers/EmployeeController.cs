using VM.Models;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VM.Storage.Repository.IRepository;

namespace VM.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class EmployeeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult GetAllEmployees()
    {
        try
        {
            IEnumerable<Employee> employees = _unitOfWork.Employee.GetAll(properties: "Department");
            return Json(new { success = true, data = employees });
        }
        catch
        {
            return Json(new { success = false });
        }
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
