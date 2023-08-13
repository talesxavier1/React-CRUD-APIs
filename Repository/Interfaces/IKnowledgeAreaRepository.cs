using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IKnowledgeAreaRepository {
    public Boolean addKnowledgeArea(KnowledgeAreaModel KnowledgeAreaModel, UserModel user);
    public List<KnowledgeAreaModel> getKnowledgeAreas(int skip, int take);
    public List<KnowledgeAreaModel> getKnowledgeAreas(int skip, int take, String query);
    public KnowledgeAreaModel getKnowledgeAreaById(string codigo);
    public Boolean updateKnowledgeArea(KnowledgeAreaModel KnowledgeAreaModel, UserModel User);
    public Boolean deleteKnowledgeArea(String codigo);
    public Boolean logicalDeleteKnowledgeArea(String[] codigos, UserModel user);
    public long count();
    public long count(String query);
}
