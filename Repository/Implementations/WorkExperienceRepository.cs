using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class WorkExperienceRepository : IWorkExperienceRepository {

    private readonly IMongoCollection<WorkExperienceModel> collection;

    public WorkExperienceRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<WorkExperienceModel>("WorkExperience");
    }

    public bool addExperience(WorkExperienceModel experience, UserModel user) {
        try {
            experience.dataController = new() {
                active = true,
                userPost = user.userToken,
                postDate = DateTime.UtcNow.AddHours(-3)
            };

            collection.InsertOne(experience);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long countWorkExperiences(string? codigoRef) {
        if (codigoRef == null) {
            return collection.CountDocuments(DOC => DOC.dataController.active == true);
        } else {
            return collection.CountDocuments(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef));
        }
    }

    public long countWorkExperiencesByQuery(string query) {
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

    public bool deleteWorkExperience(string id) {
        try {
            var result = collection.DeleteOne(DOC => DOC.codigo.Equals(id));
            return result.DeletedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public WorkExperienceModel getWorkExperienceById(string id) {
        try {
            return collection.Find(DOC => DOC.codigo.Equals(id)).FirstOrDefault<WorkExperienceModel>();
        } catch (Exception) {
            return null;
        }
    }

    public List<WorkExperienceModel> getWorkExperienceByStringQuery(string query, int skip, int take, string? codigoRef) {
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

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList<WorkExperienceModel>();
        } catch (Exception) {
            return new List<WorkExperienceModel>();
        }
    }

    public List<WorkExperienceModel> getWorkExperienceList(int skip, int take, string? codigoRef) {
        try {
            if (codigoRef != null) {
                return collection.Find(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef)).Skip(skip).Limit(take).ToList<WorkExperienceModel>();
            } else {
                return collection.Find(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<WorkExperienceModel>();
            }
        } catch (Exception) {
            return new List<WorkExperienceModel>();
        }
    }

    public bool logicalDeleteWorkExperience(string id, UserModel user) {
        try {
            List<UpdateDefinition<WorkExperienceModel>> updateDefinitions = new() {
                Builders<WorkExperienceModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<WorkExperienceModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<WorkExperienceModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(id), Builders<WorkExperienceModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public bool updateWorkExperience(WorkExperienceModel experience, UserModel user) {
        try {
            WorkExperienceModel currentworkExperience = collection.Find(DOC => DOC.codigo.Equals(experience.codigo)).FirstOrDefault();
            if (currentworkExperience == null) { return false; }

            List<UpdateDefinition<WorkExperienceModel>> updateDefinitions = new();
            foreach (PropertyInfo property in experience.GetType().GetProperties()) {
                updateDefinitions.Add(Builders<WorkExperienceModel>.Update.Set(property.Name, property.GetValue(experience)));
            }
            currentworkExperience.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
            currentworkExperience.dataController.userUpdate = user.userToken;
            updateDefinitions.Add(Builders<WorkExperienceModel>.Update.Set("dataController", currentworkExperience.dataController));

            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(experience.codigo), Builders<WorkExperienceModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }
}
