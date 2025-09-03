using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;

public class ApplicationContext:DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> context):base(context)
    {
 
    }
    
    
}