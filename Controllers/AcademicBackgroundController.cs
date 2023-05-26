using Microsoft.AspNetCore.Mvc;
using SingularChatAPIs.Models;
using SingularChatAPIs.Repository.Implementations;
using SingularChatAPIs.ResponseModels;
using SingularChatAPIs.ResponseModels.ENUMs;

namespace SingularChatAPIs.Controllers;

[ApiController]
[Route("academicBackground")]
public class AcademicBackgroundController : Controller {

    [HttpPost]
    [Route("addAcademicBackground")]
    public ActionResult<OperationResponseModel> addAcademicBackground([FromHeader] string userToken, AcademicBackgroundModel academicBackgroundModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);


        Boolean result = new AcademicBackgroundRepository().addAcademicBackground(academicBackgroundModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = academicBackgroundModel;

        return Ok(response);
    }

    [HttpGet]
    [Route("countAcademicBackgrounds")]
    public ActionResult<OperationResponseModel> countAcademicBackgrounds([FromHeader] string userToken) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new AcademicBackgroundRepository().count();
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpPost]
    [Route("countAcademicBackgroundsByQuery")]
    public ActionResult<OperationResponseModel> countAcademicBackgroundsByQuery([FromHeader] string userToken, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        long result = new AcademicBackgroundRepository().count(query);
        response.data = result;
        response.oparationStatus = Status.OK;

        return Ok(response);
    }

    [HttpGet]
    [Route("getAcademicBackgroundById")]
    public ActionResult<OperationResponseModel> getAcademicBackgroundById([FromHeader] string userToken, [FromQuery] string codigo) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        var result = new AcademicBackgroundRepository().getAcademicBackgroundById(codigo);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpGet]
    [Route("getAcademicBackgrounds")]
    public ActionResult<OperationResponseModel> getAcademicBackgrounds([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<AcademicBackgroundModel> result = new AcademicBackgroundRepository().getAcademicBackgrounds(skip, take);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("getAcademicBackgroundsByQuery")]
    public ActionResult<OperationResponseModel> getAcademicBackgroundsByQuery([FromHeader] string userToken, [FromQuery] int skip, [FromQuery] int take, [FromBody] string query) {
        OperationResponseModel response = new();

        if (!new UserRepository().validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }

        List<AcademicBackgroundModel> result = new AcademicBackgroundRepository().getAcademicBackgrounds(skip, take, query);
        response.data = result;
        response.oparationStatus = Status.OK;
        return Ok(response);
    }

    [HttpPost]
    [Route("logicalDeleteAcademicBackground")]
    public ActionResult<OperationResponseModel> logicalDeleteAcademicBackground([FromHeader] string userToken, [FromQuery] string[] codigos) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new AcademicBackgroundRepository().logicalDeleteAcademicBackground(codigos, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;

        return Ok(response);
    }

    [HttpPost]
    [Route("modifyAcademicBackground")]
    public ActionResult<OperationResponseModel> modifyAcademicBackground([FromHeader] string userToken, AcademicBackgroundModel academicBackgroundModel) {
        UserRepository userRepository = new();
        OperationResponseModel response = new();

        if (!userRepository.validateToken(userToken)) {
            response.oparationStatus = Status.NOK;
            response.message = "userToken Inválido.";
            return StatusCode(401, response);
        }
        UserModel currentUser = userRepository.getUserByToken(userToken);

        Boolean result = new AcademicBackgroundRepository().updateAcademicBackground(academicBackgroundModel, currentUser);
        response.oparationStatus = result == true ? Status.OK : Status.NOK;
        response.data = academicBackgroundModel;
        return Ok(response);
    }
}

