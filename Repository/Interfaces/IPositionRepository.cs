using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IPositionRepository {

    public Boolean addPosition(PositionModel positionsModel, UserModel user);
    public List<PositionModel> getPositions(int skip, int take);
    public List<PositionModel> getPositions(int skip, int take, String query);
    public PositionModel getPositionById(string codigo);
    public Boolean updatePosition(PositionModel positionsModel, UserModel User);
    public Boolean deletePosition(String codigo);
    public Boolean logicalDeletePosition(String[] codigos, UserModel user);
    public long count();
    public long count(String query);

}

