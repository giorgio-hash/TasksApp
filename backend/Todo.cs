using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TasksApi.Models;

public class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? id { get; set; }
    public string text { get; set; } = null!;
    public string day { get; set; } = null!;

    public Boolean reminder {get; set;}

    public void setId(){
        var rand = new Random();

        this.id = ObjectId.GenerateNewId().ToString();

    }

}