using Kanban.Model;

namespace Kanban.Interfaces;

public interface IBoard
{
    Task<IEnumerable<Board>> GetBoards();
    Task<Board?> GetBoard(int id);
    Task CreateBoard(Board board);
    Task UpdateBoard(Board board);
    Task DeleteBoard(int id);
}