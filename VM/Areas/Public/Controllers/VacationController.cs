﻿using VM.Models;
using VM.Library;
using VM.ViewModels;
using VM.Environment;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace VM.Areas.Public.Controllers;

[Area("Public")]
[Authorize(Roles = Roles.Admin + "," + Roles.Employee)]
public class VacationController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public VacationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpDelete]
    public JsonResult CancelVacation(int? id)
    {
        if (id == 0 || id is null)
        {
            return Json(new { success = false, message = "This vacation is invalid." });
        }

        Vacation vacation = _unitOfWork.Vacation.Get(vacation => vacation.Id == id);
        _unitOfWork.Vacation.Remove(vacation);
        _unitOfWork.Save();

        return Json(new { success = true });
    }

    [HttpGet]
    public IActionResult Employees(int? id)
    {
        if (id == 0 || id is null)
        {
            SendErrorMessage("The department is invalid.");
            return RedirectToAction("Index");
        }

        Department? department = _unitOfWork.Department.GetFirstOrDefault(department =>
            department.Id == id);

        if (department is null)
        {
            SendErrorMessage("The department is invalid.");
            return RedirectToAction("Index");
        }

        return View(department);
    }

    [HttpGet]
    public IActionResult GetAllEmployees(int departmentId)
    {
        try
        {
            IEnumerable<Employee> employees = _unitOfWork.Employee.GetAll().Where(employee =>
                employee.DepartmentId == departmentId);
            return Json(new { success = true, data = employees });
        }
        catch
        {
            return Json(new { success = false });
        }
    }

    [HttpGet]
    public IActionResult GetAllVacations(int departmentId)
    {
        try
        {
            var vacations = _unitOfWork.Vacation.GetAll(vacation =>
                vacation.DepartmentId == departmentId, properties: "Employee");

            return Json(new { success = true, data = vacations });
        }
        catch
        {
            return Json(new { success = false });
        }
    }

    [HttpGet]
    public IActionResult Index()
    {
        VacationIndexViewModel model = new()
        {
            Departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name),
            Employees = _unitOfWork.Employee.GetAll(),
            Vacations = _unitOfWork.Vacation.GetAll(vacation => vacation.ToDate >= DateTime.Now)
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult List(int? id)
    {
        if (id == 0 || id is null)
        {
            SendErrorMessage("The department is invalid.");
            return RedirectToAction("Index");
        }

        var department = _unitOfWork.Department.GetFirstOrDefault(department => department.Id == id);

        if (department is null)
        {
            SendErrorMessage("The department is invalid.");
            return RedirectToAction("Index");
        }

        return View(department);
    }

    [HttpGet]
    public IActionResult MyVacation(int? id)
    {
        if (id is null || id == 0)
        {
            SendErrorMessage("This vacation is invalid.");
            return RedirectToAction("Index");
        }

        Vacation vacation = _unitOfWork.Vacation.Get(vacation => vacation.Id == id,
            properties: "Employee,Department");

        return View(vacation);
    }

    [HttpGet]
    public IActionResult New()
    {
        Employee? employee = null;

        if (User.Identity != null)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                employee = _unitOfWork.Employee.GetFirstOrDefault(employee => employee.Id == claim.Value);
            }
        }

        if (employee == null)
        {
            SendErrorMessage("The employee is invalid.");
            return RedirectToAction("Index");
        }

        if (_unitOfWork.Employee.HasActiveVacation(employee))
        {
            SendWarningMessage("You already have an active vacation.");
            return RedirectToAction("Index");
        }

        Department department = _unitOfWork.Department.Get(department => department.Id == employee.DepartmentId);

        if (_unitOfWork.Department.GetActiveVacationsCount(department.Id) >= department.Overlaps)
        {
            SendErrorMessage("The department's vacations overlaps limit has been reached.");
            return RedirectToAction("Index");
        }

        VacationInsertViewModel model = new()
        {
            Department = department,
            Employee = employee,
            Vacation = new()
        };

        model.Vacation.FromDate = DateTime.Now;
        model.Vacation.ToDate = DateTime.Now;

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult New(VacationInsertViewModel model)
    {
        model.Department = _unitOfWork.Department.Get(department => department.Id == model.Department.Id);
        model.Employee = _unitOfWork.Employee.Get(employee => employee.Id == model.Employee.Id);

        if (_unitOfWork.Employee.HasActiveVacation(model.Employee))
        {
            SendWarningMessage("You already have an active vacation.");
            return RedirectToAction("Index");
        }

        Vacation vacation = new()
        {
            EmployeeId = model.Employee.Id,
            DepartmentId = model.Department.Id,
            FromDate = model.Vacation.FromDate,
            ToDate = model.Vacation.ToDate,
            CreatedOn = DateTime.Now
        };

        model.Vacation = vacation;

        if (vacation.FromDate >= vacation.ToDate)
        {
            SendErrorMessage("The vacation dates are invalid.");
            return View(model);
        }

        _unitOfWork.Vacation.Add(vacation);
        _unitOfWork.Save();

        SendSuccessMessage("Your vacation has been submitted.");
        return RedirectToAction("Index");
    }
}
