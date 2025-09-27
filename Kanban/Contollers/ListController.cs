using Kanban.Interfaces;
using Kanban.Model;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Contollers;

[ApiController]
[Route("api/[controller]")]
public class ListController: ControllerBase
{
    private readonly IList _list;

    public ListController(IList list)
    {
        _list = list;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var list = await _list.GetList(id);
        if (list == null) return NotFound();
        return Ok(list);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _list.GetLists());
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List list)
    {
        var newCard = await _list.CreateList(list);
        return Ok(newCard);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] List list)
    {
        return Ok(await _list.UpdateList(list));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(await _list.DeleteList(id));
    }
}