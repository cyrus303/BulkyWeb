using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController:Controller
{
    private readonly ApplicationDbContext _dbContext;
    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext= dbContext;
    }
    public IActionResult Index()
    {
        var objCategoryList = _dbContext.Categories.ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            TempData["success"]= "Category created successfully";
            return RedirectToAction("Index","Category");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDb = _dbContext.Categories.Find(id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(obj);
            _dbContext.SaveChanges();
            TempData["success"]= "Category updated successfully";
            return RedirectToAction("Index","Category");
        }
        return View();
    }
    public IActionResult Delete(int id)
    {
        var objCategory = _dbContext.Categories.Find(id);
        _dbContext.Categories.Remove(objCategory);
        _dbContext.SaveChanges();
        TempData["success"]= "Category deleted successfully";
        return RedirectToAction("Index","Category");
    }
}