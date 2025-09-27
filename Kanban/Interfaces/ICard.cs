using Kanban.Model;

namespace Kanban.Interfaces;

public interface ICard
{
    Task<IEnumerable<Card>> GetCards();
    Task<Card?> GetCard(int id);
    Task<Card> CreateCard(Card board);
    Task<Card> UpdateCard(Card board);
    Task<bool> DeleteCard(int id);
}