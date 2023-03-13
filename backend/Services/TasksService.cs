using TasksApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TasksApi.Services;

public class TaskService
{
    private readonly IMongoCollection<Todo> _tasksCollection;

    public TaskService(
        IOptions<TasksDatabaseSettings> tasksDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            tasksDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            tasksDatabaseSettings.Value.DatabaseName);

        _tasksCollection = mongoDatabase.GetCollection<Todo>(
            tasksDatabaseSettings.Value.TasksCollectionName);
    }

    public async Task<List<Todo>> GetAsync() =>
        await _tasksCollection.Find(_ => true).ToListAsync();

    public async Task<Todo?> GetAsync(string id) =>
        await _tasksCollection.Find(x => x.id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Todo newTodo) =>
        await _tasksCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, Todo updatedTodo) =>
        await _tasksCollection.ReplaceOneAsync(x => x.id == id, updatedTodo);

    public async Task RemoveAsync(string id) =>
        await _tasksCollection.DeleteOneAsync(x => x.id == id);
}