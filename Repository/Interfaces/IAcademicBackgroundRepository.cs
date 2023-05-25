using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IAcademicBackgroundRepository {
    public Boolean addAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel user);
    public List<AcademicBackgroundModel> getAcademicBackgrounds(int skip, int take);
    public List<AcademicBackgroundModel> getAcademicBackgroundsByQuery(int skip, int take, String query);
    public AcademicBackgroundModel getAcademicBackgroundById(string codigo);
    public Boolean updateAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel User);
    public Boolean deleteAcademicBackground(String codigo);
    public Boolean logicalDeleteAcademicBackground(String[] codigos, UserModel user);
    public long count();
    public long count(String query);

}

