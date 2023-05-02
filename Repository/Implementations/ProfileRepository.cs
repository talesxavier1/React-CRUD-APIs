using MongoDB.Driver;
using SingularChatAPIs.BD;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Interfaces;

namespace SingularChatAPIs.Repository.Implementations;
public class ProfileRepository : IProfileRepository {
    private readonly IMongoCollection<ProfileModel> collection;

    public ProfileRepository() {
        IMongoDatabase database = MongoDBConnection.getMongoDatabase();
        collection = database.GetCollection<ProfileModel>("Profiles");
    }

    public ProfileModel getProfileById(string id) {
        ProfileModel profile = collection.Find(DOC => DOC.id.Equals(id)).FirstOrDefault();
        return profile;
    }

    public ProfileModel getProfileByToken(string ProfileToken) {
        ProfileModel profile = collection.Find(DOC => DOC.profileToken == ProfileToken).FirstOrDefault();
        return profile;
    }

    public List<ProfileModel> getProfiles() {
        List<ProfileModel> profiles = collection.Find(DOC => true).ToList();
        return profiles;
    }
}

