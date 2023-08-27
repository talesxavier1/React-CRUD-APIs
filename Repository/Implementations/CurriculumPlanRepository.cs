using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class CurriculumPlanRepository : ICurriculumPlanRepository {

    private readonly IMongoCollection<CurriculumPlanModel> collection;

    public CurriculumPlanRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<CurriculumPlanModel>("CurriculumPlan");
    }

    public bool AddCurriculumPlan(CurriculumPlanModel curriculumPlanModel, UserModel user) {
        try {
            curriculumPlanModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(curriculumPlanModel);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long count(string? codigoRef) {
        long result = 0;
        if (codigoRef != null) {
            result = collection.CountDocuments(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef));
        }
        result = collection.CountDocuments(DOC => DOC.dataController.active == true);

        return result;
    }

    public long count(string query, string? codigoRef) {
        dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
        if (queryObject == null) {
            return 0;
        }
        queryObject["dataController.active"] = true;

        if (codigoRef != null) {
            queryObject.codigoRef = codigoRef;
        }

        string finalQueryString = JsonConvert.SerializeObject(queryObject);

        return collection.Find(finalQueryString).CountDocuments();
    }

    public bool deleteCurriculumPlan(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public CurriculumPlanModel getCurriculumPlanById(string codigo) {
        var result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<CurriculumPlanModel> getCurriculumPlans(int skip, int take, string? codigoRef) {
        if (codigoRef != null) {
            return collection.Find(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef)).Skip(skip).Limit(take).ToList();
        }
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public List<CurriculumPlanModel> getCurriculumPlans(int skip, int take, string query, string? codigoRef) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            if (codigoRef != null) {
                queryObject.codigoRef = codigoRef;
            }

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList();
        } catch (Exception) {
            return new List<CurriculumPlanModel>();
        }
    }

    public bool logicalDeleteCurriculumPlan(string[] codigos, UserModel user) {
        List<UpdateDefinition<CurriculumPlanModel>> updates = new() {
            Builders<CurriculumPlanModel>.Update.Set("dataController.active", false),
            Builders<CurriculumPlanModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<CurriculumPlanModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne(DOC => codigos.Contains(DOC.codigo), Builders<CurriculumPlanModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateCurriculumPlan(CurriculumPlanModel curriculumPlanModel, UserModel User) {
        var currentCurriculumPlanModel = collection.Find(DOC => DOC.codigo.Equals(curriculumPlanModel.codigo)).FirstOrDefault();
        if (currentCurriculumPlanModel == null) { return false; }

        PropertyInfo[] objectProperties = currentCurriculumPlanModel.GetType().GetProperties();
        List<UpdateDefinition<CurriculumPlanModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<CurriculumPlanModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(currentCurriculumPlanModel)));
        }

        currentCurriculumPlanModel.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentCurriculumPlanModel.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<CurriculumPlanModel>.Update.Set("dataController", currentCurriculumPlanModel.dataController));

        UpdateResult result = collection.UpdateOne<CurriculumPlanModel>(DOC => DOC.codigo.Equals(curriculumPlanModel.codigo), Builders<CurriculumPlanModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }

}
