using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class CurricularComponentRepository : ICurricularComponentRepository {

    private readonly IMongoCollection<CurricularComponentModel> collection;

    public CurricularComponentRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<CurricularComponentModel>("CurricularComponent");
    }

    public bool addCurricularComponent(CurricularComponentModel curricularComponentModel, UserModel user) {
        try {
            curricularComponentModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(curricularComponentModel);
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

    public bool deleteCurricularComponent(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public CurricularComponentModel getCurricularComponentById(string codigo) {
        CurricularComponentModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<CurricularComponentModel> getCurricularComponents(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<CurricularComponentModel> getCurricularComponents(int skip, int take, string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList<CurricularComponentModel>();
        } catch (Exception) {
            return new List<CurricularComponentModel>();
        }
    }

    public bool logicalDeleteCurricularComponent(string[] codigos, UserModel user) {
        List<UpdateDefinition<CurricularComponentModel>> updates = new() {
            Builders<CurricularComponentModel>.Update.Set("dataController.active", false),
            Builders<CurricularComponentModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<CurricularComponentModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<CurricularComponentModel>(DOC => codigos.Contains(DOC.codigo), Builders<CurricularComponentModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateCurricularComponent(CurricularComponentModel curricularComponentModel, UserModel User) {
        CurricularComponentModel currentCurricularComponent = collection.Find<CurricularComponentModel>(DOC => DOC.codigo.Equals(curricularComponentModel.codigo)).FirstOrDefault();
        if (currentCurricularComponent == null) { return false; }

        PropertyInfo[] objectProperties = currentCurricularComponent.GetType().GetProperties();
        List<UpdateDefinition<CurricularComponentModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<CurricularComponentModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(curricularComponentModel)));
        }

        currentCurricularComponent.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentCurricularComponent.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<CurricularComponentModel>.Update.Set("dataController", currentCurricularComponent.dataController));

        UpdateResult result = collection.UpdateOne<CurricularComponentModel>(DOC => DOC.codigo.Equals(curricularComponentModel.codigo), Builders<CurricularComponentModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}
