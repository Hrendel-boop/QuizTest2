using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using QuizTest.Models;
using QuizTest.Models.ViewModels;

namespace QuizTest.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TestController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        // GET: FilmsController
        public ActionResult Index()
        {
            IEnumerable<Test> objList = _db.Test;
            return View(objList);
        }

        // GET: FilmsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilmsController/Create
        public ActionResult Create()
        {

            TestVM testVM = new TestVM
            {
                Test = new Test(),
                
            };
            Question question = new Question();

            testVM.queCount= 1;
            
            question.Name = new string[testVM.queCount];
            question.CorrectAnswer = new string[testVM.queCount];
            question.IncorrectAnswer = new string[testVM.queCount];

            testVM.Question = question;

            return View(testVM);
        }


        // POST: FilmsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(TestVM testVM)
        {
            
            if (testVM.Question != null)
            {
                //testVM.Test.Name = testVM.queCount.ToString();
                testVM.Test.QuestionsSerialized = JsonSerializer.Serialize(testVM.Question);
                _db.Test.Add(testVM.Test);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string upload = webRootPath + Constants.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }


            testVM.Test.Image = fileName + extension;

            return View("QueAdd", testVM);
        }



        // GET: FilmsController/Edit/5
        public ActionResult Edit(int id)
        {
            TestVM testVM = new TestVM();

            testVM.Test = _db.Test.Find(id);
            testVM.Question = JsonSerializer.Deserialize<Question>(testVM.Test.QuestionsSerialized);
            testVM.queCount = testVM.Question.Name.Length;
            
            return View(testVM);
        }

        // POST: FilmsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TestVM testVM)
        {
            if (true)
            {

            }
            return View("QueEdit", testVM);
        }

        // GET: FilmsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilmsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
