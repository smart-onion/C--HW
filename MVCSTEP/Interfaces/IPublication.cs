using MVCSTEP.Models;

namespace MVCSTEP.Interfaces;

public interface IPublication
{
    public Task<IEnumerable<Publication>?> GetPublications();
    public Task<Publication?> GetPublication(int id);
    public Task<Publication> UpdatePublication(Publication publication);
    public Task<Publication> AddPublication(Publication publication);
    public Task<Publication> DeletePublication(int id);
}