using SingularChatAPIs.Models;

namespace SingularChatAPIs.Repository.Interfaces;
public interface IUserRepository {
    public UserModel getUserByToken(String token);
    public UserModel getUserByEmailAndPassword(String email, String password);
    public UserModel getUserById(String id);
    public Boolean disableEnableUserByUserToken(String UserToken);
    public Boolean disableEnableUserById(String id);
    public Boolean validateToken(String UserToken);
    public UserModel createUser(UserModel user);
    public Boolean updateUser(UserModel user);
    public Boolean userExistByToken(String UserToken);
    public Boolean userExisteById(String id);
    public Boolean userEmailExist(String email);
}

