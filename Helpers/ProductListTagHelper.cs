using hw8.Models;
using hw8.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace hw8.Helpers
{
    public class ProductListTagHelper:TagHelper
    {
        public ProductService products;
        public ProductListTagHelper(ProductService products)
        {
            this.products = products;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";    // Replaces <email> with <a> tag
            output.Content.AppendHtml($"<tr>" +
                $"<td>Name</td>" +
                $"<td>Category</td>" +
                $"<td>Price</td>" +
                $"<td>Description</td>" +
                $"</tr>");

            foreach (var product in products.GetProducts())
            {
                output.Content.AppendHtml($"<tr>" +
                    $"<th>{product.Name}</th>" +
                    $"<th>{product.Category}</th>" +
                    $"<th>{product.Price}</th>" +
                    $"<th>{product.Description}</th>" +
                    $"<th><a class=\"btn btn-primary\" href=\"Home/Edit?id={product.Id}\">Update</a></th>" +
                    $"</tr>");
            }
        }
    }
}
