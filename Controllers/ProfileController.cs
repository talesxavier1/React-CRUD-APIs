using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("Profile")]
public class ProfileController : Controller {

    [HttpGet]
    [Route("ValidProfileToken")]
    public ActionResult<ValidateTokenResponseModel> ValidToken([FromHeader] string userToken, string token) {
        ValidateTokenResponseModel response = new();
        try {
            ProfileRepository profileRepository = new();
            UserRepository userRepository = new();
            bool userTokenIsValid = userRepository.validateToken(userToken);
            if (!userTokenIsValid) {
                response.isValid = false;
                response.status = Status.NOK;
                response.message = "UserToken Inválido.";
                return StatusCode(401, response);
            }

            ProfileModel profileModel = profileRepository.getProfileByToken(token);
            if (profileModel != null) {
                response.isValid = true;
                response.status = Status.OK;
                response.message = "";
                return StatusCode(200, response);
            } else {
                response.isValid = false;
                response.status = Status.OK;
                response.message = "";
                return StatusCode(404, response);
            }
        } catch (Exception ex) {
            response.isValid = false;
            response.status = Status.NOK;
            response.message = ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("GetProfiles")]
    public ActionResult<GetProfilesResponseModel> GetProfiles([FromHeader] string userToken) {
        GetProfilesResponseModel responseModel = new();

        UserRepository userRepository = new();
        bool userTokenIsValid = userRepository.validateToken(userToken);
        if (!userTokenIsValid) {
            responseModel.status = Status.NOK;
            responseModel.message = "UserToken Inválido.";
            responseModel.profiles = null;
            return StatusCode(401, responseModel);
        }

        ProfileRepository profileRepository = new();
        List<ProfileModel> profiles = profileRepository.getProfiles();
        responseModel.status = Status.OK;
        responseModel.profiles = profiles;
        responseModel.message = "";
        return StatusCode(200, responseModel);
    }

}



