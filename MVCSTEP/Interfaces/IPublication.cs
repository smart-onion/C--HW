using MVCSTEP.Models;
using MVCSTEP.Models.Pages;

namespace MVCSTEP.Interfaces;

public interface IPublication
{
    Task<IEnumerable<Publication>> GetAllPublicationsAsync();
    PagedList<Publication> GetAllPublicationsWithCategories(QueryOptions options);
    Task<Publication> GetPublicationAsync(string id);
    Task<Publication> GetPublicationWithCategoriesAsync(string id);
 
    Task UpdateViewsAsync(string id);
 
    Task AddPublicationAsync(Publication publication);
    Task UpdatePublicationAsync(Publication publication);
    Task DeletePublicationAsync(Publication publication);
    
    Task<PagedList<Publication>> GetAllPublicationsByCategoryWithCategories(QueryOptions options, string id);
    Task<IEnumerable<Publication>> GetFourRandomPublicationsAsync(string id);
}