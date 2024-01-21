using VM.Library;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace VM.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = Roles.Admin)]
public class StatisticsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;

    public StatisticsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("api/get-vacations-data")]
    public JsonResult GetVacationsData()
    {
        List<object> data = new();

        for (int i = 1; i <= 12; i++)
        {
            object dataForMonth = new
            {
                Month = new DateTime(2024, i, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("en")),
                Count = _unitOfWork.Vacation.GetVacationsCountByMonth(i)
            };

            data.Add(dataForMonth);
        }

        return Json(data);
    }
}
