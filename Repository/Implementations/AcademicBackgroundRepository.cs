using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class AcademicBackgroundRepository : IAcademicBackgroundRepository {

    private readonly IMongoCollection<AcademicBackgroundModel> collection;

    public AcademicBackgroundRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<AcademicBackgroundModel>("AcademicBackground");
    }

    public bool addAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel user) {
        academicBackgroundModel.dataController = new ControllerModel() {
            active = true,
            postDate = DateTime.UtcNow.AddHours(-3),
            userPost = user.userToken
        };
        collection.InsertOne(academicBackgroundModel);
        return true;
    }

    public long count(string? codigoRef) {
        long result = collection.CountDocuments(DOC => DOC.dataController.active == true);
        return result;
    }

    public bool deleteAcademicBackground(string codigo) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount == 1;
    }

    public AcademicBackgroundModel getAcademicBackgroundById(string codigo) {
        AcademicBackgroundModel result = collection.Find(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
        return result;
    }

    public List<AcademicBackgroundModel> getAcademicBackgrounds(int skip, int take) {
        return collection.Find(DOC => (DOC.dataController.active == true)).Skip(skip).Limit(take).ToList();
    }

    public bool logicalDeleteAcademicBackground(string[] codigos, UserModel user) {
        List<UpdateDefinition<AcademicBackgroundModel>> updates = new() {
            Builders<AcademicBackgroundModel>.Update.Set("dataController.active", false),
            Builders<AcademicBackgroundModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<AcademicBackgroundModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<AcademicBackgroundModel>(DOC => codigos.Contains(DOC.codigo), Builders<AcademicBackgroundModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel User) {
        AcademicBackgroundModel currentBackground = collection.Find<AcademicBackgroundModel>(DOC => DOC.codigo.Equals(academicBackgroundModel.codigo)).FirstOrDefault();
        if (currentBackground == null) { return false; }

        PropertyInfo[] objectProperties = academicBackgroundModel.GetType().GetProperties();
        List<UpdateDefinition<AcademicBackgroundModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<AcademicBackgroundModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(academicBackgroundModel)));
        }

        currentBackground.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentBackground.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<AcademicBackgroundModel>.Update.Set("dataController", currentBackground.dataController));

        UpdateResult result = collection.UpdateOne<AcademicBackgroundModel>(DOC => DOC.codigo.Equals(academicBackgroundModel.codigo), Builders<AcademicBackgroundModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}

