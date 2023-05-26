using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class AreaOfSpecializationRepository : IAreaOfSpecializationRepository {

    private readonly IMongoCollection<AreaOfSpecializationModel> collection;

    public AreaOfSpecializationRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<AreaOfSpecializationModel>("AreaOfSpecialization");
    }

    public bool addAreaOfSpecialization(AreaOfSpecializationModel areaOfSpecializationModel, UserModel user) {
        try {
            areaOfSpecializationModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(areaOfSpecializationModel);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long count() {
        long result = collection.CountDocuments(DOC => DOC.dataController.active == true);
        return result;
    }

    public bool deleteAreaOfSpecialization(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public AreaOfSpecializationModel getAreasOfSpecializationById(string codigo) {
        AreaOfSpecializationModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<AreaOfSpecializationModel> getAreasOfSpecialization(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<AreaOfSpecializationModel> getAreasOfSpecialization(int skip, int take, string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find<AreaOfSpecializationModel>(finalQueryString).Skip(skip).Limit(take).ToList<AreaOfSpecializationModel>();
        } catch (Exception) {
            return new List<AreaOfSpecializationModel>();
        }
    }

    public bool logicalDeleteAreaOfSpecialization(string[] codigos, UserModel user) {
        List<UpdateDefinition<AreaOfSpecializationModel>> updates = new() {
            Builders<AreaOfSpecializationModel>.Update.Set("dataController.active", false),
            Builders<AreaOfSpecializationModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<AreaOfSpecializationModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<AreaOfSpecializationModel>(DOC => codigos.Contains(DOC.codigo), Builders<AreaOfSpecializationModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateAreaOfSpecialization(AreaOfSpecializationModel areaOfSpecializationModel, UserModel User) {
        AreaOfSpecializationModel currentArea = collection.Find<AreaOfSpecializationModel>(DOC => DOC.codigo.Equals(areaOfSpecializationModel.codigo)).FirstOrDefault();
        if (currentArea == null) { return false; }

        PropertyInfo[] objectProperties = areaOfSpecializationModel.GetType().GetProperties();
        List<UpdateDefinition<AreaOfSpecializationModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<AreaOfSpecializationModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(areaOfSpecializationModel)));
        }

        currentArea.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentArea.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<AreaOfSpecializationModel>.Update.Set("dataController", currentArea.dataController));

        UpdateResult result = collection.UpdateOne<AreaOfSpecializationModel>(DOC => DOC.codigo.Equals(areaOfSpecializationModel.codigo), Builders<AreaOfSpecializationModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
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
}

