using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class ContactPersonRepository : IContactPersonRepository {
    private readonly IMongoCollection<ContactModel> collection;

    public ContactPersonRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<ContactModel>("Contact");
    }

    public bool addContact(ContactModel contact, UserModel user) {
        contact.dataController = new ControllerModel() {
            active = true,
            postDate = DateTime.UtcNow.AddHours(-3),
            userPost = user.userToken
        };
        collection.InsertOne(contact);
        return true;
    }

    public bool deleteContact(string codigo) {
        DeleteResult result = collection.DeleteOne<ContactModel>(DOC => DOC.codigo.Equals(codigo));
        return result.DeletedCount > 0;
    }

    public List<ContactModel> getContact(string codigoRef, int skip, int take) {
        if (codigoRef != null) {
            return collection.Find<ContactModel>(DOC => (DOC.codigoRef.Equals(codigoRef) && DOC.dataController.active == true)).Skip(skip).Limit(take).ToList<ContactModel>();
        }

        return collection.Find<ContactModel>(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<ContactModel>();
    }

    public ContactModel getContactById(string codigo) {
        return collection.Find<ContactModel>(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
    }

    public bool logicalDeleteContact(string[] codigos, UserModel user) {
        List<UpdateDefinition<ContactModel>> updates = new() {
            Builders<ContactModel>.Update.Set("dataController.active", false),
            Builders<ContactModel>.Update.Set("dataController.userUpdate", user.userToken),
            Builders<ContactModel>.Update.Set("dataController.updateDate", DateTime.UtcNow.AddHours(-3))
        };
        UpdateResult result = collection.UpdateOne<ContactModel>(DOC => codigos.Contains(DOC.codigo), Builders<ContactModel>.Update.Combine(updates));
        return result.ModifiedCount > 0;
    }

    public bool updateContact(ContactModel contact, UserModel user) {
        ContactModel currentContact = collection.Find<ContactModel>(DOC => DOC.codigo.Equals(contact.codigo)).FirstOrDefault();
        if (currentContact == null) { return false; }

        PropertyInfo[] objectProperties = contact.GetType().GetProperties();
        List<UpdateDefinition<ContactModel>> updateDefinitions = new();

        foreach (PropertyInfo propertyInfo in objectProperties) {
            updateDefinitions.Add(Builders<ContactModel>.Update.Set(propertyInfo.Name, propertyInfo.GetValue(contact)));
        }

        currentContact.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentContact.dataController.userUpdate = user.userToken;
        updateDefinitions.Add(Builders<ContactModel>.Update.Set("dataController", currentContact.dataController));

        UpdateResult result = collection.UpdateOne<ContactModel>(DOC => DOC.codigo.Equals(contact.codigo), Builders<ContactModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }

    public long count(string? codigoRef) {
        if (codigoRef == null) {
            return collection.CountDocuments<ContactModel>(DOC => DOC.dataController.active == true);
        }
        return collection.CountDocuments<ContactModel>(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef));
    }
}
