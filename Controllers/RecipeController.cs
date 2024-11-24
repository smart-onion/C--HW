using Microsoft.AspNetCore.Mvc;
using MVCHW1.Models;
using System.Text;
using System.Text.Json;

namespace MVCHW1.Controllers
{
    public class RecipeController : Controller
    {

        readonly ProductContext _productContext;

        public RecipeController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<IActionResult> Random()
        {
            var str = await GetRecipeFromChatGptAsync("");
            var recipe = new Recipe() { Description = str };
            await _productContext.Recipes.AddAsync(recipe);
            await _productContext.SaveChangesAsync();
            return Json(recipe);
        }

        public async Task<IActionResult> Ingredient(string ingredient)
        {
            var str = await GetRecipeFromChatGptAsync(ingredient);
            var recipe = new Recipe() { Description = str };
            await _productContext.Recipes.AddAsync(recipe);
            await _productContext.SaveChangesAsync();
            return Json(recipe);
        }
        public async Task<IActionResult> Category(string category)
        {
            var str = await GetRecipeFromChatGptAsync(category);
            var recipe = new Recipe() { Description = str };
            await _productContext.Recipes.AddAsync(recipe);
            await _productContext.SaveChangesAsync();
            return Json(recipe);
        }

        [NonAction]
        async Task<string> GetRecipeFromChatGptAsync(string? word)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            var apiKey = configuration.GetValue<string>("ChatGptApiKey");

            var requestBody = new
            {
                model = "gpt-4o-mini",
                messages = new[]
                {
                    new { role = "user", content = $"write a random recipe with {word}" }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
        }
    }
}
