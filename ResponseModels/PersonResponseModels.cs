using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.ResponseModels;
public class OperationResponseModel {
    public Status oparationStatus { get; set; }
    public string? message { get; set; }
    public Object data { get; set; }
}