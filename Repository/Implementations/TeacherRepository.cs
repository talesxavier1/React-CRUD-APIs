using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class TeacherRepository : ITeacherRepository {

    private readonly IMongoCollection<TeacherModel> collection;

    public TeacherRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<TeacherModel>("Teacher");
    }

    public bool addTeacher(TeacherModel teacher, UserModel user) {
        try {
            teacher.dataController = new() {
                active = true,
                userPost = user.userToken,
                postDate = DateTime.UtcNow.AddHours(-3)
            };

            collection.InsertOne(teacher);
            return true;
        } catch (Exception) {
            return false;
        }

        throw new NotImplementedException();
    }

    public long countTeacher(string query) {
        dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
        if (queryObject == null) {
            return 0;
        }
        queryObject["dataController.active"] = true;
        string finalQueryString = JsonConvert.SerializeObject(queryObject);

        return collection.Find(finalQueryString).CountDocuments();
    }

    public long countTeacher() {
        return collection.CountDocuments<TeacherModel>(DOC => DOC.dataController.active);
    }

    public bool deleteTeacher(string id) {
        try {
            DeleteResult result = collection.DeleteOne<TeacherModel>(DOC => DOC.codigo == id);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public TeacherModel getTeacherById(string id) {
        try {
            TeacherModel personModelResult = collection.Find<TeacherModel>(DOC => DOC.codigo.Equals(id) && DOC.dataController.active == true).FirstOrDefault();
            return personModelResult;
        } catch (Exception) {
            return null;
        }
    }

    public List<TeacherModel> getTeacherByStringQuery(string query, int skip, int take) {
        try {
            dynamic? queryObject = JsonConvert.DeserializeObject<dynamic>(query);
            if (queryObject == null) {
                throw new Exception();
            }
            queryObject["dataController.active"] = true;

            string finalQueryString = JsonConvert.SerializeObject(queryObject);

            return collection.Find<TeacherModel>(finalQueryString).Skip(skip).Limit(take).ToList<TeacherModel>();
        } catch (Exception) {
            return new List<TeacherModel>();
        }
    }

    public List<TeacherModel> getTeacherList(int skip, int take) {
        try {
            List<TeacherModel> result = collection.Find<TeacherModel>(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList<TeacherModel>();
            return result;
        } catch (Exception) {
            return null;
        }
    }

    public bool logicalDeleteTeacher(string id, UserModel user) {
        try {
            List<UpdateDefinition<TeacherModel>> updateDefinitions = new() {
                Builders<TeacherModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<TeacherModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<TeacherModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne<TeacherModel>(DOC => DOC.codigo.Equals(id), Builders<TeacherModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public bool updateTeacher(TeacherModel teacher, UserModel user) {
        TeacherModel currentTeacher = collection.Find<TeacherModel>(DOC => DOC.codigo.Equals(teacher.codigo)).FirstOrDefault();
        if (currentTeacher == null) { return false; }

        List<UpdateDefinition<TeacherModel>> updateDefinitions = new();
        foreach (PropertyInfo property in teacher.GetType().GetProperties()) {
            updateDefinitions.Add(Builders<TeacherModel>.Update.Set(property.Name, property.GetValue(teacher)));
        }
        currentTeacher.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
        currentTeacher.dataController.userUpdate = user.userToken;
        updateDefinitions.Add(Builders<TeacherModel>.Update.Set("dataController", currentTeacher.dataController));

        UpdateResult result = collection.UpdateOne<TeacherModel>(DOC => DOC.codigo.Equals(teacher.codigo), Builders<TeacherModel>.Update.Combine(updateDefinitions));
        return result.ModifiedCount > 0;
    }
}
