using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IClassRepository {
    public Boolean addClass(ClassModel classModel, UserModel user);
    public ClassModel getClassById(String id);
    public List<ClassModel> getClassByStringQuery(string query, int skip, int take);
    public List<ClassModel> getClassesList(int skip, int take);
    public long countClassesByQuery(string query);
    public long countClasses();
    public Boolean updateClass(ClassModel classModel, UserModel user);
    public Boolean deleteClass(string id);
    public Boolean logicalDeleteClass(string id, UserModel user);
}
