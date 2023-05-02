using SingularChatAPIs.Models;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.ResponseModels;

public class UserResponseModel {
    public Status status { get; set; }
    public string message { get; set; }
    public UserModel user { get; set; }
}

public class CreateUserResponseModel {
    public Status status { get; set; }
    public string id { get; set; }
    public string userToken { get; set; }
    public string message { get; set; }
}
