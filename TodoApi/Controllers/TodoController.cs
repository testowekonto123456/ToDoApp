using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoRepository _repository;

    public TodoController(TodoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll()
    {
        return Ok(_repository.GetAll());
    }

    [HttpPost]
    public ActionResult AddTask([FromBody] TodoItem item)
    {
        _repository.Add(item);
        return Ok(item);
    }

    [HttpPut("{id}/done")]
    public ActionResult MarkAsDone(int id)
    {
        var result = _repository.MarkAsDone(id);
        return result ? Ok() : NotFound();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var result = _repository.Delete(id);
        return result ? Ok() : NotFound();
    }
}