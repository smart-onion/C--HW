using AspIdentityHW1.Data;
using AspIdentityHW1.Models;
using AspIdentityHW1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspIdentityHW1.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationContext _applicationContext;

        public BlogController(UserManager<IdentityUser> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        [HttpGet]
        public  IActionResult Index() => View(_applicationContext.Articles.ToList());

        [HttpGet]
        public IActionResult CreateArticle() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                var newArticle = new Article { Title = model.Title, Description = model.Description, Author =  userName };

                _applicationContext.Articles.Add(newArticle);
                await _applicationContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult GetArticle(int id)
        {
            var article = _applicationContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null) return NotFound();

            return View(article);
        }

        [HttpGet]
        public IActionResult EditArticle(int id)
        {
            var article = _applicationContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null) return NotFound();
            return View(new EditArticleViewModel { Id = article.Id, Description = article.Description, Title = article.Title});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditArticle(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = _applicationContext.Articles.FirstOrDefault(a => a.Id == model.Id);
                if (article == null) return NotFound();
                article.Title = model.Title;
                article.Description = model.Description;
                await _applicationContext.SaveChangesAsync();
                return RedirectToAction("GetArticle", new { id = article.Id });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var article = _applicationContext.Articles.FirstOrDefault(a => a.Id == id);
            if (article == null) return NotFound();
            _applicationContext.Articles.Remove(article);
            await _applicationContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
