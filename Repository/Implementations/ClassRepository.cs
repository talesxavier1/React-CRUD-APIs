using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class ClassRepository : IClassRepository {

    private readonly IMongoCollection<ClassModel> collection;

    public ClassRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<ClassModel>("class");
    }

    public bool addClass(ClassModel classModel, UserModel user) {
        try {
            classModel.dataController = new ControllerModel() {
                active = true,
                postDate = DateTime.UtcNow.AddHours(-3),
                userPost = user.userToken
            };
            collection.InsertOne(classModel);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long countClasses() {
        long result = collection.CountDocuments(DOC => DOC.dataController.active == true);
        return result;
    }

    public long countClassesByQuery(string query) {
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

    public bool deleteClass(string id) {
        DeleteResult result = collection.DeleteOne(DOC => DOC.codigo.Equals(id));
        return result.DeletedCount == 1;
    }

    public ClassModel getClassById(string id) {
        try {
            return collection.Find(DOC => DOC.codigo.Equals(id)).FirstOrDefault();
        } catch (Exception) {
            return null;
        }
    }

    public List<ClassModel> getClassByStringQuery(string query, int skip, int take) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;
            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList();
        } catch (Exception) {
            return new List<ClassModel>();
        }
    }

    public List<ClassModel> getClassesList(int skip, int take) {
        try {
            return collection.Find(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList();
        } catch (Exception) {
            return new List<ClassModel>();
        }
    }

    public bool logicalDeleteClass(string id, UserModel user) {
        try {
            List<UpdateDefinition<ClassModel>> updateDefinitions = new() {
                Builders<ClassModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<ClassModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<ClassModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(id), Builders<ClassModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public bool updateClass(ClassModel classModel, UserModel user) {
        try {
            ClassModel currentClass = collection.Find(DOC => DOC.codigo.Equals(classModel.codigo)).FirstOrDefault();
            if (currentClass == null) { return false; }

            List<UpdateDefinition<ClassModel>> updateDefinitions = new();
            foreach (PropertyInfo property in currentClass.GetType().GetProperties()) {
                updateDefinitions.Add(Builders<ClassModel>.Update.Set(property.Name, property.GetValue(classModel)));
            }
            currentClass.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
            currentClass.dataController.userUpdate = user.userToken;
            updateDefinitions.Add(Builders<ClassModel>.Update.Set("dataController", currentClass.dataController));

            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(classModel.codigo), Builders<ClassModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }
}
