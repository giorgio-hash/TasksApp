using TasksApi.Models;
using TasksApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace TaskApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService) =>
        _taskService = taskService;

    [HttpGet]
    public async Task<List<Todo>> Get() =>
        await _taskService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> Get(string id)
    {
        var Todo = await _taskService.GetAsync(id);

        if (Todo is null)
        {
            return NotFound();
        }

        return Todo;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Todo newTodo)
    {
        newTodo.setId();
        Console.WriteLine("postato: " + newTodo);

        await _taskService.CreateAsync(newTodo);

        return CreatedAtAction(nameof(Get), new { id = newTodo.id }, newTodo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> Update(string id, Todo updatedTask)
    {
        Todo todo = await _taskService.GetAsync(id);

        if( todo is null)
        {
            return NotFound();
        }

        updatedTask.reminder = !todo.reminder;

        await _taskService.UpdateAsync(id, updatedTask);

        return updatedTask;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Todo = await _taskService.GetAsync(id);

        if (Todo is null)
        {
            return NotFound();
        }

        await _taskService.RemoveAsync(id);


        return NoContent(); //ho modificato la frontend: codice 204
    }
}