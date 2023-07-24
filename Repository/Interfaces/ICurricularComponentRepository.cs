using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface ICurricularComponentRepository {
    public Boolean addCurricularComponent(CurricularComponentModel curricularComponentModel, UserModel user);
    public List<CurricularComponentModel> getCurricularComponents(int skip, int take);
    public List<CurricularComponentModel> getCurricularComponents(int skip, int take, String query);
    public CurricularComponentModel getCurricularComponentById(string codigo);
    public Boolean updateCurricularComponent(CurricularComponentModel curricularComponentModel, UserModel User);
    public Boolean deleteCurricularComponent(String codigo);
    public Boolean logicalDeleteCurricularComponent(String[] codigos, UserModel user);
    public long count();
    public long count(String query);
}
