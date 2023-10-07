using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VM.Storage.Repository.IRepository;

namespace VM.Areas.Public.Controllers;

[Area("Public")]
[Authorize(Roles = Roles.Admin + "," + Roles.Employee)]
public class VacationController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public VacationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name);
        return View(departments);
    }
}
