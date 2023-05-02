using SingularChatAPIs.Models;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.ResponseModels;

public class ValidateTokenResponseModel {
    public Status status { get; set; }
    public Boolean isValid { get; set; }
    public string message { get; set; }
}


public class GetProfilesResponseModel {
    public Status status { get; set; }
    public string message { get; set; }
    public List<ProfileModel>? profiles { get; set; }
}