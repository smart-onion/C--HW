using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Services;

namespace ToDoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ApplicationContext _context;
    private readonly RedisCacheService _cache;
    private const string ListKey = "tasks:all";
 
    public TasksController(ApplicationContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }
 
    [HttpGet]
    public async Task<IEnumerable<TaskItem>> GetAll()
    {
        var cached = await _cache.GetAsync<List<TaskItem>>(ListKey);
        if (cached != null) return cached;
 
        var list = await _context.Tasks.OrderBy(t => t.CreatedAt).ToListAsync();
        await _cache.SetAsync(ListKey, list, TimeSpan.FromMinutes(5));
        return list;
    }
 
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(Guid id)
    {
        var key = $"task:{id}";
        var item = await _cache.GetAsync<TaskItem>(key);
        if (item != null) return Ok(item);
 
        item = await _context.Tasks.FindAsync(id);
        if (item == null) return NotFound();
 
        await _cache.SetAsync(key, item, TimeSpan.FromMinutes(2));
        return Ok(item);
    }
 
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem dto)
    {
        dto.Id = Guid.NewGuid();
        dto.CreatedAt = DateTime.UtcNow;
        _context.Tasks.Add(dto);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync(ListKey);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }
 
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, TaskItem dto)
    {
        var exists = await _context.Tasks.FindAsync(id);
        if (exists == null) return NotFound();
 
        exists.Title = dto.Title;
        exists.Description = dto.Description;
        exists.IsCompleted = dto.IsCompleted;
        await _context.SaveChangesAsync();
 
        await _cache.RemoveAsync(ListKey);
        await _cache.RemoveAsync($"task:{id}");
        return NoContent();
    }
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _context.Tasks.FindAsync(id);
        if (item == null) return NotFound();
 
        _context.Tasks.Remove(item);
        await _context.SaveChangesAsync();
        await _cache.RemoveAsync(ListKey);
        await _cache.RemoveAsync($"task:{id}");
        return NoContent();
    }
}