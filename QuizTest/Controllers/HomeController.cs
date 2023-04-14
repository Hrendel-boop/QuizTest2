using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuizTest.Models;
using QuizTest.Models.ViewModels;
using System.Diagnostics;
using System.Text.Json;

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

        public IActionResult Details(int id)
        {
            var obj = _db.Test.Find(id);

            var homeVM = new HomeVM
            {
                Question = JsonSerializer.Deserialize<Question>(obj.QuestionsSerialized),
            };

            return View(homeVM);
        }

        public IActionResult DetailsPost(HomeVM homeVM)
        {
            homeVM.QuestionCount++;

            if (homeVM.QuestionCount == homeVM.Question.Name.Length)
            {
                return RedirectToAction("Index");
            }

            return View("Details", homeVM);
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