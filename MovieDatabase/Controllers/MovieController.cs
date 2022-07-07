using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Data;
using MovieDatabase.Models;
using System.IO;


namespace MovieDatabase.Controllers
{
    public class MovieController : Controller
    {
        private readonly RepositoryDbContext _db;
        private readonly IWebHostEnvironment _iwebhost;

        public MovieController(RepositoryDbContext db, IWebHostEnvironment iwebhost)
        {
            _db = db;
            _iwebhost = iwebhost;
        }
        public IActionResult Index()
        {
            IEnumerable<Movie> movies = _db.Movies;
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
                await _db.Movies.AddAsync(movie);
                await _db.SaveChangesAsync();
            }
            

            return RedirectToAction("Index");
        }

        //Get-Create
        
        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Movies.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie, int? id)
        {
            
            //if (ModelState.IsValid)
            //{
                var obj = _db.Movies.Find(id);
                if (id == null || id == 0)
                {
                    return NotFound();
                }else if (obj == null)
                {
                    return NotFound();
                }
                else
                {
                    _db.Movies.Update(movie);
                    //_db.Movies.Update(movie);
                    _db.SaveChanges();
                }
                
           // }


            return RedirectToAction("Index");
        }


    }
}
