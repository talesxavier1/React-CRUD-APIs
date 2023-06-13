using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class PersonRepository : IPersonRepository {
    private readonly IMongoCollection<PersonModel> collection;

    public PersonRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<PersonModel>("Persons");
    }

    public bool addPerson(PersonModel person, UserModel user) {
        try {
            person.dataController = new() {
                active = true,
                userPost = user.userToken,
                postDate = DateTime.UtcNow.AddHours(-3)
            };

            collection.InsertOne(person);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public PersonModel getPersonById(String codigo) {
        try {
            PersonModel personModelResult = collection.Find<PersonModel>(DOC => DOC.codigo.Equals(codigo) && DOC.dataController.active == true).FirstOrDefault();
            return personModelResult;
        } catch (Exception) {
            return null;
        }
    }

    public Boolean deletePerson(string codigo) {
        try {
            DeleteResult result = collection.DeleteOne<PersonModel>(DOC => DOC.codigo == codigo);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public List<PersonModel> getPersonList(int skip, int take) {
        try {
            List<PersonModel> result = collection.Find<PersonModel>(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<PersonModel>();
            return result;
        } catch (Exception) {
            return null;
        }
    }

    public Boolean logicalDeletePerson(string codigo, UserModel user) {

        try {
            List<UpdateDefinition<PersonModel>> updateDefinitions = new() {
                Builders<PersonModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<PersonModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<PersonModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne<PersonModel>(DOC => DOC.codigo.Equals(codigo), Builders<PersonModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public Boolean updatePerson(PersonModel person, UserModel User) {
        PersonModel currentPerson = collection.Find<PersonModel>(DOC => DOC.codigo.Equals(person.codigo)).FirstOrDefault();
        if (currentPerson == null) { return false; }

        List<UpdateDefinition<PersonModel>> updateDefinitions = new();
        foreach (PropertyInfo property in person.GetType().GetProperties()) {
            updateDefinitions.Add(Builders<PersonModel>.Update.Set(property.Name, property.GetValue(person)));
        }
        currentPerson.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentPerson.dataController.userUpdate = User.userToken;
        updateDefinitions.Add(Builders<PersonModel>.Update.Set("dataController", currentPerson.dataController));

        UpdateResult result = collection.UpdateOne<PersonModel>(DOC => DOC.codigo.Equals(person.codigo), Builders<PersonModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }

    public long countPersons(string query) {
        dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
        if (queryObject == null) {
            return 0;
        }
        queryObject["dataController.active"] = true;
        string finalQueryString = JsonConvert.SerializeObject(queryObject);

        return collection.Find(finalQueryString).CountDocuments();
    }

    public long countPersons() {
        return collection.CountDocuments<PersonModel>(DOC => DOC.dataController.active);
    }

    public List<PersonModel> getPersonsByStringQuery(string query, int skip, int take) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find<PersonModel>(finalQueryString).Skip(skip).Limit(take).ToList<PersonModel>();
        } catch (Exception) {
            return new List<PersonModel>();
        }
    }
}

