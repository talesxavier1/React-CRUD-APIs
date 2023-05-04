using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IAcademicBackgroundRepository {
    public Boolean addAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel user);
    public List<AcademicBackgroundModel> getAcademicBackgrounds(string codigoRef, int skip, int take);
    public AcademicBackgroundModel getAcademicBackgroundById(string codigo);
    public Boolean updateAcademicBackground(AcademicBackgroundModel academicBackgroundModel, UserModel User);
    public Boolean deleteAcademicBackground(String codigo);
    public Boolean logicalDeleteAcademicBackground(String[] codigos, UserModel user);
    public long count(String? codigoRef);
}

