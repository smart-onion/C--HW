using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Enums;
using MVCSTEP.Models;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(MatrixViewModel? model)
    {
        if (model == null)
        {
            
        }
        return View(model);
    }

    [HttpPost]
    public IActionResult Calculate(MatrixViewModel model)
    {
        if (!ModelState.IsValid) ;
        var matrix = new Matrix();

        if (model.Action == MatrixAction.Add)
        {
            if (model.First!.AxisX == model.Second!.AxisX && model.First.AxisY == model.Second.AxisY)
            {
                matrix.AxisX = model.First.AxisX;
                matrix.AxisY = model.First.AxisY;
                matrix.Array = new int[matrix.AxisX][];
                for (int i = 0; i < matrix.AxisX; i++)
                {
                    matrix.Array[i] = new int[matrix.AxisY];
                }

                for (var x = 0; x < model.First!.AxisX; x++)
                {
                    for (int y = 0; y < model.First!.AxisY; y++)
                    {
                        matrix.Array[x][y] = model.First.Array[x][y] + model.Second.Array[x][y];
                    }
                }
            }


            model.Result = matrix;
        }
        else if (model.Action == MatrixAction.Multiply)
        {
            if (model.First.AxisX == model.Second.AxisY)
            {
                matrix.AxisX = model.First.AxisX;
                matrix.AxisY = model.Second.AxisY; 
                matrix.Array = new int[matrix.AxisX][];
                for (int i = 0; i < matrix.AxisX; i++)
                {
                    matrix.Array[i] = new int[matrix.AxisY];
                }

               
                for (int i = 0; i < model.First.AxisX; i++) 
                {
                    for (int j = 0; j < model.Second.AxisY; j++) 
                    {
                        for (int k = 0; k < model.First.AxisY; k++) 
                        {
                            matrix.Array[i][j] += model.First.Array[i][k] * model.Second.Array[k][j];
                        }
                    }
                }

                model.Result = matrix;
            }
        }


        return View(nameof(Index), model);
    }
}