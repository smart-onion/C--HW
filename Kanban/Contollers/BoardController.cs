using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Kanban.Contollers;

[ApiController]
[Route("api/[controller]")]
public class BoardController : ControllerBase
{
    private readonly IBoard _board;

    public BoardController(IBoard board)
    {
        _board = board;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var board = await _board.GetBoard(id);
        if (board == null) return NotFound();
        return Ok(board);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _board.GetBoards());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Board board)
    {
        await _board.CreateBoard(board);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Board board)
    {
        await _board.UpdateBoard(board);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _board.DeleteBoard(id);
        return Ok();
    }
}