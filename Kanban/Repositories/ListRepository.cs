using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Model;
using Kanban.Services;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories;

public class ListRepository : IList
{
    private readonly ApplicationContext _context;
    private readonly RedisCacheService _cache;
    private readonly string _list = "list:";

    public ListRepository(ApplicationContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<List>> GetLists()
    {
        var result = await _cache.GetAsync<IEnumerable<List>>(_list);
        if (result != null) return result;
        var lists =  await _context.Lists.ToListAsync();
        await _cache.SetAsync<List<List>>(_list, lists, TimeSpan.FromSeconds(60));
        return lists;
    }

    public async Task<List?> GetList(int id)
    {
        var result = await _cache.GetAsync<List>(id.ToString());
        if (result != null) return result;
        var list = await _context.Lists.FirstOrDefaultAsync(b => b.Id == id);
        if (list == null) return null;
        await _cache.SetAsync(list.Id.ToString(), list, TimeSpan.FromSeconds(60));
        return list;
    }

    public async Task<List> CreateList(List list)
    {
        await _cache.RemoveAsync(_list);
        var newCard = await _context.Lists.AddAsync(list);
        await _context.SaveChangesAsync();
        return newCard.Entity;
    }

    public async Task<List> UpdateList(List list)
    {
        await _cache.RemoveAsync(_list);
        await _cache.RemoveAsync(list.Id.ToString());
        var updated = _context.Lists.Update(list);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task<bool> DeleteList(int id)
    {
        var list = await _cache.GetAsync<List>(id.ToString());
        if (list == null)
        {
            list = await _context.Lists.FirstOrDefaultAsync(b => b.Id == id);
        }

        if (list != null)
        {
            await _cache.RemoveAsync(_list);
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}