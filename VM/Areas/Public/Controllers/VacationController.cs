﻿using VM.Models;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using VM.ViewModels;

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

    [HttpGet]
    public IActionResult Employees(int? id)
    {
        if (id == 0 || id is null)
        {
            return NotFound();
        }

        Department? department = _unitOfWork.Department.GetFirstOrDefault(department =>
            department.Id == id);

        if (department is null)
        {
            return NotFound();
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
    public IActionResult Index()
    {
        VacationIndexViewModel model = new()
        {
            Departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name),
            Employees = _unitOfWork.Employee.GetAll()
        };

        return View(model);
    }
}
