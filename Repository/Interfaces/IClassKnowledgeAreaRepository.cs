using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IClassKnowledgeAreaRepository {
    public Boolean addClassKnowledgeArea(ClassKnowledgeAreaModel classKnowledgeAreaModel, UserModel user);
    public List<ClassKnowledgeAreaModel> getClassKnowledgeAreas(int skip, int take);
    public List<ClassKnowledgeAreaModel> getClassKnowledgeAreas(int skip, int take, String query);
    public ClassKnowledgeAreaModel getClassKnowledgeAreaById(string codigo);
    public Boolean updateClassKnowledgeArea(ClassKnowledgeAreaModel classKnowledgeAreaModel, UserModel User);
    public Boolean deleteClassKnowledgeArea(String codigo);
    public Boolean logicalDeleteClassKnowledgeArea(String[] codigos, UserModel user);
    public long count();
    public long count(String query);
}
