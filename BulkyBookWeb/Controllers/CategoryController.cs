using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> allCategory = _dbContext.Categories;

        return View(allCategory);
    }

    // GET
    public IActionResult Create()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); 
        }

        return View(category);
    }

    // GET
    public IActionResult Edit(int? id)
    {
        if (id is null || id < 1)
        {
            return NotFound();
        }

        var getCategoryReponse = _dbContext.Categories.Find(id);

        if (getCategoryReponse is null)
        {
            return NotFound();
        }

        return View(getCategoryReponse);
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category category)
    {
        if (category.Name == category.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }

        if (ModelState.IsValid)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();

            return RedirectToAction("Index"); 
        }

        return View(category);
    }
}
