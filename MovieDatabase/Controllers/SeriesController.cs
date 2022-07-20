using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Data;
using MovieDatabase.Models;
using MovieDatabase.Repository.IRepository;
using System.IO;


namespace MovieDatabase.Controllers
{
    public class SeriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _iwebhost;

        public SeriesController(IUnitOfWork db, IWebHostEnvironment iwebhost)
        {
            _unitOfWork = db;
            _iwebhost = iwebhost;
        }
        public IActionResult Index()
        {
            IEnumerable<Series> series = _unitOfWork.Series.GetAll();
            return View(series);
        }

        //Get-Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Description,
                    Value = u.Id.ToString()
                });
            ViewBag.CategoryList = CategoryList;
            return View();
        }

        //Post-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Series series)
        {
            
                string imgext = Path.GetExtension(series.Image.FileName);
                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG" || imgext == ".jpeg" || imgext == ".JPEG")
                {
                    var saveImg = Path.Combine(_iwebhost.WebRootPath, "Images", series.Image.FileName);
                    var stream = new FileStream(saveImg, FileMode.Create);
                    await series.Image.CopyToAsync(stream);

                    series.ImgName = series.Image.FileName;
                    series.ImgPath = saveImg;
                }
                _unitOfWork.Series.AddAsync(series);
                _unitOfWork.Save();
                TempData["success"] = "Serie created sucefully";

            return RedirectToAction("Index");
        }

        //Get-Create
        public IActionResult Edit(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Description,
                    Value = u.Id.ToString()
                });
            ViewBag.CategoryList = CategoryList;
            if (id== null || id == 0)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }
            var seriesFromDB = _unitOfWork.Series.Find(id);
            if(seriesFromDB == null)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }
            return View(seriesFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Series series, int? id)
        {
            
            var obj = _unitOfWork.Series.Find(id);
            if (id == null || id == 0)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            } else if (obj == null)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }
            else if (series.Image == null)
            {
                series.ImgName = obj.ImgName;
                series.ImgPath = obj.ImgPath;
                _unitOfWork.Series.Clear();
                _unitOfWork.Series.Update(series);
                _unitOfWork.Save();
                TempData["success"] = "Serie updated sucefully";
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
                string imgext = Path.GetExtension(series.Image.FileName);
                if (imgext == ".jpg" || imgext == ".png" || imgext == ".JPG" || imgext == ".PNG" || imgext == ".jpeg" || imgext == ".JPEG")
                {
                    var saveImg = Path.Combine(_iwebhost.WebRootPath, "Images", series.Image.FileName);
                    var stream = new FileStream(saveImg, FileMode.Create);
                    await series.Image.CopyToAsync(stream);

                    series.ImgName = series.Image.FileName;
                    series.ImgPath = saveImg;
                    series.ModifiedDate = DateTime.Now.Date;
                }
                _unitOfWork.Series.Clear();
                _unitOfWork.Series.Update(series);
                _unitOfWork.Save();
                TempData["success"] = "Serie updated sucefully";
            }          
            return RedirectToAction("Index");
        }
        
        
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }
            var serieFromDB = _unitOfWork.Series.Find(id);
            if (serieFromDB == null)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }
            return View(serieFromDB);
        }
        

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            var seriesFromDB = _unitOfWork.Series.Find(id);
            if (seriesFromDB == null)
            {
                TempData["error"] = "Serie not found!";
                return NotFound();
            }

            _unitOfWork.Series.Remove(seriesFromDB);
            _unitOfWork.Save();
            TempData["success"] = "Serie deleted sucefully";
            return RedirectToAction("Index");
        }
    }
}
 