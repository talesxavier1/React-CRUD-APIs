using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IWorkExperienceRepository {
    public Boolean addExperience(WorkExperienceModel experience, UserModel user);
    public WorkExperienceModel getWorkExperienceById(String id);
    public List<WorkExperienceModel> getWorkExperienceByStringQuery(string query, int skip, int take, string? codigoRef);
    public List<WorkExperienceModel> getWorkExperienceList(int skip, int take, string? codigoRef);
    public long countWorkExperiencesByQuery(string query);
    public long countWorkExperiences(string? codigoRef);
    public Boolean updateWorkExperience(WorkExperienceModel experience, UserModel user);
    public Boolean deleteWorkExperience(String id);
    public Boolean logicalDeleteWorkExperience(String id, UserModel user);
}
