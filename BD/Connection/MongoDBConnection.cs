using MongoDB.Driver;
using SingularChatAPIs.utils;

namespace SingularChatAPIs.BD;
public static class MongoDBConnection {

    private static MongoClient mongoClient;
    private static IMongoDatabase database;

    static MongoDBConnection() {
        start();
    }
    public static IMongoDatabase getMongoDatabase() {
        return database;
    }

    public static void start() {
        if (mongoClient == null) {
            Console.WriteLine("[MongoDBConnection:MongoDBConnection] Init MongoConnection.");
            mongoClient = new MongoClient(AppSettings.appSetting["MongoConnection:connectionString"]);
            database = mongoClient.GetDatabase(AppSettings.appSetting["MongoConnection:database"]);
            Console.WriteLine("[MongoDBConnection:MongoDBConnection] Final MongoConnection.");
        }
    }
}

