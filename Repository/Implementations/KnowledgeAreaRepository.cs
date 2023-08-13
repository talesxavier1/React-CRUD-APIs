using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class KnowledgeAreaRepository : IKnowledgeAreaRepository {

    private readonly IMongoCollection<KnowledgeAreaModel> collection;

    public KnowledgeAreaRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<KnowledgeAreaModel>("KnowledgeArea");
    }

    public bool addKnowledgeArea(KnowledgeAreaModel classKnowledgeAreaModel, UserModel user) {
        try {
            classKnowledgeAreaModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(classKnowledgeAreaModel);
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

    public bool deleteKnowledgeArea(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public KnowledgeAreaModel getKnowledgeAreaById(string codigo) {
        KnowledgeAreaModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<KnowledgeAreaModel> getKnowledgeAreas(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<KnowledgeAreaModel> getKnowledgeAreas(int skip, int take, string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList<KnowledgeAreaModel>();
        } catch (Exception) {
            return new List<KnowledgeAreaModel>();
        }
    }

    public bool logicalDeleteKnowledgeArea(string[] codigos, UserModel user) {
        List<UpdateDefinition<KnowledgeAreaModel>> updates = new() {
            Builders<KnowledgeAreaModel>.Update.Set("dataController.active", false),
            Builders<KnowledgeAreaModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<KnowledgeAreaModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<KnowledgeAreaModel>(DOC => codigos.Contains(DOC.codigo), Builders<KnowledgeAreaModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateKnowledgeArea(KnowledgeAreaModel classKnowledgeAreaModel, UserModel User) {
        KnowledgeAreaModel currentclassKnowledgeArea = collection.Find<KnowledgeAreaModel>(DOC => DOC.codigo.Equals(classKnowledgeAreaModel.codigo)).FirstOrDefault();
        if (currentclassKnowledgeArea == null) { return false; }

        PropertyInfo[] objectProperties = classKnowledgeAreaModel.GetType().GetProperties();
        List<UpdateDefinition<KnowledgeAreaModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<KnowledgeAreaModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(classKnowledgeAreaModel)));
        }

        currentclassKnowledgeArea.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentclassKnowledgeArea.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<KnowledgeAreaModel>.Update.Set("dataController", currentclassKnowledgeArea.dataController));

        UpdateResult result = collection.UpdateOne<KnowledgeAreaModel>(DOC => DOC.codigo.Equals(classKnowledgeAreaModel.codigo), Builders<KnowledgeAreaModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}
