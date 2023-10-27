﻿using VM.Models;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

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
        return View(new Department());
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

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null)
        {
            return Json(new { success = false });
        }

        var department = _unitOfWork.Department.Get(department => department.Id == id);

        if (department is null)
        {
            return Json(new { success = false });
        }

        _unitOfWork.Department.Remove(department);
        _unitOfWork.Save();

        return Json(new { success = true });
    }

    [HttpGet]
    public IActionResult Index()
    {
        var departments = _unitOfWork.Department.GetAll().OrderBy(department => department.Name);
        return View(departments);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        if (id == 0 || id == null)
        {
            return RedirectToAction("Index");
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
