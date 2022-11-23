using ePets.Models;
using ePets.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IO;




namespace ePets.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IAnimalService _DataBase;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _HostDataBase;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public CatalogController(IAnimalService context, Microsoft.AspNetCore.Hosting.IHostingEnvironment HostingContext)
        {
            _DataBase = context;
            _HostDataBase = HostingContext;
        }
        public IActionResult Index(int AnimalId = 1)
        {

            ViewBag.CurrentCategory = _DataBase.GetAnimalType(AnimalId);
            ViewBag.Categories = _DataBase.GetTypes();


            return View(_DataBase.GetAnimalsByType(AnimalId));
        }

        [HttpGet]
        public IActionResult ShowAnimal(int id)
        {
            return View(_DataBase.GetSpecificAnimal(id));
        }

        [HttpPost]
        public IActionResult ShowAnimal(int animalId, string CommentMessage)
        {

            if (CommentMessage == null || CommentMessage == string.Empty)
            {
                TempData["Error"] = "Comment Is Empty. Please Try Again";
                return RedirectToAction("ShowAnimal", "Catalog", new { id = animalId });
            }
            else
            {
                //var Content = Request.Form["CommentMessage"];
                var animal = _DataBase.GetSpecificAnimal(animalId);


                animal.Comments.Add(new Comment() { Content=CommentMessage });
                _DataBase.SaveChanges();
                return RedirectToAction("ShowAnimal", "Catalog", new { id = animalId });
            }
        }

        [HttpGet]
        public IActionResult CreateAnimal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAnimal([Bind("Name ,Photo,AnimalTypeId,Age,Price,Bio ")] AnimalCreateNewModel animal)
        {
            if (!ModelState.IsValid)
            {

                return View(animal);
            }

            string UniqeFileName = null;
            if (animal.Photo != null)
            {
                string UploadFolder = Path.Combine(_HostDataBase.WebRootPath, "Images", "Animals");
                UniqeFileName = Guid.NewGuid().ToString()+"_"+animal.Photo.FileName;
                string FilePath = Path.Combine(UploadFolder, UniqeFileName);
                animal.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
            }

            Animal NewAnimal = new Animal
            {
                Name = animal.Name,
                Age = animal.Age,
                Price = animal.Price,
                Bio = animal.Bio,
                AnimalTypeId = animal.AnimalTypeId,
                ImageUrl ="~/Images/Animals/" +UniqeFileName

            };
            
            _DataBase.AddAnimal(NewAnimal);
            return RedirectToAction("Index", "Catalog");

        }

        public IActionResult Delete(int id)
        {
            var ChosenAnimal = _DataBase.GetSpecificAnimal(id);
            if (ChosenAnimal == null) return View("NotFound");
            _DataBase.DeleteAnimal(id);
            return RedirectToAction("Index", "Catalog");
        }

        public IActionResult DeleteComment(int animalId, int Commentid)
        {

            var anms = _DataBase.GetSpecificAnimal(animalId);
            var ChosenComment = anms.Comments.FirstOrDefault(c => c.CommentId == Commentid);
            //anms.Comments.Remove(ChosenComment);
            _DataBase.DeleteComment(ChosenComment);


            return RedirectToAction("ShowAnimal", "Catalog", new { id = animalId });
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var animal = _DataBase.GetSpecificAnimal(id);
            ViewBag.AnComments =  _DataBase.GetCommentsOfAnimal(id);

         

            var respone = new AnimalCreateNewModel()
            {
                AnimalId= animal.AnimalId,
                Name = animal.Name,
                Age = animal.Age,
                Price = animal.Price,
                Bio = animal.Bio,
                AnimalTypeId = animal.AnimalTypeId,
                
            };

            return View(respone);
        }
        [HttpPost]
        public IActionResult Edit(int id, AnimalCreateNewModel animal)
        {


            if (!ModelState.IsValid)
            { return View(animal); }


            string UniqeFileName = null;
            if (animal.Photo != null)
            {
                string UploadFolder = Path.Combine(_HostDataBase.WebRootPath, "Images", "Animals");
                UniqeFileName = Guid.NewGuid().ToString()+"_"+animal.Photo.FileName;
                string FilePath = Path.Combine(UploadFolder, UniqeFileName);
                animal.Photo.CopyTo(new FileStream(FilePath, FileMode.Create));
            }

            Animal NewAnimal = new Animal
            {
                AnimalId=id,
                Name = animal.Name,
                Age = animal.Age,
                Price = animal.Price,
                Bio = animal.Bio,
                AnimalTypeId = animal.AnimalTypeId,
                ImageUrl ="~/Images/Animals/" +UniqeFileName

            };

            _DataBase.UpdateAnimal(id, NewAnimal);
            return RedirectToAction("Index", "Catalog");

        }
    }
}
