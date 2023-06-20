using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface ICourseRepository {
    public Boolean addCourse(CourseModel course, UserModel user);
    public CourseModel getCourseById(String id);
    public List<CourseModel> getCoursesByStringQuery(string query, int skip, int take, string? codigoRef);
    public List<CourseModel> getCoursesList(int skip, int take, string? codigoRef);
    public long countCoursesByQuery(string query);
    public long countCourses(string? codigoRef);
    public Boolean updateCourse(CourseModel course, UserModel user);
    public Boolean deleteCourse(String id);
    public Boolean logicalDeleteCourse(String id, UserModel user);
}
