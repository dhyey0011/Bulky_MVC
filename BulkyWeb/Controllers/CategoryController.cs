
using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder Cannot Match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Sucessfully";
                return RedirectToAction("Index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=> u.CategoryId == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=> u.CategoryId == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj,int? id)
        {
            if (ModelState.IsValid)
            {
                // Find the existing category from the database
                Category? existingCategory = _db.Categories.Find(id);

                if (existingCategory != null)
                {
                    // Update the existing category's properties
                    existingCategory.Name = obj.Name;
                    existingCategory.DisplayOrder = obj.DisplayOrder;
                    _db.Categories.Update(existingCategory);
                    _db.SaveChanges();
                    TempData["success"] = "Category Updated Sucessfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=> u.CategoryId == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=> u.CategoryId == id).FirstOrDefault();
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            if (ModelState.IsValid)
            {
                // Find the existing category from the database
                Category? existingCategory = _db.Categories.Find(id);

                if (existingCategory != null)
                {
                    // Update the existing category's properties

                    _db.Categories.Remove(existingCategory);
                    _db.SaveChanges();
                    TempData["success"] = "Category deleted Sucessfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }
    }
}
 