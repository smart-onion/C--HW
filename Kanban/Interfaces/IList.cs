using Kanban.Model;

namespace Kanban.Interfaces;

public interface IList
{
    Task<IEnumerable<List>> GetLists();
    Task<List?> GetList(int id);
    Task<List> CreateList(List list);
    Task<List> UpdateList(List list);
    Task<bool> DeleteList(int id);
}