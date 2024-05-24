using IndigoExam.DAL;
using Microsoft.AspNetCore.Mvc;

namespace IndigoExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly IndigoContext _ındigoContext;

        public HomeController(IndigoContext ındigoContext)
        {
            _ındigoContext = ındigoContext;
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}
