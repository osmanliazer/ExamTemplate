using Microsoft.AspNetCore.Mvc;

namespace IndigoExam.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
