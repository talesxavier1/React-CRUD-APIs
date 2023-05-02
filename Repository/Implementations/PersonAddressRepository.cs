using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class PersonAddressRepository : IPersonAdressRepository {

    private readonly IMongoCollection<AddressModel> collection;

    public PersonAddressRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<AddressModel>("Address");
    }

    public bool addAddress(AddressModel addressModel, UserModel user) {
        addressModel.dataController = new ControllerModel() {
            active = true,
            postDate = DateTime.UtcNow.AddHours(-3),
            userPost = user.userToken
        };
        collection.InsertOne(addressModel);
        return true;
    }

    public bool updateAddress(AddressModel addressModel, UserModel user) {
        AddressModel currentAdress = collection.Find<AddressModel>(DOC => DOC.codigo.Equals(addressModel.codigo)).FirstOrDefault();
        if (currentAdress == null) { return false; }

        PropertyInfo[] objectProperties = addressModel.GetType().GetProperties();
        List<UpdateDefinition<AddressModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<AddressModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(addressModel)));
        }

        currentAdress.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentAdress.dataController.userUpdate = user.userToken;
        updateDefinitions.Add(Builders<AddressModel>.Update.Set("dataController", currentAdress.dataController));

        UpdateResult result = collection.UpdateOne<AddressModel>(DOC => DOC.codigo.Equals(addressModel.codigo), Builders<AddressModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }

    public List<AddressModel> getAddress(string codigoRef, int skip, int take) {
        if (codigoRef != null) {
            return collection.Find<AddressModel>(DOC => (DOC.codigoRef.Equals(codigoRef) && DOC.dataController.active == true)).Skip(skip).Limit(take).ToList<AddressModel>();
        }

        return collection.Find<AddressModel>(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<AddressModel>();
    }

    public bool deleteAddress(string codigo) {
        DeleteResult result = collection.DeleteOne<AddressModel>(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount > 0;
    }

    public bool logicalDeleteAddress(string[] codigos, UserModel user) {
        List<UpdateDefinition<AddressModel>> updates = new() {
            Builders<AddressModel>.Update.Set("dataController.active", false),
            Builders<AddressModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<AddressModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };

        UpdateResult result = collection.UpdateOne<AddressModel>(DOC => codigos.Contains(DOC.codigo), Builders<AddressModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public AddressModel getAddressById(string codigo) {
        return collection.Find<AddressModel>(DOC => (DOC.codigo.Equals(codigo) && DOC.dataController.active == true)).FirstOrDefault();
    }

    public long count(string? codigoRef) {
        if (codigoRef != null) {
            return collection.CountDocuments<AddressModel>(DOC => (DOC.codigoRef.Equals(codigoRef) && DOC.dataController.active == true));
        }
        return collection.CountDocuments<AddressModel>(DOC => DOC.dataController.active == true);
    }
}

