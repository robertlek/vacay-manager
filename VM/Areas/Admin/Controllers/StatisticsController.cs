using VM.Library;
using VM.Environment;
using Microsoft.AspNetCore.Mvc;
using VM.Storage.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

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
}
