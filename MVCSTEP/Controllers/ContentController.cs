using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

[Authorize(Roles = "Admin,Editor")]
public class ContentController : Controller
{
    private readonly ICategory _categories;
    private readonly IPublication _publications;
    private readonly IWebHostEnvironment _appEnvironment;
 
    public ContentController(ICategory categories, IPublication publications, IWebHostEnvironment appEnvironment)
    {
        _categories = categories;
        _publications = publications;
        _appEnvironment = appEnvironment;
    }
 
    [Route("content")]
    public async Task<IActionResult> Index()
    {
        return View(new ContentViewModel
        {
            Categories = await _categories.GetAllCategoriesAsync(),
            Publications = await _publications.GetAllPublicationsWithCategoriesAsync()
        });
    }
 
    #region Categories
    [Route("create-category")]
    public IActionResult CreateCategory()
    {
        return View();
    }
 
    [Route("create-category")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> CreateCategory(CategoryViewModel categoryViewModel)
    {
        if (ModelState.IsValid)
        {
            await _categories.AddCategoryAsync(new Category
            {
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            });
            return RedirectToAction(nameof(Index));
        }
        return View(categoryViewModel);
    }
 
    [Route("edit-category")]
    public async Task<IActionResult> EditCategory(string id)
    {
        var currentCategory = await _categories.GetCategoryAsync(id);
        if (currentCategory is not null)
        {
            return View(new CategoryViewModel
            {
                Id = currentCategory.Id,
                Name = currentCategory.Name,
                Description = currentCategory.Description
            });
        }
        return NotFound();
    }
 
    [Route("edit-category")]
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> EditCategory(CategoryViewModel categoryViewModel)
    {
        if (ModelState.IsValid)
        {
            await _categories.UpdateCategoryAsync(new Category
            {
                Id = categoryViewModel.Id.Value,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            });
            return RedirectToAction(nameof(Index));
        }
        return View(categoryViewModel);
    }
 
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> DeleteCategory(string id)
    {
        var currentCategory = await _categories.GetCategoryAsync(id);
        if (currentCategory != null)
        {
            await _categories.DeleteCategoryAsync(currentCategory);
        }
        return RedirectToAction(nameof(Index));
    }
    #endregion
}