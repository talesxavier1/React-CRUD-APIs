using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using SingularChatAPIs.BD;
using System.Text.Json;

namespace SingularChatAPIs.Loger;

public class Log {
    
    [BsonId]
    public string codigo { get; set; }

    [BsonElement("logDate")]
    public DateTime logDate { get; set; }

    [BsonElement("isProd")]
    public bool isProd { get; set; }

    [BsonElement("method")]
    public string method { get; set; }

    [BsonElement("headers")]
    public string headers { get; set; }

    [BsonElement("body")]
    public string body { get; set; }

    [BsonElement("path")]
    public string path { get; set; }

    

    public Log() {
        this.codigo = Guid.NewGuid().ToString();
    }
}

public class LoggerService {

    private readonly IMongoCollection<Log> collection;

    public LoggerService() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<Log>("Logs");
    }

    public void postLog(Log log) {
        collection.InsertOne(log);
    }

    public void printLog(Log log) {
        var logDictionary = new Dictionary<string, object>();

        foreach (var property in log.GetType().GetProperties()) {
            logDictionary.Add(property.Name, property.GetValue(log));
        }
        
        Console.WriteLine("\n\n"+JsonSerializer.Serialize(logDictionary));

    }
}



