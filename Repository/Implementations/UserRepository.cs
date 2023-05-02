using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;

namespace SingularChatAPIs.Repository.Implementations;
public class UserRepository : IUserRepository {
    private readonly IMongoCollection<UserModel> collection;

    public UserRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<UserModel>("Users");
    }

    public UserModel createUser(UserModel user) {
        try {
            collection.InsertOne(user);
            return user;
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public bool disableEnableUserByUserToken(string UserToken) {
        try {
            UserModel user = getUserByToken(UserToken);
            if (user != null) {
                UpdateDefinition<UserModel> update;
                if (user.active) {
                    update = Builders<UserModel>.Update.Set("active", false);
                } else {
                    update = Builders<UserModel>.Update.Set("active", true);
                }
                UpdateResult updateResult = collection.UpdateOne(DOC => DOC.userToken.Equals(UserToken), update);
                if (updateResult != null && updateResult.ModifiedCount >= 1) {
                    return true;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public bool disableEnableUserById(string id) {
        try {
            UserModel user = getUserById(id);
            if (user != null) {
                UpdateDefinition<UserModel> update;
                if (user.active) {
                    update = Builders<UserModel>.Update.Set("active", false);
                } else {
                    update = Builders<UserModel>.Update.Set("active", true);
                }
                UpdateResult updateResult = collection.UpdateOne(DOC => DOC.id.Equals(id), update);
                if (updateResult != null && updateResult.ModifiedCount >= 1) {
                    return true;
                }
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public UserModel getUserByEmailAndPassword(String email, String password) {
        UserModel userModel = collection.Find(DOC => DOC.email == email && DOC.password == password).FirstOrDefault();
        return userModel;
    }

    public UserModel getUserById(String id) {
        UserModel userModel = collection.Find(DOC => DOC.id == id).FirstOrDefault();
        return userModel;
    }

    public UserModel getUserByToken(string token) {
        UserModel userModel = collection.Find(DOC => DOC.userToken == token).FirstOrDefault();
        return userModel;
    }

    public bool updateUser(UserModel user) {
        try {
            if (userExistByToken(user.userToken)) {
                collection.ReplaceOne(DOC => DOC.userToken.Equals(user.userToken), user);
                return true;
            }
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        return false;
    }

    public bool userEmailExist(string email) {
        long result = collection.CountDocuments(DOC => DOC.email == email);
        if (result == 0) {
            return false;
        } else {
            return true;
        }
    }

    public bool userExistByToken(string UserToken) {
        long result = collection.CountDocuments(DOC => DOC.userToken.Equals(UserToken));
        if (result == 0) {
            return false;
        } else {
            return true;
        }
    }

    public bool userExisteById(string id) {
        long result = collection.CountDocuments(DOC => DOC.id.Equals(id));
        if (result == 0) {
            return false;
        } else {
            return true;
        }
    }

    public bool validateToken(string UserToken) {
        try {
            long result = collection.CountDocuments(DOC => DOC.userToken.Equals(UserToken) && DOC.active == true);
            if (result == 0) {
                return false;
            } else {
                return true;
            }
        } catch (Exception) {
            return false;
        }
    }
}

