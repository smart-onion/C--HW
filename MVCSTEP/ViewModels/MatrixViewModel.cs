using MVCSTEP.Enums;
using MVCSTEP.Models;

namespace MVCSTEP.ViewModels;

public class MatrixViewModel
{
    public Matrix? First { get; set; }
    public Matrix? Second { get; set; }
    public Matrix? Result { get; set; }
    public MatrixAction? Action { get; set; }
}