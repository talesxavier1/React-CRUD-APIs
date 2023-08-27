using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface ICurriculumPlanRepository {
    public Boolean AddCurriculumPlan(CurriculumPlanModel curriculumPlanModel, UserModel user);
    public List<CurriculumPlanModel> getCurriculumPlans(int skip, int take, string? codigoRef);
    public List<CurriculumPlanModel> getCurriculumPlans(int skip, int take, String query, string? codigoRef);
    public CurriculumPlanModel getCurriculumPlanById(string codigo);
    public Boolean updateCurriculumPlan(CurriculumPlanModel curriculumPlanModel, UserModel User);
    public Boolean deleteCurriculumPlan(String codigo);
    public Boolean logicalDeleteCurriculumPlan(String[] codigos, UserModel user);
    public long count(string? codigoRef);
    public long count(String query, string? codigoRef);
}
