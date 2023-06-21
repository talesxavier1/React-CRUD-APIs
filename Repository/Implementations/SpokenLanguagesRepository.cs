using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class SpokenLanguagesRepository : ISpokenLanguagesRepository {


    private readonly IMongoCollection<SpokenLanguagesModel> collection;

    public SpokenLanguagesRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<SpokenLanguagesModel>("SpokenLanguage");
    }

    public bool addSpokenLanguage(SpokenLanguagesModel spokenLanguages, UserModel user) {
        try {
            spokenLanguages.dataController = new() {
                active = true,
                userPost = user.userToken,
                postDate = DateTime.UtcNow.AddHours(-3)
            };

            collection.InsertOne(spokenLanguages);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long countSpokenLanguages(string? codigoRef) {
        if (codigoRef == null) {
            return collection.CountDocuments(DOC => DOC.dataController.active == true);
        } else {
            return collection.CountDocuments(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef));
        }
    }

    public long countSpokenLanguagesByQuery(string query) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                return 0;
            }
            queryObject["dataController.active"] = true;
            string finalQueryString = JsonConvert.SerializeObject(queryObject);
            return collection.Find(finalQueryString).CountDocuments();
        } catch (Exception) {
            return 0;
        }
    }

    public bool deleteSpokenLanguage(string id) {
        try {
            var result = collection.DeleteOne(DOC => DOC.codigo.Equals(id));
            return result.DeletedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public SpokenLanguagesModel getSpokenLanguageById(string id) {
        try {
            return collection.Find(DOC => DOC.codigo.Equals(id)).FirstOrDefault<SpokenLanguagesModel>();
        } catch (Exception) {
            return null;
        }
    }

    public List<SpokenLanguagesModel> getSpokenLanguagesByStringQuery(string query, int skip, int take, string? codigoRef) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;
            if (codigoRef != null) {
                queryObject["codigoRef"] = codigoRef;
            }
            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList<SpokenLanguagesModel>();
        } catch (Exception) {
            return new List<SpokenLanguagesModel>();
        }
    }

    public List<SpokenLanguagesModel> getSpokenLanguagesList(int skip, int take, string? codigoRef) {
        try {
            if (codigoRef != null) {
                return collection.Find(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef)).Skip(skip).Limit(take).ToList<SpokenLanguagesModel>();
            } else {
                return collection.Find(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<SpokenLanguagesModel>();
            }
        } catch (Exception) {
            return new List<SpokenLanguagesModel>();
        }
    }

    public bool logicalDeleteSpokenLanguage(string id, UserModel user) {
        try {
            List<UpdateDefinition<SpokenLanguagesModel>> updateDefinitions = new() {
                Builders<SpokenLanguagesModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<SpokenLanguagesModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<SpokenLanguagesModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(id), Builders<SpokenLanguagesModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public bool updateSpokenLanguage(SpokenLanguagesModel spokenLanguages, UserModel user) {
        try {
            SpokenLanguagesModel currentspokenLanguagesModel = collection.Find(DOC => DOC.codigo.Equals(spokenLanguages.codigo)).FirstOrDefault();
            if (currentspokenLanguagesModel == null) { return false; }

            List<UpdateDefinition<SpokenLanguagesModel>> updateDefinitions = new();
            foreach (PropertyInfo property in updateDefinitions.GetType().GetProperties()) {
                updateDefinitions.Add(Builders<SpokenLanguagesModel>.Update.Set(property.Name, property.GetValue(spokenLanguages)));
            }
            currentspokenLanguagesModel.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
            currentspokenLanguagesModel.dataController.userUpdate = user.userToken;
            updateDefinitions.Add(Builders<SpokenLanguagesModel>.Update.Set("dataController", currentspokenLanguagesModel.dataController));

            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(spokenLanguages.codigo), Builders<SpokenLanguagesModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }
}
