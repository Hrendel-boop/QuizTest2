using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuizTest.Models;
using System.Diagnostics;

namespace QuizTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment= webHostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Test> objList = _db.Test;
            return View(objList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}