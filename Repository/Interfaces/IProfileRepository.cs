using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces {
    public interface IProfileRepository {
        public ProfileModel getProfileById(string id);
        public ProfileModel getProfileByToken(string name);
        public List<ProfileModel> getProfiles();
    }
}
