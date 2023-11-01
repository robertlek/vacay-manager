using Microsoft.AspNetCore.Mvc;

namespace VM.Library;

public class BaseController : Controller
{
    protected void SendErrorMessage(string message)
    {
        TempData["error"] = message;
    }

    protected void SendInfoMessage(string message)
    {
        TempData["info"] = message;
    }

    protected void SendSuccessMessage(string message)
    {
        TempData["success"] = message;
    }

    protected void SendWarningMessage(string message)
    {
        TempData["warning"] = message;
    }
}
