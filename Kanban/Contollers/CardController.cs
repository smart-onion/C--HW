using Kanban.Interfaces;
using Kanban.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Contollers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICard _card;

    public CardController(ICard card)
    {
        _card = card;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var card = await _card.GetCard(id);
        if (card == null) return NotFound();
        return Ok(card);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _card.GetCards());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Card card)
    {
        var newCard = await _card.CreateCard(card);
        return Ok(newCard);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Card card)
    {
        return Ok(await _card.UpdateCard(card));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _card.DeleteCard(id));
    }
}