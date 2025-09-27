using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Model;
using Kanban.Services;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repositories;

public class CardRepository : ICard
{
    private readonly ApplicationContext _context;
    private readonly RedisCacheService _cache;
    private readonly string _list = "cards:";

    public CardRepository(ApplicationContext context, RedisCacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<Card>> GetCards()
    {
        var result = await _cache.GetAsync<IEnumerable<Card>>(_list);
        if (result != null) return result;
        var cards =  await _context.Cards.ToListAsync();
        await _cache.SetAsync<List<Card>>(_list, cards, TimeSpan.FromSeconds(60));
        return cards;
    }

    public async Task<Card?> GetCard(int id)
    {
        var result = await _cache.GetAsync<Card>(id.ToString());
        if (result != null) return result;
        var card = await _context.Cards.FirstOrDefaultAsync(b => b.Id == id);
        if (card == null) return null;
        await _cache.SetAsync(card.Id.ToString(), card, TimeSpan.FromSeconds(60));
        return card;
    }

    public async Task<Card> CreateCard(Card card)
    {
        await _cache.RemoveAsync(_list);
        var newCard = await _context.Cards.AddAsync(card);
        await _context.SaveChangesAsync();
        return newCard.Entity;
    }

    public async Task<Card> UpdateCard(Card card)
    {
        await _cache.RemoveAsync(_list);
        await _cache.RemoveAsync(card.Id.ToString());
        var updated = _context.Cards.Update(card);
        await _context.SaveChangesAsync();
        return updated.Entity;
    }

    public async Task<bool> DeleteCard(int id)
    {
        var card = await _cache.GetAsync<Card>(id.ToString());
        if (card == null)
        {
            card = await _context.Cards.FirstOrDefaultAsync(b => b.Id == id);
        }

        if (card != null)
        {
            await _cache.RemoveAsync(_list);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
}