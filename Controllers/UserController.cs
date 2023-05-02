using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;
using SingularChatAPIs.utils;

namespace SingularChatAPIs.Controllers;
[ApiController]
[Route("User")]
public class UserController : ControllerBase {

    [HttpGet]
    [Route("getUserByEmailAndPass")]
    public ActionResult<UserResponseModel> getUserByEmailAndPassword(string Email, string Password) {
        UserResponseModel response = new();
        UserRepository userRepository = new();
        UserModel user = userRepository.getUserByEmailAndPassword(Email, Password);
        if (user == null) {
            response.status = Status.NOK;
            response.message = "Usuário não encontrado";
            return NotFound(response);
        } else {
            response.status = Status.OK;
            response.message = "";
            response.user = user;
            return Ok(response);
        }
    }

    [HttpGet]
    [Route("ValidateUserToken")]
    public ActionResult<ValidateTokenResponseModel> validateToken(string token) {
        ValidateTokenResponseModel response = new();
        try {
            UserRepository userRepository = new();
            bool isValid = userRepository.validateToken(token);
            response.status = Status.OK;
            response.isValid = isValid;
            return Ok(response);
        } catch (Exception ex) {
            response.status = Status.NOK;
            response.isValid = false;
            response.message = ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpGet]
    [Route("EmailExist")]
    public ActionResult<ValidateTokenResponseModel> emailExist(string email) {
        ValidateTokenResponseModel responseModel = new();

        UserRepository userRepository = new();
        bool emailExist = userRepository.userEmailExist(email);
        if (emailExist) {
            responseModel.isValid = true;
            responseModel.status = Status.OK;
            responseModel.message = "";
        } else {
            responseModel.isValid = false;
            responseModel.status = Status.OK;
            responseModel.message = "";
        }
        return StatusCode(200, responseModel);
    }

    [HttpPost]
    [Route("CreateUser")]
    public ActionResult<CreateUserResponseModel> createUser([FromHeader] string userToken, UserModel userModel) {
        CreateUserResponseModel userResponse = new();

        UserRepository userRepository = new();
        bool userTokenIsValid = userRepository.validateToken(userToken);
        if (!userTokenIsValid) {
            userResponse.status = Status.NOK;
            userResponse.message = "UserToken inválido";
            return StatusCode(401, userResponse);
        }

        ProfileRepository profileRepository = new();
        ProfileModel userContentProfile = profileRepository.getProfileByToken(userModel.profileToken);
        if (userContentProfile == null) {
            userResponse.status = Status.NOK;
            userResponse.message = "ProfileToken passado é inválido.";
            return StatusCode(404, userResponse);
        }

        if (!userContentProfile.canCreateUser) {
            userResponse.status = Status.NOK;
            userResponse.message = "Usuário não possui permissão para criar outro usuário.";
            return StatusCode(404, userResponse);
        }

        bool emailExist = userRepository.userEmailExist(userModel.email);
        if (emailExist) {
            userResponse.status = Status.NOK;
            userResponse.message = "Email inválido.";
            userResponse.id = "";
            userResponse.userToken = "";
            return StatusCode(404, userResponse);
        }

        UserModel userPost = userRepository.getUserByToken(userToken);
        ProfileModel userPostProfile = profileRepository.getProfileByToken(userPost.profileToken);
        if (userPostProfile.profileRate > userContentProfile.profileRate) {
            userResponse.status = Status.NOK;
            userResponse.message = $"Um usuário {userPostProfile.profileName} não pode criar um usuário {userContentProfile.profileName}";
            userResponse.id = "";
            userResponse.userToken = "";
            return StatusCode(404, userResponse);
        }

        UserModel createdUser = userRepository.createUser(userModel);
        if (createdUser != null) {
            userResponse.status = Status.OK;
            userResponse.message = "Usuário criado.";
            userResponse.id = createdUser.id;
            userResponse.userToken = createdUser.userToken;
            return StatusCode(201, createdUser);
        } else {
            userResponse.status = Status.NOK;
            userResponse.message = "Não foi possível criar usuário";
            return StatusCode(500, createdUser);
        }
    }

    [HttpGet]
    [Route("ADM_getUserByUserToken")]
    public ActionResult<UserResponseModel> getUserByUserToken([FromQuery] string userToken, [FromQuery] string adminAccessToken) {
        UserResponseModel response = new();
        string AdminAccessToken = AppSettings.appSetting["Admin_AccessToken"];
        if (AdminAccessToken != adminAccessToken) {
            response.status = Status.NOK;
            response.message = "AdminAccesToken Inválido";
            response.user = null;
            return StatusCode(401, response);
        }
        UserRepository userRepository = new();
        UserModel userResponse = userRepository.getUserByToken(userToken);
        if (userResponse != null) {
            response.status = Status.OK;
            response.message = "";
            response.user = userResponse;
            return StatusCode(200, response);
        } else {
            response.status = Status.NOK;
            response.message = "Usuário não encontrado";
            response.user = null;
            return StatusCode(404, response);
        }
    }
}

