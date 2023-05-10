using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IAreaOfSpecializationRepository {

    public Boolean addAreaOfSpecialization(AreaOfSpecializationModel areaOfSpecializationModel, UserModel user);
    public List<AreaOfSpecializationModel> getAreasOfSpecialization(int skip, int take);
    public AreaOfSpecializationModel getAreasOfSpecializationById(string codigo);
    public Boolean updateAreaOfSpecialization(AreaOfSpecializationModel areaOfSpecializationModel, UserModel User);
    public Boolean deleteAreaOfSpecialization(String codigo);
    public Boolean logicalDeleteAreaOfSpecialization(String[] codigos, UserModel user);
    public long count();

}

