using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MVCSTEP.Models;

namespace MVCSTEP.Helpers;

public class MatrixTagHelper : TagHelper
{
    public required string Name { get; set; }     

    public Matrix Matrix { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "matrix";
        var html = new StringBuilder();
        if (Matrix.Array == null)
        {
            Matrix.Array = new int[Matrix.AxisX][];
            for (int i = 0; i < Matrix.AxisX; i++)
            {
                Matrix.Array[i] = new int[Matrix.AxisY];
            }
        }

        html.Append($"<input type='hidden' name='{Name}.AxisX' value='{Matrix.AxisX}'>");
        html.Append($"<input type='hidden' name='{Name}.AxisY' value='{Matrix.AxisY}'>");
        html.Append($"<div style='display: grid;grid-template-columns: repeat({Matrix.AxisX}, 200px);'>");
        for (var x = 0; x < Matrix.AxisX; x++)
        {
            html.Append("<div>");
            for (var y = 0; y < Matrix.AxisY; y++)
            {
                html.Append($"<input value='{Matrix.Array[x][y]}' name='{Name}.Array[{x}][{y}]' />");
            }
            html.Append("</div>");
            
        }

        html.Append("</div>");
        
        output.Content.SetHtmlContent(html.ToString());
    }
}