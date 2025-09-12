using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Models.Pages;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly ICategory _categories;
    private readonly IPublication _publications;
    private readonly IWebHostEnvironment _appEnvironment;

    public HomeController(ICategory categories, IPublication publications, IWebHostEnvironment appEnvironment)
    {
        _categories = categories;
        _publications = publications;
        _appEnvironment = appEnvironment;
    }

    public async Task<IActionResult> Index(QueryOptions? options, string? categoryId)
    {
        var allCategories = await _categories.GetAllCategoriesAsync();
        var allPublications = await _publications.GetAllPublicationsByCategoryWithCategories(options, categoryId);

        return View(new IndexViewModel
        {
            Categories = allCategories.ToList(),
            Publications = allPublications
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public async Task<IActionResult> GetPublication(string? id, string? returnUrl)
    {
        var currentPublication = await _publications.GetPublicationWithCategoriesAsync(id);
        if (currentPublication != null)
        {
            await _publications.UpdateViewsAsync(currentPublication.Id.ToString());
            return View(new GetPublicationViewModel
            {
                Publication = currentPublication,
                ReturnUrl = returnUrl
            });
        }
        return NotFound();
    }
}