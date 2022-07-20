using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Models.ViewModels;
using MovieDatabase.Repository.IRepository;
using System.IO;


namespace MovieDatabase.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _iwebhost;

        public ProductController(IUnitOfWork db, IWebHostEnvironment iwebhost)
        {
            _unitOfWork = db;
            _iwebhost = iwebhost;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Description,
                    Value = i.Id.ToString()
                }),

            };
            if(id== null || id == 0)
            {
                //create product
                //ViewBag.CategoryList = CategoryList;
                return View(productVM);
            }
            else
            {
                //update product
            }
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM obj, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _iwebhost.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath,@"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\Images\Products\" + filename + extension;
                }
                _unitOfWork.Product.Add(obj.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product added sucefully";
                return RedirectToAction("Index");
            }

            return View(obj);
            
        }
        
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }
            var movieFromDb = _unitOfWork.Movie.Find(id);
            if (movieFromDb == null)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }
            return View(movieFromDb);
        }
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var movieFromDb = _unitOfWork.Movie.Find(id);
            if (movieFromDb == null)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }

            _unitOfWork.Movie.Remove(movieFromDb);
            _unitOfWork.Save();
            TempData["success"] = "Movie deleted sucefully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return Json(new { data = productList });
        }
        #endregion

    }
}
 