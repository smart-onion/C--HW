using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Model;
using Kanban.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Kanban.Repositories;

public class BoardRepository : IBoard
{
    private readonly ApplicationContext _context;
    private readonly RedisCacheService _cache;
    private readonly string _list = "boards:";

    public BoardRepository(ApplicationContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Board>> GetBoards()
    {
        var result = await _cache.GetAsync<IEnumerable<Board>>(_list);
        if (result != null) return result;
        var boards = await _context.Boards.ToListAsync();
        await _cache.SetAsync<List<Board>>(_list, boards, TimeSpan.FromSeconds(60));
        return boards;
    }

    public async Task<Board?> GetBoard(int id)
    {
        var result = await _cache.GetAsync<Board>(id.ToString());
        if (result != null) return result;
        var board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == id);
        if (board == null) return null;
        await _cache.SetAsync(board.Id.ToString(), board, TimeSpan.FromSeconds(60));
        return board;
    }

    public async Task CreateBoard(Board board)
    {
        await _cache.RemoveAsync(_list);
        await _context.Boards.AddAsync(board);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBoard(Board board)
    {
        await _cache.RemoveAsync(_list);
        await _cache.RemoveAsync(board.Id.ToString());
        _context.Boards.Update(board);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBoard(int id)
    {
        var board = await _cache.GetAsync<Board>(id.ToString());
        if (board == null)
        {
            board = await _context.Boards.FirstOrDefaultAsync(b => b.Id == id);
        }

        if (board != null)
        {
            await _cache.RemoveAsync(_list);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }
    }
}