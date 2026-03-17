using TodoApi.Models;

namespace TodoApi.Services;

public class TodoRepository
{
    private readonly List<TodoItem> _tasks = new()
    {
        new TodoItem { Id = 1, Title = "Prepare project structure", IsDone = false },
        new TodoItem { Id = 2, Title = "Create first API endpoint", IsDone = true }
    };

    public List<TodoItem> GetAll() => _tasks;

    public TodoItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(TodoItem item)
    {
        item.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
        _tasks.Add(item);
    }

    public bool MarkAsDone(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        item.IsDone = true;
        return true;
    }

    public bool Delete(int id)
    {
        var item = GetById(id);
        if (item == null) return false;
        _tasks.Remove(item);
        return true;
    }
}