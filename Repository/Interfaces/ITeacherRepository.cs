using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface ITeacherRepository {
    public Boolean addTeacher(TeacherModel teacher, UserModel user);
    public TeacherModel getTeacherById(String id);
    public List<TeacherModel> getTeacherByStringQuery(string query, int skip, int take);
    public List<TeacherModel> getTeacherList(int skip, int take);
    public long countTeacher(string query);
    public long countTeacher();
    public Boolean updateTeacher(TeacherModel teacher, UserModel user);
    public Boolean deleteTeacher(String id);
    public Boolean logicalDeleteTeacher(String id, UserModel user);
}
