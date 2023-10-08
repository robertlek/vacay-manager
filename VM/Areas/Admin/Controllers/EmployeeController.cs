using VM.Models;
using VM.ViewModels;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VM.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class EmployeeController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeController(
        RoleManager<IdentityRole> roleManager,
        UserManager<IdentityUser> userManager,
        IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public IActionResult Add()
    {
        EmployeeUpsertViewModel model = new()
        {
            Departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name)
                .Select(department => new SelectListItem
                {
                    Text = department.Name,
                    Value = department.Id.ToString()
                }),
            Employee = new Employee(),
            Roles = _roleManager.Roles.OrderBy(role => role.Name).Select(role => role.Name)
                .Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role
                }),
            User = new User()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(EmployeeUpsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.User.Email);

        if (user is not null)
        {
            return View(model);
        }

        _userManager.CreateAsync(new Employee
        {
            UserName = model.User.Email,
            Email = model.User.Email,
            PhoneNumber = model.User.PhoneNumber,
            DepartmentId = model.Employee.DepartmentId,
            EmployedOn = DateTime.Now,
            FirstName = model.Employee.FirstName,
            LastName = model.Employee.LastName
        }, model.User.Password).GetAwaiter().GetResult();

        user = _unitOfWork.Employee.Get(user => user.UserName.Equals(model.User.Email));
        _userManager.AddToRoleAsync(user, model.Role).GetAwaiter().GetResult();

        return RedirectToAction("Index", "Employee");
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
