using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private static List<TodoItem> todoItems = new List<TodoItem>
    {
        new TodoItem { Id = 1, Name = "Learn .NET Core", IsComplete = false },
        new TodoItem { Id = 2, Name = "Build a Web API", IsComplete = false }
    };

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> Get()
    {
        return Ok(todoItems);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> Get(long id)
    {
        var todoItem = todoItems.Find(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public ActionResult<TodoItem> Post(TodoItem todoItem)
    {
        todoItem.Id = todoItems.Count + 1;
        todoItems.Add(todoItem);
        return CreatedAtAction(nameof(Get), new { id = todoItem.Id }, todoItem);
    }

    [HttpPut("{id}")]
    public IActionResult Put(long id, TodoItem updatedTodoItem)
    {
        var todoItem = todoItems.Find(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItem.Name = updatedTodoItem.Name;
        todoItem.IsComplete = updatedTodoItem.IsComplete;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id)
    {
        var todoItem = todoItems.Find(item => item.Id == id);
        if (todoItem == null)
        {
            return NotFound();
        }

        todoItems.Remove(todoItem);
        return NoContent();
    }
}
