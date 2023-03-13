using Microsoft.EntityFrameworkCore;
using TasksApi.Models;
using TasksApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<TasksDatabaseSettings>(builder.Configuration.GetSection("TasksDatabase"));
builder.Services.AddSingleton<TaskService>();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();
/**

app.MapGet("/",() => "Hello world!");

app.MapGet("/tasks", async (TaskService db) =>
    await db.GetAsync());

app.MapGet("/tasks/{id}", async (int id,TaskService db) =>
    {
        var task = await db.GetAsync(id.ToString());
        if (task is null) return Results.NotFound();

        return Results.Ok(task);

    });

app.MapPost("/tasks", async (Todo task, TaskService db) =>
{
    db.CreateAsync(task);


    return Results.Created($"/tasks/{task.Id}", task);
});

app.MapPut("/tasks/{id}", async (int id, Todo inputTask, TaskService db) =>
{
    var task = await db.GetAsync(id.ToString());

    if (task is null) return Results.NotFound();


    task.reminder = !task.reminder;

    return Results.Ok(task);
});

app.MapDelete("/tasks/{id}", async (int id, TaskService db) =>
{
    if (await db.GetAsync(id.ToString()) is Todo task)
    {
        db.RemoveAsync(id.ToString());
        return Results.Ok(task);
    }

    return Results.NotFound();
});

**/



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{id?}");


app.Run();