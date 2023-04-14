using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using QuizTest.Models;
using QuizTest.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.DotNet.Scaffolding.Shared;

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
        public ActionResult Edit(TestVM testVM, string viewName)
        {
            if (viewName=="QueEdit")
            {
                testVM.Test.QuestionsSerialized = JsonSerializer.Serialize(testVM.Question);
                _db.Test.Update(testVM.Test);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;

                var objFromDb = _db.Test.AsNoTracking().FirstOrDefault(u => u.Id == testVM.Test.Id);
                string upload = webRootPath + Constants.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                var oldFile = Path.Combine(upload, objFromDb.Image);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                testVM.Test.Image = fileName + extension;
            }
            return View("QueEdit", testVM);
        }

        // GET: FilmsController/Delete/5
        public ActionResult Delete(int id)
        {
            var obj = _db.Test.Find(id);
            return View(obj);
        }

        // POST: FilmsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var obj = _db.Test.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            string upload = _webHostEnvironment.WebRootPath + Constants.ImagePath;
            var oldFile = Path.Combine(upload, obj.Image);

            if (System.IO.File.Exists(oldFile))
            {
                System.IO.File.Delete(oldFile);
            }

            _db.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));          
        }
    }
}
