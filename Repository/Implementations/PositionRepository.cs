using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class PositionRepository : IPositionRepository {

    private readonly IMongoCollection<PositionModel> collection;

    public PositionRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<PositionModel>("Position");
    }

    public bool addPosition(PositionModel positionsModel, UserModel user) {
        try {
            positionsModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(positionsModel);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long count() {
        long result = collection.CountDocuments(DOC => DOC.dataController.active == true);
        return result;
    }

    public long count(string query) {
        dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
        if (queryObject == null) {
            return 0;
        }
        queryObject["dataController.active"] = true;
        string finalQueryString = JsonConvert.SerializeObject(queryObject);

        return collection.Find(finalQueryString).CountDocuments();
    }

    public bool deletePosition(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public PositionModel getPositionById(string codigo) {
        PositionModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<PositionModel> getPositions(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<PositionModel> getPositions(int skip, int take, string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find<PositionModel>(finalQueryString).Skip(skip).Limit(take).ToList<PositionModel>();
        } catch (Exception) {
            return new List<PositionModel>();
        }
    }

    public bool logicalDeletePosition(string[] codigos, UserModel user) {
        List<UpdateDefinition<PositionModel>> updates = new() {
            Builders<PositionModel>.Update.Set("dataController.active", false),
            Builders<PositionModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<PositionModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<PositionModel>(DOC => codigos.Contains(DOC.codigo), Builders<PositionModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updatePosition(PositionModel positionsModel, UserModel User) {
        PositionModel currentPosition = collection.Find<PositionModel>(DOC => DOC.codigo.Equals(positionsModel.codigo)).FirstOrDefault();
        if (currentPosition == null) { return false; }

        PropertyInfo[] objectProperties = positionsModel.GetType().GetProperties();
        List<UpdateDefinition<PositionModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<PositionModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(positionsModel)));
        }

        currentPosition.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentPosition.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<PositionModel>.Update.Set("dataController", currentPosition.dataController));

        UpdateResult result = collection.UpdateOne<PositionModel>(DOC => DOC.codigo.Equals(positionsModel.codigo), Builders<PositionModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}

