using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class ClassKnowledgeAreaRepository : IClassKnowledgeAreaRepository {

    private readonly IMongoCollection<ClassKnowledgeAreaModel> collection;

    public ClassKnowledgeAreaRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<ClassKnowledgeAreaModel>("ClassKnowledgeAreaModel");
    }

    public bool addClassKnowledgeArea(ClassKnowledgeAreaModel classKnowledgeAreaModel, UserModel user) {
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

    public bool deleteClassKnowledgeArea(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public ClassKnowledgeAreaModel getClassKnowledgeAreaById(string codigo) {
        ClassKnowledgeAreaModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<ClassKnowledgeAreaModel> getClassKnowledgeAreas(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<ClassKnowledgeAreaModel> getClassKnowledgeAreas(int skip, int take, string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList<ClassKnowledgeAreaModel>();
        } catch (Exception) {
            return new List<ClassKnowledgeAreaModel>();
        }
    }

    public bool logicalDeleteClassKnowledgeArea(string[] codigos, UserModel user) {
        List<UpdateDefinition<ClassKnowledgeAreaModel>> updates = new() {
            Builders<ClassKnowledgeAreaModel>.Update.Set("dataController.active", false),
            Builders<ClassKnowledgeAreaModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<ClassKnowledgeAreaModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<ClassKnowledgeAreaModel>(DOC => codigos.Contains(DOC.codigo), Builders<ClassKnowledgeAreaModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateClassKnowledgeArea(ClassKnowledgeAreaModel classKnowledgeAreaModel, UserModel User) {
        ClassKnowledgeAreaModel currentclassKnowledgeArea = collection.Find<ClassKnowledgeAreaModel>(DOC => DOC.codigo.Equals(classKnowledgeAreaModel.codigo)).FirstOrDefault();
        if (currentclassKnowledgeArea == null) { return false; }

        PropertyInfo[] objectProperties = classKnowledgeAreaModel.GetType().GetProperties();
        List<UpdateDefinition<ClassKnowledgeAreaModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<ClassKnowledgeAreaModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(classKnowledgeAreaModel)));
        }

        currentclassKnowledgeArea.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentclassKnowledgeArea.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<ClassKnowledgeAreaModel>.Update.Set("dataController", currentclassKnowledgeArea.dataController));

        UpdateResult result = collection.UpdateOne<ClassKnowledgeAreaModel>(DOC => DOC.codigo.Equals(classKnowledgeAreaModel.codigo), Builders<ClassKnowledgeAreaModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}
