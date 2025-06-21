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
        // if (obj.Name == obj.DisplayOrder.ToString())
        // {
        //     ModelState.AddModelError("name", "Name cannot be same as the Display Order");
        // }
        //
        // if (obj.Name.ToLower() == "test")
        // {
        //     ModelState.AddModelError("", "Test is an invalid value");
        // }
        // {
        //     ModelState.AddModelError("name", "Name cannot be same as the Display Order");
        // }
        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index","Category");
        }
        return View();
    }

    public IActionResult Edit(int id)
    {
        var objCategory = _dbContext.Categories.Find(id);
        return View(objCategory);
    }

    public IActionResult Delete(int id)
    {
        var objCategory = _dbContext.Categories.Find(id);
        _dbContext.Categories.Remove(objCategory);
        _dbContext.SaveChanges();
        return RedirectToAction("Index","Category");
    }
}