using Microsoft.AspNetCore.Mvc;
using PieShop.DataAccess.Repository.IRepository;
using PieShop.Models;
using System.Drawing;

namespace PieShopProject.Controllers
{
    public class PiesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public PiesController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var allPies = _unitOfWork.Pie.GetAll();
            return View(allPies);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        //i have done some changes in below parameters
        public IActionResult Create(Pie pieObj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = file.FileName;
                    var uploads = Path.Combine(wwwRootPath, @"images\pies");
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                         file.CopyToAsync(fileStream);
                    }
                    pieObj.ImageUrl = @"\images\pies\" + fileName;
                    
                }

                _unitOfWork.Pie.Add(pieObj);
                _unitOfWork.Save();
                TempData["Success"] = "Pie added successfully";
                return RedirectToAction("Index");
            }
            return View(pieObj);
        }
        public IActionResult Details(int pieId)
        {
            var pieDetails = _unitOfWork.Pie.GetFirstOrDefault(c => c.PieId == pieId);
            return View(pieDetails);
        }
       
    }
}
