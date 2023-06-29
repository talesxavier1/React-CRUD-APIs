using MongoDB.Driver;
using Newtonsoft.Json;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;
using System.Reflection;

namespace SingularChatAPIs.Repository.Implementations;
public class CourseRepository : ICourseRepository {

    private readonly IMongoCollection<CourseModel> collection;

    public CourseRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<CourseModel>("Courses");
    }

    public bool addCourse(CourseModel course, UserModel user) {
        try {
            course.dataController = new() {
                active = true,
                userPost = user.userToken,
                postDate = DateTime.UtcNow.AddHours(-3)
            };

            collection.InsertOne(course);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public long countCourses(string? codigoRef) {
        if (codigoRef == null) {
            return collection.CountDocuments(DOC => DOC.dataController.active == true);
        } else {
            return collection.CountDocuments(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef));
        }
    }

    public long countCoursesByQuery(string query) {
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

    public bool deleteCourse(string id) {
        try {
            var result = collection.DeleteOne(DOC => DOC.codigo.Equals(id));
            return result.DeletedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public CourseModel getCourseById(string id) {
        try {
            return collection.Find(DOC => DOC.codigo.Equals(id)).FirstOrDefault();
        } catch (Exception) {
            return null;
        }
    }

    public List<CourseModel> getCoursesByStringQuery(string query, int skip, int take, string? codigoRef) {
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

            return collection.Find(finalQueryString).Skip(skip).Limit(take).ToList();
        } catch (Exception) {
            return new List<CourseModel>();
        }
    }

    public List<CourseModel> getCoursesList(int skip, int take, string? codigoRef) {
        try {
            if (codigoRef != null) {
                return collection.Find(DOC => DOC.dataController.active == true && DOC.codigoRef.Equals(codigoRef)).Skip(skip).Limit(take).ToList();
            } else {
                return collection.Find(DOC => DOC.dataController.active == true).Skip(skip).Limit(take).ToList();
            }
        } catch (Exception) {
            return new List<CourseModel>();
        }
    }

    public bool logicalDeleteCourse(string id, UserModel user) {
        try {
            List<UpdateDefinition<CourseModel>> updateDefinitions = new() {
                Builders<CourseModel>.Update.Set(DOC => DOC.dataController.updateDate, DateTime.UtcNow.AddHours(-3)),
                Builders<CourseModel>.Update.Set(DOC => DOC.dataController.userUpdate, user.userToken),
                Builders<CourseModel>.Update.Set(DOC => DOC.dataController.active, false)
            };
            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(id), Builders<CourseModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }

    public bool updateCourse(CourseModel course, UserModel user) {
        try {
            CourseModel currentCourse = collection.Find(DOC => DOC.codigo.Equals(course.codigo)).FirstOrDefault();
            if (currentCourse == null) { return false; }

            List<UpdateDefinition<CourseModel>> updateDefinitions = new();
            foreach (PropertyInfo property in course.GetType().GetProperties()) {
                updateDefinitions.Add(Builders<CourseModel>.Update.Set(property.Name, property.GetValue(course)));
            }
            currentCourse.dataController.updateDate = DateTime.UtcNow.AddHours(-3);
            currentCourse.dataController.userUpdate = user.userToken;
            updateDefinitions.Add(Builders<CourseModel>.Update.Set("dataController", currentCourse.dataController));

            UpdateResult result = collection.UpdateOne(DOC => DOC.codigo.Equals(course.codigo), Builders<CourseModel>.Update.Combine(updateDefinitions));
            return result.ModifiedCount > 0;
        } catch (Exception) {
            return false;
        }
    }
}
