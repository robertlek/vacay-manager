using VM.Models;
using VM.Library;
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
public class EmployeeController : BaseController
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

    [HttpDelete]
    public async Task<IActionResult> Delete(string? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Employee? employee = _unitOfWork.Employee.GetFirstOrDefault(employee => employee.Id == id);

        if (employee is null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByEmailAsync(employee.Email);

        if (user is not null)
        {
            string role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().First();

            await _userManager.RemoveFromRoleAsync(user, role);
            await _userManager.DeleteAsync(user);
        }

        return Json(new { success = true });
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

    [HttpGet]
    public async Task<IActionResult> Update(string? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Employee? employee = _unitOfWork.Employee.GetFirstOrDefault(employee => employee.Id == id);

        if (employee is null)
        {
            return NotFound();
        }

        IdentityUser user = await _userManager.FindByEmailAsync(employee.Email);

        string? role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

        if (role is null)
        {
            await _userManager.AddToRoleAsync(user, Roles.Employee);
            role = _userManager.GetRolesAsync(user).Result.First();
        }

        EmployeeUpsertViewModel model = new()
        {
            Departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name)
                .Select(department => new SelectListItem
                {
                    Text = department.Name,
                    Value = department.Id.ToString()
                }),
            Employee = employee,
            Role = role,
            Roles = _roleManager.Roles.OrderBy(role => role.Name).Select(role => role.Name)
                .Select(role => new SelectListItem
                {
                    Text = role,
                    Value = role
                }),
            User = new User
            {
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Password = employee.PasswordHash,
                ConfirmPassword = employee.PasswordHash
            }
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(EmployeeUpsertViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.FindByEmailAsync(model.User.Email);

        if (user is not null)
        {
            string role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().First();

            await _userManager.RemoveFromRoleAsync(user, role);
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        Employee? employee = _unitOfWork.Employee.GetFirstOrDefault(employee => employee.Id == model.Employee.Id);

        if (employee is not null)
        {
            employee.FirstName = model.Employee.FirstName;
            employee.LastName = model.Employee.LastName;
            employee.PhoneNumber = model.User.PhoneNumber;
            employee.DepartmentId = model.Employee.DepartmentId;

            _unitOfWork.Employee.Update(employee);
            _unitOfWork.Save();
        }

        return RedirectToAction("Index", "Employee");
    }
}
