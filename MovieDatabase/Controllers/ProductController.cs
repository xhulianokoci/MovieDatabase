using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Data;
using MovieDatabase.Models;
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
            IEnumerable<Product> products = _unitOfWork.Product.GetAll();
            return View(products);
        }

        //GET
        
        public IActionResult Upsert(int? id)
        {
            Product product = new();
            IEnumerable<Product> ProductList = _unitOfWork.Product.GetAll();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Description,
                    Value = u.Id.ToString()
                });
            if(id== null || id == 0)
            {
                //create product
                ViewBag.CategoryList = CategoryList;
                return View(product);
            }
            else
            {
                //update product
            }
            
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Movie movie, int? id)
        {
            
            var obj = _unitOfWork.Movie.Find(id);
            if (id == null || id == 0)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            } else if (obj == null)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }
            else if (movie.Image == null)
            {
                movie.ImgName = obj.ImgName;
                movie.ImgPath = obj.ImgPath;
                _unitOfWork.Movie.Clear();
                _unitOfWork.Movie.Update(movie);
                _unitOfWork.Save();
                TempData["success"] = "Movie updated sucefully";
            }
            else
            {
                //Deletes the old image stored localy
                if (System.IO.File.Exists(obj.ImgPath))
                {
                    System.IO.File.Delete(obj.ImgPath);
                    ViewBag.deleteSuccess = "true";
                }
                //Generating ImagPath and ImgName for new image
                string imgext = Path.GetExtension(movie.Image.FileName);
                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG" || imgext == ".jpeg" || imgext == ".JPEG")
                {
                    var saveImg = Path.Combine(_iwebhost.WebRootPath, "Images", movie.Image.FileName);
                    var stream = new FileStream(saveImg, FileMode.Create);
                    await movie.Image.CopyToAsync(stream);
                    
                    movie.ImgName = movie.Image.FileName;
                    movie.ImgPath = saveImg;
                }
                _unitOfWork.Movie.Clear();
                _unitOfWork.Movie.Update(movie);
                _unitOfWork.Save();
                TempData["success"] = "Movie updated sucefully";
            }          
            return RedirectToAction("Index");
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



    }
}
 