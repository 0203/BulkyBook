using BulkyBookWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {

            _db = db;

        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET --> Create Category
        public IActionResult Create()
        {
            return View();
        }

        //POST --> Create Category

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "The Display Order cannot exactly similar with the Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }

        //GET --> Edit Category
        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

                //Find will return details based on the PRIMARY KEY of the table
                var categoryFromDb = _db.Categories.Find(id);

                //FirstOrDefault will not throw an exception if more than 1 element. It will return 1 element of the list 
                //var categoryFromDbFirstD = _db.Categories.FirstOrDefault(u => u.Id == id);

                //SingleOrDefault will just return empty if no element were found on that id. It will throw exception if more than 1 element  
                //var categoryFromDbSingleD = _db.Categories.SingleOrDefault(u => u.Id == id);

                //Single will throw an exception if no element were found on that id  
                //var categoryFromDbSingle = _db.Categories.Single(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound(); 
            }
          
            return View(categoryFromDb);
        }

        //POST --> Edit Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly similar with the Name");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }


        //GET --> Delete Category
        public IActionResult Remove(int id)
        {
            var obj = _db.Categories.Find(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        //POST --> Delete Category

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }

            return View(obj);

        }

    }
}
