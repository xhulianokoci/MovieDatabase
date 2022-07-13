using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.IO;


namespace MovieDatabase.Controllers
{
    public class MovieController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _iwebhost;

        public MovieController(IUnitOfWork db, IWebHostEnvironment iwebhost)
        {
            _unitOfWork = db;
            _iwebhost = iwebhost;
        }
        public IActionResult Index()
        {
            IEnumerable<Movie> movies = _unitOfWork.Movie.GetAll();
            return View(movies);
        }

        //Get-Create
        public IActionResult Create()
        {
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                string imgext = Path.GetExtension(movie.Image.FileName);
                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG" || imgext == ".jpeg" || imgext == ".JPEG")
                {
                    var saveImg = Path.Combine(_iwebhost.WebRootPath, "Images", movie.Image.FileName);
                    var stream = new FileStream(saveImg, FileMode.Create);
                    await movie.Image.CopyToAsync(stream);

                    movie.ImgName = movie.Image.FileName;
                    movie.ImgPath = saveImg;


                }
                _unitOfWork.Movie.AddAsync(movie);
                _unitOfWork.Save();
                TempData["success"] = "Movie created sucefully";
            }
            else
            {
                TempData["error"] = "Movie was not added to database!";
            }
            

            return RedirectToAction("Index");
        }

        //Get-Create
        
        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }
            var movieFromDb = _unitOfWork.Movie.Find(id);
            if(movieFromDb == null)
            {
                TempData["error"] = "Movie not found!";
                return NotFound();
            }
            return View(movieFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie, int? id)
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
 